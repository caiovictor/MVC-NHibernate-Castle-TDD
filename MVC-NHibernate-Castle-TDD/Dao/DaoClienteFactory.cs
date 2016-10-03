using Core.Entity;
using Core.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public class DaoClienteFactory : DaoFactory<Cliente, int>, IClienteFactory
    {

    }
}
