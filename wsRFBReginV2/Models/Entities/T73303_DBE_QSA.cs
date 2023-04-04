using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using psc.Receita.ConnectionBase;

namespace psc.Receita.Entities
{
    class T73303_DBE_QSA : DBInteractionBase
    {

        #region  Property Declarations
        protected decimal _t73300_id_control;
        protected string _t73303_cpf_cnpj_qsa = "";
        protected string _t73303_nom_qsa = "";
        protected string _t73303_qualificacao_qsa = "";
        protected string _t73303_tip_lograd_qsa = "";
        protected string _t73303_lograd_qsa = "";
        protected string _t73303_num_lograd_qsa = "";
        protected string _t73303_complemento_lograd_qsa = "";
        protected string _t73303_bairro_qsa = "";
        protected string _t73303_distrito_qsa = "";
        protected string _t73303_cod_munic_qsa = "";
        protected string _t73303_cep_qsa = "";
        protected string _t73303_uf_qsa = "";
        protected string _t73303_ddd_telefone_qsa = "";
        protected string _t73303_telefone_qsa = "";
        protected string _t73303_ddd_fax_qsa = "";
        protected string _t73303_fax_qsa = "";
        protected string _t73303_correio_eletronico_qsa = "";
        protected string _t73303_ind_cpf_cnpj_qsa = "";
        protected string _t73303_cod_evento = "";
        protected DateTime _t73303_dat_evento;
        protected string _t73303_cod_pais = "";
        protected decimal _t73303_perc_partic_qsa = 0;
        protected decimal _t73303_capital_social_qsa = 0;
        protected string _t73303_orig_inf_lograd = "";
        protected string _t73303_nire_qsa = "";
        protected string _t73303_matricula_rcpj = "";
        protected string _t73303_ident_passap_qsa = "";
        protected string _t73303_orgao_emissor_ident = "";
        protected string _t73303_uf_orgao_emissor_ident = "";
        protected DateTime _t73303_dat_emissao_ident;
        protected string _t73303_nacionalidade_qsa = "";
        protected DateTime _t73303_dt_nascimento_socio_pf;
        protected DateTime _t73303_dat_inicio_mandato;
        protected DateTime _t73303_dat_termino_mandato;
        protected string _t73303_uso_firma_administrador = "";
        protected string _t73303_cargo_qsa = "";
        protected string _t73303_cpf_rep_legal = "";
        protected string _t73303_nom_rep_legal = "";
        protected string _t73303_qualificacao_rep_legal = "";
        protected string _t73303_ident_rep_legal = "";
        protected string _t73303_or_emis_ident_rep_legal = "";
        protected string _t73303_uf_or_emissor_rep_legal = "";
        protected DateTime _t73303_dat_emis_ident_rep_lega;
        protected string _t73303_origem_endereco_rep_leg = "";
        protected string _t73303_tip_lograd_rep_legal = "";
        protected string _t73303_lograd_rep_legal = "";
        protected string _t73303_num_lograd_rep_legal = "";
        protected string _t73303_complemento_lograd_rep_ = "";
        protected string _t73303_bairro_rep_legal = "";
        protected string _t73303_distrito_rep_legal = "";
        protected string _t73303_cod_munic_rep_legal = "";
        protected string _t73303_cep_rep_legal = "";
        protected string _t73303_uf_rep_legal = "";
        protected string _t73303_ddd_telefone_rep_legal = "";
        protected string _t73303_telefone_rep_legal = "";
        protected string _t73303_ddd_fax_rep_legal = "";
        protected string _t73303_fax_rep_legal = "";
        protected string _t73303_correio_eletronico_rep_ = "";
        protected decimal _t73303_capital_social_empresa = 0;
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public decimal t73300_id_control
        {
            get { return _t73300_id_control; }
            set { _t73300_id_control = value; }
        }
        public string t73303_cpf_cnpj_qsa
        {
            get { return _t73303_cpf_cnpj_qsa; }
            set { _t73303_cpf_cnpj_qsa = value; }
        }
        public string t73303_nom_qsa
        {
            get { return _t73303_nom_qsa; }
            set { _t73303_nom_qsa = value; }
        }
        public string t73303_qualificacao_qsa
        {
            get { return _t73303_qualificacao_qsa; }
            set { _t73303_qualificacao_qsa = value; }
        }
        public string t73303_tip_lograd_qsa
        {
            get { return _t73303_tip_lograd_qsa; }
            set { _t73303_tip_lograd_qsa = value; }
        }
        public string t73303_lograd_qsa
        {
            get { return _t73303_lograd_qsa; }
            set { _t73303_lograd_qsa = value; }
        }
        public string t73303_num_lograd_qsa
        {
            get { return _t73303_num_lograd_qsa; }
            set { _t73303_num_lograd_qsa = value; }
        }
        public string t73303_complemento_lograd_qsa
        {
            get { return _t73303_complemento_lograd_qsa; }
            set { _t73303_complemento_lograd_qsa = value; }
        }
        public string t73303_bairro_qsa
        {
            get { return _t73303_bairro_qsa; }
            set { _t73303_bairro_qsa = value; }
        }
        public string t73303_distrito_qsa
        {
            get { return _t73303_distrito_qsa; }
            set { _t73303_distrito_qsa = value; }
        }
        public string t73303_cod_munic_qsa
        {
            get { return _t73303_cod_munic_qsa; }
            set { _t73303_cod_munic_qsa = value; }
        }
        public string t73303_cep_qsa
        {
            get { return _t73303_cep_qsa; }
            set { _t73303_cep_qsa = value; }
        }
        public string t73303_uf_qsa
        {
            get { return _t73303_uf_qsa; }
            set { _t73303_uf_qsa = value; }
        }
        public string t73303_ddd_telefone_qsa
        {
            get { return _t73303_ddd_telefone_qsa; }
            set { _t73303_ddd_telefone_qsa = value; }
        }
        public string t73303_telefone_qsa
        {
            get { return _t73303_telefone_qsa; }
            set { _t73303_telefone_qsa = value; }
        }
        public string t73303_ddd_fax_qsa
        {
            get { return _t73303_ddd_fax_qsa; }
            set { _t73303_ddd_fax_qsa = value; }
        }
        public string t73303_fax_qsa
        {
            get { return _t73303_fax_qsa; }
            set { _t73303_fax_qsa = value; }
        }
        public string t73303_correio_eletronico_qsa
        {
            get { return _t73303_correio_eletronico_qsa; }
            set { _t73303_correio_eletronico_qsa = value; }
        }
        public string t73303_ind_cpf_cnpj_qsa
        {
            get { return _t73303_ind_cpf_cnpj_qsa; }
            set { _t73303_ind_cpf_cnpj_qsa = value; }
        }
        public string t73303_cod_evento
        {
            get { return _t73303_cod_evento; }
            set { _t73303_cod_evento = value; }
        }
        public DateTime t73303_dat_evento
        {
            get { return _t73303_dat_evento; }
            set { _t73303_dat_evento = value; }
        }
        public string t73303_cod_pais
        {
            get { return _t73303_cod_pais; }
            set { _t73303_cod_pais = value; }
        }
        public decimal t73303_perc_partic_qsa
        {
            get { return _t73303_perc_partic_qsa; }
            set { _t73303_perc_partic_qsa = value; }
        }
        public decimal t73303_capital_social_qsa
        {
            get { return _t73303_capital_social_qsa; }
            set { _t73303_capital_social_qsa = value; }
        }
        public string t73303_orig_inf_lograd
        {
            get { return _t73303_orig_inf_lograd; }
            set { _t73303_orig_inf_lograd = value; }
        }
        public string t73303_nire_qsa
        {
            get { return _t73303_nire_qsa; }
            set { _t73303_nire_qsa = value; }
        }
        public string t73303_matricula_rcpj
        {
            get { return _t73303_matricula_rcpj; }
            set { _t73303_matricula_rcpj = value; }
        }
        public string t73303_ident_passap_qsa
        {
            get { return _t73303_ident_passap_qsa; }
            set { _t73303_ident_passap_qsa = value; }
        }
        public string t73303_orgao_emissor_ident
        {
            get { return _t73303_orgao_emissor_ident; }
            set { _t73303_orgao_emissor_ident = value; }
        }
        public string t73303_uf_orgao_emissor_ident
        {
            get { return _t73303_uf_orgao_emissor_ident; }
            set { _t73303_uf_orgao_emissor_ident = value; }
        }
        public DateTime t73303_dat_emissao_ident
        {
            get { return _t73303_dat_emissao_ident; }
            set { _t73303_dat_emissao_ident = value; }
        }
        public string t73303_nacionalidade_qsa
        {
            get { return _t73303_nacionalidade_qsa; }
            set { _t73303_nacionalidade_qsa = value; }
        }
        public DateTime t73303_dt_nascimento_socio_pf
        {
            get { return _t73303_dt_nascimento_socio_pf; }
            set { _t73303_dt_nascimento_socio_pf = value; }
        }
        public DateTime t73303_dat_inicio_mandato
        {
            get { return _t73303_dat_inicio_mandato; }
            set { _t73303_dat_inicio_mandato = value; }
        }
        public DateTime t73303_dat_termino_mandato
        {
            get { return _t73303_dat_termino_mandato; }
            set { _t73303_dat_termino_mandato = value; }
        }
        public string t73303_uso_firma_administrador
        {
            get { return _t73303_uso_firma_administrador; }
            set { _t73303_uso_firma_administrador = value; }
        }
        public string t73303_cargo_qsa
        {
            get { return _t73303_cargo_qsa; }
            set { _t73303_cargo_qsa = value; }
        }
        public string t73303_cpf_rep_legal
        {
            get { return _t73303_cpf_rep_legal; }
            set { _t73303_cpf_rep_legal = value; }
        }
        public string t73303_nom_rep_legal
        {
            get { return _t73303_nom_rep_legal; }
            set { _t73303_nom_rep_legal = value; }
        }
        public string t73303_qualificacao_rep_legal
        {
            get { return _t73303_qualificacao_rep_legal; }
            set { _t73303_qualificacao_rep_legal = value; }
        }
        public string t73303_ident_rep_legal
        {
            get { return _t73303_ident_rep_legal; }
            set { _t73303_ident_rep_legal = value; }
        }
        public string t73303_or_emis_ident_rep_legal
        {
            get { return _t73303_or_emis_ident_rep_legal; }
            set { _t73303_or_emis_ident_rep_legal = value; }
        }
        public string t73303_uf_or_emissor_rep_legal
        {
            get { return _t73303_uf_or_emissor_rep_legal; }
            set { _t73303_uf_or_emissor_rep_legal = value; }
        }
        public DateTime t73303_dat_emis_ident_rep_lega
        {
            get { return _t73303_dat_emis_ident_rep_lega; }
            set { _t73303_dat_emis_ident_rep_lega = value; }
        }
        public string t73303_origem_endereco_rep_leg
        {
            get { return _t73303_origem_endereco_rep_leg; }
            set { _t73303_origem_endereco_rep_leg = value; }
        }
        public string t73303_tip_lograd_rep_legal
        {
            get { return _t73303_tip_lograd_rep_legal; }
            set { _t73303_tip_lograd_rep_legal = value; }
        }
        public string t73303_lograd_rep_legal
        {
            get { return _t73303_lograd_rep_legal; }
            set { _t73303_lograd_rep_legal = value; }
        }
        public string t73303_num_lograd_rep_legal
        {
            get { return _t73303_num_lograd_rep_legal; }
            set { _t73303_num_lograd_rep_legal = value; }
        }
        public string t73303_complemento_lograd_rep_
        {
            get { return _t73303_complemento_lograd_rep_; }
            set { _t73303_complemento_lograd_rep_ = value; }
        }
        public string t73303_bairro_rep_legal
        {
            get { return _t73303_bairro_rep_legal; }
            set { _t73303_bairro_rep_legal = value; }
        }
        public string t73303_distrito_rep_legal
        {
            get { return _t73303_distrito_rep_legal; }
            set { _t73303_distrito_rep_legal = value; }
        }
        public string t73303_cod_munic_rep_legal
        {
            get { return _t73303_cod_munic_rep_legal; }
            set { _t73303_cod_munic_rep_legal = value; }
        }
        public string t73303_cep_rep_legal
        {
            get { return _t73303_cep_rep_legal; }
            set { _t73303_cep_rep_legal = value; }
        }
        public string t73303_uf_rep_legal
        {
            get { return _t73303_uf_rep_legal; }
            set { _t73303_uf_rep_legal = value; }
        }
        public string t73303_ddd_telefone_rep_legal
        {
            get { return _t73303_ddd_telefone_rep_legal; }
            set { _t73303_ddd_telefone_rep_legal = value; }
        }
        public string t73303_telefone_rep_legal
        {
            get { return _t73303_telefone_rep_legal; }
            set { _t73303_telefone_rep_legal = value; }
        }
        public string t73303_ddd_fax_rep_legal
        {
            get { return _t73303_ddd_fax_rep_legal; }
            set { _t73303_ddd_fax_rep_legal = value; }
        }
        public string t73303_fax_rep_legal
        {
            get { return _t73303_fax_rep_legal; }
            set { _t73303_fax_rep_legal = value; }
        }
        public string t73303_correio_eletronico_rep_
        {
            get { return _t73303_correio_eletronico_rep_; }
            set { _t73303_correio_eletronico_rep_ = value; }
        }
        public decimal t73303_capital_social_empresa
        {
            get { return _t73303_capital_social_empresa; }
            set { _t73303_capital_social_empresa = value; }
        }
        #endregion 

