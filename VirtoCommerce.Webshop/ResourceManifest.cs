using System;
using Orchard.UI.Resources;

namespace VirtoCommerce.Webshop
{
    public class ResourceManifest : IResourceManifestProvider
    {
        public void BuildManifests(ResourceManifestBuilder builder)
        {
            var manifest = builder.Add();

            manifest.DefineStyle("LatoFont").SetUrl("//fonts.googleapis.com/css?family=Lato:300,300italic,400,400italic,700,700italic|Shadows+Into+Light");
            manifest.DefineStyle("Webshop").SetUrl("webshop.css");
        }
    }
}