using System;
using System.Collections.Generic;
using System.Text;
using psc.Receita.ConnectionBase;
using psc.Receita.Entities;
using psc.Receita;
using System.Configuration;
using WsRFBReginV2.Models;

namespace psc.Receita
{
    public class ProcessaDBE
    {
        protected string _Xml = "";
        protected int idControl = int.MinValue;

        //public string Xml
        //{
        //    get { return _Xml; }
        //    set { _Xml = value; }
        //}

        public void ProcessaDadosDbe(WsRFBReginV2.WsServices35RFB.serviceResponse pReponse, string pRecibo, string pIdentificacao)
        {
            string Xml = "";
            bool eventoConstituicao = false;
            bool eventoTransformacao = false;
            bool eventoResponsavelPJ = false;
            bool trocadeQSATipoEmpresario = false;


            int hashcode = int.MinValue;

            using (T73300_DBE_CONTROL dbe = new T73300_DBE_CONTROL())
            {
                dbe.t73300_ide_solicitacao = pIdentificacao;
                dbe.t73300_rec_solicitacao = pRecibo;
                idControl = dbe.getIdControl();
                /*
                    extraido o XML da clase e gero o hashcode
                */

                //cOMENTADO PELO Raul dia 29/03/2018 para nao gravar mais o XML do 35, ja que a base estava creciendo muito e nao precisa gravar mais
                //Xml = GlobalV1.CreateXML(pReponse);
                hashcode = Xml.GetHashCode();
            }

            WsRFBReginV2.WsServices35RFB.redesim DadosDbe = pReponse.dadosRedesim;
            using (ConnectionProvider cp = new ConnectionProvider())
            {
                try
                {
                    cp.OpenConnection();
                    cp.BeginTransaction();

                    #region Apagar Registros
                    if (idControl != int.MinValue)
                    {
                        using (T73303_DBE_QSA qsa = new T73303_DBE_QSA())
                        {
                            qsa.MainConnectionProvider = cp;
                            qsa.t73300_id_control = idControl;
                            qsa.Delete();
                        }

                        using (T73301_DBE_EVENTO ev = new T73301_DBE_EVENTO())
                        {
                            ev.MainConnectionProvider = cp;
                            ev.t73300_id_control = idControl;
                            ev.Delete();
                        }

                        using (T73308_DBE_FORMA_ATUACAO fo = new T73308_DBE_FORMA_ATUACAO())
                        {
                            fo.MainConnectionProvider = cp;
                            fo.t73300_id_control = idControl;
                            fo.Delete();
                        }

                        using (T73304_DBE_CNAE_SECUNDARIA cs = new T73304_DBE_CNAE_SECUNDARIA())
                        {
                            cs.MainConnectionProvider = cp;
                            cs.t73300_id_control = idControl;
                            cs.Delete();
                        }

                        using (T73305_DBE_CONTADOR co = new T73305_DBE_CONTADOR())
                        {
                            co.MainConnectionProvider = cp;
                            co.t73300_id_control = idControl;
                            co.Delete();
                        }

                        using (T73302_DBE_FCPJ co = new T73302_DBE_FCPJ())
                        {
                            co.MainConnectionProvider = cp;
                            co.t73300_id_control = idControl;
                            co.Delete();
                        }

                        //using (T73300_DBE_CONTROL co = new T73300_DBE_CONTROL())
                        //{
                        //    co.MainConnectionProvider = cp;
                        //    co.t73300_id_control = idControl;
                        //    co.Delete();
                        //}
                        //cp.CommitTransaction();
                    }
                    #endregion


                    #region Verifica se e Constituição
                    if (DadosDbe.fcpj.codEvento != null)
                    {
                        using (T73301_DBE_EVENTO ev = new T73301_DBE_EVENTO())
                        {
                            foreach (string pCodEvento in DadosDbe.fcpj.codEvento)
                            {
                                if (pCodEvento == "")
                                {
                                    break;
                                }
                                /*
                                    Isso parqa nao gravar o evento 202, porque se vier socios com esses eventos nao e para colocar o 202
                                 * na tabela
                                 */
                                if (pCodEvento.Trim() == "101" || pCodEvento.Trim() == "102")
                                {
                                    eventoConstituicao = true;
                                }

                                //Verificar se o evento se tem evento de tranformação
                                if (pCodEvento.Trim() == "225")
                                {
                                    eventoTransformacao = true;
                                }

                                if (pCodEvento.Trim() == "202")
                                {
                                    eventoResponsavelPJ = true;
                                }



                            }
                        }
                    }

                    #endregion

                    using (T73300_DBE_CONTROL dbe = new T73300_DBE_CONTROL())
                    {
                        dbe.MainConnectionProvider = cp;
                        dbe.t73300_id_control = idControl;
                        dbe.t73300_ide_solicitacao = pIdentificacao;
                        dbe.t73300_rec_solicitacao = pRecibo;

                        if (DadosDbe.fcpj.endereco != null)
                        {
                            dbe.t7300_uf_origem = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.uf);// != null ? DadosDbe.fcpj.endereco.uf : DadosDbe.header.ufOrigem;

                            dbe.t73300_cod_munic_origem = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.codMunicipio);// != null ? DadosDbe.fcpj.endereco.codMunicipio : DadosDbe.header.codMunicOrigem;
                            if (dbe.t73300_cod_munic_origem != "")
                            {
                                dbe.t73300_cod_munic_origem += psc.Framework.General.CalculateVerificationDigit(dbe.t73300_cod_munic_origem, 11).ToString();
                                dbe.t73300_cod_munic_origem = int.Parse(dbe.t73300_cod_munic_origem).ToString();
                            }
                        }



                        dbe.t73300_cnpj_empresa = DadosDbe.cnpj;
                        dbe.t73300_cod_convenio = GlobalV1.valNuloBranco(DadosDbe.convenio);
                        dbe.t73300_cpf_cnpj_solicitante = "";
                        dbe.t73300_in_estab_matriz = GlobalV1.valNuloBranco(DadosDbe.fcpj.inMatriz);
                        dbe.t73300_nat_evento = "1";
                        dbe.t73300_num_arquivamento = GlobalV1.valNuloBranco(DadosDbe.fcpj.arquivamento);
                        dbe.t73300_tip_ppa = "";
                        dbe.t733300_Arquivo_RFB = Xml;
                        dbe.t733300_HashCode_arq_RFB = hashcode.ToString();
                        dbe.t733300_orgaoResponsavelDeferimento = GlobalV1.valNuloBranco(DadosDbe.orgaoResponsavelDeferimento);

                        dbe.Update();

                        idControl = dbe.getIdControl();

                    }

                    using (T73302_DBE_FCPJ pj = new T73302_DBE_FCPJ())
                    {
                        pj.MainConnectionProvider = cp;
                        pj.t73300_id_control = idControl;
                        pj.t73302_caixa_postal = GlobalV1.valNuloBranco(DadosDbe.fcpj.caixaPostal);
                        if (!GlobalV1.valNulo(DadosDbe.fcpj.capitalSocial))
                        {
                            pj.t73302_capital_social = decimal.Parse(DadosDbe.fcpj.capitalSocial) / 100;
                        }


                        if (DadosDbe.fcpj.endereco != null)
                        {
                            pj.t73302_bairro = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.bairro);
                            pj.t73302_cep = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.cep);
                            pj.t73302_cod_munic = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.codMunicipio);
                            if (pj.t73302_cod_munic != "")
                            {
                                pj.t73302_cod_munic += psc.Framework.General.CalculateVerificationDigit(pj.t73302_cod_munic, 11).ToString();
                                pj.t73302_cod_munic = int.Parse(pj.t73302_cod_munic).ToString();
                            }
                            pj.t73302_cod_pais = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.codPais);
                            pj.t73302_complemento_logradouro = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.complementoLogradouro);
                            pj.t73302_distrito = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.distrito);
                            pj.t73302_nom_logradouro = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.logradouro);
                            pj.t73302_num_logradouro = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.numLogradouro);
                            pj.t73302_referencia = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.referencia);
                            pj.t73302_tip_logradouro = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.codTipoLogradouro);
                            pj.t73302_cidade_exterior = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.cidadeExterior);
                            pj.t73302_uf = GlobalV1.valNuloBranco(DadosDbe.fcpj.endereco.uf);
                            //if (DadosDbe.atividadeEconomica != null)
                            //{
                            //    pj.t73302_cod_tipo_unidade = GlobalV1.valNuloBranco(DadosDbe.atividadeEconomica.codTipoUnidade);
                            //}
                        }

                        pj.t73302_cep_caixa_postal = GlobalV1.valNuloBranco(DadosDbe.fcpj.cepCaixaPostal);
                        pj.t73302_cnpj_estab_matriz = GlobalV1.valNuloBranco(DadosDbe.cnpj);
                        if (GlobalV1.valNuloBranco(DadosDbe.fcpj.nire) != "")
                        {
                            pj.t73302_nire = GlobalV1.valNuloBranco(decimal.Parse(DadosDbe.fcpj.nire));
                        }
                        pj.t73302_tip_org_registro = GlobalV1.valNuloBranco(DadosDbe.fcpj.codTipoOrgaoRegistro);
                        pj.t73302_porte_empresa = GlobalV1.valNuloBranco(DadosDbe.fcpj.codPorteEmpresa);
                        pj.t73302_nom_fantasia = GlobalV1.valNuloBranco(DadosDbe.fcpj.nomeFantasia);
                        pj.t73302_nat_juridica = GlobalV1.valNuloBranco(DadosDbe.fcpj.codNaturezaJuridica);
                        pj.t73302_nom_empresarial = GlobalV1.valNuloBranco(DadosDbe.fcpj.nomeEmpresarial);


                        if (DadosDbe.atividadeEconomica != null)
                        {
                            pj.t73302_objeto_social = GlobalV1.valNuloBranco(DadosDbe.atividadeEconomica.objetoSocial);
                            string cnae = "";
                            if (GlobalV1.valNuloBranco(DadosDbe.atividadeEconomica.codCnaeFiscal) != "")
                            {
                                cnae = GlobalV1.valNuloBranco(DadosDbe.atividadeEconomica.codCnaeFiscal.PadLeft(7, '0'));
                            }
                            pj.t73302_cnae_principal = cnae;
                        }

                        if (DadosDbe.fcpj.contato != null)
                        {
                            pj.t73302_correio_eletronico = GlobalV1.valNuloBranco(DadosDbe.fcpj.contato.correioEletronico);
                            pj.t73302_telefone_1 = GlobalV1.valNuloBranco(DadosDbe.fcpj.contato.telefone1);
                            pj.t73302_telefone_2 = GlobalV1.valNuloBranco(DadosDbe.fcpj.contato.telefone2);
                            pj.t73302_ddd_fax = GlobalV1.valNuloBranco(DadosDbe.fcpj.contato.dddFax);
                            pj.t73302_ddd_telefone_1 = GlobalV1.valNuloBranco(DadosDbe.fcpj.contato.dddTelefone1);
                            pj.t73302_ddd_telefone_2 = GlobalV1.valNuloBranco(DadosDbe.fcpj.contato.dddTelefone2);
                            pj.t73302_fax = GlobalV1.valNuloBranco(DadosDbe.fcpj.contato.fax);
                        }

                        pj.Update();

                        bool pSocioEOResponsavel = false;
                        if (ConfigurationManager.AppSettings["pNaturezaSocioEOResponsavelWs35"] != null && ConfigurationManager.AppSettings["pNaturezaSocioEOResponsavelWs35"].ToString() != "")
                        {
                            string[] pNaturezaSocioEOResponsavel = ConfigurationManager.AppSettings["pNaturezaSocioEOResponsavelWs35"].ToString().Split('-');

                            foreach (string Natureza in pNaturezaSocioEOResponsavel)
                            {
                                if (Natureza == DadosDbe.fcpj.codNaturezaJuridica)
                                {
                                    pSocioEOResponsavel = true;
                                    break;
                                }
                            }
                        }

                        //Falado com arnaldo dia 16/08/2021, caso seja constituição e nao  tenha QSA (node), vamos considerar para pegar o QSA que seja o responsavel
                        if (eventoConstituicao && DadosDbe.socios == null)
                        {
                            pSocioEOResponsavel = true;
                        }

                        if (DadosDbe.fcpj.codNaturezaJuridica == "2135"
                            || DadosDbe.fcpj.codNaturezaJuridica == "1279"
                            || DadosDbe.fcpj.codNaturezaJuridica == "3077"
                            || DadosDbe.fcpj.codNaturezaJuridica == "3131"
                            || DadosDbe.fcpj.codNaturezaJuridica == "3085"
                            || DadosDbe.fcpj.codNaturezaJuridica == "1252"
                            || DadosDbe.fcpj.codNaturezaJuridica == "1023"
                            || DadosDbe.fcpj.codNaturezaJuridica == "3271"
                            || DadosDbe.fcpj.codNaturezaJuridica == "3301"
                            || pSocioEOResponsavel == true)
                        {
                            //Se e alteração de representante, vou buscar o s11 se e o mesmo, caso nao seja 
                            //coloco que esta no s11  na tabela como sainda da empresa e vou mudar para o que esta no dbe vou mudar de 003 para 001 (inclusao)
                            //Isto foi uma conversa com arnaldo para o requerimento fazer o contato com a pessoa saindo e a outra entrando quando e 
                            // tipo de QSA igual ao empresario, ou seja que nao tem o node qsa da rfb
                            //Vamos fazer so para partido politico, conversado com arnaldo dia 23/06/2020
                            if (eventoResponsavelPJ && DadosDbe.fcpj.codNaturezaJuridica == "3271")
                            {
                                WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB regin = new WsRFBReginV2.WsServicesReginRFB.ServiceReginRFB();
                                WsRFBReginV2.WsServicesReginRFB.Retorno resulRegin = new WsRFBReginV2.WsServicesReginRFB.Retorno();
                                regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                                resulRegin = regin.ServiceWs11(DadosDbe.cnpj);
                                if (resulRegin.status == "OK")
                                {

                                    WsRFBReginV2.WsServicesReginRFB.dadosCNPJ dadosws11 = new WsRFBReginV2.WsServicesReginRFB.dadosCNPJ();
                                    dadosws11 = resulRegin.oCNPJResponse.dadosCNPJ[0];
                                    //caso e diferente ai que vou fazer as trocas
                                    if (dadosws11.cpfRepresentante != DadosDbe.fcpj.cpfResponsavel)
                                    {
                                        trocadeQSATipoEmpresario = true;
                                        //Grava o QSA que esta na empresa como QSA que esta saindo da empresa
                                        using (T73303_DBE_QSA qsa = new T73303_DBE_QSA())
                                        {
                                            qsa.MainConnectionProvider = cp;
                                            qsa.t73300_id_control = idControl;
                                            if (dadosws11.cpfRepresentante != "")
                                            {
                                                qsa.MainConnectionProvider = cp;



                                                qsa.t73303_perc_partic_qsa = 0;

                                                qsa.t73303_cargo_qsa = dadosws11.qualificacaoRepresentante;
                                                if (qsa.t73303_cargo_qsa == null || qsa.t73303_cargo_qsa == "")
                                                    qsa.t73303_cargo_qsa = "50";

                                                qsa.t73303_cod_evento = "005";
                                                qsa.t73303_ind_cpf_cnpj_qsa = "2"; //Sempre cpf
                                                int De = 14;
                                                if (qsa.t73303_ind_cpf_cnpj_qsa == "2")
                                                {
                                                    De = 11;
                                                }
                                                int Ate = dadosws11.cpfRepresentante.Length - De;
                                                qsa.t73303_cpf_cnpj_qsa = dadosws11.cpfRepresentante.Substring(Ate);

                                                qsa.t73303_dat_evento = DateTime.Now;
                                                qsa.t73303_dat_inicio_mandato = DateTime.Now;

                                                qsa.t73303_nom_qsa = dadosws11.nomeRepresentante;
                                                //qsa.t73303_num_lograd_qsa = dadosws11.cpfRepresentante.numLogradouro;

                                                qsa.t73303_qualificacao_qsa = dadosws11.qualificacaoRepresentante;
                                                if (qsa.t73303_qualificacao_qsa == null || qsa.t73303_qualificacao_qsa == "")
                                                    qsa.t73303_qualificacao_qsa = "50";

                                                qsa.t73303_qualificacao_qsa = qsa.t73303_qualificacao_qsa.PadLeft(2, '0');

                                                qsa.Update();
                                            }
                                        }
                                    }
                                }

                            }

                            #region Preenche Socio caso seja naturea Empresaio
                            using (T73303_DBE_QSA qsa = new T73303_DBE_QSA())
                            {
                                qsa.MainConnectionProvider = cp;
                                qsa.t73300_id_control = idControl;
                                if (DadosDbe.fcpj.cpfResponsavel != "")
                                {
                                    qsa.MainConnectionProvider = cp;

                                    WsRFBReginV2.WsServices35RFB.endereco enderecoResponsavel = new WsRFBReginV2.WsServices35RFB.endereco();
                                    WsRFBReginV2.WsServices35RFB.contato contatoResponsavel = new WsRFBReginV2.WsServices35RFB.contato();

                                    /*
                                        Comença com o dado da emporesa mesmo, caso venha o endereço do responsavel pego de la
                                     * comentado para nao levar o endereço da empresa para o socio, solicitado por xico 28/10/2014
                                     * alegando que os estados falaram que a pessoa quasi sempre chega com o memso endereço, por isso
                                     * sera forçado a digitar ele novamente se for o caso.
                                     */
                                    //enderecoResponsavel = DadosDbe.fcpj.endereco;
                                    //contatoResponsavel = DadosDbe.fcpj.contato;

                                    if (DadosDbe.fcpj.endResponsavel != null)
                                    {
                                        if (!GlobalV1.valNulo(DadosDbe.fcpj.endResponsavel.bairro) && !GlobalV1.valNulo(DadosDbe.fcpj.endResponsavel.cep))
                                        {
                                            enderecoResponsavel = DadosDbe.fcpj.endResponsavel;
                                            contatoResponsavel = DadosDbe.fcpj.contatoResponsavel;
                                        }
                                    }

                                    //qsa.t73303_bairro_qsa = enderecoResponsavel.bairro;
                                    qsa.t73303_bairro_qsa = GlobalV1.valNuloBranco(enderecoResponsavel.bairro);
                                    if (!GlobalV1.valNulo(DadosDbe.fcpj.capitalSocial))
                                    {
                                        qsa.t73303_capital_social_empresa = decimal.Parse(DadosDbe.fcpj.capitalSocial) / 100;
                                        qsa.t73303_capital_social_qsa = qsa.t73303_capital_social_empresa;
                                    }

                                    qsa.t73303_perc_partic_qsa = 100;

                                    qsa.t73303_cargo_qsa = GlobalV1.valNulo(DadosDbe.fcpj.codQualificResponsavel) ? "" : DadosDbe.fcpj.codQualificResponsavel;
                                    if (qsa.t73303_cargo_qsa == null || qsa.t73303_cargo_qsa == "")
                                        qsa.t73303_cargo_qsa = "50";

                                    qsa.t73303_cep_qsa = GlobalV1.valNulo(enderecoResponsavel.cep) ? "" : enderecoResponsavel.cep;
                                    qsa.t73303_cod_evento = "001";
                                    // If trocado para verificar se tem evento de transformação porque no DBE o empresario nao vem no QSA e nao tem 
                                    //indicador do evento, entao vamos marcamos como 001 e nao como 003
                                    //Mudadança solicitada pelo arnldo dia 06/01/2020
                                    // if (!eventoConstituicao && !eventoTransformacao)
                                    if (!eventoConstituicao && !eventoTransformacao)
                                    {
                                        qsa.t73303_cod_evento = "003";
                                    }

                                    //Isto aqui e porque foi colocado  resposnsavel que esta no ws11 como saindo, entao o que esta no DBE vai insluindo
                                    if (trocadeQSATipoEmpresario)
                                        qsa.t73303_cod_evento = "001";

                                    qsa.t73303_cod_munic_qsa = GlobalV1.valNulo(enderecoResponsavel.codMunicipio) ? "" : enderecoResponsavel.codMunicipio;
                                    qsa.t73303_cod_munic_qsa += psc.Framework.General.CalculateVerificationDigit(qsa.t73303_cod_munic_qsa, 11).ToString();
                                    qsa.t73303_cod_munic_qsa = int.Parse(qsa.t73303_cod_munic_qsa).ToString();
                                    qsa.t73303_cod_pais = GlobalV1.valNulo(enderecoResponsavel.codPais) ? "" : enderecoResponsavel.codPais;
                                    qsa.t73303_complemento_lograd_qsa = GlobalV1.valNulo(enderecoResponsavel.complementoLogradouro) ? "" : enderecoResponsavel.complementoLogradouro;
                                    qsa.t73303_correio_eletronico_qsa = GlobalV1.valNulo(contatoResponsavel.correioEletronico) ? "" : contatoResponsavel.correioEletronico;
                                    qsa.t73303_ind_cpf_cnpj_qsa = "2"; //Sempre cpf
                                    int De = 14;
                                    if (qsa.t73303_ind_cpf_cnpj_qsa == "2")
                                    {
                                        De = 11;
                                    }
                                    if (GlobalV1.valNuloBranco(DadosDbe.fcpj.cpfResponsavel).Length > 2)
                                    {
                                        int Ate = DadosDbe.fcpj.cpfResponsavel.Length - De;
                                        qsa.t73303_cpf_cnpj_qsa = DadosDbe.fcpj.cpfResponsavel.Substring(Ate);
                                    }

                                    qsa.t73303_dat_evento = DateTime.Now;
                                    qsa.t73303_dat_inicio_mandato = DateTime.Now;
                                    qsa.t73303_ddd_fax_qsa = GlobalV1.valNuloBranco(contatoResponsavel.dddFax);
                                    qsa.t73303_ddd_telefone_qsa = GlobalV1.valNuloBranco(contatoResponsavel.dddTelefone1);
                                    qsa.t73303_distrito_qsa = GlobalV1.valNuloBranco(enderecoResponsavel.distrito);
                                    qsa.t73303_fax_qsa = GlobalV1.valNuloBranco(contatoResponsavel.fax);
                                    qsa.t73303_lograd_qsa = GlobalV1.valNuloBranco(enderecoResponsavel.logradouro);
                                    //qsa.t73303_nacionalidade_qsa = Socio.nacionalidadeSocioPf;
                                    //qsa.t73303_nire_qsa = Socio.;
                                    qsa.t73303_nom_qsa = GlobalV1.valNuloBranco(DadosDbe.fcpj.nomeResponsavel);
                                    qsa.t73303_num_lograd_qsa = GlobalV1.valNuloBranco(enderecoResponsavel.numLogradouro);

                                    qsa.t73303_qualificacao_qsa = GlobalV1.valNuloBranco(DadosDbe.fcpj.codQualificResponsavel);
                                    if (qsa.t73303_qualificacao_qsa == null || qsa.t73303_qualificacao_qsa == "")
                                        qsa.t73303_qualificacao_qsa = "50";

                                    qsa.t73303_qualificacao_qsa = qsa.t73303_qualificacao_qsa.PadLeft(2, '0');

                                    //qsa.t73303_qualificacao_qsa = "50";
                                    qsa.t73303_telefone_qsa = GlobalV1.valNuloBranco(contatoResponsavel.telefone1);
                                    qsa.t73303_tip_lograd_qsa = GlobalV1.valNuloBranco(enderecoResponsavel.codTipoLogradouro);
                                    qsa.t73303_uf_qsa = GlobalV1.valNuloBranco(enderecoResponsavel.uf);


                                    //Isto aqui e porque as quando e empresario com evento 407 - inventariante (empresario) no XML vem somento o representante e nao mais o QSA
                                    // com isso vamos contianar colocando esse representante na tabela de QSA mas tb vamos a preenchar os campos do 
                                    //representante com ele mesmo
                                    //Feito Raul Gamboa e Arnaldo dia 04/07/2022
                                    if (qsa.t73303_cargo_qsa == "12")
                                    {
                                        qsa.t73303_cpf_rep_legal = qsa.t73303_cpf_cnpj_qsa;
                                        qsa.t73303_qualificacao_rep_legal = qsa.t73303_cargo_qsa;
                                        qsa.t73303_nom_rep_legal = qsa.t73303_nom_qsa;
                                        qsa.t73303_bairro_rep_legal = GlobalV1.valNulo(enderecoResponsavel.bairro) ? "" : enderecoResponsavel.bairro;
                                        qsa.t73303_correio_eletronico_rep_ = qsa.t73303_correio_eletronico_qsa;
                                        qsa.t73303_telefone_rep_legal = qsa.t73303_telefone_qsa;
                                        qsa.t73303_tip_lograd_rep_legal = qsa.t73303_tip_lograd_qsa;
                                        qsa.t73303_cep_rep_legal = GlobalV1.valNulo(enderecoResponsavel.cep) ? "" : enderecoResponsavel.cep;
                                        qsa.t73303_cod_munic_rep_legal = GlobalV1.valNulo(enderecoResponsavel.codMunicipio) ? "" : enderecoResponsavel.codMunicipio;
                                        qsa.t73303_cod_munic_rep_legal += psc.Framework.General.CalculateVerificationDigit(qsa.t73303_cod_munic_rep_legal, 11).ToString();
                                        qsa.t73303_cod_munic_rep_legal = int.Parse(qsa.t73303_cod_munic_rep_legal).ToString();
                                        qsa.t73303_complemento_lograd_rep_ = GlobalV1.valNulo(enderecoResponsavel.complementoLogradouro) ? "" : enderecoResponsavel.complementoLogradouro;
                                        qsa.t73303_distrito_rep_legal = GlobalV1.valNuloBranco(enderecoResponsavel.distrito);
                                        qsa.t73303_lograd_rep_legal = GlobalV1.valNuloBranco(enderecoResponsavel.logradouro);
                                        qsa.t73303_num_lograd_rep_legal = GlobalV1.valNuloBranco(enderecoResponsavel.numLogradouro);
                                        qsa.t73303_uf_rep_legal = GlobalV1.valNuloBranco(enderecoResponsavel.uf);
                                        qsa.t73303_tip_lograd_rep_legal = GlobalV1.valNuloBranco(enderecoResponsavel.codTipoLogradouro);
                                    }



                                    qsa.Update();
                                }
                            }
                            #endregion
                        }
                    }

                    #region Forma de atuação
                    if (DadosDbe.atividadeEconomica != null)
                    {
                        if (DadosDbe.atividadeEconomica.codFormaDeAtuacao != null)
                        {
                            using (T73308_DBE_FORMA_ATUACAO fa = new T73308_DBE_FORMA_ATUACAO())
                            {
                                fa.MainConnectionProvider = cp;
                                fa.t73300_id_control = idControl;
                                if (DadosDbe.atividadeEconomica.codFormaDeAtuacao != null)
                                {
                                    foreach (string pCodFormaAtuacao in DadosDbe.atividadeEconomica.codFormaDeAtuacao)
                                    {
                                        if (pCodFormaAtuacao == "")
                                        {
                                            break;
                                        }
                                        fa.t73308_cod_forma_atuacao = pCodFormaAtuacao;
                                        fa.Update();
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    if (DadosDbe.fcpj.codEvento != null)
                    {
                        using (T73301_DBE_EVENTO ev = new T73301_DBE_EVENTO())
                        {
                            ev.MainConnectionProvider = cp;
                            ev.t73300_id_control = idControl;
                            int i = 0;
                            foreach (string pCodEvento in DadosDbe.fcpj.codEvento)
                            {
                                if (pCodEvento == "")
                                {
                                    break;
                                }
                                if (pCodEvento.Trim() == "101" || pCodEvento.Trim() == "102")
                                {
                                    eventoConstituicao = true;
                                }
                                ev.t73301_cod_evento = pCodEvento;
                                //if (GlobalV1.valNulo(DadosDbe.fcpj.tipoEvento[i].ToString() != ""))
                                //{
                                //    ev.t73301_tip_evento = DadosDbe.fcpj.tipoEvento[i].ToString();
                                //}
                                i += i;
                                /*
                                    isso para nao gravar o 202 quando e limitada por exemplo, ja que esta vindo 202 sem socio, entao
                                 * o requerimento acha que tem alteração de socio no dbe e nao tem
                                 */
                                if (DadosDbe.fcpj.codNaturezaJuridica != "2135" && pCodEvento == "202")
                                {

                                }
                                else
                                {
                                    ev.Update();
                                }
                            }
                        }
                    }

                    if (DadosDbe.atividadeEconomica != null)
                    {
                        using (T73304_DBE_CNAE_SECUNDARIA cs = new T73304_DBE_CNAE_SECUNDARIA())
                        {
                            cs.MainConnectionProvider = cp;
                            cs.t73300_id_control = idControl;
                            if (DadosDbe.atividadeEconomica.codCnaeSecundaria != null)
                            {
                                foreach (string pCodEvento in DadosDbe.atividadeEconomica.codCnaeSecundaria)
                                {
                                    if (pCodEvento.Length < 5)
                                    {
                                        break;
                                    }

                                    cs.t73304_cnae_secundaria = pCodEvento.PadLeft(7, '0');

                                    cs.Update();
                                }
                            }
                        }
                    }

                    using (T73305_DBE_CONTADOR co = new T73305_DBE_CONTADOR())
                    {
                        if (GlobalV1.valNuloBranco(DadosDbe.fcpj.cnpjEmpresaContabil) != "" || GlobalV1.valNuloBranco(DadosDbe.fcpj.cpfContadorPF) != "")
                        {
                            co.MainConnectionProvider = cp;
                            co.t73300_id_control = idControl;
                            WsRFBReginV2.WsServices35RFB.endereco enderecoContador = new WsRFBReginV2.WsServices35RFB.endereco();
                            WsRFBReginV2.WsServices35RFB.contato contatocontador = new WsRFBReginV2.WsServices35RFB.contato();

                            co.t73305_nom_contador = GlobalV1.valNuloBranco(DadosDbe.fcpj.nomeContadorPF);

                            co.t73305_uf_crc = GlobalV1.valNuloBranco(DadosDbe.fcpj.ufContadorPF);
                            co.t73305_tip_crc = GlobalV1.valNuloBranco(DadosDbe.fcpj.codTipoCRCcontadorPF);
                            co.t73305_seq_crc = GlobalV1.valNuloBranco(DadosDbe.fcpj.numSeqContadorPF);
                            co.t73305_cpf_cnpj_contador = GlobalV1.valNuloBranco(DadosDbe.fcpj.cpfContadorPF);
                            co.t73305_cod_classific_empresa = GlobalV1.valNuloBranco(DadosDbe.fcpj.codClassificEmpresaContabil);
                            co.t73305_cod_classific_contabilista = GlobalV1.valNuloBranco(DadosDbe.fcpj.codClassificCRCcontadorPF);
                            co.t73305_tip_contador = "1";
                            //_t73305_cod_classific_contabilista = "";
                            //protected string _t73305_cod_classific_empresa = "";



                            if (DadosDbe.fcpj.cnpjEmpresaContabil != null && DadosDbe.fcpj.cnpjEmpresaContabil != "")
                            {
                                co.t73305_cpf_cnpj_contador = GlobalV1.valNuloBranco(DadosDbe.fcpj.cnpjEmpresaContabil);


                                co.t73305_tip_contador = "2";
                                enderecoContador = DadosDbe.fcpj.endEmpresaContabilComplementar;
                                contatocontador = DadosDbe.fcpj.contatoContadorPf;

                                co.t73305_nom_contador = GlobalV1.valNuloBranco(DadosDbe.fcpj.nomeEmpresaContabil);

                                co.t73305_uf_crc = GlobalV1.valNuloBranco(DadosDbe.fcpj.ufCRCempresaContabil);
                                co.t73305_tip_crc = GlobalV1.valNuloBranco(DadosDbe.fcpj.codTipoCRCempresaContabil);
                                co.t73305_seq_crc = GlobalV1.valNuloBranco(DadosDbe.fcpj.seqCRCempresaContabil);



                                /*
                                   Responsavel e a pessoa Fisica quando vem o cnpj do contador
                                 */
                                co.t73305_cpf_responsavel = GlobalV1.valNuloBranco(DadosDbe.fcpj.cpfContadorPF);
                                co.t73305_uf_crc_responsavel = GlobalV1.valNuloBranco(DadosDbe.fcpj.ufContadorPF);
                                co.t73305_seq_crc_responsavel = GlobalV1.valNuloBranco(DadosDbe.fcpj.numSeqContadorPF);
                                co.t73305_tip_crc_responsavel = GlobalV1.valNuloBranco(DadosDbe.fcpj.codTipoCRCcontadorPF);

                            }
                            else
                            {
                                //co.t73305_cpf_cnpj_contador = GlobalV1.valNuloBranco(DadosDbe.fcpj.cpfContadorPF);
                                //co.t73305_tip_contador = "1";
                                enderecoContador = DadosDbe.fcpj.endContadorPf;
                                contatocontador = DadosDbe.fcpj.contatoEmpresaContabilComplementar;
                            }

                            if (enderecoContador != null)
                            {
                                co.t73305_bairro = GlobalV1.valNuloBranco(enderecoContador.bairro);
                                co.t73305_cep = GlobalV1.valNuloBranco(enderecoContador.cep);

                                co.t73305_cod_munic = GlobalV1.valNuloBranco(enderecoContador.codMunicipio);

                                if (co.t73305_cod_munic != "")
                                {
                                    co.t73305_cod_munic += psc.Framework.General.CalculateVerificationDigit(enderecoContador.codMunicipio, 11).ToString();
                                    co.t73305_cod_munic = int.Parse(co.t73305_cod_munic).ToString();
                                }

                                co.t73305_complemento_logradouro = GlobalV1.valNuloBranco(enderecoContador.complementoLogradouro);

                                co.t73305_distrito = GlobalV1.valNuloBranco(enderecoContador.distrito);
                                co.t73305_nom_logradouro = GlobalV1.valNuloBranco(enderecoContador.logradouro);
                                co.t73305_num_logradouro = GlobalV1.valNuloBranco(enderecoContador.numLogradouro);
                                co.t73305_uf = GlobalV1.valNuloBranco(enderecoContador.uf);


                            }

                            co.t73305_cod_classificacao = GlobalV1.valNuloBranco(DadosDbe.fcpj.codClassificCRCcontadorPF);


                            if (DadosDbe.fcpj.contatoContadorPf != null)
                            {
                                co.t73305_correio_eletronico = GlobalV1.valNuloBranco(DadosDbe.fcpj.contatoContadorPf.correioEletronico);
                            }


                            DateTime dt;
                            if (DadosDbe.fcpj.dataEmissaoIdContadorPF == null)
                            {
                                dt = new DateTime();
                            }
                            else
                            {
                                dt = dHelperQuery.convertStringDateYYYMMDD(DadosDbe.fcpj.dataEmissaoIdContadorPF.ToString());
                            }
                            co.t73305_dat_emissao_id = dt;

                            if (DadosDbe.fcpj.dataRegistroCRCcontadorPF == null)
                            {
                                dt = new DateTime();
                            }
                            else
                            {
                                dt = dHelperQuery.convertStringDateYYYMMDD(DadosDbe.fcpj.dataRegistroCRCcontadorPF.ToString());
                            }
                            co.t73305_dat_registro_crc = dt;
                            if (contatocontador != null)
                            {
                                co.t73305_ddd_fax = GlobalV1.valNuloBranco(contatocontador.dddFax);
                                co.t73305_ddd_telefone = GlobalV1.valNuloBranco(contatocontador.dddTelefone1);
                                co.t73305_fax = GlobalV1.valNuloBranco(contatocontador.fax);
                                co.t73305_telefone = GlobalV1.valNuloBranco(contatocontador.telefone1);
                            }

                            co.t73305_ident_contador = GlobalV1.valNuloBranco(DadosDbe.fcpj.idContadorPF);
                            co.t73305_opcao_doc_eletronico = GlobalV1.valNuloBranco(DadosDbe.fcpj.inOpcaoDocumentosEletronicos);
                            co.t73305_opcao_livro_eletronico = GlobalV1.valNuloBranco(DadosDbe.fcpj.inOpcaoLivrosEletronicos);
                            co.t73305_orgao_emissor_id = GlobalV1.valNuloBranco(DadosDbe.fcpj.orgaoEmissorIdContadorPF);
                            co.t73305_perman_livro_fiscal = GlobalV1.valNuloBranco(DadosDbe.fcpj.inPermanencialivrosFiscais);
                            co.t73305_proc_eletr_dados = GlobalV1.valNuloBranco(DadosDbe.fcpj.inProcessamentoEletronicoDados);

                            if (DadosDbe.fcpj.endContadorPf != null)
                            {
                                co.t73305_tip_logradouro = GlobalV1.valNuloBranco(DadosDbe.fcpj.endContadorPf.codTipoLogradouro);
                            }
                            co.t73305_uf_orgao_emis = GlobalV1.valNuloBranco(DadosDbe.fcpj.ufOrgaoEmissorIdContadorPF);
                            co.t73305_util_ecf = GlobalV1.valNuloBranco(DadosDbe.fcpj.inUtilizacaoECF);

                            co.Update();
                        }
                    }

                    if (DadosDbe.socios != null)
                    {
                        foreach (WsRFBReginV2.WsServices35RFB.socio Socio in DadosDbe.socios)
                        {
                            using (T73303_DBE_QSA qsa = new T73303_DBE_QSA())
                            {
                                qsa.MainConnectionProvider = cp;
                                qsa.t73300_id_control = idControl;
                                if (Socio.cnpjCpfSocio != "")
                                {
                                    qsa.MainConnectionProvider = cp;

                                    if (Socio.endSocio != null)
                                    {
                                        qsa.t73303_bairro_qsa = GlobalV1.valNulo(Socio.endSocio.bairro) ? "" : Socio.endSocio.bairro;
                                        qsa.t73303_cep_qsa = GlobalV1.valNulo(Socio.endSocio.cep) ? "" : Socio.endSocio.cep;
                                        qsa.t73303_cod_munic_qsa = GlobalV1.valNulo(Socio.endSocio.codMunicipio) ? "" : Socio.endSocio.codMunicipio;
                                        qsa.t73303_cod_munic_qsa += psc.Framework.General.CalculateVerificationDigit(qsa.t73303_cod_munic_qsa, 11).ToString();
                                        qsa.t73303_cod_munic_qsa = int.Parse(qsa.t73303_cod_munic_qsa).ToString();

                                        qsa.t73303_cod_pais = GlobalV1.valNulo(Socio.endSocio.codPais) ? "" : Socio.endSocio.codPais;
                                        qsa.t73303_complemento_lograd_qsa = GlobalV1.valNulo(Socio.endSocio.complementoLogradouro) ? "" : Socio.endSocio.complementoLogradouro;
                                        qsa.t73303_lograd_qsa = GlobalV1.valNuloBranco(Socio.endSocio.logradouro);
                                        qsa.t73303_distrito_qsa = GlobalV1.valNuloBranco(Socio.endSocio.distrito);
                                        qsa.t73303_num_lograd_qsa = GlobalV1.valNuloBranco(Socio.endSocio.numLogradouro);
                                        qsa.t73303_tip_lograd_qsa = GlobalV1.valNuloBranco(Socio.endSocio.codTipoLogradouro);
                                        qsa.t73303_uf_qsa = GlobalV1.valNuloBranco(Socio.endSocio.uf);

                                    }
                                    if (Socio.endRepLegal != null)
                                    {
                                        qsa.t73303_bairro_rep_legal = GlobalV1.valNulo(Socio.endRepLegal.bairro) ? "" : Socio.endRepLegal.bairro;
                                        qsa.t73303_cep_rep_legal = GlobalV1.valNulo(Socio.endRepLegal.cep) ? "" : Socio.endRepLegal.cep;
                                        qsa.t73303_cod_munic_rep_legal = GlobalV1.valNulo(Socio.endRepLegal.codMunicipio) ? "" : Socio.endRepLegal.codMunicipio;
                                        qsa.t73303_cod_munic_rep_legal += psc.Framework.General.CalculateVerificationDigit(qsa.t73303_cod_munic_rep_legal, 11).ToString();
                                        qsa.t73303_cod_munic_rep_legal = int.Parse(qsa.t73303_cod_munic_rep_legal).ToString();
                                        qsa.t73303_complemento_lograd_rep_ = GlobalV1.valNulo(Socio.endRepLegal.complementoLogradouro) ? "" : Socio.endRepLegal.complementoLogradouro;
                                        qsa.t73303_distrito_rep_legal = GlobalV1.valNuloBranco(Socio.endRepLegal.distrito);
                                        qsa.t73303_lograd_rep_legal = GlobalV1.valNuloBranco(Socio.endRepLegal.logradouro);
                                        qsa.t73303_num_lograd_rep_legal = GlobalV1.valNuloBranco(Socio.endRepLegal.numLogradouro);
                                        qsa.t73303_uf_rep_legal = GlobalV1.valNuloBranco(Socio.endRepLegal.uf);
                                        qsa.t73303_tip_lograd_rep_legal = GlobalV1.valNuloBranco(Socio.endRepLegal.codTipoLogradouro);

                                    }

                                    if (!GlobalV1.valNulo(DadosDbe.fcpj.capitalSocial))
                                    {
                                        qsa.t73303_capital_social_empresa = decimal.Parse(DadosDbe.fcpj.capitalSocial) / 100;
                                    }

                                    if (!GlobalV1.valNulo(Socio.capitalSocialSocio))
                                    {
                                        //qsa.t73303_perc_partic_qsa = decimal.Parse(Socio.capitalSocialSocio) / 100;
                                        qsa.t73303_capital_social_qsa = decimal.Parse(Socio.capitalSocialSocio) / 100;
                                    }

                                    /*
                                        Isto aqui e porque antes o percentual vinha no campo capitalSocialSocio
                                     * mas foi enviado uma ou iam colocar uma atualização nova para mudar o percentual para
                                     * percentualCapitalSocialSocio, entao se esse campo vier considero este mesmo para o calculo
                                     */
                                    //if (!GlobalV1.valNulo(Socio.percentualCapitalSocialSocio))
                                    //{
                                    qsa.t73303_perc_partic_qsa = 0;
                                    //qsa.t73303_capital_social_qsa = decimal.Parse(Socio.capitalSocialSocio) / 100;
                                    //}

                                    //
                                    //Alterado em 03/11/2014 para pegar o capital social do socio caso o campo Socio.capitalSocialSocio > 0
                                    //
                                    if ((!GlobalV1.valNulo(qsa.t73303_capital_social_empresa) && qsa.t73303_capital_social_empresa > 0)
                                        && qsa.t73303_perc_partic_qsa != 0)
                                    {
                                        qsa.t73303_capital_social_qsa = (qsa.t73303_capital_social_empresa * qsa.t73303_perc_partic_qsa) / 100;
                                        //qsa.t73303_perc_partic_qsa = (qsa.t73303_capital_social_empresa * qsa.t73303_perc_partic_qsa) / 100;
                                    }
                                    else
                                    {
                                        if (!GlobalV1.valNulo(Socio.capitalSocialSocio) && decimal.Parse(Socio.capitalSocialSocio) > 0)
                                        {
                                            qsa.t73303_capital_social_qsa = decimal.Parse(Socio.capitalSocialSocio) / 100;
                                        }
                                    }


                                    qsa.t73303_cargo_qsa = GlobalV1.valNulo(Socio.codQualificacaoSocio) ? "" : Socio.codQualificacaoSocio;
                                    qsa.t73303_cod_evento = GlobalV1.valNulo(Socio.codEvento) ? "" : Socio.codEvento;



                                    if (Socio.contatoSocio != null)
                                    {
                                        qsa.t73303_correio_eletronico_qsa = GlobalV1.valNulo(Socio.contatoSocio.correioEletronico) ? "" : Socio.contatoSocio.correioEletronico;
                                        qsa.t73303_ddd_fax_qsa = Socio.contatoSocio.dddFax;
                                        qsa.t73303_ddd_telefone_qsa = GlobalV1.valNuloBranco(Socio.contatoSocio.dddTelefone1);
                                        qsa.t73303_fax_qsa = GlobalV1.valNuloBranco(Socio.contatoSocio.fax);
                                        qsa.t73303_telefone_qsa = GlobalV1.valNuloBranco(Socio.contatoSocio.telefone1);

                                    }
                                    if (Socio.contatoRepLegal != null)
                                    {
                                        qsa.t73303_correio_eletronico_rep_ = GlobalV1.valNulo(Socio.contatoRepLegal.correioEletronico) ? "" : Socio.contatoRepLegal.correioEletronico;
                                        qsa.t73303_ddd_fax_rep_legal = GlobalV1.valNulo(Socio.contatoRepLegal.dddFax) ? "" : Socio.contatoRepLegal.dddFax;
                                        qsa.t73303_ddd_telefone_rep_legal = GlobalV1.valNulo(Socio.contatoRepLegal.dddTelefone1) ? "" : Socio.contatoRepLegal.dddTelefone1;
                                        qsa.t73303_fax_rep_legal = GlobalV1.valNulo(Socio.contatoRepLegal.fax) ? "" : Socio.contatoRepLegal.fax;
                                        qsa.t73303_telefone_rep_legal = GlobalV1.valNulo(Socio.contatoRepLegal.telefone1) ? "" : Socio.contatoRepLegal.telefone1;
                                    }

                                    qsa.t73303_ind_cpf_cnpj_qsa = GlobalV1.valNulo(Socio.indCnpjCpfSocio) ? "" : Socio.indCnpjCpfSocio;
                                    int De = 14;
                                    if (Socio.indCnpjCpfSocio == "2")
                                    {
                                        De = 11;
                                    }
                                    int Ate = (Socio.cnpjCpfSocio.Length - De);
                                    qsa.t73303_cpf_cnpj_qsa = Socio.cnpjCpfSocio.Substring(Ate);

                                    qsa.t73303_cpf_rep_legal = GlobalV1.valNulo(Socio.cpfRepresentanteLegal) ? "" : Socio.cpfRepresentanteLegal;
                                    //qsa.t73303_dat_emis_ident_rep_lega = Socio.representanteLegal.;
                                    //qsa.t73303_dat_emissao_ident = Socio.d;
                                    qsa.t73303_dat_evento = dHelperQuery.convertStringDateYYYMMDD(GlobalV1.valNulo(Socio.dataEvento) ? "" : Socio.dataEvento);
                                    qsa.t73303_dat_inicio_mandato = dHelperQuery.convertStringDateYYYMMDD(GlobalV1.valNulo(Socio.dataInclusaoCorreta) ? "" : Socio.dataInclusaoCorreta);
                                    //qsa.t73303_dat_termino_mandato;
                                    //qsa.t73303_dt_nascimento_socio_pf = Socio.d;
                                    //qsa.t73303_ident_passap_qsa = Socio.i;
                                    //qsa.t73303_ident_rep_legal;

                                    //qsa.t73303_matricula_rcpj = Socio.contatoSocio.ma
                                    qsa.t73303_nacionalidade_qsa = GlobalV1.valNulo(Socio.nacionalidadeSocioPf) ? "" : Socio.nacionalidadeSocioPf;
                                    //qsa.t73303_nire_qsa = Socio.;
                                    qsa.t73303_nom_qsa = GlobalV1.valNulo(Socio.socio1) ? "" : Socio.socio1;
                                    qsa.t73303_nom_rep_legal = GlobalV1.valNulo(Socio.representanteLegal) ? "" : Socio.representanteLegal;
                                    //qsa.t73303_or_emis_ident_rep_legal = Socio.;
                                    //qsa.t73303_orgao_emissor_ident;
                                    //qsa.t73303_orig_inf_lograd;
                                    //qsa.t73303_origem_endereco_rep_leg;

                                    qsa.t73303_qualificacao_qsa = GlobalV1.valNulo(Socio.codQualificacaoSocio) ? "" : Socio.codQualificacaoSocio;
                                    qsa.t73303_qualificacao_rep_legal = GlobalV1.valNulo(Socio.codQualificacaoRepresentanteLegal) ? "" : Socio.codQualificacaoRepresentanteLegal;
                                    //qsa.t73303_uf_or_emissor_rep_legal = Socio.;
                                    //qsa.t73303_uf_orgao_emissor_ident;
                                    qsa.t73303_uso_firma_administrador = "";

                                    qsa.Update();


                                    /*
                                        Para criar ou atualizar o evento de socio 202
                                     */
                                    if (!eventoConstituicao)
                                    {
                                        using (T73301_DBE_EVENTO ev = new T73301_DBE_EVENTO())
                                        {
                                            ev.MainConnectionProvider = cp;
                                            ev.t73300_id_control = idControl;
                                            ev.t73301_cod_evento = "202";
                                            ev.Update();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #region Verifica Se tem evento 210 e 225 para apagar 0 202 de socio
                    if (DadosDbe.fcpj.codEvento != null)
                    {
                        foreach (string pCodEvento in DadosDbe.fcpj.codEvento)
                        {
                            using (T73301_DBE_EVENTO ev = new T73301_DBE_EVENTO())
                            {
                                if (pCodEvento == "")
                                {
                                    break;
                                }
                                /*
                                    Isso parqa nao gravar o evento 202, porque se vier socios com esses eventos nao e para colocar o 202
                                 * na tabela
                                 * alterada somente para 210 dia 07/07/2017
                                 */
                                //if (pCodEvento.Trim() == "210" || pCodEvento.Trim() == "225")
                                //Retirado para nao apagar nem quando for 210 (Arnaldo 26/04/2018), porque e uma tranferencia de sede para outra UF
                                //if (pCodEvento.Trim() == "210")
                                //{
                                //    ev.MainConnectionProvider = cp;
                                //    ev.t73300_id_control = idControl;
                                //    ev.t73301_cod_evento = "202";
                                //    ev.DeletePk();
                                //}
                            }
                        }
                    }

                    #endregion
                    cp.CommitTransaction();
                }
                catch (Exception ex)
                {
                    cp.RollbackTransaction("");
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
