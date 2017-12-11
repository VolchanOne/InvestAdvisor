using InvestAdvisor.Model;
using InvestAdvosor.Entities;

namespace InvestAdvisor.Services.Converters
{
    public static class ImageConverter
    {
        public static ImageModel ToImageModel(this Image image)
        {
            return new ImageModel
            {
                ImageId = image.ImageId,
                Name = image.Name,
                ImageType = image.ImageType,
                Content = image.Content
            };
        }
    }
}
