using MfcUfmt.Models;
using System;
using System.Collections.Generic;


namespace MfcUfmt.Models.ViewModels
{
    public class CursoViewModel 
    {
        public Curso Curso { get; set; }
        public ICollection<Trabalho> Trabalhos { get; set; }

    }
}