using Microsoft.JSInterop;
using Parking_Blazor.ApiRequests.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static Parking_Blazor.ApiRequests.Models.User;

namespace Parking_Blazor.ApiRequests.Services
{
    public class UserRequests
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly UserService _userService;
        public UserRequests(HttpClient httpClient, ILocalStorageService localStorage, UserService userService)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _userService = userService;
        }

        public async Task<UserResponse> GetAuthorizeUser(AuthUserRequest authUser)
        {
            var response = await _httpClient.PostAsJsonAsync("/authUser", authUser);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<UserResponse>();

            if (result != null && result.status && !string.IsNullOrEmpty(result.token))
            {
                _localStorage.SetItem("token", result.token);

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", result.token);

                if (result.user != null)
                    _userService.CurrentUser = result.user;
            }

            return result;
        }

        public async Task<bool> LoadUserFromToken()
        {
            var token = _localStorage.GetItem<string>("token");

            if (string.IsNullOrEmpty(token))
                return false;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("/authByToken");

            if (!response.IsSuccessStatusCode)
            {
                await Logout();
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<UserResponse>();

            if (result == null || !result.status || result.user == null)
            {
                await Logout();
                return false;
            }

            _userService.CurrentUser = result.user;
            _userService.Notify();
            return true;

        }

        public async Task Logout()
        {
            _localStorage.RemoveItem("token");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            _userService.CurrentUser = null;
            _userService.Notify();
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