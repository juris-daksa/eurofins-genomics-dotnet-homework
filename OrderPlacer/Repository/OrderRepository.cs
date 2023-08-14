using OrderPlacer.Interfaces;
using OrderPlacer.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OrderPlacer.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly DbContext _context;

    public OrderRepository(DbContext context) 
    {
        _context = context;
    }

    public async Task AddAsync(IOrder order)
    {
        await _context.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<IOrder>> GetOrdersByCustomerIdAsync(string customerId)
    {
        return await _context.Set<Order>().Where(order => order.CustomerId.Equals(customerId)).ToListAsync();
    }
}