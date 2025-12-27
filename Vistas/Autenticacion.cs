using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entidades;
using static System.Collections.Specialized.BitVector32;

namespace Vistas
{
    public static class Autenticacion
    {
        public static Usuario ObtenerUsuario()
        {
            var session = HttpContext.Current?.Session;

            if (session == null || session["Usuario"] == null)
            {
                HttpContext.Current.Response.Redirect("InterfazLoguin.aspx", true);
                return null;
            }

            return (Usuario)session["Usuario"];
        }

        public static void SaludarUsuario()
        {

        }
    }
}