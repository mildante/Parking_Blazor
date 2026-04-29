using System.Net.Http.Json;
using static Parking_Blazor.ApiRequests.Models.ParkingSession;

namespace Parking_Blazor.ApiRequests.Services
{
    public class ParkingSessionRequests
    {
        private readonly HttpClient _httpClient;

        public ParkingSessionRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ParkingSessionListResponse?> GetAllSessions()
        {
            return await _httpClient.GetFromJsonAsync<ParkingSessionListResponse>("/getAllSessions");
        }

        public async Task<ParkingSessionListResponse?> GetSessionsByUser(int userId)
        {
            return await _httpClient.GetFromJsonAsync<ParkingSessionListResponse>($"/getSessionsByUser/{userId}");
        }

        public async Task<ParkingSessionListResponse?> GetActiveSessions()
        {
            return await _httpClient.GetFromJsonAsync<ParkingSessionListResponse>("/getActiveSessions");
        }

        public async Task<ParkingSessionResponse?> CreateSession(ParkingSessionModel sessionModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/createSession", sessionModel);
            return await response.Content.ReadFromJsonAsync<ParkingSessionResponse>();
        }

        public async Task<ParkingSessionResponse?> CloseSession(int sessionId)
        {
            var response = await _httpClient.PutAsync($"/closeSession/{sessionId}", null);
            return await response.Content.ReadFromJsonAsync<ParkingSessionResponse>();
        }

        public async Task<ParkingSessionResponse?> DeleteSession(int sessionId)
        {
            var response = await _httpClient.DeleteAsync($"/deleteSession/{sessionId}");
            return await response.Content.ReadFromJsonAsync<ParkingSessionResponse>();
        }
    }
}