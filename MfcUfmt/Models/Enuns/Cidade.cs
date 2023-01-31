using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MfcUfmt.Models.Enuns
{
    public enum Cidade
    {
        [Display(Name = "Cuiabá")]
        Cuiaba,

        [Display(Name = "Rondonópolis")]
        Rondonopolis,

        [Display(Name = "Sinop")]
        Sinop,

        [Display(Name = "Pontal do Araguaia")]
        PontalDoAraguaia,

        [Display(Name = "Barra do Garças")]
        BarraDoGarcas


    }

}
