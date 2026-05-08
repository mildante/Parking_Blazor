using Microsoft.AspNetCore.SignalR.Client;
using static Parking_Blazor.ApiRequests.Models.Parking;

namespace Parking_Blazor.ApiRequests.Services
{
    public class ParkingRealtimeService : IAsyncDisposable
    {
        private readonly HttpClient _httpClient;
        private HubConnection? _hubConnection;

        public event Action<ParkingSpotModel, string>? SpotChanged;
        public event Action<int>? ComplexChanged;
        public event Action<string>? NotificationReceived;

        public ParkingRealtimeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public bool IsConnected => _hubConnection?.State == HubConnectionState.Connected;

        public async Task Connect()
        {
            if (_hubConnection != null)
            {
                if (_hubConnection.State == HubConnectionState.Disconnected)
                    await _hubConnection.StartAsync();

                return;
            }

            var hubUrl = new Uri(_httpClient.BaseAddress!, "/parkingHub");

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<ParkingSpotModel, string>("ParkingSpotChanged", (spot, changeType) =>
            {
                SpotChanged?.Invoke(spot, changeType);
            });

            _hubConnection.On<int>("ParkingComplexChanged", complexId =>
            {
                ComplexChanged?.Invoke(complexId);
            });

            _hubConnection.On<string>("ParkingNotification", message =>
            {
                NotificationReceived?.Invoke(message);
            });

            await _hubConnection.StartAsync();
        }

        public async Task JoinParkingComplex(int complexId)
        {
            if (_hubConnection?.State == HubConnectionState.Connected)
                await _hubConnection.InvokeAsync("JoinParkingComplex", complexId);
        }

        public async Task LeaveParkingComplex(int complexId)
        {
            if (_hubConnection?.State == HubConnectionState.Connected)
                await _hubConnection.InvokeAsync("LeaveParkingComplex", complexId);
        }

        public async Task JoinUserNotifications(int userId)
        {
            if (_hubConnection?.State == HubConnectionState.Connected)
                await _hubConnection.InvokeAsync("JoinUserNotifications", userId);
        }

        public async Task CheckSubscriptionWarnings(int userId)
        {
            if (_hubConnection?.State == HubConnectionState.Connected)
                await _hubConnection.InvokeAsync("CheckSubscriptionWarnings", userId);
        }

        public async Task CheckParkingSessionWarnings(int userId)
        {
            if (_hubConnection?.State == HubConnectionState.Connected)
                await _hubConnection.InvokeAsync("CheckParkingSessionWarnings", userId);
        }

        public async Task CheckExpiredSessions(int complexId)
        {
            if (_hubConnection?.State == HubConnectionState.Connected)
                await _hubConnection.InvokeAsync("CheckExpiredSessions", complexId);
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
                await _hubConnection.DisposeAsync();
        }
    }
}
