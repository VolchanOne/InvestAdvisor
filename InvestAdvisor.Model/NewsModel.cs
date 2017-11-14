using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace InvestAdvisor.Model
{
    public class NewsModel
    {
        public NewsModel()
        {
            ListProjects = new List<SelectListItem>();
        }
        public int NewsId { get; set; }

        [DisplayName("Заголовок")]
        public string Title { get; set; }
       
        [DisplayName("Контент")]
        public string Content { get; set; }

        public bool? IsPublished { get; set; }

        public ProjectModel Project { get; set; }

        public List<SelectListItem> ListProjects { get; set; }
        public int? SelectedProject { get; set; }
    }
}
