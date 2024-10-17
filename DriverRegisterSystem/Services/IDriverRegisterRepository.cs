namespace DriverRegisterSystem.Services
{
    public interface IDriverRegisterRepository<T>
    {
        Task Add(T entity);
        Task Delete(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Update(T entity);
    }
}
