using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using psc.Receita.ConnectionBase;
using System.Text;
using System.Data.OracleClient;
using WsRFBReginV2.Models;

/// <summary>
/// Summary description for T0101_RFB_PROCESSO_DEFERIDOS
/// </summary>
public class T0101_RFB_PROCESSO_DEFERIDOS : DBInteractionBaseORACLE
{
    public T0101_RFB_PROCESSO_DEFERIDOS()
    {
    }

    #region  Property Declarations
    protected decimal _t0101_id_rfb = int.MinValue;
    protected string _t0101_cnpj = "";
    protected decimal _t0101_tp_estab = int.MinValue;
    protected string _t0101_uf = "";
    protected decimal _t0101_cod_mun = int.MinValue;
    protected string _t0101_or = "";
    protected DateTime _t0101_dt_deferimento = DateTime.MinValue;
    protected DateTime _t0101_dt_criacao = DateTime.MinValue;
    protected decimal _t0101_status = int.MinValue;
    protected string _t0101_dbe = "";
    protected string _t0101_cnpj_matriz = "";
    protected string _t0101_nat_juridica = "";
    protected string _t0101_cod_serv_rfb = "";
    protected string _t0101_indicador_mei = "";
    protected string _t0101_cnpj_mei = "";
    protected DateTime _t0101_dt_evento_mei = DateTime.MinValue;
    protected string _t0101_xml_rfb = "";
    protected DateTime _t0101_dt_carrega_mac = DateTime.MinValue;
    protected string _t0101_protocolo_regin = "";
    protected string _t0101_xml_rfb_09 = "";
    protected string _t0102_xml_sxx = "";
    protected string _t0102_xml_regin = "";
    protected string _t0101_nome_fantasia = "";
    protected DateTime _t0101_data_recebido_rfb = DateTime.MinValue;
    protected string _t0101_codigo_retorno = "";
    protected string _t0101_viabilid_associada = "";
    protected string _t0101_numero_ato_oficio = "";
    protected string _t0101_evento_simples = "N";
    protected decimal _t0101_marcado_s24 = int.MinValue;
    protected string _t0101_altera_matriz_fora = "N";
    protected string _t0101_orgao_deferimento = "";
    protected string _t0101_codigoconvenioato = "";
    protected string _t0101_numeroProtocolo = "";

    protected string _t0101_uf_ant = "";
    protected decimal _t0101_cod_mun_ant = int.MinValue;



    #endregion

    #region Class Member Declarations
    public string t0101_numeroProtocolo
    {
        get { return _t0101_numeroProtocolo; }
        set { _t0101_numeroProtocolo = value; }
    }
    public string t0101_codigoconvenioato
    {
        get { return _t0101_codigoconvenioato; }
        set { _t0101_codigoconvenioato = value; }
    }

    public string t0101_uf_ant
    {
        get { return _t0101_uf_ant; }
        set { _t0101_uf_ant = value; }
    }
    public decimal t0101_cod_mun_ant
    {
        get { return _t0101_cod_mun_ant; }
        set { _t0101_cod_mun_ant = value; }
    }

    public string t0101_altera_matriz_fora
    {
        get { return _t0101_altera_matriz_fora; }
        set { _t0101_altera_matriz_fora = value; }
    }
    public string t0101_orgao_deferimento
    {
        get { return _t0101_orgao_deferimento; }
        set { _t0101_orgao_deferimento = value; }
    }
    public decimal t0101_marcado_s24
    {
        get { return _t0101_marcado_s24; }
        set { _t0101_marcado_s24 = value; }
    }
    public string t0101_evento_simples
    {
        get { return _t0101_evento_simples; }
        set { _t0101_evento_simples = value; }
    }

    public string t0101_numero_ato_oficio
    {
        get { return _t0101_numero_ato_oficio; }
        set { _t0101_numero_ato_oficio = value; }
    }
    public string t0102_xml_regin
    {
        get { return _t0102_xml_regin; }
        set { _t0102_xml_regin = value; }
    }
    

    public string t0102_xml_sxx
    {
        get { return _t0102_xml_sxx; }
        set { _t0102_xml_sxx = value; }
    }
    public string t0101_viabilid_associada
    {
        get { return _t0101_viabilid_associada; }
        set { _t0101_viabilid_associada = value; }
    }

