using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.DTO.Subscription
{
    public class SubscriptionDto
    {
        public bool IsSubscribed { get; set; }
        public string? PlanType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
