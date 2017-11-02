using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestAdvosor.Entities
{
    //TODO:
    //Deposits
    //Withdrawal
    //Refbacks
    //Comments

    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(256)]
        public string Description { get; set; }

        public string Url { get; set; }

        public DateTime? PublishedAt { get; set; }

        public int? ProjectAdditionalId { get; set; }
        public int? ProjectTechId { get; set; }
        public int? ProjectReviewId { get; set; }

        #region Navigation properties
        public virtual ProjectAdditional Additional { get; set; }
        public virtual ProjectTech TechInfo { get; set; }
        public virtual ProjectReview Review { get; set; }
        public virtual List<Image> Images { get; set; }
        #endregion
    }
}
