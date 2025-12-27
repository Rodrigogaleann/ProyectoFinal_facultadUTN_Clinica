using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;
using static Datos.AccesoDatos;

namespace Negocio
{
    public class NegocioUsuario
    {
        private DatosUsuarios usuarios = new DatosUsuarios();

        public Usuario VerificarAcceso(string nombreUsuario, string contrasenia)
        {
            Usuario usuario = usuarios.validarAcceso(nombreUsuario, contrasenia);

            if (usuario == null)
            {
                return null;
            }
            else { return usuario; }
        }

        public bool AgregarUsuario(Usuario us)
        {
            return usuarios.AgregarUsuario(us);
        }

        public bool UsuarioExiste(string usuario)
        {
            return usuarios.Existe(usuario);
        }
    
        public DataTable ObtenerUsuarios()
        {
            return usuarios.ObtenerUsuarios();
        }
    
        public Usuario ObtenerUsuarioPorId(string id)
        {
            return usuarios.ObtenerUsuarioPorId(id);
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            if (usuarios.Existe(usuario.NombreUsuario, usuario.IdUsuario))
            {
                throw new Exception("El usuario " + usuario.NombreUsuario + " ya está en uso.");
            }

            usuarios.ActualizarUsuario(usuario);
        }
    
        public void ActualizarContrasenia(string idUsuario, string nuevaContrasenia)
        {
            if (string.IsNullOrEmpty(nuevaContrasenia))
            {
                throw new Exception("La nueva contraseña es inválida.");
            }

            usuarios.ActualizarContrasenia(idUsuario, nuevaContrasenia);
        }
    
        public int BajaLogicaUsuario(string idUsuario)
        {
            return usuarios.BajaLogicaUsuario(idUsuario);
        }
    
        public bool EsAdministrador(string nombreUsuario) 
        {
            return usuarios.EsAdministrador(nombreUsuario);
        }
    }
}
