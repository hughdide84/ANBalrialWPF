using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBalrial.DTOs
{
    public class UsuarioDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string login { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int? cp { get; set; }
        public int? telegramId { get; set; }
        public string dias { get; set; }
        public string horaInicio { get; set; }
        public string horaFin { get; set; }
        public int? disponibilidad { get; set; }
    }
}
