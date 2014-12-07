using System;
using System.Security.Claims;
using System.Web;

namespace CreaturesOfCode.Core
{
    public class UserProfile
    {
        public static UserProfile Current
        {
            get
            {
                if (HttpContext.Current == null) return null;

                var currentProfile = HttpContext.Current.Items["User_Profile"];
                if (currentProfile == null && ClaimsPrincipal.Current.Identity.IsAuthenticated)
                {
                    HttpContext.Current.Items["User_Profile"] = new UserProfile
                    {
                        UserId = GetClaimValue<string>("user_id"),
                        Email = GetClaimValue<string>("email"),
                        Username = GetClaimValue<string>("nickname"),
                        PictureUrl = GetClaimValue<string>("picture"),
                        FirstName = GetClaimValue<string>("given_name"),
                        LastName = GetClaimValue<string>("family_name")
                    };
                    currentProfile = HttpContext.Current.Items["User_Profile"];
                }

                return currentProfile as UserProfile;
            }
        }

        public string UserId { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }
        public string PictureUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get { return string.Join(" ", FirstName, LastName); } }

        public bool IsVerified { get; set; }

        private static T GetClaimValue<T>(string key)
        {
            var value = ClaimsPrincipal.Current.FindFirst(key).Value;

            if (value == null) return default(T);

            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
