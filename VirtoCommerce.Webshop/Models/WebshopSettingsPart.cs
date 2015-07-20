using Orchard.ContentManagement;
using System.ComponentModel.DataAnnotations;

namespace VirtoCommerce.Webshop.Models
{
    public class WebshopSettingsPart : ContentPart
    {
        [Required]
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

        [Required]
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

        [Required]
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
    }
}