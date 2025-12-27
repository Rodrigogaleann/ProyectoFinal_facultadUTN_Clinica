using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        public string IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public bool TipoUsuario { get; set; }
        public string Contrasenia { get; set; }

        public Usuario() { }
    }
}
