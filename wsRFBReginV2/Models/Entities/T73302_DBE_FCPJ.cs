using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using psc.Receita.ConnectionBase;

namespace psc.Receita.Entities
{
    class T73302_DBE_FCPJ : DBInteractionBase
    {
        #region  Property Declarations
        protected decimal _t73300_id_control;
        protected string _t73302_nom_empresarial = "";
        protected string _t73302_nom_fantasia = "";
        protected string _t73302_tip_org_registro = "";
        protected string _t73302_nire = "";
        protected decimal _t73302_capital_social = 0;
        protected string _t73302_cnpj_estab_matriz = "";
        protected string _t73302_tip_logradouro = "";
        protected string _t73302_nom_logradouro = "";
        protected string _t73302_num_logradouro = "";
        protected string _t73302_complemento_logradouro = "";
        protected string _t73302_bairro = "";
        protected string _t73302_distrito = "";
        protected string _t73302_cod_munic = "";
        protected string _t73302_uf = "";
        protected string _t73302_cep = "";
        protected string _t73302_referencia = "";
        protected string _t73302_cod_pais = "";
        protected string _t73302_cidade_exterior = "";
        protected string _t73302_ddd_telefone_1 = "";
        protected string _t73302_telefone_1 = "";
        protected string _t73302_ddd_telefone_2 = "";
        protected string _t73302_telefone_2 = "";
        protected string _t73302_ddd_fax = "";
        protected string _t73302_fax = "";
        protected string _t73302_correio_eletronico = "";
        protected string _t73302_caixa_postal = "";
        protected string _t73302_cep_caixa_postal = "";
        protected string _t73302_porte_empresa = "";
        protected string _t73302_nat_juridica = "";
        protected string _t73302_objeto_social = "";
        protected string _t73302_cnae_principal = "";
        protected string _t73302_cod_tipo_unidade = "";
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string t73302_cod_tipo_unidade
        {
            get { return _t73302_cod_tipo_unidade; }
            set { _t73302_cod_tipo_unidade = value; }
        }

