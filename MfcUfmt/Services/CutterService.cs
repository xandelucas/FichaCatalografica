using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MfcUfmt.Data;
using MfcUfmt.Models;

namespace MfcUfmt.Services
{
    public class NegocioCutter
    {
        private readonly MfcUfmtContext _context;

        public NegocioCutter(MfcUfmtContext context)
        {
            _context = context;
        }

        public NegocioCutter()
        {
        }

        private static Hashtable htCutters = null;

        public Cutter GetCodigoCutter(string sobrenome, string nome)
        {
            Cutter cutterGerado = null;
            IList<Cutter> cutters = this.GetCutter(sobrenome[0].ToString());
            int indice = 1;

            //Primeiramente, iremos iterar para cada posição do sobrenome procurando cutters que comecem com a string
            // de 0 até o índice da iteração
            //Se não encontrar nenhum registro em um índice, utilizar a última lista gerada
            for (int i = 2; i <= sobrenome.Length; i++)
            {
                IList<Cutter> novoCutter = (from cutter in cutters
                                            where cutter.Sobrenome.StartsWith(sobrenome.Substring(0, i))
                                            select cutter).ToList();
                if (novoCutter == null || novoCutter.Count < 1)
                {
                    break;
                }
                else
                {
                    cutters = novoCutter;
                    indice = i;
                }
            }

            //Verificar se existem cutters que são iguais ao sobrenome
            IList<Cutter> cuttersIguais = (from cutter in cutters
                                           where cutter.Sobrenome.Equals(sobrenome)
                                           select cutter).ToList();

            if (cuttersIguais == null || cuttersIguais.Count < 1)
            {
                //Se não houverem cutters iguais, verificar os cutters de ordem alfabética inferior ao caractere
                // da posição onde a interação parou no sobrenome
                IList<Cutter> cuttersParecidos = (from cutter in cutters
                                                  where
                          cutter.Sobrenome.Length >= indice + 1
                          && sobrenome.Length > indice
                          && sobrenome[indice] >= cutter.Sobrenome[indice]
                                                  select cutter).ToList();
                if (cuttersParecidos == null || cuttersParecidos.Count < 1)
                {
                    //Se não houverem cutters parecidos, recuperar os cutters que, até a posição do índice, sejam
                    // iguais ao sobrenome
                    cuttersParecidos = (from cutter in cutters
                                        where
                      cutter.Sobrenome == sobrenome.Substring(0, indice)
                                        select cutter).ToList();

                    if (cuttersParecidos == null || cuttersParecidos.Count < 1)
                    {
                        //se ainda não houverem resultados, provavelmente o primeiro registro da lista de cutters
                        // que foi gerada na iteração já passa do cutter que estamos procurando
                        //Logo devemos recuperar o cutter anterior a este
                        cutterGerado = this.GetCutterAnterior(sobrenome[0].ToString(), cutters[0].CodigoCutter);
                    }
                    else
                    {
                        //se houverem cutters, o cutter escolhido deve ser o último
                        cutterGerado = cuttersParecidos.Last();
                    }
                }
                else
                {
                    //se houverem cutters, o cutter escolhido deve ser o último
                    cutterGerado = cuttersParecidos.Last();
                }
            }
            else
            {
                //Para os cutters iguais, o algoritmo deve-se repetir para o nome
                cutters = cuttersIguais;
                indice = 0;
                for (int i = 1; i <= nome.Length; i++)
                {
                    IList<Cutter> novoCutter = (from cutter in cutters
                                                where !string.IsNullOrEmpty(cutter.Nome) &&
                                                cutter.Nome.StartsWith(nome.Substring(0, i))
                                                select cutter).ToList();
                    if (novoCutter == null || novoCutter.Count < 1)
                    {
                        break;
                    }
                    else
                    {
                        cutters = novoCutter;
                        indice = i;
                    }
                }

                if (indice == 0)
                {
                    //Se o índice não mudou, significa que não encontrou nenhum nome que fosse válido na primeira
                    // posição do nome
                    //Nesse caso, devemos verificar os nomes de ordem alfabética inferior
                    IList<Cutter> cuttersParecidos = (from cutter in cuttersIguais
                                                      where
                   !string.IsNullOrEmpty(cutter.Nome) &&
                   cutter.Nome.Length >= indice + 1
                   && nome.Length > indice
                   && nome[indice] >= cutter.Nome[indice]
                                                      select cutter).ToList();
                    if (cuttersParecidos == null || cuttersParecidos.Count < 1)
                    {
                        //Se não existe nenhum cutter com nome nem abaixo do nome da pessoa, logo devemos
                        // pegar os cutters com sobrenome igual, mas sem nome
                        cuttersParecidos = (from cutter in cuttersIguais
                                            where
                   string.IsNullOrEmpty(cutter.Nome)
                                            select cutter).ToList();
                        if (cuttersParecidos == null || cuttersParecidos.Count < 1)
                        {
                            //Se não houverem cutters de sobrenome igual mas sem nome, devemos pegar
                            // o cutter anterior ao primeiro deles
                            cutterGerado = this.GetCutterAnterior(sobrenome[0].ToString(), cuttersIguais[0].CodigoCutter);
                        }
                        else
                        {
                            //Se encontrarmos um cutter de sobrenome igual mas sem nome, provavelmente será único
                            cutterGerado = cuttersParecidos[0];
                        }

                    }
                    else
                    {
                        //Se encontrarmos algum cutter com nome parecido, devemos pegar o último deles
                        cutterGerado = cuttersParecidos.Last();
                    }
                }
                else
                {
                    //Senão, devemos ver quais dos nomes são exatamente iguais
                    cuttersIguais = (from cutter in cutters
                                     where cutter.Nome.Equals(nome)
                                     select cutter).ToList();
                    if (cuttersIguais == null || cuttersIguais.Count < 1)
                    {
                        //Se não houverem cutters iguais, devemos ver se existem parecidos
                        IList<Cutter> cuttersParecidos = (from cutter in cutters
                                                          where
                         cutter.Nome.Length >= indice + 1
                         && nome[indice] >= cutter.Nome[indice]
                                                          select cutter).ToList();
                        if (cuttersParecidos == null || cuttersParecidos.Count < 1)
                        {
                            //Se não houverem cutters parecidos, recuperar os cutters que, até a posição do índice
                            // sejam iguais ao nome
                            cuttersParecidos = (from cutter in cutters
                                                where
                             cutter.Nome == nome.Substring(0, indice)
                                                select cutter).ToList();

                            if (cuttersParecidos == null || cuttersParecidos.Count < 1)
                            {
                                //se ainda não houverem resultados, provavelmente o primeiro registro da lista de cutters
                                // que foi gerada na iteração já passa do cutter que estamos procurando
                                //Logo devemos recuperar o cutter anterior a este
                                cutterGerado = this.GetCutterAnterior(sobrenome[0].ToString(), cutters[0].CodigoCutter);
                            }
                            else
                            {
                                //se houverem cutters, o cutter escolhido deve ser o último
                                cutterGerado = cuttersParecidos.Last();
                            }
                        }
                        else
                        {
                            //se houverem cutters, o cutter escolhido deve ser o último
                            cutterGerado = cuttersParecidos.Last();
                        }

                    }
                    else
                    {
                        //Se houverem cutters com nome iguais, como não há mais classificação, qualquer um vale
                        cutterGerado = cuttersIguais[0];
                    }
                }
            }

            //Se não foi possível definir o cutter, provavelmente o sobrenome não consta na lista da letra inicial
            // do mesmo, não sendo parecido nem mesmo com o primeiro (no caso do Qa)
            //Neste caso, o certo é retornar o primeiro cutter da lista, para que a classificação ainda continue
            // na letra Q
            if (cutterGerado == null)
            {
                cutterGerado = this.GetCutter(sobrenome[0].ToString())[0];
            }
            return cutterGerado;
        }

