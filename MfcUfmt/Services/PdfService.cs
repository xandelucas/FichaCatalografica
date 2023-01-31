using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;


namespace MfcUfmt.Services
{
    public class PdfBuilder
    {
        private static float MARGEM_SUPERIOR_PAPEL = 368.498f;
        private static float MARGEM_INFERIOR_PAPEL = 115.73f;
        private static float MARGEM_ESQUERDA_PAPEL = 82.04f;
        private static float MARGEM_DIREITA_PAPEL = 82.04f;
        private static float LARGURA_CAIXA = 369.0885f; //12,5cm convertido para pontos (mais 14,7635 de ajustes), 1cm = 28,346 pontos
        private static float ALTURA_CAIXA = 221.452f; //7,5cm convertido para pontos (mais 11,81 de ajustes)
        //private static float MARGEM_SUPERIOR_CAIXA = 3.68498f; //0,13cm
        //private static float MARGEM_INFERIOR_CAIXA = 3.68498f; //0,13cm
        //private static float MARGEM_ESQUERDA_CAIXA = 7.0865f; //0,25cm
        //private static float MARGEM_DIREITA_CAIXA = 7.0865f; //0,25cm
        private static float ESPESSURA_CAIXA = 0.75f;
        private static float LEADING_TEXTO = 1.1f;
        //private static float IDENTACAO_PRIMEIRA_LINHA = 12f;
        //private static float IDENTACAO = 31.8f;

        private HttpResponse httpResponse;

        MemoryStream stream = new MemoryStream();
        Document document = new Document(PageSize.A4, MARGEM_ESQUERDA_PAPEL, MARGEM_DIREITA_PAPEL, MARGEM_SUPERIOR_PAPEL, MARGEM_INFERIOR_PAPEL);

        public PdfBuilder(HttpResponse response)
        {
            this.httpResponse = response;
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            FontFactory.RegisterDirectory(($"{AppContext.BaseDirectory}/ Fontes"));

            document.Open();
        }

        public PdfBuilder()
        {
        }

        public void gerarFichaCatalografica(String sobrenome, String nome, String tituloTrabalho, String subTituloTrabalho,
                                            String cutter, String programa, String nomeOrientador,
                                            bool isOrientadora, String nomeCoorientador,
                                            bool isCoorientadora, String ano, String paginas, int ilustracaoUID, bool incluiBibliografia,
                                            String tamanhoFolha, IList<String> palavras, bool isTimes, float tamanhoLetra)
        {
            tamanhoLetra += tamanhoLetra * 0.049f;

            PdfPTable box = new PdfPTable(1);
            box.TotalWidth = LARGURA_CAIXA;
            box.LockedWidth = true;

            AddHeader(isTimes);

            PdfPCell cell = new PdfPCell();
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.FixedHeight = ALTURA_CAIXA;
            cell.BorderWidth = ESPESSURA_CAIXA;

            //cell.Left = MARGEM_ESQUERDA_CAIXA;
            //cell.Bottom = MARGEM_INFERIOR_CAIXA;
            //cell.Top = MARGEM_SUPERIOR_CAIXA;
            //cell.Right = MARGEM_DIREITA_CAIXA;

            String cutterNome = "\n" + cutter + "    " + sobrenome + ", " + nome + ".";

            Paragraph cutterParagraph = new Paragraph(cutterNome, getFonte(isTimes, false, tamanhoLetra, false));
            cutterParagraph.Alignment = Element.ALIGN_LEFT;
            cutterParagraph.IndentationLeft = tamanhoLetra * 0.57f;
            cutterParagraph.SetLeading(0f, LEADING_TEXTO);

            float distanciaSobrenome = System.Drawing.Graphics.FromImage(new System.Drawing.Bitmap(1, 1)).MeasureString(sobrenome.Substring(0, 3), new System.Drawing.Font((isTimes ? "Times New Roman" : "Arial"), tamanhoLetra)).Width;

            cell.AddElement(cutterParagraph);

            String ficha = tituloTrabalho + (String.IsNullOrEmpty(subTituloTrabalho) ? "" : " : " + subTituloTrabalho) + " / " + nome + " " + sobrenome + ". -- " + ano + "\n" +
                           paginas + " f. " + (ilustracaoUID > 0 ? (ilustracaoUID > 1 ? ": il. color. ; " : ": il. ; ") : "; ") + tamanhoFolha + " cm.\n\n" +
                           "Orientador" + (isOrientadora ? "a: " : ": ") + nomeOrientador + ".\n" + //" " + sobrenomeOrientador + ".\n" +
                           (String.IsNullOrEmpty(nomeCoorientador) ? "" :
                           "Co-orientador" + (isCoorientadora ? "a: " : ": ") + nomeCoorientador + ".\n") +//" " + sobrenomeCoorientador + ".\n") +
                           programa + " " + ano + ".\n" +
                           (incluiBibliografia ?
                           "Inclui bibliografia.\n\n\n" : "\n\n");
            //String ficha = "    " + tituloTrabalho + (String.IsNullOrEmpty(subTituloTrabalho) ? "" : " : " +subTituloTrabalho) + " / " + nome + " " + sobrenome + ". -- " + ano + "\n" +
            //               "    " + paginas + " f. " + (ilustracaoUID > 0 ? (ilustracaoUID > 1 ? ": il. color. ; " : ": il. ; ") : "; ") + tamanhoFolha + " cm.\n\n" +
            //               "    " + "Orientador" + (isOrientadora ? "a: " : ": ") + nomeOrientador + ".\n" + //" " + sobrenomeOrientador + ".\n" +
            //               (String.IsNullOrEmpty(nomeCoorientador) ? "" :
            //               "    " + "Co-orientador" + (isCoorientadora ? "a: " : ": ") + nomeCoorientador + ".\n") +//" " + sobrenomeCoorientador + ".\n") +
            //               "    " + programa + " " + ano + ".\n" +
            //               (incluiBibliografia ? "    " + "Inclui bibliografia.\n\n\n" : "\n\n") + 
            //               "    ";
            int contador = 1;
            foreach (String palavra in palavras)
            {
                if (!String.IsNullOrEmpty(palavra))
                {
                    ficha += contador.ToString() + ". " + palavra + ". ";
                    contador++;
                }
            }
            ficha += "I. Título.";

            Paragraph fichaParagraph = new Paragraph(ficha, getFonte(isTimes, false, tamanhoLetra, false));
            //fichaParagraph.FirstLineIndent = (tamanhoLetra * 9 * (isTimes ? 0.4f : 0.448f)) + (distanciaSobrenome * 0.572f) + cutterParagraph.IndentationLeft;
            fichaParagraph.IndentationLeft = (tamanhoLetra * 9 * (isTimes ? 0.4f : 0.448f)) + cutterParagraph.IndentationLeft;//(distanciaSobrenome * 0.572f) 
            fichaParagraph.FirstLineIndent = distanciaSobrenome * 0.572f;
            fichaParagraph.Alignment = Element.ALIGN_LEFT;
            fichaParagraph.SetLeading(0f, LEADING_TEXTO);

            cell.AddElement(fichaParagraph);

            box.AddCell(cell);

            document.Add(box);

            AddFooter(isTimes);

            Write();

            //float a = (cutterNome.Length + ficha.Length) * tamanhoLetra;

            //return (cutterNome.Length + ficha.Length) * tamanhoLetra > 0; 
        }

