using Microsoft.Extensions.DependencyInjection;

using ThothShop.Service.Contract;
using ThothShop.Service.implementations;
using ThothShop.Service.Implementations;

namespace ThothShop.Service
{
    
        public static class ModuleServicesDependencies
        {
            public static IServiceCollection AddServciesDependencies(this IServiceCollection services)
            {

                services.AddTransient<IAuhtorService, AuhtorService>();
                services.AddTransient<IAuthenticationService, AuthenticationService>();
                services.AddTransient<IAuthorizationService, AuthorizationService>();
                services.AddTransient<IBookService, BookService>();
                services.AddTransient<IBookImageService, BookImageService>();
                services.AddTransient<ICartService, CartService>();
                services.AddTransient<ICategoryService, CategoryService>();
                services.AddTransient<IEmailService, EmailService>();
                services.AddTransient<IUsedBookService, UsedBookService>();
                services.AddTransient<IReviewService, ReviewService>();
                services.AddTransient<IWishListService, WishListService>();
                services.AddTransient<IOrderService, OrderService>();
                services.AddTransient<IUserService, UserService>();
                services.AddTransient<IImageUploaderService, ImageUploaderService>();
                services.AddTransient<IPaymentService, PaymentService>();
                services.AddTransient<IPaymentProcessService, PaymentProcessService>();


            return services;
            }
        }
    
}
