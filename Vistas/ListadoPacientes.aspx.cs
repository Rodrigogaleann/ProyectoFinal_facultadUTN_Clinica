using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;


namespace Vistas
{
    public partial class ListadoPacientes : System.Web.UI.Page
    {
        NegocioPaciente negocioPaciente = new NegocioPaciente();
        NegocioLocalidades negocioLocalidades = new NegocioLocalidades();
        NegocioProvincias negocioProvincias = new NegocioProvincias();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();
            Autorizacion.VerificarPermisos(user.NombreUsuario);

            if (!IsPostBack)
            {
                lblUsuario.Text = "Bienvenido, " + user.NombreUsuario;
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarPacientes();
            }
        }
        
        private void CargarPacientes()
        {
            try
            {
                gvListaPacientes.DataSource = negocioPaciente.ObtenerPacientes();
                gvListaPacientes.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void gvListaPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvListaPacientes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvListaPacientes.DataKeys[e.RowIndex].Value);

            if (negocioPaciente.BajaPaciente(id))
            {
                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Paciente dado de baja correctamente.";
            }
            else
            {
                lblMensaje.Text = "Error al dar de baja el paciente.";
            }

            CargarPacientes();
        }

        protected void gvListaPacientes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvListaPacientes.EditIndex = e.NewEditIndex;
            CargarPacientes();
        }

        protected void gvListaPacientes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvListaPacientes.EditIndex = -1;
            CargarPacientes();
        }

        protected void gvListaPacientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvListaPacientes.PageIndex = e.NewPageIndex;
            CargarPacientes();
        }

        protected void gvListaPacientes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(gvListaPacientes.DataKeys[e.RowIndex].Value);
                DateTime fechaNacimiento;
                GridViewRow fila = gvListaPacientes.Rows[e.RowIndex];
                DropDownList ddlSexo = (DropDownList)fila.FindControl("ddlSexo");
                DropDownList ddlProvincias = (DropDownList)fila.FindControl("ddlProvincias");
                DropDownList ddlLocalidades = (DropDownList)fila.FindControl("ddlLocalidades");

                Paciente p = new Paciente();
                p.IdPaciente = id;
                p.Nombre = ((TextBox)fila.FindControl("txtNombre")).Text;
                p.Apellido = ((TextBox)fila.FindControl("txtApellido")).Text;
                p.Sexo = ddlSexo.SelectedValue;
                p.Dni = ((TextBox)fila.FindControl("txtDni")).Text;
                p.Nacionalidad = ((TextBox)fila.FindControl("txtNacionalidad")).Text;
                p.Direccion = ((TextBox)fila.FindControl("txtDireccion")).Text;
                p.IdProvincia = ddlProvincias.SelectedValue.ToString();
                p.IdLocalidad = ddlLocalidades.SelectedValue.ToString();
                p.CorreoElectronico = ((TextBox)fila.FindControl("txtCorreo")).Text;
                p.Telefono = ((TextBox)fila.FindControl("txtTelefono")).Text;

                if (!DateTime.TryParse(((TextBox)fila.FindControl("txtFechaNacimiento")).Text, out fechaNacimiento))
                {
                    lblMensaje.Text = "La fecha de nacimiento no es válida.";
                    return;
                }

                p.FechaNacimiento = fechaNacimiento;

                if (negocioPaciente.ModificarPaciente(p))
                {
                    lblMensaje.Text = "Paciente modificado correctamente.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "Error al modificar paciente.";
                }

                gvListaPacientes.EditIndex = -1;
                CargarPacientes();
            }
            catch(Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void gvListaPacientes_SelectedIndexChanged1(object sender, EventArgs e)
        {

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

            gvListaPacientes.EditIndex = row.RowIndex;
        }

        protected void gvListaPacientes_RowDataBound(object sender, GridViewRowEventArgs e)
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

            DropDownList ddlSexo = (DropDownList)e.Row.FindControl("ddlSexo");
            if (ddlSexo != null)
            {
                ddlSexo.Items.Add("Masculino");
                ddlSexo.Items.Add("Femenino");
                ddlSexo.Items.Add("Otro");

                ddlSexo.SelectedValue = DataBinder.Eval(e.Row.DataItem, "sexo").ToString();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string dniBuscado = txtBuscarPorDNI.Text.Trim();

            if (string.IsNullOrEmpty(dniBuscado))
            {
                lblMensaje.Text = "Debe ingresar un DNI para buscar.";
                gvListaPacientes.DataSource = null;
                gvListaPacientes.DataBind();
                return;
            }

            try
            {
                DataTable dtPacientes = negocioPaciente.BuscarPacientePorDni(dniBuscado);

                if (dtPacientes != null && dtPacientes.Rows.Count > 0)
                {

                    gvListaPacientes.DataSource = dtPacientes;
                    gvListaPacientes.DataBind();
                    lblMensaje.Text = "";
                }
                else
                {
                    gvListaPacientes.DataSource = null;
                    gvListaPacientes.DataBind();
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = $"No se encontró ningún paciente con DNI {dniBuscado}.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al buscar paciente: " + ex.Message;
                gvListaPacientes.DataSource = null;
                gvListaPacientes.DataBind();
            }

            txtBuscarPorDNI.Text = string.Empty;

        }

        protected void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            txtBuscarPorDNI.Text = "";
            lblMensaje.Text = "";             
            gvListaPacientes.EditIndex = -1; 

            CargarPacientes();
        }
    }
}