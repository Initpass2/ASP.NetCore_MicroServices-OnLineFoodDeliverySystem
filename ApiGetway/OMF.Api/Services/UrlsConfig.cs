using System;

namespace OMF.Api.Services
{
    public class UrlsConfig
    {
        public class RestaurantOperations
        {
            public static string GetMenuByRestaurantId(int id) => $"/api/v1/restaurant/GetMenuByRestaurantId/{id}";
        }

        public class OderOpertions
        {
            public static string CreateOrder() => $"/api/v1/Order/CreateOrder";
            public static string UpdateOrder() => $"/api/v1/Order/UpdateOrder";
            public static string CancelOrder(Guid orderId) => $"/api/v1/Order/DeleteOrder/{orderId}";
            public static string GetOrder(Guid orderId) => $"/api/v1/Order/GetOrder/{orderId}";
        }

        public string Customer { get; set; }
        public string Order { get; set; }
        public string Restaurant { get; set; }
        public string Review { get; set; }
    }
}
