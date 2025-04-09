using ITIGraduationProject.BL.DTO.Subscription;
using ITIGraduationProject.DAL;

namespace ITIGraduationProject.BL.Manger.SubscriptionManger
{
    public interface ISubscriptionManger
    {
        public Task<PaymentResponseDto> ProcessSubscription(ApplicationUser user);
    }
}
