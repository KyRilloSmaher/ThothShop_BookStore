using Microsoft.Extensions.DependencyInjection;
using ThothShop.Domain.Models;
using ThothShop.Infrastructure.Contracts;
using ThothShop.Infrastructure.Implementations;
using ThothShop.Infrastructure.Implementions;
using ThothShop.Infrastructure.RepositoriesContracts;
using ThothShop.Infrastructure.RepositoriesImplementions;


namespace ThothShop.Infrastructure
{
    public static class ModuleInfrastractureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services) {

            services.AddTransient<IAuthorRepository , AuthorRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IUsedBookRepository, UsedBookRepository>();
            services.AddTransient<ICartRepository,CartRepositroy>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IBookImageRepository, BookmageRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IWishListRepository, WishListRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IAuthorCategoriesRepository, AuthorCategoriesRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IAuthorImageRepository, AuthormageRepository>();



            return services;
        }
    }
}
