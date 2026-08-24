using System;
using System.Collections.Generic;
using System.Text;
using Masar.Domain.Common.Results;
namespace Masar.Domain.Users
{
    public static class UserError
    {
        public static readonly Error UsernameRequired =
            Error.Validation("User_Username_Required", "Username is required.");
      
        public static readonly Error PersonIdRequired = Error.Validation("User_PersonId_Required", "Person ID is required.");
        public static readonly Error UserNotFound = Error.NotFound("User_Not_Found", "User not found.");
        
    }
}
