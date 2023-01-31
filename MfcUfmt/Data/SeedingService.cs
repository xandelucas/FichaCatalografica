using System.Linq;
using MfcUfmt.Models;

namespace MfcUfmt.Data
{
    public class SeedingService
    {
        private MfcUfmtContext _context;

        public SeedingService(MfcUfmtContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Curso.Any() || _context.Trabalho.Any())
            {
                return; //BD populado
            }
            Trabalho t1 = new Trabalho(1, "Tese");
            Trabalho t2 = new Trabalho(2, "Dissertação");
            Trabalho t3 = new Trabalho(3, "TCC (Especialização)");
            Trabalho t4 = new Trabalho(4, "TCC (Graduação)");

            Curso c1 = new Curso(1, "Administração", "TCC (graduação em Administração) - Universidade Federal de Mato Grosso, Faculdade de Administração e Ciências Contábeis, Cuiabá,	", t4);
            Curso c2 = new Curso(2, "Ciência da Computação", "TCC (graduação em Ciência da Computação) - Universidade Federal de Mato Grosso, Instituto de Computação, Cuiabá,", t4);
            Curso c3 = new Curso(3, "Ciências da Saúde", "Tese (doutorado) - Universidade Federal de Mato Grosso, Faculdade de Ciências Médicas, Programa de Pós-Graduação em Ciências da Saúde, Cuiabá,", t1);
            Curso c4 = new Curso(4, "Biociências - Nutrição", "Dissertação (mestrado) - Universidade Federal de Mato Grosso, Faculdade de Nutrição, Programa de Pós-Graduação em Biociências, Cuiabá,", t2);
            Curso c5 = new Curso(5, "Direito Penal e Processual Penal", "TCC (especialização em Direito Penal e Processual Penal) - Universidade Federal de Mato Grosso, Faculdade de Direito, Cuiabá,", t3);
            Curso c6 = new Curso(6, "Física", "Tese (doutorado) - Universidade Federal de Mato Grosso, Instituto de Física, Programa de Pós-Graduação em Física, Cuiabá,", t1);
            Curso c7 = new Curso(7, "Educação (CUR)", "Dissertação (mestrado) - Universidade Federal de Mato Grosso, Instituto de Ciências Humanas e Sociais, Programa de Pós-Graduação em Educação, Rondonópolis,", t2);
            Curso c8 = new Curso(8, "Língua Portuguesa", "TCC (especialização em Língua Portuguesa) - Universidade Federal de Mato Grosso, Instituto de Ciências Humanas e Sociais, Rondonópolis,", t3);

            _context.Trabalho.AddRange(t1, t2, t3, t4);

            _context.Curso.AddRange(c1, c2, c3, c4, c5, c6,c7, c8);



            _context.SaveChanges();

        }

    }
}