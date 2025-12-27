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
    public partial class AgregarPaciente : System.Web.UI.Page
    {
        NegocioPaciente negocio = new NegocioPaciente();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();
            Autorizacion.VerificarPermisos(user.NombreUsuario);

            if (!IsPostBack)
            {
                lblUsuario.Text = "Bienvenido, " + user.NombreUsuario;
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarProvincias();
            }
        }

        public void CargarProvincias()
        {
            NegocioProvincias negocioProvincia = new NegocioProvincias();

            List<Provincias> lista = negocioProvincia.ObtenerListadoProvincias();

            CargarDDLProvincia(lista);
        }

        private void CargarDDLProvincia(List<Provincias> provincias)
        {
            if (provincias != null && provincias.Count > 0)
            {
                ddlProvinciaPaciente.DataSource = provincias;
                ddlProvinciaPaciente.DataValueField = "idProvincia";
                ddlProvinciaPaciente.DataTextField = "provincia";
                ddlProvinciaPaciente.DataBind();

                ddlProvinciaPaciente.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione Provincia --", "0"));
            }
        }

        protected void ddlProvincia_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string IdProv = ddlProvinciaPaciente.SelectedValue;

            CargarDDLLocalidades(IdProv);
        }

        public void CargarDDLLocalidades(string IdProvincia)
        {
            NegocioLocalidades negocioLocalidades = new NegocioLocalidades();

            ddlLocalidad.Items.Clear();
            ddlLocalidad.DataSource = negocioLocalidades.ObtenerLocalidadesPorProvincia(IdProvincia);
            ddlLocalidad.DataTextField = "localidad";
            ddlLocalidad.DataValueField = "IdLocalidades";
            ddlLocalidad.DataBind();

            ddlLocalidad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione Localidad --", "0"));
        }

        private void LimpiarCampos()
        {
            txtDNIpaciente.Text = "";
            txtNombrePaciente.Text = "";
            txtApellidoPaciente.Text = "";
            DropDownListSexo.SelectedIndex = 0;
            txtNacionalidadPaciente.Text = "";
            txtFNacimientoPaciente.Text = "";
            txtDireccionPaciente.Text = "";
            ddlProvinciaPaciente.SelectedIndex = 0;
            ddlLocalidad.SelectedIndex = 0;
            txtCElectronicoPaciente.Text = "";
            txtTelefonoPaciente.Text = "";
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Paciente p = new Paciente();
                p.IdPaciente = -1;
                p.Nombre = txtNombrePaciente.Text.Trim();
                p.Apellido = txtApellidoPaciente.Text.Trim();
                p.Dni = txtDNIpaciente.Text;
                p.Sexo = DropDownListSexo.SelectedValue;
                p.Nacionalidad = txtNacionalidadPaciente.Text;
                p.FechaNacimiento = DateTime.Parse(txtFNacimientoPaciente.Text);
                p.Direccion = txtDireccionPaciente.Text;
                p.CorreoElectronico = txtCElectronicoPaciente.Text;
                p.Telefono = txtTelefonoPaciente.Text;
                p.IdProvincia = ddlProvinciaPaciente.SelectedValue;
                p.IdLocalidad = ddlLocalidad.SelectedValue;

                negocio.VerificarExistenciaPaciente(p);

                bool estado = negocio.AgregarPaciente(p);


                if (estado)
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Paciente agregado correctamente";

                    LimpiarCampos();
                }
                else
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "No se puedo agregar el paciente";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}