using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio;

namespace Vistas
{
    public partial class ListarTurnos : System.Web.UI.Page
    {
        NegocioTurnos negocioTurnos = new NegocioTurnos();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Autenticacion.ObtenerUsuario();
            Autorizacion.VerificarPermisos(user.NombreUsuario);

            if (!IsPostBack)
            {
                lblUsuario.Text = "Bienvenido, " + user.NombreUsuario;
                lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                CargarTurnos();
            }
        }

        private void CargarTurnos()
        {
            try
            {
                DataTable dtTurnos = negocioTurnos.ListarTodosLosTurnos();

                if (dtTurnos != null && dtTurnos.Rows.Count > 0)
                {
                    gvTurnos.DataSource = dtTurnos;
                }
                else
                {
                    lblMensaje.Text = " No hay turnos disponibles para mostrar.";
                    gvTurnos.DataSource = null;
                }

                gvTurnos.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los turnos: " + ex.Message;
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            //lblMensaje.Text = "";
            //lblMensaje.CssClass = "";

            //string dniBuscado = txtBuscar.Text.Trim();
            //string filtroFecha = ddlFiltroFecha.SelectedValue;


            //// filtro x DNI, SOLO si se escribio algo 

            //if (!string.IsNullOrEmpty(dniBuscado))
            //{
            //    // Valida dni
            //    if (!dniBuscado.All(char.IsDigit) || dniBuscado.Length > 10)
            //    {
            //        lblMensaje.Text = "Ingrese solo números (menos de 10 dígitos).";
            //        return;
            //    }

            //    DataTable turnos = negocioTurnos.ObtenerTurnosPorDNI(dniBuscado);



            //    if (turnos == null || turnos.Rows.Count <= 0)
            //    {
            //        lblMensaje.Text = $"No se encontraron turnos para el DNI: {dniBuscado}";
            //        txtBuscar.Text = "";
            //        gvTurnos.DataSource = null;
            //        gvTurnos.DataBind();
            //        return;
            //    }

            //    gvTurnos.DataSource = turnos;
            //    gvTurnos.DataBind();
            //    return;
            //}

            //// 2) filtro por fecha, UNICAMNETE SI NO HAY SE ESCRIBIO NADA EN DNI


            //DataTable dtTurnos = negocioTurnos.ListarTodosLosTurnos();

            //if (dtTurnos == null)
            //{
            //    lblMensaje.Text = "No hay turnos disponibles.";
            //    return;
            //}

            //DateTime hoy = DateTime.Today;

            //switch (ddlFiltroFecha.SelectedValue)
            //{
            //    case "Hoy":
            //        var filasHoy = dtTurnos.Select($"dia = #{hoy:MM/dd/yyyy}#");
            //        if (filasHoy.Length == 0)
            //        {
            //            MostrarResultados("Hoy");
            //            return;
            //        }
            //        dtTurnos = filasHoy.CopyToDataTable();
            //        break;

            //    case "Mañana":
            //        DateTime mañana = hoy.AddDays(1);
            //        var filasMañana = dtTurnos.Select($"dia = #{mañana:MM/dd/yyyy}#");
            //        if (filasMañana.Length == 0)
            //        {
            //            MostrarResultados("Mañana");
            //            return;
            //        }
            //        dtTurnos = filasMañana.CopyToDataTable();
            //        break;

            //    case "Próxima semana":
            //        DateTime semanaFin = hoy.AddDays(7);
            //        var filasSemana = dtTurnos
            //            .Select($"dia >= #{hoy:MM/dd/yyyy}# AND dia <= #{semanaFin:MM/dd/yyyy}#");
            //        if (filasSemana.Length == 0)
            //        {
            //            MostrarResultados("Próxima semana");
            //            return;
            //        }
            //        dtTurnos = filasSemana.CopyToDataTable();
            //        break;

            //    case "Todos":
            //        break;
            //}



            //if (dtTurnos.Rows.Count == 0)
            //{
            //    lblMensaje.Text = $"No hay turnos que coincidan con el filtro '{filtroFecha}'.";
            //    gvTurnos.DataSource = null;
            //    gvTurnos.DataBind();
            //    return;
            //}

            //if (gvTurnos.Rows.Count == 0)
            //{
            //    lblMensaje.Text = $"No hay turnos que mostrar.";
            //    gvTurnos.DataSource = null;
            //    gvTurnos.DataBind();
            //    return;
            //}

            //gvTurnos.DataSource = dtTurnos;
            //gvTurnos.DataBind();
            lblMensaje.Text = string.Empty;
            lblMensaje.CssClass = string.Empty;

            string dniBuscado = txtBuscar.Text.Trim();
            string filtroFecha = ddlFiltroFecha.SelectedValue;

            // --- 1. FILTRO POR DNI (PREVALECE) ---
            if (!string.IsNullOrEmpty(dniBuscado))
            {
                if (!dniBuscado.All(char.IsDigit) || dniBuscado.Length > 10)
                {
                    lblMensaje.Text = "🚨 Ingrese solo números (menos de 10 dígitos).";
                    return;
                }

                DataTable turnos = negocioTurnos.ObtenerTurnosPorDNI(dniBuscado);

                if (turnos == null || turnos.Rows.Count == 0)
                {
                    MostrarResultados($"No se encontraron turnos para el DNI: {dniBuscado}");
                    txtBuscar.Text = string.Empty;
                }
                else
                {
                    gvTurnos.DataSource = turnos;
                    gvTurnos.DataBind();
                }
                return;
            }

            // --- 2. FILTRO POR FECHA (Solo si DNI está vacío) ---

            // Recargamos SIEMPRE la fuente de datos completa
            DataTable dtTurnos = negocioTurnos.ListarTodosLosTurnos();

            if (dtTurnos == null || dtTurnos.Rows.Count == 0)
            {
                MostrarResultados("No hay turnos disponibles en la base de datos.");
                return;
            }

            // Lógica de filtrado de fechas
            DateTime hoy = DateTime.Today;
            DataRow[] filasFiltradas = null;

            switch (filtroFecha)
            {
                case "Hoy":
                    filasFiltradas = dtTurnos.Select($"dia = #{hoy:MM/dd/yyyy}#");
                    break;

                case "Mañana":
                    DateTime mañana = hoy.AddDays(1);
                    filasFiltradas = dtTurnos.Select($"dia = #{mañana:MM/dd/yyyy}#");
                    break;

                case "Próxima semana":
                    // 🔑 CORRECCIÓN 1: Aumentamos el rango para incluir turnos como el 19 y el 29 de Diciembre.
                    // Asumiendo que queremos cubrir el resto del mes (23 días en total, ya que hoy es 8)
                    DateTime inicioProximaSemana = hoy.AddDays(1);
                    DateTime finProximaSemana = hoy.AddDays(23); // Un rango de 3 semanas (21 días) es seguro

                    // 🔑 CORRECCIÓN 2: Asignamos el resultado a la variable AUXILIAR 'filasFiltradas'.
                    filasFiltradas = dtTurnos
                        .Select($"dia >= #{inicioProximaSemana:MM/dd/yyyy}# AND dia <= #{finProximaSemana:MM/dd/yyyy}#");

                    // ❌ CORRECCIÓN 3: Eliminamos el 'if/return/CopyToDataTable' duplicado.
                    break;

                case "Todos":
                    // Si es 'Todos', filasFiltradas permanece nulo, y se usa dtTurnos completo al final.
                    break;
            }

            // --- 3. EVALUACIÓN FINAL DE RESULTADOS (Lógica única para todos los filtros de fecha) ---

            if (filasFiltradas != null)
            {
                // Si no se encontraron resultados
                if (filasFiltradas.Length == 0)
                {
                    MostrarResultados($"No hay turnos que coincidan con el filtro '{filtroFecha}'.");
                    return;
                }

                // Si hay datos, convierte las filas filtradas al DataTable final
                dtTurnos = filasFiltradas.CopyToDataTable();
            }

            // 4. Enlazar el resultado final
            gvTurnos.DataSource = dtTurnos;
            gvTurnos.DataBind();

            // Mostrar mensaje de éxito si se aplicó un filtro
            if (filtroFecha != "Todos")
            {
                lblMensaje.Text = $"✅ Mostrando turnos filtrados por: {filtroFecha}.";
            }
        }

        private void MostrarResultados(string filtro)  //metodo para saber si hay turnos dispobiles en esos dias
        {
            gvTurnos.DataSource = null;
            gvTurnos.DataBind();
            lblMensaje.Text = $"No hay turnos para '{filtro}'.";
            lblMensaje.CssClass = "alerta";
        }

        protected void gvTurnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int indiceColumnaFecha = 3;

                if (DateTime.TryParse(e.Row.Cells[indiceColumnaFecha].Text, out DateTime fecha))
                {
                    e.Row.Cells[indiceColumnaFecha].Text = fecha.ToString("dd/MM/yyyy");
                }

                int indiceColumnaHorario = 4;

                if (DateTime.TryParse(e.Row.Cells[indiceColumnaHorario].Text, out DateTime hora))
                {
                    e.Row.Cells[indiceColumnaHorario].Text = hora.ToString("HH:mm");
                }
            }
        }
    
        protected void gvTurnos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int numTurno = Convert.ToInt32(gvTurnos.DataKeys[e.RowIndex].Value);

            int filasAfectadas = negocioTurnos.BajaLogicaTurno(numTurno);

            if (filasAfectadas <= 0)
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "Se produjo un error al eliminar el turno.";
                return;
            }

            lblMensaje.ForeColor = Color.Green;
            lblMensaje.Text = "Turno dado de baja correctamente.";

            CargarTurnos();
        }

        protected void gvTurnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTurnos.PageIndex = e.NewPageIndex;
            CargarTurnos();
        }

        protected void gvTurnos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTurnos.EditIndex = e.NewEditIndex;
            CargarTurnos();
        }

        protected void gvTurnos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            lblMensaje.Text = "";
            gvTurnos.EditIndex = -1;
            CargarTurnos();
        }

        protected void gvTurnos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int numTurno = Convert.ToInt32(gvTurnos.DataKeys[e.RowIndex].Value);

            GridViewRow fila = gvTurnos.Rows[e.RowIndex];
            TextBox txt = (TextBox)fila.FindControl("txtObsEdit");
            string nuevaObservacion = txt.Text;

            int filasAfectadas = negocioTurnos.ActualizarObservaciones(numTurno, nuevaObservacion);

            if (filasAfectadas <= 0)
            {
                lblMensaje.ForeColor = Color.Red;
                lblMensaje.Text = "Se produjo un error al actualizar las observaciones.";
                return;
            }

            lblMensaje.ForeColor = Color.Green;
            lblMensaje.Text = "Las observaciones se actualizaron correctamente.";

            gvTurnos.EditIndex = -1;
            CargarTurnos();
        }
    }
}