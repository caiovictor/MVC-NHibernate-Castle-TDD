using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IClienteService
    {
        List<Cliente> GetAll();
        Cliente Get(int Idcliente);
        bool Create(Cliente cliente);
        bool Update(Cliente cliente);
        bool Delete(int Idcliente);
    }
}
