using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestAdvosor.Entities
{
    public class News
    {
        public int NewsId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? PublishedAt { get; set; }

        public int? ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
