using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class InterfazLoguin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string usario = txtUsuario.Text.Trim();
            string contrasenia = txtContrasenia.Text;

            NegocioUsuario negocio = new NegocioUsuario();

            Usuario usuario = negocio.VerificarAcceso(usario, contrasenia);

            if (usuario != null)
            {
                // guarda el usuario logueado en sesión
                Session["Usuario"] = usuario;


                
                if (usuario.TipoUsuario == true) 
                {
                    Response.Redirect("Home.aspx");
                }
                else 
                {
                    Response.Redirect("HomeUsuarioMedicos.aspx");
                }
            }
            else
            {
                lblIngresoLogin.Text = "Usuario/Contraseña incorrecto";
            }
        }
    }
}