using System.ComponentModel.DataAnnotations;

namespace InvestAdvisor.Model
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Legend { get; set; }

        [Required]
        public string Url { get; set; }

        public int? ImageId { get; set; }
        public Image Image { get; set; }
    }
}
