using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaraSotoProyecto.Models
{
    public class Archivos
    {
        public int ArchivosID { get; set; }

        public int ForosID { get; set; }

        public string Titulo { get; set; }

        public DateTime FechaSubida { get; set; }

        public virtual Foros Foros { get; set; }
    }
}