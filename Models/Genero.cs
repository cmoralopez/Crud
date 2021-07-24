using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud.Models
{
    public class Genero
    {
        public Genero()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int Codigo { get; set; }
        public string Detalles { get; set; }
        public int Estado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
