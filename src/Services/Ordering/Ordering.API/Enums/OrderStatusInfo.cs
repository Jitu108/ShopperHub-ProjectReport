namespace Ordering.API.Enums
{
    public enum OrderStatusInfo
    {
        OrderPlaced = 1,
        OrderCouldNotBePlaced = 2,
        OrderCancelled = 3,
        OrderCouldNotBeCancelled = 4,
        OrderRefunded = 5,
        OrderCouldNotBeRefundedBecauseItIsNotCancelled = 6,
        OrderCouldNotBeCancelledBecauseItIsDelivered = 7,
        OrderCouldNotBeCancelledBecauseItIsAlreadyCancelled = 8,
        OrderCouldNotBeCancelledBecauseItIsAlreadyRefunded = 9
    }
}
