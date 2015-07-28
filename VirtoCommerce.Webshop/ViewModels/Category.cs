namespace VirtoCommerce.Webshop.ViewModels
{
    public class Category
    {
        public string Id { get; set; }

        public string Slug { get; set; }

        public string Name { get; set; }

        public string UrlPattern
        {
            get
            {
                return "~/Category?id={0}";
            }
        }
    }
}