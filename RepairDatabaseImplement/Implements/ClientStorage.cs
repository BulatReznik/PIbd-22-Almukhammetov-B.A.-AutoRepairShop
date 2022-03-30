using RepairContracts.BindingModels;
using RepairContracts.StorageContracts;
using RepairContracts.ViewModels;
using RepairDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairDatabaseImplement.Implements
{
    public class ClientStorage: IClientStorage
    {
        public List<ClientViewModel> GetFullList()
        {
            using (var context = new RepairDatabase())
            return context.Clients.Select(CreateModel).ToList();
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new RepairDatabase();
            return context.Clients.Where(rec => rec.Email == model.Email && rec.Password == model.Password).Select(CreateModel).ToList();
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new RepairDatabase();
            var client = context.Clients.FirstOrDefault(rec => rec.Email == model.Email || rec.Id == model.Id);
            return client != null ? CreateModel(client) : null;
        }

        public void Insert(ClientBindingModel model)
        {
            using var context = new RepairDatabase();
            context.Clients.Add(CreateModel(model, new Client()));
            context.SaveChanges();
        }

        public void Update(ClientBindingModel model)
        {
            using var context = new RepairDatabase();
            var element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Клиент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        public void Delete(ClientBindingModel model)
        {
            using var context = new RepairDatabase();
            Client client = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (client != null)
            {
                context.Clients.Remove(client);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Клиент не найден");
            }
        }

        private Client CreateModel(ClientBindingModel model, Client client)
        {
            client.ClientFIO = model.ClientFIO;
            client.Email = model.Email;
            client.Password = model.Password;
            return client;
        }
        private static ClientViewModel CreateModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                Email = client.Email,
                ClientFIO = client.ClientFIO,
                Password = client.Password
            };
        }
    }
}
