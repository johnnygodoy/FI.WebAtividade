using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DML
{
    public class Beneficiario
    {
        public long Id { get; set; }

        [Required]
        [StringLength(11)] // Defina o tamanho máximo do CPF
        public string CPF { get; set; }

        [Required]
        [StringLength(100)] // Defina o tamanho máximo do nome
        public string Nome { get; set; }

        // Chave estrangeira para Cliente
        public long IdCliente { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
