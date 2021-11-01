using DisneyFilms.Models;
using DisneyFilms.Models.Common;
using DisneyFilms.Models.Response;
using DisneyFilms.Request;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DisneyFilms.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public UserResponse Auth(AuthRequest model)
        {
            UserResponse userresponse = new UserResponse();
            using (var db=new PelisDisneyContext())
            {
                string spassword = model.Contraseña;
                
                var usuario = db.Usuarios.Where(d => d.Nombre == model.Nombre && d.Contraseña == spassword).FirstOrDefault();
                if (usuario == null) return null;
                userresponse.Nombre = usuario.Nombre;
                userresponse.Token = GetToken(usuario);
            }
            return userresponse;
        }

        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Nombre.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Email)
                    }
                    ),
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