    public string t0101_codigo_retorno
    {
        get { return _t0101_codigo_retorno; }
        set { _t0101_codigo_retorno = value; }
    }
    public DateTime t0101_data_recebido_rfb
    {
        get { return _t0101_data_recebido_rfb; }
        set { _t0101_data_recebido_rfb = value; }
    }
    public decimal t0101_id_rfb
    {
        get { return _t0101_id_rfb; }
        set { _t0101_id_rfb = value; }
    }
    public string t0101_cnpj
    {
        get { return _t0101_cnpj; }
        set { _t0101_cnpj = value; }
    }
    public decimal t0101_tp_estab
    {
        get { return _t0101_tp_estab; }
        set { _t0101_tp_estab = value; }
    }
    public string t0101_uf
    {
        get { return _t0101_uf; }
        set { _t0101_uf = value; }
    }
    public decimal t0101_cod_mun
    {
        get { return _t0101_cod_mun; }
        set { _t0101_cod_mun = value; }
    }
    public string t0101_or
    {
        get { return _t0101_or; }
        set { _t0101_or = value; }
    }
    public DateTime t0101_dt_deferimento
    {
        get { return _t0101_dt_deferimento; }
        set { _t0101_dt_deferimento = value; }
    }
    public DateTime t0101_dt_criacao
    {
        get { return _t0101_dt_criacao; }
        set { _t0101_dt_criacao = value; }
    }
    public decimal t0101_status
    {
        get { return _t0101_status; }
        set { _t0101_status = value; }
    }
    public string t0101_dbe
    {
        get { return _t0101_dbe; }
        set { _t0101_dbe = value; }
    }
    public string t0101_cnpj_matriz
    {
        get { return _t0101_cnpj_matriz; }
        set { _t0101_cnpj_matriz = value; }
    }
    public string t0101_nat_juridica
    {
        get { return _t0101_nat_juridica; }
        set { _t0101_nat_juridica = value; }
    }
    public string t0101_cod_serv_rfb
    {
        get { return _t0101_cod_serv_rfb; }
        set { _t0101_cod_serv_rfb = value; }
    }
    public string t0101_indicador_mei
    {
        get { return _t0101_indicador_mei; }
        set { _t0101_indicador_mei = value; }
    }
    public string t0101_cnpj_mei
    {
        get { return _t0101_cnpj_mei; }
        set { _t0101_cnpj_mei = value; }
    }
    public DateTime t0101_dt_evento_mei
    {
        get { return _t0101_dt_evento_mei; }
        set { _t0101_dt_evento_mei = value; }
    }
    public string t0101_xml_rfb
    {
        get { return _t0101_xml_rfb; }
        set { _t0101_xml_rfb = value; }
    }
    public DateTime t0101_dt_carrega_mac
    {
        get { return _t0101_dt_carrega_mac; }
        set { _t0101_dt_carrega_mac = value; }
    }
    public string t0101_protocolo_regin
    {
        get { return _t0101_protocolo_regin; }
        set { _t0101_protocolo_regin = value; }
    }
    public string t0101_xml_rfb_09
    {
        get { return _t0101_xml_rfb_09; }
        set { _t0101_xml_rfb_09 = value; }
    }
    public string t0101_nome_fantasia
    {
        get { return _t0101_nome_fantasia; }
        set { _t0101_nome_fantasia = value; }
    }
    #endregion

