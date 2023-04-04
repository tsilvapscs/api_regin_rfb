using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for DadosWs06
/// </summary>
public class DadosWs06
{
	public DadosWs06()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    string _identificacaoSolicitacao = "";
    string _reciboSolicitacao = "";
    string _nomeEmpresarial = "";
    string _numeroNire = "";
    string _numeroRegistroCartorio = "";
    string _numeroRegistroOab = "";
    string _dataRegistro = "";
    string _cpfResponsavelDeferimento = "";
    string _numeroNire246 = "";
    string _resultadoRegistroIntegradorEstadual = "";
    string _numeroServentia = "";
    string _Uf = "";
    string _IndicadorCNPJNome = "";

    incompRegistroIntegradorEstadual[] _incompRegistroIntegradorEstadual;
    public string IndicadorCNPJNome
    {
        get { return _IndicadorCNPJNome; }
        set { _IndicadorCNPJNome = value; }
    }
    public string Uf
    {
        get { return _Uf; }
        set { _Uf = value; }
    }
    public string numeroServentia
    {
        get { return _numeroServentia; }
        set { _numeroServentia = value; }
    }
    public string numeroRegistroOab
    {
        get { return _numeroRegistroOab; }
        set { _numeroRegistroOab = value; }
    }

    public string resultadoRegistroIntegradorEstadual
    {
        get { return _resultadoRegistroIntegradorEstadual; }
        set { _resultadoRegistroIntegradorEstadual = value; }
    }


    public string numeroNire246
    {
        get { return _numeroNire246; }
        set { _numeroNire246 = value; }
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
    public string nomeEmpresarial
    {
        get { return _nomeEmpresarial; }
        set { _nomeEmpresarial = value; }
    }
    public string numeroNire
    {
        get { return _numeroNire; }
        set { _numeroNire = value; }
    }
    public string numeroRegistroCartorio
    {
        get { return _numeroRegistroCartorio; }
        set { _numeroRegistroCartorio = value; }
    }
    
    public string dataRegistro
    {
        get { return _dataRegistro; }
        set { _dataRegistro = value; }
    }
    public string cpfResponsavelDeferimento
    {
        get { return _cpfResponsavelDeferimento; }
        set { _cpfResponsavelDeferimento = value; }
    }

    public incompRegistroIntegradorEstadual[] incompRegistroIntegradorEstadual
    {
        get { return _incompRegistroIntegradorEstadual; }
        set { _incompRegistroIntegradorEstadual = value; }
    }
}