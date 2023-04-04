using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for DadosWs06
/// </summary>
public class DadosWs13
{
	public DadosWs13()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    string _identificacaoSolicitacao = "";
    string _reciboSolicitacao = "";
    string _protocoloOcorrencia = "";
    string _protocolo = "";
    



    mensagemInformativa[] _mensagemInformativa;
    mensagemInformativa _mensagemInformativaUnica = new mensagemInformativa();
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
    public mensagemInformativa[] mensagemInformativa
    {
        get { return _mensagemInformativa; }
        set { _mensagemInformativa = value; }
    }

    public mensagemInformativa mensagemInformativaUnica
    {
        get { return _mensagemInformativaUnica; }
        set { _mensagemInformativaUnica = value; }
    }
}