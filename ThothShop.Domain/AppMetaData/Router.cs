using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.AppMetaData
{
    public static class Router
    {
        public const string SignleRoute = "/{Id:GUID}";
        public const string root = "ThothShopAPI";
        public const string Rule = root + "/";


        #region Author EndPoints-Routes
        public static class Author
        {
            //******** HttpGet ********//
            public const string Prefix = Rule + "Authors";
            public const string GetById = Prefix + SignleRoute;
            public const string GetAll = Prefix;
            public const string GetAllAuthorPaginted = Prefix + "/Paginted";
            public const string FilterAuthors = Prefix + "/Filter-Authors";
            public const string PopularAuthors = Prefix + "/Most-Popular-Authors";
            public const string TotalAuthors = Prefix + "/Author-count";

            //******** HttpPost ********//
            public const string CreateAuthor = Prefix + "/Create";

            //******** HttpPut ********//
            public const string UpdateAuthor = Prefix + "/Update";

            //******** HttpDelete ********//
            public const string DeleteAuthor = Prefix + "/Delete" + SignleRoute;


        }
        #endregion

        #region Category EndPoints-Routes
        public static class Categories {

            //******** HttpGet ********//
            public const string Prefix = Rule + "Categories";
            public const string GetById = Prefix + SignleRoute;
            public const string GetByName = Prefix + "/Name/{name:alpha}";
            public const string GetAll = Prefix;
            public const string GetPopularCategories = Prefix + "/Most-Popular";
            public const string GetBooksCountInCategory = Prefix + SignleRoute+ "/Books-Count";

            //******** HttpPost ********//
            public const string CreateCategory = Prefix + "/Create";
            public const string AddAuthorToCategories = Prefix + "/Add-Author-To-Categories";

            //******** HttpPut ********//
            public const string UpdateCategory = Prefix + "/Update";

            //******** HttpDelete ********//
            public const string DeleteCategory = Prefix + "/Delete" + SignleRoute;
        }

        #endregion

        #region Books EndPoints-Routes
        public static class Books
        {

            //******** HttpGet ********//
                public const string Prefix = Rule + "Books";
                public const string GetById = Prefix + SignleRoute;
                public const string GetTotalNumberofBooks = Prefix + "/Total-Number-Of-Books";
                public const string GetAllBooks = Prefix + "/All";
                public const string GetBooksByAuthorId = Prefix + "/Author/{authorId}";
                public const string GetBooksByAuthorIdOrderedByViewCount = Prefix + "/Author/{authorId}/ordered-By-ViewCount";
                public const string GetBooksByCategoryId = Prefix + "/Category/{categoryId}";
                public const string GetTopRatedBooks = Prefix + "/Top-Rated";
                public const string SearchBooks = Prefix + "/Filter-Books";
                public const string GetBooksByAuthorOrderedByViews = Prefix + "/Author/{authorId}" + "/Ordered-By-Views";
                public const string GetBooksOrderedByStockASC = Prefix + "/ordered-by-stock/asc";
                public const string GetBooksOrderedByStockDESC = Prefix + "/ordered-by-stock/desc";
                public const string GetTopSellingBooks = Prefix + "/top-selling";
                public const string GetnewlyReleaseBooks = Prefix + "/new-releases";
                public const string GetOutOfStockBooks = Prefix + "/out-of-stock";
                public const string GetLowStockBooks = Prefix + "/low-stock/{threshold}";
                public const string GetBooksByBothAuthorAndCategory = Prefix + "/Author/{authorId}/Category/{categoryId}";
                public const string GetSimilarBooks = Prefix + SignleRoute + "/Similar";
                public const string GetBooksCount = Prefix + "/Count";
                public const string GetAverageRating = Prefix + SignleRoute + "/Average-Rating";
                public const string GetAuthorsOfBook = Prefix + SignleRoute + "/Book/Authors";
            //******** HttpPost ********//
            public const string CreateBook = Prefix + "/Create";



            //******** HttpPut ********//

            public const string UpdateBook = Prefix + "/Update";
            public const string IncreaseBookStock = Prefix + "/IncreaseStock";
            public const string DecreaseBookStock = Prefix + "/DecreaseStock";

            //******** HttpDelete ********//
            public const string DeleteBook = Prefix + "/Delete" + SignleRoute;


        }

        #endregion

        #region BookShelf EndPoints_Routes

        public static class userBookShelf
        {
            //******** HttpGet ********//
            public const string Prefix = Rule + "BookShelf";
            public const string GetAllBookShelves = Prefix + "/Available-Books";
            public const string GetMyBookShelf = Prefix+"/my";
            public const string GetUsedBookDetailsById= Prefix + "/Books" + SignleRoute;
            public const string SearchUsedBooks = Prefix + "/Books/Search";
            public const string FilterUsedBooksByCondition = Prefix + "/Books/Filter-By-Condition";

            //******** HttpPost ********//
            public const string CreateUsedBook = Prefix + "/my/Add-Used-Bood";

            //******** HttpPut ********//
            public const string UpdateUsedBook = Prefix + "/my/Update-Used-Bood";

            //******** HttpDelete ********//
            public const string DeleteUsedBook = Prefix + "/my/Delete-Used-Bood" + SignleRoute;

    }
        #endregion

        #region User Endpoints-Routes
        public static class User
        {
            public const string Prefix = Rule + "Users";
            public const string GetById = Prefix + SignleRoute;
            public const string MyProfile = Prefix + "/MyProfile";
            public const string GetAllUsers = Prefix + "/All-Users";
            public const string GetAllAdmins = Prefix + "/All-Admins";
            public const string GetByEmail = Prefix + "/Email/{email}";
            public const string GetByUsername = Prefix + "/Username/{username}";
            public const string GetByPhoneNumber = Prefix + "/PhoneNumber/{phoneNumber}";
            public const string GetUserRoles = Prefix + SignleRoute + "/Roles";
            public const string GetUserClaims = Prefix + SignleRoute + "/Claims";

            //******** HttpPost ********//
            public const string RegisterUser = Prefix + "/Register-User";
            public const string RegisterAdmin = Prefix + "/Register-Admin";
            //******** HttpPut ********//
            public const string UpdateUser = Prefix + "/Update";
            //******** HttpDelete ********//
            public const string DeleteUser = Prefix + "/Delete" + SignleRoute;

        }
        #endregion
        
        #region  Authentication EndPoints-Routes
   
        public static class Authentication
        {
            public const string Prefix = Rule + "Authentication";
            public const string Login = Prefix + "/Login";
            public const string ConfirmEmail = Prefix + "/Confirm-Email";
            public const string ForgotPassword = Prefix + "/Forgot-Password";
            public const string ConfirmResetPasswordCode = Prefix + "/Confirm-Reset-Password-Code";
            public const string ChangePassword = Prefix + "/Change-Password";
            public const string SendResetCode = Prefix + "/Send-Reset-Code";
            public const string ResetPassword = Prefix + "/Reset-Password";
            public const string RefreshToken = Prefix + "/Refresh-Token";
        }
        #endregion

        #region  WishList EndPoints-Routes
  
        public static class WishList
        {
            private const string Prefix = Rule+"My/Wishlist";
            public const string GetById = Prefix + SignleRoute;
            public const string GetMyWishList = Prefix;
            public const string AddToWishList = Prefix+ "/Add-to-WishList/{bookId}";
            public const string RemoveFromWishList = Prefix + "/remove/{bookId}";
            public const string ClearWishList = Prefix + "/Clear";
            public const string IsInWishList =Prefix+"/check/{bookId}";
        }

        #endregion
        
        #region  Reviews EndPoints-Routes
        public static class Reviews
        {
            private const string Prefix = Rule + "Reviews";
            public const string GetById = Prefix + SignleRoute;
            public const string GetByUser = Prefix + "/My";
            public const string GetByBook = Prefix + "/book/{bookId}";
            public const string GetAverageRating = Prefix + "/book/{bookId}/average-rating";
            public const string CreateReview = Prefix + "/create";
            public const string UpdateReview = Prefix + "/update";
            public const string DeleteReview = Prefix +"/Delete"+ SignleRoute;
        }
        #endregion

        #region  Cart EndPoints-Routes
        public static class Cart
        {
            private const string Prefix = Rule + "Cart";
            public const string GetCartById = Prefix + SignleRoute;
            public const string GetUserCarts = Prefix + "/my";
            public const string GetAllCarts = Prefix + "/all";
            public const string CreateCart = Prefix + "/create";
            public const string UpdateCart = Prefix + "/update";
            public const string DeleteCart = Prefix + "/delete" + SignleRoute;
            public const string AddToCart = Prefix +SignleRoute+ "/add";
            public const string RemoveFromCart = Prefix + SignleRoute+"/remove/{bookId}";
            public const string UpdateQuantity = Prefix + SignleRoute+"/{bookId}"+"/update-quantity";
            public const string ClearCart = Prefix + SignleRoute+"/clear";
            public const string GetItemCount = Prefix + SignleRoute+"/count";
            public const string GetTotal = Prefix + SignleRoute+"/total";
            public const string GetCartItems = Prefix + SignleRoute+"/items";
        }
        #endregion

        #region  Order EndPoints-Routes
        public static class Order
        {
            private const string Prefix = Rule + "Orders";
            public const string GetById = Prefix + SignleRoute;
            public const string GetUserOrders = Prefix + "/my";
            public const string TotalSales = Prefix + "/Total-Sales";
            public const string OrdersCount = Prefix + "/Orders-count";
            public const string RecentOrders = Prefix + "/Recent-orders";
            public const string GetAll = Prefix + "/All";
            public const string GetByStatus = Prefix + "/status/{status}";
            public const string CreateFromCart = Prefix + "/create-from-cart/{cartId}";
            public const string UpdateStatus = Prefix + SignleRoute + "/update-status";
            public const string Delete = Prefix + "/delete" + SignleRoute;
        }
        #endregion

        #region  CheckOut EndPoints-Routes
        public static class CheckOut
        {
            public const string Prefix = Rule + "Checkout";
            public const string checkout = Prefix + "/checkout";
            public const string Success = Prefix + "/success";
            public const string Failed = Prefix + "/failed";

        }
        #endregion
    }
}
