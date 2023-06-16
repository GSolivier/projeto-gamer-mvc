using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projeto_gamer_mvc.Infra;
using projeto_gamer_mvc.Models;

namespace projeto_gamer_mvc.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        [TempData]
        public string message {get;set;}
        Context c = new Context();

        [Route("Login")]
        public IActionResult Index()
        {
            ViewBag.Login = HttpContext.Session.GetString("UserName");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }

        [Route("Logar")]
        public IActionResult Logar(IFormCollection form)
        {
           string email = form["Email"].ToString();
           string senha = form["Senha"].ToString();

           Jogador jogadorBuscado = c.Jogador.FirstOrDefault(x => x.Email == email && x.Senha == senha);

           if (jogadorBuscado != null)
           {
                HttpContext.Session.SetString("UserName", jogadorBuscado.Nome);
                HttpContext.Session.SetString("Email", jogadorBuscado.Email);

                return LocalRedirect("~/");
           }

            message = "Email e/ou senha incorretos";

            return LocalRedirect("~/Login/Login");
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");

            return LocalRedirect("~/");
        }

        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}