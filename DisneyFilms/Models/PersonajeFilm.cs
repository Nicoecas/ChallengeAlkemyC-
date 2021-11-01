using System;
using System.Collections.Generic;

#nullable disable

namespace DisneyFilms.Models
{
    public partial class PersonajeFilm
    {
        public int Id { get; set; }
        public string Npersonaje { get; set; }
        public string Nfilm { get; set; }

        public virtual Film NfilmNavigation { get; set; }
        public virtual Personaje NpersonajeNavigation { get; set; }
    }
}
