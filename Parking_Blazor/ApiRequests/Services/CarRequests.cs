using System.Net.Http.Json;
using static Parking_Blazor.ApiRequests.Models.Car;

namespace Parking_Blazor.ApiRequests.Services
{
    public class CarRequests
    {
        private readonly HttpClient _httpClient;

        public CarRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CarListResponse?> GetAllCars()
        {
            return await _httpClient.GetFromJsonAsync<CarListResponse>("/getAllCars");
        }

        public async Task<CarListResponse?> GetCarsByUser(int userId)
        {
            return await _httpClient.GetFromJsonAsync<CarListResponse>($"/getCarsByUser/{userId}");
        }

        public async Task<CarResponse?> CreateCar(CarModel carModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/createCar", carModel);
            return await response.Content.ReadFromJsonAsync<CarResponse>();
        }

        public async Task<CarResponse?> UpdateCar(CarModel carModel)
        {
            var response = await _httpClient.PutAsJsonAsync("/updateCar", carModel);
            return await response.Content.ReadFromJsonAsync<CarResponse>();
        }

        public async Task<CarResponse?> DeleteCar(int carId)
        {
            var response = await _httpClient.DeleteAsync($"/deleteCar/{carId}");
            return await response.Content.ReadFromJsonAsync<CarResponse>();
        }
    }
}