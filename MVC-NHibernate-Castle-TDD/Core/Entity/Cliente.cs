using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class Cliente : Pessoa<int>
    {

        public virtual string Nome { get; set; }
        public virtual string Telefone { get; set; }
        public virtual string Endereco { get; set; }

    }
}
