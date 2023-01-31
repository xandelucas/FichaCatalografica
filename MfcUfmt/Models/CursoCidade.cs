using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MfcUfmt.Models
{
    public class CursoCidade : Curso
    {
        public String Cidade { get; set; }

        public CursoCidade()
        {
        }

        public CursoCidade(int id, string nomeCurso, string descricao, Trabalho trabalho, int trabalhoId, string cidade) : base(id,
            nomeCurso, descricao, trabalho)
        {
            Cidade = cidade;
            TrabalhoId = trabalhoId;
        }
    }

}
