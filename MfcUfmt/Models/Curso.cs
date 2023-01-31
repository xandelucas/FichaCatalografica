using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MfcUfmt.Models
{
    public class Curso
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Nome do Curso:")]
        public string NomeCurso { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Descrição:")]
        public string Descricao { get; set; }

        [Display(Name = "Trabalho:")]
        public Trabalho Trabalho { get; set; }

        public int TrabalhoId { get; set; }

        public Curso()
        {

        }

        public Curso(int id, string nomeCurso, string descricao, Trabalho trabalho)
        {
            Id = id;
            NomeCurso = nomeCurso;
            Descricao = descricao;
            Trabalho = trabalho;

        }
       

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
