using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InvestAdvosor.Entities
{
    public class PaymentSystem
    {
        public int PaymentSystemId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        public string ShortName { get; set; }

        public virtual List<Image> Images { get; set; }

        public string Url { get; set; }

        public string RouteName { get; set; }
    }
}
