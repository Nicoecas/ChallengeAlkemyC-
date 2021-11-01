using System;
using System.Collections.Generic;

#nullable disable

namespace DisneyFilms.Models
{
    public partial class Film
    {
        public Film()
        {
            PersonajeFilms = new HashSet<PersonajeFilm>();
        }

        public string Titulo { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Clasificacion { get; set; }
        public byte[] Imagen { get; set; }
        public string GeneroN { get; set; }

        public virtual Genero GeneroNNavigation { get; set; }
        public virtual ICollection<PersonajeFilm> PersonajeFilms { get; set; }
    }
}
