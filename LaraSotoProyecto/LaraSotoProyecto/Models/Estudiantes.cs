using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaraSotoProyecto.Models
{
    public class Estudiantes
    {
        public int EstudiantesID { get; set; }

        public int PeriodosID { get; set; }
        
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Matricula { get; set; }

        public int CalificacionFinal { get; set; }

        public virtual Periodos Periodos { get; set; }

        public virtual ICollection<Foros> Foros { get; set; }
    }
}