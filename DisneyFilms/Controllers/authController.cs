using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DisneyFilms.Models;
using DisneyFilms.Request;
using DisneyFilms.Services;
using DisneyFilms.Models.Response;
using System.Net.Mail;

namespace DisneyFilms.Controllers
{
    public class authController : Controller
    {
        private readonly PelisDisneyContext _context;
        private readonly IUserService _userService;
        public authController(PelisDisneyContext context, IUserService userService)
        {
            _userService = userService;
            _context = context;
        }


        // GET: auth/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Nombre == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        
        // GET: auth/registrer
        public IActionResult register()
        {
            return View();
        }

        // POST: auth/register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> register([Bind("Nombre,Contraseña,Email")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                //Envio de Mail
                string EmailOrigen = "PelisDisney1996@gmail.com";
                string EmailDestino = usuario.Email;
                string Contraseña = "PelisDisney";

                MailMessage MailMensaje = new MailMessage(EmailOrigen, EmailDestino, "Bienvenido a PelisDisney", "<p>Te has registrado con éxito</p>");
                MailMensaje.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);
                smtpClient.Send(MailMensaje);
                //fin de envio de mail--


                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(login));
            }
            return View(usuario);
        }
        

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }



        /*   Si se quiere capturar datos desde el MVC debe cambiar el siguiente public por: 
             "public IActionResult login([Bind("Nombre,Contraseña)] AuthRequest model)" */        
        [HttpPost]
        public IActionResult login([FromBody] AuthRequest model)
        {
            
            Respuesta respuesta = new Respuesta();
            var userresponse = _userService.Auth(model);
            if (userresponse == null)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o Contraseña incorrecto";
                return BadRequest(respuesta);
            }
            respuesta.Exito = 1;
            respuesta.Data = userresponse;
            return Ok(respuesta);
        }
        
    
    }
}