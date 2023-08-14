using OrderPlacer.Interfaces;

namespace OrderPlacer.Repository;

public interface IOrderRepository
{
    Task AddAsync(IOrder order);
    Task<IEnumerable<IOrder>> GetOrdersByCustomerIdAsync(string customerId);
}