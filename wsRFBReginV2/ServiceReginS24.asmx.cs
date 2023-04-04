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
    /// Descrição resumida de ServiceReginS24
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceReginS24 : System.Web.Services.WebService
    {

        public ServiceReginS24()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        /// <summary>
        /// Marcar = 1 - Sim
        ///          2 = Não
        /// </summary>
        /// <param name="cnpjOrgaoRegistro"></param>
        /// <param name="cnpjEmpresa"></param>
        /// <param name="Marcar"></param>
        /// <returns></returns>
        [WebMethod(Description = @"Marcar CNPJ para Receber informaçoes do CNPJ de Outro estado 
                        <br>
                                cnpjOrgaoRegistro = CNPJ do orgao de Registro<br>
                                cnpjEmpresa = CNPJ da empresa que se quer a Informação<br>
                                Marcar = 1 - Sim
                                  2 = Não ")]
        public RetornoBasico ServiceWs24(string cnpjOrgaoRegistro, string cnpjEmpresa, string Marcar)
        {
            RetornoBasico result = new RetornoBasico();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                ServiceReginRFB s24 = new ServiceReginRFB();
                //RetornoBasico presul = new RetornoBasico();
                //RetornoBasico presuls24 = new RetornoBasico();

                result = s24.ServiceWs24(cnpjOrgaoRegistro, cnpjEmpresa, Marcar);



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
