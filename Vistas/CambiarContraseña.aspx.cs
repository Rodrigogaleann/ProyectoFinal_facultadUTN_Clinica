using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vistas
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        NegocioUsuario negocioUsuario = new NegocioUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();
            Autorizacion.VerificarPermisos(user.NombreUsuario);

            if (!IsPostBack)
            {
                lblUsuario.Text = "Bienvenido, " + user.NombreUsuario;
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

                string id = Request.QueryString["id"];

                Usuario usuario = negocioUsuario.ObtenerUsuarioPorId(id);

                if (usuario == null)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "No se ha encontrado un usuario con ese ID. Vuelva a intentarlo.";
                    btnCambiar.Visible = false;
                    return;
                }

                lblUsuario.Text = usuario.NombreUsuario;
            }
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                string id = Request.QueryString["id"];

                if (String.IsNullOrEmpty(txtNuevaContrasenia.Text) || string.IsNullOrEmpty(txtRepetirContrasenia.Text))
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "Las contraseñas no son válidas.";
                    return;
                }

                if (txtNuevaContrasenia.Text != txtRepetirContrasenia.Text)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "Las contraseñas no coinciden.";
                    return;
                }

                negocioUsuario.ActualizarContrasenia(id, txtNuevaContrasenia.Text);

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Contraseña cambiada con éxito.";
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}