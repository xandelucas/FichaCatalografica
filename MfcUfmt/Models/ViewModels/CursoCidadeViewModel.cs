using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MfcUfmt.Models.ViewModels
{
    public class CursoCidadeViewModel
    {
        public string CursoSelecionado { get; set; }
        public IEnumerable<CursoCidade> Cursos { get; set; }


        public string ToFriendlyString(Trabalho trabalho)
        {
            return trabalho.NomeTrabalho;
        }
    }
}
