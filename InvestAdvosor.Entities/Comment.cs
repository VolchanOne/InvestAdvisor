using System;
using System.ComponentModel.DataAnnotations;

namespace InvestAdvosor.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
