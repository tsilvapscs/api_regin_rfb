using AcessoService;
using psc.Receita.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using WsRFBReginV2.Models;

namespace WsRFBReginV2
{
    /// <summary>
    /// Descrição resumida de ServiceViabilidade
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceViabilidade : System.Web.Services.WebService
    {

        public ServiceViabilidade()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public RetornoV2 ServiceDadosViabilidade(string pProtocolo)
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            return ServiceDadosProceso(pProtocolo, "2");
        }

        private RetornoV2 ServiceDadosProceso(string pProtocolo, string pTipoProcesso)
        {

            RetornoV2 result = new RetornoV2();
            WsServicesReginRFB.Retorno resultIn = new WsServicesReginRFB.Retorno();
            try
            {

                //GlobalV1.ValidaReques();

                string pCodAplicacao = "RECEITA";

                if (pTipoProcesso == "2")
                    pCodAplicacao = "VIABILID";

                #region Pega Xml Regin

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "PKG_JUCESC.Crear_Arch_Xml_Group";
                        cmd.CommandType = CommandType.StoredProcedure;

                        decimal pOrdem = int.MinValue;
                        string parametros = "";

                        cmd.Parameters.Add(new OracleParameter("P_COD_APLIC", OracleType.Char, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCodAplicacao));
                        cmd.Parameters.Add(new OracleParameter("P_PARAMETRO0", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, pProtocolo));
                        cmd.Parameters.Add(new OracleParameter("P_PARAMETRO1", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                        cmd.Parameters.Add(new OracleParameter("P_PARAMETRO2", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                        cmd.Parameters.Add(new OracleParameter("P_PARAMETRO3", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                        cmd.Parameters.Add(new OracleParameter("P_PARAMETRO4", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                        cmd.Parameters.Add(new OracleParameter("P_PARAMETRO5", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                        cmd.Parameters.Add(new OracleParameter("P_PARAMETRO6", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                        cmd.Parameters.Add(new OracleParameter("P_PARAMETRO7", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                        cmd.Parameters.Add(new OracleParameter("P_PARAMETRO8", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                        cmd.Parameters.Add(new OracleParameter("P_PARAMETRO9", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                        cmd.Parameters.Add(new OracleParameter("p_ordem", OracleType.Number, 15, ParameterDirection.Input, false, 15, 0, "", DataRowVersion.Proposed, pOrdem));
                        cmd.Parameters.Add(new OracleParameter("P_XML", OracleType.Clob, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));


                        cmd.ExecuteNonQuery();
                        OracleLob CLOB = (OracleLob)cmd.Parameters["P_XML"].Value;

                        string pXml = (string)CLOB.Value;

                        result.XmlDBE = pXml;
                        result.status = "OK";
                        result.codretorno = "00";

                        if (pXml.Length < 500)
                        {
                            result.XmlDBE = "";
                            result.status = "NOK";
                            result.codretorno = "99";
                            result.descricao = "Registro não encontrado";
                        }


                    }
                }
                #endregion

                return result;
            }
            catch (Exception ex)
            {
                string XmlDados = GlobalV1.CreateXML(result);
                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = XmlDados;
                    e35.t73307_erro = ex.Message;
                    e35.t73307_ide_solicitacao = pProtocolo;
                    e35.t73307_rec_solicitacao = "WSVIAEXT";
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
