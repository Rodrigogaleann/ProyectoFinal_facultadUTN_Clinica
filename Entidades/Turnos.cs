using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Turno
    {
        public int numTurno { get; set; }
        public int idMedico { get; set; }
        public string idEspecialidad { get; set; }
        public DateTime dia { get; set; }
        public TimeSpan horario { get; set; }
        public int idPaciente { get; set; }
        public bool asistencia { get; set; }
        public string observaciones { get; set; }

        //constructor
        public Turno() { }


    }
}

