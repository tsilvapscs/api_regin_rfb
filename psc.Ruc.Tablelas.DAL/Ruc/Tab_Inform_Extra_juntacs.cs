using System;
using System.Collections.Generic;
using System.Text;
//using psc.Ruc.Tablelas.Entities;
using psc.Ruc.Tablelas.ConnectionBase;
//using psc.ApplicationBlocks.DAL;
using psc.Framework;
using psc.Framework.Data;
using System.Data;
using System.Data.OracleClient;
//using Oracle.DataAccess.Types;
using System.Xml;
using System.IO;
using System.Data.OleDb;


namespace psc.Ruc.Tablelas.Ruc
{
    public class Tab_Inform_Extra_juntacs : DBInteractionBaseORACLE
    {
        // Variables ******************* 
        #region  Property Declarations
        protected string _tie_protocolo = "";
        protected string _tie_cpf_cnpj = "";
        protected decimal _tie_tipo_relacao = 0;
        protected string _tie_ddd_fone1 = "";
        protected string _tie_fone1 = "";
        protected string _tie_ddd_fone2 = "";
        protected string _tie_fone2 = "";
        protected string _tie_ddd_fax = "";
        protected string _tie_fax = "";
        protected string _tie_tipo_unidade = "0";
        protected string _tie_orgao_registro = "";
        protected string _tie_cnpj_registro = "";
        protected string _tie_forma_atuacao = "";
        protected string _tie_email = "";
        protected string _tie_distrito = "";
        protected decimal _tie_in_centro_distribuicao = 0;
        protected decimal _tie_in_franqueado = 0;
        protected string _tie_cnpj_franqueador = "";
        protected string _tie_nr_ato_legal = "";
        protected decimal _tie_tipo_propriedade = 0;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string tie_protocolo
        {
            get { return _tie_protocolo; }
            set { _tie_protocolo = value; }
        }
        public string tie_cpf_cnpj
        {
            get { return _tie_cpf_cnpj; }
            set { _tie_cpf_cnpj = value; }
        }
        public decimal tie_tipo_relacao
        {
            get { return _tie_tipo_relacao; }
            set { _tie_tipo_relacao = value; }
        }
        public string tie_ddd_fone1
        {
            get { return _tie_ddd_fone1; }
            set { _tie_ddd_fone1 = value; }
        }
        public string tie_fone1
        {
            get { return _tie_fone1; }
            set { _tie_fone1 = value; }
        }
        public string tie_ddd_fone2
        {
            get { return _tie_ddd_fone2; }
            set { _tie_ddd_fone2 = value; }
        }
        public string tie_fone2
        {
            get { return _tie_fone2; }
            set { _tie_fone2 = value; }
        }
        public string tie_ddd_fax
        {
            get { return _tie_ddd_fax; }
            set { _tie_ddd_fax = value; }
        }
        public string tie_fax
        {
            get { return _tie_fax; }
            set { _tie_fax = value; }
        }
        public string tie_tipo_unidade
        {
            get { return _tie_tipo_unidade; }
            set { _tie_tipo_unidade = value; }
        }
        public string tie_orgao_registro
        {
            get { return _tie_orgao_registro; }
            set { _tie_orgao_registro = value; }
        }
        public string tie_cnpj_registro
        {
            get { return _tie_cnpj_registro; }
            set { _tie_cnpj_registro = value; }
        }
        public string tie_forma_atuacao
        {
            get { return _tie_forma_atuacao; }
            set { _tie_forma_atuacao = value; }
        }
        public string tie_email
        {
            get { return _tie_email; }
            set { _tie_email = value; }
        }
        public string tie_distrito
        {
            get { return _tie_distrito; }
            set { _tie_distrito = value; }
        }
        public decimal tie_in_centro_distribuicao
        {
            get { return _tie_in_centro_distribuicao; }
            set { _tie_in_centro_distribuicao = value; }
        }
        public decimal tie_in_franqueado
        {
            get { return _tie_in_franqueado; }
            set { _tie_in_franqueado = value; }
        }
        public string tie_cnpj_franqueador
        {
            get { return _tie_cnpj_franqueador; }
            set { _tie_cnpj_franqueador = value; }
        }
        public string tie_nr_ato_legal
        {
            get { return _tie_nr_ato_legal; }
            set { _tie_nr_ato_legal = value; }
        }
        public decimal tie_tipo_propriedade
        {
            get { return _tie_tipo_propriedade; }
            set { _tie_tipo_propriedade = value; }
        }
        #endregion


