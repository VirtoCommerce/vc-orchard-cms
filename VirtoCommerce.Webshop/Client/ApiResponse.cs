using VirtoCommerce.Webshop.Client.DataContracts;

namespace VirtoCommerce.Webshop.Client
{
    public class ApiResponse<T> where T : class
    {
        public T Body { get; set; }

        public ManagementError Error { get; set; }
    }
}