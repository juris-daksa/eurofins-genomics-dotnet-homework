namespace OrderPlacer.Interfaces;

public interface IOrderUnit
{
    IProduct Product { get; set; }
    int Quantity { get; set; }
}