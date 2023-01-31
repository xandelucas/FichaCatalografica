using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MfcUfmt.Models.Enuns;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MfcUfmt.Models.ViewModels
{
    public class FormViewModel
    {
        [BindProperty(SupportsGet = true)]
        public Form Form { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CursoSelecionado { get; set; }
        public IEnumerable<CursoCidade> Cursos { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CodCutter { get; set; }



        [BindProperty(SupportsGet = true)]
        [Display(Name = "Trabalhos: ")]
        public ICollection<Trabalho> Trabalhos { get; set; }

        [BindProperty(SupportsGet = true)]
        [Display(Name = "Padrão do Número de folhas: ")]
        public PadraoNumFol PadraoNumFols { get; set; }

        [BindProperty(SupportsGet = true)]
        [Display(Name = "Ilustrações:")]
        public Ilustracao Ilustracaos { get; set; }

        [BindProperty(SupportsGet = true)]
        [Display(Name = "Fonte:")]
        public Fonte Fontes { get; set; }

        [BindProperty(SupportsGet = true)]
        [Display(Name = "Bibliografias:")]
        public Bibliografia Bibliografias { get; set; }

        [BindProperty(SupportsGet = true)]
        [Display(Name = "Cidades:")]
        public Cidade Cidades { get; set; }



        public string ToFriendlyString(Trabalho trabalho)
        {
            return trabalho.NomeTrabalho;
        }
    }

}