        #region Implementes
        public void Delete()
        {
            StringBuilder SqlU = new StringBuilder();

            SqlU.AppendLine(" Delete    from T73303_DBE_QSA ");
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


            Sql.AppendLine(" Insert into t73303_dbe_qsa");
            Sql.AppendLine("  (");
            Sql.AppendLine("	t73300_id_control, ");
            Sql.AppendLine("	t73303_cpf_cnpj_qsa, ");
            Sql.AppendLine("	t73303_nom_qsa, ");
            Sql.AppendLine("	t73303_qualificacao_qsa, ");
            Sql.AppendLine("	t73303_tip_lograd_qsa, ");
            Sql.AppendLine("	t73303_lograd_qsa, ");
            Sql.AppendLine("	t73303_num_lograd_qsa, ");
            Sql.AppendLine("	t73303_complemento_lograd_qsa, ");
            Sql.AppendLine("	t73303_bairro_qsa, ");
            Sql.AppendLine("	t73303_distrito_qsa, ");
            Sql.AppendLine("	t73303_cod_munic_qsa, ");
            Sql.AppendLine("	t73303_cep_qsa, ");
            Sql.AppendLine("	t73303_uf_qsa, ");
            Sql.AppendLine("	t73303_ddd_telefone_qsa, ");
            Sql.AppendLine("	t73303_telefone_qsa, ");
            Sql.AppendLine("	t73303_ddd_fax_qsa, ");
            Sql.AppendLine("	t73303_fax_qsa, ");
            Sql.AppendLine("	t73303_correio_eletronico_qsa, ");
            Sql.AppendLine("	t73303_ind_cpf_cnpj_qsa, ");
            Sql.AppendLine("	t73303_cod_evento, ");
            Sql.AppendLine("	t73303_dat_evento, ");
            Sql.AppendLine("	t73303_cod_pais, ");
            Sql.AppendLine("	t73303_perc_partic_qsa, ");
            Sql.AppendLine("	t73303_capital_social_qsa, ");
            Sql.AppendLine("	t73303_orig_inf_lograd, ");
            Sql.AppendLine("	t73303_nire_qsa, ");
            Sql.AppendLine("	t73303_matricula_rcpj, ");
            Sql.AppendLine("	t73303_ident_passap_qsa, ");
            Sql.AppendLine("	t73303_orgao_emissor_ident, ");
            Sql.AppendLine("	t73303_uf_orgao_emissor_ident, ");
            Sql.AppendLine("	t73303_dat_emissao_ident, ");
            Sql.AppendLine("	t73303_nacionalidade_qsa, ");
            Sql.AppendLine("	t73303_dt_nascimento_socio_pf, ");
            Sql.AppendLine("	t73303_dat_inicio_mandato, ");
            Sql.AppendLine("	t73303_dat_termino_mandato, ");
            Sql.AppendLine("	t73303_uso_firma_administrador, ");
            Sql.AppendLine("	t73303_cargo_qsa, ");
            Sql.AppendLine("	t73303_cpf_rep_legal, ");
            Sql.AppendLine("	t73303_nom_rep_legal, ");
            Sql.AppendLine("	t73303_qualificacao_rep_legal, ");
            Sql.AppendLine("	t73303_ident_rep_legal, ");
            Sql.AppendLine("	t73303_or_emis_ident_rep_legal, ");
            Sql.AppendLine("	t73303_uf_or_emissor_rep_legal, ");
            Sql.AppendLine("	t73303_dat_emis_ident_rep_lega, ");
            Sql.AppendLine("	t73303_origem_endereco_rep_leg, ");
            Sql.AppendLine("	t73303_tip_lograd_rep_legal, ");
            Sql.AppendLine("	t73303_lograd_rep_legal, ");
            Sql.AppendLine("	t73303_num_lograd_rep_legal, ");
            Sql.AppendLine("	t73303_complemento_lograd_rep_, ");
            Sql.AppendLine("	t73303_bairro_rep_legal, ");
            Sql.AppendLine("	t73303_distrito_rep_legal, ");
            Sql.AppendLine("	t73303_cod_munic_rep_legal, ");
            Sql.AppendLine("	t73303_cep_rep_legal, ");
            Sql.AppendLine("	t73303_uf_rep_legal, ");
            Sql.AppendLine("	t73303_ddd_telefone_rep_legal, ");
            Sql.AppendLine("	t73303_telefone_rep_legal, ");
            Sql.AppendLine("	t73303_ddd_fax_rep_legal, ");
            Sql.AppendLine("	t73303_fax_rep_legal, ");
            Sql.AppendLine("	t73303_correio_eletronico_rep_, ");
            Sql.AppendLine("	t73303_capital_social_empresa");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_t73300_id_control, ");
            Sql.AppendLine("	@v_t73303_cpf_cnpj_qsa, ");
            Sql.AppendLine("	@v_t73303_nom_qsa, ");
            Sql.AppendLine("	@v_t73303_qualificacao_qsa, ");
            Sql.AppendLine("	@v_t73303_tip_lograd_qsa, ");
            Sql.AppendLine("	@v_t73303_lograd_qsa, ");
            Sql.AppendLine("	@v_t73303_num_lograd_qsa, ");
            Sql.AppendLine("	@v_t73303_complemento_lograd_qsa, ");
            Sql.AppendLine("	@v_t73303_bairro_qsa, ");
            Sql.AppendLine("	@v_t73303_distrito_qsa, ");
            Sql.AppendLine("	@v_t73303_cod_munic_qsa, ");
            Sql.AppendLine("	@v_t73303_cep_qsa, ");
            Sql.AppendLine("	@v_t73303_uf_qsa, ");
            Sql.AppendLine("	@v_t73303_ddd_telefone_qsa, ");
            Sql.AppendLine("	@v_t73303_telefone_qsa, ");
            Sql.AppendLine("	@v_t73303_ddd_fax_qsa, ");
            Sql.AppendLine("	@v_t73303_fax_qsa, ");
            Sql.AppendLine("	@v_t73303_correio_eletronico_qsa, ");
            Sql.AppendLine("	@v_t73303_ind_cpf_cnpj_qsa, ");
            Sql.AppendLine("	@v_t73303_cod_evento, ");
            Sql.AppendLine("	@v_t73303_dat_evento, ");
            Sql.AppendLine("	@v_t73303_cod_pais, ");
            Sql.AppendLine("	@v_t73303_perc_partic_qsa, ");
            Sql.AppendLine("	@v_t73303_capital_social_qsa, ");
            Sql.AppendLine("	@v_t73303_orig_inf_lograd, ");
            Sql.AppendLine("	@v_t73303_nire_qsa, ");
            Sql.AppendLine("	@v_t73303_matricula_rcpj, ");
            Sql.AppendLine("	@v_t73303_ident_passap_qsa, ");
            Sql.AppendLine("	@v_t73303_orgao_emissor_ident, ");
            Sql.AppendLine("	@v_t73303_uf_orgao_emissor_ident, ");
            Sql.AppendLine("	@v_t73303_dat_emissao_ident, ");
            Sql.AppendLine("	@v_t73303_nacionalidade_qsa, ");
            Sql.AppendLine("	@v_t73303_dt_nascimento_socio_pf, ");
            Sql.AppendLine("	@v_t73303_dat_inicio_mandato, ");
            Sql.AppendLine("	@v_t73303_dat_termino_mandato, ");
            Sql.AppendLine("	@v_t73303_uso_firma_administrador, ");
            Sql.AppendLine("	@v_t73303_cargo_qsa, ");
            Sql.AppendLine("	@v_t73303_cpf_rep_legal, ");
            Sql.AppendLine("	@v_t73303_nom_rep_legal, ");
            Sql.AppendLine("	@v_t73303_qualificacao_rep_legal, ");
            Sql.AppendLine("	@v_t73303_ident_rep_legal, ");
            Sql.AppendLine("	@v_t73303_or_emis_ident_rep_legal, ");
            Sql.AppendLine("	@v_t73303_uf_or_emissor_rep_legal, ");
            Sql.AppendLine("	@v_t73303_dat_emis_ident_rep_lega, ");
            Sql.AppendLine("	@v_t73303_origem_endereco_rep_leg, ");
            Sql.AppendLine("	@v_t73303_tip_lograd_rep_legal, ");
            Sql.AppendLine("	@v_t73303_lograd_rep_legal, ");
            Sql.AppendLine("	@v_t73303_num_lograd_rep_legal, ");
            Sql.AppendLine("	@v_t73303_complemento_lograd_rep_, ");
            Sql.AppendLine("	@v_t73303_bairro_rep_legal, ");
            Sql.AppendLine("	@v_t73303_distrito_rep_legal, ");
            Sql.AppendLine("	@v_t73303_cod_munic_rep_legal, ");
            Sql.AppendLine("	@v_t73303_cep_rep_legal, ");
            Sql.AppendLine("	@v_t73303_uf_rep_legal, ");
            Sql.AppendLine("	@v_t73303_ddd_telefone_rep_legal, ");
            Sql.AppendLine("	@v_t73303_telefone_rep_legal, ");
            Sql.AppendLine("	@v_t73303_ddd_fax_rep_legal, ");
            Sql.AppendLine("	@v_t73303_fax_rep_legal, ");
            Sql.AppendLine("	@v_t73303_correio_eletronico_rep_, ");
            Sql.AppendLine("	@v_t73303_capital_social_empresa");
            Sql.AppendLine("  )");


           

            #region Update comentado
            // Codigo Update ******************* 
            SqlU.AppendLine(" Update     T73303_DBE_QSA Set ");
            SqlU.AppendLine("		t73303_nom_qsa = @v_t73303_nom_qsa, ");
            SqlU.AppendLine("		t73303_tip_lograd_qsa = @v_t73303_tip_lograd_qsa, ");
            SqlU.AppendLine("		t73303_lograd_qsa = @v_t73303_lograd_qsa, ");
            SqlU.AppendLine("		t73303_num_lograd_qsa = @v_t73303_num_lograd_qsa, ");
            SqlU.AppendLine("		t73303_complemento_lograd_qsa = @v_t73303_complemento_lograd_qsa, ");
            SqlU.AppendLine("		t73303_bairro_qsa = @v_t73303_bairro_qsa, ");
            SqlU.AppendLine("		t73303_distrito_qsa = @v_t73303_distrito_qsa, ");
            SqlU.AppendLine("		t73303_cod_munic_qsa = @v_t73303_cod_munic_qsa, ");
            SqlU.AppendLine("		t73303_cep_qsa = @v_t73303_cep_qsa, ");
            SqlU.AppendLine("		t73303_uf_qsa = @v_t73303_uf_qsa, ");
            SqlU.AppendLine("		t73303_ddd_telefone_qsa = @v_t73303_ddd_telefone_qsa, ");
            SqlU.AppendLine("		t73303_telefone_qsa = @v_t73303_telefone_qsa, ");
            SqlU.AppendLine("		t73303_ddd_fax_qsa = @v_t73303_ddd_fax_qsa, ");
            SqlU.AppendLine("		t73303_fax_qsa = @v_t73303_fax_qsa, ");
            SqlU.AppendLine("		t73303_correio_eletronico_qsa = @v_t73303_correio_eletronico_qsa, ");
            SqlU.AppendLine("		t73303_ind_cpf_cnpj_qsa = @v_t73303_ind_cpf_cnpj_qsa, ");
            SqlU.AppendLine("		t73303_cod_evento = @v_t73303_cod_evento, ");
            SqlU.AppendLine("		t73303_dat_evento = @v_t73303_dat_evento, ");
            SqlU.AppendLine("		t73303_cod_pais = @v_t73303_cod_pais, ");
            SqlU.AppendLine("		t73303_perc_partic_qsa = @v_t73303_perc_partic_qsa, ");
            SqlU.AppendLine("		t73303_capital_social_qsa = @v_t73303_capital_social_qsa, ");
            SqlU.AppendLine("		t73303_orig_inf_lograd = @v_t73303_orig_inf_lograd, ");
            SqlU.AppendLine("		t73303_nire_qsa = @v_t73303_nire_qsa, ");
            SqlU.AppendLine("		t73303_matricula_rcpj = @v_t73303_matricula_rcpj, ");
            SqlU.AppendLine("		t73303_ident_passap_qsa = @v_t73303_ident_passap_qsa, ");
            SqlU.AppendLine("		t73303_orgao_emissor_ident = @v_t73303_orgao_emissor_ident, ");
            SqlU.AppendLine("		t73303_uf_orgao_emissor_ident = @v_t73303_uf_orgao_emissor_ident, ");
            SqlU.AppendLine("		t73303_dat_emissao_ident = @v_t73303_dat_emissao_ident, ");
            SqlU.AppendLine("		t73303_nacionalidade_qsa = @v_t73303_nacionalidade_qsa, ");
            SqlU.AppendLine("		t73303_dt_nascimento_socio_pf = @v_t73303_dt_nascimento_socio_pf, ");
            SqlU.AppendLine("		t73303_dat_inicio_mandato = @v_t73303_dat_inicio_mandato, ");
            SqlU.AppendLine("		t73303_dat_termino_mandato = @v_t73303_dat_termino_mandato, ");
            SqlU.AppendLine("		t73303_uso_firma_administrador = @v_t73303_uso_firma_administrador, ");
            SqlU.AppendLine("		t73303_cargo_qsa = @v_t73303_cargo_qsa, ");
            SqlU.AppendLine("		t73303_cpf_rep_legal = @v_t73303_cpf_rep_legal, ");
            SqlU.AppendLine("		t73303_nom_rep_legal = @v_t73303_nom_rep_legal, ");
            SqlU.AppendLine("		t73303_qualificacao_rep_legal = @v_t73303_qualificacao_rep_legal, ");
            SqlU.AppendLine("		t73303_ident_rep_legal = @v_t73303_ident_rep_legal, ");
            SqlU.AppendLine("		t73303_or_emis_ident_rep_legal = @v_t73303_or_emis_ident_rep_legal, ");
            SqlU.AppendLine("		t73303_uf_or_emissor_rep_legal = @v_t73303_uf_or_emissor_rep_legal, ");
            SqlU.AppendLine("		t73303_dat_emis_ident_rep_lega = @v_t73303_dat_emis_ident_rep_lega, ");
            SqlU.AppendLine("		t73303_origem_endereco_rep_leg = @v_t73303_origem_endereco_rep_leg, ");
            SqlU.AppendLine("		t73303_tip_lograd_rep_legal = @v_t73303_tip_lograd_rep_legal, ");
            SqlU.AppendLine("		t73303_lograd_rep_legal = @v_t73303_lograd_rep_legal, ");
            SqlU.AppendLine("		t73303_num_lograd_rep_legal = @v_t73303_num_lograd_rep_legal, ");
            SqlU.AppendLine("		t73303_complemento_lograd_rep_ = @v_t73303_complemento_lograd_rep_, ");
            SqlU.AppendLine("		t73303_bairro_rep_legal = @v_t73303_bairro_rep_legal, ");
            SqlU.AppendLine("		t73303_distrito_rep_legal = @v_t73303_distrito_rep_legal, ");
            SqlU.AppendLine("		t73303_cod_munic_rep_legal = @v_t73303_cod_munic_rep_legal, ");
            SqlU.AppendLine("		t73303_cep_rep_legal = @v_t73303_cep_rep_legal, ");
            SqlU.AppendLine("		t73303_uf_rep_legal = @v_t73303_uf_rep_legal, ");
            SqlU.AppendLine("		t73303_ddd_telefone_rep_legal = @v_t73303_ddd_telefone_rep_legal, ");
            SqlU.AppendLine("		t73303_telefone_rep_legal = @v_t73303_telefone_rep_legal, ");
            SqlU.AppendLine("		t73303_ddd_fax_rep_legal = @v_t73303_ddd_fax_rep_legal, ");
            SqlU.AppendLine("		t73303_fax_rep_legal = @v_t73303_fax_rep_legal, ");
            SqlU.AppendLine("		t73303_correio_eletronico_rep_ = @v_t73303_correio_eletronico_rep_, ");
            SqlU.AppendLine("		t73303_capital_social_empresa = @v_t73303_capital_social_empresa");
            SqlU.AppendLine(" Where	t73300_id_control = @v_t73300_id_control ");
            SqlU.AppendLine(" And	t73303_cpf_cnpj_qsa = @v_t73303_cpf_cnpj_qsa ");
            SqlU.AppendLine(" And	t73303_qualificacao_qsa = @v_t73303_qualificacao_qsa ");
            #endregion

            MySqlCommand cmdToExecute = new MySqlCommand();
            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.CommandType = CommandType.Text;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                if (_t73303_qualificacao_qsa.ToString().Trim() != "")
                {
                    _t73303_qualificacao_qsa = int.Parse(_t73303_qualificacao_qsa).ToString();
                }

                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73300_id_control", MySqlDbType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73300_id_control));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_cpf_cnpj_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_cpf_cnpj_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_nom_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_nom_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_qualificacao_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_qualificacao_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_tip_lograd_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_tip_lograd_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_lograd_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_lograd_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_num_lograd_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_num_lograd_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_complemento_lograd_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_complemento_lograd_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_bairro_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_bairro_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_distrito_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_distrito_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_cod_munic_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_cod_munic_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_cep_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_cep_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_uf_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_uf_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_ddd_telefone_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_ddd_telefone_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_telefone_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_telefone_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_ddd_fax_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_ddd_fax_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_fax_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_fax_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_correio_eletronico_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_correio_eletronico_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_ind_cpf_cnpj_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_ind_cpf_cnpj_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_cod_evento", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_cod_evento));

