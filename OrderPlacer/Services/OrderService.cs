using OrderPlacer.Entities;
using OrderPlacer.Exceptions;
using OrderPlacer.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace OrderPlacer.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService (IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order> PlaceOrderAsync (IOrderDraft orderDraft)
    {
        if (orderDraft == null)
        {
            throw new ArgumentNullException(nameof(orderDraft));
        }

        var invalidOrderMessage = ValidateOrder(orderDraft);
        if (invalidOrderMessage != null) 
        {
            throw new InvalidOrderException(invalidOrderMessage); 
        }

        var newOrder = new Order 
        { 
            CustomerId = orderDraft.CustomerId,
            OrderedUnits = orderDraft.OrderedUnits,
            ExpectedDeliveryDate = orderDraft.ExpectedDeliveryDate
        };

        newOrder.PriceSubTotal = CalculatePriceSubTotal(newOrder.OrderedUnits);

        var discountCoeff = GetDiscount(newOrder.OrderedUnits.Sum(item => item.Quantity));
        newOrder.PriceTotal = CalculatePriceTotal(newOrder.PriceSubTotal, discountCoeff);

        return newOrder;
    }

    public async Task<List<Order>> GetOrdersByCustomerIdAsync(string CustomerId)
    {
        throw new NotImplementedException();
    }

    private string? ValidateOrder(IOrderDraft orderDraft)
    {
        if (orderDraft == null)
        {
            throw new ArgumentNullException(nameof(orderDraft));
        }

        if (orderDraft.ExpectedDeliveryDate.Date <= DateTime.Today)
        {
            return "Expected delivery date should be greater than today";
        }

        if (orderDraft.OrderedUnits.Any(item => item.Quantity <= 0))
        {
            return "Ordered product should have an amount greater than 0";
        }

        if (orderDraft.OrderedUnits.Sum(item => item.Quantity) > 999)
        {
            return "Amount of ordered products should not exceed 999";
        }

        return null;
    }

    private decimal CalculatePriceSubTotal(IEnumerable<OrderedUnit> orderedUnits)
    {
        return orderedUnits.Sum(item => item.Quantity * item.Product.BasePrice);
    }

    private decimal CalculatePriceTotal(decimal priceSubTotal, decimal discountCoeff)
    {
        return priceSubTotal * discountCoeff;
    }

    private decimal GetDiscount(int amount)
    {
        if (amount > 50) 
        {
            return 0.85m;
        }

        if (amount > 10)
        {
            return 0.95m;
        }
    }

}
