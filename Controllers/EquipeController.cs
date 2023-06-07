using Microsoft.AspNetCore.Mvc;
using projeto_gamer_mvc.Infra;
using projeto_gamer_mvc.Models;

namespace projeto_gamer_mvc.Controllers
{
    [Route("[controller]")]
    public class EquipeController : Controller
    {
        private readonly ILogger<EquipeController> _logger;

        public EquipeController(ILogger<EquipeController> logger)
        {
            _logger = logger;
        }

        Context c = new Context();

        [Route("Listar")]
        public IActionResult Index()
        {

            ViewBag.Login = HttpContext.Session.GetString("UserName");
            ViewBag.Equipe = c.Equipe.ToList();

            
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //instância do objeto Equipe
            Equipe novaEquipe = new Equipe();


            //atribuição dos valores recebidos pelo usuário
            novaEquipe.Nome = form["Nome"].ToString();
            

             if (form.Files.Count > 0)
            {
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);


                }

                //gerar o caminho completo ate o caminho do arquivo
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;
            }

            else{
                novaEquipe.Imagem = "padrao.png";

            }

            //adiciona o valor na tabela do banco de dados
            c.Equipe.Add(novaEquipe);

            //Salva as alterações na tabela do banco de dados
            c.SaveChanges();

            //Retorna para o local chamando a rota de listar (método Index)
            return LocalRedirect("~/Equipe/Listar");
        }


        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            Equipe equipeDelete = c.Equipe.First(x => x.IdEquipe == id);

            c.Remove(equipeDelete);

            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            ViewBag.Login = HttpContext.Session.GetString("UserName");
            Equipe equipeEditar = c.Equipe.First(x => x.IdEquipe == id);

            ViewBag.Equipe = equipeEditar;

            return View("Editar");
        }

        [Route("Atualizar")]
        public IActionResult Atualizar(IFormCollection form)
        {
            Equipe equipeAtualizada = new Equipe();

            equipeAtualizada.IdEquipe = int.Parse(form["IdEquipe"].ToString());

            equipeAtualizada.Nome = form["Nome"].ToString();


             if (form.Files.Count > 0)
            {
                var file = form.Files[0];

                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);


                }

                //gerar o caminho completo ate o caminho do arquivo
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                equipeAtualizada.Imagem = file.FileName;
            }

            else{
                equipeAtualizada.Imagem = "padrao.png";

            }

            Equipe buscada = c.Equipe.First(x => x.IdEquipe == equipeAtualizada.IdEquipe);

            buscada.Nome = equipeAtualizada.Nome;
            buscada.Imagem = equipeAtualizada.Imagem;

            c.Equipe.Update(buscada);

            c.SaveChanges();

            return LocalRedirect("~/Equipe/Listar");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


    }
}