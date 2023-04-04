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
    public class PSC_PROTOCOLO : DBInteractionBaseORACLE
    {
        // Variables ******************* 
        #region  Property Declarations

        private decimal _pro_origem_dado = 1;
        public decimal pro_origem_dado
        {
            get { return _pro_origem_dado; }
            set { _pro_origem_dado = value; }
        }

        
        
        private string _PRO_NR_DBE = "";
        public string PRO_NR_DBE
        {
            get { return _PRO_NR_DBE; }
            set { _PRO_NR_DBE = value; }
        }
        
        private string _pro_protocolo;

        public string Pro_protocolo
        {
            get { return _pro_protocolo; }
            set { _pro_protocolo = value; }
        }

        private int _pro_status;

        public int Pro_status
        {
            get { return _pro_status; }
            set { _pro_status = value; }
        }

        private DateTime _pro_fec_inc;

        public DateTime Pro_fec_inc
        {
            get { return _pro_fec_inc; }
            set { _pro_fec_inc = value; }
        }

        private string _pro_tmu_tuf_uf;

        public string Pro_tmu_tuf_uf
        {
            get { return _pro_tmu_tuf_uf; }
            set { _pro_tmu_tuf_uf = value; }
        }

        private int _pro_tmu_cod_mun;

        public int Pro_tmu_cod_mun
        {
            get { return _pro_tmu_cod_mun; }
            set { _pro_tmu_cod_mun = value; }
        }

        private int _pro_tip_operacao;

        public int Pro_tip_operacao
        {
            get { return _pro_tip_operacao; }
            set { _pro_tip_operacao = value; }
        }

        private int _pro_env_sef;

        public int Pro_env_sef
        {
            get { return _pro_env_sef; }
            set { _pro_env_sef = value; }
        }
        private int _pro_flag_vigilancia;

        public int Pro_flag_vigilancia
        {
            get { return _pro_flag_vigilancia; }
            set { _pro_flag_vigilancia = value; }
        }
        private DateTime _pro_fec_atualizacao;

        public DateTime Pro_fec_atualizacao
        {
            get { return _pro_fec_atualizacao; }
            set { _pro_fec_atualizacao = value; }
        }
        private int _pro_tge_tgacao;

        public int Pro_tge_tgacao
        {
            get { return _pro_tge_tgacao; }
            set { _pro_tge_tgacao = value; }
        }
        private int _pro_tge_vgacao;

        public int Pro_tge_vgacao
        {
            get { return _pro_tge_vgacao; }
            set { _pro_tge_vgacao = value; }
        }
        private string _pro_cnpj_org_reg;

        public string Pro_cnpj_org_reg
        {
            get { return _pro_cnpj_org_reg; }
            set { _pro_cnpj_org_reg = value; }
        }
        private string _PRO_NR_REQUERIMENTO;

        public string PRO_NR_REQUERIMENTO
        {
            get { return _PRO_NR_REQUERIMENTO; }
            set { _PRO_NR_REQUERIMENTO = value; }
        }
        private string _PRO_VPV_COD_PROTOCOLO;

        public string PRO_VPV_COD_PROTOCOLO
        {
            get { return _PRO_VPV_COD_PROTOCOLO; }
            set { _PRO_VPV_COD_PROTOCOLO = value; }
        }
        #endregion


       
        // Property ******************* 


        public void Update(OracleTransaction bd)
        {

            StringBuilder Sql = new StringBuilder();


            using (OracleCommand cmdToExecute = new OracleCommand())
            {
                cmdToExecute.CommandType = CommandType.Text;
                Sql.AppendLine(@"insert into psc_protocolo
                              (pro_protocolo, 
                              pro_status, 
                              pro_fec_inc, 
                              pro_tmu_tuf_uf, 
                              pro_tmu_cod_mun, 
                              pro_tip_operacao, 
                              pro_env_sef, 
                              pro_flag_vigilancia, 
                              pro_fec_atualizacao, 
                              pro_tge_tgacao, 
                              pro_tge_vgacao,
                              pro_cnpj_org_reg,
                              PRO_NR_REQUERIMENTO,  
                              PRO_VPV_COD_PROTOCOLO,
                              PRO_NR_DBE,
                              pro_origem_dado)
                            values ");
                Sql.Append("  (");
                Sql.Append("	:v_pro_protocolo, ");
                Sql.Append("	:v_pro_status, ");
                Sql.Append("	:v_pro_fec_inc, ");
                Sql.Append("	:v_pro_tmu_tuf_uf, ");

                Sql.Append("	:v_pro_tmu_cod_mun, ");
                Sql.Append("	:v_pro_tip_operacao, ");
                Sql.Append("	:v_pro_env_sef, ");
                Sql.Append("	:v_pro_flag_vigilancia, ");
                Sql.Append("	:v_pro_fec_atualizacao, ");
                Sql.Append("	:v_pro_tge_tgacao, ");
                Sql.Append("	:v_pro_tge_vgacao, ");
                Sql.Append("	:v_pro_cnpj_org_reg, ");
                Sql.Append("	:v_PRO_NR_REQUERIMENTO, ");
                Sql.Append("	:v_PRO_VPV_COD_PROTOCOLO, ");
                Sql.Append("	:v_PRO_NR_DBE, ");
                Sql.Append("	:v_pro_origem_dado ");
                Sql.Append("  )");

                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.Connection = _mainConnectionORACLE;


                // Codigo dbParameter ******************* 
                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_protocolo));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_status", OracleType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_status));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_fec_inc", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_fec_inc));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_tmu_tuf_uf", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_tmu_tuf_uf));

                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_tmu_cod_mun", OracleType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_tmu_cod_mun));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_tip_operacao", OracleType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_tip_operacao));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_env_sef", OracleType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_env_sef));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_flag_vigilancia", OracleType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_flag_vigilancia));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_fec_atualizacao", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_fec_atualizacao));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_tge_tgacao", OracleType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_tge_tgacao));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_tge_vgacao", OracleType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_tge_vgacao));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_cnpj_org_reg", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_cnpj_org_reg));
                cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_NR_REQUERIMENTO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _PRO_NR_REQUERIMENTO));
                cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_VPV_COD_PROTOCOLO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _PRO_VPV_COD_PROTOCOLO));
                cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_NR_DBE", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _PRO_NR_DBE));
                cmdToExecute.Parameters.Add(new OracleParameter("v_pro_origem_dado", OracleType.Int32, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _pro_origem_dado));

                cmdToExecute.Transaction = bd;

                cmdToExecute.Connection = bd.Connection;

                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.CommandText = Sql.ToString();

                cmdToExecute.ExecuteNonQuery();
            }
        }
    }
}
