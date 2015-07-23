namespace VirtoCommerce.Webshop.Client.DataContracts
{
    public enum AddressType
    {
        Billing = 1,
        Shipping = 2,
        BillingAndShipping = Billing | Shipping
    }
}