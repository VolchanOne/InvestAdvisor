using System;

namespace InvestAdvisor.Model
{
    public class NewsModel
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
       
        public string Content { get; set; }

        public DateTime? CreatedAt { get; set; }

        public ProjectModel Project { get; set; }
    }
}
