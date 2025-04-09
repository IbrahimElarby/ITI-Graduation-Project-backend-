using ITIGraduationProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ITIGraduationProject.BL.DTO.Subscription;
using ITIGraduationProject.BL.Manger.SubscriptionManger;

namespace ITIGraduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SubscriptionManger _subscriptionManger;
        private readonly ApplicationDbContext _dbContext;

        public SubscriptionController(IConfiguration configuration,
                                     UserManager<ApplicationUser> userManager,
                                     SubscriptionManger subscriptionManger,
                                     ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _userManager = userManager;
            _subscriptionManger = subscriptionManger;
            _dbContext = dbContext;
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return BadRequest(new PaymentResponseDto
                {
                    Status = "failed",
                    Message = "User not found"
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                CustomerEmail = userEmail,
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = "price_1RBbAFDfe4bO0vYJ9DksFyPy",
                    Quantity = 1,
                },
            },
                Mode = "subscription",
                SuccessUrl = "https://localhost:7157/api/Subscription/payment-success?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:7157/api/Subscription/payment-cancelled",
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Ok(new { url = session.Url });
        }

        [HttpGet("payment-success")]
        public async Task<IActionResult> PaymentSuccess(string session_id)
        {
            try
            {
                var service = new SessionService();
                var session = await service.GetAsync(session_id);

                if (session.PaymentStatus == "paid")
                {
                    var customerService = new CustomerService();
                    var customer = await customerService.GetAsync(session.CustomerId);

                    var user = await _userManager.FindByEmailAsync(customer.Email);
                    if (user == null)
                    {
                        return BadRequest(new PaymentResponseDto
                        {
                            Status = "failed",
                            Message = "User not found"
                        });
                    }

                    var result = await _subscriptionManger.ProcessSubscription(user);

                    return Ok(result);
                }
                else
                {
                    return BadRequest(new PaymentResponseDto
                    {
                        Status = "failed",
                        Message = "Payment not successful"
                    });
                }
            }
            catch (StripeException e)
            {
                return BadRequest(new PaymentResponseDto
                {
                    Status = "error",
                    Message = e.Message
                });
            }
        }

        [HttpGet("payment-cancelled")]
        public IActionResult PaymentCancelled()
        {
            return Ok(new PaymentResponseDto
            {
                Status = "cancelled",
                Message = "Payment was cancelled"
            });
        }

        [HttpGet("subscription-details")]
        public async Task<IActionResult> GetSubscriptionDetails(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return BadRequest(new PaymentResponseDto
                {
                    Status = "failed",
                    Message = "User not found"
                });
            }

            var subscription = await _dbContext.Subscriptions
                .Where(s => s.UserID == user.Id)
                .OrderByDescending(s => s.EndDate)
                .FirstOrDefaultAsync();

            if (subscription != null && subscription.EndDate > DateTime.UtcNow)
            {
                return Ok(new SubscriptionDto
                {
                    IsSubscribed = true,
                    PlanType = subscription.PlanType,
                    StartDate = subscription.StartDate,
                    EndDate = subscription.EndDate
                });
            }

            return Ok(new SubscriptionDto { IsSubscribed = false });
        }
    }
}