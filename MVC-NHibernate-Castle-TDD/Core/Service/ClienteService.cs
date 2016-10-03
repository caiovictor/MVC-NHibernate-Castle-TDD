using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using Core.Factory;

namespace Core.Service
{
    public class ClienteService : IClienteService
    {
        private IClienteFactory _cliente;

        public ClienteService(IClienteFactory clienteFac)
        {
            _cliente = clienteFac;
        }

        public bool Create(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.Nome))
                return false;

            _cliente.Insert(cliente);
            return true;
        }

        public bool Delete(int Idcliente)
        {
            _cliente.Delete(Idcliente);
            return true;
        }

        public Cliente Get(int Idcliente)
        {
            return _cliente.Get(Idcliente);
        }

        [UnitOfWorkAttribute]
        public List<Cliente> GetAll()
        {
            return _cliente.GetAll().OrderByDescending(cliente => cliente.Id).ToList();
        }

        public bool Update(Cliente cliente)
        {
            if (string.IsNullOrEmpty(cliente.Nome))
                return false;

            _cliente.Update(cliente);
            return true;
        }
    }
}
