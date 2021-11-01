using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyFilms.Request
{
    public class AuthRequest
    {
        [Required]
        public string Nombre { set; get; }
        [Required]
        public string Contraseña { set; get; }
    }
}
