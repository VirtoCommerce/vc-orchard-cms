using Orchard.UI.Resources;

namespace VirtoCommerce.WebStore
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineStyle("LatoFont").SetUrl("//fonts.googleapis.com/css?family=Lato:300,300italic,400,400italic,700,700italic|Shadows+Into+Light");
            manifest.DefineStyle("VirtoCommerce.WebStore").SetUrl("virtocommerce.webstore.css");

            manifest.DefineScript("Masonry").SetUrl("//cdnjs.cloudflare.com/ajax/libs/masonry/3.3.1/masonry.pkgd.min.js").SetDependencies("jQuery");
            manifest.DefineScript("VirtoCommerce.WebStore").SetUrl("virtocommerce.webstore.js").SetDependencies("jQuery");
        }
    }
}