using Moq;
using OrderPlacer.Entities;
using OrderPlacer.Exceptions;
using OrderPlacer.Repository;
using OrderPlacer.Services;

namespace OrderPlacer.Tests;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepositoryMock;

    public OrderServiceTests()
    {
        _orderRepositoryMock = new Mock<IOrderRepository>();
    }

    [Fact]
    public async Task PlaceOrderAsync_ValidOrder_NoDiscount()
    {
        var _sut = new OrderService(this._orderRepositoryMock.Object);

        var testOrder = new OrderDraft
        {
            CustomerId = "ABC",
            ExpectedDeliveryDate = DateTime.Today.AddDays(1),
            OrderedUnits = new List<Interfaces.IOrderUnit>
            {
                new OrderUnit
                {
                    Product = new DefaultProduct(),
                    Quantity = 10
                }
            }
        };

        var result = await _sut.PlaceOrderAsync(testOrder);

        //Assert.NotNull(result);
        Assert.Equal(989.90m, result.PriceTotal);
        Assert.Equal(DateTime.Today.AddDays(1), result.ExpectedDeliveryDate);
    }

    [Fact]
    public async Task PlaceOrderAsync_ValidOrder_WithDiscount()
    {
        var _sut = new OrderService(this._orderRepositoryMock.Object);

        var testOrder_Discount_10 = new OrderDraft
        {
            CustomerId = "ABC",
            ExpectedDeliveryDate = DateTime.Today.AddDays(1),
            OrderedUnits = new List<Interfaces.IOrderUnit>
            {
                new OrderUnit
                {
                    Product = new DefaultProduct(),
                    Quantity = 20
                }
            }
        };

        var result = await _sut.PlaceOrderAsync(testOrder_Discount_10);

        Assert.Equal(1880.81m, result.PriceTotal);

        var testOrder_Discount_50 = new OrderDraft
        {
            CustomerId = "ABC",
            ExpectedDeliveryDate = DateTime.Today.AddDays(1),
            OrderedUnits = new List<Interfaces.IOrderUnit>
            {
                new OrderUnit
                {
                    Product = new DefaultProduct(),
                    Quantity = 60
                }
            }
        };

        result = await _sut.PlaceOrderAsync(testOrder_Discount_50);

        Assert.Equal(5048.49m, result.PriceTotal);
    }

    [Fact]
    public async Task PlaceOrderAsync_DateNotInFuture_ShouldThrowException()
    {
        var _sut = new OrderService(this._orderRepositoryMock.Object);

        var testOrder_DateNotInFuture = new OrderDraft
        {
            ExpectedDeliveryDate = DateTime.Today.AddDays(-1),
            OrderedUnits = new List<Interfaces.IOrderUnit>
            {
                new OrderUnit
                {
                    Product = new DefaultProduct(),
                    Quantity = 60
                }
            }
        };

        Func<Task> act = () => _sut.PlaceOrderAsync(testOrder_DateNotInFuture);

        var exception = await Assert.ThrowsAsync<InvalidOrderException>(act);

        Assert.Contains("Expected delivery date should be greater than today", exception.Message);
    }

    [Fact]
    public async Task PlaceOrderAsync_OrderQuantityNotPositiveNumber_ShouldThrowException()
    {
        var _sut = new OrderService(this._orderRepositoryMock.Object);

        var testOrder_QuantityInvalid_0 = new OrderDraft
        {
            ExpectedDeliveryDate = DateTime.Today.AddDays(1),
            OrderedUnits = new List<Interfaces.IOrderUnit>
            {
                new OrderUnit
                {
                    Product = new DefaultProduct(),
                    Quantity = 0
                }
            }
        };

        Func<Task> act = () => _sut.PlaceOrderAsync(testOrder_QuantityInvalid_0);

        var exception = await Assert.ThrowsAsync<InvalidOrderException>(act);

        Assert.Contains("Ordered product should have an amount greater than 0", exception.Message);
    }

    [Fact]
    public async Task PlaceOrderAsync_OrderQuantityBiggerThan999_ShouldThrowException()
    {
        var _sut = new OrderService(this._orderRepositoryMock.Object);

        var testOrder_QuantityInvalid_1000 = new OrderDraft
        {
            ExpectedDeliveryDate = DateTime.Today.AddDays(1),
            OrderedUnits = new List<Interfaces.IOrderUnit>
            {
                new OrderUnit
                {
                    Product = new DefaultProduct(),
                    Quantity = 1000
                }
            }
        };

        Func<Task> act = () => _sut.PlaceOrderAsync(testOrder_QuantityInvalid_1000);

        var exception = await Assert.ThrowsAsync<InvalidOrderException>(act);

        Assert.Contains("Amount of ordered products should not exceed 999", exception.Message);
    }
}