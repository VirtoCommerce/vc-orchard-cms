using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;
using VirtoCommerce.WebStore.Models;

namespace VirtoCommerce.WebStore.Handlers
{
    public class WebStoreSettingsPartHandler : ContentHandler
    {
        public WebStoreSettingsPartHandler()
        {
            T = NullLocalizer.Instance;

            Filters.Add(new ActivatingFilter<WebStoreSettingsPart>("Site"));
            Filters.Add(new TemplateFilterForPart<WebStoreSettingsPart>("WebStoreSettings", "Parts/WebStoreSettings", "WebStore"));
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
            {
                return;
            }

            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("VirtoCommerce WebStore")) { Id = "WebStore" });
        }
    }
}