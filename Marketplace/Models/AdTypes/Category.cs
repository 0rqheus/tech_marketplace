namespace Marketplace.Models
{
    public class Category
    {
        public string CategoryName { get; set; }

        public Category()
        { }

        public Category(string category)
        {
            CategoryName = category;
        }
    }
}
