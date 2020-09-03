namespace QHomeGroup.Utilities.Constants
{
    public static class SystemConstants
    {
        public const string ConnectionString = "DefaultConnection";

        public class TokenProvider
        {
            public const string EmailConfirm = "EmailConfirmationTokenProvider";
            public const string Passwordless = "passwordless-auth";
        }

        public class AuthenticationScheme
        {
            public const string AdminSide = "AdminSide";
            public const string ClientSide = "ClientSide";
        }

        public class UserClaim
        {
            public const string Id = "Id";
            public const string FullName = "fullName";
            public const string Avatar = "avatar";
        }
    }
}