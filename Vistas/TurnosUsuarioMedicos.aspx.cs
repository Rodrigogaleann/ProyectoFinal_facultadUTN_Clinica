using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vistas
{
    public partial class TurnosUsuarioMedicos : System.Web.UI.Page
    {
        NegocioTurnos negocioTurnos = new NegocioTurnos();
        NegocioMedico negocioMedico = new NegocioMedico();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();

            if (!IsPostBack)
            {
                lblUsuario.Text = "Bienvenido, " + user.NombreUsuario;
                lblFechaMedico.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarTurnosAsignados();
                rblAsistencia.Enabled = false;
                txtObservacion.Enabled = false;
                btnGuardarObservacion.Enabled = false;
            }
        }

        protected void gvTurnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvTurnos.SelectedDataKey == null)
            {
                lblMensaje.Text = "Error: No se pudo obtener la clave del turno seleccionado.";
                return;
            }

            rblAsistencia.Enabled = true;
            btnGuardarObservacion.Enabled = true;
            txtObservacion.Enabled = true;

            int numTurno = Convert.ToInt32(gvTurnos.SelectedDataKey.Value);

            bool asistencia = negocioTurnos.VerificarAsistencia(numTurno);

            rblAsistencia.Items[0].Selected = asistencia; // Presente
            rblAsistencia.Items[1].Selected = !asistencia; // Ausente

            txtObservacion.Text = negocioTurnos.ObtenerObservaciones(numTurno);
        }

        private void CargarTurnosAsignados()
        {
            try
            {
                Usuario user = Autenticacion.ObtenerUsuario();
                Medicos medico = negocioMedico.ObtenerMedicoPorIdUsuario(user.IdUsuario);

                List<Turno> listaTurnos = negocioTurnos.ObtenerTurnosAsignados(medico.IdMedico.ToString());

                if (listaTurnos.Count <= 0)
                {
                    lblMensaje.Text = " No hay turnos disponibles para mostrar.";
                    gvTurnos.DataSource = null;
                    return;
                }

                gvTurnos.DataSource = listaTurnos;
                gvTurnos.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los turnos: " + ex.Message;
            }
        }

        protected void gvTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int indiceColumnaFecha = 4;

                if (DateTime.TryParse(e.Row.Cells[indiceColumnaFecha].Text, out DateTime fecha))
                {
                    e.Row.Cells[indiceColumnaFecha].Text = fecha.ToString("dd/MM/yyyy");
                }

                int indiceColumnaHorario = 5;

                if (DateTime.TryParse(e.Row.Cells[indiceColumnaHorario].Text, out DateTime hora))
                {
                    e.Row.Cells[indiceColumnaHorario].Text = hora.ToString("HH:mm");
                }
            }
        }

        protected void btnGuardarObservacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvTurnos.SelectedDataKey == null)
                {
                    lblMensaje.Text = "Error: No se pudo obtener la clave del turno seleccionado.";
                    return;
                }

                int numTurno = Convert.ToInt32(gvTurnos.SelectedDataKey.Value);
                bool asistencia = rblAsistencia.Items[0].Selected;
                string observaciones = txtObservacion.Text;

                negocioTurnos.ActualizarAsistencia(numTurno, asistencia);
                negocioTurnos.ActualizarObservaciones(numTurno, observaciones);

                CargarTurnosAsignados();

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Asistencia y observaciones actualizadas con éxito.";

                txtObservacion.Text = "";
            }
            catch(Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                string dniBusqueda = txtBuscar.Text.Trim();
                
                if (string.IsNullOrWhiteSpace(dniBusqueda))
                {
                    CargarTurnosAsignados();
                    return;
                }

                Usuario user = Autenticacion.ObtenerUsuario();
                Medicos medico = negocioMedico.ObtenerMedicoPorIdUsuario(user.IdUsuario);

                DataTable dtTurnos = negocioTurnos.ObtenerTurnosAsignadosPorDNI(medico.IdMedico.ToString(), dniBusqueda);

                if (dtTurnos == null || dtTurnos.Rows.Count <= 0)
                {
                    gvTurnos.DataSource = null;
                    gvTurnos.DataBind();
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = $"No se encontró ningún médico con el DNI: {dniBusqueda}.";
                    return;
                }

                gvTurnos.DataSource = dtTurnos;
                gvTurnos.DataBind();
                lblMensaje.Text = "";
            }
            catch(Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error al buscar médico: " + ex.Message;
            }
        }
    }
}