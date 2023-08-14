using OrderPlacer.Interfaces;
using System.Runtime.CompilerServices;
using OrderPlacer.Interfaces;

namespace OrderPlacer.Entities;

public class Order : IOrder
{
    private string _id;
    private decimal _priceTotal;
    private decimal _priceSubTotal;
    private string _customerId;
    private DateTime _expectedDeliveryDate;

    public string Id { get => _id; set => _id = value; }
    public List<IOrderUnit> OrderedUnits { get; set; }
    public string CustomerId { get => _customerId; set => _customerId = value; }
    public DateTime ExpectedDeliveryDate { get => _expectedDeliveryDate; set => _expectedDeliveryDate = value; }
    public decimal PriceTotal { get => _priceTotal; set => _priceTotal = value; }
    public decimal PriceSubTotal { get => _priceSubTotal; set => _priceSubTotal = value; }

    public Order()
    {
        Id = Guid.NewGuid().ToString();
        PriceTotal = 0;
        PriceSubTotal = 0;
        CustomerId= string.Empty;
        ExpectedDeliveryDate = DateTime.MinValue;
    }
}