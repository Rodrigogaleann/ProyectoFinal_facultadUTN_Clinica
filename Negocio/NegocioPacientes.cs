using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class NegocioPaciente
    {
        private DatosPaciente datos = new DatosPaciente();

        public bool AgregarPaciente(Paciente p)
        {
            return datos.AgregarPaciente(p);
        }

        public DataTable ObtenerPacientes()
        {
            return datos.ObtenerPacientes();
        }

        public List<Paciente> ListarPacientes()
        {
            return datos.ListarPacientes();
        }
        
        public bool BajaPaciente(int idPaciente)
        {
            return datos.BajaPaciente(idPaciente);
        }

        public bool ModificarPaciente(Paciente p)
        {
            VerificarExistenciaPaciente(p);

            return datos.ModificarPaciente(p);
        }

        public DataTable BuscarPacientePorDni(string dni)
        {
            if (string.IsNullOrEmpty(dni))
            {

                return new DataTable();

            }

            return datos.BuscarPacientePorDni(dni);
        }

        /*public Paciente BuscarPaciente(string dniPaciente)
        {
            return datos.BuscarPaciente(dniPaciente);
        }*/

        public void VerificarExistenciaPaciente(Paciente paciente)
        {
            if (datos.YaExisteDni(paciente.Dni, paciente.IdPaciente))
            {
                throw new Exception("Ya existe un paciente con el DNI" + paciente.Dni);
            }
            if (datos.YaExisteCorreo(paciente.CorreoElectronico, paciente.IdPaciente))
            {
                throw new Exception("Ya existe un paciente con el correo electronico" + paciente.CorreoElectronico);
            }
            if (datos.YaExisteTelefono(paciente.Telefono, paciente.IdPaciente))
            {
                throw new Exception("Ya existe un paciente con el telefono" + paciente.Telefono);
            }
        }
    }
}
