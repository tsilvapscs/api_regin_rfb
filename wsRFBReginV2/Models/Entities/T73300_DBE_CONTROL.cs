using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using psc.Receita.ConnectionBase;

namespace psc.Receita.Entities
{
    public class T73300_DBE_CONTROL : DBInteractionBase
    {
        // Variables ******************* 
        #region  Property Declarations
        protected decimal _t73300_id_control;
        protected string _t73300_rec_solicitacao = "";
        protected string _t73300_ide_solicitacao = "";
        protected string _t73300_cod_convenio = "";
        protected string _t73300_cnpj_empresa = "";
        protected string _t73300_tip_ppa = "";
        protected string _t73300_nat_evento = "";
        protected string _t73300_tip_certificado = "";
        protected string _t73300_cpf_cnpj_solicitante = "";
        protected string _t7300_uf_origem = "";
        protected string _t73300_cod_munic_origem = "";
        protected string _t73300_in_estab_matriz = "";
        protected string _t73300_num_arquivamento = "";
        protected string _t733300_Arquivo_RFB = "";
        protected string _t733300_HashCode_arq_RFB = "";
        protected string _t733300_orgaoResponsavelDeferimento = "";
        #endregion

        // Property ******************* 
        #region Class Member Declarations

        public string t733300_orgaoResponsavelDeferimento
        {
            get { return _t733300_orgaoResponsavelDeferimento; }
            set { _t733300_orgaoResponsavelDeferimento = value; }
        }
        
        public string t733300_HashCode_arq_RFB
        {
            get { return _t733300_HashCode_arq_RFB; }
            set { _t733300_HashCode_arq_RFB = value; }
        }

        public string t733300_Arquivo_RFB
        {
            get { return _t733300_Arquivo_RFB; }
            set { _t733300_Arquivo_RFB = value; }
        }

        public decimal t73300_id_control
        {
            get { return _t73300_id_control; }
            set { _t73300_id_control = value; }
        }
        public string t73300_rec_solicitacao
        {
            get { return _t73300_rec_solicitacao; }
            set { _t73300_rec_solicitacao = value; }
        }
        public string t73300_ide_solicitacao
        {
            get { return _t73300_ide_solicitacao; }
            set { _t73300_ide_solicitacao = value; }
        }
        public string t73300_cod_convenio
        {
            get { return _t73300_cod_convenio; }
            set { _t73300_cod_convenio = value; }
        }
        public string t73300_cnpj_empresa
        {
            get { return _t73300_cnpj_empresa; }
            set { _t73300_cnpj_empresa = value; }
        }
        public string t73300_tip_ppa
        {
            get { return _t73300_tip_ppa; }
            set { _t73300_tip_ppa = value; }
        }
        public string t73300_nat_evento
        {
            get { return _t73300_nat_evento; }
            set { _t73300_nat_evento = value; }
        }
        public string t73300_tip_certificado
        {
            get { return _t73300_tip_certificado; }
            set { _t73300_tip_certificado = value; }
        }
        public string t73300_cpf_cnpj_solicitante
        {
            get { return _t73300_cpf_cnpj_solicitante; }
            set { _t73300_cpf_cnpj_solicitante = value; }
        }
        public string t7300_uf_origem
        {
            get { return _t7300_uf_origem; }
            set { _t7300_uf_origem = value; }
        }
        public string t73300_cod_munic_origem
        {
            get { return _t73300_cod_munic_origem; }
            set { _t73300_cod_munic_origem = value; }
        }
        public string t73300_in_estab_matriz
        {
            get { return _t73300_in_estab_matriz; }
            set { _t73300_in_estab_matriz = value; }
        }
        public string t73300_num_arquivamento
        {
            get { return _t73300_num_arquivamento; }
            set { _t73300_num_arquivamento = value; }
        }
        #endregion

