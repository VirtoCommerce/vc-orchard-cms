using Orchard.ContentManagement;

namespace VirtoCommerce.WebStore.Models
{
    public class WebStoreSettingsPart : ContentPart
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

        public string ApplicationId
        {
            get
            {
                return this.Retrieve(x => x.ApplicationId);
            }
            set
            {
                this.Store(x => x.ApplicationId, value);
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
                return "eu-US";
            }
        }

        public string AnonymousCookieId
        {
            get
            {
                return "vc-anonymous-customer-id";
            }
        }

        public int PageSize
        {
            get
            {
                int pageSize = this.Retrieve(x => x.PageSize);

                return pageSize > 0 ? pageSize : 20;
            }
            set
            {
                this.Store(x => x.PageSize, value);
            }
        }
    }
}