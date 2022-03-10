using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItspTest.Core.Statics
{
    public static class Constants
    {
        public static class ResponseMessages
        {
            public static class Error
            {
                public const string UsernameOrPasswordRequired = "The username or password is empty.";
                public const string UserExist = "User already exists!.";
                public const string UserCreationFailed = "User creation failed! Please check user details and try again.";
            }

            public static class Success
            {
                public const string UserCreated = "User created successfully.";
            }
        }
    }
}
