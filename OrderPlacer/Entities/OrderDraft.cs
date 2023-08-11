using OrderPlacer.Interfaces;

namespace OrderPlacer.Entities;

public class OrderDraft
{
    public List<IOrderUnit> OrderedUnits { get; set; }
    public string CustomerId { get; set; }
    public DateTime ExpectedDeliveryDate { get; set; }
}