using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MfcUfmt.Models.Enuns

{
    public enum PadraoNumFol:int
    {
        [Display(Name = "Apenas Números Arábicos")]
        ApenasNumerosArabicos,
        [Display(Name = "Parte Romanos e parte Arábicos")]
        RomanoseArabicos

    }
}