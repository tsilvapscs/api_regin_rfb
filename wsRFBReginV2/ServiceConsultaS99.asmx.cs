using AcessoService;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace WsRFBReginV2
{
    /// <summary>
    /// Descrição resumida de ServiceConsultaS99
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class ServiceConsultaS99 : System.Web.Services.WebService
    {

        public ServiceConsultaS99()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        public Retorno getS99NumeroUnico(string pNumeroUnico)
        {
            string pDBE = "";
            string pCNPJMEI = "";
            string pServico = "";
            Retorno pResul = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                pResul.status = "OK";

                if (pNumeroUnico == "")
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pNumeroUnico tem que ser preenchidos";
                    return pResul;
                }

                if (pDBE == "" && pCNPJMEI == "" && pNumeroUnico == "")
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pDBE, pCNPJMEI ou pNumeroUnico tem que ser preenchidos";
                    return pResul;
                }

                if (pDBE != "" && pDBE.Length != 24)
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pDBE invalido";
                    return pResul;
                }

                if (pCNPJMEI != "" && pCNPJMEI.Length != 14)
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pCNPJMEI invalido";
                    return pResul;
                }

                if (pNumeroUnico != "" && pNumeroUnico.Length != 13)
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pNumeroUnico invalido";
                    return pResul;
                }



                string Recibo = "";
                string Identificacao = "";
                if (pDBE != "")
                {
                    Recibo = pDBE.Substring(0, 10);
                    Identificacao = pDBE.Substring(10, 14);
                }


                DataTable toReturn = RecuperaS99(Recibo, Identificacao, pCNPJMEI, pServico, pNumeroUnico);

                DataSet result = new DataSet("rowset");

                result.Tables.Add(toReturn);
                setNullToDefVals(ref result);

                //if (result.Namespace != "RecuperaPessoa")
                //{
                //    result.Namespace = "RecuperaPessoa";
                //}

                if (toReturn.Rows.Count == 0)
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "99";
                    pResul.descricao = "Registros não encontrados";
                }

                pResul.XmlDBE = result.GetXml();

                return pResul;
            }
            catch (Exception ex)
            {
                pResul.status = "NOK";
                pResul.descricao = ex.Message;
                return pResul;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pCNPJMEI"></param>
        /// <returns></returns>
        [WebMethod]
        public Retorno getS99CNPJMei(string pCNPJMEI)
        {

            string pDBE = "";
            //string pCNPJMEI = "";
            string pServico = "";
            Retorno pResul = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                pResul.status = "OK";

                if (pCNPJMEI == "")
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pCNPJMEI tem que ser preenchidos";
                    return pResul;
                }

                if (pDBE == "" && pCNPJMEI == "")
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pDBE, pCNPJMEI  tem que ser preenchidos";
                    return pResul;
                }

                if (pDBE != "" && pDBE.Length != 24)
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pDBE invalido";
                    return pResul;
                }

                if (pCNPJMEI != "" && pCNPJMEI.Length != 14)
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pCNPJMEI invalido";
                    return pResul;
                }



                string Recibo = "";
                string Identificacao = "";
                if (pDBE != "")
                {
                    Recibo = pDBE.Substring(0, 10);
                    Identificacao = pDBE.Substring(10, 14);
                }


                DataTable toReturn = RecuperaS99(Recibo, Identificacao, pCNPJMEI, pServico, "");

                DataSet result = new DataSet("rowset");

                result.Tables.Add(toReturn);
                setNullToDefVals(ref result);

                //if (result.Namespace != "RecuperaPessoa")
                //{
                //    result.Namespace = "RecuperaPessoa";
                //}

                if (toReturn.Rows.Count == 0)
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "99";
                    pResul.descricao = "Registros não encontrados";
                }

                pResul.XmlDBE = result.GetXml();

                return pResul;
            }
            catch (Exception ex)
            {
                pResul.status = "NOK";
                pResul.descricao = ex.Message;
                return pResul;
            }
        }

        [WebMethod]
        public Retorno getS99DadosS99(string pRecibo, string pIdentificacao, string pCNPJMEI, string pServico, string pNumeroUnico)
        {
            //string pDBE = "";
            //string pCNPJMEI = "";
            //string pServico = "";
            Retorno pResul = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                pResul.status = "OK";


                if (pCNPJMEI != "" && pCNPJMEI.Length != 14)
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pCNPJMEI invalido";
                    return pResul;
                }


                DataTable toReturn = RecuperaS99(pRecibo, pIdentificacao, pCNPJMEI, pServico, pNumeroUnico);

                DataSet result = new DataSet("rowset");

                result.Tables.Add(toReturn);
                setNullToDefVals(ref result);

                //if (result.Namespace != "RecuperaPessoa")
                //{
                //    result.Namespace = "RecuperaPessoa";
                //}

                if (toReturn.Rows.Count == 0)
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "99";
                    pResul.descricao = "Registros não encontrados";
                }

                pResul.XmlDBE = result.GetXml();

                return pResul;
            }
            catch (Exception ex)
            {
                pResul.status = "NOK";
                pResul.descricao = ex.Message;
                return pResul;
            }
        }

        [WebMethod]
        public Retorno getS99Ultimos100()
        {
            string pDBE = "";
            string pCNPJMEI = "";
            string pServico = "";
            Retorno pResul = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                pResul.status = "OK";

                //if (pNumeroUnico == "")
                //{
                //    pResul.status = "NOK";
                //    pResul.descricao = "Parametros pNumeroUnico tem que ser preenchidos";
                //    return pResul;
                //}

                //if (pDBE == "" && pCNPJMEI == "" && pNumeroUnico == "")
                //{
                //    pResul.status = "NOK";
                //    pResul.descricao = "Parametros pDBE, pCNPJMEI ou pNumeroUnico tem que ser preenchidos";
                //    return pResul;
                //}

                //if (pDBE != "" && pDBE.Length != 24)
                //{
                //    pResul.status = "NOK";
                //    pResul.descricao = "Parametros pDBE invalido";
                //    return pResul;
                //}

                //if (pCNPJMEI != "" && pCNPJMEI.Length != 14)
                //{
                //    pResul.status = "NOK";
                //    pResul.descricao = "Parametros pCNPJMEI invalido";
                //    return pResul;
                //}

                //if (pNumeroUnico != "" && pNumeroUnico.Length != 13)
                //{
                //    pResul.status = "NOK";
                //    pResul.descricao = "Parametros pNumeroUnico invalido";
                //    return pResul;
                //}



                //string Recibo = "";
                //string Identificacao = "";
                //if (pDBE != "")
                //{
                //    Recibo = pDBE.Substring(0, 10);
                //    Identificacao = pDBE.Substring(10, 14);
                //}


                DataTable toReturn = RecuperaS99Regin("", "", "", "", "");

                DataSet result = new DataSet("rowset");

                result.Tables.Add(toReturn);
                setNullToDefVals(ref result);

                //if (result.Namespace != "RecuperaPessoa")
                //{
                //    result.Namespace = "RecuperaPessoa";
                //}

                if (toReturn.Rows.Count == 0)
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "99";
                    pResul.descricao = "Registros não encontrados";
                }

                pResul.XmlDBE = result.GetXml();

                return pResul;
            }
            catch (Exception ex)
            {
                pResul.status = "NOK";
                pResul.descricao = ex.Message;
                return pResul;
            }
        }


        public void setNullToDefVals(ref DataSet dsXML)
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

        public DataTable RecuperaS99Regin(string pRecibo, string pIdentificacao, string pCNPJMEI, string pServico, string pNumeroUnico)
        {
            //string pRecibo = "";
            //string pIdentificacao = "";
            //string pCNPJMEI = "";
            //string pServico = "";

            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" Select id ");
            Sql.AppendLine(", codServicoDisponivel ");
            Sql.AppendLine(", codigoConvenioAto ");
            Sql.AppendLine(", identificacaoSolicitacao ");
            Sql.AppendLine(", numeroAtoOficio ");
            Sql.AppendLine(", numeroOcorrencia ");
            Sql.AppendLine(", reciboSolicitacao ");
            Sql.AppendLine(", numeroProtocolo ");
            Sql.AppendLine(", datainclusao ");
            Sql.AppendLine(", uf ");
            Sql.AppendLine(", cnpjMei ");
            Sql.AppendLine(", indicadorMei ");
            Sql.AppendLine(", dataEventoMei");

            Sql.AppendLine(" From   t73309_dados_servico99 p ");
            Sql.AppendLine(" Where  1 = 1 ");

            if (pRecibo != "")
            {
                Sql.AppendLine(" And    p.reciboSolicitacao = @pRecibo ");
            }
            if (pIdentificacao != "")
            {
                Sql.AppendLine(" And    p.identificacaoSolicitacao = @pIdentificacao");
            }
            if (pServico != "")
            {
                Sql.AppendLine(" And    p.codServicoDisponivel = @pServico ");
            }

            if (pNumeroUnico != "")
            {
                Sql.AppendLine(" And    p.numeroProtocolo = @pNumeroUnico ");
            }

            if (pCNPJMEI != "")
            {
                Sql.AppendLine(" And    p.cnpjMei = @pCNPJMEI ");
            }

            Sql.AppendLine(" ORDER BY id DESC ");

            Sql.AppendLine(" limit 100 ");

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (DataTable toReturn = new DataTable("S99"))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            conn.Open();
                            cmd.Connection = conn;
                            cmd.CommandText = Sql.ToString();
                            cmd.CommandType = CommandType.Text;
                            if (pRecibo != "")
                            {
                                cmd.Parameters.Add(new MySqlParameter("pRecibo", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRecibo));
                            }
                            if (pIdentificacao != "")
                            {
                                cmd.Parameters.Add(new MySqlParameter("pIdentificacao", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pIdentificacao));
                            }
                            if (pServico != "")
                            {
                                cmd.Parameters.Add(new MySqlParameter("pServico", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pServico));
                            }

                            if (pCNPJMEI != "")
                            {
                                cmd.Parameters.Add(new MySqlParameter("pCNPJMEI", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCNPJMEI));
                            }


                            if (pNumeroUnico != "")
                            {
                                cmd.Parameters.Add(new MySqlParameter("pNumeroUnico", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pNumeroUnico));
                            }

                            //cmd.ExecuteNonQuery();

                            adapter.Fill(toReturn);

                            return toReturn;

                        }
                    }

                }
            }
        }

        private DataTable RecuperaS99(string pRecibo, string pIdentificacao, string pCNPJMEI, string pServico, string pNumeroUnico)
        {
            //string pRecibo = "";
            //string pIdentificacao = "";
            //string pCNPJMEI = "";
            //string pServico = "";

            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" Select id ");
            Sql.AppendLine(", codServicoDisponivel ");
            Sql.AppendLine(", codigoConvenioAto ");
            Sql.AppendLine(", identificacaoSolicitacao ");
            Sql.AppendLine(", numeroAtoOficio ");
            Sql.AppendLine(", numeroOcorrencia ");
            Sql.AppendLine(", reciboSolicitacao ");
            Sql.AppendLine(", numeroProtocolo ");
            Sql.AppendLine(", datainclusao ");
            Sql.AppendLine(", uf ");
            Sql.AppendLine(", cnpjMei ");
            Sql.AppendLine(", indicadorMei ");
            Sql.AppendLine(", dataEventoMei");

            Sql.AppendLine(" From   t73309_dados_servico99 p ");
            Sql.AppendLine(" Where  1 = 1 ");

            if (pRecibo != "")
            {
                Sql.AppendLine(" And    p.reciboSolicitacao = @pRecibo ");
            }
            if (pIdentificacao != "")
            {
                Sql.AppendLine(" And    p.identificacaoSolicitacao = @pIdentificacao");
            }
            if (pServico != "")
            {
                Sql.AppendLine(" And    p.codServicoDisponivel = @pServico ");
            }

            if (pNumeroUnico != "")
            {
                Sql.AppendLine(" And    p.numeroProtocolo = @pNumeroUnico ");
            }

            if (pCNPJMEI != "")
            {
                Sql.AppendLine(" And    p.cnpjMei = @pCNPJMEI ");
            }

            Sql.AppendLine(" order by id desc ");
            Sql.AppendLine(" limit 100 ");

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (DataTable toReturn = new DataTable("S99"))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            conn.Open();
                            cmd.Connection = conn;
                            cmd.CommandText = Sql.ToString();
                            cmd.CommandType = CommandType.Text;
                            if (pRecibo != "")
                            {
                                cmd.Parameters.Add(new MySqlParameter("pRecibo", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRecibo));
                            }
                            if (pIdentificacao != "")
                            {
                                cmd.Parameters.Add(new MySqlParameter("pIdentificacao", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pIdentificacao));
                            }
                            if (pServico != "")
                            {
                                cmd.Parameters.Add(new MySqlParameter("pServico", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pServico));
                            }

                            if (pCNPJMEI != "")
                            {
                                cmd.Parameters.Add(new MySqlParameter("pCNPJMEI", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCNPJMEI));
                            }


                            if (pNumeroUnico != "")
                            {
                                cmd.Parameters.Add(new MySqlParameter("pNumeroUnico", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pNumeroUnico));
                            }

                            //cmd.ExecuteNonQuery();

                            adapter.Fill(toReturn);

                            return toReturn;

                        }
                    }

                }
            }
        }

    }
}
