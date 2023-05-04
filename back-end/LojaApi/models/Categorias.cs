using System;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace Cat.Models
 {

    public class Categoria {

        [Key]

        public int Id {get; set;}

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome {get; set;}

        public string Descricao {get; set;}

        public int Quantidade {get; set;}

        public int Preco {get; set;}

        public DateTime DataCadastro {get; set;}
    }

}