        public decimal t73300_id_control
        {
            get { return _t73300_id_control; }
            set { _t73300_id_control = value; }
        }
        public string t73302_nom_empresarial
        {
            get { return _t73302_nom_empresarial; }
            set { _t73302_nom_empresarial = value; }
        }
        public string t73302_nom_fantasia
        {
            get { return _t73302_nom_fantasia; }
            set { _t73302_nom_fantasia = value; }
        }
        public string t73302_tip_org_registro
        {
            get { return _t73302_tip_org_registro; }
            set { _t73302_tip_org_registro = value; }
        }
        public string t73302_nire
        {
            get { return _t73302_nire; }
            set { _t73302_nire = value; }
        }
        public decimal t73302_capital_social
        {
            get { return _t73302_capital_social; }
            set { _t73302_capital_social = value; }
        }
        public string t73302_cnpj_estab_matriz
        {
            get { return _t73302_cnpj_estab_matriz; }
            set { _t73302_cnpj_estab_matriz = value; }
        }
        public string t73302_tip_logradouro
        {
            get { return _t73302_tip_logradouro; }
            set { _t73302_tip_logradouro = value; }
        }
        public string t73302_nom_logradouro
        {
            get { return _t73302_nom_logradouro; }
            set { _t73302_nom_logradouro = value; }
        }
        public string t73302_num_logradouro
        {
            get { return _t73302_num_logradouro; }
            set { _t73302_num_logradouro = value; }
        }
        public string t73302_complemento_logradouro
        {
            get { return _t73302_complemento_logradouro; }
            set { _t73302_complemento_logradouro = value; }
        }
        public string t73302_bairro
        {
            get { return _t73302_bairro; }
            set { _t73302_bairro = value; }
        }
        public string t73302_distrito
        {
            get { return _t73302_distrito; }
            set { _t73302_distrito = value; }
        }
        public string t73302_cod_munic
        {
            get { return _t73302_cod_munic; }
            set { _t73302_cod_munic = value; }
        }
        public string t73302_uf
        {
            get { return _t73302_uf; }
            set { _t73302_uf = value; }
        }
        public string t73302_cep
        {
            get { return _t73302_cep; }
            set { _t73302_cep = value; }
        }
        public string t73302_referencia
        {
            get { return _t73302_referencia; }
            set { _t73302_referencia = value; }
        }
        public string t73302_cod_pais
        {
            get { return _t73302_cod_pais; }
            set { _t73302_cod_pais = value; }
        }
        public string t73302_cidade_exterior
        {
            get { return _t73302_cidade_exterior; }
            set { _t73302_cidade_exterior = value; }
        }
        public string t73302_ddd_telefone_1
        {
            get { return _t73302_ddd_telefone_1; }
            set { _t73302_ddd_telefone_1 = value; }
        }
        public string t73302_telefone_1
        {
            get { return _t73302_telefone_1; }
            set { _t73302_telefone_1 = value; }
        }
        public string t73302_ddd_telefone_2
        {
            get { return _t73302_ddd_telefone_2; }
            set { _t73302_ddd_telefone_2 = value; }
        }
        public string t73302_telefone_2
        {
            get { return _t73302_telefone_2; }
            set { _t73302_telefone_2 = value; }
        }
        public string t73302_ddd_fax
        {
            get { return _t73302_ddd_fax; }
            set { _t73302_ddd_fax = value; }
        }
        public string t73302_fax
        {
            get { return _t73302_fax; }
            set { _t73302_fax = value; }
        }
        public string t73302_correio_eletronico
        {
            get { return _t73302_correio_eletronico; }
            set { _t73302_correio_eletronico = value; }
        }
        public string t73302_caixa_postal
        {
            get { return _t73302_caixa_postal; }
            set { _t73302_caixa_postal = value; }
        }
        public string t73302_cep_caixa_postal
        {
            get { return _t73302_cep_caixa_postal; }
            set { _t73302_cep_caixa_postal = value; }
        }
        public string t73302_porte_empresa
        {
            get { return _t73302_porte_empresa; }
            set { _t73302_porte_empresa = value; }
        }
        public string t73302_nat_juridica
        {
            get { return _t73302_nat_juridica; }
            set { _t73302_nat_juridica = value; }
        }
        public string t73302_objeto_social
        {
            get { return _t73302_objeto_social; }
            set { _t73302_objeto_social = value; }
        }
        public string t73302_cnae_principal
        {
            get { return _t73302_cnae_principal; }
            set { _t73302_cnae_principal = value; }
        }
        #endregion

