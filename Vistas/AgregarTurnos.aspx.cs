using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Entidades;
using System.Data;

namespace Vistas
{
    public partial class Turnos : System.Web.UI.Page
    {
        NegocioTurnos negocioTurnos = new NegocioTurnos();
        NegocioEspecialidades negocioEspecialidades = new NegocioEspecialidades();
        NegocioMedico negocioMedico = new NegocioMedico();
        NegocioPaciente negocioPaciente = new NegocioPaciente();
        NegocioHorariosMedicos negocioHorariosMedicos = new NegocioHorariosMedicos();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();
            Autorizacion.VerificarPermisos(user.NombreUsuario);

            if (!IsPostBack)
            {
                lblUsuario.Text = "Bienvenido, " + user.NombreUsuario;
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlMedicos.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
                CargarEspecialidades();
                CargarPacientes();
            }
        }
        
        public void CargarEspecialidades()
        {
            ddlEspecialidad.DataSource = negocioEspecialidades.ObtenerEspecialidades();
            ddlEspecialidad.DataValueField = "idEspecialidad";
            ddlEspecialidad.DataTextField = "nombreEspecialidad";
            ddlEspecialidad.DataBind();
            ddlEspecialidad.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
        }

        public void CargarMedicos(DataTable dt)
        {
            ddlMedicos.DataSource = dt;
            ddlMedicos.DataValueField = "idMedico";
            ddlMedicos.DataTextField = "nombreCompleto";
            ddlMedicos.DataBind();
        }

        public void CargarPacientes()
        {
            ddlPacientes.DataSource = negocioPaciente.ObtenerPacientes();
            ddlPacientes.DataValueField = "idPaciente";
            ddlPacientes.DataTextField = "datosIdentificadores";
            ddlPacientes.DataBind();
            ddlPacientes.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
        }

        protected void limpiarCampos()
        {
            ddlEspecialidad.SelectedIndex = 0;
            ddlMedicos.SelectedIndex = 0;
            calTurno.SelectedDates.Clear();
            calTurno.DataBind();
            ddlPacientes.SelectedIndex = 0;
            ddlHorarios.Items.Clear();
            ddlHorarios.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
            ddlHorarios.SelectedIndex = 0;
        }

        protected void btnTurno_Click(object sender, EventArgs e)
        {
            try
            {
                Turno turno = new Turno();

                turno.idEspecialidad = ddlEspecialidad.SelectedValue;
                turno.idMedico = int.Parse(ddlMedicos.SelectedValue);
                turno.dia = calTurno.SelectedDate;
                turno.horario = TimeSpan.Parse(ddlHorarios.SelectedValue);
                turno.idPaciente = int.Parse(ddlPacientes.SelectedValue);
                turno.observaciones = "";

                negocioTurnos.AltaTurno(turno);

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "Turno creado exitosamente.";
                limpiarCampos();
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Error: " + ex.Message;
            }
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idEspecialidad = int.Parse(ddlEspecialidad.SelectedValue);

            DataTable dt = negocioMedico.ObtenerMedicosPorIdEspecialidad(idEspecialidad);

            ddlMedicos.Items.Clear();

            if (dt.Rows.Count <= 0)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "No se encontraron médicos para la especialidad seleccionada.";
                ddlMedicos.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
                ddlMedicos.SelectedIndex = 0;
                return; 
            }

            lblMensaje.ForeColor = System.Drawing.Color.Green;
            lblMensaje.Text = "";
            CargarMedicos(dt);
            ddlMedicos.Items.Insert(0, new ListItem("-- Seleccionar --", "0"));
            ddlMedicos.SelectedIndex = 0;
        }

        protected void ddlMedicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idMedico = int.Parse(ddlMedicos.SelectedValue);
            List<HorarioMedico> listaHorarios = negocioHorariosMedicos.ObtenerHorariosMedico(idMedico);

            List<DayOfWeek> diasTrabajo = new List<DayOfWeek>();

            foreach (var item in listaHorarios)
            {
                // IdDia: 1=Lunes ... 7=Domingo
                int idDia = item.IdDia;

                // Convertir de 1-7 (Lun-Dom) → DayOfWeek (Mon-Sun)
                DayOfWeek dia = (DayOfWeek)(idDia % 7);

                diasTrabajo.Add(dia);
            }

            ViewState["DiasTrabajo"] = diasTrabajo;

            calTurno.SelectedDates.Clear();
            calTurno.DataBind();
        }

        protected void calTurno_DayRender(object sender, DayRenderEventArgs e)
        {
            // Bloqueamos días pasados
            if (e.Day.Date < DateTime.Today)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.LightGray;
                return;
            }

            // Si NO hay médico seleccionado deshabilitamos todo
            if (ViewState["DiasTrabajo"] == null || ddlMedicos.SelectedIndex == 0)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.LightGray;
                return;
            }

            // Si ya tenemos cargados los días que trabaja el médico
            List<DayOfWeek> diasTrabajo = (List<DayOfWeek>)ViewState["DiasTrabajo"];

            // Si el día NO es un día laboral de ese médico lo deshabilita
            if (!diasTrabajo.Contains(e.Day.Date.DayOfWeek))
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.LightGray;
            }
        }

        protected void calTurno_SelectionChanged(object sender, EventArgs e)
        {
            DateTime fechaSeleccionada = calTurno.SelectedDate;
            int idMedico = Convert.ToInt32(ddlMedicos.SelectedValue);

            ddlHorarios.Items.Clear();

            // 1) Determino el día elegido de la semana
            int diaSemana = (int)fechaSeleccionada.DayOfWeek;
            if (diaSemana == 0) diaSemana = 7; // Domingo = 0 → 7

            HorarioMedico horarioMedico = negocioHorariosMedicos.ObtenerHorarioMedicoPorDia(idMedico, diaSemana);
            
            if (horarioMedico == null)
                return;

            TimeSpan horaInicio = TimeSpan.Parse(horarioMedico.HoraInicio);
            TimeSpan horaFin = TimeSpan.Parse(horarioMedico.HoraFin);

            // Generamos turnos entre Inicio y Fin y cargamos el DDL Horarios.
            for (TimeSpan h = horaInicio; h <= horaFin; h = h.Add(TimeSpan.FromHours(1)))
            {
                ddlHorarios.Items.Add(h.ToString(@"hh\:mm"));
            }

            // 2) Restamos aquellos horarios donde ya existen turno. Turno con ese mismo doc, ese mismo dia.
            List<Turno> turnosOcupados = negocioTurnos.ObtenerTurnosOcupados(idMedico, fechaSeleccionada);

            foreach (Turno turno in turnosOcupados)
            {
                // Convertimos el TimeSpan a string para comparar con el DropDownList
                string hora = turno.horario.ToString(@"hh\:mm");

                ListItem item = ddlHorarios.Items.FindByText(hora);
                if (item != null)
                    ddlHorarios.Items.Remove(item);
            }


            // Si un dia no hay mas horarios? o dejarlo
        }
    }
}