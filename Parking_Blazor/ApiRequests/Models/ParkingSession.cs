using static Parking_Blazor.ApiRequests.Models.Car;
using static Parking_Blazor.ApiRequests.Models.Parking;
using static Parking_Blazor.ApiRequests.Models.Subscription;
using static Parking_Blazor.ApiRequests.Models.User;

namespace Parking_Blazor.ApiRequests.Models
{
    public class ParkingSession
    {
        public class ParkingSessionModel
        {
            public int id_session { get; set; }
            public int user_id { get; set; }
            public UserModel? user { get; set; }
            public int car_id { get; set; }
            public CarModel? car { get; set; }
            public int parking_complex_id { get; set; }
            public ParkingComplexModel? parkingComplex { get; set; }
            public int parking_spot_id { get; set; }
            public ParkingSpotModel? parkingSpot { get; set; }
            public int? subscription_id { get; set; }
            public SubscriptionModel? subscription { get; set; }
            public DateTime entry_time { get; set; }
            public DateTime? exit_time { get; set; }
            public string status { get; set; } = "Занято";
        }

        public class ParkingSessionResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public ParkingSessionModel? session { get; set; }
        }

        public class ParkingSessionListResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public List<ParkingSessionModel> list { get; set; } = new();
        }
    }
}