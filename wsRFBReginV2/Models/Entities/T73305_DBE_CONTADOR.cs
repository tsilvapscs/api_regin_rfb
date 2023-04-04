using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using psc.Receita.ConnectionBase;

namespace psc.Receita.Entities
{
    class T73305_DBE_CONTADOR : DBInteractionBase
    {

        #region  Property Declarations
        protected decimal _t73300_id_control;
        protected string _t73305_nom_contador;
        protected string _t73305_tip_contador;
        protected string _t73305_uf_crc;
        protected string _t73305_seq_crc;
        protected string _t73305_tip_crc;
        protected string _t73305_cpf_cnpj_contador;
        protected string _t73305_tip_logradouro;
        protected string _t73305_nom_logradouro;
        protected string _t73305_num_logradouro;
        protected string _t73305_complemento_logradouro;
        protected string _t73305_bairro;
        protected string _t73305_distrito;
        protected string _t73305_cod_munic;
        protected string _t73305_uf;
        protected string _t73305_cep;
        protected string _t73305_ddd_telefone;
        protected string _t73305_telefone;
        protected string _t73305_proc_eletr_dados;
        protected string _t73305_util_ecf;
        protected string _t73305_ddd_fax;
        protected string _t73305_fax;
        protected string _t73305_correio_eletronico;
        protected string _t73305_perman_livro_fiscal;
        protected string _t73305_opcao_livro_eletronico;
        protected string _t73305_opcao_doc_eletronico;
        protected string _t73305_cod_classificacao;
        protected DateTime _t73305_dat_registro_crc;
        protected string _t73305_ident_contador;
        protected string _t73305_orgao_emissor_id;
        protected string _t73305_uf_orgao_emis;
        protected DateTime _t73305_dat_emissao_id;

        protected string _t73305_uf_crc_responsavel = "";
        protected string _t73305_cpf_responsavel = "";
        protected string _t73305_seq_crc_responsavel = "";
        protected string _t73305_tip_crc_responsavel = "";

        protected string _t73305_cod_classific_contabilista = "";
        protected string _t73305_cod_classific_empresa = "";


        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string t73305_cod_classific_contabilista
        {
            get { return _t73305_cod_classific_contabilista; }
            set { _t73305_cod_classific_contabilista = value; }
        }

        public string t73305_cod_classific_empresa
        {
            get { return _t73305_cod_classific_empresa; }
            set { _t73305_cod_classific_empresa = value; }
        }


        public string t73305_cpf_responsavel
        {
            get { return _t73305_cpf_responsavel; }
            set { _t73305_cpf_responsavel = value; }
        }

        public string t73305_seq_crc_responsavel
        {
            get { return _t73305_seq_crc_responsavel; }
            set { _t73305_seq_crc_responsavel = value; }
        }

        public string t73305_tip_crc_responsavel
        {
            get { return _t73305_tip_crc_responsavel; }
            set { _t73305_tip_crc_responsavel = value; }
        }


        public string t73305_uf_crc_responsavel
        {
            get { return _t73305_uf_crc_responsavel; }
            set { _t73305_uf_crc_responsavel = value; }
        }


