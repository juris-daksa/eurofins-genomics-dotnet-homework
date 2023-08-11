using OrderPlacer.Entities;
using OrderPlacer.Interfaces;
using System.Net;

namespace OrderPlacer.Interfaces;

public interface IOrderService
{
    Task<IOrder> PlaceOrderAsync(IOrderDraft orderDraft);
    Task<List<IOrder>> GetOrdersByCustomerIdAsync(string CustomerId);
}