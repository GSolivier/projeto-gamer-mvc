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
    public class JogadorController : Controller
    {
        private readonly ILogger<JogadorController> _logger;

        public JogadorController(ILogger<JogadorController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();

        [Route("Listar")]
        public IActionResult Index()
        {
            ViewBag.Login = HttpContext.Session.GetString("UserName");
            ViewBag.Jogador = c.Jogador.ToList();
            ViewBag.Equipe = c.Equipe.ToList();
            return View();
        }

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

            return LocalRedirect("~/Jogador/Listar");
        }

        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            Jogador jogadorExcluir = c.Jogador.First(x => x.IdJogador == id);

            c.Remove(jogadorExcluir);

            c.SaveChanges();

            return LocalRedirect("~/Jogador/Listar");
        }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            ViewBag.Login = HttpContext.Session.GetString("UserName");
            Jogador jogadorEditar = c.Jogador.First(x => x.IdJogador == id);

            ViewBag.Jogador = jogadorEditar;

            ViewBag.Equipe = c.Equipe.ToList();

            return View("Editar");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Jogador jogadorAtualizado = new Jogador();

            jogadorAtualizado.IdJogador = int.Parse(form["IdJogador"].ToString());

            jogadorAtualizado.Nome = form["Nome"].ToString();

            jogadorAtualizado.Email = form["Email"].ToString();

            jogadorAtualizado.Senha = form["Senha"].ToString();

            jogadorAtualizado.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            
            Jogador jogadorBuscado = c.Jogador.First(x => x.IdJogador == jogadorAtualizado.IdJogador);

            jogadorBuscado.Nome = jogadorAtualizado.Nome;
            jogadorBuscado.Email = jogadorAtualizado.Email;
            jogadorBuscado.Senha = jogadorAtualizado.Senha;
            jogadorBuscado.IdEquipe = jogadorAtualizado.IdEquipe;

            c.Jogador.Update(jogadorBuscado);

            c.SaveChanges();


            return LocalRedirect("~/Jogador/Listar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}