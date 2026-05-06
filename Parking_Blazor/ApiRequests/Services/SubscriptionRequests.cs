using System.Net.Http.Json;
using static Parking_Blazor.ApiRequests.Models.Subscription;

namespace Parking_Blazor.ApiRequests.Services
{
    public class SubscriptionRequests
    {
        private readonly HttpClient _httpClient;

        public SubscriptionRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PlanListResponse?> GetPlansByComplex(int complexId)
        {
            return await _httpClient.GetFromJsonAsync<PlanListResponse>($"/getPlansByComplex/{complexId}");
        }

        public async Task<SubscriptionResponse?> CreatePlan(SubscriptionPlanModel planModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/createPlan", planModel);
            return await ReadSubscriptionResponse(response, "Не удалось создать тариф");
        }

        public async Task<SubscriptionResponse?> UpdatePlan(SubscriptionPlanModel planModel)
        {
            var response = await _httpClient.PutAsJsonAsync("/updatePlan", planModel);
            return await ReadSubscriptionResponse(response, "Не удалось обновить тариф");
        }

        public async Task<SubscriptionResponse?> DeletePlan(int planId)
        {
            var response = await _httpClient.DeleteAsync($"/deletePlan/{planId}");
            return await ReadSubscriptionResponse(response, "Не удалось удалить тариф");
        }

        public async Task<SubscriptionListResponse?> GetAllSubscriptions()
        {
            return await _httpClient.GetFromJsonAsync<SubscriptionListResponse>("/getAllSubscriptions");
        }

        public async Task<SubscriptionListResponse?> GetSubscriptionsByUser(int userId)
        {
            return await _httpClient.GetFromJsonAsync<SubscriptionListResponse>($"/getSubscriptionsByUser/{userId}");
        }

        public async Task<SubscriptionResponse?> CreateSubscription(SubscriptionModel subscriptionModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/createSubscription", subscriptionModel);
            return await ReadSubscriptionResponse(response, "Не удалось оформить абонемент");
        }

        private static async Task<SubscriptionResponse> ReadSubscriptionResponse(HttpResponseMessage response, string fallbackMessage)
        {
            if (!response.IsSuccessStatusCode)
                return new SubscriptionResponse { status = false, message = fallbackMessage };

            try
            {
                var result = await response.Content.ReadFromJsonAsync<SubscriptionResponse>();
                return result ?? new SubscriptionResponse { status = false, message = fallbackMessage };
            }
            catch
            {
                return new SubscriptionResponse { status = false, message = fallbackMessage };
            }
        }
    }
}
