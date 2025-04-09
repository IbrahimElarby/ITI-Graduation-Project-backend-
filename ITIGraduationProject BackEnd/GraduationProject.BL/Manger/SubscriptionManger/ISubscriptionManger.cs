using ITIGraduationProject.BL.DTO.RecipeManger.Output;
using ITIGraduationProject.BL.DTO.Subscription;
using ITIGraduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.Manger.SubscriptionManger
{
    public interface ISubscriptionManger
    {
        public Task<PaymentResponseDto> ProcessSubscription(ApplicationUser user);
    }
}
