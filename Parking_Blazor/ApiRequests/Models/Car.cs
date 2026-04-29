using static Parking_Blazor.ApiRequests.Models.User;

namespace Parking_Blazor.ApiRequests.Models
{
    public class Car
    {
        public class CarModel
        {
            public int id_car { get; set; }
            public string license_plate { get; set; }
            public string? brand { get; set; }
            public string? model { get; set; }
            public string? color { get; set; }
            public int user_id { get; set; }
            public UserModel? user { get; set; }
        }

        public class CarResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public CarModel? car { get; set; }
        }

        public class CarListResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public List<CarModel> list { get; set; } = new();
        }
    }
}