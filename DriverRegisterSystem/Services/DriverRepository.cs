using DriverRegisterSystem.Data;
using DriverRegisterSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DriverRegisterSystem.Services
{
    public class DriverRepository : IDriverRegisterRepository<Driver>
    {
        private readonly AppDbContext _context;
        public DriverRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(Driver driver)
        {
            try
            {
                _context.Drivers.Add(driver);
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

        public async Task Delete(Driver driver)
        {
            try
            {
                _context.Drivers.Remove(driver);
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

        public async Task<IEnumerable<Driver>> GetAll()
        {
            try
            {
                return await _context.Drivers.ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occured: {ex.Message} ; {ex}");
            }
        }

        public async Task<Driver> GetById(int driverId)
        {
            try
            {
                return await _context.Drivers.Include(d => d.Notes).FirstOrDefaultAsync(d => d.DriverId == driverId);
            }
            catch (Exception ex)
            {

                throw new Exception($"An error occured: {ex.Message}, {ex}");
            }
        }

        public async Task Update(Driver driver)
        {
            try
            {
                _context.Update(driver);
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
    }
}
