using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using projeto_gamer_mvc.Infra;

namespace projeto_gamer_mvc.Models
{
    public class Equipe
    {
        [Key]
        public int IdEquipe { get; set; }

        public string Nome { get; set; }
        public string Imagem { get; set; }

        //referencia que a classe vai ter acesso a collection jogador
        public ICollection<Jogador> Jogador {get;set;}

    }
}