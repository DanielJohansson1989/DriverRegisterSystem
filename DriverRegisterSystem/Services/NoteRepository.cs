using DriverRegisterSystem.Data;
using DriverRegisterSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DriverRegisterSystem.Services
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;
        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Note note)
        {
            try
            {
                _context.Notes.Add(note);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Database update error: {dbEx.Message} , {dbEx}");
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occured: {ex.Message} , {ex}");
            }
        }

        public async Task<IEnumerable<Note>> GetAll()
        {
            try
            {
                return await _context.Notes.Include(n => n.Driver).ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occured: {ex.Message} , {ex}");
            }
        }
    }
}
