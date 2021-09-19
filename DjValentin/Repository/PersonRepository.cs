using DjValentin.Context;
using DjValentin.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DjValentin.Repository
{
    public class PersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<Person> GetById(int? id)
        {
            return await _context.Persons.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Person> GetByEmail(string email)
        {
            return await _context.Persons.FirstOrDefaultAsync(m => m.Email == email);
        }
    }
}
