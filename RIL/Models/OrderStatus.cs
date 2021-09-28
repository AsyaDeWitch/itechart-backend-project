using System.ComponentModel;

namespace RIL.Models
{
    public enum OrderStatus
    {
        [Description("Pending")]
        Pending,

        [Description("Awaiting Payment")]
        Awaiting_Payment,

        [Description("Awaiting Fulfillment")]
        Awaiting_Fulfillment,

        [Description("Awaiting Shipment")]
        Awaiting_Shipment,

        [Description("Awaiting Pickup")]
        Awaiting_Pickup,

        [Description("Partially Shipped")]
        Partially_Shipped,

        [Description("Completed")]
        Completed,

        [Description("Shipped")]
        Shipped,

        [Description("Cancelled")]
        Cancelled,

        [Description("Declined")]
        Declined,

        [Description("Refunded")]
        Refunded,

        [Description("Disputed")]
        Disputed,

        [Description("Manual Verification Required")]
        Manual_Verification_Required,

        [Description("Partially Refunded")]
        Partially_Refunded,
    }
}
