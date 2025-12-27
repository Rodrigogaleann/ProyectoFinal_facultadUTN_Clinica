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
    public partial class WebForm5 : System.Web.UI.Page
    {
        NegocioMedico negocioMedico = new NegocioMedico();
        NegocioHorariosMedicos negocioHorariosMedicos = new NegocioHorariosMedicos();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();
            Autorizacion.VerificarPermisos(user.NombreUsuario);

            if (!IsPostBack)
            {
                lblUsuario.Text = "Bienvenido, " + user.NombreUsuario;
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarMedicos();
                CargarDDlsHoras();
                DeshabilitarCheckBoxes();
            }
        }

        private void CargarMedicos()
        {
            ddlMedicos.DataSource = negocioMedico.ObtenerMedicos();
            ddlMedicos.DataTextField = "nombreCompleto";
            ddlMedicos.DataValueField = "idMedico";
            ddlMedicos.DataBind();
            ddlMedicos.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
        }

        private void CargarDDlsHoras()
        {
            CargarHoras(ddlInicioLunes);
            CargarHoras(ddlFinLunes);

            CargarHoras(ddlInicioMartes);
            CargarHoras(ddlFinMartes);

            CargarHoras(ddlInicioMiercoles);
            CargarHoras(ddlFinMiercoles);

            CargarHoras(ddlInicioJueves);
            CargarHoras(ddlFinJueves);

            CargarHoras(ddlInicioViernes);
            CargarHoras(ddlFinViernes);

            CargarHoras(ddlInicioSabado);
            CargarHoras(ddlFinSabado);

            CargarHoras(ddlInicioDomingo);
            CargarHoras(ddlFinDomingo);
        }

        private void CargarHoras(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));

            for (int hora = 8; hora <= 20; hora++)
            {
                ddl.Items.Add(hora.ToString("00") + ":00");
            }
        }

        private void CargarHorariosMedico(int idMedico)
        {
            List<HorarioMedico> horarios = negocioHorariosMedicos.ObtenerHorariosMedico(idMedico);

            ProcesarDia(1, chkLunes, ddlInicioLunes, ddlFinLunes, horarios);
            ProcesarDia(2, chkMartes, ddlInicioMartes, ddlFinMartes, horarios);
            ProcesarDia(3, chkMiercoles, ddlInicioMiercoles, ddlFinMiercoles, horarios);
            ProcesarDia(4, chkJueves, ddlInicioJueves, ddlFinJueves, horarios);
            ProcesarDia(5, chkViernes, ddlInicioViernes, ddlFinViernes, horarios);
            ProcesarDia(6, chkSabado, ddlInicioSabado, ddlFinSabado, horarios);
            ProcesarDia(7, chkDomingo, ddlInicioDomingo, ddlFinDomingo, horarios);
        }

        private void ProcesarDia(int dia, CheckBox chk, DropDownList ddlInicio, DropDownList ddlFin, List<HorarioMedico> horarios)
        {
            var registro = horarios.FirstOrDefault(x => x.IdDia == dia);

            if (registro != null)
            {
                chk.Checked = true;
                ddlInicio.Enabled = true;
                ddlFin.Enabled = true;

                ddlInicio.SelectedValue = TimeSpan.Parse(registro.HoraInicio).ToString(@"hh\:mm");
                ddlFin.SelectedValue = TimeSpan.Parse(registro.HoraFin).ToString(@"hh\:mm");
            }
        }

        protected void ddlMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarCampos();

            if (ddlMedicos.SelectedValue == "0")
            {
                DeshabilitarCheckBoxes();
                return;
            }

            HabilitarCheckBoxes();
            CargarHorariosMedico(Convert.ToInt32(ddlMedicos.SelectedValue));
            lblMensaje.Text = "";
        }

        private void LimpiarCampos()
        {
            chkLunes.Checked = false;
            chkMartes.Checked = false;
            chkMiercoles.Checked = false;
            chkJueves.Checked = false;
            chkViernes.Checked = false;
            chkSabado.Checked = false;
            chkDomingo.Checked = false;

            ddlInicioLunes.Enabled = false;
            ddlInicioLunes.SelectedIndex = 0;
            ddlFinLunes.Enabled = false;
            ddlFinLunes.SelectedIndex = 0;

            ddlInicioMartes.Enabled = false;
            ddlInicioMartes.SelectedIndex = 0;
            ddlFinMartes.Enabled = false;
            ddlFinMartes.SelectedIndex = 0;

            ddlInicioMiercoles.Enabled = false;
            ddlInicioMiercoles.SelectedIndex = 0;
            ddlFinMiercoles.Enabled = false;
            ddlFinMiercoles.SelectedIndex = 0;

            ddlInicioJueves.Enabled = false;
            ddlInicioJueves.SelectedIndex = 0;
            ddlFinJueves.Enabled = false;
            ddlFinJueves.SelectedIndex = 0;

            ddlInicioViernes.Enabled = false;
            ddlInicioViernes.SelectedIndex = 0;
            ddlFinViernes.Enabled = false;
            ddlFinViernes.SelectedIndex = 0;

            ddlInicioSabado.Enabled = false;
            ddlInicioSabado.SelectedIndex = 0;
            ddlFinSabado.Enabled = false;
            ddlFinSabado.SelectedIndex = 0;

            ddlInicioDomingo.Enabled = false;
            ddlInicioDomingo.SelectedIndex = 0;
            ddlFinDomingo.Enabled = false;
            ddlFinDomingo.SelectedIndex = 0;
        }

        protected void chkDia_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk == chkLunes)
                ToggleDia(chk, ddlInicioLunes, ddlFinLunes);

            else if (chk == chkMartes)
                ToggleDia(chk, ddlInicioMartes, ddlFinMartes);

            else if (chk == chkMiercoles)
                ToggleDia(chk, ddlInicioMiercoles, ddlFinMiercoles);

            else if (chk == chkJueves)
                ToggleDia(chk, ddlInicioJueves, ddlFinJueves);

            else if (chk == chkViernes)
                ToggleDia(chk, ddlInicioViernes, ddlFinViernes);

            else if (chk == chkSabado)
                ToggleDia(chk, ddlInicioSabado, ddlFinSabado);

            else if (chk == chkDomingo)
                ToggleDia(chk, ddlInicioDomingo, ddlFinDomingo);
        }

        private void ToggleDia(CheckBox chk, DropDownList ddlInicio, DropDownList ddlFin)
        {
            bool activar = chk.Checked;

            ddlInicio.Enabled = activar;
            ddlFin.Enabled = activar;

            if (!activar)
            {
                ddlInicio.SelectedIndex = 0;
                ddlFin.SelectedIndex = 0;
            }
        }

        private void DeshabilitarCheckBoxes()
        {
            chkLunes.Enabled = false;
            chkMartes.Enabled = false;
            chkMiercoles.Enabled = false;
            chkJueves.Enabled = false;
            chkViernes.Enabled = false;
            chkSabado.Enabled = false;
            chkDomingo.Enabled = false;
        }

        private void HabilitarCheckBoxes()
        {
            chkLunes.Enabled = true;
            chkMartes.Enabled = true;
            chkMiercoles.Enabled = true;
            chkJueves.Enabled = true;
            chkViernes.Enabled = true;
            chkSabado.Enabled = true;
            chkDomingo.Enabled = true;
        }

        private bool ValidarHorarios()
        {
            return ValidarDia(chkLunes, ddlInicioLunes, ddlFinLunes)
                && ValidarDia(chkMartes, ddlInicioMartes, ddlFinMartes)
                && ValidarDia(chkMiercoles, ddlInicioMiercoles, ddlFinMiercoles)
                && ValidarDia(chkJueves, ddlInicioJueves, ddlFinJueves)
                && ValidarDia(chkViernes, ddlInicioViernes, ddlFinViernes)
                && ValidarDia(chkSabado, ddlInicioSabado, ddlFinSabado)
                && ValidarDia(chkDomingo, ddlInicioDomingo, ddlFinDomingo);
        }

        private bool ValidarDia(CheckBox chk, DropDownList ddlInicio, DropDownList ddlFin)
        {
            if (!chk.Checked)
                return true;

            if (ddlInicio.SelectedIndex == 0 || ddlFin.SelectedIndex == 0)
            {
                lblMensaje.Text = "Debe seleccionar horarios válidos para los días marcados.";
                return false;
            }

            TimeSpan inicio = TimeSpan.Parse(ddlInicio.SelectedValue);
            TimeSpan fin = TimeSpan.Parse(ddlFin.SelectedValue);

            if (inicio >= fin)
            {
                lblMensaje.Text = "La hora de inicio debe ser menor que la hora de fin.";
                return false;
            }

            return true;
        }

        private void GuardarDia(int idMedico, int dia, CheckBox chk, DropDownList ddlInicio, DropDownList ddlFin)
        {
            if (!chk.Checked)
                return;

            HorarioMedico horarioMedico = new HorarioMedico();
            horarioMedico.IdMedico = idMedico;
            horarioMedico.IdDia = dia;
            horarioMedico.HoraInicio = ddlInicio.SelectedValue;
            horarioMedico.HoraFin = ddlFin.SelectedValue;

            negocioHorariosMedicos.InsertarHorario(horarioMedico);
        }

        protected void btnGuardar_OnClick(object sender, EventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (ddlMedicos.SelectedValue == "0")
                {
                    lblMensaje.Text = "Seleccione un médico.";
                    return;
                }

                if (!ValidarHorarios())
                {
                    return;
                }

                int idMedico = int.Parse(ddlMedicos.SelectedValue);

                negocioHorariosMedicos.EliminarHorariosMedico(idMedico);

                GuardarDia(idMedico, 1, chkLunes, ddlInicioLunes, ddlFinLunes);
                GuardarDia(idMedico, 2, chkMartes, ddlInicioMartes, ddlFinMartes);
                GuardarDia(idMedico, 3, chkMiercoles, ddlInicioMiercoles, ddlFinMiercoles);
                GuardarDia(idMedico, 4, chkJueves, ddlInicioJueves, ddlFinJueves);
                GuardarDia(idMedico, 5, chkViernes, ddlInicioViernes, ddlFinViernes);
                GuardarDia(idMedico, 6, chkSabado, ddlInicioSabado, ddlFinSabado);
                GuardarDia(idMedico, 7, chkDomingo, ddlInicioDomingo, ddlFinDomingo);

                lblMensaje.Text = "Horarios guardados correctamente.";
            }
            catch(Exception ex)
            {
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }
    }
}