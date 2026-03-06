
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AplicacaoBanco.Models
{
    public class Usuario
    {
        [Display(Name="Código")]
        public int? IdUsu { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string? nomeUsu { get; set; }

        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "O campo Cargo é obrigatório")]
        public string? Cargo { get; set; }

        [Display(Name = "Nascimento")]
        [Required(ErrorMessage = "O campo nascimento é obrigatório")]
        [DataType(DataType.DataTime)]
        public DataSetDateTime DataNasc { get; set; }
    }
    
}
