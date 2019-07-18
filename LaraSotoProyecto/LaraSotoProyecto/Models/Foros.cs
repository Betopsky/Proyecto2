using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaraSotoProyecto.Models
{
    public class Foros
    {
        public int ForosID { get; set; }

        public int EstudiantesID { get; set; }

        public int ProfesoresID { get; set; }

        public string Asunto { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public virtual Estudiantes Estudiantes { get; set; }

        public virtual Profesores Profesores { get; set; }

        public virtual ICollection<Archivos> Archivos { get; set; }

        public virtual ICollection<Comentarios> Comentarios { get; set; }
    }
}