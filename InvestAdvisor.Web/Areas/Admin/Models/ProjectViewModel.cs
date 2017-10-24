using System.ComponentModel.DataAnnotations;
using InvestAdvisor.Model;

namespace InvestAdvisor.Web.Areas.Admin.Models
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Required]
        public string Url { get; set; }

        public int? ImageId { get; set; }

        public Image Image { get; set; }
    }
}
