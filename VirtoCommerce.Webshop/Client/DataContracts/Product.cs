using System;
using System.Collections.Generic;
namespace VirtoCommerce.Webshop.Client.DataContracts
{
    public class Product
    {
        public string ManufacturerPartNumber { get; set; }

        public string Gtin { get; set; }

        public Association[] Associations { get; set; }

        public string CatalogId { get; set; }

        public string[] Categories { get; set; }

        public string Code { get; set; }

        public EditorialReview[] EditorialReviews { get; set; }

        public string Id { get; set; }

        public ItemImage[] Images { get; set; }

        public ItemImage PrimaryImage { get; set; }

        public string MainProductId { get; set; }

        public bool? TrackInventory { get; set; }

        public bool? IsBuyable { get; set; }

        public bool? IsActive { get; set; }

        public int? MaxQuantity { get; set; }

        public int? MinQuantity { get; set; }

        public string Name { get; set; }

        public string Outline { get; set; }

        public Inventory Inventory { get; set; }

        public IDictionary<string, object> Properties { get; set; }

        public IDictionary<string, object> VariationProperties { get; set; }

        public double Rating { get; set; }

        public int ReviewsTotal { get; set; }

        public SeoKeyword[] Seo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string ProductType { get; set; }

        public string WeightUnit { get; set; }

        public decimal? Weight { get; set; }

        public string MeasureUnit { get; set; }

        public decimal? Height { get; set; }

        public decimal? Length { get; set; }

        public decimal? Width { get; set; }

        public bool? EnableReview { get; set; }

        public int? MaxNumberOfDownload { get; set; }

        public DateTime? DownloadExpiration { get; set; }

        public string DownloadType { get; set; }

        public bool? HasUserAgreement { get; set; }

        public string ShippingType { get; set; }

        public string TaxType { get; set; }

        public string Vendor { get; set; }

        public Asset[] Assets { get; set; }

        public Product[] Variations { get; set; }
    }
}