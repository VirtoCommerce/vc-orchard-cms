using Orchard.ContentManagement;

namespace VirtoCommerce.Webshop.Models
{
    public class WebshopSettingsPart : ContentPart
    {
        public string ApiUrl
        {
            get
            {
                return this.Retrieve(x => x.ApiUrl);
            }
            set
            {
                this.Store(x => x.ApiUrl, value);
            }
        }

        public string AppId
        {
            get
            {
                return this.Retrieve(x => x.AppId);
            }
            set
            {
                this.Store(x => x.AppId, value);
            }
        }

        public string SecretKey
        {
            get
            {
                return this.Retrieve(x => x.SecretKey);
            }
            set
            {
                this.Store(x => x.SecretKey, value);
            }
        }

        public string StoreId
        {
            get
            {
                return this.Retrieve(x => x.StoreId);
            }
            set
            {
                this.Store(x => x.StoreId, value);
            }
        }

        public string Culture
        {
            get
            {
                return "en-US";
            }
        }

        public string AnonymousCookieId
        {
            get
            {
                return "vcf.AnonymousId";
            }
        }

        public int PageSize
        {
            get
            {
                return 18;
            }
        }
    }
}