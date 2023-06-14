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
    public class CadastroUserController : Controller
    {
        private readonly ILogger<CadastroUserController> _logger;

        public CadastroUserController(ILogger<CadastroUserController> logger)
        {
            _logger = logger;
        }

        [Route("CadastroUser")]
        public IActionResult Index()
        {
            ViewBag.Equipe = c.Equipe.ToList();
            return View();
        }

        Context c = new Context();

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Jogador novoJogador = new Jogador();

            novoJogador.Nome = form["Nome"].ToString();

            novoJogador.Email = form["Email"].ToString();

            novoJogador.Senha = form["Senha"].ToString();

            novoJogador.IdEquipe = int.Parse(form["IdEquipe"]);

            c.Jogador.Add(novoJogador);

            c.SaveChanges();

            return LocalRedirect("~/Login/Login");
        }

        // [Route("CadastroUser")]
        // public IActionResult CadastroUsers()
        // {
        //     ViewBag.Equipe = c.Equipe.ToList();
        //     return View("CadastroUser");
        // }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}