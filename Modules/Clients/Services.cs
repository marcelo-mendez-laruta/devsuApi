using devsuApi.Context;
using Microsoft.EntityFrameworkCore;

namespace devsuApi.Modules.Clients
{
    public class Services : Contracts
    {
        public DevsudbContext _db;
        public Services(DevsudbContext db)
        {
            _db = db;
        }
        public Task<Client> CreateClientAsync(newClientModel request)
        {
            Client newClient = new Client();
            Person newPerson = new Person();
            newPerson.Name = request.Name??"No Name";
            newPerson.Address = request.Address;
            newPerson.Phone = request.Phone;
            var x = _db.Persons.Add(newPerson).Entity;
            _db.SaveChanges();
            newClient.PersonId = x.PersonId;
            newClient.Password = request.Password??"123456";
            newClient.State = request.State;
            var response = _db.Clients.Add(newClient).Entity;
            _db.SaveChanges();
            return Task.FromResult(response);
        }

        public Task<bool> DeleteAccountAsync(int id)
        {
            try
            {
                var clientDeleted = _db.Clients.Remove(_db.Clients.Find(id)!).Entity;
                var personDeleted = clientDeleted!=null?_db.Persons.Remove(_db.Persons.Find(clientDeleted.PersonId)!).Entity:null;
                _db.SaveChanges();
                return clientDeleted != null && personDeleted != null ? Task.FromResult(true) : Task.FromResult(false);
            }
            catch (System.Exception)
            {
                return Task.FromResult(false);
            }

        }

        public Task<Client> GetClientAsync(int id)
        {
            var client = _db.Clients.Include("Person").FirstOrDefault(x => x.ClientId == id);
            return client!=null?Task.FromResult(client):Task.FromResult(new Client());
        }

        public Task<List<Client>> GetClientsAsync()
        {
            var clients = _db.Clients.Include("Person").ToList();
            return Task.FromResult(clients);
        }

        public Task<Client> UpdateAccountAsync(int id, newClientModel request)
        {
            try
            {
                var client = _db.Clients.Find(id);
                var person =client!=null? _db.Persons.Find(client.PersonId):null;
                if(client==null || person==null)
                    return Task.FromResult(new Client());
                person.Name = request.Name??"No Name";
                person.Address = request.Address;
                person.Phone = request.Phone;
                client.Password = request.Password??"123456";
                client.State = request.State;
                _db.SaveChanges();
                return Task.FromResult(client);
            }
            catch (System.Exception)
            {
                return Task.FromResult(new Client());
            }

        }
    }
}

