﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ajuda.aspx.cs" Inherits="ufmt.FichaCatalografica.Ajuda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <%--<asp:HyperLink ID="lnkVoltar" runat="server" NavigateUrl="~/Default.aspx">Voltar</asp:HyperLink>
    <br /><br />--%>
    <asp:Panel ID="panelErro" runat="server" CssClass="panelInformacao">    
        Nesta página estão listadas algumas normas e dicas sobre o preenchimento dos campos do formulário de geração de ficha catalográfica.
    </asp:Panel>
    <br />
    <p>
        <b>Nome: </b>
        Informe seu nome juntamente com os primeiros sobrenomes, incluindo as preposições. (ex: João da Silva e)
    </p>
    <p>
        <b>Sobrenome: </b>
        Informe seu último sobrenome, sem preposições. (ex: Souza)        
    </p>
    <p>
        <b>Título do trabalho: </b>
        Informe o título do seu trabalho.        
    </p>
    <p>
        <b>Sub-título do trabalho: </b>
        Caso o seu trabalho tenha sub-título, informe-o neste campo.        
    </p>
    <p>
        <b>Código Cutter: </b>        
        Uma vez que os campos Nome, Sobrenome e Título do Trabalho estejam preenchidos, clique no botão "Gerar" para gerar o código Cutter.
    </p>
    <p>
        <b>Trabalho: </b>
        Selecione o tipo do trabalho (Tese, Dissertação, etc.).
    </p>
    <p>
        <b>Curso/Programa: </b>
        Uma vez que o tipo do trabalho esteja selecionado, clique em buscar para abrir uma tela onde é possível procurar pelo nome do seu curso ou programa, tomando cuidado quanto ao campus do mesmo.        
        Ao encontrá-lo, clique no ícone de seleção.
    </p>
    <p>
        <b>Nome completo do orientador: </b>
        Informe o nome completo de seu orientador, <b>não incluindo</b> títulos como Dr. ou Msc.        
    </p>
    <p>
        <b>Orientadora: </b>
        Caso seja uma orientadora, marque esta caixa.        
    </p>    
    <p>
        <b>Nome completo do coorientador: </b>
        Informe o nome completo de seu co-orientador, <b>não incluindo</b> títulos como Dr. ou Msc.                
    </p>
    <p>
        <b>Coorientadora: </b>   
        Caso seja uma Co-orientadora, marque esta caixa.             
    </p>    
    <p>
        <b>Ano: </b>
        Informe o Ano do trabalho na forma yyyy. (ex: 1990);
    </p>
    <p>
        <b>Padrão do Número de folhas: </b>
        Selecione qual o padrão a ser usado ao mostrar o número de folhas. Caso selecione "Apenas números Arábicos", informe o total de páginas em arábico no campo logo abaixo (ex: 150).
        Caso selecione "Parte Romanos e parte Arábicos", informe primeiramente a quantidade de folhas em algarismos romanos em minúsculo (ex: xxii) e no outro campo informe o número restante de folhas em arábico (ex: 128).
    </p>    
    <p>
        <b>Número em romano: </b>
        Caso selecione "Parte Romanos e parte Arábicos" como Padrão do Número de folhas, informe a quantidade de folhas em algarismo romano em minúsculo (ex: xxii).
    </p>
    <p>
        <b>Número em arábico: </b>
         Caso selecione "Apenas números Arábicos", informe o total de folhas em algarismos arábicos (ex: 150).
         Caso selecione "Parte Romanos e parte Arábicos", informe apenas a quantidade de folhas usando algarismos arábicos (ex: 128).
    </p>
    <p>
        <b>Ilustrações: </b>
        Selecione "Não possui" caso seu trabalho não possua ilustrações, "Preto e branco" caso possua apenas ilustrações em preto e branco
        ou "Coloridas" caso possua alguma ilustração colorida.        
    </p>
    <p>
        <b>Bibliografia: </b>
        Selecione "Não incluída" caso seu trabalho não inclua bibliografia ou "Incluída" caso inclua bibliografia.
    </p>
    <p>
        <b>Altura da folha: </b> 
        Informe a altura da folha ou do volume de seu trabalho.       
    </p>
    <p>
        <b>Palavras-chave: </b>
        Informe no mínimo uma e no máximo cinco palavras-chave do seu trabalho.        
    </p>
    <p>
        <b>Fonte: </b>
        Selecione o tipo da fonte a ser utilizada na ficha catalográfica (geralmente a mesma do trabalho).        
    </p>
    <p>
        <b>Tamanho da Fonte: </b>        
        Selecione o tamanho da fonte que mais se adequa ao quadro da ficha catalográfica. Este tamanho deve ser alterado
        para ajustar o conteúdo gerado dentro dos limites das linhas do quadro, para que todo o texto seja mostrado uniformemente.
        Utilize fontes pequenas caso parte do texto esteja sumindo ou fontes grandes caso o espaço ao final seja muito grande.
    </p>
</asp:Content>
