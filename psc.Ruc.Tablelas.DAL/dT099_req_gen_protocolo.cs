using System;
using System.Text;
using System.Data;
using RCPJ.DAL.ConnectionBase;
using psc.Framework;
using MySql.Data.MySqlClient;

namespace RCPJ.DAL
{
    public class dT099_req_gen_protocolo : DBInteractionBase
    {
        #region  Property Declarations
        private string _T005_NR_PROTOCOLO;
        private int _T099_IN_TIPO_ENQUADRAMENTO;
        private int _T009_CO_PORTE;
        #endregion

        #region Class Member Declarations
        public string T005_NR_PROTOCOLO
        {
            get { return _T005_NR_PROTOCOLO; }
            set { _T005_NR_PROTOCOLO = value; }
        }
        public int T099_IN_TIPO_ENQUADRAMENTO
        {
            get { return _T099_IN_TIPO_ENQUADRAMENTO; }
            set { _T099_IN_TIPO_ENQUADRAMENTO = value; }
        }
        public int T009_CO_PORTE
        {
            get { return _T009_CO_PORTE; }
            set { _T009_CO_PORTE = value; }
        }
        #endregion

        #region Implements
        public DataTable Query()
        {
            StringBuilder Sql = new StringBuilder();

            Sql.AppendLine(" select *   ");
            Sql.AppendLine(" from t099_req_gen_protocolo ");
            Sql.AppendLine(" where 1 = 1   ");
            Sql.AppendLine(" and  T005_NR_PROTOCOLO = '" + _T005_NR_PROTOCOLO + "'");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable("Protocolo");
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmdToExecute);
            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Open connection.
                _mainConnection.Close();
                _mainConnection.Open();

                // Execute query. 

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
                // Close connection. 
                _mainConnection.Close();
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        public void Update()
        {
            StringBuilder SqlI = new StringBuilder();
            StringBuilder SqlU = new StringBuilder();

            SqlI.AppendLine(" Insert into t099_req_gen_protocolo");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	T005_NR_PROTOCOLO ");
            SqlI.AppendLine("	,T099_IN_TIPO_ENQUADRAMENTO ");
            SqlI.AppendLine("	,T009_CO_PORTE ");
            SqlI.AppendLine("  )");
            SqlI.AppendLine(" Values ");
            SqlI.AppendLine("  (");
            SqlI.AppendLine("	@v_T005_NR_PROTOCOLO ");
            SqlI.AppendLine("	,@v_T099_IN_TIPO_ENQUADRAMENTO ");
            SqlI.AppendLine("	,@v_T009_CO_PORTE ");
            SqlI.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     t099_req_gen_protocolo Set ");
            SqlU.AppendLine("		T099_IN_TIPO_ENQUADRAMENTO = @v_T099_IN_TIPO_ENQUADRAMENTO ");
            SqlU.AppendLine("		T009_CO_PORTE = @v_T009_CO_PORTE ");
            SqlU.AppendLine(" Where	 ");
            SqlU.AppendLine(" T005_NR_PROTOCOLO = '" + _T005_NR_PROTOCOLO + "'");


            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object 
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T005_NR_PROTOCOLO", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T005_NR_PROTOCOLO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T099_IN_TIPO_ENQUADRAMENTO", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T099_IN_TIPO_ENQUADRAMENTO));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_T009_CO_PORTE", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _T009_CO_PORTE));

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

                // Execute query. 
                if (cmdToExecute.ExecuteNonQuery() == 0)
                {
                    cmdToExecute.CommandText = SqlI.ToString();
                    cmdToExecute.ExecuteNonQuery();

                }
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
