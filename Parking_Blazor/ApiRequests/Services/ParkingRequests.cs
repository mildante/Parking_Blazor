using System.Net.Http.Json;
using static Parking_Blazor.ApiRequests.Models.Parking;

namespace Parking_Blazor.ApiRequests.Services
{
    public class ParkingRequests
    {
        private readonly HttpClient _httpClient;

        public ParkingRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ParkingComplexListResponse?> GetAllComplexes()
        {
            return await _httpClient.GetFromJsonAsync<ParkingComplexListResponse>("/getAllComplexes");
        }

        public async Task<ParkingComplexResponse?> CreateComplex(ParkingComplexModel complexModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/createComplex", complexModel);
            return await response.Content.ReadFromJsonAsync<ParkingComplexResponse>();
        }

        public async Task<ParkingComplexResponse?> UpdateComplex(ParkingComplexModel complexModel)
        {
            var response = await _httpClient.PutAsJsonAsync("/updateComplex", complexModel);
            return await response.Content.ReadFromJsonAsync<ParkingComplexResponse>();
        }

        public async Task<ParkingComplexResponse?> DeleteComplex(int complexId)
        {
            var response = await _httpClient.DeleteAsync($"/deleteComplex/{complexId}");
            return await response.Content.ReadFromJsonAsync<ParkingComplexResponse>();
        }

        public async Task<ParkingSpotListResponse?> GetSpotsByComplex(int complexId)
        {
            return await _httpClient.GetFromJsonAsync<ParkingSpotListResponse>($"/getSpotsByComplex/{complexId}");
        }

        public async Task<ParkingSpotResponse?> CreateSpot(ParkingSpotModel spotModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/createSpot", spotModel);
            return await response.Content.ReadFromJsonAsync<ParkingSpotResponse>();
        }

        public async Task<ParkingSpotResponse?> UpdateSpotStatus(int spotId, string status)
        {
            var response = await _httpClient.PutAsJsonAsync($"/updateSpotStatus/{spotId}", status);
            return await response.Content.ReadFromJsonAsync<ParkingSpotResponse>();
        }

        public async Task<ParkingSpotResponse?> DeleteSpot(int spotId)
        {
            var response = await _httpClient.DeleteAsync($"/deleteSpot/{spotId}");
            return await response.Content.ReadFromJsonAsync<ParkingSpotResponse>();
        }
    }
}
