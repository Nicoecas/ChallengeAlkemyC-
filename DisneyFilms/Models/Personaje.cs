using System;
using System.Collections.Generic;

#nullable disable

namespace DisneyFilms.Models
{
    public partial class Personaje
    {
        public Personaje()
        {
            PersonajeFilms = new HashSet<PersonajeFilm>();
        }

        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public double? Peso { get; set; }
        public string Historia { get; set; }
        public byte[] Imagen { get; set; }

        public virtual ICollection<PersonajeFilm> PersonajeFilms { get; set; }
    }
}
