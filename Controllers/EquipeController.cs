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
                novaEquipe.Imagem = "padrao";

            }

            //adiciona o valor na tabela do banco de dados
            c.Equipe.Add(novaEquipe);

            //Salva as alterações na tabela do banco de dados
            c.SaveChanges();

            //Retorna para o local chamando a rota de listar (método Index)
            return LocalRedirect("~/Equipe/Listar");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }


    }
}