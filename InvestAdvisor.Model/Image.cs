using System.ComponentModel.DataAnnotations;

namespace InvestAdvisor.Model
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
