using System;
using System.Collections.Generic;
using RestWithASPNET.Model;
using RestWithASPNET.Context;
using System.Linq;

namespace RestWithASPNET.Repository.Implementations
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySqlContext _context;
        //Era utilizado como contador;
        // private volatile int count;

        public PersonRepositoryImplementation(MySqlContext context)
        {
            _context = context;
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }

            return person;
        }

        public Person Update(Person person)
        {
            if(!Exists(person.Id))
                return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));

            if(result is not null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));

            if(result is not null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }

       
        //Era utilizado para testar a aplicação sem um banco de dados
        // private Person MockPerson(int i)
        // {
        //     return new Person
        //     {
        //         Id = 1,
        //         FirstName = "testeFN" + i,
        //         LastName = "testeLN" + i,
        //         Address = "rua de teste" + i,
        //         Gender = "male"
        //     };
        // }

        //Era utilizado com autoincremento para testar a API sem o banco de dados
        // private long IncrementAndGet()
        // {
        //     return Interlocked.Increment(ref count);
        // }

    }
}