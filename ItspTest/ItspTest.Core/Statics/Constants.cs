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
                public const string CollectionAlreadyExist = "The movie collection already exist.";
                public const string CollectionNotExist = "The collection not exist for the user.";
                public const string Forbidden = "User is not allowed to do this action.";
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
                public const string GetCollectionsRequestReceived = "Get all collection request received.";
                public const string AddCollectionRequestReceived = "Add collection request received.";
                public const string CollectionCreated = "The collection created successfully: {0}";
                public const string SearchCollectionRequestReceived = "Search collection request received.";
                public const string AddMovieRequestReceived = "Add movie request received.";
            }

            public static class Error
            {
                public const string InvalidRequest = "Invalid request. \n{0}";
                public const string UserNotExist = "User does not exist for the username: {0}";
                public const string UserAlreadyExist = "User already exists for the username: {0}";
                public const string UserCreationFailed = "User creation failed: {0}";
                public const string CollectionExist = "Collection already exist for the user: {0}";
                public const string CollectionNotExist = "Collection not exist for the user: {0}";
                public const string AddCollectionFailed = "Failed on creating a collection: {0}";
                public const string SearchCollectionFailed = "Failed on searching collection: {0}";
                public const string AddMovieFailed = "Failed on adding a movie {0}";
                public const string NotAllowed = "Sorry, you cannot perform this action.";

            }
        }
    }
}