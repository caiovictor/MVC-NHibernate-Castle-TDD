using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entity
{
    public class Pessoa<TPrimaryKey> : IPessoa<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }
    }
}
