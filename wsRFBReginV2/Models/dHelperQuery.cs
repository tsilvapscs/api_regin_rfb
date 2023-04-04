using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
//using dal = psc.ApplicationBlocks.DAL.MySqlHelper;
//using RCPJ.DAL.Entities;
using psc.Receita.ConnectionBase;
using System.Data.OracleClient;
using System.Configuration;
using System.Data.SqlClient;
//using psc.ApplicationBlocks.DAL;
//using psc.Framework;
//using psc.Framework.Data;


namespace psc.Receita
{
    public class dHelperQuery
    {
        public void HomologControlQualidade(string pProtocolo, string pcq_cnpj_or, string pCpfRespHomologacao, string pObervacao)
        {
            StringBuilder sqlD = new StringBuilder();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                conn.Open();
                using (OracleTransaction _conn = conn.BeginTransaction())
                {
                    using (OracleCommand cmdToExecute = new OracleCommand())
                    {
                        sqlD.AppendLine(" Update psc_control_qualidade ");
                        sqlD.AppendLine(" Set    pcq_status = 2 ");
                        sqlD.AppendLine(" ,      PCQ_CPF_RESP_CORRECAO = :v_pCpfRespHomologacao ");
                        sqlD.AppendLine("  ,     PCQ_DT_CORRECAO = Sysdate ");
                        sqlD.AppendLine(" Where  PCQ_PROTOCOLO = :v_PCQ_PROTOCOLO ");
                        sqlD.AppendLine(" and    pcq_cnpj_or = :v_pcq_cnpj_or ");
                        sqlD.AppendLine(" and    pcq_status <> 2 ");

                        cmdToExecute.Parameters.Clear();

                        cmdToExecute.Parameters.Add(new OracleParameter("v_PCQ_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                        cmdToExecute.Parameters.Add(new OracleParameter("v_pcq_cnpj_or", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pcq_cnpj_or));
                        cmdToExecute.Parameters.Add(new OracleParameter("v_pCpfRespHomologacao", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCpfRespHomologacao));

                        cmdToExecute.Connection = _conn.Connection;
                        cmdToExecute.Transaction = _conn;

                        cmdToExecute.CommandText = sqlD.ToString();
                        cmdToExecute.CommandType = CommandType.Text;
                        cmdToExecute.ExecuteNonQuery();

                    }
                    _conn.Commit();
                }
            }
        }

        public void ReprocessoControlQualidade(string pProtocolo, string pcq_cnpj_or, string pCpfRespHomologacao, string pObervacao)
        {
            StringBuilder sqlD = new StringBuilder();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                conn.Open();
                using (OracleTransaction _conn = conn.BeginTransaction())
                {
                    using (OracleCommand cmdToExecute = new OracleCommand())
                    {
                        sqlD.AppendLine(" Update psc_control_qualidade ");
                        sqlD.AppendLine(" Set    pcq_status = 2 ");
                        sqlD.AppendLine(" ,      PCQ_CPF_RESP_REPROCESSO = :v_pCpfRespHomologacao ");
                        sqlD.AppendLine("  ,     PCQ_DT_REPROCESSO = Sysdate ");
                        sqlD.AppendLine(" Where  PCQ_PROTOCOLO = :v_PCQ_PROTOCOLO ");
                        sqlD.AppendLine(" and    pcq_cnpj_or = :v_pcq_cnpj_or ");
                        sqlD.AppendLine(" and    pcq_status <> 2 ");

                        cmdToExecute.Parameters.Clear();

                        cmdToExecute.Parameters.Add(new OracleParameter("v_PCQ_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                        cmdToExecute.Parameters.Add(new OracleParameter("v_pcq_cnpj_or", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pcq_cnpj_or));
                        cmdToExecute.Parameters.Add(new OracleParameter("v_pCpfRespHomologacao", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCpfRespHomologacao));

                        cmdToExecute.Connection = _conn.Connection;
                        cmdToExecute.Transaction = _conn;

                        cmdToExecute.CommandText = sqlD.ToString();
                        cmdToExecute.CommandType = CommandType.Text;
                        cmdToExecute.ExecuteNonQuery();

                    }
                    _conn.Commit();
                }
            }
        }
        public void UpdateStatusControlQualidade(string pProtocolo, string pcq_cnpj_or, string pStatus)
        {
            StringBuilder sqlD = new StringBuilder();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                conn.Open();
                using (OracleTransaction _conn = conn.BeginTransaction())
                {
                    using (OracleCommand cmdToExecute = new OracleCommand())
                    {
                        sqlD.AppendLine(" Update psc_control_qualidade ");
                        sqlD.AppendLine(" Set    pcq_status = :v_pcq_status ");
                        if (pStatus == "2")
                        {
                            sqlD.AppendLine("  , PCQ_DT_CORRECAO = sysdate ");
                        }
                        sqlD.AppendLine(" Where  PCQ_PROTOCOLO = :v_PCQ_PROTOCOLO ");
                        sqlD.AppendLine(" and    pcq_cnpj_or = :v_pcq_cnpj_or ");
                        //sqlD.AppendLine(" And    pcq_cnpj_or = v_pcq_cnpj_or ");


                        cmdToExecute.Parameters.Clear();

                        cmdToExecute.Parameters.Add(new OracleParameter("v_PCQ_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                        cmdToExecute.Parameters.Add(new OracleParameter("v_pcq_cnpj_or", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pcq_cnpj_or));
                        cmdToExecute.Parameters.Add(new OracleParameter("v_pcq_status", OracleType.Number, 2, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pStatus));

                        cmdToExecute.Connection = _conn.Connection;
                        cmdToExecute.Transaction = _conn;

                        cmdToExecute.CommandText = sqlD.ToString();
                        cmdToExecute.CommandType = CommandType.Text;
                        cmdToExecute.ExecuteNonQuery();

                    }
                    _conn.Commit();
                }
            }
        }

        public DataTable getControlQualidade(string pProtocolo, string pcq_cnpj_or)
        {
            StringBuilder sqlD = new StringBuilder();
            DataTable toReturn = new DataTable();
            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                using (OracleCommand cmdToExecute = new OracleCommand())
                {
                    sqlD.AppendLine(" Select    * ");
                    sqlD.AppendLine(" From      psc_control_qualidade ");
                    sqlD.AppendLine(" Where     PCQ_PROTOCOLO = :v_PCQ_PROTOCOLO ");
                    sqlD.AppendLine(" and       pcq_cnpj_or = :v_pcq_cnpj_or ");

                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.Parameters.Add(new OracleParameter("v_PCQ_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecute.Parameters.Add(new OracleParameter("v_pcq_cnpj_or", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pcq_cnpj_or));

                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Connection.Open();

                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;

                    OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);
                    adapter.Fill(toReturn);
                    return toReturn;

                }
            }

        }

        public DataTable getMarcadoS24(string pCnpj)
        {
            StringBuilder sqlD = new StringBuilder();
            DataTable toReturn = new DataTable();
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
            {
                using (MySqlCommand cmdToExecute = new MySqlCommand())
                {
                    sqlD.AppendLine(" Select    * ");
                    sqlD.AppendLine(" FROM      t73310_dados_s24 aa ");
                    sqlD.AppendLine(" Where     aa.cnpjEmpresa = @v_CNPJ ");
                    sqlD.AppendLine(" order by  aa.dataRegistro desc limit 1");

                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_CNPJ", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCnpj));

                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Connection.Open();

                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
                    adapter.Fill(toReturn);
                    return toReturn;

                }
            }

        }



        public static bool UpdateCamposTabelasRuc(string XmlValidacao, string pProtocolo)
        {
            bool pResulAtualizou = false;
            XmlDocument docResposta = new XmlDocument();
            docResposta.Load(new StringReader(XmlValidacao));
            if (docResposta.SelectNodes("//root//atualizacao") != null)
            {
                foreach (XmlNode nodeResp in docResposta.SelectNodes("//root//atualizacao"))
                {
                    string tipo = nodeResp["tipo"].InnerText.Trim().ToLower();
                    string posicao = nodeResp["posicao"].InnerText.Trim().ToLower();
                    string valor = nodeResp["valor"].InnerText.Trim().ToLower();
                    string cpfcnpj = nodeResp["cpfcnpj"].InnerText.Trim().ToLower();

                    if (tipo == "cep")
                    {
                        UpdateCepFaixaCerta(pProtocolo, posicao, valor, cpfcnpj);
                        pResulAtualizou = true;
                    }
                }
            }

            return pResulAtualizou;
        }
        private static void UpdateCepFaixaCerta(string protocolo, string posicao, string cep, string cpfcnpj)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                StringBuilder sqlD = new StringBuilder();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction _conn = conn.BeginTransaction())
                    {
                        using (SqlCommand cmdToExecute = new SqlCommand())
                        {
                            #region ruc_estab
                            if (posicao.ToLower() == "ruc_estab")
                            {
                                sqlD.AppendLine(" update    ruc_estab ");
                                sqlD.AppendLine(" set	    RES_ZONA_POSTAL = @v_cep ");
                                sqlD.AppendLine(" where	    RES_RGE_PRA_PROTOCOLO = @v_protocolo ");

                                cmdToExecute.Parameters.Clear();

                                cmdToExecute.Parameters.Add(new SqlParameter("v_cep", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cep));
                                cmdToExecute.Parameters.Add(new SqlParameter("v_protocolo", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, protocolo));

                                cmdToExecute.Connection = _conn.Connection;
                                cmdToExecute.Transaction = _conn;

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();


                                sqlD = new StringBuilder();
                                sqlD.AppendLine(" update    ruc_comp ");
                                sqlD.AppendLine(" set	    RCO_ZONA_POSTAL = @v_cep ");
                                sqlD.AppendLine(" where	    RCO_RGE_PRA_PROTOCOLO = @v_protocolo ");

                                cmdToExecute.Parameters.Clear();

                                cmdToExecute.Parameters.Add(new SqlParameter("v_cep", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cep));
                                cmdToExecute.Parameters.Add(new SqlParameter("v_protocolo", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, protocolo));

                                cmdToExecute.Connection = _conn.Connection;
                                cmdToExecute.Transaction = _conn;

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();
                            }
                            #endregion

                            #region ruc_prof
                            if (posicao.ToLower() == "ruc_prof")
                            {
                                sqlD = new StringBuilder();
                                sqlD.AppendLine(" update    ruc_prof ");
                                sqlD.AppendLine(" set	    RPR_ZONA_POSTAL = @v_cep ");
                                sqlD.AppendLine(" where	    RPR_RGE_PRA_PROTOCOLO = @v_protocolo ");
                                sqlD.AppendLine(" And	    RPR_CGC_CPF_SECD = @v_cpfcnpj ");

                                cmdToExecute.Parameters.Clear();

                                cmdToExecute.Parameters.Add(new SqlParameter("v_cep", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cep));
                                cmdToExecute.Parameters.Add(new SqlParameter("v_protocolo", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, protocolo));
                                cmdToExecute.Parameters.Add(new SqlParameter("v_cpfcnpj", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cpfcnpj));

                                cmdToExecute.Connection = _conn.Connection;
                                cmdToExecute.Transaction = _conn;

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();

                            }
                            #endregion
                        }
                        _conn.Commit();
                    }
                }
            }
            else
            {
                StringBuilder sqlD = new StringBuilder();
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (OracleTransaction _conn = conn.BeginTransaction())
                    {
                        using (OracleCommand cmdToExecute = new OracleCommand())
                        {
                            #region ruc_estab
                            if (posicao.ToLower() == "ruc_estab")
                            {
                                sqlD.AppendLine(" update    ruc_estab ");
                                sqlD.AppendLine(" set	    RES_ZONA_POSTAL = :v_cep ");
                                sqlD.AppendLine(" where	    RES_RGE_PRA_PROTOCOLO = :v_protocolo ");

                                cmdToExecute.Parameters.Clear();

                                cmdToExecute.Parameters.Add(new OracleParameter("v_cep", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cep));
                                cmdToExecute.Parameters.Add(new OracleParameter("v_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, protocolo));

                                cmdToExecute.Connection = _conn.Connection;
                                cmdToExecute.Transaction = _conn;

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();


                                sqlD = new StringBuilder();
                                sqlD.AppendLine(" update    ruc_comp ");
                                sqlD.AppendLine(" set	    RCO_ZONA_POSTAL = :v_cep ");
                                sqlD.AppendLine(" where	    RCO_RGE_PRA_PROTOCOLO = :v_protocolo ");

                                cmdToExecute.Parameters.Clear();

                                cmdToExecute.Parameters.Add(new OracleParameter("v_cep", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cep));
                                cmdToExecute.Parameters.Add(new OracleParameter("v_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, protocolo));

                                cmdToExecute.Connection = _conn.Connection;
                                cmdToExecute.Transaction = _conn;

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();
                            }
                            #endregion

                            #region ruc_prof
                            if (posicao.ToLower() == "ruc_prof")
                            {
                                sqlD = new StringBuilder();
                                sqlD.AppendLine(" update    ruc_prof ");
                                sqlD.AppendLine(" set	    RPR_ZONA_POSTAL = :v_cep ");
                                sqlD.AppendLine(" where	    RPR_RGE_PRA_PROTOCOLO = :v_protocolo ");
                                sqlD.AppendLine(" And	    RPR_CGC_CPF_SECD = :v_cpfcnpj ");

                                cmdToExecute.Parameters.Clear();

                                cmdToExecute.Parameters.Add(new OracleParameter("v_cep", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cep));
                                cmdToExecute.Parameters.Add(new OracleParameter("v_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, protocolo));
                                cmdToExecute.Parameters.Add(new OracleParameter("v_cpfcnpj", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cpfcnpj));

                                cmdToExecute.Connection = _conn.Connection;
                                cmdToExecute.Transaction = _conn;

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();

                            }
                            #endregion
                        }
                        _conn.Commit();
                    }
                }
            }
        }

        public static DateTime convertStringDateYYYMMDD(string pValue)
        {
            if (pValue.ToString() != "")
            {
                int ano = int.Parse(pValue.Substring(0, 4));
                int mes = int.Parse(pValue.Substring(4, 2));
                int dia = int.Parse(pValue.Substring(6, 2));

                return new DateTime(ano, mes, dia);
            }

            return new DateTime();
        }
        public static bool ValidaCepFaixaMunicipio(string pUf, string pCodMunicipio, ref string cep)
        {
            if (pUf == "" || pCodMunicipio == "" || cep == "")
            {
                return true;
            }

            DataTable toReturn = new DataTable();
            StringBuilder sql = new StringBuilder();
            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {

                    sql.AppendLine(" Select a.tmu_cod_mun, a.tmu_nom_mun, a.tmu_cep_faixa_inicial, a.tmu_cep_faixa_final ");
                    sql.AppendLine(" From   tab_munic a ");
                    sql.AppendLine(" Where  a.tmu_tuf_uf = '" + pUf + "'");
                    sql.AppendLine(" And    a.tmu_cod_mun = " + pCodMunicipio);


                    cmd.CommandText = sql.ToString();
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = _conn;
                    cmd.Connection.Open();
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                    {
                        string cepInici = toReturn.Rows[0]["tmu_cep_faixa_inicial"].ToString().Trim();
                        string cepFinal = toReturn.Rows[0]["tmu_cep_faixa_final"].ToString().Trim();
                        if (cepInici == "" || cepFinal == "")
                        {
                            return true;
                        }

                        if (decimal.Parse(cepInici) <= decimal.Parse(cep) && decimal.Parse(cepFinal) >= decimal.Parse(cep))
                        {
                            return true;
                        }
                        else
                        {
                            cep = cepInici;
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public static string getUrlOR(string pCNPJ)
        {
            DataTable toReturn = new DataTable();
            StringBuilder sql = new StringBuilder();
            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {

                    sql.AppendLine("Select PCO_URL_WS11");
                    sql.AppendLine("From   PSC_CONTROL_OR  ");
                    sql.AppendLine("Where  PCO_CNPJ_OR = '" + pCNPJ + "'");

                    cmd.CommandText = sql.ToString();
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = _conn;
                    cmd.Connection.Open();
                    object pURL = cmd.ExecuteOracleScalar();
                    return pURL.ToString();
                }

            }

        }

        public static DataTable GetParametros(string pCNPJ, string codParametro)
        {
            DataTable toReturn = new DataTable();
            StringBuilder sql = new StringBuilder();
            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {

                    sql.AppendLine("Select og.pco_cnpj_or cnpj, ");
                    sql.AppendLine("       t.pcc_descricao descricao, ");
                    sql.AppendLine("       t.pcc_id codigo, ");
                    sql.AppendLine("       t.PCC_MENSAGEM mensagem ");
                    sql.AppendLine("From   PSC_CONTROL_CAMPO t, psc_control_campo_og og, PSC_CONTROL_OR po");
                    sql.AppendLine("Where  t.pcc_id = og.pco_pcc_id ");
                    sql.AppendLine("And    og.PCO_CNPJ_OR = po.PCO_CNPJ_OR ");
                    sql.AppendLine("And    po.PCO_STATUS = 1 ");
                    sql.AppendLine("And    t.pcc_status = 1 -- ativo ");
                    sql.AppendLine("And    og.pco_status = 1 ");
                    sql.AppendLine("And    og.pco_cnpj_or = '" + pCNPJ + "'");


                    if (codParametro != "")
                        sql.AppendLine("and     t.pcc_id = " + codParametro);

                    cmd.CommandText = sql.ToString();
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = _conn;
                    cmd.Connection.Open();
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    adapter.Fill(toReturn);
                    return toReturn;
                }

            }

        }


        public string BuscarCorrelativo(decimal pTipo)
        {
            try
            {
                decimal pNumero = 0;
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString()))
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.CommandText = "PKG_UTIL.GetProtocoloRegistraArq";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new OracleParameter("pTipo", OracleType.Number, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pTipo));
                        cmd.Parameters.Add(new OracleParameter("pNumero", OracleType.VarChar, 20, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, pNumero));


                        cmd.Connection = conn;

                        cmd.Connection.Open();

                        cmd.ExecuteNonQuery();

                        return (string)cmd.Parameters["pNumero"].Value;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Balcão Unico
        public static string getEmailBalcaoUnico(string pProtocolo)
        {
            StringBuilder sqlD = new StringBuilder();
            DataTable toReturn = new DataTable("email");
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
            {
                using (MySqlCommand cmdToExecute = new MySqlCommand())
                {
                    sqlD.AppendLine(@"  SELECT t001_email
                                        FROM
                                          requerimento.t005_protocolo a
                                        INNER JOIN requerimento.r001_vinculo v
                                        ON v.t001_sq_pessoa_pai = a.t001_sq_pessoa
                                        AND v.A009_CO_CONDICAO = 500
                                        INNER JOIN requerimento.t001_pessoa p
                                        ON p.T001_SQ_PESSOA = v.T001_SQ_PESSOA
                                        WHERE
                                          a.T005_NR_PROTOCOLO = @v_T005_NR_PROTOCOLO");

                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Connection.Open();

                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                        return toReturn.Rows[0]["t001_email"].ToString();
                    else
                        return "";

                }
            }

        }
        public static DataTable getdadosComplementaresBU(string pProtocolo)
        {
            StringBuilder sqlD = new StringBuilder();
            DataTable toReturn = new DataTable("dadosComplementares");
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
            {
                using (MySqlCommand cmdToExecute = new MySqlCommand())
                {
                    sqlD.AppendLine(@" SELECT p.T005_NR_PROTOCOLO_VIABILIDADE as numeroViabilidade
                                             , 'N' AS empresaComEstabelecimento
                                             , ps.T001_NOME_FANTASIA AS nomeFantasia
                                             , replace(pj.t003_vl_capital_social,'.','') AS capitalSocial
                                             , replace(pj.T003_VL_CAPITAL_INTEGRALIZADO,'.','') AS capitalIntegralizado
                                             , replace(pj.T003_VL_CAPITAL_NAO_INTEGRALIZADO,'.','') AS capitalIntegralizar
                                             , FLOOR(pj.T003_VL_QTD_COTAS) AS qtdeQuotas
                                             , replace(pj.T003_VL_VALOR_COTA,'.','') AS valorQuota
                                             , ps.T001_DDD AS dddTelefone1
                                             , ps.T001_TEL_1 AS telefone1
                                             , '' AS dddTelefone2
                                             , '' AS telefone2
                                             , ps.T001_EMAIL AS correioEletronico
                                             , (SELECT PP.CIDORFB
                                                FROM
                                                  requerimento.a011_porte PP
                                                WHERE
                                                  a011_co_porte = pj.t003_tipo_enquadramento) AS codPorteEmpresa
                                               , if(pj.T003_DT_PRAZO_DETERMINADO = NULL, '01', '02') AS indicadorPrazoDuracaoAtividade

                                             , if(pj.T003_DT_INICIO_ATIVIDADE = NULL, '', date_format(pj.T003_DT_INICIO_ATIVIDADE, '%Y%m%d')) AS dataInicioAtividades
                                             , if(pj.T003_DT_TERMINO_ATIV = NULL, '', date_format(pj.T003_DT_TERMINO_ATIV, '%Y%m%d')) AS dataTerminoAtividades
                                             , date_format(p.T005_DATA_ASSINATURA, '%Y%m%d') AS dataAssinaturaContrato
                                             , adv.T021_NR_CPF_ADVOGADO AS cpfAdvogado
                                             , adv.T021_DS_NOME_ADVOGADO AS nomeAdvogado
                                             , adv.T021_NR_INSCR_OAB AS registroOAB
                                             , adv.T021_UF_OAB AS ufOAB
                                             , '' AS indOrigemEndereco
                                             , ve.r002_nr_cep AS cep
                                             , ve.r002_uf AS uf
                                             , ve.a005_co_municipio
                                             , substr(ve.a005_co_municipio, 1, length(ve.a005_co_municipio) - 1) AS codMunicipio
                                             , ve.a015_ds_tipo_logradouro AS codTipoLogradouro
                                             , ve.r002_ds_logradouro AS logradouro
                                             , ve.r002_nr_logradouro AS numLogradouro
                                             , ve.r002_ds_complemento AS complementoLogradouro
                                             , ve.r002_ds_bairro AS bairro
                                             , '' AS distrito
                                             , '' AS referencia
                                             , '' AS cidadeExterior
                                             , (select CODIGO_PAIS from requerimento.a017_pais where A017_CO_PAIS = ve.a004_co_pais) AS codPaisEnd

                                        FROM
                                          requerimento.t005_protocolo p
                                        INNER JOIN requerimento.t003_pessoa_juridica pj
                                        ON p.T001_SQ_PESSOA = pj.T001_SQ_PESSOA
                                        INNER JOIN requerimento.t001_pessoa ps
                                        ON p.t001_sq_pessoa = ps.T001_SQ_PESSOA
                                        LEFT JOIN requerimento.t021_advogado_visto adv
                                        ON adv.T005_NR_PROTOCOLO = p.T005_NR_PROTOCOLO
                                        inner join requerimento.r002_vinculo_endereco ve
                                          on ve.t001_sq_pessoa = p.T001_SQ_PESSOA
                                        WHERE
                                          p.T005_NR_PROTOCOLO = @v_T005_NR_PROTOCOLO");

                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Connection.Open();

                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
                    adapter.Fill(toReturn);
                    return toReturn;

                }
            }

        }

        public static DataTable getdadosContadorBU(string pProtocolo)
        {
            StringBuilder sqlD = new StringBuilder();
            DataTable toReturn = new DataTable("Contador");
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
            {
                using (MySqlCommand cmdToExecute = new MySqlCommand())
                {
                    sqlD.AppendLine(@" SELECT 
                                           if(length(T093_CPFCNPJ) = 11, T093_TIP_CLASS_EMPRESA, T093_TIP_CLASS_RESP) AS codClassificCRCcontadorPF
                                         , if(length(T093_CPFCNPJ) = 11, T093_UF_CRC_EMPRESA, T093_UF_CRC_RESP) AS ufContadorPF
                                         , if(length(T093_CPFCNPJ) = 11, T093_CO_CRC_EMPRESA, T093_CO_CRC_RESP) AS numSeqContadorPF
                                         , if(length(T093_CPFCNPJ) = 11, T093_TIP_CRC_EMPRESA, T093_TIP_CRC_RESP) AS codTipoCRCcontadorPF
                                         , if(length(T093_CPFCNPJ) = 11, T093_CPFCNPJ, T093_CPF_RESP) AS cpfContadorPF
                                         , if(length(T093_CPFCNPJ) = 11, T093_DS_PESSOA, T093_NOME_CONTADOR_PF) AS nomeContadorPF
                                         , if(length(T093_CPFCNPJ) = 14, T093_TIP_CLASS_EMPRESA, '') AS codClassificEmpresaContabil
                                         , if(length(T093_CPFCNPJ) = 14, T093_UF_CRC_EMPRESA, '') AS ufCRCempresaContabil
                                         , if(length(T093_CPFCNPJ) = 14, T093_CO_CRC_EMPRESA, '') AS seqCRCempresaContabil
                                         , if(length(T093_CPFCNPJ) = 14, T093_TIP_CRC_EMPRESA, '') AS codTipoCRCempresaContabil
                                         , if(length(T093_CPFCNPJ) = 14, T093_CPFCNPJ, '') AS cnpjEmpresaContabil
                                         , if(length(T093_CPFCNPJ) = 14, T093_DS_PESSOA, '') AS nomeEmpresaContabil
                                         , '' AS indOrigemEndereco
                                         , c.t093_end_cep AS cep
                                         , c.t093_end_uf AS uf
                                         , substr(c.t093_end_cod_munic, 1, length(c.t093_end_cod_munic) - 1) AS codMunicipio
                                         , c.t093_ds_tipo_logradouro AS codTipoLogradouro
                                         , c.t093_end_logradouro AS logradouro
                                         , c.t093_end_numero AS numLogradouro
                                         , c.t093_end_complemento AS complementoLogradouro
                                         , c.t093_end_bairro AS bairro
                                         , '' AS distrito
                                         , '' AS referencia
                                         , '' AS cidadeExterior
                                         , '105' AS codPais
                                         , c.t093_DDD AS dddTelefone1
                                         , c.t093_telefone AS telefone1
                                         , '' AS dddTelefone2
                                         , '' AS telefone2
                                         , c.t093_email AS correioEletronico
                                    FROM
                                      requerimento.t005_protocolo p
                                    INNER JOIN requerimento.t093_contador c
                                    ON p.t005_nr_protocolo = c.t005_nr_protocolo
                                    WHERE
                                      p.T005_NR_PROTOCOLO = @v_T005_NR_PROTOCOLO");

                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Connection.Open();

                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
                    adapter.Fill(toReturn);
                    return toReturn;

                }
            }

        }

        public static DataTable getdadosSociosBU(string pProtocolo)
        {
            StringBuilder sqlD = new StringBuilder();
            DataTable toReturn = new DataTable("Socios"); 
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
            {
                using (MySqlCommand cmdToExecute = new MySqlCommand())
                {
                    sqlD.AppendLine(@"SELECT 
                                           pf.t001_sq_pessoa SqPessoa
                                         ,  if(pf.t002_in_responsavel_cnpj= 1,'S','N') AS indicadorRepresentantePJ
                                         , '001' AS codEvento
                                         , if(length(pf.T002_NR_CPF) = 14, 1, 2) AS indCnpjCpfSocio
                                         , pf.T002_NR_CPF AS cnpjCpfSocio
                                         , pe.T001_DS_PESSOA AS socio
                                         , v.A009_CO_CONDICAO AS codQualificacaoSocio
                                         , (select CODIGO_PAIS from requerimento.a017_pais where A017_CO_PAIS = pf.A004_CO_PAIS) codPais
                                           , replace((pf.T002_CAPITAL_INTEGRALIZADO + T002_CAPITAL_A_INTEGRALIZAR), '.', '') AS valorParticipacaoCapitalSocial
                                         , replace((pf.T002_CAPITAL_INTEGRALIZADO), '.', '') AS valorIntegralizado
                                         , replace((T002_CAPITAL_A_INTEGRALIZAR), '.', '') AS valorIntegralizar
                                         , pf.T002_NR_QTD_COTAS AS qtdeQuotas
                                           , requerimento.fnDeParaEstadoCivil(pf.A012_CO_ESTADO_CIVIL) AS estadoCivil
                                         , requerimento.fnDeParaRegimeBens(pf.A013_CO_REGIME_BENS) AS comunhaoBens
                                         , if(pf.A014_CO_EMANCIPACAO <> 0, 'S', 'N') AS indicadorEmancipacao
                                         , requerimento.fnDeParaTipoEmancipacao(pf.A014_CO_EMANCIPACAO) AS tipoEmancipacao
                                           , '' AS indOrigemEndereco
                                         , ve.r002_nr_cep AS cep
                                         , ve.r002_uf AS uf
                                         , ve.a005_co_municipio
                                         , substr(ve.a005_co_municipio, 1, length(ve.a005_co_municipio) - 1) AS codMunicipio
                                         , ve.a015_ds_tipo_logradouro AS codTipoLogradouro
                                         , ve.r002_ds_logradouro AS logradouro
                                         , ve.r002_nr_logradouro AS numLogradouro
                                         , ve.r002_ds_complemento AS complementoLogradouro
                                         , ve.r002_ds_bairro AS bairro
                                         , (select CODIGO_PAIS from requerimento.a017_pais where A017_CO_PAIS = ve.a004_co_pais) AS codPaisEnd
                                         , '' AS distrito
                                         , '' AS referencia
                                         , '' AS cidadeExterior
                                           , pe.T001_DDD AS dddTelefone1
                                         , pe.T001_TEL_1 AS telefone1
                                         , '' AS dddTelefone2
                                         , '' AS telefone2
                                         , pe.T001_EMAIL AS correioEletronico
                                    FROM
                                      requerimento.t005_protocolo p
                                    INNER JOIN requerimento.r001_vinculo v
                                    ON p.T001_SQ_PESSOA = v.T001_SQ_PESSOA_PAI
                                    INNER JOIN requerimento.t002_pessoa_fisica pf
                                    ON v.t001_sq_pessoa = pf.t001_sq_pessoa
                                    INNER JOIN requerimento.t001_pessoa pe
                                    ON pe.T001_SQ_PESSOA = pf.T001_SQ_PESSOA
                                    INNER JOIN requerimento.r002_vinculo_endereco ve
                                    ON pf.T001_SQ_PESSOA = ve.t001_sq_pessoa
                                    WHERE
                                      p.T005_NR_PROTOCOLO = @v_T005_NR_PROTOCOLO
                                      AND v.A009_CO_CONDICAO NOT IN (500, 501)");

                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Connection.Open();

                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
                    adapter.Fill(toReturn);
                    return toReturn;

                }
            }

        }

        public static DataTable getdadosRepresentanteBU(string pSqPessoa)
        {
            StringBuilder sqlD = new StringBuilder();
            DataTable toReturn = new DataTable("Representante");
            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
            {
                using (MySqlCommand cmdToExecute = new MySqlCommand())
                {
                    sqlD.AppendLine(@"SELECT pe.T001_DS_PESSOA AS representanteLegalSocio
                                             , (SELECT t015_co_repres_RFB
                                                FROM
                                                  requerimento.t015_tipo_assistido_representado ta
                                                WHERE
                                                  ta.t015_co_tipo_assistido_representado = v.A030_CO_TIPO_ASSISTIDO) AS codQualificacaoRepresentanteLegalSocio
                                             , pf.T002_NR_CPF AS cpfRepresentanteLegalSocio
                                             , '' AS indOrigemEnderecoRep
                                             , ve.r002_nr_cep AS cep
                                             , ve.r002_uf AS uf
                                             , substr(ve.a005_co_municipio, 1, length(ve.a005_co_municipio) - 1) AS codMunicipio
                                             , ve.a015_ds_tipo_logradouro AS codTipoLogradouro
                                             , ve.r002_ds_logradouro AS logradouro
                                             , ve.r002_nr_logradouro AS numLogradouro
                                             , ve.r002_ds_complemento AS complementoLogradouro
                                             , ve.r002_ds_bairro AS bairro
                                             , '' AS distrito
                                             , '' AS referencia
                                             , '' AS cidadeExterior
                                             , (select CODIGO_PAIS from requerimento.a017_pais where A017_CO_PAIS = ve.a004_co_pais) codPaisEnd
                                             , pe.T001_DDD AS dddTelefone1
                                             , pe.T001_TEL_1 AS telefone1
                                             , '' AS dddTelefone2
                                             , '' AS telefone2
                                             , pe.T001_EMAIL AS correioEletronico
                                        FROM
                                          requerimento.r001_vinculo v
                                        INNER JOIN requerimento.t002_pessoa_fisica pf
                                        ON v.t001_sq_pessoa = pf.t001_sq_pessoa
                                        INNER JOIN requerimento.t001_pessoa pe
                                        ON pe.T001_SQ_PESSOA = pf.T001_SQ_PESSOA
                                        INNER JOIN requerimento.r002_vinculo_endereco ve
                                        ON pf.T001_SQ_PESSOA = ve.t001_sq_pessoa
                                        WHERE
                                          V.T001_SQ_PESSOA_PAI = @v_T001_SQ_PESSOA_PAI");

                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_T001_SQ_PESSOA_PAI", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pSqPessoa));

                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Connection.Open();

                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
                    adapter.Fill(toReturn);
                    return toReturn;

                }
            }

        }

        public static string getXMLS01(string pViabilidade)
        {
            DataTable toReturn = new DataTable();
            StringBuilder sql = new StringBuilder();
            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {

                    sql.AppendLine(@"Select a.vpx_cod_protocolo 
                                            , a.vpx_dt_inclusao 
                                            , a.vpx_xml_enviado 
                                     From VIA_PRO_XMLRFB a 
                                     Where a.vpx_cod_protocolo = '" + pViabilidade + "'");

                    cmd.CommandText = sql.ToString();
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = _conn;
                    cmd.Connection.Open();
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    adapter.Fill(toReturn);
                    if (toReturn.Rows.Count > 0)
                        return toReturn.Rows[0]["vpx_xml_enviado"].ToString();
                    else
                        return "";
                }

            }
        }

        public static void UpdateVIA_PRO_XMLRFB_OK(string pProtocolo, string pNumeroDBE)
        {
            StringBuilder sqlD = new StringBuilder();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                conn.Open();
                using (OracleTransaction _conn = conn.BeginTransaction())
                {
                    using (OracleCommand cmdToExecute = new OracleCommand())
                    {
                        sqlD.AppendLine(" Update VIA_PRO_XMLRFB ");
                        sqlD.AppendLine(" Set    VPX_DT_S53 = sysdate ");
                        sqlD.AppendLine(" ,      VPX_STATUS_S53 = '1' ");
                        sqlD.AppendLine(" ,      vpx_xml_erro = null ");
                        sqlD.AppendLine("  ,     VPX_NR_DBE = :v_VPX_NR_DBE ");
                        sqlD.AppendLine(" Where  VPX_NR_REQUERIMENTO = :v_VPX_NR_REQUERIMENTO ");

                        cmdToExecute.Parameters.Clear();

                        cmdToExecute.Parameters.Add(new OracleParameter("v_VPX_NR_REQUERIMENTO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                        cmdToExecute.Parameters.Add(new OracleParameter("v_VPX_NR_DBE", OracleType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pNumeroDBE));

                        cmdToExecute.Connection = _conn.Connection;
                        cmdToExecute.Transaction = _conn;

                        cmdToExecute.CommandText = sqlD.ToString();
                        cmdToExecute.CommandType = CommandType.Text;
                        cmdToExecute.ExecuteNonQuery();

                    }
                    _conn.Commit();
                }
            }
        }

        public static void UpdateDBERequerimento(string pProtocolo, string pDBE)
        {
            StringBuilder sqlD = new StringBuilder();
            StringBuilder sql = new StringBuilder();

            using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
            {
                using (MySqlCommand cmdToExecute = new MySqlCommand())
                {
                    sqlD.AppendLine(@"Update requerimento.t005_protocolo
                                        set T005_NR_DBE = @v_T005_NR_DBE
                                        Where T005_NR_PROTOCOLO = @v_T005_NR_PROTOCOLO");


                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_DBE", MySqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pDBE));

                    cmdToExecute.Connection = _conn;
                    cmdToExecute.Connection.Open();

                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;
                    cmdToExecute.ExecuteNonQuery();

                    sql.AppendLine(@"UPDATE requerimento.t003_pessoa_juridica pj
                                    SET
                                       t003_DBE = @v_t003_DBE
                                    Where t001_sq_Pessoa
                                      in (SELECT p.t001_sq_Pessoa
                                            From requerimento.t005_protocolo p
                                            Where T005_NR_PROTOCOLO = @v_T005_NR_PROTOCOLO
                                            and p.t001_sq_Pessoa = pj.t001_sq_Pessoa)");

                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t003_DBE", MySqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pDBE));

                    cmdToExecute.CommandText = sql.ToString();
                    cmdToExecute.CommandType = CommandType.Text;
                    cmdToExecute.ExecuteNonQuery();

                }
            }

        }

        public static void UpdateVIA_PRO_XMLRFB_NOK(string pProtocolo, string pXML, string status)
        {
            StringBuilder sqlD = new StringBuilder();
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                conn.Open();
                using (OracleTransaction _conn = conn.BeginTransaction())
                {
                    using (OracleCommand cmdToExecute = new OracleCommand())
                    {
                        sqlD.AppendLine(" Update VIA_PRO_XMLRFB ");
                        sqlD.AppendLine(" Set    VPX_DT_S53 = sysdate ");
                        sqlD.AppendLine(" ,      VPX_STATUS_S53 = :v_VPX_STATUS_S53 ");
                        sqlD.AppendLine("  ,     VPX_XML_ERRO = :v_VPX_XML_ERRO ");
                        sqlD.AppendLine(" Where  VPX_NR_REQUERIMENTO = :v_VPX_NR_REQUERIMENTO ");

                        cmdToExecute.Parameters.Clear();

                        cmdToExecute.Parameters.Add(new OracleParameter("v_VPX_NR_REQUERIMENTO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                        cmdToExecute.Parameters.Add(new OracleParameter("v_VPX_XML_ERRO", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pXML));
                        cmdToExecute.Parameters.Add(new OracleParameter("v_VPX_STATUS_S53", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, status));

                        cmdToExecute.Connection = _conn.Connection;
                        cmdToExecute.Transaction = _conn;

                        cmdToExecute.CommandText = sqlD.ToString();
                        cmdToExecute.CommandType = CommandType.Text;
                        cmdToExecute.ExecuteNonQuery();

                    }
                    _conn.Commit();
                }
            }
        }
        public static void AtualizaStatus53comErroParaReenvio()
        {
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                StringBuilder sqlU = new StringBuilder();
                conn.Open();
                using (OracleTransaction _conn = conn.BeginTransaction())
                {
                    using (OracleCommand cmdToExecute = new OracleCommand())
                    {
                        cmdToExecute.Connection = _conn.Connection;
                        cmdToExecute.Transaction = _conn;
                        //Isso e para atualizar caso nao envie o registro a RFB e de status 2, para nulo novamente depois de uma hora, ate 8 horas
                        sqlU.AppendLine(@"  update via_pro_xmlrfb a
                                            Set    a.vpx_status_s53 = null
                                            Where  a.vpx_xml_enviado is not null
                                                  and a.vpx_nr_requerimento is not null
                                                  and a.vpx_nr_dbe is null
                                                  and a.vpx_status_s53 = 2
                                                  and a.vpx_dt_inclusao > sysdate - 8 / 24
                                                  and a.vpx_dt_s53 < sysdate - 1 / 24");

                        cmdToExecute.CommandText = sqlU.ToString();
                        cmdToExecute.CommandType = CommandType.Text;
                        cmdToExecute.ExecuteNonQuery();
                    }
                    _conn.Commit();
                }
            }

        }
        public static DataTable getEnvioS53(string pRequerimento)
        {

            DataTable toReturn = new DataTable();
            StringBuilder sql = new StringBuilder();
            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {

                    sql.AppendLine(@"Select a.vpx_cod_protocolo viabilidade, a.vpx_nr_requerimento requerimento
                                    From VIA_PRO_XMLRFB a, via_protocolo_viab vv 
                                    Where  1 = 1
                                            and a.vpx_cod_protocolo = vv.vpv_cod_protocolo
                                            and vv.vpv_status_env_rfb = '01'
                                            and a.vpx_xml_enviado is not null
                                            and a.vpx_nr_requerimento is not null
                                            and a.vpx_nr_dbe is null
                                            and a.vpx_status_s53 is null ");
                    if (!string.IsNullOrEmpty(pRequerimento))
                    {
                        sql.AppendLine(" and vpx_nr_requerimento = '" + pRequerimento + "'");
                    }
                    //Comentado porque acima atualizo para null a cada uma hora por 8 horas desde a inclusao
                    //sql.AppendLine(@" union
                    //                    Select a.vpx_cod_protocolo viabilidade, a.vpx_nr_requerimento requerimento
                    //                    From VIA_PRO_XMLRFB a 
                    //                    Where  a.vpx_xml_enviado is not null
                    //                          and a.vpx_nr_requerimento is not null
                    //                          and a.vpx_nr_dbe is null
                    //                          and a.vpx_status_s53 = 2
                    //                          and a.vpx_dt_inclusao > sysdate - 4/24"
                    //                );
                    //if (!string.IsNullOrEmpty(pRequerimento))
                    //{
                    //    sql.AppendLine(" and vpx_nr_requerimento = '" + pRequerimento + "'");
                    //}
                    cmd.CommandText = sql.ToString();
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = _conn;
                    cmd.Connection.Open();
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    adapter.Fill(toReturn);

                    
                }
                return toReturn;
            }
        }
        #endregion


        public void CancelarProtocoloViabilidade(OracleTransaction cp, string pProtocolo)
        {
 
            string _VPV_OBJETIVO = "Viabilidade de balcão Único Cancelada.";

            using (OracleCommand cmdToExecute = new OracleCommand())
            {
                cmdToExecute.CommandText = "PKG_VIABILIDADE.CancelaProcessoViabilidade";
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                cmdToExecute.Transaction = cp;
                cmdToExecute.Connection = cp.Connection;
                cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.InputOutput, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                cmdToExecute.Parameters.Add(new OracleParameter("pObservacao", OracleType.Clob, _VPV_OBJETIVO.Length + 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _VPV_OBJETIVO));

                cmdToExecute.ExecuteNonQuery();
            }

            // Execute query.
            


        }


        #region Comparacao DBE x Viabilidade
        public static DataTable getDadosDBEControl(String pNumeroDBE)
        {

            string recibo = pNumeroDBE.Substring(0, 10);
            string identificador = pNumeroDBE.Substring(pNumeroDBE.Length - 14);



            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("DadosDBEControl");

            Sql.AppendLine(@"SELECT * FROM T73300_DBE_CONTROL
                            WHERE t73300_rec_solicitacao = '" + recibo + @"' 
                                  AND t73300_ide_solicitacao = '" + identificador + "' ");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public static DataTable getDadosDBEEventos(int Chave)
        {

            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("DadosEventos");

            Sql.AppendLine(@"SELECT * FROM t73301_dbe_evento
                            WHERE t73300_id_control = " + Chave);


            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }


        public static DataTable getDadosDBEFCPJ(int Chave)
        {

            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("DadosFCPJ");

            Sql.AppendLine(@"SELECT * FROM t73302_dbe_fcpj
                            WHERE t73300_id_control = " + Chave);

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public static DataTable getDadosDBEQSA(int Chave)
        {

            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("DadosQSA");

            Sql.AppendLine(@"SELECT * FROM t73303_dbe_qsa
                            WHERE t73300_id_control = " + Chave);
            Sql.AppendLine(" order by t73303_cpf_cnpj_qsa");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public static DataTable getDadosDBECNAESecundaria(int Chave)
        {

            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("DadosCNAE");

            Sql.AppendLine(@"SELECT * FROM t73304_dbe_cnae_secundaria
                            WHERE t73300_id_control = " + Chave);
            Sql.AppendLine(" order by t73304_cnae_secundaria ");

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public static DataTable getDadosDBEContador(int Chave)
        {

            MySqlConnection _mainConnection = new MySqlConnection();
            _mainConnection.ConnectionString = psc.Framework.General.ConnectionStringMYSQL();

            MySqlCommand cmdToExecute = new MySqlCommand();
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("DadosContador");

            Sql.AppendLine(@"SELECT * FROM t73305_dbe_contador
                            WHERE t73300_id_control = " + Chave);

            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);

            try
            {
                _mainConnection.Open();

                adapter.Fill(toReturn);
                return toReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        //        public static string GetTipoLogradouroDBE1(string _tipoLogradouro)
        //        {

        //            StringBuilder sql = new StringBuilder();
        //            string _retTipoLogradouro = "";

        //            sql.AppendLine(@" select TPOLOGR
        //                        from tab_cep_tipo_abrev 
        //                        where ");
        //            sql.AppendLine(" TPOLOGRABREV = '" + _tipoLogradouro + "'");

        //            DataTable Dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));

        //            if (Dt.Rows.Count != 0)
        //            {
        //                _retTipoLogradouro = Dt.Rows[0]["TPOLOGR"].ToString();
        //            }
        //            else
        //            {
        //                _retTipoLogradouro = _tipoLogradouro;
        //            }

        //            return _retTipoLogradouro;
        //        }
        //        public static string GetDescricaoEventoRFB(string _evento)
        //        {

        //            StringBuilder sql = new StringBuilder();
        //            string _ret = "";

        //            sql.AppendLine(@" SELECT RTRIM(A002_DS_ATO) DESCRICAO
        //                              FROM a002_ato
        //                              WHERE A002_CO_ATO = '" + _evento + "'");

        //            DataTable Dt = DataHelper.GetTable(dal.ExecuteReader(General.ConnectionStringMYSQL(), CommandType.Text, sql.ToString()));

        //            if (Dt.Rows.Count != 0)
        //            {
        //                _ret = Dt.Rows[0]["DESCRICAO"].ToString();
        //            }

        //            return _ret;
        //        }
        #endregion

        #region Email
        public static void CriaEmail(string pViabilidade
                                     , string pRequerimento
                                     , string pProtocolo
                                     , string pTipo
                                     , string pAssunto
                                     , string pCorpoEmail
                                     , string pDestinatario)
        {

            using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.CommandText = "Pkg_Notif_Email.CriaEmail";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new OracleParameter("pViabilidade", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pViabilidade));
                    cmd.Parameters.Add(new OracleParameter("pRequerimento", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRequerimento));
                    cmd.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmd.Parameters.Add(new OracleParameter("pTipo", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pTipo));
                    cmd.Parameters.Add(new OracleParameter("pAssunto", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pAssunto));
                    cmd.Parameters.Add(new OracleParameter("pCorpoEmail", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCorpoEmail));
                    cmd.Parameters.Add(new OracleParameter("pDestinatario", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pDestinatario));


                    cmd.Connection = conn;

                    cmd.Connection.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }
        #endregion

        

    }
}
