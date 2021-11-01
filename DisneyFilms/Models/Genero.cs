using System;
using System.Collections.Generic;

#nullable disable

namespace DisneyFilms.Models
{
    public partial class Genero
    {
        public Genero()
        {
            Films = new HashSet<Film>();
        }

        public string Nombre { get; set; }
        public byte[] Imagen { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}
