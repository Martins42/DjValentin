using DjValentin.Context;
using DjValentin.Models;
using DjValentin.Repository;
using System;
using System.Threading.Tasks;

namespace DjValentin.Services
{
    public class PersonService
    {
        private readonly PersonRepository _personRepository;

        public PersonService(AppDbContext context)
        {
            _personRepository = new PersonRepository(context);
        }

        public Task<Person> GetById(int? id)
        {
            return _personRepository.GetById(id);
        }

        public Task<Person> GetByEmail(string email)
        {
            return _personRepository.GetByEmail(email);
        }        
    }
}
