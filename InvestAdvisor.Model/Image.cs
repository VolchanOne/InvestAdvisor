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
    }
}
