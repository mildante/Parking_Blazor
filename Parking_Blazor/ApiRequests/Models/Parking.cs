namespace Parking_Blazor.ApiRequests.Models
{
    public class Parking
    {
        public class ParkingComplexModel
        {
            public int id_complex { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public int total_spots { get; set; }
            public List<ParkingSpotModel> spots { get; set; } = new();
        }

        public class ParkingSpotModel
        {
            public int id_spot { get; set; }
            public string number { get; set; }
            public string status { get; set; } = "Свободно";
            public int parking_complex_id { get; set; }
            public ParkingComplexModel? parkingComplex { get; set; }
        }

        public class ParkingComplexResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public ParkingComplexModel? complex { get; set; }
        }

        public class ParkingComplexListResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public List<ParkingComplexModel> list { get; set; } = new();
        }

        public class ParkingSpotResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public ParkingSpotModel? spot { get; set; }
        }

        public class ParkingSpotListResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public List<ParkingSpotModel> list { get; set; } = new();
        }
    }
}