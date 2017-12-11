using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.DynamicFilters;
using Ixq.Core.Entity;
using Ixq.Core.Repository;

namespace Ixq.Data.Repository
{
    /// <summary>
    ///     数据库上下文基类
    /// </summary>
    public abstract class DbContextBase : DbContext, IUnitOfWork
    {
        /// <summary>
        ///     使用约定构造一个新的上下文实例以创建将连接到的数据库的名称。按照约定，该名称是派生上下文类的全名（命名空间与类名称的组合）。请参见有关这如何用于创建连接的类备注。
        /// </summary>
        protected DbContextBase()
        {
        }

        /// <summary>
        ///     可以将给定字符串用作将连接到的数据库的名称或连接字符串来构造一个新的上下文实例。请参见有关这如何用于创建连接的类备注。
        /// </summary>
        /// <param name="nameOrConnectionString">数据库名称或连接字符串。</param>
        protected DbContextBase(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        /// <summary>
        ///     使用约定构造一个新的上下文实例以创建将连接到的数据库的名称，并从给定模型初始化该名称。按照约定，该名称是派生上下文类的全名（命名空间与类名称的组合）。请参见有关这如何用于创建连接的类备注。
        /// </summary>
        /// <param name="model">支持此上下文的模型。</param>
        protected DbContextBase(DbCompiledModel model) : base(model)
        {
        }

        /// <summary>
        ///     通过现有连接来连接到数据库以构造一个新的上下文实例。如果 contextOwnsConnection 是 false，则释放上下文时将不会释放该连接。
        /// </summary>
        /// <param name="existingConnection">要用于新的上下文的现有连接。</param>
        /// <param name="contextOwnsConnection">如果设置为 true，则释放上下文时将释放该连接；否则调用方必须释放该连接。</param>
        protected DbContextBase(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <summary>
        ///     可以将给定字符串用作将连接到的数据库的名称或连接字符串来构造一个新的上下文实例，并从给定模型初始化该实例。请参见有关这如何用于创建连接的类备注。
        /// </summary>
        /// <param name="nameOrConnectionString">数据库名称或连接字符串。</param>
        /// <param name="model">支持此上下文的模型。</param>
        protected DbContextBase(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        /// <summary>
        ///     通过使用现有连接来连接到数据库以构造一个新的上下文实例，并从给定模型初始化该实例。如果 contextOwnsConnection 是 false，则释放上下文时将不会释放该连接。
        /// </summary>
        /// <param name="existingConnection">要用于新的上下文的现有连接。</param>
        /// <param name="model">支持此上下文的模型。</param>
        /// <param name="contextOwnsConnection">如果设置为 true，则释放上下文时将释放该连接；否则调用方必须释放该连接。</param>
        protected DbContextBase(DbConnection existingConnection, DbCompiledModel model,
            bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <summary>
        ///     异步提交当前单元操作的更改。
        /// </summary>
        /// <returns>操作影响的行数</returns>
        public virtual Task<int> SaveAsync()
        {
            return SaveChangesAsync(true);
        }

        /// <summary>
        ///     回滚事务
        /// </summary>
        public virtual void Rollback()
        {
            Database.CurrentTransaction?.Rollback();
        }

        /// <summary>
        ///     提交当前单元操作的更改
        /// </summary>
        /// <returns>操作影响的行数</returns>
        public virtual int Save()
        {
            return SaveChanges(true);
        }

        /// <summary>
        ///     在此上下文上运行注册的 System.Data.Entity.IDatabaseInitializer`1。如果将“force”设置为
        ///     true，则将运行初始值设定项，不管它之前是否已运行。如果在应用程序正在运行时删除了数据库并且需要重新初始化数据库时，则这样做会很有用。如果将“force”设置为
        ///     false，则仅在尚未为此应用程序域中的此上下文、模型和连接运行初始值设定项的情况下运行它。当必须确保在开始某些操作之前已创建数据库并设定其种子时（这样偷懒的做法会导致问题，例如，当操作是事务的一部分时），通常会使用此方法。
        /// </summary>
        protected virtual void Initialize()
        {
            Database.Initialize(false);
        }

        /// <summary>
        ///     提交当前单元操作的更改
        /// </summary>
        /// <param name="validateOnSaveEnabled">提交保存时是否验证实体约束有效性。</param>
        /// <returns>操作影响的行数</returns>
        internal virtual int SaveChanges(bool validateOnSaveEnabled)
        {
            var isReturn = Configuration.ValidateOnSaveEnabled != validateOnSaveEnabled;
            try
            {
                Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;

                int count;
                try
                {
                    OnSaveChangeComplete();
                    count = base.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    var errorResults = ex.EntityValidationErrors;
                    var ls = (from result in errorResults
                        let lines =
                            result.ValidationErrors.Select(
                                error => $"{error.PropertyName}: {error.ErrorMessage}").ToArray()
                        select
                            $"{result.Entry.Entity.GetType().FullName}({string.Join(",", lines)})").ToList();
                    var message = "数据验证引发异常——" + string.Join(" | ", ls);
                    throw new DataException(message, ex);
                }

                return count;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    var sqlEx = e.InnerException.InnerException as SqlException;
                    var errorMessages = new StringBuilder();
                    for (var i = 0; i < sqlEx.Errors.Count; i++)
                        errorMessages.Append("Index #" + i + "\n" +
                                             "Message: " + sqlEx.Errors[i].Message + "\n" +
                                             "Error Number: " + sqlEx.Errors[i].Number + "\n" +
                                             "LineNumber: " + sqlEx.Errors[i].LineNumber + "\n" +
                                             "Source: " + sqlEx.Errors[i].Source + "\n" +
                                             "Procedure: " + sqlEx.Errors[i].Procedure + "\n");
                    throw new Exception("提交数据更新时发生异常：" + errorMessages, sqlEx);
                }
                throw;
            }
            finally
            {
                if (isReturn)
                    Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
            }
        }

        /// <summary>
        ///     提交当前单元操作的更改。
        /// </summary>
        /// <param name="validateOnSaveEnabled">提交保存时是否验证实体约束有效性。</param>
        /// <returns>操作影响的行数</returns>
        internal virtual async Task<int> SaveChangesAsync(bool validateOnSaveEnabled)
        {
            var isReturn = Configuration.ValidateOnSaveEnabled != validateOnSaveEnabled;
            try
            {
                Configuration.ValidateOnSaveEnabled = validateOnSaveEnabled;

                var count = 0;
                try
                {
                    OnSaveChangeComplete();
                    count = await base.SaveChangesAsync();
                }
                catch (DbEntityValidationException ex)
                {
                    var errorResults = ex.EntityValidationErrors;
                    var ls = (from result in errorResults
                        let lines =
                            result.ValidationErrors.Select(
                                error => $"{error.PropertyName}: {error.ErrorMessage}").ToArray()
                        select
                            $"{result.Entry.Entity.GetType().FullName}({string.Join(",", lines)})").ToList();
                    var message = "数据验证引发异常——" + string.Join(" | ", ls);
                    throw new DataException(message, ex);
                }

                return count;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    var sqlEx = e.InnerException.InnerException as SqlException;
                    var errorMessages = new StringBuilder();
                    for (var i = 0; i < sqlEx.Errors.Count; i++)
                        errorMessages.Append("Index #" + i + "\n" +
                                             "Message: " + sqlEx.Errors[i].Message + "\n" +
                                             "Error Number: " + sqlEx.Errors[i].Number + "\n" +
                                             "LineNumber: " + sqlEx.Errors[i].LineNumber + "\n" +
                                             "Source: " + sqlEx.Errors[i].Source + "\n" +
                                             "Procedure: " + sqlEx.Errors[i].Procedure + "\n");
                    throw new Exception("提交数据更新时发生异常：" + errorMessages, sqlEx);
                }
                throw;
            }
            finally
            {
                if (isReturn)
                    Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
            }
        }

        /// <summary>
        ///     在调用 <see cref="SaveChanges" />、<see cref="SaveChangesAsync" /> 方法时会执行此方法。
        /// </summary>
        protected virtual void OnSaveChangeComplete()
        {
            var entries = ChangeTracker.Entries().ToList();

            foreach (var entry in entries)
                switch (entry.State)
                {
                    case EntityState.Added:
                        AutofillGuid(entry);
                        ApplyICreateSpecification(entry);

                        break;
                    case EntityState.Modified:
                        ApplyIUpdataSpecification(entry);

                        break;
                    case EntityState.Deleted:
                        ApplyISoftDeleteSpecification(entry);

                        break;
                }
        }


        /// <summary>
        ///     自动填充Guid。
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void AutofillGuid(DbEntityEntry entry)
        {
            if (!(entry.Entity is IEntity<Guid>))
                return;
            var entityEntry = entry.Cast<IEntity<Guid>>();
            if (entityEntry.Entity.Id == Guid.Empty)
                entityEntry.Entity.Id = Guid.NewGuid();
        }

        /// <summary>
        ///     应用 <see cref="ISoftDeleteSpecification" />。
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void ApplyISoftDeleteSpecification(DbEntityEntry entry)
        {
            if (!(entry.Entity is ISoftDeleteSpecification))
                return;
            var softDeleteEntry = entry.Cast<ISoftDeleteSpecification>();
            softDeleteEntry.State = EntityState.Modified;
            softDeleteEntry.Entity.DeleteDate = DateTime.Now;
            softDeleteEntry.Entity.IsDeleted = true;
            softDeleteEntry.Entity.OnSoftDeleteComplete();
        }

        /// <summary>
        ///     应用 <see cref="IUpdataSpecification" />。
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void ApplyIUpdataSpecification(DbEntityEntry entry)
        {
            if (!(entry.Entity is IUpdataSpecification))
                return;
            var upDataEntry = entry.Cast<IUpdataSpecification>();
            upDataEntry.Entity.UpdataDate = DateTime.Now;
            upDataEntry.Entity.OnUpdataComplete();
        }

        /// <summary>
        ///     应用 <see cref="ICreateSpecification" />。
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void ApplyICreateSpecification(DbEntityEntry entry)
        {
            if (!(entry.Entity is ICreateSpecification))
                return;
            var createEntry = entry.Cast<ICreateSpecification>();
            createEntry.Entity.CreateDate = DateTime.Now;
            createEntry.Entity.OnCreateComplete();
        }

        /// <summary>
        ///     在完成对派生上下文的模型的初始化后，并在该模型已锁定并用于初始化上下文之前，将调用此方法。
        ///     此方法内部关闭了级联删除，以及启用了删除过滤器。所有实现了 <see cref="ISoftDeleteSpecification" /> 接口的实体在删除时只逻辑删除。
        /// </summary>
        /// <param name="modelBuilder">定义要创建的上下文的模型的生成器。</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 禁用 级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            // 软删除过滤器，通过仓储接口将不能查询到，但直接执行SQL查询语句，仍可查询。
            modelBuilder.Filter("SoftDelete", (ISoftDeleteSpecification d) => d.IsDeleted, false);
        }
    }
}