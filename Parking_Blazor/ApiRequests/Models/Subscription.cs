using static Parking_Blazor.ApiRequests.Models.Parking;
using static Parking_Blazor.ApiRequests.Models.User;

namespace Parking_Blazor.ApiRequests.Models
{
    public class Subscription
    {
        public class SubscriptionPlanModel
        {
            public int id_plan { get; set; }
            public string name { get; set; }
            public int duration_days { get; set; }
            public decimal price { get; set; }
            public int parking_complex_id { get; set; }
            public ParkingComplexModel? parkingComplex { get; set; }
        }

        public class SubscriptionModel
        {
            public int id_subscription { get; set; }
            public int user_id { get; set; }
            public UserModel? user { get; set; }
            public int subscription_plan_id { get; set; }
            public SubscriptionPlanModel? subscriptionPlan { get; set; }
            public DateOnly start_date { get; set; }
            public DateOnly end_date { get; set; }
            public string status { get; set; } = "active";
        }

        public class PlanListResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public List<SubscriptionPlanModel> list { get; set; } = new();
        }

        public class SubscriptionListResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public List<SubscriptionModel> list { get; set; } = new();
        }

        public class SubscriptionResponse
        {
            public bool status { get; set; }
            public string? message { get; set; }
            public SubscriptionModel? subscription { get; set; }
        }
    }
}