using OrderPlacer.Entities;
using OrderPlacer.Interfaces;

namespace OrderPlacer.Interfaces;

public interface IOrderService
{
    Task<IOrder> PlaceOrderAsync(IOrderDraft orderDraft);
    Task<List<IOrder>> GetOrdersByCustomerIdAsync(string CustomerId);
}