        #region Implementes
        public string getXMLDBE()
        {
            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(" Select    t733300_Arquivo_RFB ");
            SqlU.AppendLine(" from      t73300_dbe_control ");
            SqlU.AppendLine(" Where	    t73300_rec_solicitacao = @v_t73300_rec_solicitacao ");
            SqlU.AppendLine(" And	    t73300_ide_solicitacao = @v_t73300_ide_solicitacao ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_rec_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_rec_solicitacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_ide_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_ide_solicitacao));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                object pid = (object)cmdToExecute.ExecuteScalar();

                if (pid == null || pid.ToString() == "")
                {
                    return "";
                }

                return pid.ToString();

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }

        public string getHashCode()
        {
            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(" Select    t733300_HashCode_arq_RFB ");
            SqlU.AppendLine(" from      t73300_dbe_control ");
            SqlU.AppendLine(" Where	    t73300_rec_solicitacao = @v_t73300_rec_solicitacao ");
            SqlU.AppendLine(" And	    t73300_ide_solicitacao = @v_t73300_ide_solicitacao ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_rec_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_rec_solicitacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_ide_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_ide_solicitacao));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                object pid = (object)cmdToExecute.ExecuteScalar();

                if (pid == null || pid.ToString() == "")
                {
                    return "";
                }

                return pid.ToString();

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }
        public int getIdControl()
        {
            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(" Select    t73300_id_control ");
            SqlU.AppendLine(" from      t73300_dbe_control ");
            SqlU.AppendLine(" Where	    t73300_rec_solicitacao = @v_t73300_rec_solicitacao ");
            SqlU.AppendLine(" And	    t73300_ide_solicitacao = @v_t73300_ide_solicitacao ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_rec_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_rec_solicitacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_ide_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_ide_solicitacao));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                object pid = (object)cmdToExecute.ExecuteScalar();

                if (pid == null || pid.ToString() == "")
                {
                    return int.MinValue;
                }

                return int.Parse(pid.ToString());

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }
        public void Delete()
        {
            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(" Delete    from t73300_dbe_control ");
            SqlU.AppendLine(" Where	    t73300_id_control = @v_t73300_id_control ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_id_control", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_id_control));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                cmdToExecute.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }

        public void Update()
        {
            StringBuilder SqlU = new StringBuilder();
            StringBuilder Sql = new StringBuilder();


            Sql.AppendLine(" Insert into t73300_dbe_control");
            Sql.AppendLine("  (");
            Sql.AppendLine("	t73300_id_control, ");
            Sql.AppendLine("	t73300_rec_solicitacao, ");
            Sql.AppendLine("	t73300_ide_solicitacao, ");
            Sql.AppendLine("	t73300_cod_convenio, ");
            Sql.AppendLine("	t73300_cnpj_empresa, ");
            Sql.AppendLine("	t73300_tip_ppa, ");
            Sql.AppendLine("	t73300_nat_evento, ");
            Sql.AppendLine("	t73300_tip_certificado, ");
            Sql.AppendLine("	t73300_cpf_cnpj_solicitante, ");
            Sql.AppendLine("	t7300_uf_origem, ");
            Sql.AppendLine("	t73300_cod_munic_origem, ");
            Sql.AppendLine("	t73300_in_estab_matriz, ");
            Sql.AppendLine("	t73300_num_arquivamento, ");
            Sql.AppendLine("	t733300_Arquivo_RFB, ");
            Sql.AppendLine("	t733300_HashCode_arq_RFB, ");
            Sql.AppendLine("	t733300_orgaoResponsavelDeferimento ");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_t73300_id_control, ");
            Sql.AppendLine("	@v_t73300_rec_solicitacao, ");
            Sql.AppendLine("	@v_t73300_ide_solicitacao, ");
            Sql.AppendLine("	@v_t73300_cod_convenio, ");
            Sql.AppendLine("	@v_t73300_cnpj_empresa, ");
            Sql.AppendLine("	@v_t73300_tip_ppa, ");
            Sql.AppendLine("	@v_t73300_nat_evento, ");
            Sql.AppendLine("	@v_t73300_tip_certificado, ");
            Sql.AppendLine("	@v_t73300_cpf_cnpj_solicitante, ");
            Sql.AppendLine("	@v_t7300_uf_origem, ");
            Sql.AppendLine("	@v_t73300_cod_munic_origem, ");
            Sql.AppendLine("	@v_t73300_in_estab_matriz, ");
            Sql.AppendLine("	@v_t73300_num_arquivamento, ");
            Sql.AppendLine("	@v_t733300_Arquivo_RFB, ");
            Sql.AppendLine("	@v_t733300_HashCode_arq_RFB, ");
            Sql.AppendLine("	@v_t733300_orgaoResponsavelDeferimento ");
            Sql.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T73300_DBE_CONTROL Set ");
            SqlU.AppendLine("		t73300_rec_solicitacao = @v_t73300_rec_solicitacao, ");
            SqlU.AppendLine("		t73300_ide_solicitacao = @v_t73300_ide_solicitacao, ");
            SqlU.AppendLine("		t73300_cod_convenio = @v_t73300_cod_convenio, ");
            SqlU.AppendLine("		t73300_cnpj_empresa = @v_t73300_cnpj_empresa, ");
            SqlU.AppendLine("		t73300_tip_ppa = @v_t73300_tip_ppa, ");
            SqlU.AppendLine("		t73300_nat_evento = @v_t73300_nat_evento, ");
            SqlU.AppendLine("		t73300_tip_certificado = @v_t73300_tip_certificado, ");
            SqlU.AppendLine("		t73300_cpf_cnpj_solicitante = @v_t73300_cpf_cnpj_solicitante, ");
            SqlU.AppendLine("		t7300_uf_origem = @v_t7300_uf_origem, ");
            SqlU.AppendLine("		t73300_cod_munic_origem = @v_t73300_cod_munic_origem, ");
            SqlU.AppendLine("		t73300_in_estab_matriz = @v_t73300_in_estab_matriz, ");
            SqlU.AppendLine("		t73300_num_arquivamento = @v_t73300_num_arquivamento, ");
            SqlU.AppendLine("	    t733300_Arquivo_RFB = @v_t733300_Arquivo_RFB, ");
            SqlU.AppendLine("	    t733300_HashCode_arq_RFB = @v_t733300_HashCode_arq_RFB, ");
            SqlU.AppendLine("	    t733300_orgaoResponsavelDeferimento = @v_t733300_orgaoResponsavelDeferimento ");
            SqlU.AppendLine(" Where	t73300_id_control = @v_t73300_id_control ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                if (_t73300_id_control == int.MinValue)
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_id_control", MySqlDbType.Int32, 0, ParameterDirection.InputOutput, true, 0, 0, "", DataRowVersion.Proposed, null));
                else
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_id_control", MySqlDbType.Int32, 0, ParameterDirection.InputOutput, true, 0, 0, "", DataRowVersion.Proposed, _t73300_id_control));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_rec_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_rec_solicitacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_ide_solicitacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_ide_solicitacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_cod_convenio", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_cod_convenio));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_cnpj_empresa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_cnpj_empresa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_tip_ppa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_tip_ppa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_nat_evento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_nat_evento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_tip_certificado", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_tip_certificado));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_cpf_cnpj_solicitante", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_cpf_cnpj_solicitante));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t7300_uf_origem", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t7300_uf_origem));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_cod_munic_origem", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_cod_munic_origem));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_in_estab_matriz", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_in_estab_matriz));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_num_arquivamento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_num_arquivamento));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t733300_Arquivo_RFB", MySqlDbType.MediumText, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t733300_Arquivo_RFB));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t733300_HashCode_arq_RFB", MySqlDbType.VarChar, 0, ParameterDirection.InputOutput, true, 0, 0, "", DataRowVersion.Proposed, _t733300_HashCode_arq_RFB));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t733300_orgaoResponsavelDeferimento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t733300_orgaoResponsavelDeferimento));


                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                if (cmdToExecute.ExecuteNonQuery() == 0)
                {
                    cmdToExecute.CommandText = Sql.ToString();
                    cmdToExecute.ExecuteNonQuery();
                }

                long id = cmdToExecute.LastInsertedId;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }
        #endregion
    }
}
