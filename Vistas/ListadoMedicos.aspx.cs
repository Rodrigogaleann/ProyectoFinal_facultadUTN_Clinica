using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;
using static System.Net.Mime.MediaTypeNames;

namespace Vistas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        NegocioMedico negocioMedicos = new NegocioMedico();
        NegocioUsuario negocioUsuario = new NegocioUsuario();
        NegocioProvincias negocioProvincias = new NegocioProvincias();
        NegocioLocalidades negocioLocalidades = new NegocioLocalidades();
        NegocioEspecialidades negocioEspecialidades = new NegocioEspecialidades();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();
            Autorizacion.VerificarPermisos(user.NombreUsuario);

            if (!IsPostBack)
            {
                lblUsuario.Text = "Bienvenido, " + user.NombreUsuario;
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarMedicos();
            }
        }

        private void CargarMedicos()
        {
            gvListaMedicos.DataSource = negocioMedicos.ObtenerMedicos();
            gvListaMedicos.DataBind();
        }

        protected void gvListaMedicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListaMedicos.PageIndex = e.NewPageIndex;
            CargarMedicos();
        }

        protected void gvListaMedicos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvListaMedicos.EditIndex = e.NewEditIndex;
            CargarMedicos();
        }

        protected void gvListaMedicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvListaMedicos.EditIndex = -1;
            CargarMedicos();
            lblMensaje.Text = "";
        }

        protected void ddlProvincias_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlProvincias = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlProvincias.NamingContainer;

            string nuevaProvincia = ddlProvincias.SelectedValue;

            DropDownList ddlLocalidades = (DropDownList)row.FindControl("ddlLocalidades");

            ddlLocalidades.DataSource = negocioLocalidades.ObtenerLocalidades(nuevaProvincia);
            ddlLocalidades.DataTextField = "localidad";
            ddlLocalidades.DataValueField = "IdLocalidades";
            ddlLocalidades.DataBind();

            ddlLocalidades.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));

            gvListaMedicos.EditIndex = row.RowIndex;
        }

        protected void gvListaMedicos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) <= 0)
            {
                return;
            }

            DropDownList ddlProvincias = (DropDownList)e.Row.FindControl("ddlProvincias");
            if (ddlProvincias != null)
            {
                ddlProvincias.DataSource = negocioProvincias.ObtenerProvincias();
                ddlProvincias.DataTextField = "provincia";
                ddlProvincias.DataValueField = "idProvincia";
                ddlProvincias.DataBind();

                ddlProvincias.SelectedValue = DataBinder.Eval(e.Row.DataItem, "idProvincia").ToString();
            }
                
            DropDownList ddlLocalidades = (DropDownList)e.Row.FindControl("ddlLocalidades");
            if (ddlLocalidades != null)
            {
                ddlLocalidades.DataSource = negocioLocalidades.ObtenerLocalidades(ddlProvincias.SelectedValue);
                ddlLocalidades.DataTextField = "localidad";
                ddlLocalidades.DataValueField = "IdLocalidades";
                ddlLocalidades.DataBind();

                ddlLocalidades.SelectedValue = DataBinder.Eval(e.Row.DataItem, "idLocalidad").ToString();
            }

            DropDownList ddlEspecialidades = (DropDownList)e.Row.FindControl("ddlEspecialidades");
            if (ddlEspecialidades != null)
            {
                ddlEspecialidades.DataSource = negocioEspecialidades.ObtenerEspecialidades();
                ddlEspecialidades.DataTextField = "nombreEspecialidad";
                ddlEspecialidades.DataValueField = "IdEspecialidad";
                ddlEspecialidades.DataBind();

                ddlEspecialidades.SelectedValue = DataBinder.Eval(e.Row.DataItem, "IdEspecialidad").ToString();
            }
                
            DropDownList ddlSexo = (DropDownList)e.Row.FindControl("ddlSexo");
            if (ddlSexo != null)
            {
                ddlSexo.Items.Add("Masculino");
                ddlSexo.Items.Add("Femenino");
                ddlSexo.Items.Add("Otro");

                ddlSexo.SelectedValue = DataBinder.Eval(e.Row.DataItem, "sexo").ToString();
            }
        }

        protected void gvListaMedicos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Medicos medico = new Medicos();
                DateTime fechaNacimiento;
                GridViewRow fila = gvListaMedicos.Rows[e.RowIndex];
                DropDownList ddlSexo = (DropDownList)fila.FindControl("ddlSexo");
                DropDownList ddlProvincias = (DropDownList)fila.FindControl("ddlProvincias");
                DropDownList ddlLocalidades = (DropDownList)fila.FindControl("ddlLocalidades");
                DropDownList ddlEspecialidades = (DropDownList)fila.FindControl("ddlEspecialidades");

                medico.IdMedico = Convert.ToInt32(gvListaMedicos.DataKeys[e.RowIndex].Value);
                medico.Legajo = ((TextBox)fila.FindControl("txtLegajo")).Text;
                medico.Dni = ((TextBox)fila.FindControl("txtDni")).Text;
                medico.Nombre = ((TextBox)fila.FindControl("txtNombre")).Text;
                medico.Apellido = ((TextBox)fila.FindControl("txtApellido")).Text;
                medico.Sexo = ddlSexo.SelectedValue;
                medico.Nacionalidad = ((TextBox)fila.FindControl("txtNacionalidad")).Text;
                medico.IdProvincia = ddlProvincias.SelectedValue.ToString();
                medico.IdLocalidad = ddlLocalidades.SelectedValue.ToString();
                medico.Direccion = ((TextBox)fila.FindControl("txtDireccion")).Text;
                medico.CorreoElectronico = ((TextBox)fila.FindControl("txtCorreoElectronico")).Text;
                medico.Telefono = ((TextBox)fila.FindControl("txtTelefono")).Text;
                medico.IdEspecialidad = ddlEspecialidades.SelectedValue.ToString();

                if (!DateTime.TryParse(((TextBox)fila.FindControl("txtFechaNacimiento")).Text, out fechaNacimiento))
                {
                    lblMensaje.Text = "La fecha de nacimiento no es válida.";
                    return;
                }

                medico.FechaNacimiento = fechaNacimiento;

                negocioMedicos.ActualizarMedico(medico);

                gvListaMedicos.EditIndex = -1;
                CargarMedicos();

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Médico actualizado correctamente.";
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void gvListaMedicos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int idMedico = Convert.ToInt32(gvListaMedicos.DataKeys[e.RowIndex].Value);
            string idUsuario = gvListaMedicos.DataKeys[e.RowIndex].Values["idUsuario"].ToString();

            int filasAfectadas = negocioMedicos.BajaLogicaMedico(idMedico);

            if (filasAfectadas <= 0)
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "Se produjo un error al eliminar el médico.";
                return;
            }

            int filasAfectadas2 = negocioUsuario.BajaLogicaUsuario(idUsuario);

            if (filasAfectadas2 <= 0)
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "Se produjo un error al eliminar el usuario asignado.";
                return;
            }

            lblMensaje.ForeColor = Color.Green;
            lblMensaje.Text = "Médico y usuario dados de baja correctamente.";

            CargarMedicos();
        }

        protected void btnBuscarMedico_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = string.Empty;
            gvListaMedicos.DataSource = null;
            gvListaMedicos.DataBind(); // Limpiar la grilla antes de buscar

            string dniBusqueda = txtDNI.Text.Trim();

            try
            {
                NegocioMedico neg = new NegocioMedico();
                DataTable dtMedico = neg.ObtenerMedicoPorDNI(dniBusqueda);

                if (dtMedico != null && dtMedico.Rows.Count > 0)
                {
                    
                    gvListaMedicos.DataSource = dtMedico;
                    gvListaMedicos.DataBind();
                    lblMensaje.Text = "";
                }
                else
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = $"No se encontró ningún médico con el DNI: {dniBusqueda}.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error al buscar médico: " + ex.Message;
            }
        }

        protected void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            CargarMedicos();
            lblMensaje.Text = "";
            txtDNI.Text = "";
        }
    }
}