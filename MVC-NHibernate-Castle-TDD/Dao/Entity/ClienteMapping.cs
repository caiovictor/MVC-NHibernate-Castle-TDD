using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;
using FluentNHibernate.Mapping;

namespace Dao.Entity
{
    public class ClienteMapping : ClassMap<Cliente>
    {
        public ClienteMapping()
        {
            Table("Cliente");
            Id(x => x.Id).Column("ClienteId");
            Map(x => x.Nome);
            Map(x => x.Endereco);
            Map(x => x.Telefone);
        }
    }
}
