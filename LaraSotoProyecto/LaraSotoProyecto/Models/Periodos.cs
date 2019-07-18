using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaraSotoProyecto.Models
{
    public class Periodos
    {
        public int PeriodosID { get; set; }

        public DateTime Anio { get; set; }

        public string Semestre { get; set; }

        public virtual ICollection<Estudiantes> Estudiantes { get; set; }
    }
}