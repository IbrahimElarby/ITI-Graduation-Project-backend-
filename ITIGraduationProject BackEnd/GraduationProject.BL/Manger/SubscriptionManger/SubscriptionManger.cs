using ITIGraduationProject.BL.DTO.Subscription;
using ITIGraduationProject.DAL;
using Microsoft.EntityFrameworkCore;

namespace ITIGraduationProject.BL.Manger.SubscriptionManger
{
    public class SubscriptionManger : ISubscriptionManger
    {
        private readonly ApplicationDbContext _dbContext;
        public SubscriptionManger(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaymentResponseDto> ProcessSubscription(ApplicationUser user)
        {
            var existingSubscription = await _dbContext.Subscriptions
                .Where(s => s.UserID == user.Id)
                .OrderByDescending(s => s.EndDate)
                .FirstOrDefaultAsync();

            DateTime startDate = DateTime.UtcNow;
            DateTime endDate;

            if (existingSubscription != null && existingSubscription.EndDate > DateTime.UtcNow)
            {
                startDate = existingSubscription.StartDate;
                endDate = existingSubscription.EndDate.AddMonths(1);

                existingSubscription.StartDate = startDate;
                existingSubscription.EndDate = endDate;

                _dbContext.Subscriptions.Update(existingSubscription);
            }
            else
            {
                endDate = startDate.AddMonths(1);

                _dbContext.Subscriptions.Add(new Subscription
                {
                    UserID = user.Id,
                    PlanType = "Premium",
                    StartDate = startDate,
                    EndDate = endDate
                });
            }

            await _dbContext.SaveChangesAsync();

            return new PaymentResponseDto
            {
                Status = "success",
                Message = "Subscription updated or saved",
                StartDate = startDate,
                EndDate = endDate
            };
        }
    }
}
