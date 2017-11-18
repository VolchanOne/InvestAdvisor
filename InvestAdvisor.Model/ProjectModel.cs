using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace InvestAdvisor.Model
{
    public class ProjectModel
    {
        public ProjectModel()
        {
            Additional = new ProjectAdditionalModel();
            Review = new ProjectReviewModel();
            Images = new List<ImageModel>();
        }

        public int ProjectId { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Ссылка")]
        public string Url { get; set; }

        public string RouteName { get; set; }

        public bool IsActive { get; set; }

        public DateTime? ActivatedAt { get; set; }

        public bool InPortofolio { get; set; }

        public ProjectAdditionalModel Additional { get; set; }

        public ProjectReviewModel Review { get; set; }

        public ProjectTechModel TechInfo { get; set; }

        public List<ImageModel> Images { get; set; }

        public string Address
        {
            get
            {
                try
                {
                    return new Uri(Url).Host;
                }
                catch (Exception)
                {
                    return Url;
                }                
            }
        }
    }
}
