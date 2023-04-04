using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for DadosWs06
/// </summary>
public class DadosWs04
{
	public DadosWs04()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    string _identificacaoSolicitacao = "";
    string _reciboSolicitacao = "";
    string _protocoloOcorrencia = "";
    string _protocolo = "";
    string _resultadoValidacao = "";



    mensagemInformativa[] _mensagem;
    mensagemInformativa _mensagemUnica = new mensagemInformativa();

    public string resultadoValidacao
    {
        get { return _resultadoValidacao; }
        set { _resultadoValidacao = value; }
    }
    public string protocoloOcorrencia
    {
        get { return _protocoloOcorrencia; }
        set { _protocoloOcorrencia = value; }
    }


    public string protocolo
    {
        get { return _protocolo; }
        set { _protocolo = value; }
    }


    public string identificacaoSolicitacao
    {
        get { return _identificacaoSolicitacao; }
        set { _identificacaoSolicitacao = value; }
    }
    public string reciboSolicitacao
    {
        get { return _reciboSolicitacao; }
        set { _reciboSolicitacao = value; }
    }
    public mensagemInformativa[] mensagem
    {
        get { return _mensagem; }
        set { _mensagem = value; }
    }

    public mensagemInformativa mensagemUnica
    {
        get { return _mensagemUnica; }
        set { _mensagemUnica = value; }
    }
}