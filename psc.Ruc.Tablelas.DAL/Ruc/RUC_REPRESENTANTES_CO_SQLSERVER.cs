using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace psc.Ruc.Tablelas.DAL.Ruc
{
    public class RUC_REPRESENTANTES_CO_SQLSERVER
    {
        // Variables ******************* 
        #region  Property Declarations
        protected string _rsr_pra_protocolo = "";
        protected string _rsr_cgc_cpf_princ = "";
        protected string _rsr_cgc_cpf_secd = "";
        protected decimal _rsr_tipo = int.MinValue;
        protected string _rsr_nomb = "";
        protected decimal _rsr_tge_tcod_qual = int.MinValue;
        protected decimal _rsr_tge_vcod_qual = int.MinValue;
        protected string _rsr_crc_ctdr = "";
        protected string _rsr_uf_crc_ctdr = "";
        protected decimal _rsr_tge_ttip_pers = int.MinValue;
        protected decimal _rsr_tge_vtip_pers = int.MinValue;
        protected string _rsr_ttl_tip_logradoro = "";
        protected string _rsr_direccion = "";
        protected string _rsr_nume = "";
        protected string _rsr_ident_comp = "";
        protected string _rsr_urbanizacion = "";
        protected string _rsr_distrito = "";
        protected string _rsr_tmu_cod_mun = "0";
        protected string _rsr_tes_cod_estado = "";
        protected string _rsr_zona_postal = "";
        protected decimal _rsr_tge_tpais = 22;
        protected string _rsr_tge_vpais = "105";
        protected string _rsr_ddd_telefone = "";
        protected string _rsr_telefone = "";
        protected string _rsr_nro_identidade = "";
        protected string _rsr_orgao_exped = "";
        protected string _rsr_uf_emissor = "";
        #endregion

        // Property ******************* 
        #region Class Member Declarations
        public string rsr_pra_protocolo
        {
            get { return _rsr_pra_protocolo; }
            set { _rsr_pra_protocolo = value; }
        }
        public string rsr_cgc_cpf_princ
        {
            get { return _rsr_cgc_cpf_princ; }
            set { _rsr_cgc_cpf_princ = value; }
        }
        public string rsr_cgc_cpf_secd
        {
            get { return _rsr_cgc_cpf_secd; }
            set { _rsr_cgc_cpf_secd = value; }
        }
        public decimal rsr_tipo
        {
            get { return _rsr_tipo; }
            set { _rsr_tipo = value; }
        }
        public string rsr_nomb
        {
            get { return _rsr_nomb; }
            set { _rsr_nomb = value; }
        }
        public decimal rsr_tge_tcod_qual
        {
            get { return _rsr_tge_tcod_qual; }
            set { _rsr_tge_tcod_qual = value; }
        }
        public decimal rsr_tge_vcod_qual
        {
            get { return _rsr_tge_vcod_qual; }
            set { _rsr_tge_vcod_qual = value; }
        }
        public string rsr_crc_ctdr
        {
            get { return _rsr_crc_ctdr; }
            set { _rsr_crc_ctdr = value; }
        }
        public string rsr_uf_crc_ctdr
        {
            get { return _rsr_uf_crc_ctdr; }
            set { _rsr_uf_crc_ctdr = value; }
        }
        public decimal rsr_tge_ttip_pers
        {
            get { return _rsr_tge_ttip_pers; }
            set { _rsr_tge_ttip_pers = value; }
        }
        public decimal rsr_tge_vtip_pers
        {
            get { return _rsr_tge_vtip_pers; }
            set { _rsr_tge_vtip_pers = value; }
        }
        public string rsr_ttl_tip_logradoro
        {
            get { return _rsr_ttl_tip_logradoro; }
            set { _rsr_ttl_tip_logradoro = value; }
        }
        public string rsr_direccion
        {
            get { return _rsr_direccion; }
            set { _rsr_direccion = value; }
        }
        public string rsr_nume
        {
            get { return _rsr_nume; }
            set { _rsr_nume = value; }
        }
        public string rsr_ident_comp
        {
            get { return _rsr_ident_comp; }
            set { _rsr_ident_comp = value; }
        }
        public string rsr_urbanizacion
        {
            get { return _rsr_urbanizacion; }
            set { _rsr_urbanizacion = value; }
        }
        public string rsr_distrito
        {
            get { return _rsr_distrito; }
            set { _rsr_distrito = value; }
        }
        public string rsr_tmu_cod_mun
        {
            get { return _rsr_tmu_cod_mun; }
            set { _rsr_tmu_cod_mun = value; }
        }
        public string rsr_tes_cod_estado
        {
            get { return _rsr_tes_cod_estado; }
            set { _rsr_tes_cod_estado = value; }
        }
        public string rsr_zona_postal
        {
            get { return _rsr_zona_postal; }
            set { _rsr_zona_postal = value; }
        }
        public decimal rsr_tge_tpais
        {
            get { return _rsr_tge_tpais; }
            set { _rsr_tge_tpais = value; }
        }
        public string rsr_tge_vpais
        {
            get { return _rsr_tge_vpais; }
            set { _rsr_tge_vpais = value; }
        }
        public string rsr_ddd_telefone
        {
            get { return _rsr_ddd_telefone; }
            set { _rsr_ddd_telefone = value; }
        }
        public string rsr_telefone
        {
            get { return _rsr_telefone; }
            set { _rsr_telefone = value; }
        }
        public string rsr_nro_identidade
        {
            get { return _rsr_nro_identidade; }
            set { _rsr_nro_identidade = value; }
        }
        public string rsr_orgao_exped
        {
            get { return _rsr_orgao_exped; }
            set { _rsr_orgao_exped = value; }
        }
        public string rsr_uf_emissor
        {
            get { return _rsr_uf_emissor; }
            set { _rsr_uf_emissor = value; }
        }
        #endregion

        public void Update(SqlTransaction bd)
        {

            SqlCommand cmdToExecuteSql = new SqlCommand();

            cmdToExecuteSql.Transaction = bd;
            cmdToExecuteSql.Connection = bd.Connection;

            StringBuilder sSocio = new StringBuilder();
            sSocio.AppendLine(" select	Count(*) ");
            sSocio.AppendLine(" from	    ruc_representantes ");
            sSocio.AppendLine(" where	RSR_PRA_PROTOCOLO = @v_rsr_pra_protocolo");
            sSocio.AppendLine(" and		RSR_CGC_CPF_PRINC = @v_rsr_cgc_cpf_princ");
            sSocio.AppendLine(" and		RSR_CGC_CPF_SECD = @v_rsr_cgc_cpf_secd ");
            sSocio.AppendLine(" and		RSR_TIPO = @v_rsr_tipo ");

            cmdToExecuteSql.CommandText = sSocio.ToString();
            cmdToExecuteSql.CommandType = CommandType.Text;

            cmdToExecuteSql.Parameters.Add(new SqlParameter("v_rsr_pra_protocolo", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_pra_protocolo));
            cmdToExecuteSql.Parameters.Add(new SqlParameter("v_rsr_cgc_cpf_princ", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_cgc_cpf_princ));
            cmdToExecuteSql.Parameters.Add(new SqlParameter("v_rsr_cgc_cpf_secd", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_cgc_cpf_secd));
            cmdToExecuteSql.Parameters.Add(new SqlParameter("v_rsr_tipo", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_tipo));

            object pCount = cmdToExecuteSql.ExecuteScalar();

            if (int.Parse(pCount.ToString()) > 0)
                return;



            SqlCommand cmdToExecute = new SqlCommand();
            StringBuilder Sql = new StringBuilder();
            Sql.AppendLine(" Insert into ruc_representantes");
            Sql.AppendLine("  (");
            Sql.AppendLine("	rsr_pra_protocolo, ");
            Sql.AppendLine("	rsr_cgc_cpf_princ, ");
            Sql.AppendLine("	rsr_cgc_cpf_secd, ");
            Sql.AppendLine("	rsr_tipo, ");
            Sql.AppendLine("	rsr_nomb, ");
            Sql.AppendLine("	rsr_tge_tcod_qual, ");
            Sql.AppendLine("	rsr_tge_vcod_qual, ");
            Sql.AppendLine("	rsr_crc_ctdr, ");
            Sql.AppendLine("	rsr_uf_crc_ctdr, ");
            Sql.AppendLine("	rsr_tge_ttip_pers, ");
            Sql.AppendLine("	rsr_tge_vtip_pers, ");
            Sql.AppendLine("	rsr_ttl_tip_logradoro, ");
            Sql.AppendLine("	rsr_direccion, ");
            Sql.AppendLine("	rsr_nume, ");
            Sql.AppendLine("	rsr_ident_comp, ");
            Sql.AppendLine("	rsr_urbanizacion, ");
            Sql.AppendLine("	rsr_distrito, ");
            Sql.AppendLine("	rsr_tmu_cod_mun, ");
            Sql.AppendLine("	rsr_tes_cod_estado, ");
            Sql.AppendLine("	rsr_zona_postal, ");
            Sql.AppendLine("	rsr_tge_tpais, ");
            Sql.AppendLine("	rsr_tge_vpais, ");
            Sql.AppendLine("	rsr_ddd_telefone, ");
            Sql.AppendLine("	rsr_telefone, ");
            Sql.AppendLine("	rsr_nro_identidade, ");
            Sql.AppendLine("	rsr_orgao_exped, ");
            Sql.AppendLine("	rsr_uf_emissor,");
            Sql.AppendLine("	RSR_ORIGEM_ENDERECO ");
            Sql.AppendLine("  )");
            Sql.AppendLine(" Values ");
            Sql.AppendLine("  (");
            Sql.AppendLine("	@v_rsr_pra_protocolo, ");
            Sql.AppendLine("	@v_rsr_cgc_cpf_princ, ");
            Sql.AppendLine("	@v_rsr_cgc_cpf_secd, ");
            Sql.AppendLine("	@v_rsr_tipo, ");
            Sql.AppendLine("	@v_rsr_nomb, ");
            Sql.AppendLine("	@v_rsr_tge_tcod_qual, ");
            Sql.AppendLine("	@v_rsr_tge_vcod_qual, ");
            Sql.AppendLine("	@v_rsr_crc_ctdr, ");
            Sql.AppendLine("	@v_rsr_uf_crc_ctdr, ");
            Sql.AppendLine("	@v_rsr_tge_ttip_pers, ");
            Sql.AppendLine("	@v_rsr_tge_vtip_pers, ");
            Sql.AppendLine("	@v_rsr_ttl_tip_logradoro, ");
            Sql.AppendLine("	@v_rsr_direccion, ");
            Sql.AppendLine("	@v_rsr_nume, ");
            Sql.AppendLine("	@v_rsr_ident_comp, ");
            Sql.AppendLine("	@v_rsr_urbanizacion, ");
            Sql.AppendLine("	@v_rsr_distrito, ");
            Sql.AppendLine("	@v_rsr_tmu_cod_mun, ");
            Sql.AppendLine("	@v_rsr_tes_cod_estado, ");
            Sql.AppendLine("	@v_rsr_zona_postal, ");
            Sql.AppendLine("	@v_rsr_tge_tpais, ");
            Sql.AppendLine("	@v_rsr_tge_vpais, ");
            Sql.AppendLine("	@v_rsr_ddd_telefone, ");
            Sql.AppendLine("	@v_rsr_telefone, ");
            Sql.AppendLine("	@v_rsr_nro_identidade, ");
            Sql.AppendLine("	@v_rsr_orgao_exped, ");
            Sql.AppendLine("	@v_rsr_uf_emissor, ");
            Sql.AppendLine("	2 ");
            Sql.AppendLine("  )");


            // Codigo dbParameter ******************* 
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_pra_protocolo", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_pra_protocolo));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_cgc_cpf_princ", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_cgc_cpf_princ));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_cgc_cpf_secd", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_cgc_cpf_secd));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_tipo", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_tipo));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_nomb", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_nomb));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_tge_tcod_qual", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_tge_tcod_qual));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_tge_vcod_qual", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_tge_vcod_qual));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_crc_ctdr", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_crc_ctdr));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_uf_crc_ctdr", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_uf_crc_ctdr));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_tge_ttip_pers", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_tge_ttip_pers));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_tge_vtip_pers", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_tge_vtip_pers));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_ttl_tip_logradoro", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_ttl_tip_logradoro));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_direccion", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_direccion));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_nume", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_nume));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_ident_comp", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_ident_comp));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_urbanizacion", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_urbanizacion));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_distrito", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_distrito));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_tmu_cod_mun", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_tmu_cod_mun));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_tes_cod_estado", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_tes_cod_estado));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_zona_postal", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_zona_postal));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_tge_tpais", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_tge_tpais));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_tge_vpais", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_tge_vpais));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_ddd_telefone", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_ddd_telefone));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_telefone", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_telefone));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_nro_identidade", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_nro_identidade));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_orgao_exped", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_orgao_exped));
            cmdToExecute.Parameters.Add(new SqlParameter("v_rsr_uf_emissor", SqlDbType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _rsr_uf_emissor));

            cmdToExecute.Transaction = bd;

            cmdToExecute.Connection = bd.Connection;

            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.CommandText = Sql.ToString();

            cmdToExecute.ExecuteNonQuery();
        }
    }
}
