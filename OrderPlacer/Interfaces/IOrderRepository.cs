namespace OrderPlacer.Interfaces

public interface IOrderRepository
{
    void Save(IOrderDraft order);
}