using System.ComponentModel;

namespace InvestAdvisor.Model
{
    public class ProjectTechModel
    {
        public int? ProjectTechId { get; set; }

        [DisplayName("Домен")]
        public string Domain { get; set; }

        [DisplayName("Хостинг")]
        public string Hosting { get; set; }

        [DisplayName("SSL")]
        public string Ssl { get; set; }
    }
}
