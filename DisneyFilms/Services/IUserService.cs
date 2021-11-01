using DisneyFilms.Models;
using DisneyFilms.Models.Response;
using DisneyFilms.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisneyFilms.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