        private Cutter GetCutterAnterior(String letra, int codigoCutter)
        {
            IList<Cutter> cutters = this.GetCutter(letra);

            int meio;
            int min = 0;
            int max = cutters.Count - 1;
            do
            {
                meio = (int)(min + max) / 2;
                if (cutters[meio].CodigoCutter == codigoCutter)
                {
                    if (meio - 1 < 0)
                    {
                        return null;
                    }
                    else
                    {
                        return cutters[meio - 1];
                    }
                }
                else if (codigoCutter > cutters[meio].CodigoCutter)
                {
                    min = meio + 1;
                }
                else
                {
                    max = meio - 1;
                }
            }
            while (min <= max);

            return null;
        }

        private IList<Cutter> GetCutter(String letra)
        {
            if (htCutters == null)
            {
                htCutters = this.GetHashTableCutters();
            }
            return htCutters[letra] as IList<Cutter>;
        }


        /*
         * Método que lê o XML de cutters e retorna a hashtable de cutters 
         */
        private Hashtable GetHashTableCutters()
        {
            Hashtable cutters = new Hashtable();
            String letraAtual = "";
            String elementoAtual = "";
            Cutter cutter = null;
            XmlTextReader reader = new XmlTextReader($"{AppContext.BaseDirectory}/XML/xmlCutters.xml");//alterado do original
            while (reader.Read())
            {

                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "letra":
                                while (reader.MoveToNextAttribute())
                                {
                                    letraAtual = reader.Value;
                                }
                                cutters.Add(letraAtual, new List<Cutter>());
                                break;
                            case "cutter":
                                cutter = new Cutter();
                                break;
                            case "codigoCutter":
                                elementoAtual = "codigoCutter";
                                break;
                            case "sobrenome":
                                elementoAtual = "sobrenome";
                                break;
                            case "nome":
                                elementoAtual = "nome";
                                break;
                        }
                        break;
                    case XmlNodeType.Text:
                        switch (elementoAtual)
                        {
                            case "codigoCutter":
                                cutter.CodigoCutter = int.Parse(reader.Value);
                                break;
                            case "sobrenome":
                                cutter.Sobrenome = reader.Value.ToUpper();
                                break;
                            case "nome":
                                cutter.Nome = reader.Value.ToUpper();
                                break;
                        }
                        elementoAtual = "";
                        break;
                    case XmlNodeType.EndElement:
                        if (reader.Name.Equals("cutter"))
                        {
                            (cutters[letraAtual] as List<Cutter>).Add(cutter);
                        }
                        break;
                }
            }

            return cutters;
        }
    }
}