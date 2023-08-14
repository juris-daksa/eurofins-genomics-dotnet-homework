using OrderPlacer.Interfaces;

namespace OrderPlacer.Entities;

public class OrderUnit : IOrderUnit
{
    private IProduct _product;
    private int _quantity;
    public IProduct Product { get => _product; set => _product = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }
}