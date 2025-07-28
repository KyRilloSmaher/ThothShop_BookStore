using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThothShop.Domain.Helpers
{
    /*
       Class that contains common messages that can use in responses.
       example of usage : FOR => message will be "Email already exists."
                             ==> string message = string.Format(SystemMessages.AlreadyExists, "Email");
       
    */
    public static class SystemMessages
    {
        // Success messages
        public const string Success = "Operation completed successfully.";
        public const string CreatedSuccessfully = "{0} created successfully.";
        public const string AddedSuccessfully = "{0} Added successfully.";
        public const string UpdatedSuccessfully = "{0} updated successfully.";
        public const string RetrievedSuccessfully = "{0} retrieved successfully.";
        public const string DeletedSuccessfully = "{0} deleted successfully.";
        public const string OperationCompleted = "Operation completed successfully.";
        public const string LoginSuccessful = "Login successful.";
        public const string RegistrationSuccessful = "Registration successful.";

        // Error messages
        public const string BadRequest = "Invalid request data.";
        public const string Empty = "No data found.";
        public const string Failed = "Operation Failed.";
        public const string Unauthorized = "Unauthorized access. Please authenticate.";
        public const string FailedToAddEntity = "Adding Object Failed";
        public const string Forbidden = "You don't have permission to access this resource.";
        public const string NotFound = "Resource not found.";
        public const string MethodNotAllowed = "HTTP method not allowed for this resource.";
        public const string Conflict = "Resource conflict detected.";
        public const string InternalServerError = "An unexpected error occurred on the server.";
        public const string ServiceUnavailable = "Service temporarily unavailable.";
        public const string EntityNotFound = "Entity not found.";
        public const string FailedToDeleteEntity = "Failed to delete {0}.";
        public const string FailedToUpdateEntity = "Failed to update {0}.";
        public const string LoginFailed = "Login failed. Please check your Email and Passowrd.";

        // Validation messages
        public const string ValidationFailed = "Validation failed. Please check your input.";
        public const string RequiredField = "{0} is required.";
        public const string InvalidFormat = "{0} has invalid format.";
        public const string OutOfRange = "{0} must be between {1} and {2}.";
        public const string AlreadyExists = "{0} already exists.";

        // Authentication/Authorization messages
        public const string InvalidCredentials = "Invalid username or password.";
        public const string AccountLocked = "Account locked. Please try again later or contact support.";
        public const string TokenExpired = "Access token has expired.";
        public const string InvalidToken = "Invalid access token.";
        public const string PasswordResetRequired = "Password reset required.";
        public const string EmailAlreadyExists = "Email already exists.";
        public const string UserNameAlreadyExists = "Username already exists.";
        public const string PasswordTooWeak = "Password does not meet complexity requirements.";
        public const string PasswordChanged = "Password changed successfully.";

        // Business logic messages
        public const string OperationNotAllowed = "This operation is not allowed in the current context.";
        public const string LimitExceeded = "You have exceeded the allowed limit for this operation.";
        public const string InMaintenance = "Service is currently under maintenance.";
        public const string FeatureDisabled = "This feature is currently disabled.";

        // File/Document messages
        public const string FileUploaded = "File uploaded successfully.";
        public const string FileTooLarge = "File size exceeds the maximum allowed limit.";
        public const string InvalidFileType = "Invalid file type. Only {0} are allowed.";

        // API specific messages
        public const string RateLimitExceeded = "Too many requests. Please try again later.";
        public const string DeprecatedApiVersion = "This API version is deprecated. Please upgrade.";
    }
}