        private void AddHeader(bool isTimes)
        {
            Paragraph headerParagraph = new Paragraph("Dados Internacionais de Catalogação na Fonte.\n\n", getFonte(isTimes, false, 12.8f, true));
            headerParagraph.Alignment = Element.ALIGN_CENTER;
            headerParagraph.SetLeading(0f, LEADING_TEXTO);

            document.Add(headerParagraph);
        }

        private void AddFooter(bool isTimes)
        {
            Paragraph footerParagraph = new Paragraph("\nFicha catalográfica elaborada automaticamente de acordo com os dados fornecidos pelo(a) autor(a).", getFonte(isTimes, false, 10f, false));
            footerParagraph.Alignment = Element.ALIGN_CENTER;
            footerParagraph.SetLeading(0f, LEADING_TEXTO);
            document.Add(footerParagraph);

            footerParagraph = new Paragraph("\nPermitida a reprodução parcial ou total, desde que citada a fonte.", getFonte(isTimes, false, 12.8f, true));
            footerParagraph.Alignment = Element.ALIGN_CENTER;
            footerParagraph.SetLeading(0f, LEADING_TEXTO);
            document.Add(footerParagraph);
        }

        public void Write()
        {
            document.Close();
            httpResponse.ContentType = "application/pdf";
            httpResponse.Headers.Add("Content-Disposition", "attachment; filename=FichaCatalografica.pdf");
            httpResponse.Body.WriteAsync(stream.GetBuffer());
            httpResponse.Body.Close();
            stream.Close();
        }

        private Font getFonte(bool isTimes, bool isRed, float tamanho, bool isBold)
        {
            if (isTimes)
            {
                return FontFactory.GetFont("Times New Roman", tamanho, isBold ? Font.BOLD : Font.NORMAL, isRed ? BaseColor.RED : BaseColor.BLACK);
            }
            else
            {
                return FontFactory.GetFont("Arial", tamanho, isBold ? Font.BOLD : Font.NORMAL, isRed ? BaseColor.RED : BaseColor.BLACK);
            }
        }
    }
}