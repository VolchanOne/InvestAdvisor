using System.ComponentModel;

namespace InvestAdvisor.Model
{
    public class ProjectReviewModel
    {
        public int? ProjectReviewId { get; set; }
        [DisplayName("Полный обзор")]
        public string Review { get; set; }
    }
}
