using Orchard.UI.Resources;

namespace VirtoCommerce.Webshop
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineStyle("LatoFont").SetUrl("//fonts.googleapis.com/css?family=Lato");
            manifest.DefineStyle("Webshop").SetUrl("webshop.css");
        }
    }
}