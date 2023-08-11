namespace OrderPlacer.Interfaces

public interface IOrderRepository
{
    void Save(IOrder order);
}