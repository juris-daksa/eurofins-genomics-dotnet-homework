using OrderPlacer.Interfaces;
using System.Runtime.CompilerServices;

namespace OrderPlacer.Entities

public class Order
{
    private decimal _priceTotal;
    private decimal _priceSubTotal;
    private List<IOrderUnit> _orderUnits;
    private string _customerId;
    private DateTime _expectedDeliveryDate;

    public List<IOrderUnit> OrderedUnits { get => _orderUnits; set => _orderUnits = value; }
    public string CustomerId { get => _customerId; set => _customerId = value; }
    public DateTime ExpectedDeliveryDate { get => _expectedDeliveryDate; set => _expectedDeliveryDate = value; }
    public decimal PriceTotal { get => _priceTotal; set => _priceTotal = value; }
    public decimal PriceSubTotal { get => _priceSubTotal; set => _priceSubTotal = value; }


    public Order()
    {
        PriceTotal = 0;
        PriceSubTotal = 0;
        _customerId= string.Empty;
        _expectedDeliveryDate = DateTime.MinValue;
    }
}