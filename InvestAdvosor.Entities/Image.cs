using System.ComponentModel.DataAnnotations;
using InvestAdvisor.Common.Enums;

namespace InvestAdvosor.Entities
{
    public class Image
    {
        public int ImageId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public byte[] Content { get; set; }

        public ImageType ImageType { get; set; }

        public int? ProjectId { get; set; }

        #region Navigation properties

        public virtual Project Project { get; set; }

        #endregion
    }
}
