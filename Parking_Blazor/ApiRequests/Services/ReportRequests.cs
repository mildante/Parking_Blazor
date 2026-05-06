using System.Net.Http.Json;
using static Parking_Blazor.ApiRequests.Models.Report;

namespace Parking_Blazor.ApiRequests.Services
{
    public class ReportRequests
    {
        private readonly HttpClient _httpClient;

        public ReportRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AdminReportResponse?> GetAdminReport(int days)
        {
            return await _httpClient.GetFromJsonAsync<AdminReportResponse>($"/getAdminReport?days={days}");
        }
    }
}
