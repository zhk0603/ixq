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
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration;

namespace Ixq.Security.Identity
{
    public abstract class IdentityDbContextBase<TUser, TRole> :
        IdentityDbContext<TUser, TRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUnitOfWork
        where TUser : IdentityUser
        where TRole : IdentityRole
    {
        #region 构造函数。
        protected IdentityDbContextBase() : base()
        {
        }

        protected IdentityDbContextBase(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        protected IdentityDbContextBase(DbCompiledModel model) : base(model)
        {

        }

        protected IdentityDbContextBase(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected IdentityDbContextBase(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {

        }

        protected IdentityDbContextBase(DbConnection existingConnection, DbCompiledModel model,
            bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
        {

        }

        #endregion
        public void Rollback()
        {
            Database.CurrentTransaction?.Rollback();
        }

        public int Save()
        {
            return SaveChanges(true);
        }

        public Task<int> SaveAsync()
        {
            return SaveChangesAsync(true);
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
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                                             "Message: " + sqlEx.Errors[i].Message + "\n" +
                                             "Error Number: " + sqlEx.Errors[i].Number + "\n" +
                                             "LineNumber: " + sqlEx.Errors[i].LineNumber + "\n" +
                                             "Source: " + sqlEx.Errors[i].Source + "\n" +
                                             "Procedure: " + sqlEx.Errors[i].Procedure + "\n");
                    }
                    throw new Exception("提交数据更新时发生异常：" + errorMessages, sqlEx);
                }
                throw;
            }
            finally
            {
                if (isReturn)
                {
                    Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
                }
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
                    {
                        errorMessages.Append("Index #" + i + "\n" +
                                             "Message: " + sqlEx.Errors[i].Message + "\n" +
                                             "Error Number: " + sqlEx.Errors[i].Number + "\n" +
                                             "LineNumber: " + sqlEx.Errors[i].LineNumber + "\n" +
                                             "Source: " + sqlEx.Errors[i].Source + "\n" +
                                             "Procedure: " + sqlEx.Errors[i].Procedure + "\n");
                    }
                    throw new Exception("提交数据更新时发生异常：" + errorMessages, sqlEx);
                }
                throw;
            }
            finally
            {
                if (isReturn)
                {
                    Configuration.ValidateOnSaveEnabled = !validateOnSaveEnabled;
                }
            }
        }

        protected virtual void OnSaveChangeComplete()
        {
            var entries = ChangeTracker.Entries().ToList();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        AutofillGuid(entry);
                        AuditImplodedICreateSpecification(entry);

                        break;
                    case EntityState.Modified:
                        AuditImplodedIUpdataSpecification(entry);

                        break;
                    case EntityState.Deleted:
                        AuditImplodedISoftDeleteSpecification(entry);

                        break;
                }
            }
        }


        /// <summary>
        ///     自动填充Guid
        /// </summary>
        /// <param name="entry"></param>
        protected virtual void AutofillGuid(DbEntityEntry entry)
        {
            if (!(entry.Entity is IEntity<Guid>))
            {
                return;
            }
            var entityEntry = entry.Cast<IEntity<Guid>>();
            if (entityEntry.Entity.Id == Guid.Empty)
            {
                entityEntry.Entity.Id = Guid.NewGuid();
            }
        }

        protected virtual void AuditImplodedISoftDeleteSpecification(DbEntityEntry entry)
        {
            if (!(entry.Entity is ISoftDeleteSpecification))
            {
                return;
            }
            var softDeleteEntry = entry.Cast<ISoftDeleteSpecification>();
            softDeleteEntry.State = EntityState.Modified;
            softDeleteEntry.Entity.DeleteDate = DateTime.Now;
            softDeleteEntry.Entity.IsDeleted = true;
            softDeleteEntry.Entity.OnSoftDeleteComplete();
        }

        protected virtual void AuditImplodedIUpdataSpecification(DbEntityEntry entry)
        {
            if (!(entry.Entity is IUpdataSpecification))
            {
                return;
            }
            var upDataEntry = entry.Cast<IUpdataSpecification>();
            upDataEntry.Entity.UpdataDate = DateTime.Now;
            upDataEntry.Entity.OnUpdataComplete();
        }

        protected virtual void AuditImplodedICreateSpecification(DbEntityEntry entry)
        {
            if (!(entry.Entity is ICreateSpecification))
            {
                return;
            }
            var createEntry = entry.Cast<ICreateSpecification>();
            createEntry.Entity.CreateDate = DateTime.Now;
            createEntry.Entity.OnCreateComplete();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 禁用 级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            // 移除 表名复数
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // 软删除过滤器，通过仓储接口将不能查询到，但直接执行SQL查询语句，仍可查询。
            modelBuilder.Filter("SoftDelete", (ISoftDeleteSpecification d) => d.IsDeleted, false);
        }
    }
}