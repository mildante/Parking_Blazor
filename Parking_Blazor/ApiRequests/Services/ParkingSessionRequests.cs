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

        public async Task<ParkingSessionResponse?> CreateSession(CreateSessionRequest sessionModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/createSession", sessionModel);
            return await ReadSessionResponse(response, "Не удалось оформить парковку");
        }

        public async Task<ParkingSessionResponse?> CreateGuestSession(GuestSessionRequest sessionModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/createGuestSession", sessionModel);
            return await ReadSessionResponse(response, "Не удалось оформить гостевую парковку");
        }

        public async Task<ParkingSessionResponse?> CloseSession(int sessionId)
        {
            var response = await _httpClient.PutAsync($"/closeSession/{sessionId}", null);
            return await ReadSessionResponse(response, "Не удалось зафиксировать выезд");
        }

        private static async Task<ParkingSessionResponse> ReadSessionResponse(HttpResponseMessage response, string fallbackMessage)
        {
            if (!response.IsSuccessStatusCode)
            {
                return new ParkingSessionResponse
                {
                    status = false,
                    message = fallbackMessage
                };
            }

            try
            {
                var result = await response.Content.ReadFromJsonAsync<ParkingSessionResponse>();
                return result ?? new ParkingSessionResponse { status = false, message = fallbackMessage };
            }
            catch
            {
                return new ParkingSessionResponse
                {
                    status = false,
                    message = fallbackMessage
                };
            }
        }
    }
}
