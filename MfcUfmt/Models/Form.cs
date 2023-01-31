using MfcUfmt.Models.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MfcUfmt.Models
{
    public class Form
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Nome: ")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Sobrenome: ")]
        public string SobreNome { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Título do trabalho:")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Sub-título do trabalho:")]
        public string SubTitulo { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Código Cutter:")]
        public Cutter CodCutter { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        public Trabalho Trabalho { get; set; }
        public ICollection<Curso> Curso { get; set; } = new List<Curso>(); //Pesquisar como fazer a pesquisa

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Nome completo do orientador:")]
        public string NomeOrientador { get; set; }
        public bool Orientadora { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Nome completo do coorientador:")]
        public string NomeCoorientador { get; set; }
        public bool Coorientadora { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Ano:")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Somente números são permitidos")]
        [Range(1, 9999)]
        public string Ano { get; set; }

        [Display(Name = "Número em romano:")]
        [Required(ErrorMessage = "{0} necessário")]
        [RegularExpression("^(?=[MDCLXVI])M*(C[MD]|D?C{0,3})(X[CL]|L?X{0,3})(I[XV]|V?I{0,3})$", ErrorMessage = "Somente números romanos são permitidos e/ou a ordem dos elementos está errada")]
        public string NumFolhaRomano { get; set; }

        [Display(Name = "Número em arábico:")]
        [Required(ErrorMessage = "{0} necessário")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Somente números são permitidos")]
        public string NumFolhaArabico { get; set; }

        [Display(Name = "Ilustrações:")]
        public Ilustracao TipoIlustracao { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Bibliografia:")]
        public Bibliografia Bibliografia { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Altura da folha:")]
        public string AlturaFolha { get; set; } = "30";

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Palavras Chaves:")]
        public List<string> PalavrasChave { get; set; } //Erro por ser list

        [Required(ErrorMessage = "{0} necessário")]
        public Fonte TipoFonte { get; set; }

        [Required(ErrorMessage = "{0} necessário")]
        [Display(Name = "Tamanho da Fonte:")]
        [Range(8,11, ErrorMessage = "{0} deve ser entre {1} e {2}")]
        public int TamFonte { get; set; }

        public Form()
        {

        }

        public Form(int id, string nome, string sobreNome, string titulo, string subTitulo, Cutter codCutter, Trabalho trabalho,
            string nomeOrientador, bool orientadora, string nomeCoorientador, bool coorientadora, string ano
            , string numFolhaRomano, string numFolhaArabico, Ilustracao tipoIlustracao,
            Bibliografia bibliografia, string alturaFolha, string palavrasChave, Fonte tipoFonte, int tamFonte)
        {
            Id = id;
            Nome = nome;
            SobreNome = sobreNome;
            Titulo = titulo;
            SubTitulo = subTitulo;
            CodCutter = codCutter;
            Trabalho = trabalho;
            NomeOrientador = nomeOrientador;
            Orientadora = orientadora;
            NomeCoorientador = nomeCoorientador;
            Coorientadora = coorientadora;
            Ano = ano;
            NumFolhaRomano = numFolhaRomano;
            NumFolhaArabico = numFolhaArabico;
            TipoIlustracao = tipoIlustracao;
            Bibliografia = bibliografia;
            AlturaFolha = alturaFolha;
            TipoFonte = tipoFonte;
            TamFonte = tamFonte;
        }
    }
}
