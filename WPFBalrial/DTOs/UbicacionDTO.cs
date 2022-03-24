using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBalrial.DTOs
{
    public class UbicacionDTO
    {
        public int id { get; set; }
        public int idEntidad { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public int? cp { get; set; }
        public string poblacion { get; set; }
        public string zona { get; set; }
        public double? longitud { get; set; }
        public double? latitud { get; set; }
        public int? volumen { get; set; }
    }
}
