namespace Kiosk.WebAPI.Persistance
{
    public interface IKioskUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }

        void Commit();
    }
}
