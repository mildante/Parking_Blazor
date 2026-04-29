using System.Net.Http.Json;
using static Parking_Blazor.ApiRequests.Models.User;

namespace Parking_Blazor.ApiRequests.Services
{
    public class UserRequests
    {
        private readonly HttpClient _httpClient;

        public UserRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserResponse?> AuthUser(AuthUserRequest authModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/authUser", authModel);
            return await response.Content.ReadFromJsonAsync<UserResponse>();
        }

        public async Task<UserResponse?> RegistrationUser(UserModel userModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/registrationUser", userModel);
            return await response.Content.ReadFromJsonAsync<UserResponse>();
        }

        public async Task<UserResponse?> UpdateUser(UserModel userModel)
        {
            var response = await _httpClient.PutAsJsonAsync("/updateUser", userModel);
            return await response.Content.ReadFromJsonAsync<UserResponse>();
        }

        public async Task<UserResponse?> DeleteUser(int userId)
        {
            var response = await _httpClient.DeleteAsync($"/deleteUser/{userId}");
            return await response.Content.ReadFromJsonAsync<UserResponse>();
        }

        public async Task<UserListResponse?> GetAllUser()
        {
            return await _httpClient.GetFromJsonAsync<UserListResponse>("/getAllUser");
        }
    }
}