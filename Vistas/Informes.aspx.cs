using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vistas
{
    public partial class Informes : System.Web.UI.Page
    {

        private NegocioInformes negocioInformes = new NegocioInformes();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();
            Autorizacion.VerificarPermisos(user.NombreUsuario);

            if (!IsPostBack)
            {
                lblUsuario.Text = "Bienvenido, " + user.NombreUsuario;
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio = DateTime.ParseExact(txtFechaInicioInforme.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime fechaFin = DateTime.ParseExact(txtFechaFinInforme.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                //validacion para q se ponga fecha
                if (string.IsNullOrWhiteSpace(txtFechaInicioInforme.Text) ||
                  string.IsNullOrWhiteSpace(txtFechaFinInforme.Text))
                {
                    lblInforme.Text = "ingrese una fecha de inicio y una fecha de fin";
                    return;
                }
                int informeSeleccionado = int.Parse(ddlSeleccionarInforme.SelectedValue);

                if (informeSeleccionado == 1)
                {

                    int pacienteAsistencias = negocioInformes.AsistenciaPacietes(fechaInicio, fechaFin);
                    int pacienteFaltas = negocioInformes.InformeNoAsistenciaPacientes(fechaInicio, fechaFin);

                    int totalPacientes = pacienteAsistencias + pacienteFaltas;

                    //validacion para saber si no hay ningun dato para comparar
                    if (totalPacientes == 0)
                    {
                        lblInforme.Text = "No hay datos para hacer el informe.";
                    }
                    else
                    {
                        //hace las cuentas para sacar el porcentaje
                        float porcentajeAsistencias = (pacienteAsistencias * 100f) / totalPacientes;
                        float porcentajeFaltas = (pacienteFaltas * 100f) / totalPacientes;
                        //el :F2 trunca los decimales: ej si una cuenta da 53.3333...% con el F2 se veria 53.33%
                        lblInforme.Text =
                            $"porcentaje de pacientes que asistieron a la consulta: {porcentajeAsistencias:F2}%<br/><br/>" +
                            $" porcentaje de pacientes que no asistieron:  {porcentajeFaltas:F2}%";

                    }
                }
                else
                {
                    int medicosConActividad = negocioInformes.MedicosConActividad(fechaInicio, fechaFin);
                    int medicosSinActividad = negocioInformes.MedicosSinActividad(fechaInicio, fechaFin);

                    int totalActividadMedica = medicosConActividad + medicosSinActividad;

                    if (totalActividadMedica == 0)
                    {
                        lblInforme.Text = "No hay datos para hacer el informe";
                    }
                    else
                    {
                        float porcentajeActividad = (medicosConActividad * 100f) / totalActividadMedica;
                        float porcentajeSinActividad = (medicosSinActividad * 100f) / totalActividadMedica;
                        lblInforme.Text =
                            $"Porcentaje de médicos con actividad: {porcentajeActividad:F2}%<br/><br/>" +
                            $"Porcentaje de médicos sin actividad: {porcentajeSinActividad:F2}%";
                    }
                }
            }
            catch (Exception ex)
            {
                lblInforme.Text = $"Error al generar informe: {ex.Message}";

            }
        }
    }
}