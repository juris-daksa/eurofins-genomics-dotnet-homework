namespace OrderPlacer.Interfaces

public interface IOrder
{
    List<IOrderUnit> OrderedUnits { get; set; }
    string CustomerId { get; set; }
    DateTime ExpectedDeliveryDate { get; set; }
    decimal PriceSubTotal { get; set; }
    decimal PriceTotal { get; set; }
}