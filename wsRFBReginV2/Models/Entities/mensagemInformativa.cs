using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for mensagemInformativa
/// </summary>
public class mensagemInformativa
{
	public mensagemInformativa()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected string _texto = "";
    protected string _nomeOrgaoResponsavel = "";
    protected string _link = "";

    public string link
    {
        get { return _link; }
        set { _link = value; }
    }
    public string texto
    {
        get { return _texto; }
        set { _texto = value; }
    }

    public string nomeOrgaoResponsavel
    {
        get { return _nomeOrgaoResponsavel; }
        set { _nomeOrgaoResponsavel = value; }
    }
}