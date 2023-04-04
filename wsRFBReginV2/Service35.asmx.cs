using psc.Receita.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using WsRFBReginV2.Models;

namespace WsRFBReginV2
{
    /// <summary>
    /// Descrição resumida de Service35
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class Service35 : System.Web.Services.WebService
    {

        public Service35()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public RetornoV2 ServiceWs35(string Identificacao, string Recibo)
        {

            RetornoV2 result = new RetornoV2();
            WsServicesReginRFB.Retorno resultIn = new WsServicesReginRFB.Retorno();
            try
            {
                GlobalV1.ValidaReques();

                WsServicesReginRFB.ServiceReginRFB se = new WsServicesReginRFB.ServiceReginRFB();

                se.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                resultIn = se.ServiceWs35Soarquivo(Identificacao, Recibo);

                result.codretorno = resultIn.codretorno;
                result.descricao = resultIn.descricao;
                result.status = resultIn.status;
                result.XmlDBE = resultIn.XmlDBE;
                result.Identificacao = resultIn.Identificacao;
                result.Recibo = resultIn.Recibo;

                return result;
            }
            catch (Exception ex)
            {
                string XmlDados = GlobalV1.CreateXML(result);
                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = XmlDados;
                    e35.t73307_erro = ex.Message;
                    e35.t73307_ide_solicitacao = Identificacao;
                    e35.t73307_rec_solicitacao = "WS35EXT";
                    e35.Update();
                }
                result.status = "NOK";
                result.codretorno = "99";
                result.descricao = ex.Message;
                return result;
            }
        }
    }
}
