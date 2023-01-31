using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using MfcUfmt.Data;
using MfcUfmt.Models;
using MfcUfmt.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MfcUfmt.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MfcUfmt.Controllers
{
    [AllowAnonymous]
    public class FormsController : Controller
    {
        private readonly MfcUfmtContext _context;

        private readonly TrabalhoService _trabalhoService;

        private readonly CursoService _cursoService;

        private readonly NegocioCutter _negocioCutter;

        private readonly PdfBuilder _pdfBuilder;

        private HttpResponse httpResponse;

        public FormsController(MfcUfmtContext context, TrabalhoService trabalhoService, CursoService cursoService, NegocioCutter negocioCutter, PdfBuilder pdfBuilder)
        {
            _context = context;
            _trabalhoService = trabalhoService;
            _cursoService = cursoService;
            _negocioCutter = negocioCutter;
            _pdfBuilder = pdfBuilder;
        }


        // GET: Forms/Create

        public async Task<ActionResult> Create()
        {

            ViewBag.TamanhoFonte = new[]
            {
                new SelectListItem(){ Value = "8", Text = "8"},
                new SelectListItem(){ Value = "9", Text = "9", Selected = true},
                new SelectListItem(){ Value = "10", Text = "10"},
                new SelectListItem(){ Value = "11", Text = "11"},
            };
           
            var trabalho = await _trabalhoService.FindAllAsync();
            var cursos = await _cursoService.FindByTrabalhoId(3);

            var viewModel = new FormViewModel() { Trabalhos = trabalho, Cursos = cursos };
            return View(viewModel);
        }



        // POST: Forms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void GerarPdf(string sobreNome, string nome, string tituloTrabalho, 
            string subTitulo, string cutter,string programa, string nomeOrientador, 
            bool isOrientadora, string nomeCoOrientador, bool isCoOrientadora, string ano, 
            string numFolRom, string numFolArab, int ilustracao,bool bibliografia, 
            string alturaFolha,bool isTimes,float tamanhoFonte, 
            string txtPalavra, string txtPalavra2,string txtPalavra3, string txtPalavra4, string txtPalavra5)
        {
            
            // numFolRom,string numFolArab
            var paginas = numFolRom + " , " + numFolArab;
            var listPalavras = new List<string>(){txtPalavra, txtPalavra2, txtPalavra3, txtPalavra4, txtPalavra5};

            PdfBuilder ficha = new PdfBuilder(Response);

            ficha.gerarFichaCatalografica( sobreNome,  nome,  tituloTrabalho,  subTitulo,  cutter,  programa,  nomeOrientador,  isOrientadora,  nomeCoOrientador,
             isCoOrientadora,  ano,  paginas,  ilustracao,  bibliografia,  alturaFolha, listPalavras, isTimes, tamanhoFonte  );

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void GerarPdf2(string sobreNome, string nome, string tituloTrabalho,
            string subTitulo, string cutter,
            string programa, string nomeOrientador, bool isOrientadora, string nomeCoOrientador,
            bool isCoOrientadora, string ano, string paginas, int ilustracao, bool bibliografia,
            string alturaFolha, bool isTimes, float tamanhoFonte,
            string txtPalavra, string txtPalavra2, string txtPalavra3, string txtPalavra4, string txtPalavra5)
        {
            // 
            //var paginas = numFolRom + " , " + numFolArab;
            var listPalavras = new List<string>() { txtPalavra, txtPalavra2, txtPalavra3, txtPalavra4, txtPalavra5 };

            PdfBuilder ficha = new PdfBuilder(Response);

            ficha.gerarFichaCatalografica(sobreNome, nome, tituloTrabalho, subTitulo, cutter, programa, nomeOrientador, isOrientadora, nomeCoOrientador,
                isCoOrientadora, ano, paginas, ilustracao, bibliografia, alturaFolha, listPalavras, isTimes, tamanhoFonte);

        }

        public IActionResult Ajuda()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GerarCodCutter(string Nome, string sobreNome, string tituloTrabalho)
        {

            if (Nome != null && sobreNome != null && tituloTrabalho != null)
            {
                string codCutter;
                string sobrenome = RemoverAcentos(sobreNome.Trim().ToUpper());
                string nome = RemoverAcentos(Nome.Trim().ToUpper());

                String[] artigos =
                {
                    "o", "a", "ao", "os", "as", "um", "uns", "uma", "umas", "de", "do", "da", "dos", "das", "no", "na",
                    "nos", "nas", "ao", "aos", "à", "às", "pelo", "pela", "pelos", "pelas", "duma", "dumas", "dum",
                    "duns", "num", "numa", "nuns", "numas", "com", "por", "em"
                };

                String[] trabalho = removerCaracteresEspeciais(tituloTrabalho).Trim().Split(' ');

                Cutter cutter;
                try
                {
                    cutter = new NegocioCutter().GetCodigoCutter(sobrenome, nome);
                    if (artigos.Contains(trabalho[0].ToLower()))
                    {
                        try
                        {
                            codCutter = sobrenome[0] + cutter.CodigoCutter.ToString() +
                                             Util.NumeroParaExtenso(decimal.Parse(trabalho[1].Trim()))[0];
                            return Json(codCutter);
                        }
                        catch
                        {
                            try
                            {
                                int.Parse(trabalho[1].Trim().ToLower()[0].ToString());
                                codCutter = sobrenome[0] + cutter.CodigoCutter.ToString();
                                return Json(codCutter);
                            }
                            catch
                            {
                                codCutter = sobrenome[0] + cutter.CodigoCutter.ToString() +
                                                 trabalho[1].Trim().ToLower()[0];
                                return Json(codCutter);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            codCutter = sobrenome[0] + cutter.CodigoCutter.ToString() +
                                             Util.NumeroParaExtenso(decimal.Parse(trabalho[0].Trim()))[0];
                            return Json(codCutter);
                        }
                        catch
                        {
                            try
                            {
                                int.Parse(trabalho[0].Trim().ToLower()[0].ToString());
                                codCutter = sobrenome[0] + cutter.CodigoCutter.ToString();
                                return Json(codCutter);
                            }
                            catch
                            {
                                codCutter = sobrenome[0] + cutter.CodigoCutter.ToString() +
                                                 trabalho[0].Trim().ToLower()[0];
                                return Json(codCutter);
                            }
                        }
                    }
                }
                catch
                {
                    return Error("Não foi possível gerar o código Cutter através do nome e sobrenome informados. Eles devem ser compostos apenas por letras, sem números ou outros caracteres. Se o erro persistir, entre em contato com o administrador.");
                }

            }
            return Error("Não foi possível gerar o código Cutter através do nome e sobrenome informados.Eles devem ser compostos apenas por letras, sem números ou outros caracteres.Se o erro persistir, entre em contato com o administrador.");


        }

        [HttpPost]
        public async Task<JsonResult> AtualizaCursos(int trabId)
        {

            var cursos = await _cursoService.FindByTrabalhoId(trabId);
            return Json(cursos);
        }

        public async Task<IActionResult> UpdateCursos(int trabId)
        {
            //var cursos = await _cursoService.FindByTrabalhoId(2);
            var listCursos = await _cursoService.FindByTrabalhoId(trabId);
            ViewBag.Cursos = new SelectList(listCursos, "Descricao", "NomeCurso", null, dataGroupField: "Cidade");
            return Json(ViewBag.Cursos);
        }



        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier

            };
            return View(viewModel);
        }

        private String removerCaracteresEspeciais(String trabalho)
        {
            for (int i = trabalho.Length - 1; i >= 0; i--)
            {
                if (!Char.IsLetterOrDigit(trabalho[i]) && !trabalho[i].Equals(' '))
                {
                    trabalho = trabalho.Remove(i, 1);
                }
            }

            return trabalho;
        }

        private String RemoverAcentos(string texto)
        {
            string s = texto.Normalize(NormalizationForm.FormD);

            StringBuilder sb = new StringBuilder();

            for (int k = 0; k < s.Length; k++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(s[k]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(s[k]);
                }
            }
            return sb.ToString();
        }
    }
}