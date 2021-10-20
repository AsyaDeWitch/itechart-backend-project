using System.ComponentModel;

namespace RIL.Models
{
    public enum OrderStatus
    {
        [Description("Pending")]
        Pending,

        [Description("Awaiting Payment")]
        AwaitingPayment,

        [Description("Awaiting Fulfillment")]
        AwaitingFulfillment,

        [Description("Awaiting Shipment")]
        AwaitingShipment,

        [Description("Awaiting Pickup")]
        AwaitingPickup,

        [Description("Partially Shipped")]
        PartiallyShipped,

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
        ManualVerificationRequired,

        [Description("Partially Refunded")]
        PartiallyRefunded,
    }
}
