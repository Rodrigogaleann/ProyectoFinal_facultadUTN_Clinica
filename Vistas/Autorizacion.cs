using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entidades;
using Negocio;

namespace Vistas
{
    public class Autorizacion
    {
        private static NegocioUsuario negocioUsuario = new NegocioUsuario();

        public static void VerificarPermisos(string nombreUsuario)
        {
            var session = HttpContext.Current?.Session;

            if (!negocioUsuario.EsAdministrador(nombreUsuario) ||
                session == null ||
                session["Usuario"] == null)
            {
                HttpContext.Current.Response.Redirect("NoAutorizado.aspx", true);
                return;
            }

            // No hacemos nada si es admin.
        }
    }
}