        #region Implementes
        public void Delete()
        {
            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(" Delete    from t73302_dbe_fcpj ");
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


            Sql.AppendLine(" Insert into t73302_dbe_fcpj");
            Sql.AppendLine("  (");
            Sql.AppendLine("	t73300_id_control, ");
            Sql.AppendLine("	t73302_nom_empresarial, ");
            Sql.AppendLine("	t73302_nom_fantasia, ");
            Sql.AppendLine("	t73302_tip_org_registro, ");
            Sql.AppendLine("	t73302_nire, ");
            Sql.AppendLine("	t73302_capital_social, ");
            Sql.AppendLine("	t73302_cnpj_estab_matriz, ");
            Sql.AppendLine("	t73302_tip_logradouro, ");
            Sql.AppendLine("	t73302_nom_logradouro, ");
            Sql.AppendLine("	t73302_num_logradouro, ");
            Sql.AppendLine("	t73302_complemento_logradouro, ");
            Sql.AppendLine("	t73302_bairro, ");
            Sql.AppendLine("	t73302_distrito, ");
            Sql.AppendLine("	t73302_cod_munic, ");
            Sql.AppendLine("	t73302_uf, ");
            Sql.AppendLine("	t73302_cep, ");
            Sql.AppendLine("	t73302_referencia, ");
            Sql.AppendLine("	t73302_cod_pais, ");
            Sql.AppendLine("	t73302_cidade_exterior, ");
            Sql.AppendLine("	t73302_ddd_telefone_1, ");
            Sql.AppendLine("	t73302_telefone_1, ");
            Sql.AppendLine("	t73302_ddd_telefone_2, ");
            Sql.AppendLine("	t73302_telefone_2, ");
            Sql.AppendLine("	t73302_ddd_fax, ");
            Sql.AppendLine("	t73302_fax, ");
            Sql.AppendLine("	t73302_correio_eletronico, ");
            Sql.AppendLine("	t73302_caixa_postal, ");
            Sql.AppendLine("	t73302_cep_caixa_postal, ");
            Sql.AppendLine("	t73302_porte_empresa, ");
            Sql.AppendLine("	t73302_nat_juridica, ");
            Sql.AppendLine("	t73302_objeto_social, ");
            Sql.AppendLine("	t73302_cnae_principal,");
            Sql.AppendLine("	t73302_cod_tipo_unidade ");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_t73300_id_control, ");
            Sql.AppendLine("	@v_t73302_nom_empresarial, ");
            Sql.AppendLine("	@v_t73302_nom_fantasia, ");
            Sql.AppendLine("	@v_t73302_tip_org_registro, ");
            Sql.AppendLine("	@v_t73302_nire, ");
            Sql.AppendLine("	@v_t73302_capital_social, ");
            Sql.AppendLine("	@v_t73302_cnpj_estab_matriz, ");
            Sql.AppendLine("	@v_t73302_tip_logradouro, ");
            Sql.AppendLine("	@v_t73302_nom_logradouro, ");
            Sql.AppendLine("	@v_t73302_num_logradouro, ");
            Sql.AppendLine("	@v_t73302_complemento_logradouro, ");
            Sql.AppendLine("	@v_t73302_bairro, ");
            Sql.AppendLine("	@v_t73302_distrito, ");
            Sql.AppendLine("	@v_t73302_cod_munic, ");
            Sql.AppendLine("	@v_t73302_uf, ");
            Sql.AppendLine("	@v_t73302_cep, ");
            Sql.AppendLine("	@v_t73302_referencia, ");
            Sql.AppendLine("	@v_t73302_cod_pais, ");
            Sql.AppendLine("	@v_t73302_cidade_exterior, ");
            Sql.AppendLine("	@v_t73302_ddd_telefone_1, ");
            Sql.AppendLine("	@v_t73302_telefone_1, ");
            Sql.AppendLine("	@v_t73302_ddd_telefone_2, ");
            Sql.AppendLine("	@v_t73302_telefone_2, ");
            Sql.AppendLine("	@v_t73302_ddd_fax, ");
            Sql.AppendLine("	@v_t73302_fax, ");
            Sql.AppendLine("	@v_t73302_correio_eletronico, ");
            Sql.AppendLine("	@v_t73302_caixa_postal, ");
            Sql.AppendLine("	@v_t73302_cep_caixa_postal, ");
            Sql.AppendLine("	@v_t73302_porte_empresa, ");
            Sql.AppendLine("	@v_t73302_nat_juridica, ");
            Sql.AppendLine("	@v_t73302_objeto_social, ");
            Sql.AppendLine("	@v_t73302_cnae_principal,");
            Sql.AppendLine("	@v_t73302_cod_tipo_unidade ");
            Sql.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T73302_DBE_FCPJ Set ");
            SqlU.AppendLine("		t73302_nom_empresarial = @v_t73302_nom_empresarial, ");
            SqlU.AppendLine("		t73302_nom_fantasia = @v_t73302_nom_fantasia, ");
            SqlU.AppendLine("		t73302_tip_org_registro = @v_t73302_tip_org_registro, ");
            SqlU.AppendLine("		t73302_nire = @v_t73302_nire, ");
            SqlU.AppendLine("		t73302_capital_social = @v_t73302_capital_social, ");
            SqlU.AppendLine("		t73302_cnpj_estab_matriz = @v_t73302_cnpj_estab_matriz, ");
            SqlU.AppendLine("		t73302_tip_logradouro = @v_t73302_tip_logradouro, ");
            SqlU.AppendLine("		t73302_nom_logradouro = @v_t73302_nom_logradouro, ");
            SqlU.AppendLine("		t73302_num_logradouro = @v_t73302_num_logradouro, ");
            SqlU.AppendLine("		t73302_complemento_logradouro = @v_t73302_complemento_logradouro, ");
            SqlU.AppendLine("		t73302_bairro = @v_t73302_bairro, ");
            SqlU.AppendLine("		t73302_distrito = @v_t73302_distrito, ");
            SqlU.AppendLine("		t73302_cod_munic = @v_t73302_cod_munic, ");
            SqlU.AppendLine("		t73302_uf = @v_t73302_uf, ");
            SqlU.AppendLine("		t73302_cep = @v_t73302_cep, ");
            SqlU.AppendLine("		t73302_referencia = @v_t73302_referencia, ");
            SqlU.AppendLine("		t73302_cod_pais = @v_t73302_cod_pais, ");
            SqlU.AppendLine("		t73302_cidade_exterior = @v_t73302_cidade_exterior, ");
            SqlU.AppendLine("		t73302_ddd_telefone_1 = @v_t73302_ddd_telefone_1, ");
            SqlU.AppendLine("		t73302_telefone_1 = @v_t73302_telefone_1, ");
            SqlU.AppendLine("		t73302_ddd_telefone_2 = @v_t73302_ddd_telefone_2, ");
            SqlU.AppendLine("		t73302_telefone_2 = @v_t73302_telefone_2, ");
            SqlU.AppendLine("		t73302_ddd_fax = @v_t73302_ddd_fax, ");
            SqlU.AppendLine("		t73302_fax = @v_t73302_fax, ");
            SqlU.AppendLine("		t73302_correio_eletronico = @v_t73302_correio_eletronico, ");
            SqlU.AppendLine("		t73302_caixa_postal = @v_t73302_caixa_postal, ");
            SqlU.AppendLine("		t73302_cep_caixa_postal = @v_t73302_cep_caixa_postal, ");
            SqlU.AppendLine("		t73302_porte_empresa = @v_t73302_porte_empresa, ");
            SqlU.AppendLine("		t73302_nat_juridica = @v_t73302_nat_juridica, ");
            SqlU.AppendLine("		t73302_objeto_social = @v_t73302_objeto_social, ");
            SqlU.AppendLine("		t73302_cnae_principal = @v_t73302_cnae_principal,");
            SqlU.AppendLine("	    t73302_cod_tipo_unidade = @v_t73302_cod_tipo_unidade");
            SqlU.AppendLine(" Where	t73300_id_control = @v_t73300_id_control ");

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_id_control", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_id_control));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_nom_empresarial", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_nom_empresarial));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_nom_fantasia", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_nom_fantasia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_tip_org_registro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_tip_org_registro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_nire", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_nire));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_capital_social", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 22, 2, "", DataRowVersion.Proposed, _t73302_capital_social));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_cnpj_estab_matriz", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_cnpj_estab_matriz));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_tip_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_tip_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_nom_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_nom_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_num_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_num_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_complemento_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_complemento_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_bairro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_bairro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_distrito", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_distrito));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_cod_munic", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_cod_munic));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_cep", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_cep));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_referencia", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_referencia));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_cod_pais", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_cod_pais));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_cidade_exterior", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_cidade_exterior));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_ddd_telefone_1", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_ddd_telefone_1));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_telefone_1", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_telefone_1));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_ddd_telefone_2", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_ddd_telefone_2));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_telefone_2", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_telefone_2));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_ddd_fax", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_ddd_fax));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_fax", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_fax));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_correio_eletronico", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_correio_eletronico));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_caixa_postal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_caixa_postal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_cep_caixa_postal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_cep_caixa_postal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_porte_empresa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_porte_empresa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_nat_juridica", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_nat_juridica));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_objeto_social", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_objeto_social));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_cnae_principal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_cnae_principal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73302_cod_tipo_unidade", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73302_cod_tipo_unidade));


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
