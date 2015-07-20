using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;
using VirtoCommerce.Webshop.Models;

namespace VirtoCommerce.Webshop.Handlers
{
    public class WebshopSettingsHandler : ContentHandler
    {
        public WebshopSettingsHandler()
        {
            T = NullLocalizer.Instance;

            Filters.Add(new ActivatingFilter<WebshopSettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<WebshopSettingsPart>("WebshopSettings", "Parts/WebshopSettings", "settings"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
            {
                return;
            }

            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("VirtoCommerce Webshop")) { Id = "settings" });
        }
    }
}