    public void UpdateV2()
    {
        StringBuilder Sql = new StringBuilder();
        StringBuilder SqlU = new StringBuilder();
        OracleCommand cmdToExecute = new OracleCommand();
        cmdToExecute.CommandType = CommandType.Text;
        Sql.Append(" Insert into t0101_rfb_processo_deferidos");
        Sql.Append("  (");
        Sql.Append("	t0101_id_rfb, ");
        Sql.Append("	t0101_cnpj, ");
        Sql.Append("	t0101_tp_estab, ");
        Sql.Append("	t0101_uf, ");
        Sql.Append("	t0101_cod_mun, ");
        Sql.Append("	t0101_uf_ant, ");
        Sql.Append("	t0101_cod_mun_ant, ");
        Sql.Append("	t0101_or, ");
        Sql.Append("	t0101_dt_deferimento, ");
        Sql.Append("	t0101_dt_criacao, ");
        Sql.Append("	t0101_dbe, ");
        Sql.Append("	t0101_cnpj_matriz, ");
        Sql.Append("	t0101_nat_juridica, ");
        Sql.Append("	t0101_cod_serv_rfb, ");
        Sql.Append("	t0101_indicador_mei, ");
        Sql.Append("	t0101_cnpj_mei, ");
        Sql.Append("	t0101_dt_evento_mei, ");
        //Sql.Append("	t0101_xml_rfb, ");
        //Sql.Append("	t0101_protocolo_regin, ");
        Sql.Append("	t0101_data_recebido_rfb, ");
        //Sql.Append("	t0101_xml_rfb_09, ");
        Sql.Append("	t0101_marcado_s24, ");
        Sql.Append("	t0101_codigoconvenioato, ");
        Sql.Append("	t0101_nome_fantasia");
        Sql.Append("  )");
        Sql.Append(" Values ");
        Sql.Append("  (");
        Sql.Append("	:v_t0101_id_rfb, ");
        Sql.Append("	:v_t0101_cnpj, ");
        Sql.Append("	evalnumeric(:v_t0101_tp_estab), ");
        Sql.Append("	:v_t0101_uf, ");
        Sql.Append("	evalnumeric(:v_t0101_cod_mun), ");
        Sql.Append("	:v_t0101_uf_ant, ");
        Sql.Append("	evalnumeric(:v_t0101_cod_mun_ant), ");
        Sql.Append("	:v_t0101_or, ");
        Sql.Append("	evaldate(:v_t0101_dt_deferimento), ");
        Sql.Append("	sysdate, ");
        Sql.Append("	:v_t0101_dbe, ");
        Sql.Append("	:v_t0101_cnpj_matriz, ");
        Sql.Append("	:v_t0101_nat_juridica, ");
        Sql.Append("	:v_t0101_cod_serv_rfb, ");
        Sql.Append("	:v_t0101_indicador_mei, ");
        Sql.Append("	:v_t0101_cnpj_mei, ");
        Sql.Append("	evaldate(:v_t0101_dt_evento_mei), ");
        //Sql.Append("	:v_t0101_protocolo_regin, ");
        Sql.Append("	evaldate(:v_t0101_data_recebido_rfb), ");
        Sql.Append("	evalnumeric(:v_t0101_marcado_s24), ");
        Sql.Append("	:v_t0101_codigoconvenioato, "); 
        Sql.Append("	:v_t0101_nome_fantasia");
        Sql.Append("  )");

        // Codigo Update ******************* 
        SqlU.Append(" Update     T0101_RFB_PROCESSO_DEFERIDOS Set ");
        SqlU.Append("		t0101_cnpj = :v_t0101_cnpj, ");
        SqlU.Append("		t0101_tp_estab = evalnumeric(:v_t0101_tp_estab), ");
        SqlU.Append("		t0101_uf = :v_t0101_uf, ");
        SqlU.Append("		t0101_cod_mun = evalnumeric(:v_t0101_cod_mun), ");
        SqlU.Append("		t0101_uf_ant = :v_t0101_uf_ant, ");
        SqlU.Append("		t0101_cod_mun_ant = evalnumeric(:v_t0101_cod_mun_ant), ");
        SqlU.Append("		t0101_or = :v_t0101_or, ");
        SqlU.Append("		t0101_dt_deferimento = evaldate(:v_t0101_dt_deferimento), ");
        SqlU.Append("		t0101_dbe = :v_t0101_dbe, ");
        SqlU.Append("		t0101_cnpj_matriz = :v_t0101_cnpj_matriz, ");
        SqlU.Append("		t0101_nat_juridica = :v_t0101_nat_juridica, ");
        SqlU.Append("		t0101_cod_serv_rfb = :v_t0101_cod_serv_rfb, ");
        SqlU.Append("		t0101_indicador_mei = :v_t0101_indicador_mei, ");
        SqlU.Append("		t0101_cnpj_mei = :v_t0101_cnpj_mei, ");
        SqlU.Append("		t0101_dt_evento_mei = evaldate(:v_t0101_dt_evento_mei), ");
        //SqlU.Append("		t0101_protocolo_regin = :v_t0101_protocolo_regin, ");
        SqlU.Append("		t0101_data_recebido_rfb = evaldate(:v_t0101_data_recebido_rfb), ");
        SqlU.Append("		t0101_marcado_s24 = evalnumeric(:v_t0101_marcado_s24), ");
        SqlU.Append("		t0101_codigoconvenioato = :v_t0101_codigoconvenioato, ");
        SqlU.Append("		t0101_nome_fantasia = :v_t0101_nome_fantasia");
        SqlU.Append(" Where	t0101_id_rfb = :v_t0101_id_rfb ");


        try
        {
            // Codigo dbParameter ******************* 
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_id_rfb", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_id_rfb));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_cnpj", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_cnpj));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_tp_estab", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_tp_estab));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_uf", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_uf));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_cod_mun", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_cod_mun));

            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_uf_ant", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_uf_ant));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_cod_mun_ant", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_cod_mun_ant));


            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_or", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_or));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_dt_deferimento", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_dt_deferimento));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_dbe", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_dbe));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_cnpj_matriz", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_cnpj_matriz));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_nat_juridica", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_nat_juridica));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_cod_serv_rfb", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_cod_serv_rfb));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_indicador_mei", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_indicador_mei));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_cnpj_mei", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_cnpj_mei));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_dt_evento_mei", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_dt_evento_mei));
            //cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_xml_rfb", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_xml_rfb));
            //cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_protocolo_regin", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_protocolo_regin));
            //cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_xml_rfb_09", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_xml_rfb_09));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_data_recebido_rfb", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_data_recebido_rfb));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_marcado_s24", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_marcado_s24));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_codigoconvenioato", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_codigoconvenioato));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_nome_fantasia", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_nome_fantasia));

            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.Connection = _mainConnectionORACLE;

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

            int atualizado = cmdToExecute.ExecuteNonQuery();

            if (atualizado == 0)
            {
                cmdToExecute.CommandText = Sql.ToString();
                atualizado = cmdToExecute.ExecuteNonQuery();
            }

            //if (atualizado > 0)
            //{

            //    string sqlD = " update t0101_rfb_processo_deferidos set t0101_xml_rfb = :t0101_xml_rfb, t0101_xml_rfb_09 = :t0101_xml_rfb_09 where t0101_id_rfb = " + t0101_id_rfb;

            //    cmdToExecute.Parameters.Clear();

            //    cmdToExecute.Parameters.Add(new OracleParameter("t0101_xml_rfb", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _t0101_xml_rfb));
            //    cmdToExecute.Parameters.Add(new OracleParameter("t0101_xml_rfb_09", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _t0101_xml_rfb_09));

            //    cmdToExecute.CommandText = sqlD;
            //    cmdToExecute.CommandType = CommandType.Text;
            //    cmdToExecute.ExecuteNonQuery();

            //}


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
                _mainConnectionORACLE.Close();
            }
            cmdToExecute.Dispose();
        }
    }

    public void Update()
    {
        StringBuilder Sql = new StringBuilder();
        StringBuilder SqlU = new StringBuilder();
        OracleCommand cmdToExecute = new OracleCommand();
        cmdToExecute.CommandType = CommandType.Text;
        Sql.Append(" Insert into t0101_rfb_processo_deferidos");
        Sql.Append("  (");
        Sql.Append("	t0101_id_rfb, ");
        Sql.Append("	t0101_cnpj, ");
        Sql.Append("	t0101_tp_estab, ");
        Sql.Append("	t0101_uf, ");
        Sql.Append("	t0101_cod_mun, ");
        Sql.Append("	t0101_uf_ant, ");
        Sql.Append("	t0101_cod_mun_ant, ");
        Sql.Append("	t0101_or, ");
        Sql.Append("	t0101_dt_deferimento, ");
        Sql.Append("	t0101_dt_criacao, ");
        Sql.Append("	t0101_dbe, ");
        Sql.Append("	t0101_cnpj_matriz, ");
        Sql.Append("	t0101_nat_juridica, ");
        Sql.Append("	t0101_cod_serv_rfb, ");
        Sql.Append("	t0101_indicador_mei, ");
        Sql.Append("	t0101_cnpj_mei, ");
        Sql.Append("	t0101_dt_evento_mei, ");
        //Sql.Append("	t0101_xml_rfb, ");
        //Sql.Append("	t0101_protocolo_regin, ");
        Sql.Append("	t0101_data_recebido_rfb, ");
        //Sql.Append("	t0101_xml_rfb_09, ");
        Sql.Append("	t0101_nome_fantasia,");
        Sql.Append("	t0101_viabilid_associada,");
        Sql.Append("	t0101_numero_ato_oficio, ");
        Sql.Append("	t0101_evento_simples, ");
        Sql.Append("	t0101_marcado_s24, ");
        Sql.Append("	T0101_ALTERA_MATRIZ_FORA, ");
        Sql.Append("	T0101_ORGAO_DEFERIMENTO, ");
        Sql.Append("	t0101_codigoconvenioato, ");
        Sql.Append("	t0101_numeroProtocolo, "); 
        Sql.Append("	t0101_codigo_retorno ");
        Sql.Append("  )");
        Sql.Append(" Values ");
        Sql.Append("  (");
        Sql.Append("	:v_t0101_id_rfb, ");
        Sql.Append("	:v_t0101_cnpj, ");
        Sql.Append("	evalnumeric(:v_t0101_tp_estab), ");
        Sql.Append("	:v_t0101_uf, ");
        Sql.Append("	evalnumeric(:v_t0101_cod_mun), ");
        Sql.Append("	:v_t0101_uf_ant, ");
        Sql.Append("	evalnumeric(:v_t0101_cod_mun_ant), ");
        Sql.Append("	:v_t0101_or, ");
        Sql.Append("	evaldate(:v_t0101_dt_deferimento), ");
        Sql.Append("	sysdate, ");
        Sql.Append("	:v_t0101_dbe, ");
        Sql.Append("	:v_t0101_cnpj_matriz, ");
        Sql.Append("	:v_t0101_nat_juridica, ");
        Sql.Append("	:v_t0101_cod_serv_rfb, ");
        Sql.Append("	:v_t0101_indicador_mei, ");
        Sql.Append("	:v_t0101_cnpj_mei, ");
        Sql.Append("	evaldate(:v_t0101_dt_evento_mei), ");
        //Sql.Append("	:v_t0101_protocolo_regin, ");
        Sql.Append("	evaldate(:v_t0101_data_recebido_rfb), ");
        Sql.Append("	:v_t0101_nome_fantasia, ");
        Sql.Append("	:v_t0101_viabilid_associada, ");
        Sql.Append("	:v_t0101_numero_ato_oficio, ");
        Sql.Append("	:v_t0101_evento_simples, ");
        Sql.Append("	evalnumeric(:v_t0101_marcado_s24), ");
        Sql.Append("	:v_T0101_ALTERA_MATRIZ_FORA, ");
        Sql.Append("	:v_T0101_ORGAO_DEFERIMENTO, ");
        Sql.Append("	:v_t0101_codigoconvenioato, ");
        Sql.Append("	:v_t0101_numeroProtocolo, "); 
        Sql.Append("	:v_t0101_codigo_retorno");

        Sql.Append("  )");

        // Codigo Update ******************* 
        SqlU.Append(" Update     T0101_RFB_PROCESSO_DEFERIDOS Set ");
        SqlU.Append("		t0101_cnpj = :v_t0101_cnpj, ");
        SqlU.Append("		t0101_tp_estab = evalnumeric(:v_t0101_tp_estab), ");
        SqlU.Append("		t0101_uf = :v_t0101_uf, ");
        SqlU.Append("		t0101_cod_mun = evalnumeric(:v_t0101_cod_mun), ");
        SqlU.Append("		t0101_uf_ant = :v_t0101_uf_ant, ");
        SqlU.Append("		t0101_cod_mun_ant = evalnumeric(:v_t0101_cod_mun_ant), ");
        SqlU.Append("		t0101_or = :v_t0101_or, ");
        SqlU.Append("		t0101_dt_deferimento = evaldate(:v_t0101_dt_deferimento), ");
        SqlU.Append("		t0101_dbe = :v_t0101_dbe, ");
        SqlU.Append("		t0101_cnpj_matriz = :v_t0101_cnpj_matriz, ");
        SqlU.Append("		t0101_nat_juridica = :v_t0101_nat_juridica, ");
        SqlU.Append("		t0101_cod_serv_rfb = :v_t0101_cod_serv_rfb, ");
        SqlU.Append("		t0101_indicador_mei = :v_t0101_indicador_mei, ");
        SqlU.Append("		t0101_cnpj_mei = :v_t0101_cnpj_mei, ");
        SqlU.Append("		t0101_dt_evento_mei = evaldate(:v_t0101_dt_evento_mei), ");
        //SqlU.Append("		t0101_protocolo_regin = :v_t0101_protocolo_regin, ");
        SqlU.Append("		t0101_data_recebido_rfb = evaldate(:v_t0101_data_recebido_rfb), ");
        SqlU.Append("		t0101_nome_fantasia = :v_t0101_nome_fantasia,");
        SqlU.Append("		t0101_viabilid_associada = :v_t0101_viabilid_associada,");
        SqlU.Append("		t0101_numero_ato_oficio = :v_t0101_numero_ato_oficio, ");
        SqlU.Append("		t0101_evento_simples = :v_t0101_evento_simples, ");
        SqlU.Append("		t0101_marcado_s24 = evalnumeric(:v_t0101_marcado_s24), ");
        SqlU.Append("		t0101_codigo_retorno = :v_t0101_codigo_retorno,");
        SqlU.Append("		T0101_ALTERA_MATRIZ_FORA = :v_T0101_ALTERA_MATRIZ_FORA,");
        SqlU.Append("		t0101_codigoconvenioato = :v_t0101_codigoconvenioato, ");
        SqlU.Append("		t0101_numeroProtocolo = :v_t0101_numeroProtocolo, "); 
        SqlU.Append("		T0101_ORGAO_DEFERIMENTO = :v_T0101_ORGAO_DEFERIMENTO");

        
        SqlU.Append(" Where	t0101_id_rfb = :v_t0101_id_rfb ");


        try
        {
            if (_t0101_nat_juridica == null)
                _t0101_nat_juridica = "";

            // Codigo dbParameter ******************* 
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_id_rfb", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_id_rfb));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_cnpj", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_cnpj));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_tp_estab", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_tp_estab));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_uf", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_uf));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_cod_mun", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_cod_mun));

            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_uf_ant", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_uf_ant));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_cod_mun_ant", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_cod_mun_ant));


            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_or", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_or));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_dt_deferimento", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_dt_deferimento));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_dbe", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_dbe));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_cnpj_matriz", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_cnpj_matriz));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_nat_juridica", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_nat_juridica));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_cod_serv_rfb", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_cod_serv_rfb));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_indicador_mei", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_indicador_mei));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_cnpj_mei", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_cnpj_mei));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_dt_evento_mei", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_dt_evento_mei));
            //cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_xml_rfb", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_xml_rfb));
            //cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_protocolo_regin", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_protocolo_regin));
            //cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_xml_rfb_09", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_xml_rfb_09));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_data_recebido_rfb", OracleType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_data_recebido_rfb));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_nome_fantasia", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_nome_fantasia));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_viabilid_associada", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_viabilid_associada));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_numero_ato_oficio", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_numero_ato_oficio));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_evento_simples", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_evento_simples));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_marcado_s24", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_marcado_s24));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_codigo_retorno", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_codigo_retorno));
            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_codigoconvenioato", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_codigoconvenioato));

            cmdToExecute.Parameters.Add(new OracleParameter("v_t0101_numeroProtocolo", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_numeroProtocolo));


            

            cmdToExecute.Parameters.Add(new OracleParameter("v_T0101_ALTERA_MATRIZ_FORA", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_altera_matriz_fora));
            cmdToExecute.Parameters.Add(new OracleParameter("v_T0101_ORGAO_DEFERIMENTO", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_orgao_deferimento));

            cmdToExecute.CommandText = SqlU.ToString();
            cmdToExecute.Connection = _mainConnectionORACLE;

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

            int atualizado = cmdToExecute.ExecuteNonQuery();

            if (atualizado == 0)
            {
                cmdToExecute.CommandText = Sql.ToString();
                atualizado = cmdToExecute.ExecuteNonQuery();
            }

            

            if (atualizado > 0)
            {

                string sqlD = " delete t0102_rfb_processo_arquivos where t0102_id_rfb = :v_t0102_id_rfb";
                cmdToExecute.Parameters.Clear();
                cmdToExecute.Parameters.Add(new OracleParameter("v_t0102_id_rfb", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_id_rfb));
                cmdToExecute.CommandText = sqlD;
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.ExecuteNonQuery();

                if (_t0101_xml_rfb != "" || _t0101_xml_rfb_09 != "" || _t0102_xml_sxx != "" || _t0102_xml_regin != "")
                {
                    sqlD = " insert into t0102_rfb_processo_arquivos  (t0102_id_rfb) values (:v_t0102_id_rfb)";
                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.Parameters.Add(new OracleParameter("v_t0102_id_rfb", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _t0101_id_rfb));
                    cmdToExecute.CommandText = sqlD;
                    cmdToExecute.CommandType = CommandType.Text;
                    cmdToExecute.ExecuteNonQuery();


                    sqlD = " update t0102_rfb_processo_arquivos set t0102_xml_rfb = :v_t0102_xml_rfb, t0102_xml_rfb_09 = :v_t0102_xml_rfb_09, T0102_XML_SXX = :v_T0102_XML_SXX, t0102_xml_regin = :v_t0102_xml_regin where t0102_id_rfb = " + t0101_id_rfb;
                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.Parameters.Add(new OracleParameter("v_t0102_xml_rfb", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _t0101_xml_rfb));
                    cmdToExecute.Parameters.Add(new OracleParameter("v_t0102_xml_rfb_09", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _t0101_xml_rfb_09));
                    cmdToExecute.Parameters.Add(new OracleParameter("v_T0102_XML_SXX", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _t0102_xml_sxx));
                    cmdToExecute.Parameters.Add(new OracleParameter("v_t0102_xml_regin", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _t0102_xml_regin));
                    cmdToExecute.CommandText = sqlD;
                    cmdToExecute.CommandType = CommandType.Text;
                    cmdToExecute.ExecuteNonQuery();


                    //sqlD = " update t0101_rfb_processo_deferidos set t0101_xml_rfb = :t0101_xml_rfb, t0101_xml_rfb_09 = :t0101_xml_rfb_09 where t0101_id_rfb = " + t0101_id_rfb;
                    //cmdToExecute.Parameters.Clear();
                    //cmdToExecute.Parameters.Add(new OracleParameter("t0101_xml_rfb", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _t0101_xml_rfb));
                    //cmdToExecute.Parameters.Add(new OracleParameter("t0101_xml_rfb_09", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _t0101_xml_rfb_09));
                    //cmdToExecute.CommandText = sqlD;
                    //cmdToExecute.CommandType = CommandType.Text;
                    //cmdToExecute.ExecuteNonQuery();
                }
            }


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
                _mainConnectionORACLE.Close();
            }
            cmdToExecute.Dispose();
        }
    }

    public void EventoUpdate(decimal Id_RFB, string Cod_Evento)
    {
        using (OracleCommand cmdToExecute = new OracleCommand())
        {
            try
            {

                string SqlD = " delete from r0101_processo_ev_rfb t " +
                                " where t.r0101_t0101_id_rfb = " + Id_RFB + 
                                 " and t.r0101_mer_cod_evento = " + Cod_Evento;


                string Sql = " insert into r0101_processo_ev_rfb t " +
                                 " (t.r0101_t0101_id_rfb, t.r0101_mer_cod_evento) " +
                                 " values (" + Id_RFB + "," + Cod_Evento + ") ";


                
                cmdToExecute.Connection = _mainConnectionORACLE;

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

                //Apago primeiro
                cmdToExecute.CommandText = SqlD;
                cmdToExecute.ExecuteNonQuery();

                cmdToExecute.CommandText = Sql;
                cmdToExecute.ExecuteNonQuery();
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
                    _mainConnectionORACLE.Close();
                }
                cmdToExecute.Dispose();
            }
        }

    }
    public int EventoQueryQtd(decimal Id_RFB)
    {
        using (OracleCommand cmdToExecute = new OracleCommand())
        {
            try
            {
                string Sql = " select count(*) from r0101_processo_ev_rfb " +
                                 " where r0101_t0101_id_rfb = " + Id_RFB;


                cmdToExecute.CommandText = Sql;
                cmdToExecute.Connection = _mainConnectionORACLE;

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

                int pCount = int.Parse(cmdToExecute.ExecuteScalar().ToString());
                return pCount;
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
                    _mainConnectionORACLE.Close();
                }
                cmdToExecute.Dispose();
            }
        }

    }

    public void EventoDelete(decimal Id_RFB)
    {
        using (OracleCommand cmdToExecute = new OracleCommand())
        {
            try
            {
                string Sql = " delete from r0101_processo_ev_rfb " +
                                 " where r0101_t0101_id_rfb = " + Id_RFB;


                cmdToExecute.CommandText = Sql;
                cmdToExecute.Connection = _mainConnectionORACLE;

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
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnectionORACLE.Close();
                }
                cmdToExecute.Dispose();
            }
        }

    }
    public void UpdateSIMPLES_ARQ_LINHA(decimal _tipo_arquivo, decimal _nro_linha, string _linha)
    {
        StringBuilder Sql = new StringBuilder();
        StringBuilder SqlU = new StringBuilder();
        OracleCommand cmdToExecute = new OracleCommand();

        cmdToExecute.CommandType = CommandType.Text;
        Sql.Append(" Insert into T08016_ARQ_EVEN_SN");
        Sql.Append("  (");
        Sql.Append("	t08016_tipo_arquivo, ");
        Sql.Append("	t08016_nro_linha, ");
        Sql.Append("	t08016_linha ");
        Sql.Append("  )");
        Sql.Append(" Values ");
        Sql.Append("  (");
        Sql.Append("	:v_tipo_arquivo, ");
        Sql.Append("	:v_nro_linha, ");
        Sql.Append("	:v_linha ");
        Sql.Append("  )");

        // Codigo Update ******************* 
        try
        {
            /// Tipo Normal
            //string _mcl_linha_str_nova = _linha_str;

            //Tipo Baixa ou desenquadramento
            //string _mcl_cnpj = _mcl_linha_str.Substring(0, 14);
            //string _mcl_cpf = "";// _mcl_linha_str.Substring(0, 11);
            //string _mcl_nire = "";// _mcl_linha_str.Substring(2185, 11);
            //string _mcl_linha_str_nova = _mcl_linha_str.Substring(0, _mcl_linha_str.Length);

            // Codigo dbParameter ******************* 
            cmdToExecute.Parameters.Add(new OracleParameter("v_tipo_arquivo", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _tipo_arquivo));
            cmdToExecute.Parameters.Add(new OracleParameter("v_nro_linha", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _nro_linha));
            cmdToExecute.Parameters.Add(new OracleParameter("v_linha", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _linha));


            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.Connection = _mainConnectionORACLE;

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

            if (cmdToExecute.ExecuteNonQuery() == 0)
            {
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();
            }
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
                _mainConnectionORACLE.Close();
            }
            cmdToExecute.Dispose();
        }
    }

    public void UpdateMEI_ARQ_LINHA(decimal _mcl_maa_id, decimal _mcl_numero_linha, string _mcl_linha_str)
    {
        StringBuilder Sql = new StringBuilder();
        StringBuilder SqlU = new StringBuilder();
        OracleCommand cmdToExecute = new OracleCommand();
        cmdToExecute.CommandType = CommandType.Text;
        Sql.Append(" Insert into mac_carga_mei_arq_linha");
        Sql.Append("  (");
        Sql.Append("	mcl_maa_id, ");
        Sql.Append("	mcl_numero_linha, ");
        Sql.Append("	mcl_cnpj, ");
        Sql.Append("	mcl_cpf, ");
        Sql.Append("	mcl_nire, ");
        Sql.Append("	mcl_linha_str ");
        Sql.Append("  )");
        Sql.Append(" Values ");
        Sql.Append("  (");
        Sql.Append("	:v_mcl_maa_id, ");
        Sql.Append("	:v_mcl_numero_linha, ");
        Sql.Append("	:v_mcl_cnpj, ");
        Sql.Append("	:v_mcl_cpf, ");
        Sql.Append("	:v_mcl_nire, ");
        Sql.Append("	:v_mcl_linha_str ");
        Sql.Append("  )");

        // Codigo Update ******************* 
        try
        {
            /// Tipo Normal
            string _mcl_cnpj = _mcl_linha_str.Substring(11, 14);
            string _mcl_cpf = _mcl_linha_str.Substring(0, 11);
            string _mcl_nire = _mcl_linha_str.Substring(2185, 11);
            string _mcl_linha_str_nova = _mcl_linha_str.Substring(0, 2484);

            //Tipo Baixa ou desenquadramento
            //string _mcl_cnpj = _mcl_linha_str.Substring(0, 14);
            //string _mcl_cpf = "";// _mcl_linha_str.Substring(0, 11);
            //string _mcl_nire = "";// _mcl_linha_str.Substring(2185, 11);
            //string _mcl_linha_str_nova = _mcl_linha_str.Substring(0, _mcl_linha_str.Length);

            // Codigo dbParameter ******************* 
            cmdToExecute.Parameters.Add(new OracleParameter("v_mcl_maa_id", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _mcl_maa_id));
            cmdToExecute.Parameters.Add(new OracleParameter("v_mcl_numero_linha", OracleType.Number, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _mcl_numero_linha));
            cmdToExecute.Parameters.Add(new OracleParameter("v_mcl_cnpj", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _mcl_cnpj));
            cmdToExecute.Parameters.Add(new OracleParameter("v_mcl_cpf", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _mcl_cpf));
            cmdToExecute.Parameters.Add(new OracleParameter("v_mcl_nire", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _mcl_nire));
            cmdToExecute.Parameters.Add(new OracleParameter("v_mcl_linha_str", OracleType.VarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _mcl_linha_str_nova));


            cmdToExecute.CommandText = Sql.ToString();
            cmdToExecute.Connection = _mainConnectionORACLE;

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

            if (cmdToExecute.ExecuteNonQuery() == 0)
            {
                cmdToExecute.CommandText = Sql.ToString();
                cmdToExecute.ExecuteNonQuery();
            }
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
                _mainConnectionORACLE.Close();
            }
            cmdToExecute.Dispose();
        }
    }

    public void DeletePeriodosSimplesOracle(OracleTransaction cp, string pCNPJ)
    {
       

        string cnpj = pCNPJ.Substring(0, 8);

        using (OracleCommand cmdToExecuteSql = new OracleCommand())
        {
            cmdToExecuteSql.Transaction = cp;
            cmdToExecuteSql.Connection = cp.Connection;

            StringBuilder sqlD = new StringBuilder();
            sqlD.AppendLine(" delete psc_periodos_simples ");
            sqlD.AppendLine(" where pps_cnpj = :v_pps_cnpj ");

            cmdToExecuteSql.Parameters.Clear();
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_pps_cnpj", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cnpj));
     
            cmdToExecuteSql.CommandText = sqlD.ToString();
            cmdToExecuteSql.CommandType = CommandType.Text;
            cmdToExecuteSql.ExecuteNonQuery();

        }
    }


    public void UpdatePeriodosSimples(OracleTransaction cp, string pCNPJ, string indicadorMeiSimples, string pId, string dataInclusao, string dataExclusao, string indicadorPeriodoCancelado, string numeroOpcao)
    {
        DateTime oDateInicio = DateTime.MinValue;
        DateTime oDateFim = DateTime.MinValue;

        oDateInicio = DateTime.ParseExact(dataInclusao, "yyyyMMdd", null);
        
        if (dataExclusao != "" && dataExclusao != "00000000" && dataExclusao != "null")
            oDateFim = DateTime.ParseExact(dataExclusao, "yyyyMMdd", null);

        string cnpj = pCNPJ.Substring(0, 8);

        using (OracleCommand cmdToExecuteSql = new OracleCommand())
        {
            cmdToExecuteSql.Transaction = cp;
            cmdToExecuteSql.Connection = cp.Connection;

            StringBuilder sqlD = new StringBuilder();
            //sqlD.AppendLine(" update psc_periodos_simples ");
            //sqlD.AppendLine(" set pps_per_final = EvalDate(:v_pps_per_final), ");
            //sqlD.AppendLine("     pps_dt_atualizacao = sysdate, ");
            //sqlD.AppendLine("     pps_t01_id = :v_pps_t01_id, ");
            //sqlD.AppendLine("     pps_per_cancelado = :v_pps_per_cancelado, ");
            //sqlD.AppendLine("     pps_numero_opcao = :v_pps_numero_opcao ");
            //sqlD.AppendLine(" where pps_cnpj = :v_pps_cnpj ");
            //sqlD.AppendLine("    and pps_tipo = :v_pps_tipo ");
            //sqlD.AppendLine("    and pps_per_inicial = :v_pps_per_inicial");

            cmdToExecuteSql.Parameters.Clear();
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_pps_cnpj", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cnpj));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_pps_tipo", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, indicadorMeiSimples));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_pps_per_inicial", OracleType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, oDateInicio));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_pps_per_final", OracleType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, oDateFim));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_pps_t01_id", OracleType.Number, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pId));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_pps_per_cancelado", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, indicadorPeriodoCancelado));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_pps_numero_opcao", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, numeroOpcao));


            //cmdToExecuteSql.CommandText = sqlD.ToString();
            //cmdToExecuteSql.CommandType = CommandType.Text;
            //if (cmdToExecuteSql.ExecuteNonQuery() == 0)
            //{
            sqlD = new StringBuilder();

            sqlD.AppendLine(" insert into psc_periodos_simples ");
            sqlD.AppendLine(" (pps_cnpj, pps_tipo, pps_per_inicial, pps_per_final, pps_dt_atualizacao, pps_t01_id, pps_per_cancelado, pps_numero_opcao) ");
            sqlD.AppendLine(" values ");
            sqlD.AppendLine("   (:v_pps_cnpj, :v_pps_tipo, :v_pps_per_inicial, EvalDate(:v_pps_per_final), sysdate, :v_pps_t01_id, :v_pps_per_cancelado, :v_pps_numero_opcao) ");

            cmdToExecuteSql.CommandText = sqlD.ToString();
            cmdToExecuteSql.CommandType = CommandType.Text;
            cmdToExecuteSql.ExecuteNonQuery();

          //  }

        }
    }


    public void UpdateXmlSIMPLESMei(OracleTransaction cp, string pCNPJ, string pId, string pXmlSimples)
    {

        WsRFBReginV2.WsServices15RFB.simplesNacional simples = new WsRFBReginV2.WsServices15RFB.simplesNacional();
        simples = (WsRFBReginV2.WsServices15RFB.simplesNacional)GlobalV1.CreateObject(pXmlSimples, simples);

        if (simples.periodoMei != null || simples.periodoSimples != null)
            DeletePeriodosSimplesOracle(cp, pCNPJ);

        if (simples.periodoMei != null)
        {
            foreach (WsRFBReginV2.WsServices15RFB.periodo periodo in simples.periodoMei)
            {
                string dataExclusao = periodo.dataExclusao == null ? "" : periodo.dataExclusao;
                string dataInclusao = periodo.dataInclusao;
                string indicadorPeriodoCancelado = periodo.indicadorPeriodoCancelado;
                string numeroOpcao = periodo.numeroOpcao;
                UpdatePeriodosSimples(cp, pCNPJ, "2", pId, dataInclusao, dataExclusao, indicadorPeriodoCancelado, numeroOpcao);
            }
        }

        if (simples.periodoSimples != null)
        {
            foreach (WsRFBReginV2.WsServices15RFB.periodo periodo in simples.periodoSimples)
            {
                string dataExclusao = periodo.dataExclusao == null ? "" : periodo.dataExclusao;
                string dataInclusao = periodo.dataInclusao;
                string indicadorPeriodoCancelado = periodo.indicadorPeriodoCancelado;
                string numeroOpcao = periodo.numeroOpcao;
                UpdatePeriodosSimples(cp, pCNPJ, "1", pId, dataInclusao, dataExclusao, indicadorPeriodoCancelado, numeroOpcao);
            }
        }

        
    }
}
