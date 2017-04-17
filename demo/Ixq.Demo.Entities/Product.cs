namespace Ixq.Demo.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public virtual ProductType Type { get; set; }
    }
}