        public decimal t73300_id_control
        {
            get { return _t73300_id_control; }
            set { _t73300_id_control = value; }
        }
        public string t73305_nom_contador
        {
            get { return _t73305_nom_contador; }
            set { _t73305_nom_contador = value; }
        }
        public string t73305_tip_contador
        {
            get { return _t73305_tip_contador; }
            set { _t73305_tip_contador = value; }
        }
        public string t73305_uf_crc
        {
            get { return _t73305_uf_crc; }
            set { _t73305_uf_crc = value; }
        }
        public string t73305_seq_crc
        {
            get { return _t73305_seq_crc; }
            set { _t73305_seq_crc = value; }
        }
        public string t73305_tip_crc
        {
            get { return _t73305_tip_crc; }
            set { _t73305_tip_crc = value; }
        }
        public string t73305_cpf_cnpj_contador
        {
            get { return _t73305_cpf_cnpj_contador; }
            set { _t73305_cpf_cnpj_contador = value; }
        }
        public string t73305_tip_logradouro
        {
            get { return _t73305_tip_logradouro; }
            set { _t73305_tip_logradouro = value; }
        }
        public string t73305_nom_logradouro
        {
            get { return _t73305_nom_logradouro; }
            set { _t73305_nom_logradouro = value; }
        }
        public string t73305_num_logradouro
        {
            get { return _t73305_num_logradouro; }
            set { _t73305_num_logradouro = value; }
        }
        public string t73305_complemento_logradouro
        {
            get { return _t73305_complemento_logradouro; }
            set { _t73305_complemento_logradouro = value; }
        }
        public string t73305_bairro
        {
            get { return _t73305_bairro; }
            set { _t73305_bairro = value; }
        }
        public string t73305_distrito
        {
            get { return _t73305_distrito; }
            set { _t73305_distrito = value; }
        }
        public string t73305_cod_munic
        {
            get { return _t73305_cod_munic; }
            set { _t73305_cod_munic = value; }
        }
        public string t73305_uf
        {
            get { return _t73305_uf; }
            set { _t73305_uf = value; }
        }
        public string t73305_cep
        {
            get { return _t73305_cep; }
            set { _t73305_cep = value; }
        }
        public string t73305_ddd_telefone
        {
            get { return _t73305_ddd_telefone; }
            set { _t73305_ddd_telefone = value; }
        }
        public string t73305_telefone
        {
            get { return _t73305_telefone; }
            set { _t73305_telefone = value; }
        }
        public string t73305_proc_eletr_dados
        {
            get { return _t73305_proc_eletr_dados; }
            set { _t73305_proc_eletr_dados = value; }
        }
        public string t73305_util_ecf
        {
            get { return _t73305_util_ecf; }
            set { _t73305_util_ecf = value; }
        }
        public string t73305_ddd_fax
        {
            get { return _t73305_ddd_fax; }
            set { _t73305_ddd_fax = value; }
        }
        public string t73305_fax
        {
            get { return _t73305_fax; }
            set { _t73305_fax = value; }
        }
        public string t73305_correio_eletronico
        {
            get { return _t73305_correio_eletronico; }
            set { _t73305_correio_eletronico = value; }
        }
        public string t73305_perman_livro_fiscal
        {
            get { return _t73305_perman_livro_fiscal; }
            set { _t73305_perman_livro_fiscal = value; }
        }
        public string t73305_opcao_livro_eletronico
        {
            get { return _t73305_opcao_livro_eletronico; }
            set { _t73305_opcao_livro_eletronico = value; }
        }
        public string t73305_opcao_doc_eletronico
        {
            get { return _t73305_opcao_doc_eletronico; }
            set { _t73305_opcao_doc_eletronico = value; }
        }
        public string t73305_cod_classificacao
        {
            get { return _t73305_cod_classificacao; }
            set { _t73305_cod_classificacao = value; }
        }
        public DateTime t73305_dat_registro_crc
        {
            get { return _t73305_dat_registro_crc; }
            set { _t73305_dat_registro_crc = value; }
        }
        public string t73305_ident_contador
        {
            get { return _t73305_ident_contador; }
            set { _t73305_ident_contador = value; }
        }
        public string t73305_orgao_emissor_id
        {
            get { return _t73305_orgao_emissor_id; }
            set { _t73305_orgao_emissor_id = value; }
        }
        public string t73305_uf_orgao_emis
        {
            get { return _t73305_uf_orgao_emis; }
            set { _t73305_uf_orgao_emis = value; }
        }
        public DateTime t73305_dat_emissao_id
        {
            get { return _t73305_dat_emissao_id; }
            set { _t73305_dat_emissao_id = value; }
        }
        #endregion

