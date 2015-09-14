using VirtoCommerce.WebStore.ViewModels;
using DataContracts = VirtoCommerce.ApiClient.DataContracts;

namespace VirtoCommerce.WebStore.Converters
{
    public static class ImageConverter
    {
        public static Image ToViewModel(this DataContracts.ItemImage image)
        {
            var imageViewModel = new Image();

            imageViewModel.AlternateText = image.Name;
            imageViewModel.Url = image.Src;

            return imageViewModel;
        }
    }
}