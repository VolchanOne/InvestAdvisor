using System;
using System.ComponentModel;

namespace InvestAdvisor.Model
{
    public class ProjectAdditionalModel
    {
        public int? ProjectAdditionalId { get; set; }

        [DisplayName("Маркетинг")]
        public string Marketing { get; set; }

        [DisplayName("Партнерская программа")]
        public string Referral { get; set; }

        [DisplayName("Дата старта")]
        public string StartDate { get; set; }
    }
}