        #region Implements
        public void Delete()
        {
            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(" Delete    from t73305_dbe_contador ");
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


            Sql.AppendLine(" Insert into t73305_dbe_contador");
            Sql.AppendLine("  (");
            Sql.AppendLine("	t73300_id_control, ");
            Sql.AppendLine("	t73305_nom_contador, ");
            Sql.AppendLine("	t73305_tip_contador, ");
            Sql.AppendLine("	t73305_uf_crc, ");
            Sql.AppendLine("	t73305_seq_crc, ");
            Sql.AppendLine("	t73305_tip_crc, ");
            Sql.AppendLine("	t73305_cpf_cnpj_contador, ");
            Sql.AppendLine("	t73305_tip_logradouro, ");
            Sql.AppendLine("	t73305_nom_logradouro, ");
            Sql.AppendLine("	t73305_num_logradouro, ");
            Sql.AppendLine("	t73305_complemento_logradouro, ");
            Sql.AppendLine("	t73305_bairro, ");
            Sql.AppendLine("	t73305_distrito, ");
            Sql.AppendLine("	t73305_cod_munic, ");
            Sql.AppendLine("	t73305_uf, ");
            Sql.AppendLine("	t73305_cep, ");
            Sql.AppendLine("	t73305_ddd_telefone, ");
            Sql.AppendLine("	t73305_telefone, ");
            Sql.AppendLine("	t73305_proc_eletr_dados, ");
            Sql.AppendLine("	t73305_util_ecf, ");
            Sql.AppendLine("	t73305_ddd_fax, ");
            Sql.AppendLine("	t73305_fax, ");
            Sql.AppendLine("	t73305_correio_eletronico, ");
            Sql.AppendLine("	t73305_perman_livro_fiscal, ");
            Sql.AppendLine("	t73305_opcao_livro_eletronico, ");
            Sql.AppendLine("	t73305_opcao_doc_eletronico, ");
            Sql.AppendLine("	t73305_cod_classificacao, ");
            Sql.AppendLine("	t73305_dat_registro_crc, ");
            Sql.AppendLine("	t73305_ident_contador, ");
            Sql.AppendLine("	t73305_orgao_emissor_id, ");
            Sql.AppendLine("	t73305_uf_orgao_emis, ");
            Sql.AppendLine("	t73305_dat_emissao_id,");

            Sql.AppendLine("	t73305_cpf_responsavel, ");// = Global.valNuloBranco(DadosDbe.fcpj.cpfContadorPF);
            Sql.AppendLine("	t73305_uf_crc_responsavel, ");// = Global.valNuloBranco(DadosDbe.fcpj.ufContadorPF);
            Sql.AppendLine("	t73305_seq_crc_responsavel, ");// = Global.valNuloBranco(DadosDbe.fcpj.codTipoCRCcontadorPF);
            Sql.AppendLine("	t73305_tip_crc_responsavel, ");// = Global.valNuloBranco(DadosDbe.fcpj.numSeqContadorPF);
            Sql.AppendLine("	t73305_cod_classific_empresa, ");
            Sql.AppendLine("	t73305_cod_classific_contabilista ");


            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_t73300_id_control, ");
            Sql.AppendLine("	@v_t73305_nom_contador, ");
            Sql.AppendLine("	@v_t73305_tip_contador, ");
            Sql.AppendLine("	@v_t73305_uf_crc, ");
            Sql.AppendLine("	@v_t73305_seq_crc, ");
            Sql.AppendLine("	@v_t73305_tip_crc, ");
            Sql.AppendLine("	@v_t73305_cpf_cnpj_contador, ");
            Sql.AppendLine("	@v_t73305_tip_logradouro, ");
            Sql.AppendLine("	@v_t73305_nom_logradouro, ");
            Sql.AppendLine("	@v_t73305_num_logradouro, ");
            Sql.AppendLine("	@v_t73305_complemento_logradouro, ");
            Sql.AppendLine("	@v_t73305_bairro, ");
            Sql.AppendLine("	@v_t73305_distrito, ");
            Sql.AppendLine("	@v_t73305_cod_munic, ");
            Sql.AppendLine("	@v_t73305_uf, ");
            Sql.AppendLine("	@v_t73305_cep, ");
            Sql.AppendLine("	@v_t73305_ddd_telefone, ");
            Sql.AppendLine("	@v_t73305_telefone, ");
            Sql.AppendLine("	@v_t73305_proc_eletr_dados, ");
            Sql.AppendLine("	@v_t73305_util_ecf, ");
            Sql.AppendLine("	@v_t73305_ddd_fax, ");
            Sql.AppendLine("	@v_t73305_fax, ");
            Sql.AppendLine("	@v_t73305_correio_eletronico, ");
            Sql.AppendLine("	@v_t73305_perman_livro_fiscal, ");
            Sql.AppendLine("	@v_t73305_opcao_livro_eletronico, ");
            Sql.AppendLine("	@v_t73305_opcao_doc_eletronico, ");
            Sql.AppendLine("	@v_t73305_cod_classificacao, ");
            Sql.AppendLine("	@v_t73305_dat_registro_crc, ");
            Sql.AppendLine("	@v_t73305_ident_contador, ");
            Sql.AppendLine("	@v_t73305_orgao_emissor_id, ");
            Sql.AppendLine("	@v_t73305_uf_orgao_emis, ");
            Sql.AppendLine("	@v_t73305_dat_emissao_id,");
            Sql.AppendLine("	@v_t73305_cpf_responsavel, ");
            Sql.AppendLine("	@v_t73305_uf_crc_responsavel,");
            Sql.AppendLine("	@v_t73305_seq_crc_responsavel, ");
            Sql.AppendLine("	@v_t73305_tip_crc_responsavel, ");
            Sql.AppendLine("	@v_t73305_cod_classific_empresa, ");
            Sql.AppendLine("	@v_t73305_cod_classific_contabilista ");

            Sql.AppendLine("  )");

            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T73305_DBE_CONTADOR Set ");
            SqlU.AppendLine("		t73305_nom_contador = @v_t73305_nom_contador, ");
            SqlU.AppendLine("		t73305_tip_contador = @v_t73305_tip_contador, ");
            SqlU.AppendLine("		t73305_uf_crc = @v_t73305_uf_crc, ");
            SqlU.AppendLine("		t73305_seq_crc = @v_t73305_seq_crc, ");
            SqlU.AppendLine("		t73305_tip_crc = @v_t73305_tip_crc, ");
            SqlU.AppendLine("		t73305_cpf_cnpj_contador = @v_t73305_cpf_cnpj_contador, ");
            SqlU.AppendLine("		t73305_tip_logradouro = @v_t73305_tip_logradouro, ");
            SqlU.AppendLine("		t73305_nom_logradouro = @v_t73305_nom_logradouro, ");
            SqlU.AppendLine("		t73305_num_logradouro = @v_t73305_num_logradouro, ");
            SqlU.AppendLine("		t73305_complemento_logradouro = @v_t73305_complemento_logradouro, ");
            SqlU.AppendLine("		t73305_bairro = @v_t73305_bairro, ");
            SqlU.AppendLine("		t73305_distrito = @v_t73305_distrito, ");
            SqlU.AppendLine("		t73305_cod_munic = @v_t73305_cod_munic, ");
            SqlU.AppendLine("		t73305_uf = @v_t73305_uf, ");
            SqlU.AppendLine("		t73305_cep = @v_t73305_cep, ");
            SqlU.AppendLine("		t73305_ddd_telefone = @v_t73305_ddd_telefone, ");
            SqlU.AppendLine("		t73305_telefone = @v_t73305_telefone, ");
            SqlU.AppendLine("		t73305_proc_eletr_dados = @v_t73305_proc_eletr_dados, ");
            SqlU.AppendLine("		t73305_util_ecf = @v_t73305_util_ecf, ");
            SqlU.AppendLine("		t73305_ddd_fax = @v_t73305_ddd_fax, ");
            SqlU.AppendLine("		t73305_fax = @v_t73305_fax, ");
            SqlU.AppendLine("		t73305_correio_eletronico = @v_t73305_correio_eletronico, ");
            SqlU.AppendLine("		t73305_perman_livro_fiscal = @v_t73305_perman_livro_fiscal, ");
            SqlU.AppendLine("		t73305_opcao_livro_eletronico = @v_t73305_opcao_livro_eletronico, ");
            SqlU.AppendLine("		t73305_opcao_doc_eletronico = @v_t73305_opcao_doc_eletronico, ");
            SqlU.AppendLine("		t73305_cod_classificacao = @v_t73305_cod_classificacao, ");
            SqlU.AppendLine("		t73305_dat_registro_crc = @v_t73305_dat_registro_crc, ");
            SqlU.AppendLine("		t73305_ident_contador = @v_t73305_ident_contador, ");
            SqlU.AppendLine("		t73305_orgao_emissor_id = @v_t73305_orgao_emissor_id, ");
            SqlU.AppendLine("		t73305_uf_orgao_emis = @v_t73305_uf_orgao_emis, ");
            SqlU.AppendLine("		t73305_dat_emissao_id = @v_t73305_dat_emissao_id,");
            SqlU.AppendLine("		t73305_cpf_responsavel = @v_t73305_cpf_responsavel,");
            SqlU.AppendLine("		t73305_uf_crc_responsavel = @v_t73305_uf_crc_responsavel,");
            SqlU.AppendLine("		t73305_seq_crc_responsavel = @v_t73305_seq_crc_responsavel," );
            SqlU.AppendLine("		t73305_tip_crc_responsavel = @v_t73305_tip_crc_responsavel,");
            SqlU.AppendLine("		t73305_cod_classific_empresa = @v_t73305_cod_classific_empresa,");
            SqlU.AppendLine("		t73305_cod_classific_contabilista = @v_t73305_cod_classific_contabilista");


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
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_nom_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_nom_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_tip_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_tip_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_uf_crc", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_uf_crc));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_seq_crc", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_seq_crc));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_tip_crc", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_tip_crc));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_cpf_cnpj_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_cpf_cnpj_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_tip_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_tip_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_nom_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_nom_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_num_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_num_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_complemento_logradouro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_complemento_logradouro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_bairro", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_bairro));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_distrito", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_distrito));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_cod_munic", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_cod_munic));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_uf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_uf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_cep", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_cep));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_ddd_telefone", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_ddd_telefone));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_telefone", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_telefone));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_proc_eletr_dados", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_proc_eletr_dados));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_util_ecf", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_util_ecf));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_ddd_fax", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_ddd_fax));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_fax", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_fax));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_correio_eletronico", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_correio_eletronico));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_perman_livro_fiscal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_perman_livro_fiscal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_opcao_livro_eletronico", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_opcao_livro_eletronico));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_opcao_doc_eletronico", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_opcao_doc_eletronico));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_cod_classificacao", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_cod_classificacao));


                if (_t73305_dat_registro_crc.Year == 1)
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_dat_registro_crc", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, null));
                }
                else
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_dat_registro_crc", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_dat_registro_crc));
                }

                
                
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_ident_contador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_ident_contador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_orgao_emissor_id", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_orgao_emissor_id));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_uf_orgao_emis", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_uf_orgao_emis));

                if (_t73305_dat_emissao_id.Year == 1)
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_dat_emissao_id", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, null));
                }
                else
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_dat_emissao_id", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_dat_emissao_id));
                }

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_cpf_responsavel", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_cpf_responsavel));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_uf_crc_responsavel", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_uf_crc_responsavel));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_seq_crc_responsavel", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_seq_crc_responsavel));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_tip_crc_responsavel", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_tip_crc_responsavel));

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_cod_classific_empresa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_cod_classific_empresa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73305_cod_classific_contabilista", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73305_cod_classific_contabilista));

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