                if (_t73303_dat_evento.Year == 1)
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_dat_evento", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, null));
                }
                else
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_dat_evento", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_dat_evento));
                }


                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_cod_pais", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_cod_pais));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_perc_partic_qsa", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 22, 2, "", DataRowVersion.Proposed, _t73303_perc_partic_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_capital_social_qsa", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 22, 2, "", DataRowVersion.Proposed, _t73303_capital_social_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_orig_inf_lograd", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_orig_inf_lograd));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_nire_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_nire_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_matricula_rcpj", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_matricula_rcpj));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_ident_passap_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_ident_passap_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_orgao_emissor_ident", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_orgao_emissor_ident));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_uf_orgao_emissor_ident", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_uf_orgao_emissor_ident));

                if (_t73303_dat_emissao_ident.Year == 1)
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_dat_emissao_ident", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, null));
                }
                else
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_dat_emissao_ident", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_dat_emissao_ident));
                }

                
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_nacionalidade_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_nacionalidade_qsa));

                if (_t73303_dt_nascimento_socio_pf.Year == 1)
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_dt_nascimento_socio_pf", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, null));
                }
                else
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_dt_nascimento_socio_pf", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_dt_nascimento_socio_pf));
                }

                if (_t73303_dat_inicio_mandato.Year == 1)
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_dat_inicio_mandato", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, null));
                }
                else
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_dat_inicio_mandato", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_dat_inicio_mandato));
                }

                if (_t73303_dat_termino_mandato.Year == 1)
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_dat_termino_mandato", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, null));
                }
                else
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_dat_termino_mandato", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_dat_termino_mandato));
                }

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_uso_firma_administrador", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_uso_firma_administrador));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_cargo_qsa", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_cargo_qsa));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_cpf_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_cpf_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_nom_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_nom_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_qualificacao_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_qualificacao_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_ident_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_ident_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_or_emis_ident_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_or_emis_ident_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_uf_or_emissor_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_uf_or_emissor_rep_legal));

                if (_t73303_dat_emis_ident_rep_lega.Year == 1)
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_dat_emis_ident_rep_lega", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, null));
                }
                else
                {
                    cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_dat_emis_ident_rep_lega", MySqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_dat_emis_ident_rep_lega));
                }

                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_origem_endereco_rep_leg", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_origem_endereco_rep_leg));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_tip_lograd_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_tip_lograd_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_lograd_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_lograd_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_num_lograd_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_num_lograd_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_complemento_lograd_rep_", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_complemento_lograd_rep_));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_bairro_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_bairro_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_distrito_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_distrito_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_cod_munic_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_cod_munic_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_cep_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_cep_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_uf_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_uf_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_ddd_telefone_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_ddd_telefone_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_telefone_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_telefone_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_ddd_fax_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_ddd_fax_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_fax_rep_legal", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_fax_rep_legal));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_correio_eletronico_rep_", MySqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t73303_correio_eletronico_rep_));
                cmdToExecute.Parameters.Add(new MySqlParameter("v_t73303_capital_social_empresa", MySqlDbType.Decimal, 0, ParameterDirection.Input, true, 22, 2, "", DataRowVersion.Proposed, _t73303_capital_social_empresa));


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
