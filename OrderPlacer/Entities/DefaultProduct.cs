using OrderPlacer.Interfaces;

namespace OrderPlacer.Entities

public class DefaultProduct : IProduct
{
    private decimal _basePrice;
    public decimal BasePrice => _basePrice;

    public DefaultProduct() 
    {
        _basePrice = 98.99m;
    }
}