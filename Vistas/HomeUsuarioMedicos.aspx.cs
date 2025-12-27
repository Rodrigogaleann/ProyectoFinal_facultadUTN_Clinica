using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class HomeUsuarioMedicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();

            if (!IsPostBack)
            {
                lblUsuarioMedico.Text = "Bienvenido, " + user.NombreUsuario;
                lblFechaMedico.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
    }
}