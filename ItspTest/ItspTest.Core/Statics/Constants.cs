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

        public static class Log
        {
            public static class Info
            {
                public const string UserAuthenticateRequestReceived = "User authenticate request received. \n{0}";
                public const string TokenCreated = "The token created successfully: {0}";
                public const string UserRegisterRequestReceived = "User register request received. \n{0}";
            }

            public static class Error
            {
                public const string InvalidRequest = "Invalid request. \n{0}";
                public const string UserNotExist = "User does not exist for the username: {0}";
                public const string UserAlreadyExist = "User already exists for the username: {0}";
                public const string UserCreationFailed = "User creation failed: {0}";

            }
        }
    }
}