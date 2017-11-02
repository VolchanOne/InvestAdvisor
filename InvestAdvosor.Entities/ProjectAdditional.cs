using System;

namespace InvestAdvosor.Entities
{
    public class ProjectAdditional
    {
        public int ProjectAdditionalId { get; set; }

        public string Legend { get; set; }

        public string Marketing { get; set; }

        public string Referral { get; set; }

        public DateTime StartDate { get; set; }
    }
}
