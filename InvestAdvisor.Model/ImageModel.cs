using System.ComponentModel.DataAnnotations;
using InvestAdvisor.Common.Enums;

namespace InvestAdvisor.Model
{
    public class ImageModel
    {
        public int ImageId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public byte[] Content { get; set; }

        public ImageType ImageType { get; set; }
    }
}
