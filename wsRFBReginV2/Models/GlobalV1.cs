using psc.Receita.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Serialization;

namespace WsRFBReginV2.Models
{
    public class GlobalV1
    {
        public const string acesso = "";
        public const string acessows0506 = "ws7735";


        public static Object CreateObject(string XMLString, Object YourClassObject)
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(YourClassObject.GetType());
            //The StringReader will be the stream holder for the existing XML file 
            YourClassObject = oXmlSerializer.Deserialize(new StringReader(XMLString));
            //initially deserialized, the data is represented by an object without a defined type 
            return YourClassObject;
        }

        public static string CreateXML(Object YourClassObject)
        {
            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
                                                      // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(YourClassObject.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, YourClassObject);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }
        public static bool valNulo(object pValue)
        {
            if (pValue == null || pValue.ToString() == "")
            {
                return true;
            }

            return false;
        }

        public static void setNullToDefVals(ref DataSet dsXML)
        {
            try
            {
                if (dsXML.Tables.Count > 0)
                {
                    for (int index = 0; index < dsXML.Tables.Count; index++)
                    {
                        if (dsXML.Tables[index].Rows.Count > 0)
                        {
                            for (int rindex = 0; rindex < dsXML.Tables[index].Rows.Count; rindex++)
                            {
                                for (int cindex = 0; cindex < dsXML.Tables[index].Columns.Count; cindex++)
                                {
                                    if (dsXML.Tables[index].Rows[rindex][cindex] == System.DBNull.Value)
                                    {
                                        DateTime defdate = new DateTime(2000, 1, 1);
                                        if (dsXML.Tables[index].Columns[cindex].DataType == System.Type.GetType("System.String"))
                                            dsXML.Tables[index].Rows[rindex][cindex] = "";
                                        else if (dsXML.Tables[index].Columns[cindex].DataType == System.Type.GetType("System.Int32"))
                                            dsXML.Tables[index].Rows[rindex][cindex] = 0;
                                        else if (dsXML.Tables[index].Columns[cindex].DataType == System.Type.GetType("System.DateTime"))
                                            dsXML.Tables[index].Rows[rindex][cindex] = defdate;
                                        else if (dsXML.Tables[index].Columns[cindex].DataType == System.Type.GetType("System.Decimal"))
                                            dsXML.Tables[index].Rows[rindex][cindex] = 0.0;
                                        else
                                        {
                                            // dsXML.Tables[index].Columns[cindex].DataType = System.Type.GetType("System.String");
                                            // dsXML.Tables[index].Rows[rindex][cindex] = "";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                int i = 0;
            }
        }

        public static string valNuloBranco(object pValue)
        {
            try
            {
                if (pValue == null || pValue.ToString() == "")
                {
                    return "";
                }

                return pValue.ToString();
            }
            catch
            {
                return "";
            }
        }


        public static DateTime ConvertStringDateTime(string _data)
        {
            if (_data.Trim() != "")
            {
                int dia = int.Parse(_data.Substring(6, 2));
                int mes = int.Parse(_data.Substring(4, 2));
                int ano = int.Parse(_data.Substring(0, 4));

                return new DateTime(ano, mes, dia);
            }
            return new DateTime(1, 1, 1);
        }
        public static void salvaDados(string xmlMEIInput, string nomeArquivo)
        {
            Random random = new Random();
            //int i = random.Next(200);
            //string nomeArquivo = codServico + "1 " + versao + "2 " + numeroProtocolo + "3 " + numeroOcorrencia.ToString() + "4 " + numeroServicoRecusado.ToString();
            FileInfo f = new FileInfo(@ConfigurationManager.AppSettings["caminhoFisicoXml"].ToString() + nomeArquivo);
            if (f.Exists)
            {
                f.Delete();
            }
            StreamWriter w = f.CreateText();
            w.WriteLine(xmlMEIInput);
            w.Close();
        }
        public void TrataS35TabelaMySql(string pCodServicoRFB, string Recibo, string Identificacao, string codretorno, string ResponseRFB)
        {

            //string codretorno = "";
            //if (pResponseNovo.@return.codigoRetorno != null && pResponseNovo.@return.codigoRetorno != "")
            //    codretorno = pResponseNovo.@return.codigoRetorno;
            if (pCodServicoRFB == "S05" || pCodServicoRFB == "S06" || pCodServicoRFB == "S35")
            {
                T73309_WS35_RFB ws35Acesso = new T73309_WS35_RFB();
                ws35Acesso.t73309_recibo = Recibo;
                ws35Acesso.t73309_Identificacao = Identificacao;
                ws35Acesso.t73309_xml = ResponseRFB;
                ws35Acesso.t73309_CodServico = pCodServicoRFB;
                ws35Acesso.t73309_codretorno = codretorno;

                ws35Acesso.Update_Tdbe_rfb_acesso();

            }

            #region Verifica Dados S35 na tabela
            int diasPrazoBuscaws35 = 0;
            bool ResponsavelDeferimentoOk = false;
            if (ConfigurationManager.AppSettings.Get("diasPrazoBuscaws35") != null && ConfigurationManager.AppSettings.Get("diasPrazoBuscaws35").ToString() != "")
            {
                try
                {
                    diasPrazoBuscaws35 = int.Parse(ConfigurationManager.AppSettings.Get("diasPrazoBuscaws35").ToString());
                }
                catch { }
            }

            if (diasPrazoBuscaws35 > 0)
            {
                // nao vou apagar no S05 porque se nao vou chemar ao 35 novamente e nao vou ganar perfommance
                //if (pCodServicoRFB == "S05" || pCodServicoRFB == "S06" || pCodServicoRFB == "S22")
                bool apagaRegistro = false;
                if (pCodServicoRFB == "S22")
                {
                    apagaRegistro = true;
                }
                if (pCodServicoRFB == "S06" && codretorno == "OK")
                {
                    apagaRegistro = true;
                }

                //Apago registro porque a RFB cancelou o DBE e nao envia para o ente
                if (pCodServicoRFB == "S05" && codretorno == "03")
                {
                    apagaRegistro = true;
                }
                //Solicitação ja esta deferida
                if (pCodServicoRFB == "S05" && codretorno == "04")
                {
                    apagaRegistro = true;
                }

                if (apagaRegistro)
                {
                    T73309_WS35_RFB pResultWs35 = new T73309_WS35_RFB();
                    pResultWs35.t73309_recibo = Recibo;
                    pResultWs35.t73309_Identificacao = Identificacao;
                    pResultWs35.DeletePk();
                }

                if (pCodServicoRFB == "S35")
                {
                    WsServices35RFB.serviceResponse pResponseNovo = new WsServices35RFB.serviceResponse();
                    pResponseNovo = (WsServices35RFB.serviceResponse)GlobalV1.CreateObject(ResponseRFB, pResponseNovo);

                    if (ConfigurationManager.AppSettings["orgaoResponsavelDeferimento"] != null && ConfigurationManager.AppSettings["orgaoResponsavelDeferimento"].ToString() != "")
                    {
                        string[] orgaoResponsavelDeferimento = ConfigurationManager.AppSettings["orgaoResponsavelDeferimento"].ToString().Split('-');

                        foreach (string pOrgaoResponsavelDeferimento in orgaoResponsavelDeferimento)
                        {
                            if (pResponseNovo.dadosRedesim.orgaoResponsavelDeferimento == pOrgaoResponsavelDeferimento)
                            {
                                ResponsavelDeferimentoOk = true;
                            }
                        }
                    }
                    //caso seja do deferimento do ortao de rsgistro
                    if (pResponseNovo.dadosRedesim.orgaoResponsavelDeferimento != null && ResponsavelDeferimentoOk)
                    {

                        //e for algumas de esses codigos
                        if (codretorno == "09" || codretorno == "10" || codretorno == "05" || codretorno == "04" || codretorno == "61" || codretorno == "11")
                        {
                            T73309_WS35_RFB ws35 = new T73309_WS35_RFB();
                            ws35.t73309_recibo = Recibo;
                            ws35.t73309_Identificacao = Identificacao;
                            ws35.t73309_xml = ResponseRFB;
                            ws35.t73309_codretorno = codretorno;

                            ws35.Update();
                        }
                    }
                    else
                    {
                        //caso nao seja do meu meu orgao de rsgustro, mas estiver indeferida, deferida ou cancelada
                        if (codretorno == "10" || codretorno == "04" || codretorno == "05")
                        {
                            T73309_WS35_RFB ws35 = new T73309_WS35_RFB();
                            ws35.t73309_recibo = Recibo;
                            ws35.t73309_Identificacao = Identificacao;
                            ws35.t73309_xml = ResponseRFB;
                            ws35.t73309_codretorno = codretorno;

                            ws35.Update();
                        }
                    }
                }


            }
            #endregion
        }

        #region Validar Versao WSDL
        public static string GetWSDL(Uri webServiceUri, X509Certificate cert)
        {
            //create a WebRequest object and fetch the WSDL file for the web service

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(webServiceUri.AbsoluteUri);
            request.ClientCertificates.Add(cert);
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            System.IO.Stream stream = response.GetResponseStream();
            ServiceDescription desc = ServiceDescription.Read(stream);
            MemoryStream leitor = new MemoryStream();
            desc.Write(leitor);
            return getMD5Hash(Encoding.ASCII.GetString(leitor.ToArray()));
            //return Encoding.ASCII.GetString(leitor.ToArray());


        }

        private static string getMD5Hash(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion


        #region Procedimentos s01
        public DataTable EnviaViabiliadesEFBFixo(string pProtocolo)
        {

            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();
                DataTable toReturn = new DataTable("DadosViabilidade");

                cmdToExecute.CommandText = "RFBCURSORVIABILIDADEEnviarFixo";
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                // Use base class' connection object
                cmdToExecute.Connection = _conn;


                cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, toReturn));
                cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));

                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                try
                {
                    adapter.Fill(toReturn);
                    return toReturn;
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                    adapter.Dispose();
                }
            }
        }

        public DataTable EnviaViabiliadesServico02RFB(string pProtocolo)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    SqlCommand cmdToExecute = new SqlCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("DadosViabilidade");

                    cmdToExecute.CommandText = "RFBCURSORServico02StatusEnviar";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;


                    cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));

                    SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
            else
            {
                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    OracleCommand cmdToExecute = new OracleCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("DadosViabilidade");

                    cmdToExecute.CommandText = "RFBCURSORServico02StatusEnviar";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;


                    cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, toReturn));
                    cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));

                    OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
        }

        public DataTable EnviaViabiliadesRFB(string pProtocolo)
        {

            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    SqlCommand cmdToExecute = new SqlCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("DadosViabilidade");

                    cmdToExecute.CommandText = "RFBCURSORVIABILIDADEEnviar";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;


                    //cmdToExecute.Parameters.Add(new SqlParameter("pCursor", OracleType.Cursor, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, toReturn));
                    cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));

                    SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
            {

                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    OracleCommand cmdToExecute = new OracleCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("DadosViabilidade");

                    cmdToExecute.CommandText = "RFBCURSORVIABILIDADEEnviar";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;


                    cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, toReturn));
                    cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));

                    OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
        }


        public void UpdateViabilidadeNAOOKReceita(string pProtocolo, string erroProcesso)
        {
            UpdateViabilidadeNAOOKReceita(pProtocolo, erroProcesso, "", "");
        }


        public void UpdateViabilidadeNAOOKReceita(string pProtocolo, string erroProcesso, string pXmlRFB, string pXmlResponseRFB)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction _conn = conn.BeginTransaction())
                    {
                        using (SqlCommand cmdToExecute = new SqlCommand())
                        {
                            cmdToExecute.Connection = _conn.Connection;
                            cmdToExecute.Transaction = _conn;

                            StringBuilder Sql = new StringBuilder();

                            cmdToExecute.CommandText = "update via_protocolo_viab set vpv_data_envio_rfb = getdate(), vpv_status_proc_rfb = 9, VPV_ERROR_PROCESSO = @ErroProcesso where vpv_cod_protocolo = @pProtocolo";
                            cmdToExecute.CommandType = CommandType.Text;
                            // Use base class' connection object
                            cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                            cmdToExecute.Parameters.Add(new SqlParameter("ErroProcesso", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, erroProcesso));

                            cmdToExecute.ExecuteNonQuery();
                            if (pXmlRFB != "")
                            {
                                cmdToExecute.Parameters.Clear();
                                cmdToExecute.CommandText = "update VIA_PRO_XMLRFB set VPX_XML_RESPONSE = @pXmlResponseRFB, VPX_XML_ENVIADO_ERRO = @pXmlRFB,  VPX_STATUS_ENVIO_JUNTA = 1,  VPX_DT_INCLUSAO = getdate() where VPX_COD_PROTOCOLO = @pProtocolo";
                                cmdToExecute.CommandType = CommandType.Text;
                                // Use base class' connection object

                                cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                                cmdToExecute.Parameters.Add(new SqlParameter("pXmlRFB", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pXmlRFB));
                                cmdToExecute.Parameters.Add(new SqlParameter("pXmlResponseRFB", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pXmlResponseRFB));


                                if (cmdToExecute.ExecuteNonQuery() == 0)
                                {
                                    Sql = new StringBuilder();
                                    cmdToExecute.Parameters.Clear();
                                    Sql.AppendLine("INSERT INTO VIA_PRO_XMLRFB (");
                                    Sql.AppendLine("VPX_COD_PROTOCOLO,");
                                    Sql.AppendLine("VPX_DT_INCLUSAO,");
                                    Sql.AppendLine("VPX_STATUS_ENVIO_JUNTA,");
                                    Sql.AppendLine("VPX_XML_ENVIADO_ERRO,");
                                    Sql.AppendLine("VPX_XML_RESPONSE");
                                    Sql.AppendLine(") ");
                                    Sql.AppendLine("Values ");
                                    Sql.AppendLine("( ");
                                    Sql.AppendLine("@pProtocolo,");
                                    Sql.AppendLine("getdate(),");
                                    Sql.AppendLine("1,");
                                    Sql.AppendLine("@pXmlRFB,");
                                    Sql.AppendLine("@pXmlResponseRFB");
                                    Sql.AppendLine(") ");



                                    cmdToExecute.CommandText = Sql.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    // Use base class' connection object

                                    cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                                    cmdToExecute.Parameters.Add(new SqlParameter("pXmlRFB", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pXmlRFB));
                                    cmdToExecute.Parameters.Add(new SqlParameter("pXmlResponseRFB", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pXmlResponseRFB));
                                    cmdToExecute.ExecuteNonQuery();
                                }
                            }

                        }
                        _conn.Commit();
                    }
                }





                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    SqlCommand cmdToExecute = new SqlCommand();
                    StringBuilder Sql = new StringBuilder();

                    cmdToExecute.CommandText = "update via_protocolo_viab set vpv_data_envio_rfb = getdate(), vpv_status_proc_rfb = 9, VPV_ERROR_PROCESSO = @ErroProcesso where vpv_cod_protocolo = @pProtocolo";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    _conn.Open();
                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecute.Parameters.Add(new SqlParameter("ErroProcesso", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, erroProcesso));

                    try
                    {
                        cmdToExecute.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                    }
                }
            }
            else
            {
                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    OracleCommand cmdToExecute = new OracleCommand();
                    StringBuilder Sql = new StringBuilder();

                    cmdToExecute.CommandText = "update via_protocolo_viab set vpv_data_envio_rfb = sysdate, vpv_status_proc_rfb = 9, VPV_ERROR_PROCESSO = :ErroProcesso where vpv_cod_protocolo = :pProtocolo";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    _conn.Open();
                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecute.Parameters.Add(new OracleParameter("ErroProcesso", OracleType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, erroProcesso));

                    try
                    {
                        cmdToExecute.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                    }
                }
            }
        }

        public void UpdateViabilidadeNAOOKReceitaWs02(string pProtocolo, string erroProcesso)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    SqlCommand cmdToExecute = new SqlCommand();
                    StringBuilder Sql = new StringBuilder();

                    cmdToExecute.CommandText = "update via_protocolo_viab set vpv_data_envio_rfb = getdate(), vpv_status_proc_rfb = -9, VPV_ERROR_PROCESSO = @ErroProcesso where vpv_cod_protocolo = @pProtocolo";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    _conn.Open();
                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecute.Parameters.Add(new SqlParameter("ErroProcesso", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, erroProcesso));

                    try
                    {
                        cmdToExecute.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                    }
                }
            }
            else
            {
                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    OracleCommand cmdToExecute = new OracleCommand();
                    StringBuilder Sql = new StringBuilder();

                    cmdToExecute.CommandText = "update via_protocolo_viab set vpv_data_envio_rfb = sysdate, vpv_status_proc_rfb = -9, VPV_ERROR_PROCESSO = :ErroProcesso where vpv_cod_protocolo = :pProtocolo";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    _conn.Open();
                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecute.Parameters.Add(new OracleParameter("ErroProcesso", OracleType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, erroProcesso));

                    try
                    {
                        cmdToExecute.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                    }
                }
            }
        }

        public void UpdateViabilidadeEnviadaReceitaWs02(string pProtocolo, string pStatusRFB)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    SqlCommand cmdToExecute = new SqlCommand();
                    StringBuilder Sql = new StringBuilder();

                    cmdToExecute.CommandText = "update via_protocolo_viab set VPV_STATUS_ENV_RFB = @pStatusRFB,  vpv_data_envio_rfb = getdate(), VPV_ERROR_PROCESSO = NULL, vpv_status_proc_rfb = -3 where vpv_cod_protocolo = @pProtocolo";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    _conn.Open();
                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecute.Parameters.Add(new SqlParameter("pStatusRFB", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pStatusRFB));

                    try
                    {

                        cmdToExecute.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                    }
                }
            }
            else
            {
                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    OracleCommand cmdToExecute = new OracleCommand();
                    StringBuilder Sql = new StringBuilder();

                    cmdToExecute.CommandText = "update via_protocolo_viab set VPV_STATUS_ENV_RFB = :pStatusRFB,  vpv_data_envio_rfb = sysdate, VPV_ERROR_PROCESSO = NULL, vpv_status_proc_rfb = -3 where vpv_cod_protocolo = :pProtocolo";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    _conn.Open();
                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecute.Parameters.Add(new OracleParameter("pStatusRFB", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pStatusRFB));

                    try
                    {

                        cmdToExecute.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                    }
                }
            }
        }

        public void UpdateRegistroEnviadoS13(string pErroEnvio, string pStatusRFB, string pRowId)
        {
            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                OracleCommand cmdToExecute = new OracleCommand();
                StringBuilder Sql = new StringBuilder();

                cmdToExecute.CommandText = "update PSC_PROTOCOLO_INSTITUICAO set PIP_ENVIA_RFB = :pStatusRFB,  PIP_DATA_ENVIO_RFB = sysdate, PIP_ERRO_ENVIO = :pErroEnvio where rowid = :pRowId";
                cmdToExecute.CommandType = CommandType.Text;
                // Use base class' connection object
                _conn.Open();
                cmdToExecute.Connection = _conn;
                cmdToExecute.Parameters.Add(new OracleParameter("pRowId", OracleType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pRowId));
                cmdToExecute.Parameters.Add(new OracleParameter("pStatusRFB", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pStatusRFB));
                cmdToExecute.Parameters.Add(new OracleParameter("pErroEnvio", OracleType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pErroEnvio));

                try
                {

                    cmdToExecute.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw ex;
                }
                finally
                {
                    _conn.Close();
                    cmdToExecute.Dispose();
                }
            }

        }
        public void UpdateViabilidadeEnviadaReceita(string pProtocolo, string pStatusRFB)
        {
            UpdateViabilidadeEnviadaReceita(pProtocolo, pStatusRFB, "", "");
        }

        public void UpdateViabilidadeEnviadaReceita(string pProtocolo, string pStatusRFB, string pXmlRFB, string pXmlResponseRFB)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction _conn = conn.BeginTransaction())
                    {
                        using (SqlCommand cmdToExecute = new SqlCommand())
                        {
                            cmdToExecute.Connection = _conn.Connection;
                            cmdToExecute.Transaction = _conn;

                            StringBuilder Sql = new StringBuilder();

                            cmdToExecute.CommandText = "update via_protocolo_viab set VPV_STATUS_ENV_RFB = @pStatusRFB,  vpv_data_envio_rfb = getdate(), VPV_ERROR_PROCESSO = NULL, vpv_status_proc_rfb = 3 where vpv_cod_protocolo = @pProtocolo";

                            cmdToExecute.CommandType = CommandType.Text;
                            // Use base class' connection object

                            cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                            cmdToExecute.Parameters.Add(new SqlParameter("pStatusRFB", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pStatusRFB));

                            cmdToExecute.ExecuteNonQuery();

                            if (pXmlRFB != "")
                            {
                                cmdToExecute.Parameters.Clear();
                                cmdToExecute.CommandText = "update VIA_PRO_XMLRFB set VPX_XML_RESPONSE = @pXmlResponseRFB, VPX_XML_ENVIADO = @pXmlRFB,  VPX_STATUS_ENVIO_JUNTA = 1,  VPX_DT_INCLUSAO = getdate() where VPX_COD_PROTOCOLO = @pProtocolo";

                                cmdToExecute.CommandType = CommandType.Text;
                                // Use base class' connection object

                                cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                                cmdToExecute.Parameters.Add(new SqlParameter("pXmlRFB", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pXmlRFB));
                                cmdToExecute.Parameters.Add(new SqlParameter("pXmlResponseRFB", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pXmlResponseRFB));


                                if (cmdToExecute.ExecuteNonQuery() == 0)
                                {
                                    Sql = new StringBuilder();
                                    cmdToExecute.Parameters.Clear();
                                    Sql.AppendLine("INSERT INTO VIA_PRO_XMLRFB (");
                                    Sql.AppendLine("VPX_COD_PROTOCOLO,");
                                    Sql.AppendLine("VPX_DT_INCLUSAO,");
                                    Sql.AppendLine("VPX_STATUS_ENVIO_JUNTA,");
                                    Sql.AppendLine("VPX_XML_ENVIADO,");
                                    Sql.AppendLine("VPX_XML_RESPONSE");
                                    Sql.AppendLine(") ");
                                    Sql.AppendLine("Values ");
                                    Sql.AppendLine("( ");
                                    Sql.AppendLine("@pProtocolo,");
                                    Sql.AppendLine("getdate(),");
                                    Sql.AppendLine("1,");
                                    Sql.AppendLine("@pXmlRFB,");
                                    Sql.AppendLine("@pXmlResponseRFB");
                                    Sql.AppendLine(") ");



                                    cmdToExecute.CommandText = Sql.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    // Use base class' connection object

                                    cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                                    cmdToExecute.Parameters.Add(new SqlParameter("pXmlRFB", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pXmlRFB));
                                    cmdToExecute.Parameters.Add(new SqlParameter("pXmlResponseRFB", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pXmlResponseRFB));
                                    cmdToExecute.ExecuteNonQuery();
                                }
                            }

                        }
                        _conn.Commit();
                    }
                }
            }
            else
            {
                StringBuilder Sql = new StringBuilder();


                using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (OracleTransaction _conn = conn.BeginTransaction())
                    {
                        using (OracleCommand cmdToExecute = new OracleCommand())
                        {
                            cmdToExecute.Connection = _conn.Connection;
                            cmdToExecute.Transaction = _conn;

                            #region Grava Dados da RFB
                            WsServices01RFB.processarViabilidadeRequest requestViab = new WsServices01RFB.processarViabilidadeRequest();
                            requestViab = (WsServices01RFB.processarViabilidadeRequest)GlobalV1.CreateObject(pXmlRFB, requestViab);
                            string format = "yyyyMMdd HHmmssf";
                            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;

                            string dateString = requestViab.dataInicioAnaliseViabilidade + " " + requestViab.horaInicioAnaliseViabilidade;
                            DateTime pdataInicioAnaliseViabilidade = DateTime.ParseExact(dateString, format, provider);

                            dateString = requestViab.dataValidadeViabilidade + " " + "0000000";
                            DateTime pdataValidadeViabilidade = DateTime.ParseExact(dateString, format, provider);

                            dateString = requestViab.dataFimAnaliseViabilidadeNome + " " + requestViab.horaFimAnaliseViabilidadeNome;
                            DateTime pdataFimAnaliseViabilidadeNome = DateTime.ParseExact(dateString, format, provider);

                            dateString = requestViab.dataFimAnaliseViabilidadeEndereco + " " + requestViab.horaFimAnaliseViabilidadeEndereco;
                            DateTime pdataFimAnaliseViabilidadeEndereco = DateTime.ParseExact(dateString, format, provider);

                            cmdToExecute.Connection = _conn.Connection;
                            cmdToExecute.Transaction = _conn;

                            Sql = new StringBuilder();

                            int pPDR_TIPO = 1;

                            Sql.AppendLine("delete rfb_dados_enviados where pdr_protocolo = :v_pdr_protocolo and PDR_TIPO = :v_PDR_TIPO ");
                            cmdToExecute.Parameters.Add(new OracleParameter("v_pdr_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_PDR_TIPO", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pPDR_TIPO));
                            cmdToExecute.CommandText = Sql.ToString();
                            cmdToExecute.CommandType = CommandType.Text;

                            cmdToExecute.ExecuteNonQuery();

                            Sql = new StringBuilder();
                            cmdToExecute.Parameters.Clear();



                            Sql.AppendLine("insert into rfb_dados_enviados ");
                            Sql.AppendLine("  (pdr_protocolo, ");
                            Sql.AppendLine("   PDR_TIPO, ");
                            Sql.AppendLine("   PDR_COD_STATUS, ");
                            Sql.AppendLine("   pdr_dt_inicio, ");
                            Sql.AppendLine("   pdr_dt_analise_nome, ");
                            Sql.AppendLine("   pdr_dt_analise_ende, ");
                            Sql.AppendLine("   pdr_dt_vencimento, ");
                            Sql.AppendLine("   pdr_cod_resul_nome, ");
                            Sql.AppendLine("   pdr_cod_resul_ende ");
                            Sql.AppendLine("   ) ");
                            Sql.AppendLine("values ");
                            Sql.AppendLine("  (:v_pdr_protocolo, ");
                            Sql.AppendLine("   :v_PDR_TIPO, ");
                            Sql.AppendLine("   :v_PDR_COD_STATUS, ");
                            Sql.AppendLine("   :v_pdr_dt_inicio, ");
                            Sql.AppendLine("   :v_pdr_dt_analise_nome, ");
                            Sql.AppendLine("   :v_pdr_dt_analise_ende, ");
                            Sql.AppendLine("   :v_pdr_dt_vencimento, ");
                            Sql.AppendLine("   :v_pdr_cod_resul_nome, ");
                            Sql.AppendLine("   :v_pdr_cod_resul_ende ");
                            Sql.AppendLine("   ) ");

                            cmdToExecute.CommandText = Sql.ToString();
                            cmdToExecute.CommandType = CommandType.Text;
                            // Use base class' connection object

                            cmdToExecute.Parameters.Add(new OracleParameter("v_pdr_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_PDR_TIPO", OracleType.Number, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pPDR_TIPO));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_PDR_COD_STATUS", OracleType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, requestViab.codStatusViabilidade));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_pdr_dt_inicio", OracleType.DateTime, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pdataInicioAnaliseViabilidade));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_pdr_dt_analise_nome", OracleType.DateTime, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pdataFimAnaliseViabilidadeNome));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_pdr_dt_analise_ende", OracleType.DateTime, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pdataFimAnaliseViabilidadeEndereco));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_pdr_dt_vencimento", OracleType.DateTime, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pdataValidadeViabilidade));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_pdr_cod_resul_nome", OracleType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, requestViab.resultadoViabilidadeNome));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_pdr_cod_resul_ende", OracleType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, requestViab.resultadoViabilidadeEndereco));
                            cmdToExecute.ExecuteNonQuery();

                            #endregion


                            Sql = new StringBuilder();
                            cmdToExecute.Parameters.Clear();
                            cmdToExecute.CommandText = "update via_protocolo_viab set VPV_STATUS_ENV_RFB = :pStatusRFB,  vpv_data_envio_rfb = sysdate, VPV_ERROR_PROCESSO = NULL, vpv_status_proc_rfb = 3 where vpv_cod_protocolo = :pProtocolo";

                            cmdToExecute.CommandType = CommandType.Text;
                            // Use base class' connection object

                            cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                            cmdToExecute.Parameters.Add(new OracleParameter("pStatusRFB", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pStatusRFB));

                            cmdToExecute.ExecuteNonQuery();

                            if (pXmlRFB != "" && pProtocolo.Substring(2, 1) == "B")
                            {
                                cmdToExecute.Parameters.Clear();
                                cmdToExecute.CommandText = "update VIA_PRO_XMLRFB set VPX_DT_INCLUSAO = sysdate, VPX_XML_ENVIADO = :pXmlRFB where VPX_COD_PROTOCOLO = :pProtocolo";

                                cmdToExecute.CommandType = CommandType.Text;
                                // Use base class' connection object

                                cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                                cmdToExecute.Parameters.Add(new OracleParameter("pXmlRFB", OracleType.Clob, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pXmlRFB));
                                //cmdToExecute.Parameters.Add(new SqlParameter("pXmlResponseRFB", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pXmlResponseRFB));


                                if (cmdToExecute.ExecuteNonQuery() == 0)
                                {
                                    Sql = new StringBuilder();
                                    cmdToExecute.Parameters.Clear();
                                    Sql.AppendLine("INSERT INTO VIA_PRO_XMLRFB (");
                                    Sql.AppendLine("VPX_COD_PROTOCOLO,");
                                    Sql.AppendLine("VPX_DT_INCLUSAO,");
                                    Sql.AppendLine("VPX_XML_ENVIADO");
                                    Sql.AppendLine(") ");
                                    Sql.AppendLine("Values ");
                                    Sql.AppendLine("( ");
                                    Sql.AppendLine(":pProtocolo,");
                                    Sql.AppendLine("sysdate,");
                                    Sql.AppendLine(":pXmlRFB");
                                    Sql.AppendLine(") ");



                                    cmdToExecute.CommandText = Sql.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    // Use base class' connection object

                                    cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                                    cmdToExecute.Parameters.Add(new OracleParameter("pXmlRFB", OracleType.Clob, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pXmlRFB));
                                    //  cmdToExecute.Parameters.Add(new SqlParameter("pXmlResponseRFB", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pXmlResponseRFB));
                                    cmdToExecute.ExecuteNonQuery();
                                }
                            }

                        }
                        _conn.Commit();
                    }
                }
            }
        }

        public static DataTable BuscaDadosViabilidadeEvento(string pProtocolo)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    SqlCommand cmdToExecute = new SqlCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("DadosViabilidadeEvento");

                    cmdToExecute.CommandText = "select PEV_COD_EVENTO CodEvento from psc_prot_evento_rfb where pev_pro_protocolo = @pProtocolo";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;

                    cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));

                    SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
            else
            {
                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    OracleCommand cmdToExecute = new OracleCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("DadosViabilidadeEvento");
                    //not in (1052) e para nao pegar o evento de REATIVAÇÃO DA JUNTA COMERCIAL feito dia 26/11/2019 pelo Raul
                    //crie um campo na mac_evento para distinguir se esse evento e de RFB, mas como nao deu para atualizar
                    //todos os ambientes ainda e tenho que atualizar este site, fiz assim mesmo ate ter todos os ambientes atualizados
                    cmdToExecute.CommandText = "select PEV_COD_EVENTO CodEvento from psc_prot_evento_rfb where pev_cod_evento not in (1052) and pev_pro_protocolo = :pProtocolo";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;

                    cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));

                    OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
        }

        public static DataTable BuscaDadosViabilidadeQsa(string pProtocolo)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    SqlCommand cmdToExecute = new SqlCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("DadosViabilidadeQSA");

                    cmdToExecute.CommandText = "select top 10 s.vps_cpf_cnpj_socio CPF, s.vps_nome_socio Nome from via_prot_socios s where len(ltrim(rtrim(s.vps_cpf_cnpj_socio))) = 11 and s.vpv_cod_protocolo = @pProtocolo";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;

                    cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));

                    SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
            else
            {
                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    OracleCommand cmdToExecute = new OracleCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("DadosViabilidadeQSA");

                    cmdToExecute.CommandText = "select s.vps_cpf_cnpj_socio CPF, s.vps_nome_socio Nome from via_prot_socios s where rownum < 11 and length(trim(s.vps_cpf_cnpj_socio)) = 11 and s.vpv_cod_protocolo = :pProtocolo";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;

                    cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));

                    OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
        }

        public static DataTable BuscaDadosViabilidadeCnaeSecundaria(string pProtocolo)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    SqlCommand cmdToExecute = new SqlCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("DadosViabilidadeCnae");

                    cmdToExecute.CommandText = "select s.vpc_tae_cod_actvd CodCnae from via_prot_cnae s where s.vpv_tip_cnae = 2 and s.vpc_cod_protocolo = @pProtocolo";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;

                    cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));

                    SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
            else
            {
                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    OracleCommand cmdToExecute = new OracleCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("DadosViabilidadeCnae");

                    cmdToExecute.CommandText = "select s.vpc_tae_cod_actvd CodCnae from via_prot_cnae s where s.vpv_tip_cnae = 2 and s.vpc_cod_protocolo = :pProtocolo";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;

                    cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));

                    OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
        }

        public static int BuscaDadosTipoComplemento(string pComplemento)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    _conn.Open();
                    using (SqlCommand cmdToExecute = new SqlCommand())
                    {
                        StringBuilder Sql = new StringBuilder();
                        cmdToExecute.CommandText = "select count(*) from TAB_COMPL_ENDERECO where TCE_CODIGO = @Complemento";
                        cmdToExecute.CommandType = CommandType.Text;
                        // Use base class' connection object
                        cmdToExecute.Connection = _conn;

                        cmdToExecute.Parameters.Add(new SqlParameter("Complemento", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pComplemento));


                        return (int)cmdToExecute.ExecuteScalar();

                    }
                }
            }
            else
            {
                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    using (OracleCommand cmdToExecute = new OracleCommand())
                    {
                        _conn.Open();
                        StringBuilder Sql = new StringBuilder();

                        cmdToExecute.CommandText = "select count(*) from TAB_COMPL_ENDERECO where upper(trim(TCE_CODIGO)) = upper(:Complemento)";
                        cmdToExecute.CommandType = CommandType.Text;
                        // Use base class' connection object
                        cmdToExecute.Connection = _conn;

                        cmdToExecute.Parameters.Add(new OracleParameter("Complemento", OracleType.VarChar, 50, ParameterDirection.Input, true, 50, 0, "", DataRowVersion.Proposed, pComplemento.Trim().ToUpper()));

                        object Resp = cmdToExecute.ExecuteScalar();

                        return int.Parse(Resp.ToString());

                        //return (int)cmdToExecute.ExecuteScalar();
                    }
                }
            }
        }

        public static DataTable BuscaDadosViabilidade(string pProtocolo)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    SqlCommand cmdToExecute = new SqlCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("DadosViabilidade");

                    cmdToExecute.CommandText = "RFBPegaInformacaoViabilid";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;

                    cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                    //cmdToExecute.Parameters.Add(new SqlParameter("pCursor", OracleType.Cursor, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, toReturn));

                    SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
            else
            {
                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    OracleCommand cmdToExecute = new OracleCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("DadosViabilidade");

                    cmdToExecute.CommandText = "RFBPegaInformacaoViabilid";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;

                    cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, toReturn));

                    OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);


                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
        }

        public static string RetiraTipoEnquadramento(string wTexto)
        {
            wTexto = wTexto.Trim();

            if (!wTexto.Contains(" "))
            {
                return wTexto;
            }

            string wAux = wTexto + " ";
            string wEnquadramento = string.Empty;

            for (int wii = 0; wii <= tbEnquadramento.Length - 1; wii++)
            {
                if (wEnquadramento.Length < wAux.Length)
                {
                    wEnquadramento = tbEnquadramento[wii];
                    int ii = wAux.IndexOf(tbEnquadramento[wii], wAux.Length - tbEnquadramento[wii].Length);
                    if (ii != -1)
                    {
                        wTexto = wTexto.Substring(0, wTexto.Length - tbEnquadramento[wii].Length + 1);
                        break;
                    }
                }
            }

            return wTexto;
        }


        protected static string[] tbEnquadramento = new string[13] { " EPP ", " - ME ", "- ME ", " -ME ", "-ME ", " ME ", " ME. ", " - EPP", "- EPP ", " -EPP ", "-EPP ", " EPP. ", "-EPP" };


        public static DataTable BuscarTipoLogradouro()
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    SqlCommand cmdToExecute = new SqlCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("TIPODELOGRADOURO");

                    cmdToExecute.CommandText += "SELECT cod cod_rfb, descricao cod_regin from rfb_tipo_logradouro ";
                    cmdToExecute.CommandText += "Union all ";
                    cmdToExecute.CommandText = "select * from rfb_tiplogr_regin_rfb t ";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;

                    //cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                    //cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, toReturn));

                    SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
            else
            {
                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    OracleCommand cmdToExecute = new OracleCommand();
                    StringBuilder Sql = new StringBuilder();
                    DataTable toReturn = new DataTable("TIPODELOGRADOURO");

                    cmdToExecute.CommandText = "select * from rfb_tiplogr_regin_rfb t ";
                    cmdToExecute.CommandText += "Union ";
                    cmdToExecute.CommandText += "SELECT cod cod_rfb, descricao cod_regin from rfb_tipo_logradouro ";
                    cmdToExecute.CommandText += "Union ";
                    cmdToExecute.CommandText += "SELECT cod cod_rfb, cod cod_regin from rfb_tipo_logradouro ";
                    cmdToExecute.CommandType = CommandType.Text;
                    // Use base class' connection object
                    cmdToExecute.Connection = _conn;

                    //cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                    //cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, toReturn));

                    OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                    try
                    {
                        adapter.Fill(toReturn);
                        return toReturn;
                    }
                    catch (Exception ex)
                    {
                        // some error occured. Bubble it to caller and encapsulate Exception object
                        throw ex;
                    }
                    finally
                    {
                        _conn.Close();
                        cmdToExecute.Dispose();
                        adapter.Dispose();
                    }
                }
            }
        }


        #endregion

        #region Valida Request.Headers
        public static void ValidaReques()
        {
            string pAutentication = "";
            string pcnpjInstituicao = "";
            string pUfEstado = "";
            try
            {
                pAutentication = HttpContext.Current.Request.Headers["Autentication"].ToString();
            }
            catch { }

            if (pAutentication == "" || pAutentication != "23fsf45435bhgfg.drte54gm.Pscs")
            {
                throw new Exception("Codigo da Autentication do Header Inválido:" + pAutentication);
            }

            try
            {
                pcnpjInstituicao = HttpContext.Current.Request.Headers["CnpjInstituicao"].ToString().Trim(); ;
            }
            catch { }

            if (pcnpjInstituicao == "" || pcnpjInstituicao.Length != 14)
            {
                throw new Exception("Codigo do CnpjInstituicao do Header Inválido");
            }

            try
            {
                pUfEstado = HttpContext.Current.Request.Headers["pUfEstado"].ToString().Trim(); ;
            }
            catch { }

            if (pUfEstado == "" || pUfEstado.Length != 2)
            {
                throw new Exception("Codigo do pUfEstado do Header Inválido:" + pUfEstado);
            }
        }


        #endregion


        #region fazer XML central de carga de processos de filial de fora da UF (S07)
        public RetornoBasico GeraXMLServico07RFB(string Recibo, string Identificacao)
        {
            RetornoBasico pResult = new RetornoBasico();
            try
            {
                pResult.status = "OK";
                pResult.codretorno = "00";
                pResult.XmlDBE = "";

                //            [21:16, 11/10/2019] Marcia Cel: Então vai ser sempre ato=310 e evento= 029(abertura), 030(alteração) ou 031 (extinção). Os 3 de filial sede fora.
                //[21:18, 11/10/2019] Marcia Cel: Obrigada. Se precisar de alguma rotina, avisa. Pra gerar essas tabelas de processo e solicitação. Depois diz o que ficou decidido


                WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();

                WsServicesReginRFB.RetornoV2 resulRegin = new WsServicesReginRFB.RetornoV2();

                regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();


                resulRegin = regin.ServiceWs07(Identificacao, Recibo);

                //resulRegin = regin.ServiceWs35Regin(Identificacao, Recibo);

                if (resulRegin.status == "OK")
                {


                    WsServices07RFB.serviceResponse ws07 = new WsServices07RFB.serviceResponse();
                    ws07 = (WsServices07RFB.serviceResponse)GlobalV1.CreateObject(resulRegin.XmlDBE, ws07);
                    WsServices07RFB.redesim dados = new WsServices07RFB.redesim();
                    WsServices07RFB.dadosCNPJ dMatriz = new WsServices07RFB.dadosCNPJ();
                    WsServices07RFB.endereco ender = new WsServices07RFB.endereco();

                    WsServices07RFB.dadosCNPJ dFilial = new WsServices07RFB.dadosCNPJ();
                    WsServices07RFB.endereco enderFilial = new WsServices07RFB.endereco();


                    dados = ws07.dadosRedesim;
                    dMatriz = ws07.dadosRedesim.dadosCNPJ[1];
                    ender = dMatriz.enderecoDadosCNPJ;
                    dFilial = dados.dadosCNPJ[0];
                    enderFilial = dFilial.enderecoDadosCNPJ;

                    string pXml = "";


                    string pEventoJunta = "030";  //Alteração
                    string pEvento = "3";
                    //Verifica evento da Junta
                    if (dados.fcpj.codEvento != null)
                    {
                        foreach (string evento in dados.fcpj.codEvento)
                        {
                            if (evento == "")
                            {
                                break;
                            }

                            if (evento == "102")
                            {
                                pEventoJunta = "029";
                                pEvento = "1";
                            }

                            if (evento == "517")
                            {
                                pEventoJunta = "031";
                                pEvento = "5";
                            }

                            if (evento == "210")
                            {
                                pEventoJunta = "037";
                                pEvento = "4";
                            }
                        }
                    }


                    pXml += fncCocatXmltag("coleta", "", "A");
                    pXml += fncCocatXmltag("dadosGerais", "", "A");

                    pXml += fncCocatXmltag("cnpjOrgaoRegistro", ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString(), "AF");//</cnpjOrgaoRegistro>";
                    pXml += fncCocatXmltag("dataProtocolo", dados.fcpj.dataEvento[0].ToString(), "AF");
                    pXml += fncCocatXmltag("uf", enderFilial.uf, "AF");
                    pXml += fncCocatXmltag("ProtocoloOR", "FALTAAAPROTOCOLO", "AF");
                    pXml += fncCocatXmltag("ProtocoloViabilidade", "", "AF");
                    pXml += fncCocatXmltag("NrDbe", "", "AF");
                    pXml += fncCocatXmltag("CnpjEmpresa", dFilial.cnpjMatriz, "AF");
                    pXml += fncCocatXmltag("Versao", "2", "AF");
                    pXml += fncCocatXmltag("ProtEnquadra", "", "AF");
                    pXml += fncCocatXmltag("AtoPrincipal", "310", "AF"); //Falta verificar
                    pXml += fncCocatXmltag("TipoDocREQ", "1", "AF");
                    pXml += fncCocatXmltag("NumeroUnico", "", "AF");
                    pXml += fncCocatXmltag("qtdEmpresas", "1", "AF");
                    pXml += fncCocatXmltag("qtdFiliaisNoEstado", "1", "AF");
                    pXml += fncCocatXmltag("qtdFiliaisForaDoEstado", "0", "AF");

                    pXml += fncCocatXmltag("dadosGerais", "", "F");

                    pXml += fncCocatXmltag("GrupoEvento", "", "A");
                    pXml += fncCocatXmltag("evento", "102", "AF");
                    pXml += fncCocatXmltag("GrupoEvento", "", "F");

                    pXml += fncCocatXmltag("GrupoEventoJunta", "", "A");
                    pXml += fncCocatXmltag("evento", pEventoJunta, "AF");
                    pXml += fncCocatXmltag("GrupoEventoJunta", "", "F");


                    pXml += fncCocatXmltag("IdentificacaoEmpresa", "", "A");

                    string NaturezaJuridica = dMatriz.naturezaJuridica.Substring(0, 3) + "-" + dMatriz.naturezaJuridica.Substring(3, 1);
                    pXml += fncCocatXmltag("NireMatricula", dMatriz.numeroOrgaoRegistro, "AF");
                    pXml += fncCocatXmltag("nomeEmpresarial", dMatriz.nomeEmpresarial, "AF");
                    pXml += fncCocatXmltag("nomeFantasia", "", "AF");
                    pXml += fncCocatXmltag("naturezaJuridica", NaturezaJuridica, "AF");
                    pXml += fncCocatXmltag("dataInicioAtividade", dMatriz.dataAberturaEmpresa, "AF");
                    pXml += fncCocatXmltag("dataAssinatura", dMatriz.dataAberturaEmpresa, "AF");
                    pXml += fncCocatXmltag("capitalSocial", dMatriz.capitalSocial, "AF");
                    pXml += fncCocatXmltag("indicaIntegralizaCapital", "2", "AF");
                    pXml += fncCocatXmltag("capitalIntegralizado", "0", "AF");

                    string pPorte = "NO";
                    if (dMatriz.porte != null)
                    {
                        if (dMatriz.porte == "01") //ME
                        {
                            pPorte = "ME";
                        }
                        if (dMatriz.porte == "03")//EPP
                        {
                            pPorte = "EPP";
                        }
                    }


                    pXml += fncCocatXmltag("porte", pPorte, "AF"); //Verificar
                    pXml += fncCocatXmltag("ddd", "", "AF");
                    pXml += fncCocatXmltag("telefone1", "", "AF");
                    pXml += fncCocatXmltag("email", "", "AF");
                    pXml += fncCocatXmltag("NatJurAnterior", NaturezaJuridica, "AF");
                    pXml += fncCocatXmltag("telefone1", "", "AF");

                    pXml += fncCocatXmltag("IdentificacaoEmpresa", "", "F");

                    pXml += fncCocatXmltag("EnderecoEmpresa", "", "A");

                    pXml += fncCocatXmltag("tipoLogradouro", ender.codTipoLogradouro, "AF");
                    pXml += fncCocatXmltag("logradouro", ender.logradouro, "AF");
                    pXml += fncCocatXmltag("numero", ender.numLogradouro, "AF");
                    pXml += fncCocatXmltag("complemento", ender.complementoLogradouro, "AF");
                    pXml += fncCocatXmltag("bairro", ender.bairro, "AF");
                    pXml += fncCocatXmltag("cep", ender.cep, "AF");

                    string codMunic = ender.codMunicipio;

                    codMunic += psc.Framework.General.CalculateVerificationDigit(codMunic, 11).ToString();
                    pXml += fncCocatXmltag("CodMunicipio", codMunic, "AF");

                    pXml += fncCocatXmltag("uf", ender.uf, "AF");
                    pXml += fncCocatXmltag("codPais", GlobalV1.valNulo(ender.codPais) ? "105" : ender.codPais, "AF");



                    pXml += fncCocatXmltag("EnderecoEmpresa", "", "F");


                    pXml += fncCocatXmltag("AtuacaoEmpresa", "", "A");

                    pXml += fncCocatXmltag("objetoSocial", dMatriz.objetoSocial, "AF");
                    pXml += fncCocatXmltag("cnaePrincipal", dMatriz.cnaePrincipal, "AF");

                    pXml += fncCocatXmltag("cnaeSecundaria", "", "A");
                    if (dMatriz.cnaeSecundaria != null)
                    {
                        foreach (string CodCnae in dMatriz.cnaeSecundaria)
                        {
                            if (CodCnae.Length < 5)
                            {
                                break;
                            }

                            pXml += fncCocatXmltag("valor", CodCnae, "AF");
                        }
                    }
                    pXml += fncCocatXmltag("cnaeSecundaria", "", "F");

                    pXml += fncCocatXmltag("AtuacaoEmpresa", "", "F");




                    string pNumeroUnico = dados.numViabilidadeAssociada;



                    pXml += fncCocatXmltag("GrupoFilial", "", "A");
                    pXml += fncCocatXmltag("Filial", "", "A");





                    pXml += fncCocatXmltag("evento", pEvento, "AF"); //falta Verificar

                    pXml += fncCocatXmltag("GrupoEventoFilialRFB", "", "A");

                    if (dados.fcpj.codEvento != null)
                    {
                        foreach (string evento in dados.fcpj.codEvento)
                        {
                            if (evento == "")
                            {
                                break;
                            }

                            pXml += fncCocatXmltag("evento", evento, "AF");
                        }
                    }
                    pXml += fncCocatXmltag("GrupoEventoFilialRFB", "", "F");



                    pXml += fncCocatXmltag("eventoJunta", pEventoJunta, "AF"); // vERIFICAR FALTA

                    pXml += fncCocatXmltag("dataEvento", "", "AF");
                    pXml += fncCocatXmltag("nome", dMatriz.nomeEmpresarial, "AF");
                    string NireFilial = dFilial.numeroOrgaoRegistro;

                    if (NireFilial == "")
                    {
                        NireFilial = dados.fcpj.nire;
                    }

                    pXml += fncCocatXmltag("nire", NireFilial, "AF");
                    pXml += fncCocatXmltag("cnpj", dFilial.cnpj, "AF");
                    pXml += fncCocatXmltag("nrDbeFilial", Recibo + Identificacao, "AF");

                    pXml += fncCocatXmltag("qualificacao", "903", "AF"); //Filial de Sede fora

                    pXml += fncCocatXmltag("tipoLogradouro", enderFilial.codTipoLogradouro, "AF");
                    pXml += fncCocatXmltag("logradouro", enderFilial.logradouro, "AF");
                    pXml += fncCocatXmltag("numero", enderFilial.numLogradouro, "AF");
                    pXml += fncCocatXmltag("complemento", enderFilial.complementoLogradouro, "AF");
                    pXml += fncCocatXmltag("bairro", enderFilial.bairro, "AF");
                    pXml += fncCocatXmltag("cep", enderFilial.cep, "AF");

                    string codMuniFilia = enderFilial.codMunicipio;
                    codMuniFilia += psc.Framework.General.CalculateVerificationDigit(codMuniFilia, 11).ToString();
                    pXml += fncCocatXmltag("CodMunicipio", codMuniFilia, "AF");


                    pXml += fncCocatXmltag("uf", enderFilial.uf, "AF");
                    pXml += fncCocatXmltag("codPais", GlobalV1.valNulo(enderFilial.codPais) ? "105" : ender.codPais, "AF");

                    if (dados.fcpj.contato != null)
                    {
                        WsServices07RFB.contato contato = new WsServices07RFB.contato();
                        contato = dados.fcpj.contato;

                        pXml += fncCocatXmltag("ddd", GlobalV1.valNulo(contato.dddTelefone1) ? "" : contato.dddTelefone1, "AF");
                        pXml += fncCocatXmltag("telefone1", GlobalV1.valNulo(contato.telefone1) ? "" : contato.telefone1, "AF");
                        pXml += fncCocatXmltag("telefone2", GlobalV1.valNulo(contato.telefone2) ? "" : contato.telefone2, "AF");
                        pXml += fncCocatXmltag("email", GlobalV1.valNulo(contato.correioEletronico) ? "" : contato.correioEletronico, "AF");
                    }

                    pXml += fncCocatXmltag("viabilidade", dados.numViabilidadeAssociada, "AF");
                    pXml += fncCocatXmltag("numerounico", pNumeroUnico, "AF"); //falta Verificar
                    pXml += fncCocatXmltag("Capital", "0", "AF");

                    pXml += fncCocatXmltag("GrupoDestacado", "", "A");

                    pXml += fncCocatXmltag("Capital", "0", "AF");
                    pXml += fncCocatXmltag("GrupoCnae", "", "A");

                    pXml += fncCocatXmltag("valor", dFilial.cnaePrincipal, "AF");

                    if (dFilial.cnaeSecundaria != null)
                    {
                        foreach (string CodCnae in dFilial.cnaeSecundaria)
                        {
                            if (CodCnae.Length < 5)
                            {
                                break;
                            }

                            pXml += fncCocatXmltag("valor", CodCnae, "AF");
                        }
                    }

                    pXml += fncCocatXmltag("GrupoCnae", "", "F");

                    string pObjetoSocialFilial = dFilial.objetoSocial;

                    if (pObjetoSocialFilial == null || pObjetoSocialFilial == "")
                    {
                        pObjetoSocialFilial = dados.atividadeEconomica.objetoSocial;
                    }

                    pXml += fncCocatXmltag("objetoSocFilial", pObjetoSocialFilial, "AF");



                    pXml += fncCocatXmltag("GrupoDestacado", "", "F");


                    pXml += fncCocatXmltag("Filial", "", "F");
                    pXml += fncCocatXmltag("GrupoFilial", "", "F");



                    pXml += fncCocatXmltag("coleta", "", "F");

                    pResult.XmlDBE = pXml;

                }
                else
                {
                    pResult.status = "NOK";
                    pResult.codretorno = resulRegin.codretorno;
                    pResult.descricao = resulRegin.descricao;

                }

            }
            catch (Exception ex)
            {
                pResult.status = "NOK";
                pResult.codretorno = "99";
                pResult.descricao = ex.Message;
            }

            return pResult;
        }
        private string fncCocatXmltag(string pTag, string pValor, string pTipo)
        {
            StringBuilder pResul = new StringBuilder();
            pTipo = pTipo.ToUpper();

            if (pTipo == "A" || pTipo == "AF")
                pResul.Append("<" + pTag + ">");

            if (pValor != "")
            {
                //    pResul.Append("" + pValor + "");
                pResul.Append("<![CDATA[" + pValor + "]]>");
            }

            if (pTipo == "F" || pTipo == "AF")
                pResul.Append("</" + pTag + ">");

            //string pResul  = "<" + pTag + "><![CDATA[" + pValor + "]]></" + pTag + ">";

            return pResul.ToString(); ;
        }

        #endregion

        #region fazer XML central de carga de processos de Matriz (alteração nome, natureza, etc) de fora da UF (S17)
        public RetornoBasico GeraXMLServico17RFB(string UfRegistroTabelaRFB, string cnpjEmpresa, string Recibo, string Identificacao, string numeroAtoOficio, string convenioAtoOficio)
        {
            RetornoBasico pResult = new RetornoBasico();
            try
            {
                pResult.status = "OK";
                pResult.codretorno = "00";
                pResult.XmlDBE = "";

                //[21:16, 11/10/2019] Marcia Cel: Então vai ser sempre ato=310 e evento= 029(abertura), 030(alteração) ou 031 (extinção). Os 3 de filial sede fora.
                //[21:18, 11/10/2019] Marcia Cel: Obrigada. Se precisar de alguma rotina, avisa. Pra gerar essas tabelas de processo e solicitação. Depois diz o que ficou decidido


                WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();

                WsServicesReginRFB.RetornoV2 resulRegin = new WsServicesReginRFB.RetornoV2();

                regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();


                resulRegin = regin.ServiceWs17(Identificacao, Recibo, numeroAtoOficio, convenioAtoOficio);

                //resulRegin = regin.ServiceWs35Regin(Identificacao, Recibo);

                if (resulRegin.status == "OK")
                {


                    WsServices17RFB.serviceResponse ws17 = new WsServices17RFB.serviceResponse();
                    ws17 = (WsServices17RFB.serviceResponse)GlobalV1.CreateObject(resulRegin.XmlDBE, ws17);
                    WsServices17RFB.redesim dados = new WsServices17RFB.redesim();
                    WsServices17RFB.dadosCNPJ dMatriz = new WsServices17RFB.dadosCNPJ();
                    WsServices17RFB.endereco ender = new WsServices17RFB.endereco();

                    //  string xmlSimples = GlobalV1.CreateXML(ws17.dadosRedesim.simplesNacional);

                    dados = ws17.dadosRedesim;
                    dMatriz = ws17.dadosRedesim.dadosCNPJ[0];
                    ender = dMatriz.enderecoDadosCNPJ;

                    //WsServices17RFB.simplesNacional xmlSimples =  

                    #region Valida se registro e de alteração de matriz com filial dento
                    //Valida se e registro de Matriz fora de enstado alterando nome, natureza, porto ou baixa
                    if (dMatriz.indMatrizFilial != "1") //Nao e matriz
                    {
                        pResult.status = "OK";
                        pResult.codretorno = "01"; // Nao e matriz 
                        pResult.XmlDBE = "";
                        return pResult;
                    }

                    if (dMatriz.enderecoDadosCNPJ.uf == UfRegistroTabelaRFB) //A matriz e no estado, por isso nao se marca
                    {
                        pResult.status = "OK";
                        pResult.codretorno = "01"; // Nao e matriz 
                        pResult.XmlDBE = "";
                        return pResult;
                    }
                    //Valida se tem eventos indicados 220, 225, 222, 517

                    bool pPosueEventoMatriz = false;
                    if (dados.fcpj != null && dados.fcpj.codEvento != null)
                    {
                        foreach (string evento in dados.fcpj.codEvento)
                        {
                            if (evento == "")
                            {
                                break;
                            }

                            if (evento == "220" || evento == "225" || evento == "222" || evento == "517")
                            {
                                pPosueEventoMatriz = true;
                                break;
                            }
                        }
                    }

                    if (!pPosueEventoMatriz)
                    {
                        pResult.status = "OK";
                        pResult.codretorno = "02"; // Posue Evento de alteração de matiriz
                        pResult.XmlDBE = "";
                        return pResult;
                    }

                    //Valida se tem pelo menos uma ilial da UF, porque caso nao tenha nao coloco, porque pode ser um registro de marcado por exemplo
                    bool pPosueUfDeFilialnoEstado = false;
                    if (dMatriz.cnpjFilial != null)
                    {

                        for (int i = 0; i < dMatriz.cnpjFilial.Length; i++)
                        {
                            string ufFilial = "";
                            ufFilial = dMatriz.ufFilial[i].ToString();

                            if (ufFilial == UfRegistroTabelaRFB)
                            {
                                pPosueUfDeFilialnoEstado = true;
                                break;
                            }
                        }
                    }

                    if (!pPosueUfDeFilialnoEstado)
                    {
                        pResult.status = "OK";
                        pResult.codretorno = "04"; // Nao Posue uf de filial no estado
                        pResult.XmlDBE = "";
                        return pResult;
                    }


                    #endregion

                    string pXml = "";


                    string pEventoJunta = "022";  //ALTERACAO DE DADOS E DE NOME EMPRESARIAL
                    bool alteracaoNome = false;
                    bool alteracaoOutros = false;
                    bool alteracaoTransformacao = false;
                    if (dados.fcpj.codEvento != null)
                    {
                        foreach (string evento in dados.fcpj.codEvento)
                        {
                            if (evento == "")
                            {
                                break;
                            }

                            if (evento == "220")
                            {
                                alteracaoNome = true;
                            }

                            if (evento == "225")
                            {
                                alteracaoTransformacao = true;
                            }

                            if (evento == "222")
                            {
                                alteracaoOutros = true;
                            }

                            if (evento == "517")
                            {
                                pEventoJunta = "003"; // EXTINÇÃO/DISTRATO/DESCONSTITUIÇÃO
                            }
                        }
                    }

                    if (!alteracaoNome && alteracaoOutros)
                    {
                        pEventoJunta = "021"; // ALTERACAO DE DADOS (EXCETO NOME EMPRESARIAL)
                    }

                    if (alteracaoNome && !alteracaoOutros)
                    {
                        pEventoJunta = "020"; // ALTERACAO DE NOME EMPRESARIAL
                    }

                    //Aqui e porque se tem tranformação, indeferetemente se esta alterando nome ou outra coisa, se coloca o 046
                    if (alteracaoTransformacao)
                    {
                        pEventoJunta = "030"; // ALTERACAO DE FILIAL COM SEDE EM OUTRA UF
                    }

                    pXml += fncCocatXmltag("coleta", "", "A");
                    pXml += fncCocatXmltag("dadosGerais", "", "A");

                    pXml += fncCocatXmltag("cnpjOrgaoRegistro", ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString(), "AF");//</cnpjOrgaoRegistro>";
                    pXml += fncCocatXmltag("dataProtocolo", dados.fcpj.dataEvento[0].ToString(), "AF");
                    pXml += fncCocatXmltag("uf", UfRegistroTabelaRFB, "AF");
                    pXml += fncCocatXmltag("ProtocoloOR", "FALTAAAPROTOCOLO", "AF");

                    string numViabiliade = GlobalV1.valNuloBranco(dados.numViabilidadeAssociada);
                    //aqui e porque nao e vibilidade o numero que vem da RFB com isso gravo branco
                    if (numViabiliade.Length > 5 && numViabiliade.Substring(2, 1) != "P")
                    {
                        numViabiliade = "";
                    }

                    pXml += fncCocatXmltag("ProtocoloViabilidade", numViabiliade, "AF");
                    pXml += fncCocatXmltag("NrDbe", Recibo + Identificacao, "AF");
                    pXml += fncCocatXmltag("CnpjEmpresa", dados.cnpj, "AF");
                    pXml += fncCocatXmltag("Versao", "2", "AF");
                    pXml += fncCocatXmltag("ProtEnquadra", "", "AF");
                    pXml += fncCocatXmltag("AtoPrincipal", "310", "AF"); //Falta verificar
                    pXml += fncCocatXmltag("TipoDocREQ", "1", "AF");
                    pXml += fncCocatXmltag("NumeroUnico", dados.numViabilidadeAssociada, "AF");
                    pXml += fncCocatXmltag("qtdEmpresas", "1", "AF");
                    pXml += fncCocatXmltag("qtdFiliaisNoEstado", "1", "AF");//Falta verificar
                    pXml += fncCocatXmltag("qtdFiliaisForaDoEstado", "0", "AF");//Falta verificar
                    pXml += fncCocatXmltag("inAlteracaoSedeFora", "1", "AF");

                    pXml += fncCocatXmltag("dadosGerais", "", "F");

                    pXml += fncCocatXmltag("GrupoEvento", "", "A");

                    if (dados.fcpj.codEvento != null)
                    {
                        foreach (string evento in dados.fcpj.codEvento)
                        {
                            if (evento == "")
                            {
                                break;
                            }

                            pXml += fncCocatXmltag("evento", evento, "AF");
                        }
                    }
                    pXml += fncCocatXmltag("GrupoEvento", "", "F");

                    pXml += fncCocatXmltag("GrupoEventoJunta", "", "A");
                    pXml += fncCocatXmltag("evento", pEventoJunta, "AF");
                    pXml += fncCocatXmltag("GrupoEventoJunta", "", "F");


                    pXml += fncCocatXmltag("IdentificacaoEmpresa", "", "A");

                    string NaturezaJuridica = dMatriz.naturezaJuridica.Substring(0, 3) + "-" + dMatriz.naturezaJuridica.Substring(3, 1);
                    pXml += fncCocatXmltag("NireMatricula", dMatriz.numeroOrgaoRegistro, "AF");
                    pXml += fncCocatXmltag("nomeEmpresarial", dMatriz.nomeEmpresarial, "AF");
                    pXml += fncCocatXmltag("nomeFantasia", "", "AF");
                    pXml += fncCocatXmltag("naturezaJuridica", NaturezaJuridica, "AF");
                    pXml += fncCocatXmltag("dataInicioAtividade", dMatriz.dataAberturaEmpresa, "AF");
                    pXml += fncCocatXmltag("dataAssinatura", dMatriz.dataAberturaEmpresa, "AF");
                    pXml += fncCocatXmltag("capitalSocial", dMatriz.capitalSocial, "AF");
                    pXml += fncCocatXmltag("indicaIntegralizaCapital", "2", "AF");
                    pXml += fncCocatXmltag("capitalIntegralizado", "0", "AF");

                    string pPorte = "NO";
                    if (dMatriz.porte != null)
                    {
                        if (dMatriz.porte == "01") //ME
                        {
                            pPorte = "ME";
                        }
                        if (dMatriz.porte == "03")//EPP
                        {
                            pPorte = "EPP";
                        }
                    }


                    pXml += fncCocatXmltag("porte", pPorte, "AF"); //Verificar
                    pXml += fncCocatXmltag("ddd", "", "AF");
                    pXml += fncCocatXmltag("telefone1", "", "AF");
                    pXml += fncCocatXmltag("email", "", "AF");
                    pXml += fncCocatXmltag("NatJurAnterior", NaturezaJuridica, "AF");
                    pXml += fncCocatXmltag("telefone1", "", "AF");

                    pXml += fncCocatXmltag("IdentificacaoEmpresa", "", "F");

                    pXml += fncCocatXmltag("EnderecoEmpresa", "", "A");

                    pXml += fncCocatXmltag("tipoLogradouro", ender.codTipoLogradouro, "AF");
                    pXml += fncCocatXmltag("logradouro", ender.logradouro, "AF");
                    pXml += fncCocatXmltag("numero", ender.numLogradouro, "AF");
                    pXml += fncCocatXmltag("complemento", ender.complementoLogradouro, "AF");
                    pXml += fncCocatXmltag("bairro", ender.bairro, "AF");
                    pXml += fncCocatXmltag("cep", ender.cep, "AF");

                    string codMunic = ender.codMunicipio;

                    codMunic += psc.Framework.General.CalculateVerificationDigit(codMunic, 11).ToString();
                    pXml += fncCocatXmltag("CodMunicipio", codMunic, "AF");

                    pXml += fncCocatXmltag("uf", ender.uf, "AF");
                    pXml += fncCocatXmltag("codPais", GlobalV1.valNulo(ender.codPais) ? "105" : ender.codPais, "AF");



                    pXml += fncCocatXmltag("EnderecoEmpresa", "", "F");


                    pXml += fncCocatXmltag("AtuacaoEmpresa", "", "A");

                    pXml += fncCocatXmltag("objetoSocial", dMatriz.objetoSocial, "AF");
                    pXml += fncCocatXmltag("cnaePrincipal", dMatriz.cnaePrincipal, "AF");

                    pXml += fncCocatXmltag("cnaeSecundaria", "", "A");
                    if (dMatriz.cnaeSecundaria != null)
                    {
                        foreach (string CodCnae in dMatriz.cnaeSecundaria)
                        {
                            if (CodCnae.Length < 5)
                            {
                                break;
                            }

                            pXml += fncCocatXmltag("valor", CodCnae, "AF");
                        }
                    }
                    pXml += fncCocatXmltag("cnaeSecundaria", "", "F");

                    pXml += fncCocatXmltag("AtuacaoEmpresa", "", "F");




                    string pNumeroUnico = dados.numViabilidadeAssociada;

                    WsServices17RFB.dadosCNPJ dFilial = new WsServices17RFB.dadosCNPJ();
                    WsServices17RFB.endereco enderFilial = new WsServices17RFB.endereco();


                    pXml += fncCocatXmltag("GrupoFilial", "", "A");



                    if (dMatriz.cnpjFilial != null)
                    {

                        for (int i = 0; i < dMatriz.cnpjFilial.Length; i++)
                        {
                            pXml += fncCocatXmltag("Filial", "", "A");
                            string cnpFilial = "";
                            string nirefilial = "";
                            string ufFilial = "";

                            cnpFilial = dMatriz.cnpjFilial[i].ToString();
                            if (dMatriz.numeroRegistroFilial[i].ToString() != null &&
                                dMatriz.numeroRegistroFilial[i].ToString() != "")
                            {

                                try
                                {
                                    nirefilial = decimal.Parse(dMatriz.numeroRegistroFilial[i].ToString()).ToString();
                                }
                                catch
                                {
                                    nirefilial = "";
                                }
                                //nirefilial = decimal.Parse(dMatriz.numeroRegistroFilial[i].ToString()).ToString();
                            }

                            if (nirefilial == "0")
                                nirefilial = "";

                            ufFilial = dMatriz.ufFilial[i].ToString();

                            pXml += fncCocatXmltag("cnpj", cnpFilial, "AF");
                            pXml += fncCocatXmltag("nire", nirefilial, "AF");
                            pXml += fncCocatXmltag("uf", ufFilial, "AF");
                            pXml += fncCocatXmltag("nrDbeFilial", Recibo + Identificacao, "AF");
                            pXml += fncCocatXmltag("Filial", "", "F");
                        }
                    }


                    pXml += fncCocatXmltag("GrupoFilial", "", "F");



                    pXml += fncCocatXmltag("coleta", "", "F");

                    pResult.XmlDBE = pXml;

                }
                else
                {
                    pResult.status = "NOK";
                    pResult.codretorno = resulRegin.codretorno;
                    pResult.descricao = resulRegin.descricao;

                }

            }
            catch (Exception ex)
            {
                pResult.status = "NOK";
                pResult.codretorno = "99";
                pResult.descricao = ex.Message;
            }

            return pResult;
        }


        #endregion



    }
}