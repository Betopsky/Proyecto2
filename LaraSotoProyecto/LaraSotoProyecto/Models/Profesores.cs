using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaraSotoProyecto.Models
{
    public class Profesores
    {
        public int ProfesoresID { get; set; }

        public string NombreProfesor { get; set; }

        public string Especializacion { get; set; }

        public virtual ICollection<Foros> Foros { get; set; }
    }
}