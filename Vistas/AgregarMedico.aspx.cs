using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using System.Runtime.InteropServices;

namespace Vistas
{
    public partial class AgregarMedico : System.Web.UI.Page
    {
        NegocioMedico negocioMedicos = new NegocioMedico();
        NegocioProvincias negocioProvincias = new NegocioProvincias();
        NegocioEspecialidades negocioEspecialidades = new NegocioEspecialidades();
        NegocioLocalidades negocioLocalidades = new NegocioLocalidades();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();
            Autorizacion.VerificarPermisos(user.NombreUsuario);

            if (!IsPostBack)
            {
                lblUsuario.Text = "Bienvenido, " + user.NombreUsuario;
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarProvincias();
                CargarEspecialidades();
            }
        }

        public void CargarProvincias()
        {
            List<Provincias> lista = negocioProvincias.ObtenerListadoProvincias();

            CargarDDLProvincia(lista);
        }

        public void CargarEspecialidades()
        {
            ddlEspecialidad.DataSource = negocioEspecialidades.ObtenerEspecialidades();
            ddlEspecialidad.DataValueField = "IdEspecialidad";
            ddlEspecialidad.DataTextField = "nombreEspecialidad";
            ddlEspecialidad.DataBind();

            ddlEspecialidad.Items.Insert(0, new ListItem("-- Seleccione Especialidad --", "0"));
        }

        private void CargarDDLProvincia(List<Provincias> provincias)
        {
            if (provincias != null && provincias.Count > 0)
            {
                ddlProvincia.DataSource = provincias;
                ddlProvincia.DataValueField = "idProvincia";
                ddlProvincia.DataTextField = "provincia";
                ddlProvincia.DataBind();

                ddlProvincia.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione Provincia --", "0"));
            }
        }

        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string IdProv = ddlProvincia.SelectedValue;

            CargarDDLLocalidades(IdProv);
        }

        public void CargarDDLLocalidades(string IdProvincia)
        {
            ddlLocalidad.Items.Clear();
            ddlLocalidad.DataSource = negocioLocalidades.ObtenerLocalidadesPorProvincia(IdProvincia);
            ddlLocalidad.DataTextField = "localidad";
            ddlLocalidad.DataValueField = "IdLocalidades";
            ddlLocalidad.DataBind();

            ddlLocalidad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione Localidad --", "0"));
        }

        private void ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtLegajo.Text.Trim()) || txtLegajo.Text.Trim().Length > 8)
            {
                throw new Exception("El legajo no puede estar vacío ni tener más de 8 caracteres.");
            }

            if (!int.TryParse(txtDni.Text.Trim(), out _) || txtDni.Text.Trim().Length < 7 || txtDni.Text.Trim().Length > 8)
            {
                throw new Exception("El DNI debe ser válido y debe tener entre 7 y 8 números.");
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text.Trim()) || txtApellido.Text.Trim().Length > 40)
            {
                throw new Exception("El apellido no puede estar vacío ni tener más de 40 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text.Trim()) || txtNombre.Text.Trim().Length > 40)
            {
                throw new Exception("El nombre no puede estar vacío ni tener más de 40 caracteres.");
            }

            if (ddlSexo.SelectedIndex == 0)
            {
                throw new Exception("Debe seleccionar un sexo válido.");
            }

            if (string.IsNullOrWhiteSpace(txtNacionalidad.Text.Trim()) || txtNacionalidad.Text.Trim().Length > 30)
            {
                throw new Exception("La nacionalidad no puede estar vacía ni exceder los 30 caracteres.");
            }

            if (!DateTime.TryParse(txtFechaNacimiento.Text.Trim(), out DateTime fecha))
            {
                throw new Exception("La fecha de nacimiento seleccionada no es válida.");
            }

            if (ddlProvincia.SelectedIndex == 0)
            {
                throw new Exception("Debe seleccionar una provincia válida.");
            }

            if (ddlLocalidad.SelectedIndex == 0)
            {
                throw new Exception("Debe seleccionar una localidad válida.");
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text.Trim()) || txtDireccion.Text.Trim().Length > 40)
            {
                throw new Exception("La dirección no puede estar vacía ni exceder los 40 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(txtCorreoElectronico.Text.Trim()) || txtCorreoElectronico.Text.Trim().Length > 60)
            {
                throw new Exception("El correo electrónico no puede estar vacío ni tener más de 60 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text.Trim()) || txtTelefono.Text.Trim().Length != 10 || !txtTelefono.Text.Trim().All(char.IsDigit))
            {
                throw new Exception("El teléfono debe contener solo números y debe tener 10 dígitos.");
            }

            if (ddlEspecialidad.SelectedIndex == 0)
            {
                throw new Exception("Debe seleccionar una especialidad válida.");
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text.Trim()) || txtUsuario.Text.Trim().Length > 40)
            {
                throw new Exception("El usuario no puede estar vacío ni exceder los 40 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(txtContraseña.Text.Trim()) || txtContraseña.Text.Trim().Length > 20)
            {
                throw new Exception("La contraseña no puede estar vacía ni exceder los 20 caracteres.");
            }

            if (txtContraseña.Text.Trim() != txtRepetirContraseña.Text.Trim())
            {
                throw new Exception("Las contraseñas no coinciden.");
            }
        }

        private void LimpiarCampos()
        {
            txtUsuario.Text = "";
            txtContraseña.Text = "";

            txtLegajo.Text = "";
            txtDni.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            ddlLocalidad.SelectedIndex = 0;
            ddlProvincia.SelectedIndex = 0;
            ddlSexo.SelectedIndex = 0;
            txtNacionalidad.Text = "";
            txtFechaNacimiento.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCorreoElectronico.Text = "";
            ddlEspecialidad.SelectedIndex = 0;
            txtRepetirContraseña.Text = "";
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarCampos();

                Medicos medico = new Medicos();
                Usuario usuario = new Usuario();

                usuario.NombreUsuario = txtUsuario.Text.Trim();
                usuario.Contrasenia = txtContraseña.Text.Trim();

                medico.IdMedico = -1; // Nos ayuda a determinar si al verificar existencias, es un nuevo medico o no.
                medico.Legajo = txtLegajo.Text.Trim();
                medico.Dni = txtDni.Text.Trim();
                medico.Nombre = txtNombre.Text.Trim();
                medico.Apellido = txtApellido.Text.Trim();
                medico.IdLocalidad = ddlLocalidad.SelectedValue;
                medico.IdProvincia = ddlProvincia.SelectedValue;
                medico.Sexo = ddlSexo.SelectedValue;
                medico.Nacionalidad = txtNacionalidad.Text.Trim();
                medico.FechaNacimiento = DateTime.Parse(txtFechaNacimiento.Text);
                medico.Direccion = txtDireccion.Text.Trim();
                medico.Telefono = txtTelefono.Text.Trim();
                medico.CorreoElectronico = txtCorreoElectronico.Text.Trim();
                medico.IdEspecialidad = ddlEspecialidad.SelectedValue;

                negocioMedicos.AgregarMedicoYUsuario(usuario, medico);

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Médico y usuario creados correctamente.";

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}