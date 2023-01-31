using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MfcUfmt.Models.Enuns
{
    public enum Ilustracao
    {
        [Display(Name = "Não possui")]
        Nenhuma,
        [Display(Name = "Preto e Branco")]
        PretoBranco,
        [Display(Name = "Coloridas")]
        Colorida,

    }
}
