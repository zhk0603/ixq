namespace Ixq.Demo.Entities
{
    public class ProductType : EntityBase
    {
        public string Name { get; set; }
        public string SortCode { get; set; }
        public virtual ProductType ParentType { get; set; }
    }
}