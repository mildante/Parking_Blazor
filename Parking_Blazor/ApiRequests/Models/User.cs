using static Parking_Blazor.ApiRequests.Models.User;

namespace Parking_Blazor.ApiRequests.Models
{
    public class UserService
    {
        public UserModel? CurrentUser { get; set; }

        public bool isAdmin => CurrentUser?.role_id == 1;
        public bool isUser => CurrentUser?.role_id == 3;

        public Action? OnAuthStateChanged { get; set; }

        public void Notify()
        {
            OnAuthStateChanged?.Invoke();
        }
    }

    public class User
    {
        public class AuthUserRequest
        {
            public string email { get; set; }
            public string password { get; set; }
        }

        public class UserModel
        {
            public int id_user { get; set; }
            public string name { get; set; }
            public string? surname { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string password { get; set; }
            public int role_id { get; set; }
        }

        public class UserResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public string? token { get; set; }
            public UserModel? user { get; set; }
        }

        public class UserListResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public List<UserModel> list { get; set; } = new();
        }
    }
}
