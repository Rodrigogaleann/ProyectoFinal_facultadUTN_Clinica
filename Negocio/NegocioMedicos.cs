using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Negocio
{
    public class NegocioMedico
    {
        private DatosMedico datosMedicos = new DatosMedico();
        private NegocioUsuario negocioUsuarios = new NegocioUsuario();

        public DataTable ObtenerMedicos()
        {
            return datosMedicos.ObtenerMedicos();
        }

        public void VerificarExistencias(Medicos medico)
        {
            if (datosMedicos.ExistePorLegajo(medico.Legajo, medico.IdMedico))
            {
                throw new Exception("Ya existe un médico con el legajo: " + medico.Legajo);
            }

            if (datosMedicos.ExistePorDNI(medico.Dni, medico.IdMedico))
            {
                throw new Exception("Ya existe un médico con el DNI: " + medico.Dni);
            }

            if (datosMedicos.CorreoEnUso(medico.CorreoElectronico, medico.IdMedico))
            {
                throw new Exception("El correo electrónico " + medico.CorreoElectronico + " ya está en uso.");
            }

            if (datosMedicos.TelefonoEnUso(medico.Telefono, medico.IdMedico))
            {
                throw new Exception("El telefono " + medico.Telefono + " ya está en uso.");
            }
        }

        public int AgregarMedicoYUsuario(Usuario usuario, Medicos medico)
        {
            VerificarExistencias(medico);

            if (negocioUsuarios.UsuarioExiste(usuario.NombreUsuario))
            {
                throw new Exception("El usuario " + usuario.NombreUsuario + " ya está en uso.");
            }

            return datosMedicos.AgregarMedicoYUsuario(usuario, medico);
        }
    
        public void ActualizarMedico(Medicos medico)
        {
            VerificarExistencias(medico);

            datosMedicos.ActualizarMedico(medico);
        }

        public int BajaLogicaMedico(int idMedico)
        {
            return datosMedicos.BajaLogicaMedico(idMedico);
        }

        public DataTable ObtenerMedicosPorIdEspecialidad(int idEspecialidad)
        {
            return datosMedicos.ObtenerMedicosPorIdEspecialidad(idEspecialidad);
        }

        public DataTable ObtenerMedicoPorDNI(string dni)
        {
            
            if (string.IsNullOrEmpty(dni))
            {
               
                return new DataTable();
                
            }

            return datosMedicos.BuscarMedicoPorDNI(dni);
        }
    
        public Medicos ObtenerMedicoPorIdUsuario(string idUsuario)
        {
            DataTable dt = datosMedicos.ObtenerMedicoPorIdUsuario(idUsuario);
            Medicos medico = new Medicos();

            if (dt.Rows.Count <= 0)
            {
                medico.IdMedico = -1;
                return medico;
            }

            medico.IdMedico = Convert.ToInt32(dt.Rows[0]["idMedico"]);
            medico.Legajo = dt.Rows[0]["legajo"].ToString();
            medico.Dni = dt.Rows[0]["dni"].ToString();
            medico.Nombre = dt.Rows[0]["nombre"].ToString();
            medico.Apellido = dt.Rows[0]["apellido"].ToString();
            medico.Sexo = dt.Rows[0]["sexo"].ToString();
            medico.Nacionalidad = dt.Rows[0]["nacionalidad"].ToString();
            medico.CorreoElectronico = dt.Rows[0]["correoElectronico"].ToString();
            medico.Telefono = dt.Rows[0]["telefono"].ToString();
            medico.Direccion = dt.Rows[0]["direccion"].ToString();
            medico.IdProvincia = dt.Rows[0]["idProvincia"].ToString();
            medico.IdLocalidad = dt.Rows[0]["idLocalidad"].ToString();
            medico.IdEspecialidad = dt.Rows[0]["idEspecialidad"].ToString();
            medico.IdUsuario = dt.Rows[0]["idUsuario"].ToString();
            medico.Estado = Convert.ToBoolean(dt.Rows[0]["estado"]);

            if (dt.Rows[0]["fechaNacimiento"] != DBNull.Value)
            {
                medico.FechaNacimiento = Convert.ToDateTime(dt.Rows[0]["fechaNacimiento"]);
            }

            return medico;
        }
    }
}
