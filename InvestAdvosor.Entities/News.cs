using System;
using System.ComponentModel.DataAnnotations;

namespace InvestAdvosor.Entities
{
    public class News
    {
        public int NewsId { get; set; }

        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}
