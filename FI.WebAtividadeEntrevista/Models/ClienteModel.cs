using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    public class ClienteModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve estar no formato 00000-000")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo E-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório")]
        [MaxLength(2, ErrorMessage = "O Estado deve conter 2 caracteres")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O campo Logradouro é obrigatório")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo Nacionalidade é obrigatório")]
        public string Nacionalidade { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Sobrenome é obrigatório")]
        public string Sobrenome { get; set; }

        [Phone(ErrorMessage = "Digite um número de telefone válido")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 000.000.000-00")]

        public string CPF { get; set; }
    }
}
