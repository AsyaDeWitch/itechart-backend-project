using System.ComponentModel;

namespace RIL.Models
{
    public enum DeliveryType
    {
        [Description("On-Demand Delivery")]
        On_Demand_Delivery,

        [Description("Scheduled On-Demand Delivery")]
        Scheduled_On_Demand_Delivery,

        [Description("Self Pickup")]
        Self_Pickup,
    }
}
