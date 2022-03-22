using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBalrial.DTOs
{
    public class PlanificacionDTO
    {
        public int id { get; set; }
        public int idProyUbicacion { get; set; }
        public string fecha { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
        public int recursos { get; set; }
        public int asignados { get; set; }
        public string nomUbicacion { get; set; }
        public string nomCoordinador { get; set; }
    }
}
