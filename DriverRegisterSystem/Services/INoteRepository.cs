using DriverRegisterSystem.Models;

namespace DriverRegisterSystem.Services
{
    public interface INoteRepository
    {
        Task Add(Note note);
        Task<IEnumerable<Note>> GetAll();
    }
}
