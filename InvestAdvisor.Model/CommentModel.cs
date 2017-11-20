using System;
using System.ComponentModel.DataAnnotations;

namespace InvestAdvisor.Model
{
    public class CommentModel
    {
        public int CommentId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
