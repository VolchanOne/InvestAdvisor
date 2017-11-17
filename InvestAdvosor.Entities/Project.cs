using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestAdvosor.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public string Url { get; set; }
        
        public string RouteName { get; set; }

        public bool IsActive { get; set; }

        public DateTime? ActivatedAt { get; set; }

        public bool InPortofolio { get; set; }

        public int? ProjectAdditionalId { get; set; }
        public int? ProjectTechId { get; set; }
        public int? ProjectReviewId { get; set; }

        #region Navigation properties
        public virtual ProjectAdditional Additional { get; set; }
        public virtual ProjectTech TechInfo { get; set; }
        public virtual ProjectReview Review { get; set; }
        public virtual List<Image> Images { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<News> News { get; set; }
        #endregion
    }
}
