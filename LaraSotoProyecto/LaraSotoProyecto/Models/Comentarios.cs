using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaraSotoProyecto.Models
{
    public class Comentarios
    {
        public int ComentariosID { get; set; }

        public int ForosID { get; set; }

        public string Tema { get; set; }

        public string Contenido { get; set; }

        public DateTime FechaModificacion { get; set; }

        public virtual Foros Foros { get; set; }
    }
}