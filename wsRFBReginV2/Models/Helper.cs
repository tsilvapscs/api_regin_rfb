using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
//using dal = psc.ApplicationBlocks.DAL.MySqlHelper;
//using RCPJ.DAL.Entities;
using psc.Receita.ConnectionBase;
using System.Data.OracleClient;
using System.Configuration;
using System.Data.SqlClient;
using psc.Ruc.Tablelas.Ruc;
using psc.Ruc.Tablelas.DAL.Ruc;
using WsRFBReginV2.Models;

/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{
    public Helper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string validaNulo(object pValue)
    {
        if (pValue == null)
        {
            return "";
        }
        return pValue.ToString();
    }

    public static string CalDvMunicipio(string CodMUnicipio)
    {
        if (CodMUnicipio != "")
        {
            string codMuni = psc.Framework.General.CalculateVerificationDigit(CodMUnicipio, 11).ToString();

            return CodMUnicipio + codMuni;
        }

        return "";

    }

    public void gravarContadorWs35(OracleTransaction cp, string pXml, string pProtocolo, DataTable DtTipoLogra, string pCodServico, string TipoColeta)
    {
        string CpfCNPJContador = "";
        string rrp_crc_ctdrPJ = "";
        string rrp_uf_crc_ctdrPJ = "";
        string RRP_TIPO_CRC_CTDRPJ = "";
        string RRP_CLASS_CRC_CTDRPJ = "";
        string NomePJ = "";

        string rrp_crc_ctdrPF = "";
        string rrp_uf_crc_ctdrPF = "";
        string RRP_TIPO_CRC_CTDRPF = "";
        string RRP_CLASS_CRC_CTDRPF = "";
        string NomePF = "";
        string rpr_email = "";
        string CPFContador = "";

        WsRFBReginV2.WsServices17RFB.dadosCNPJ dadosCNPJ = new WsRFBReginV2.WsServices17RFB.dadosCNPJ();
        WsRFBReginV2.WsServicesReginRFB.serviceResponse dados = new WsRFBReginV2.WsServicesReginRFB.serviceResponse();

        WsRFBReginV2.WsServicesReginRFB.endereco endContador = new WsRFBReginV2.WsServicesReginRFB.endereco();

        psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();

        if (TipoColeta == "fcpj")
        {
            dados = (WsRFBReginV2.WsServicesReginRFB.serviceResponse)GlobalV1.CreateObject(pXml, dados);

            if (dados.dadosRedesim.fcpj == null)
            {
                return;
            }
            if (dados.dadosRedesim.fcpj.cnpjEmpresaContabil == null && dados.dadosRedesim.fcpj.cpfContadorPF == null)
            {
                return;
            }

            CpfCNPJContador = validaNulo(dados.dadosRedesim.fcpj.cnpjEmpresaContabil);
            rrp_crc_ctdrPJ = validaNulo(dados.dadosRedesim.fcpj.seqCRCempresaContabil);
            rrp_uf_crc_ctdrPJ = validaNulo(dados.dadosRedesim.fcpj.ufCRCempresaContabil);
            RRP_TIPO_CRC_CTDRPJ = validaNulo(dados.dadosRedesim.fcpj.codTipoCRCempresaContabil);
            RRP_CLASS_CRC_CTDRPJ = validaNulo(dados.dadosRedesim.fcpj.codClassificEmpresaContabil);
            NomePJ = validaNulo(dados.dadosRedesim.fcpj.nomeEmpresaContabil);

            rrp_crc_ctdrPF = validaNulo(dados.dadosRedesim.fcpj.numSeqContadorPF);
            rrp_uf_crc_ctdrPF = validaNulo(dados.dadosRedesim.fcpj.ufContadorPF);
            RRP_TIPO_CRC_CTDRPF = validaNulo(dados.dadosRedesim.fcpj.codTipoCRCcontadorPF);
            RRP_CLASS_CRC_CTDRPF = validaNulo(dados.dadosRedesim.fcpj.codClassificCRCcontadorPF);
            NomePF = validaNulo(dados.dadosRedesim.fcpj.nomeContadorPF);
            rpr_email = "";
            CPFContador = validaNulo(dados.dadosRedesim.fcpj.cpfContadorPF);

            if (dados.dadosRedesim.fcpj.contatoContadorPf != null)
            {
                rpr_email = validaNulo(dados.dadosRedesim.fcpj.contatoContadorPf.correioEletronico);
            }

            if (dados.dadosRedesim.fcpj.endContadorPf != null)
            {
                endContador = dados.dadosRedesim.fcpj.endContadorPf;
            }

            if (endContador.bairro != null && endContador.bairro != "")
            {
                cc.Bairro = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.bairro);
                cc.Cep = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.cep);
                cc.Codigo_municipio = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.codMunicipio);
                cc.Complemento = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.complementoLogradouro);
                cc.Logradouro = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.logradouro);
                cc.Numero = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.numLogradouro);
                cc.Pais = "";
                cc.TipLogradoro = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.codTipoLogradouro);
                cc.Uf = validaNulo(dados.dadosRedesim.fcpj.endContadorPf.uf);


                cc.TrataEndereco(ref cc, DtTipoLogra);
            }
        }
        if (TipoColeta == "dadosCNPJ")
        {
            dadosCNPJ = (WsRFBReginV2.WsServices17RFB.dadosCNPJ)GlobalV1.CreateObject(pXml, dadosCNPJ);

            if (dadosCNPJ == null)
            {
                return;
            }
            if (dadosCNPJ == null && dadosCNPJ.cpfContadorPF == null)
            {
                return;
            }

            CpfCNPJContador = validaNulo(dadosCNPJ.cnpjContadorPJ);
            rrp_crc_ctdrPJ = validaNulo(dadosCNPJ.numSeqContadorPJ);
            rrp_uf_crc_ctdrPJ = validaNulo(dadosCNPJ.ufContadorPJ);
            RRP_TIPO_CRC_CTDRPJ = validaNulo(dadosCNPJ.codTipoCRCcontadorPJ);
            RRP_CLASS_CRC_CTDRPJ = validaNulo(dadosCNPJ.codClassificCRCcontadorPJ);
            //NomePJ = validaNulo(dadosCNPJ.nomeEmpresaContabil);

            rrp_crc_ctdrPF = validaNulo(dadosCNPJ.numSeqContadorPF);
            rrp_uf_crc_ctdrPF = validaNulo(dadosCNPJ.ufContadorPF);
            RRP_TIPO_CRC_CTDRPF = validaNulo(dadosCNPJ.codTipoCRCcontadorPF);
            RRP_CLASS_CRC_CTDRPF = validaNulo(dadosCNPJ.codClassificCRCcontadorPF);
            NomePF = "";
            rpr_email = "";
            CPFContador = validaNulo(dadosCNPJ.cpfContadorPF);

            if (NomePF == "" && CPFContador != "")
            {
                WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB regin = new WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB();
                WsRFBReginV2.WsServicesReginRFB.Retorno ws0911Socio = new WsRFBReginV2.WsServicesReginRFB.Retorno();
                WsRFBReginV2.WsServicesReginRFB.endereco1 enderecoPJ = new WsRFBReginV2.WsServicesReginRFB.endereco1();

                regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                ws0911Socio = regin.ServiceWs09(CPFContador);

                if (ws0911Socio.status != "OK")
                {
                    throw new Exception("Erro buscando o contador PF do CNPJ: " + CPFContador);
                }

                NomePF = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].nome;
            }


            if (NomePJ == "" && CpfCNPJContador != "")
            {
                WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB regin = new WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB();
                WsRFBReginV2.WsServicesReginRFB.Retorno ws0911Socio = new WsRFBReginV2.WsServicesReginRFB.Retorno();
                WsRFBReginV2.WsServicesReginRFB.endereco1 enderecoPJ = new WsRFBReginV2.WsServicesReginRFB.endereco1();

                regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                ws0911Socio = regin.ServiceWs11(CpfCNPJContador);

                if (ws0911Socio.status != "OK")
                {
                    throw new Exception("Erro buscando o contador PJ do CNPJ: " + CpfCNPJContador);
                }

                NomePJ = ws0911Socio.oCNPJResponse.dadosCNPJ[0].nomeEmpresarial;

                enderecoPJ = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco;

                cc.Bairro = validaNulo(enderecoPJ.bairro);
                cc.Cep = validaNulo(enderecoPJ.cep);
                cc.Codigo_municipio = validaNulo(enderecoPJ.codMunicipio);
                cc.Complemento = validaNulo(enderecoPJ.complementoLogradouro);
                cc.Logradouro = validaNulo(enderecoPJ.logradouro);
                cc.Numero = validaNulo(enderecoPJ.numLogradouro);
                cc.Pais = "";
                cc.TipLogradoro = validaNulo(enderecoPJ.codTipoLogradouro);
                cc.Uf = validaNulo(enderecoPJ.uf);


                cc.TrataEndereco(ref cc, DtTipoLogra);


            }

        }

       

        using (OracleCommand cmdToExecuteSql = new OracleCommand())
        {

            cmdToExecuteSql.Transaction = cp;
            cmdToExecuteSql.Connection = cp.Connection;

         

            if (CpfCNPJContador == "")
            {
                CpfCNPJContador = CPFContador;
                rrp_crc_ctdrPJ = rrp_crc_ctdrPF;
                rrp_uf_crc_ctdrPJ = rrp_uf_crc_ctdrPF;
                RRP_TIPO_CRC_CTDRPJ = RRP_TIPO_CRC_CTDRPF;
                RRP_CLASS_CRC_CTDRPJ = RRP_CLASS_CRC_CTDRPF;
                NomePJ = NomePF;

                rrp_crc_ctdrPF = "";
                rrp_uf_crc_ctdrPF = "";
                RRP_TIPO_CRC_CTDRPF = "";
                RRP_CLASS_CRC_CTDRPF = "";
                NomePF = "";
                CPFContador = "";

            }

            Ruc_Relat_Prof_co_oracle rp = new Ruc_Relat_Prof_co_oracle();
            // rp.MainConnectionProvider = cp;
            rp.rrp_rge_pra_protocolo = pProtocolo;
            rp.rrp_cgc_cpf_secd = CpfCNPJContador;
            rp.rrp_tge_ttip_relac = 24;
            rp.rrp_tge_vtip_relac = 3;
            rp.rrp_fec_inic_part = DateTime.Now;
            rp.rrp_tge_tcod_qual = 23;
            rp.rrp_tge_vcod_qual = 9999;
            rp.rrp_crc_ctdr = rrp_crc_ctdrPJ;
            rp.rrp_uf_crc_ctdr = rrp_uf_crc_ctdrPJ;

            rp.RRP_TIPO_CRC_CTDR = RRP_TIPO_CRC_CTDRPJ;
            if (RRP_CLASS_CRC_CTDRPJ != null && RRP_CLASS_CRC_CTDRPJ != "")
            {
                rp.RRP_CLASS_CRC_CTDR = decimal.Parse(RRP_CLASS_CRC_CTDRPJ);
            }

            rp.rrp_desc_doc = "";
            rp.rrp_tus_cod_usr = "JUNTA";
            rp.rrp_cnpj_vacio = int.MinValue;

            decimal CapitalSocio = 0;
            decimal PercentualSocio = 0;

            rp.rrp_porc_part = PercentualSocio;
            rp.RRP_VAL_CAP_SOC = CapitalSocio;
            rp.Update(cp);


            Ruc_Prof_co_oracle rf = new Ruc_Prof_co_oracle();
            //   rp.MainConnectionProvider = cp;
            rf.rpr_rge_pra_protocolo = pProtocolo;
            rf.rpr_tge_tpais = 22;
            rf.rpr_tge_vpais = 105;// Decimal.Parse(s.EndPais);
            rf.rpr_cgc_cpf_secd = CpfCNPJContador;
            rf.rpr_tge_ttip_pers = 233;
            rf.rpr_tge_vtip_pers = CpfCNPJContador.Length < 12 ? 1 : 2;

            rf.rpr_email = rpr_email;

            rf.rpr_urbanizacion = cc.Bairro;
            rf.rpr_zona_postal = cc.Cep;
            if (cc.Codigo_municipio == "")
                cc.Codigo_municipio = "0";
            rf.rpr_tmu_cod_mun = decimal.Parse(cc.Codigo_municipio);
            rf.rpr_ident_comp = cc.Complemento;
            rf.rpr_direccion = cc.Logradouro;
            rf.rpr_nume = cc.Numero;
            rf.rpr_tge_vpais = decimal.Parse(cc.Pais);
            rf.rpr_ttl_tip_logradoro = cc.TipLogradoro;
            rf.rpr_tes_cod_estado = cc.Uf;

            rf.rpr_nomb = NomePJ;

            rf.UpdateContador(cp);

            StringBuilder sqlD = new StringBuilder();
            sqlD.AppendLine(" update TAB_INFORM_EXTRA_JUNTA ");
            sqlD.AppendLine(" set	 TIE_CPF_CNPJ = :v_TIE_CPF_CNPJ, ");
            sqlD.AppendLine(" 		 TIE_EMAIL = :v_TIE_EMAIL ");
            sqlD.AppendLine(" where	TIE_PROTOCOLO = :v_TIE_PROTOCOLO ");
            sqlD.AppendLine(" and	TIE_TIPO_RELACAO = 3 ");

            cmdToExecuteSql.Parameters.Clear();
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_CPF_CNPJ", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, CpfCNPJContador));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_EMAIL", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, rpr_email));

            cmdToExecuteSql.CommandText = sqlD.ToString();
            cmdToExecuteSql.CommandType = CommandType.Text;
            if (cmdToExecuteSql.ExecuteNonQuery() == 0)
            {
                sqlD = new StringBuilder();
                sqlD.AppendLine(" Insert into TAB_INFORM_EXTRA_JUNTA (TIE_PROTOCOLO, TIE_TIPO_RELACAO, TIE_CPF_CNPJ, TIE_EMAIL) ");
                sqlD.AppendLine(" Values (:v_TIE_PROTOCOLO, 3, :v_TIE_CPF_CNPJ, :v_TIE_EMAIL) ");
                cmdToExecuteSql.CommandText = sqlD.ToString();
                cmdToExecuteSql.CommandType = CommandType.Text;
                cmdToExecuteSql.ExecuteNonQuery();

            }


            if (CPFContador != "")
            {
                RUC_REPRESENTANTES_CO_ORACLE repre = new RUC_REPRESENTANTES_CO_ORACLE();
                string cpfRepresentanteLegal = CPFContador;

                repre.rsr_pra_protocolo = pProtocolo;
                repre.rsr_cgc_cpf_princ = CpfCNPJContador;
                repre.rsr_cgc_cpf_secd = CPFContador;
                repre.rsr_crc_ctdr = rrp_crc_ctdrPF;
                repre.rsr_uf_crc_ctdr = rrp_uf_crc_ctdrPF;
                repre.rsr_tipo = 2; //Representante contador
                repre.rsr_nomb = NomePF;
                repre.rsr_tge_tcod_qual = 23;
                repre.rsr_tge_vcod_qual = decimal.Parse("9999");
                repre.rsr_tge_ttip_pers = 233;
                repre.rsr_tge_vtip_pers = cpfRepresentanteLegal.Length < 12 ? 1 : 2;
                if (!String.IsNullOrEmpty(RRP_CLASS_CRC_CTDRPF))
                {
                    repre.RSR_CLASS_CRC_CTDR = decimal.Parse(RRP_CLASS_CRC_CTDRPF);
                }
                repre.RSR_TIPO_CRC_CTDR = RRP_TIPO_CRC_CTDRPF;
                repre.Update(cp);

            }
        }
    }

    private DateTime ConvertStringDateTime(string _data)
    {
        if (_data.Trim() != "")
        {
            int dia = int.Parse(_data.Substring(6, 2));
            int mes = int.Parse(_data.Substring(4, 2));
            int ano = int.Parse(_data.Substring(0, 4));

            return new DateTime(ano, mes, dia);
        }
        return new DateTime(1, 1, 1);
    }

    public void AtualizaQualificacaoQSAdaRFB(OracleTransaction cp, string Xmlws11, string pProtocolo, string pCodServico)
    {
        WsRFBReginV2.WsServicesReginRFB.retornoWS11Redesim ws11 = new WsRFBReginV2.WsServicesReginRFB.retornoWS11Redesim();
        ws11 = (WsRFBReginV2.WsServicesReginRFB.retornoWS11Redesim)GlobalV1.CreateObject(Xmlws11, ws11);

        WsRFBReginV2.WsServicesReginRFB.dadosCNPJ dados = new WsRFBReginV2.WsServicesReginRFB.dadosCNPJ();

        dados = ws11.dadosCNPJ[0];

        //Estas narurezas nao vou processar porque pode ser que tenham o mesmo cara qualificação 65 por exemplo com administrador
        if (dados.naturezaJuridica == "2135")
        {
            return;
        }
        string QualificacaoNaoAtualiza = "";
        if (dados.naturezaJuridica == "2143" || dados.naturezaJuridica == "2305" || dados.naturezaJuridica == "2313" || dados.naturezaJuridica == "2321")
        {
            QualificacaoNaoAtualiza = "And rrp_tge_vcod_qual not in (5, 8)"; //8 Conselheiro Administrativo
        }

        if (dados.dadosSocio != null)
        {
            foreach (WsRFBReginV2.WsServicesReginRFB.dadosSocio socio in dados.dadosSocio)
            {

                using (OracleCommand cmdToExecuteSql = new OracleCommand())
                {
                    cmdToExecuteSql.Transaction = cp;
                    cmdToExecuteSql.Connection = cp.Connection;


                    string pQualificacao = socio.qualificacao;
                    string pCpfCnpjSocio = socio.cpfCnpj;

                    StringBuilder sqlD = new StringBuilder();
                    sqlD.AppendLine(" update ruc_relat_prof ");
                    sqlD.AppendLine(" set	 rrp_tge_vcod_qual = :rrp_tge_vcod_qual ");
                    sqlD.AppendLine(" where	 rrp_rge_pra_protocolo = :rrp_rge_pra_protocolo ");
                    sqlD.AppendLine(" and	 rrp_tge_vtip_relac = 2 ");
                    sqlD.AppendLine(" and	 trim(rrp_cgc_cpf_secd) = :rrp_cgc_cpf_secd ");
                    sqlD.AppendLine(QualificacaoNaoAtualiza);
                    sqlD.AppendLine(" and    rownum = 1 ");

                    cmdToExecuteSql.Parameters.Clear();
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("rrp_rge_pra_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("rrp_tge_vcod_qual", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pQualificacao));
                    cmdToExecuteSql.Parameters.Add(new OracleParameter("rrp_cgc_cpf_secd", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCpfCnpjSocio));

                    cmdToExecuteSql.CommandText = sqlD.ToString();
                    cmdToExecuteSql.CommandType = CommandType.Text;
                    cmdToExecuteSql.ExecuteNonQuery();
                    
                }
            
            }
        }
    }

    public void AtualizaRepresentantedoQSARFB(OracleTransaction cp, string Xmlws11, string pProtocolo, DataTable DtTipoLogra, string pCodServico)
    {
        WsRFBReginV2.WsServicesReginRFB.retornoWS11Redesim ws11 = new WsRFBReginV2.WsServicesReginRFB.retornoWS11Redesim();
        ws11 = (WsRFBReginV2.WsServicesReginRFB.retornoWS11Redesim)GlobalV1.CreateObject(Xmlws11, ws11);

        WsRFBReginV2.WsServicesReginRFB.dadosCNPJ dados = new WsRFBReginV2.WsServicesReginRFB.dadosCNPJ();

        dados = ws11.dadosCNPJ[0];

        decimal ValorCapitalEmpresa = 0;

        #region Apaga os reprsententes
        using (OracleCommand cmdToExecuteSql = new OracleCommand())
        {
            cmdToExecuteSql.Transaction = cp;
            cmdToExecuteSql.Connection = cp.Connection;

            StringBuilder sqlD = new StringBuilder();
            sqlD.AppendLine(" delete ruc_representantes ");
            sqlD.AppendLine(" where	 rsr_pra_protocolo = :rsr_pra_protocolo ");
            sqlD.AppendLine(" and	 rsr_tipo = 1 ");
        
            cmdToExecuteSql.Parameters.Clear();
            cmdToExecuteSql.Parameters.Add(new OracleParameter("rsr_pra_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
 
            cmdToExecuteSql.CommandText = sqlD.ToString();
            cmdToExecuteSql.CommandType = CommandType.Text;
            cmdToExecuteSql.ExecuteNonQuery();

        }
        #endregion

        #region Quando a empresa e normal ou seja tem Qsa no node Socio
        if (dados.dadosSocio != null)
        {
            foreach (WsRFBReginV2.WsServicesReginRFB.dadosSocio socio in dados.dadosSocio)
            {
                WsRFBReginV2.WsServicesReginRFB.dadosCPF dados09 = new WsRFBReginV2.WsServicesReginRFB.dadosCPF();
                int pCount = 0;
                if (socio.cpfCnpj.Trim() != "99999999999999")
                {
                    #region Carrega ruc_pof, ruc_relat_prof
                    if (2 == 1)
                    {
                       
                        dados09 = new WsRFBReginV2.WsServicesReginRFB.dadosCPF();

                        if (socio.cpfCnpj.Length < 12)
                        {
                            WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB ws09 = new WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB();
                            WsRFBReginV2.WsServicesReginRFB.Retorno resp09 = new WsRFBReginV2.WsServicesReginRFB.Retorno();
                            ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                            resp09 = ws09.ServiceWs09(socio.cpfCnpj);

                            if (resp09.status == "OK")
                            {
                                dados09 = resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                            }
                            else
                            {
                                throw new Exception("Erro ao tentar buscar o Tipo o cpf " + socio.cpfCnpj + " no ws09 na dll psc.ruc.tabelas.dall");
                            }
                        }

                        Ruc_Relat_Prof_co_oracle rp = new Ruc_Relat_Prof_co_oracle();

                        //rp.MainConnectionProvider = cp;
                        rp.rrp_rge_pra_protocolo = pProtocolo;
                        rp.rrp_cgc_cpf_secd = socio.cpfCnpj;
                        rp.rrp_tge_ttip_relac = 24;
                        rp.rrp_tge_vtip_relac = 2;
                        rp.rrp_fec_inic_part = ConvertStringDateTime(socio.dataInclusao);
                        rp.rrp_tge_tcod_qual = 23;
                        rp.rrp_tge_vcod_qual = decimal.Parse(socio.qualificacao);
                        rp.rrp_desc_doc = "";
                        rp.rrp_tus_cod_usr = "JUNTA";
                        rp.rrp_cnpj_vacio = 0;

                        decimal CapitalSocio = 0;
                        decimal PercentualSocio = 0;

                        if (validaNulo(socio.valorPartCapitalSocialString) != "")
                            CapitalSocio = decimal.Parse(socio.valorPartCapitalSocialString) / 100;

                        if (ValorCapitalEmpresa > 0 && CapitalSocio > 0)
                        {
                            PercentualSocio = (CapitalSocio * 100) / ValorCapitalEmpresa;
                            if (PercentualSocio > 100)
                            {
                                PercentualSocio = 0;
                            }
                        }

                        rp.rrp_porc_part = Math.Round(PercentualSocio, 5);
                        rp.RRP_VAL_CAP_SOC = CapitalSocio;
                        rp.Update(cp);

                        Ruc_Prof_co_oracle rf = new Ruc_Prof_co_oracle();
                        //rp.MainConnectionProvider = cp;
                        rf.rpr_rge_pra_protocolo = pProtocolo;
                        rf.rpr_tge_tpais = 22;
                        rf.rpr_tge_vpais = 105;// Decimal.Parse(s.EndPais);
                        rf.rpr_cgc_cpf_secd = socio.cpfCnpj;
                        rf.rpr_tge_ttip_pers = 233;
                        rf.rpr_tge_vtip_pers = socio.cpfCnpj.Length < 12 ? 1 : 2;
                        rf.rpr_nomb = socio.nome;

                        if (dados09 != null && dados09.numCPF != null && dados09.numCPF != "")
                        {
                            rf.rpr_nome_mae = dados09.nomeMae;

                            rf.rpr_fec_const_nasc = ConvertStringDateTime(dados09.dataNascimento);
                            rf.rpr_sexo = dados09.sexo == "1" ? 1 : 2;
                            rf.rpr_nome_mae = validaNulo(dados09.nomeMae);

                        }
                        rf.Update(cp);
                        
                    }
                    #endregion

                    #region Verifica se existe O QSA
                    using (OracleCommand cmdToExecuteSql = new OracleCommand())
                    {
                        cmdToExecuteSql.Transaction = cp;
                        cmdToExecuteSql.Connection = cp.Connection;

                        StringBuilder sqlD = new StringBuilder();
                        sqlD.AppendLine(" Select count(*) from ruc_relat_prof ");
                        sqlD.AppendLine(" where	 RRP_RGE_PRA_PROTOCOLO = :RRP_RGE_PRA_PROTOCOLO ");
                        sqlD.AppendLine(" and	 RRP_TGE_VTIP_RELAC = 2 ");
                        sqlD.AppendLine(" and	 trim(RRP_CGC_CPF_SECD) = :RRP_CGC_CPF_SECD ");

                        cmdToExecuteSql.Parameters.Clear();
                        cmdToExecuteSql.Parameters.Add(new OracleParameter("RRP_RGE_PRA_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                        cmdToExecuteSql.Parameters.Add(new OracleParameter("RRP_CGC_CPF_SECD", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, socio.cpfCnpj.Trim()));

                        cmdToExecuteSql.CommandText = sqlD.ToString();
                        cmdToExecuteSql.CommandType = CommandType.Text;

                        pCount = int.Parse(cmdToExecuteSql.ExecuteScalar().ToString());

                       
                    }
                    #endregion

                    #region Carrega Representante QSA
                    if (pCount > 0 && validaNulo(socio.cpfRepresentanteLegal) != "" && validaNulo(socio.cpfRepresentanteLegal) != "00000000000"
                        && socio.qualificacaoRepresentanteLegal != null && socio.qualificacaoRepresentanteLegal != "")
                    {
                        RUC_REPRESENTANTES_CO_ORACLE repre = new RUC_REPRESENTANTES_CO_ORACLE();
                        repre.rsr_pra_protocolo = pProtocolo;
                        repre.rsr_cgc_cpf_princ = socio.cpfCnpj;
                        repre.rsr_cgc_cpf_secd = socio.cpfRepresentanteLegal;
                        repre.rsr_tipo = 1;
                        repre.rsr_nomb = socio.nomeRepresentanteLegal;
                        repre.rsr_tge_tcod_qual = 23;
                        repre.rsr_tge_vcod_qual = decimal.Parse(socio.qualificacaoRepresentanteLegal);
                        repre.rsr_tge_ttip_pers = 233;
                        repre.rsr_tge_vtip_pers = socio.cpfRepresentanteLegal.Length < 12 ? 1 : 2; ;

                        WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB ws09 = new WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB();
                        WsRFBReginV2.WsServicesReginRFB.Retorno ws0911Socio = new WsRFBReginV2.WsServicesReginRFB.Retorno();
                        ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                        ws0911Socio = ws09.ServiceWs09(socio.cpfRepresentanteLegal);

                        if (ws0911Socio.status == "OK")
                        {
                            dados09 = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                        }
                        else
                        {
                            throw new Exception("Erro ao tentar buscar Reprsentante do cpf " + socio.cpfCnpj + " Representante " + socio.cpfRepresentanteLegal + " no ws09 na dll psc.ruc.tabelas.dall");
                        }

                        if (ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0] != null &&
                                                ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco != null)
                        {
                            psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                            cc.Bairro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.bairro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.bairro;
                            cc.Cep = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.cep == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.cep;
                            cc.Codigo_municipio = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codMunicipio == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codMunicipio;
                            cc.Complemento = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.complementoLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.complementoLogradouro;
                            cc.Logradouro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.logradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.logradouro;
                            cc.Numero = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.numLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.numLogradouro;
                            cc.Pais = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codPais == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codPais;
                            cc.TipLogradoro = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codTipoLogradouro == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codTipoLogradouro;
                            cc.Uf = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.uf == null ? "" : ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.uf;

                            cc.TrataEndereco(ref cc, DtTipoLogra);


                            repre.rsr_direccion = cc.Logradouro;
                            repre.rsr_nume = cc.Numero;
                            repre.rsr_tge_vpais = cc.Pais;
                            repre.rsr_ttl_tip_logradoro = cc.TipLogradoro;
                            repre.rsr_urbanizacion = cc.Bairro;
                            repre.rsr_tes_cod_estado = cc.Uf;
                            repre.rsr_zona_postal = cc.Cep;
                            repre.rsr_ident_comp = cc.Complemento;
                            repre.rsr_tmu_cod_mun = cc.Codigo_municipio;
                            repre.rsr_tge_vpais = cc.Pais;
                        }
                        repre.Update(cp);
                    }
                    #endregion
                }
            }
        }
        #endregion
    }

    public void AtualizaMunicipiosdasFiliais(OracleTransaction cp, string Xmlws11, string pProtocolo, string pCodServico, string pUfOrgaoDeRegistro)
    {
        WsRFBReginV2.WsServicesReginRFB.retornoWS11Redesim ws11 = new WsRFBReginV2.WsServicesReginRFB.retornoWS11Redesim();
        ws11 = (WsRFBReginV2.WsServicesReginRFB.retornoWS11Redesim)GlobalV1.CreateObject(Xmlws11, ws11);

        WsRFBReginV2.WsServicesReginRFB.dadosCNPJ dados = new WsRFBReginV2.WsServicesReginRFB.dadosCNPJ();

        dados = ws11.dadosCNPJ[0];
        
        using (OracleCommand cmdToExecute = new OracleCommand())
        {
            cmdToExecute.Transaction = cp;
            cmdToExecute.Connection = cp.Connection;

            StringBuilder sqlD = new StringBuilder();
            cmdToExecute.Parameters.Clear();
            sqlD.AppendLine(" delete psc_protocolo_munic ");
            sqlD.AppendLine(" where ppm_protocolo = :v_rqn_protocolo ");

            cmdToExecute.Parameters.Add(new OracleParameter("v_rqn_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

            cmdToExecute.CommandText = sqlD.ToString();
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.ExecuteNonQuery();
        }

        if (dados.cnpjFilial != null)
        {
            int pCount = 0;
            foreach (string pUfFilial in dados.ufFilial)
            {
                if (pUfFilial == pUfOrgaoDeRegistro)
                {
                    string cnpjFilial = dados.cnpjFilial[pCount];

                    WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB regin = new WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB();
                    WsRFBReginV2.WsServicesReginRFB.Retorno ws0911Socio = new WsRFBReginV2.WsServicesReginRFB.Retorno();
                    WsRFBReginV2.WsServicesReginRFB.endereco1 enderecoPJ = new WsRFBReginV2.WsServicesReginRFB.endereco1();
                    StringBuilder sqlD = new StringBuilder();
                    regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                    ws0911Socio = regin.ServiceWs11(cnpjFilial);

                    if (ws0911Socio.status != "OK")
                    {
                        throw new Exception("Erro buscando o Municipios das Filiais " + cnpjFilial);
                    }

                    string municipio = CalDvMunicipio(ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codMunicipio);
                    string uf = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.uf;

                    using (OracleCommand cmdToExecute = new OracleCommand())
                    {

                        cmdToExecute.Transaction = cp;
                        cmdToExecute.Connection = cp.Connection;

                        sqlD = new StringBuilder();

                        sqlD.AppendLine(" Select count(*) from psc_protocolo_munic ");
                        sqlD.AppendLine(" where	 ppm_protocolo = :v_ppm_protocolo ");
                        sqlD.AppendLine(" and	 ppm_tmu_tuf_uf = :v_ppm_tmu_tuf_uf ");
                        sqlD.AppendLine(" and	 ppm_tmu_cod_mun = :v_ppm_tmu_cod_mun ");

                        cmdToExecute.Parameters.Clear();
                        cmdToExecute.Parameters.Add(new OracleParameter("v_ppm_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                        cmdToExecute.Parameters.Add(new OracleParameter("v_ppm_tmu_tuf_uf", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, uf));
                        cmdToExecute.Parameters.Add(new OracleParameter("v_ppm_tmu_cod_mun", OracleType.Number, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, municipio));

                        cmdToExecute.CommandText = sqlD.ToString();
                        cmdToExecute.CommandType = CommandType.Text;

                        int pCountGravou = int.Parse(cmdToExecute.ExecuteScalar().ToString());

                        if (pCountGravou == 0)
                        {

                            sqlD = new StringBuilder();
                            sqlD.AppendLine(" insert into psc_protocolo_munic(ppm_protocolo, ppm_tmu_tuf_uf, ppm_tmu_cod_mun) ");
                            sqlD.AppendLine(" Values ");
                            sqlD.AppendLine(" (:v_ppm_protocolo, :v_ppm_tmu_tuf_uf, :v_ppm_tmu_cod_mun)");

                            cmdToExecute.CommandText = sqlD.ToString();
                            cmdToExecute.CommandType = CommandType.Text;
                            cmdToExecute.ExecuteNonQuery();
                        }
                    }
                }
                pCount++;
            }
        }
  
    }

    public void gravarPrepostoWs(OracleTransaction cp, string pXml, string pProtocolo, DataTable DtTipoLogra, string pCNPJEmpresa, string pCodServico)
    {
        int pCount = 0;
        StringBuilder sqlD = new StringBuilder();
        sqlD.AppendLine(" Select    Count(*) from RUC_REPRESENTANTES ");
        sqlD.AppendLine(" where	    RSR_PRA_PROTOCOLO = '" + pProtocolo + "'");
        sqlD.AppendLine(" and       RSR_TIPO = 4  "); //Preposto
        using (OracleCommand cmdToExecuteTp = new OracleCommand())
        {
            cmdToExecuteTp.Transaction = cp;
            cmdToExecuteTp.Connection = cp.Connection;

            cmdToExecuteTp.Parameters.Clear();
            cmdToExecuteTp.CommandText = sqlD.ToString();
            cmdToExecuteTp.CommandType = CommandType.Text;
            pCount = int.Parse(cmdToExecuteTp.ExecuteScalar().ToString());

            if (pCount > 0)
            {
                return;
            }
        }

        WsRFBReginV2.WsServicesReginRFB.retornoWS11Redesim ws11 = new WsRFBReginV2.WsServicesReginRFB.retornoWS11Redesim();
        WsRFBReginV2.WsServicesReginRFB.serviceResponse dados = new WsRFBReginV2.WsServicesReginRFB.serviceResponse();
        string pCpfPreposto = "";
        if (pCodServico == "S11")
        {
            ws11 = (WsRFBReginV2.WsServicesReginRFB.retornoWS11Redesim)GlobalV1.CreateObject(pXml, ws11);
            WsRFBReginV2.WsServicesReginRFB.dadosCNPJ dados11 = new WsRFBReginV2.WsServicesReginRFB.dadosCNPJ();

            dados11 = ws11.dadosCNPJ[0];
            if (dados11.cpfPreposto != null && dados11.nomePreposto != null && dados11.cpfPreposto.ToString() != "")
            {
                pCpfPreposto = validaNulo(dados11.cpfPreposto); //Estao colocando o cpf no lugar do nome
            }
        }
        else
        {
            dados = (WsRFBReginV2.WsServicesReginRFB.serviceResponse)GlobalV1.CreateObject(pXml, dados);
            if (dados.dadosRedesim.fcpj != null && dados.dadosRedesim.fcpj.nomePreposto != null && dados.dadosRedesim.fcpj.cpfPreposto != null)
            {
                pCpfPreposto = validaNulo(dados.dadosRedesim.fcpj.cpfPreposto); //Estao colocando o cpf no lugar do nome
            }
        }


        if (pCpfPreposto != "")
        {

            using (OracleCommand cmdToExecuteSql = new OracleCommand())
            {

                cmdToExecuteSql.Transaction = cp;
                cmdToExecuteSql.Connection = cp.Connection;


                string NomePF = "";
                WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB ws09 = new WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB();
                WsRFBReginV2.WsServicesReginRFB.Retorno resp09 = new WsRFBReginV2.WsServicesReginRFB.Retorno();
                ws09.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                resp09 = ws09.ServiceWs09(pCpfPreposto);
                if (resp09.status == "OK")
                {
                    //dados09 = resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0];
                    NomePF = resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0].nome;
                }
                else
                {
                    throw new Exception("Erro ao tentar buscar nome do cpf  do preposto " + pCpfPreposto + " no ws09 na dll psc.ruc.tabelas.dall");
                }

                RUC_REPRESENTANTES_CO_ORACLE repre = new RUC_REPRESENTANTES_CO_ORACLE();

                repre.rsr_pra_protocolo = pProtocolo;
                repre.rsr_cgc_cpf_princ = pCNPJEmpresa;
                repre.rsr_cgc_cpf_secd = pCpfPreposto;
                repre.rsr_crc_ctdr = "";
                repre.rsr_uf_crc_ctdr = "";
                repre.rsr_tipo = 4; //Preposto da empresa
                repre.rsr_nomb = NomePF;
                repre.rsr_tge_tcod_qual = 23;
                repre.rsr_tge_vcod_qual = decimal.Parse("9999");
                repre.rsr_tge_ttip_pers = 233;
                repre.rsr_tge_vtip_pers = pCpfPreposto.Length < 12 ? 1 : 2;
                repre.RSR_TIPO_CRC_CTDR = "";

                psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                if (dados.dadosRedesim == null)
                {
                    cc.Bairro = validaNulo(resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.bairro);
                    cc.Cep = validaNulo(resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.cep);
                    cc.Codigo_municipio = validaNulo(resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codMunicipio);
                    cc.Complemento = validaNulo(resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.complementoLogradouro);
                    cc.Logradouro = validaNulo(resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.logradouro);
                    cc.Numero = validaNulo(resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.numLogradouro);
                    cc.Pais = "";
                    cc.TipLogradoro = validaNulo(resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.codTipoLogradouro);
                    cc.Uf = validaNulo(resp09.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco.uf);
                }

                if (dados.dadosRedesim != null)
                {

                    cc.Bairro = validaNulo(dados.dadosRedesim.fcpj.endPreposto.bairro);
                    cc.Cep = validaNulo(dados.dadosRedesim.fcpj.endPreposto.cep);
                    cc.Codigo_municipio = validaNulo(dados.dadosRedesim.fcpj.endPreposto.codMunicipio);
                    cc.Complemento = validaNulo(dados.dadosRedesim.fcpj.endPreposto.complementoLogradouro);
                    cc.Logradouro = validaNulo(dados.dadosRedesim.fcpj.endPreposto.logradouro);
                    cc.Numero = validaNulo(dados.dadosRedesim.fcpj.endPreposto.numLogradouro);
                    cc.Pais = "";
                    cc.TipLogradoro = validaNulo(dados.dadosRedesim.fcpj.endPreposto.codTipoLogradouro);
                    cc.Uf = validaNulo(dados.dadosRedesim.fcpj.endPreposto.uf);
                }

                cc.TrataEndereco(ref cc, DtTipoLogra);

                repre.rsr_ident_comp = cc.Complemento;
                repre.rsr_ttl_tip_logradoro = cc.TipLogradoro;
                repre.rsr_direccion = cc.Logradouro;
                repre.rsr_nume = cc.Numero;
                repre.rsr_ident_comp = cc.Complemento;
                repre.rsr_urbanizacion = cc.Bairro;
                repre.rsr_distrito = "";
                repre.rsr_tmu_cod_mun = cc.Codigo_municipio;
                repre.rsr_tes_cod_estado = cc.Uf;
                repre.rsr_zona_postal = cc.Cep;
                repre.rsr_tge_tpais = 22;
                repre.rsr_tge_vpais = "105";

                repre.Update(cp);


            }
        }
    }


    public void gravarNodeQSAFichaRFB(OracleTransaction cp, string pXml35, string pProtocolo, string pCNPJEmpresa, string pCodServico)
    {

        WsRFBReginV2.WsServicesReginRFB.serviceResponse dados = new WsRFBReginV2.WsServicesReginRFB.serviceResponse();
        dados = (WsRFBReginV2.WsServicesReginRFB.serviceResponse)GlobalV1.CreateObject(pXml35, dados);
       
        try
        {
            if (dados.dadosRedesim.socios != null)
            {
                using (OracleCommand cmdToExecute = new OracleCommand())
                {
                    cmdToExecute.Transaction = cp;
                    cmdToExecute.Connection = cp.Connection;

                    StringBuilder sqlD = new StringBuilder();
                    cmdToExecute.Parameters.Clear();
                    sqlD.AppendLine(" delete RUC_QSA_NODE_RFB ");
                    sqlD.AppendLine(" where RQN_PROTOCOLO = :v_rqn_protocolo ");
                    // sqlD.AppendLine(" and   rev_tipo = 2 ");

                    cmdToExecute.Parameters.Add(new OracleParameter("v_rqn_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;
                    cmdToExecute.ExecuteNonQuery();

                    foreach (WsRFBReginV2.WsServicesReginRFB.socio qsa in dados.dadosRedesim.socios)
                    {
                        string v_rqn_tipo = "";
                        string v_rqn_cnpj_qsa = "";
                        string v_rqn_qualificacao = "";
                        string v_RQN_IND_CPFCNPJ = "";

                        v_rqn_tipo = qsa.codEvento;
                        v_rqn_cnpj_qsa = qsa.cnpjCpfSocio;
                        if (!String.IsNullOrEmpty(qsa.codQualificacaoSocio))
                        {
                            v_rqn_qualificacao = qsa.codQualificacaoSocio;
                        }
                        if (!String.IsNullOrEmpty(qsa.indCnpjCpfSocio))
                        {
                            v_RQN_IND_CPFCNPJ = qsa.indCnpjCpfSocio;
                        }

                        //Somente vou enviar nao sao de incluao nem de alteração
                        if (v_rqn_tipo != "" && decimal.Parse(v_rqn_tipo) != 1 && decimal.Parse(v_rqn_tipo) != 3)
                        {

                            sqlD = new StringBuilder();
                            cmdToExecute.Parameters.Clear();
                            sqlD.AppendLine(" insert into ruc_qsa_node_rfb(rqn_protocolo, rqn_tipo, rqn_cnpj_qsa, rqn_qualificacao, RQN_IND_CPFCNPJ) ");
                            sqlD.AppendLine(" Values ");
                            sqlD.AppendLine(" (:v_rqn_protocolo, :v_rqn_tipo, :v_rqn_cnpj_qsa, :v_rqn_qualificacao, :v_RQN_IND_CPFCNPJ)");

                            cmdToExecute.Parameters.Add(new OracleParameter("v_rqn_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_rqn_tipo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_rqn_tipo));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_rqn_cnpj_qsa", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_rqn_cnpj_qsa));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_rqn_qualificacao", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_rqn_qualificacao));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_RQN_IND_CPFCNPJ", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_RQN_IND_CPFCNPJ));


                            cmdToExecute.CommandText = sqlD.ToString();
                            cmdToExecute.CommandType = CommandType.Text;
                            cmdToExecute.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //throw new Exception("Erro ao tenter pegar os dados do QSA de saida e entreda do DBE gravarNodeQSAFichaRFB " + ex.Message);
            // fiz isto porquue essa tabela PRO_EMP_VINCULADAS, possiblemente nao tenha em todos os bancos de dados
        }
    }

    #region Update_RUC_INOVA_SIMPLES

    public void Update_RUC_INOVA_SIMPLES(OracleTransaction cp, string v_protocolo,
                       string v_localatuacao,
                       string v_cnpjentidadevinculada,
                       string v_formacaptacao,
                       string v_outrasformascaptacao)
    {
        string Sql = "";

        using (OracleCommand cmdToExecuteSql = new OracleCommand())
        {
            cmdToExecuteSql.Transaction = cp;
            cmdToExecuteSql.Connection = cp.Connection;

            cmdToExecuteSql.Parameters.Clear();
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_protocolo));


            Sql = @"Delete   RUC_INOVA_SIMPLES p
                        where    protocolo = :v_protocolo";

            cmdToExecuteSql.CommandText = Sql.ToString();
            cmdToExecuteSql.CommandType = CommandType.Text;
            cmdToExecuteSql.ExecuteNonQuery();


            Sql = @"insert into ruc_inova_simples
                    (protocolo, localatuacao, cnpjentidadevinculada, formacaptacao, outrasformascaptacao)
                    values
                    (:v_protocolo, :v_localatuacao, :v_cnpjentidadevinculada, :v_formacaptacao, :v_outrasformascaptacao)";

            cmdToExecuteSql.Parameters.Clear();
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_protocolo));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_localatuacao", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_localatuacao));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_cnpjentidadevinculada", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_cnpjentidadevinculada));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_formacaptacao", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_formacaptacao));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_outrasformascaptacao", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_outrasformascaptacao));

            cmdToExecuteSql.CommandText = Sql.ToString();
            cmdToExecuteSql.CommandType = CommandType.Text;
            cmdToExecuteSql.ExecuteNonQuery();

  
        }
    }
    #endregion

    #region Update_RUC_DADOS_SEGURANCA

    public void Update_RUC_DADOS_SEGURANCA(OracleTransaction cp, string v_protocolo,
                       string v_rip_ip,
                       string v_rip_tipo)
    {
        string Sql = "";

        if (v_rip_ip == "") return;

        using (OracleCommand cmdToExecuteSql = new OracleCommand())
        {
            cmdToExecuteSql.Transaction = cp;
            cmdToExecuteSql.Connection = cp.Connection;

            cmdToExecuteSql.Parameters.Clear();
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rip_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_protocolo));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rip_ip", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_rip_ip));
            cmdToExecuteSql.Parameters.Add(new OracleParameter("v_rip_tipo", OracleType.Number, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, v_rip_tipo));


            Sql = @"update  ruc_dados_seguranca
                    set     rip_ip = :v_rip_ip
                    where   rip_protocolo = :v_rip_protocolo
                    and     rip_tipo = :v_rip_tipo";

            cmdToExecuteSql.CommandText = Sql.ToString();
            cmdToExecuteSql.CommandType = CommandType.Text;
            int pCount = cmdToExecuteSql.ExecuteNonQuery();

            if (pCount == 0)
            {
                Sql = @"insert into ruc_dados_seguranca
                    (rip_protocolo, rip_tipo, rip_ip)
                    values
                    (:v_rip_protocolo, :v_rip_tipo, :v_rip_ip)";

                cmdToExecuteSql.CommandText = Sql.ToString();
                cmdToExecuteSql.CommandType = CommandType.Text;
                cmdToExecuteSql.ExecuteNonQuery();
            }


        }
    }
    #endregion

    #region Atualiza Ruc_estab
    public void UpdateRucEstab(OracleTransaction cp, string pProtocolo, string DadoXml, DataTable DtTipoLogra, string pCodServico)
    {

        return;
        // fiz return, apra nao fazer nada ate nao er a reuniao com sefaz PE que foi que solicitou
        psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
        WsRFBReginV2.WsServices15RFB.endereco dados = new WsRFBReginV2.WsServices15RFB.endereco();

        if (pCodServico == "S15")
        {
            if (DadoXml != "")
                dados = (WsRFBReginV2.WsServices15RFB.endereco)GlobalV1.CreateObject(DadoXml, dados);

        }
        cc.Bairro = validaNulo(dados.bairro);
        cc.Cep = validaNulo(dados.cep);
        cc.Codigo_municipio = validaNulo(dados.codMunicipio);
        cc.Complemento = validaNulo(dados.complementoLogradouro);
        cc.Logradouro = validaNulo(dados.logradouro);
        cc.Numero = validaNulo(dados.numLogradouro);
        cc.Pais = "";
        cc.TipLogradoro = validaNulo(dados.codTipoLogradouro);
        cc.Uf = validaNulo(dados.uf);

        cc.TrataEndereco(ref cc, DtTipoLogra);

        if (cc.Bairro != null && cc.Cep != null && cc.Bairro != "" && cc.Cep != "")
        {
            using (OracleCommand cmdToExecute = new OracleCommand())
            {
                cmdToExecute.Transaction = cp;
                cmdToExecute.Connection = cp.Connection;
                StringBuilder sqlD = new StringBuilder();
                sqlD.AppendLine(" update  ruc_estab ");
                sqlD.AppendLine(" set	  RES_NUME = :v_RPR_NUME,");
                sqlD.AppendLine("         RES_TTL_TIP_LOGRADORO = :v_RPR_TTL_TIP_LOGRADORO, ");
                sqlD.AppendLine("         RES_DIRECCION = :v_RPR_DIRECCION, ");
                sqlD.AppendLine("         RES_URBANIZACION = :v_RPR_URBANIZACION, ");
                sqlD.AppendLine("         RES_IDENT_COMP = :v_RPR_IDENT_COMP, ");
                sqlD.AppendLine("         RES_ZONA_POSTAL = :v_RPR_ZONA_POSTAL, ");
                sqlD.AppendLine("         RES_TES_COD_ESTADO = :v_RPR_TES_COD_ESTADO, ");
                sqlD.AppendLine("         RES_TMU_COD_MUN = :v_RPR_TMU_COD_MUN, ");
                sqlD.AppendLine("         RES_TUS_COD_USR = 'RFBE'");

                sqlD.AppendLine(" where	RES_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");




                cmdToExecute.Parameters.Clear();
                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_NUME", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cc.Numero));
                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_TTL_TIP_LOGRADORO", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cc.TipLogradoro));
                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_DIRECCION", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cc.Logradouro));
                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_URBANIZACION", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cc.Bairro));
                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_IDENT_COMP", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cc.Complemento));
                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_ZONA_POSTAL", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cc.Cep));
                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_TES_COD_ESTADO", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cc.Uf));
                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_TMU_COD_MUN", OracleType.Number, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, cc.Codigo_municipio));


                cmdToExecute.CommandText = sqlD.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                cmdToExecute.ExecuteNonQuery();
            }
        }
    }
    #endregion

    #region Grava e atualiza evento
    public void gravaEvento_psc_prot_evento_rfb(OracleTransaction cp, string pEvento, string pProtocolo)
    {
        StringBuilder sqlD = new StringBuilder();
        using (OracleCommand cmdToExecuteSql = new OracleCommand())
        {
            
            sqlD = new StringBuilder();
            sqlD.AppendLine(" Select count(*) ");
            sqlD.AppendLine(" from   psc_prot_evento_rfb ");
            sqlD.AppendLine(" where	 pev_pro_protocolo = '" + pProtocolo + "'");
            sqlD.AppendLine(" And    pev_cod_evento = " + pEvento);

            cmdToExecuteSql.Transaction = cp;
            cmdToExecuteSql.Connection = cp.Connection;

            cmdToExecuteSql.Parameters.Clear();
            cmdToExecuteSql.CommandText = sqlD.ToString();
            cmdToExecuteSql.CommandType = CommandType.Text;
            int Qtd = int.Parse(cmdToExecuteSql.ExecuteScalar().ToString());

            if (Qtd == 0)
            {

                sqlD = new StringBuilder();
                sqlD.AppendLine(" Insert into psc_prot_evento_rfb (pev_pro_protocolo, pev_cod_evento) ");
                sqlD.AppendLine(" Values (:v_TIE_PROTOCOLO, :v_pev_cod_evento) ");

                cmdToExecuteSql.Parameters.Clear();
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                cmdToExecuteSql.Parameters.Add(new OracleParameter("v_pev_cod_evento", OracleType.Number, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pEvento));

                cmdToExecuteSql.CommandText = sqlD.ToString();
                cmdToExecuteSql.CommandType = CommandType.Text;
                cmdToExecuteSql.ExecuteNonQuery();

            }
        }
    }
    #endregion

}