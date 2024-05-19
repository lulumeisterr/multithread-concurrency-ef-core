using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcorrenciaEF.Application.Data
{
    [Table("Negocio")]
    public class Negocio
    {
        [Key]
        [Column("NUMERO_NEGOCIO")]
        public int NumeroNegocio { get; set; }
        [Column("NATUREZA_OPERACAO")]
        public string NaturezaOperacao { get; set; }
        [Column("STATUS_NEGOCIACAO")]
        public string StatusNegociacao { get; set; }
        [Column("DTHR_MENSAGEM")]
        public DateTime DataHoraMensagem { get; set; }
    }
}