        public void UpdateDeferidor()
        {
            try
            {
                StringBuilder Sql = new StringBuilder();

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandType = CommandType.Text;
                Sql.Append(" Insert into tab_inform_extra_junta");
                Sql.Append("  (");
                Sql.Append("	tie_protocolo, ");
                Sql.Append("	tie_cpf_cnpj, ");
                Sql.Append("	tie_tipo_relacao, ");
                Sql.Append("	tie_ddd_fone1, ");
                Sql.Append("	tie_fone1, ");
                Sql.Append("	tie_ddd_fone2, ");
                Sql.Append("	tie_fone2, ");
                Sql.Append("	tie_ddd_fax, ");
                Sql.Append("	tie_fax, ");
                Sql.Append("	tie_tipo_unidade, ");
                Sql.Append("	tie_orgao_registro, ");
                Sql.Append("	tie_cnpj_registro, ");
                Sql.Append("	tie_forma_atuacao, ");
                Sql.Append("	tie_email, ");
                Sql.Append("	tie_distrito ");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	:v_tie_protocolo, ");
                Sql.Append("	:v_tie_cpf_cnpj, ");
                Sql.Append("	:v_tie_tipo_relacao, ");
                Sql.Append("	:v_tie_ddd_fone1, ");
                Sql.Append("	:v_tie_fone1, ");
                Sql.Append("	:v_tie_ddd_fone2, ");
                Sql.Append("	:v_tie_fone2, ");
                Sql.Append("	:v_tie_ddd_fax, ");
                Sql.Append("	:v_tie_fax, ");
                Sql.Append("	:v_tie_tipo_unidade, ");
                Sql.Append("	:v_tie_orgao_registro, ");
                Sql.Append("	:v_tie_cnpj_registro, ");
                Sql.Append("	:v_tie_forma_atuacao, ");
                Sql.Append("	:v_tie_email, ");
                Sql.Append("	:v_tie_distrito ");
                Sql.Append("  )");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionORACLE;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_protocolo));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_cpf_cnpj", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_cpf_cnpj));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_tipo_relacao", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_tipo_relacao));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_ddd_fone1", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_ddd_fone1));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_fone1", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_fone1));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_ddd_fone2", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_ddd_fone2));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_fone2", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_fone2));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_ddd_fax", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_ddd_fax));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_fax", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_fax));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_tipo_unidade", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_tipo_unidade));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_orgao_registro", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_orgao_registro));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_cnpj_registro", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_cnpj_registro));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_forma_atuacao", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_forma_atuacao));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_email", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_email));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_distrito", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_distrito));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnectionORACLE.Open();
                }
                else
                {
                    if (_mainConnectionProviderORACLE.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProviderORACLE.CurrentTransaction;
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
                    _mainConnectionProviderORACLE.Dispose();
                }
            }
        }

        public void Update()
        {
            try
            {
                StringBuilder Sql = new StringBuilder();

                OracleCommand cmdToExecute = new OracleCommand();
                cmdToExecute.CommandType = CommandType.Text;
                Sql.Append(" Insert into tab_inform_extra_junta");
                Sql.Append("  (");
                Sql.Append("	tie_protocolo, ");
                Sql.Append("	tie_cpf_cnpj, ");
                Sql.Append("	tie_tipo_relacao, ");
                Sql.Append("	tie_ddd_fone1, ");
                Sql.Append("	tie_fone1, ");
                Sql.Append("	tie_ddd_fone2, ");
                Sql.Append("	tie_fone2, ");
                Sql.Append("	tie_ddd_fax, ");
                Sql.Append("	tie_fax, ");
                Sql.Append("	tie_tipo_unidade, ");
                Sql.Append("	tie_orgao_registro, ");
                Sql.Append("	tie_cnpj_registro, ");
                Sql.Append("	tie_forma_atuacao, ");
                Sql.Append("	tie_email, ");
                Sql.Append("	tie_distrito, ");
                Sql.Append("	tie_in_centro_distribuicao, ");
                Sql.Append("	tie_in_franqueado, ");
                Sql.Append("	tie_cnpj_franqueador, ");
                Sql.Append("	tie_nr_ato_legal, ");
                Sql.Append("	tie_tipo_propriedade");
                Sql.Append("  )");
                Sql.Append(" Values ");
                Sql.Append("  (");
                Sql.Append("	:v_tie_protocolo, ");
                Sql.Append("	:v_tie_cpf_cnpj, ");
                Sql.Append("	:v_tie_tipo_relacao, ");
                Sql.Append("	:v_tie_ddd_fone1, ");
                Sql.Append("	:v_tie_fone1, ");
                Sql.Append("	:v_tie_ddd_fone2, ");
                Sql.Append("	:v_tie_fone2, ");
                Sql.Append("	:v_tie_ddd_fax, ");
                Sql.Append("	:v_tie_fax, ");
                Sql.Append("	:v_tie_tipo_unidade, ");
                Sql.Append("	:v_tie_orgao_registro, ");
                Sql.Append("	:v_tie_cnpj_registro, ");
                Sql.Append("	:v_tie_forma_atuacao, ");
                Sql.Append("	:v_tie_email, ");
                Sql.Append("	:v_tie_distrito, ");
                Sql.Append("	:v_tie_in_centro_distribuicao, ");
                Sql.Append("	:v_tie_in_franqueado, ");
                Sql.Append("	:v_tie_cnpj_franqueador, ");
                Sql.Append("	:v_tie_nr_ato_legal, ");
                Sql.Append("	:v_tie_tipo_propriedade");
                Sql.Append("  )");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionORACLE;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_protocolo));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_cpf_cnpj", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_cpf_cnpj));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_tipo_relacao", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_tipo_relacao));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_ddd_fone1", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_ddd_fone1));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_fone1", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_fone1));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_ddd_fone2", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_ddd_fone2));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_fone2", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_fone2));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_ddd_fax", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_ddd_fax));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_fax", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_fax));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_tipo_unidade", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_tipo_unidade));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_orgao_registro", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_orgao_registro));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_cnpj_registro", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_cnpj_registro));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_forma_atuacao", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_forma_atuacao));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_email", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_email));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_distrito", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_distrito));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_in_centro_distribuicao", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_in_centro_distribuicao));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_in_franqueado", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_in_franqueado));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_cnpj_franqueador", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_cnpj_franqueador));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_nr_ato_legal", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_nr_ato_legal));
                cmdToExecute.Parameters.Add(new OracleParameter("v_tie_tipo_propriedade", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tie_tipo_propriedade));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnectionORACLE.Open();
                }
                else
                {
                    if (_mainConnectionProviderORACLE.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProviderORACLE.CurrentTransaction;
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
                    _mainConnectionProviderORACLE.Dispose();
                }
            }
        }

    }
}
