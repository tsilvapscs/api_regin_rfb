using AcessoService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WsRFBReginV2
{
    /// <summary>
    /// Descrição resumida de ServiceReginS13
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceReginS13 : System.Web.Services.WebService
    {

        public ServiceReginS13()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public RetornoBasico ServiceWs13(DadosWs13 Dados)
        {
            RetornoBasico result = new RetornoBasico();
            Retorno ResultS13 = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                ServiceReginRFB s13 = new ServiceReginRFB();
                //RetornoBasico presul = new RetornoBasico();
                //RetornoBasico presuls24 = new RetornoBasico();

                ResultS13 = s13.ServiceWs13(Dados);

                result.codretorno = ResultS13.codretorno;
                result.descricao = ResultS13.descricao;
                result.status = ResultS13.status;
                return result;

            }
            catch (Exception ex)
            {
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.Message + " StackTrace " + ex.StackTrace + " SoapException ";
            }

            return result;
        }

    }
}
