using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using psc.Receita.ConnectionBase;

namespace psc.Receita.Entities
{
    public class T73309_WS35_RFB : DBInteractionBase
    {
        #region  Property Declarations
        protected string _t73309_recibo;
        protected DateTime _t73309_data_consulta = DateTime.Now;
        protected string _t73309_xml;
        protected string _t73309_Identificacao;
        protected string _t73309_codretorno = "";
        protected string _t73309_CodServico = "";
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string t73309_CodServico
        {
            get { return _t73309_CodServico; }
            set { _t73309_CodServico = value; }
        }
        public string t73309_codretorno
        {
            get { return _t73309_codretorno; }
            set { _t73309_codretorno = value; }
        }
        public string t73309_recibo
        {
            get { return _t73309_recibo; }
            set { _t73309_recibo = value; }
        }
        public string t73309_Identificacao
        {
            get { return _t73309_Identificacao; }
            set { _t73309_Identificacao = value; }
        }
        public string t73309_xml
        {
            get { return _t73309_xml; }
            set { _t73309_xml = value; }
        }
        public DateTime t73309_data_consulta
        {
            get { return _t73309_data_consulta; }
        }

        #endregion

        #region Implementes
        public void DeletePk()
        {
            StringBuilder SqlU = new StringBuilder();
            // Codigo Update ******************* 
            SqlU.AppendLine(" Delete    from T73309_WS35_RFB ");
            SqlU.AppendLine(" Where	    t73309_recibo = @v_t73309_recibo ");
            SqlU.AppendLine(" And	    t73309_Identificacao = @v_t73309_Identificacao ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_recibo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_recibo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_Identificacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_Identificacao));

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

            Sql.AppendLine(" Insert into T73309_WS35_RFB");
            Sql.AppendLine("  (");
            Sql.AppendLine("	t73309_recibo, ");
            Sql.AppendLine("	t73309_Identificacao, ");
            Sql.AppendLine("	t73309_codretorno, "); 
            Sql.AppendLine("	t73309_data_consulta, ");
            Sql.AppendLine("	t73309_xml ");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_t73309_recibo, ");
            Sql.AppendLine("	@v_t73309_Identificacao, ");
            Sql.AppendLine("	@v_t73309_codretorno, ");
            Sql.AppendLine("	@v_t73309_data_consulta, ");
            Sql.AppendLine("	@v_t73309_xml ");
            Sql.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update    T73309_WS35_RFB Set ");
            SqlU.AppendLine("		    t73309_codretorno = @v_t73309_codretorno, ");
            SqlU.AppendLine("		    t73309_data_consulta = @v_t73309_data_consulta, ");
            SqlU.AppendLine("		    t73309_xml = @v_t73309_xml ");
            SqlU.AppendLine(" Where	    1 = 1 ");
            SqlU.AppendLine(" and       t73309_recibo = @v_t73309_recibo ");
            SqlU.AppendLine(" and       t73309_Identificacao = @v_t73309_Identificacao ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_recibo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_recibo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_Identificacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_Identificacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_codretorno", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_codretorno));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_data_consulta", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_data_consulta));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_xml", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_xml));

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

                //Apaga resistro com mais de 6 meses
                cmdToExecute.Parameters.Clear();
                Sql = new StringBuilder();
                Sql.AppendLine(" DELETE FROM T73309_WS35_RFB WHERE t73309_data_consulta < date_sub(sysdate(), INTERVAL 60 DAY)  LIMIT 10 ");
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.CommandType = CommandType.Text;
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

        public void Update_Tdbe_rfb_acesso()
        {
            
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" Insert into t73309_dbe_rfb_acesso");
            Sql.AppendLine("  (");
            Sql.AppendLine("	t73309_recibo, ");
            Sql.AppendLine("	t73309_Identificacao, ");
            Sql.AppendLine("	t73309_CodServico, ");
            Sql.AppendLine("	t73309_codretorno, ");
            Sql.AppendLine("	t73309_data_consulta ");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_t73309_recibo, ");
            Sql.AppendLine("	@v_t73309_Identificacao, ");
            Sql.AppendLine("	@v_t73309_CodServico, ");
            Sql.AppendLine("	@v_t73309_codretorno, ");
            Sql.AppendLine("	@v_t73309_data_consulta ");
            Sql.AppendLine("  )");

          
            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_recibo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_recibo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_Identificacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_Identificacao));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_CodServico", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_CodServico));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_codretorno", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_codretorno));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_data_consulta", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_data_consulta));
   
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

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();

                //Apaga resistro com mais de 6 meses
                //cmdToExecute.Parameters.Clear();
                //Sql = new StringBuilder();
                //Sql.AppendLine(" DELETE FROM t73309_dbe_rfb_acesso WHERE t73309_data_consulta < date_sub(sysdate(), INTERVAL 180 DAY)  LIMIT 10 ");
                //cmdToExecute.CommandText = Sql.ToString();
                //cmdToExecute.CommandType = CommandType.Text;
                //cmdToExecute.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
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

        public T73309_WS35_RFB Query()
        {
            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(" Select    * ");
            SqlU.AppendLine(" from      T73309_WS35_RFB ");
            SqlU.AppendLine(" Where	    t73309_recibo = @v_t73309_recibo ");
            SqlU.AppendLine(" And	    t73309_Identificacao = @v_t73309_Identificacao ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_recibo", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_recibo));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73309_Identificacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73309_Identificacao));

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

                MySqlDataReader dr = cmdToExecute.ExecuteReader();
                T73309_WS35_RFB pResul = new T73309_WS35_RFB();
                if (dr.Read())
                {
                    pResul._t73309_xml = dr.GetString("t73309_xml").ToString();
                    pResul._t73309_data_consulta = dr.GetDateTime("t73309_data_consulta");
                    pResul._t73309_codretorno = dr.GetString("t73309_codretorno").ToString();
                    pResul._t73309_recibo = _t73309_recibo;
                    pResul._t73309_Identificacao = _t73309_Identificacao;
                }
                else
                {
                    pResul = new T73309_WS35_RFB();
                }

                return pResul;

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