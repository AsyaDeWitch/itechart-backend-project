using System.ComponentModel;

namespace RIL.Models
{
    public enum DeliveryType
    {
        [Description("On-Demand Delivery")]
        OnDemandDelivery,

        [Description("Scheduled On-Demand Delivery")]
        ScheduledOnDemandDelivery,

        [Description("Self Pickup")]
        SelfPickup,
    }
}
