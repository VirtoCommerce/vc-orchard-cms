using System.Collections.Generic;

namespace VirtoCommerce.Webshop.Client.DataContracts.Order
{
    public class ShipmentPackage
    {
        public string BarCode { get; set; }

        public string PackageType { get; set; }

        public ICollection<ShipmentItem> Items { get; set; }

        public string WeightUnit { get; set; }

        public decimal? Weight { get; set; }

        public string MeasureUnit { get; set; }

        public decimal? Height { get; set; }

        public decimal? Length { get; set; }

        public decimal? Width { get; set; }
    }
}