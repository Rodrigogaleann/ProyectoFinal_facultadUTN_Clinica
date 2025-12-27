using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Paciente
    {
        public Paciente() { }

        private int _idPaciente;
        private string _nombre;
        private string _apellido;
        private string _dni;
        private string _sexo;
        private string _nacionalidad;
        private DateTime _fechaNacimiento;
        private string _direccion;
        private string _correoElectronico;
        private string _telefono;
        private string _idLocalidad;
        private string _idProvincia;
        private bool _estado;


        public int IdPaciente
        {
            get { return _idPaciente; }
            set
            {
                if (value < -1)
                {
                    throw new ArgumentException("ID Inválido.");
                }

                _idPaciente = value;
            }
        }
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nombre Inválido.");
                }

                if (value.Length > 40)
                {
                    throw new ArgumentException("El nombre puede contener hasta 40 caracteres.");
                }

                _nombre = value.Trim();
            }
        }
        public string Apellido
        {
            get { return _apellido; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Apellido Inválido.");
                }

                if (value.Length > 40)
                {
                    throw new ArgumentException("El apellido puede contener hasta 40 caracteres.");
                }

                _apellido = value.Trim();
            }
        }
        public string Dni
        {
            get { return _dni; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("DNI Inválido.");
                }

                if (!int.TryParse(value, out int dni))
                {
                    throw new ArgumentException("El DNI solo puede contener números.");
                }

                if (value.Length > 8 || value.Length < 7)
                {
                    throw new ArgumentException("El DNI admite entre 7 y 8 dígitos.");
                }

                _dni = value.Trim();
            }
        }
        public string Sexo
        {
            get { return _sexo; }
            set
            {
                if (value != "Masculino" && value != "Femenino" && value != "Otro")
                {
                    throw new ArgumentException("Sexo Inválido.");
                }

                _sexo = value;
            }
        }
        public string Nacionalidad
        {
            get { return _nacionalidad; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Nacionalidad Inválida.");
                }

                if (value.Length > 30)
                {
                    throw new ArgumentException("La nacionalidad puede contener hasta 30 caracteres.");
                }

                _nacionalidad = value.Trim();
            }
        }
        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("La fecha de nacimiento no puede ser futura.");
                }

                _fechaNacimiento = value;
            }
        }
        public string Direccion
        {
            get { return _direccion; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("La dirección es obligatoria.");
                }

                if (value.Length > 40)
                {
                    throw new ArgumentException("La dirección admite hasta 40 caracteres.");
                }

                _direccion = value.Trim();
            }
        }
        public string CorreoElectronico
        {
            get { return _correoElectronico; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El correo es obligatorio.");
                }

                if (!value.Contains("@"))
                {
                    throw new ArgumentException("Correo electrónico inválido.");
                }

                _correoElectronico = value.Trim();
            }
        }
        public string Telefono
        {
            get { return _telefono; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Teléfono Inválido.");
                }

                if (value.Length < 10 || value.Length > 15)
                {
                    throw new ArgumentException("El teléfono debe contener entre 10 y 15 dígitos.");
                }

                _telefono = value.Trim();
            }
        }
        public string IdLocalidad
        {
            get { return _idLocalidad; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("La localidad es inválida.");
                }

                if (value.Length != 3)
                {
                    throw new ArgumentException("El ID de la localidad es inválido.");
                }

                _idLocalidad = value.Trim();
            }
        }
        public string IdProvincia
        {
            get { return _idProvincia; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("La provincia es inválida.");
                }

                if (value.Length != 2)
                {
                    throw new ArgumentException("El ID de la provincia es inválido.");
                }

                _idProvincia = value.Trim();
            }
        }
        public bool Estado
        {
            get { return _estado; }
            set { _estado = value; }
        } 
    }
}

