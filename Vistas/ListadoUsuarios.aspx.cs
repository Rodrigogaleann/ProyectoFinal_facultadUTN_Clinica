using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vistas
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        NegocioUsuario negocioUsuarios = new NegocioUsuario();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();
            Autorizacion.VerificarPermisos(user.NombreUsuario);

            if (!IsPostBack)
            {
                lblUsuario.Text = "Bienvenido, " + user.NombreUsuario;
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            gvListaUsuarios.DataSource = negocioUsuarios.ObtenerUsuarios();
            gvListaUsuarios.DataBind();
        }

        protected void gvListaUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListaUsuarios.PageIndex = e.NewPageIndex;
            CargarUsuarios();
        }

        protected void gvListaUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvListaUsuarios.EditIndex = e.NewEditIndex;
            CargarUsuarios();
        }

        protected void gvListaUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvListaUsuarios.EditIndex = -1;
            CargarUsuarios();
            lblMensaje.Text = "";
        }

        protected void gvListaUsuarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                GridViewRow fila = gvListaUsuarios.Rows[e.RowIndex];

                usuario.IdUsuario = gvListaUsuarios.DataKeys[e.RowIndex].Value.ToString();
                usuario.NombreUsuario = ((TextBox)fila.FindControl("txtUsuario")).Text;

                negocioUsuarios.ActualizarUsuario(usuario);

                gvListaUsuarios.EditIndex = -1;
                CargarUsuarios();

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Usuario actualizado correctamente.";
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void gvListaUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CambiarContrasenia")
            {
                string idUsuario = e.CommandArgument.ToString();
                Response.Redirect("CambiarContraseña.aspx?id=" + idUsuario);
            }
        }
    }
}