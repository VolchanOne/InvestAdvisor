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

        public bool IsPaymentSystem { get; set; }

        public bool IsFavorite { get; set; }

        public string Marketing { get; set; }

        public string Referral { get; set; }

        public DateTime? StartDate { get; set; }

        public decimal? Invested { get; set; }

        public string Review { get; set; }

        public string Domain { get; set; }

        public string Hosting { get; set; }

        public string Ssl { get; set; }


        public DateTime CreatedAt { get; set; }


        #region Navigation properties

        public virtual List<Image> Images { get; set; }

        #endregion
    }
}
