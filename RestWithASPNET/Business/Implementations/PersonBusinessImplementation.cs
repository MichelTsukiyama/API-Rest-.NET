using System;
using System.Collections.Generic;
using RestWithASPNET.Model;
using RestWithASPNET.Context;
using System.Linq;
using RestWithASPNET.Repository;

namespace RestWithASPNET.Business.Implementations
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private readonly IPersonRepository _repository;
        //Era utilizado como contador;
        // private volatile int count;

        public PersonBusinessImplementation(IPersonRepository repository)
        {
            _repository = repository;
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }

        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
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