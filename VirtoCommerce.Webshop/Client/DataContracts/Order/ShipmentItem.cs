namespace VirtoCommerce.Webshop.Client.DataContracts.Order
{
    public class ShipmentItem
    {
        public string Id { get; set; }

        public string LineItemId { get; set; }

        public LineItem LineItem { get; set; }

        public string BarCode { get; set; }

        public int Quantity { get; set; }
    }
}