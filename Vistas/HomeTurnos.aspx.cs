using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;

namespace Vistas
{
    public partial class HomeTurnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();
            Autorizacion.VerificarPermisos(user.NombreUsuario);

            if (!IsPostBack)
            {
                lblUsuario.Text = "Bienvenido, " + user.NombreUsuario;
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
                
        }
    }
}