namespace VirtoCommerce.Webshop.Client.DataContracts
{
    public class ManagementError
    {
        public string Message { get; set; }

        public string ExceptionMessage { get; set; }

        public string ExceptionType { get; set; }

        public string StackTrace { get; set; }
    }
}