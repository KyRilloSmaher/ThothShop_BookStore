


using ThothShop.Domain.Helpers;

namespace ThothShop.Core.Bases
{
    public class ResponseHandler
    {
        #region Feilds

               

        #endregion

        #region Constructors

                public ResponseHandler()
                {
                   
                }
        #endregion


        #region Methods
                public Response<T> Deleted<T>(string message = null)
                {
                    return new Response<T>()
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Succeeded = true,
                        Message = message ?? string.Format(SystemMessages.DeletedSuccessfully, "Object")
                    };
                }
                public Response<T> Success<T>(T entity, string message = null, object Meta = null)
                {
                    return new Response<T>()
                    {
                        Data = entity,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Succeeded = true,
                        Message = message ?? SystemMessages.Success,
                        Meta = Meta
                    };
                }
                public Response<T> Unauthorized<T>(string message = null)
                {
                    return new Response<T>()
                    {
                        StatusCode = System.Net.HttpStatusCode.Unauthorized,
                        Succeeded = true,
                        Message = message ?? string.Format(SystemMessages.Unauthorized, "Object")
                    };
                }
                public Response<T> BadRequest<T>(string message = null)
                {
                    return new Response<T>()
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Succeeded = false,
                        Message = message ?? string.Format(SystemMessages.NotFound, "Object")
                    };
                }

                public Response<T> UnprocessableEntity<T>(string message = null)
                {
                    return new Response<T>()
                    {
                        StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                        Succeeded = false,
                       Message = message ?? string.Format(SystemMessages.InternalServerError, "Object")
                    };
                }
                public Response<T> Failed<T>(string message = null)
                {
                    return new Response<T>()
                    {
                        StatusCode = System.Net.HttpStatusCode.FailedDependency,
                        Succeeded = false,
                        Message = message ?? string.Format(SystemMessages.InternalServerError, "Object")
                    };
                }
                public Response<T> NotFound<T>(string message = null)
                {
                    return new Response<T>()
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Succeeded = false,
                       Message = message?? string.Format(SystemMessages.NotFound, "Object")
                    };
                }

                public Response<T> Created<T>(T entity, object Meta = null)
                {
                    return new Response<T>()
                    {
                        Data = entity,
                        StatusCode = System.Net.HttpStatusCode.Created,
                        Succeeded = true,
                        Message = string.Format(SystemMessages.CreatedSuccessfully, "Object"),
                        Meta = Meta
                    };
                }
                public Response<T> Added<T>(string message = null)
                {
                    return new Response<T>()
                    {
                        
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Succeeded = true,
                        Message = message ?? string.Format(SystemMessages.AddedSuccessfully,"Object")

                    };
                }
        #endregion
    }
}