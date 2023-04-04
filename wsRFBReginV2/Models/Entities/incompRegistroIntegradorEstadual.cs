using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for incompRegistroIntegradorEstadual
/// </summary>
public class incompRegistroIntegradorEstadual
{
	public incompRegistroIntegradorEstadual()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected string _codigo = "";
    protected string _mensagem = "";

    public string codigo
    {
        get { return _codigo; }
        set { _codigo = value; }
    }

    public string mensagem
    {
        get { return _mensagem; }
        set { _mensagem = value; }
    }
 
   
}