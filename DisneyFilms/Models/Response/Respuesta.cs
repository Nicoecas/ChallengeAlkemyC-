using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyFilms.Models.Response
{
    public class Respuesta
    { 
        public int Exito { set; get; }
        public string Mensaje { set; get; }
        public object Data { set; get; }
    }
}
