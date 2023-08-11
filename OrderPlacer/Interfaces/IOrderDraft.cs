namespace OrderPlacer.Interfaces;

public interface IOrderDraft
{
    List<IOrderUnit> OrderedUnits { get; set; }
    string CustomerId { get; set; }
    DateTime ExpectedDeliveryDate { get; set; }
}