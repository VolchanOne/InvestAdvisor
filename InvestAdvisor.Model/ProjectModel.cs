using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InvestAdvisor.Model
{
    public class ProjectModel
    {
        public int ProjectId { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Ссылка")]
        public string Url { get; set; }

        public ProjectAdditionalModel Additional { get; set; }

        public List<ImageModel> Images { get; set; }
    }
}
