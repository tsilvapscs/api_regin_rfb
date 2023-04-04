using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using psc.Receita.Entities;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using System.IO;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using psc.Receita.ConnectionBase;
using MySql.Data.MySqlClient;
//using WsServices35RFB;
using psc.Receita;
using System.Collections.Generic;
using System.Linq;
using AcessoService;
using System.Xml;
using System.Text.RegularExpressions;
using psc.Ruc.Tablelas.Ruc;
using psc.Ruc.Tablelas.DAL.Ruc;
//using WsServicesReginRFB;
using WsRFBReginV2.Models;

namespace WsRFBReginV2
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ServiceReginRFB : System.Web.Services.WebService
    {

        public WsServices06RFB.processarComunicacaoDeferIndeferRequest _dadosWs06;



        public ServiceReginRFB()
        {


            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        [WebMethod]
        private void aa_Envias24()
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
            {
                using (OracleCommand cmdToExecute = new OracleCommand())
                {
                    //DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
                    cmdToExecute.CommandText = "select T.CNPJ from   AAAA_S24 t WHERE T.UF = 'PE' and t.descricaoRFB is null ";
                    cmdToExecute.CommandType = CommandType.Text;

                    //cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

                    _conn.Open();

                    cmdToExecute.Connection = _conn;

                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute))
                    {
                        adapter.Fill(toReturn);
                    }
                }
            }


            for (int a = 0; a < toReturn.Rows.Count; a++)
            {
                string CNPJ = toReturn.Rows[a]["CNPJ"].ToString().Trim();
                ServiceReginRFB regin24 = new ServiceReginRFB();

                RetornoBasico resulRegin24 = new RetornoBasico();

                resulRegin24 = regin24.ServiceWs24("10054583000197", CNPJ, "1");

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (OracleTransaction _conn = conn.BeginTransaction())
                    {
                        using (OracleCommand cmdToExecute = new OracleCommand())
                        {
                            cmdToExecute.Connection = _conn.Connection;
                            cmdToExecute.Transaction = _conn;
                            string mensagem = resulRegin24.status + " - " + resulRegin24.descricao;

                            StringBuilder sqlU = new StringBuilder(" update AAAA_S24 set descricaoRFB = :v_mensagem where CNPJ = :v_CNPJ ");

                            cmdToExecute.Parameters.Add(new OracleParameter("v_mensagem", OracleType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, mensagem));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_CNPJ", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, CNPJ));

                            cmdToExecute.CommandText = sqlU.ToString();
                            cmdToExecute.CommandType = CommandType.Text;
                            cmdToExecute.ExecuteNonQuery();
                            _conn.Commit();
                        }
                    }
                }

            }



        }


        #region Serviço 01 RFB
        [WebMethod]
        public RetornoS01 ServiceWs01(DadosViabilidade DadosEntrada)
        {

            RetornoS01 result = new RetornoS01();
            WsServices01RFB.processarViabilidadeRequest Request = new WsServices01RFB.processarViabilidadeRequest();
            WsServices01RFB.processarViabilidadeResponse pResult = new WsServices01RFB.processarViabilidadeResponse();


            string NomeNoCnpj = "CNPJ COMO NOME";

            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());


                WsServices01RFB.processarViabilidadeRequest R = new WsServices01RFB.processarViabilidadeRequest();
                DadosViabilidade dv = new DadosViabilidade();

                //Todos devem ser preenchidos
                Request.codServico = "S01";

                string[] codEventosViabilidade = new string[DadosEntrada.codEventosViabilidade.Length];
                int even = 0;
                foreach (string codEventos in DadosEntrada.codEventosViabilidade)
                {
                    if (codEventos != null && codEventos != "")
                    {
                        if (dv.isEventoStr(codEventos, "101, 102, 106, 209, 210, 211, 220, 225, 244, 248, 249"))
                        {
                            codEventosViabilidade[even] = codEventos;
                            even++;
                        }
                    }
                }


                Request.codEventosViabilidade = codEventosViabilidade;


                Request.codStatusViabilidade = DadosEntrada.codStatusViabilidade;
                Request.protocoloViabilidade = DadosEntrada.protocoloViabilidade;
                Request.dataFimAnaliseViabilidadeEndereco = DadosEntrada.dataFimAnaliseViabilidadeEndereco;
                Request.dataFimAnaliseViabilidadeNome = DadosEntrada.dataFimAnaliseViabilidadeNome;
                Request.dataInicioAnaliseViabilidade = DadosEntrada.dataInicioAnaliseViabilidade;
                Request.dataValidadeViabilidade = DadosEntrada.dataValidadeViabilidade;
                Request.resultadoViabilidadeEndereco = DadosEntrada.resultadoViabilidadeEndereco;
                Request.resultadoViabilidadeNome = DadosEntrada.resultadoViabilidadeNome;
                Request.naturezaJuridica = DadosEntrada.naturezaJuridica;



                //So envia o indicador no eventos 101 e 102, assim diz a documentação
                if (dv.isEvento(Request.codEventosViabilidade, "101, 102"))
                {
                    Request.indicadorContabilista = "";
                    if (DadosEntrada.indicadorContabilista != null && (DadosEntrada.indicadorContabilista == "1" || DadosEntrada.indicadorContabilista == "S"))
                        Request.indicadorContabilista = "S";
                }
                /*
                 Campos novos
                 */
                if (DadosEntrada.horaInicioAnaliseViabilidade == null || DadosEntrada.horaInicioAnaliseViabilidade == "" || DadosEntrada.horaInicioAnaliseViabilidade.Length < 6)
                {
                    DadosEntrada.horaInicioAnaliseViabilidade = "090000";
                }
                if (DadosEntrada.horaFimAnaliseViabilidadeNome == null || DadosEntrada.horaFimAnaliseViabilidadeNome == "" || DadosEntrada.horaFimAnaliseViabilidadeNome.Length < 6)
                {
                    DadosEntrada.horaFimAnaliseViabilidadeNome = "090000";
                }

                if (DadosEntrada.horaFimAnaliseViabilidadeEndereco == null || DadosEntrada.horaFimAnaliseViabilidadeEndereco == "" || DadosEntrada.horaFimAnaliseViabilidadeEndereco.Length < 6)
                {
                    DadosEntrada.horaFimAnaliseViabilidadeEndereco = "090000";
                }

                Request.horaInicioAnaliseViabilidade = DadosEntrada.horaInicioAnaliseViabilidade + "0";
                Request.horaFimAnaliseViabilidadeNome = DadosEntrada.horaFimAnaliseViabilidadeNome + "0";
                Request.horaFimAnaliseViabilidadeEndereco = DadosEntrada.horaFimAnaliseViabilidadeEndereco + "0";


                if (DadosEntrada.cpfSolicitante != null && DadosEntrada.cpfSolicitante != "")
                {
                    if (DadosEntrada.cpfSolicitante.Trim().Length < 12)
                    {
                        Request.cpfSolicitante = DadosEntrada.cpfSolicitante.Trim();
                    }
                }

                if (Request.codStatusViabilidade == "99")
                {
                    if (Request.cpfSolicitante == null || Request.cpfSolicitante == "" || Request.cpfSolicitante.Length < 11)
                    {
                        Request.cpfSolicitante = "11111111111";
                    }
                }


                if (Request.cpfSolicitante == null || Request.cpfSolicitante == "" || Request.cpfSolicitante.Length < 11)
                {
                    /*
                        Busco o cpf de algum dos qsa CASO NAO TENHA por ser CNPJ o solicitante
                     * isso e porque em SC e/ou BA finalizam viabilidade antigas e cokm
                     */
                    if (DadosEntrada.cpfSocioPf != null)
                    {
                        foreach (string cpfSocio in DadosEntrada.cpfSocioPf)
                        {
                            if (cpfSocio != null && cpfSocio.Length == 11)
                            {
                                Request.cpfSolicitante = cpfSocio;
                                break;
                            }
                        }
                    }
                }


                /*
                    Ate aqui campos novos
                 */

                //Request.tipoOrgaoRegistro = DadosEntrada.tipoOrgaoRegistro;

                //Naturezas que nao tem tipo de orgao de registro
                bool Tipo = true;

                if (DadosEntrada.tipoOrgaoRegistro == "9")
                {
                    Tipo = false;
                }
                if (Tipo)
                {
                    if (ConfigurationManager.AppSettings["NaturezaSemTipo"] != null && ConfigurationManager.AppSettings["NaturezaSemTipo"].ToString() != "")
                    {
                        string[] NaturezaSemTipo = ConfigurationManager.AppSettings["NaturezaSemTipo"].ToString().Split('-');

                        foreach (string Natureza in NaturezaSemTipo)
                        {
                            if (Natureza == Request.naturezaJuridica)
                            {
                                Tipo = false;
                                break;
                            }
                        }
                    }
                }

                if (Tipo)
                {
                    Request.tipoOrgaoRegistro = DadosEntrada.tipoOrgaoRegistro;
                }


                //Ate aqui todos devem estar preenchidos

                //bool eee = dv.isEvento(Request.codEventosViabilidade, "101, 102");


                if (DadosEntrada.cnpj != "")
                {
                    Request.cnpj = DadosEntrada.cnpj;

                    //if (Request.cnpj.Trim() == "09542167000159")
                    //{
                    //    Request.tipoOrgaoRegistro = "2";
                    //}
                }

                //Isto aqui foi combinado com reis por motivos q esta dando erro ao tentar enviar´porque diz q e cartorio
                //if (DadosEntrada.cnpj == "09542167000159")
                //{
                //    Request.tipoOrgaoRegistro = "2";
                //}

                /*
                    Quando for filia, caso seja 102, manda o VNPJ da Mariz
                 */
                if (dv.isEvento(Request.codEventosViabilidade, "102"))
                {
                    Request.cnpj = DadosEntrada.CNPJMatriz;
                    if (DadosEntrada.uf == "RJ" && DadosEntrada.CNPJMatriz == "")
                        Request.cnpj = DadosEntrada.cnpj;


                }

                if (DadosEntrada.cnpjFilial != "")
                {
                    Request.cnpjFilial = DadosEntrada.cnpjFilial;
                }


                /*
                    Verifico se a empresa e Filial com isso veja se tem alguns eventos como 202,  225 e retiro dos dados
                 * para nao enviar
                 */
                if (DadosEntrada.nire.Length > 8)
                {
                    if (DadosEntrada.tipoOrgaoRegistro.Trim() == "1")
                    {
                        //Somente faço isso quando e Junta comercial, ja que o substring para ver filial somente 
                        // pode se fazer na junta, vou tentar passar depois para esta rotina se a empresa e filial e matriz
                        //com isso resolva melhor
                        if (DadosEntrada.nire.Substring(2, 1) == "9" && dv.isEvento(Request.codEventosViabilidade, "220, 225"))
                        {
                            string[] codEventosViabilidade3 = new string[Request.codEventosViabilidade.Length];
                            int even3 = 0;
                            foreach (string codEventos in DadosEntrada.codEventosViabilidade)
                            {
                                if (codEventos != "220" && codEventos != "225")
                                {
                                    codEventosViabilidade3[even3] = codEventos;
                                    even3++;
                                }
                            }
                            Request.codEventosViabilidade = codEventosViabilidade3;
                        }
                    }
                }
                else
                {

                    bool filialSim = false;

                    if (DadosEntrada.CNPJMatriz.Trim() != "" && DadosEntrada.CNPJMatriz.Trim() != DadosEntrada.cnpj.Trim())
                    {
                        filialSim = true;
                    }
                    /*
                        Esto aqui e porque diferente de Junta to fazendo com um campo novo que estou passando 
                     * caso funcione posso colocar este codigo tanto para junta como para
                     */
                    if (dv.isEvento(Request.codEventosViabilidade, "220, 225") && filialSim)
                    {
                        //Somente faço isso quando e Junta comercial, ja que o substring para ver filial somente 
                        // pode se fazer na junta, vou tentar passar depois para esta rotina se a empresa e filial e matriz
                        //com isso resolva melhor
                        string[] codEventosViabilidade3 = new string[Request.codEventosViabilidade.Length];
                        int even3 = 0;
                        foreach (string codEventos in DadosEntrada.codEventosViabilidade)
                        {
                            if (codEventos != "220" && codEventos != "225")
                            {
                                codEventosViabilidade3[even3] = codEventos;
                                even3++;
                            }
                        }
                        Request.codEventosViabilidade = codEventosViabilidade3;

                    }
                }

                //Para verificar caso seja estas condicionantes envio o indicador para a RFB
                if (dv.isEvento(Request.codEventosViabilidade, "101, 220"))
                {
                    /*
                        204-6 Sociedade Anônima Aberta NN.NNN.NNN S.A.
                        205-4 Sociedade Anônima Fechada NN.NNN.NNN S.A.
                        206-2 Sociedade Empresária Limitada NN.NNN.NNN LTDA.
                        207-0 Sociedade Empresária em Nome Coletivo NN.NNN.NNN EM NOME COLETIVO
                        208-9 Sociedade Empresária em Comandita Simples NN.NNN.NNN EM COMANDITA SIMPLES
                        209-7 Sociedade Empresária em Comandita por Ações NN.NNN.NNN EM COMANDITA POR ACOES
                        213-5 Empresário (Individual) NN.NNN.NNN “Nome do Empresário na base CPF”
                        214-3 Cooperativa
                        233-0 Cooperativas de Consumo
                     * */
                    if (Request.naturezaJuridica == "2046" ||
                        Request.naturezaJuridica == "2054" ||
                        Request.naturezaJuridica == "2062" ||
                        Request.naturezaJuridica == "2070" ||
                        Request.naturezaJuridica == "2089" ||
                        Request.naturezaJuridica == "2097" ||
                        Request.naturezaJuridica == "2135" ||
                        Request.naturezaJuridica == "2143" ||
                        Request.naturezaJuridica == "2330")
                    {
                        Request.indicadorCnpjNomeEmpresarial = "N";
                        if (DadosEntrada.indicadorCnpjNomeEmpresarial != null && (DadosEntrada.indicadorCnpjNomeEmpresarial == "1" || DadosEntrada.indicadorCnpjNomeEmpresarial == "S"))
                            Request.indicadorCnpjNomeEmpresarial = "S";
                    }
                }


                if (dv.isEvento(Request.codEventosViabilidade, "101, 102, 106, 209, 210, 211"))
                {
                    /* 
                 * Campos de endereço
                 * */
                    Request.codMunicipio = DadosEntrada.codMunicipio.PadLeft(5, '0').Substring(0, 4);
                    Request.bairro = DadosEntrada.bairro.ToUpper();
                    Request.cep = DadosEntrada.cep;
                    //Distrito somente e preenchido se o bairro estiver vazio
                    if (Request.bairro == "")
                    {
                        Request.distrito = DadosEntrada.distrito;
                    }
                    Request.logradouro = DadosViabilidade.TiraAcento(DadosEntrada.logradouro.ToUpper());


                    Request.referencia = DadosViabilidade.TiraAcento(DadosEntrada.referencia.ToUpper());

                    //string tipo = "";

                    //foreach (DataRow TipoLogradouro in DtTipoLogra.Rows)
                    //{
                    //    if (TipoLogradouro["COD_REGIN"].ToString() == DadosEntrada.codTipoLogradouro)
                    //    {
                    //        tipo = TipoLogradouro["COD_RFB"].ToString();
                    //        //return;
                    //        break;
                    //    }
                    //}


                    Request.codTipoLogradouro = DadosEntrada.codTipoLogradouro;
                    Request.uf = DadosEntrada.uf;
                    Request.numLogradouro = DadosEntrada.numLogradouro;

                    if (Request.numLogradouro.Length > 6)
                        Request.numLogradouro = Request.numLogradouro.Substring(0, 6);


                    if (DadosEntrada.uf == "RJ" && DadosEntrada.codMunicipio == "60011")
                    {
                        DadosEntrada.complementoLogradouroRFB = DadosViabilidade.TiraAcento(DadosEntrada.complementoLogradouroRFB.ToUpper().Trim());
                    }


                    DadosEntrada.complementoLogradouroRFB = DadosEntrada.complementoLogradouroRFB.ToUpper().Trim();// DadosViabilidade.TiraAcento(DadosEntrada.complementoLogradouroRFB, "Complemento").Trim();
                    DadosEntrada.complementoLogradouro = DadosViabilidade.TiraAcento(DadosEntrada.complementoLogradouro.ToUpper(), "Complemento").Trim();

                    try
                    {
                        //decimal a = decimal.Parse("a");
                        WsServices01RFB.complementoLogradouro[] complementoRFB = new WsServices01RFB.complementoLogradouro[6];
                        Request.complementoLogradouro = processaComplemento(DadosEntrada.complementoLogradouroRFB, DadosEntrada.complementoLogradouro, "RFB");

                        foreach (WsServices01RFB.complementoLogradouro pComp in Request.complementoLogradouro)
                        {
                            if (pComp != null && pComp.tipoComplementoLogradouro != null && pComp.tipoComplementoLogradouro != "")
                            {
                                int qtd = GlobalV1.BuscaDadosTipoComplemento(pComp.tipoComplementoLogradouro);
                                if (qtd == 0)
                                {
                                    throw new Exception("Nao existe Complmento");
                                }
                            }
                        }
                    }
                    catch
                    {
                        if (DadosEntrada.complementoLogradouro.Length > 0)
                        {
                            WsServices01RFB.complementoLogradouro comp = new WsServices01RFB.complementoLogradouro();
                            WsServices01RFB.complementoLogradouro[] complemento = new WsServices01RFB.complementoLogradouro[6];
                            string complementoLogradouro = "";
                            //string[] complemento = new string[6];

                            complementoLogradouro = DadosViabilidade.TiraAcento(DadosEntrada.complementoLogradouro, "Complemento").Trim();

                            string[] palavras = complementoLogradouro.Split(' ');
                            string linha = "";
                            int aaa = 0;
                            foreach (string pLinhaComplemento in palavras)
                            {
                                string pLinhaTratada = DadosViabilidade.TiraAcento(pLinhaComplemento, "Complemento").Trim();
                                //pLinhaComplemento = DadosViabilidade.TiraAcento(pLinhaComplemento);
                                if (pLinhaTratada.Length > 19)
                                {
                                    throw new Exception("Erro palavra passa de vinte carateres");
                                }
                                if (linha.Length + pLinhaTratada.Length > 19)
                                {
                                    comp = new WsServices01RFB.complementoLogradouro();
                                    comp.tipoComplementoLogradouro = "";
                                    comp.descricaoComplementoLogradouro = linha;
                                    complemento.SetValue(comp, aaa);
                                    aaa += 1;
                                    linha = "";
                                }
                                if (pLinhaTratada != "")
                                    linha += " " + pLinhaTratada;
                            }

                            if (linha != "")
                            {
                                comp = new WsServices01RFB.complementoLogradouro();
                                comp.tipoComplementoLogradouro = "";
                                comp.descricaoComplementoLogradouro = linha.Trim();
                                complemento.SetValue(comp, aaa);
                                aaa += 1;
                                linha = "";
                            }
                            Request.complementoLogradouro = complemento;

                        }
                        //if (DadosEntrada.complementoLogradouro.Length > 0)
                        //{
                        //    WsServices01RFB.complementoLogradouro comp = new WsServices01RFB.complementoLogradouro();
                        //    //string[] complemento = new string[6];
                        //    WsServices01RFB.complementoLogradouro[] complemento = new WsServices01RFB.complementoLogradouro[6];
                        //    for (int a = 0; a <= 5; a++)
                        //    {
                        //        comp = new WsServices01RFB.complementoLogradouro();
                        //        string linha = DadosViabilidade.getLinhaComplemento(DadosEntrada.complementoLogradouro, a);
                        //        if (linha.Length > 0)
                        //        {
                        //            comp.tipoComplementoLogradouro = "";
                        //            comp.descricaoComplementoLogradouro = linha;
                        //            complemento.SetValue(comp, a);
                        //        }
                        //    }
                        //    Request.complementoLogradouro = complemento;
                        //}
                    }

                    //Ate aqui campo de endereço
                }

                Request.areaEstabelecimento = DadosEntrada.areaEstabelecimento.ToString() + "00";
                Request.areaImovel = DadosEntrada.areaImovel.ToString() + "00";



                if (dv.isEvento(Request.codEventosViabilidade, "101, 102, 106, 244"))
                {
                    Request.objetoSocial = DadosViabilidade.TiraAcento(DadosEntrada.objetoSocial.ToUpper());
                    if (Request.objetoSocial.Length > 7000)
                    {
                        Request.objetoSocial = Request.objetoSocial.Substring(0, 6999);
                    }
                    //Request.objetoSocial = DadosViabilidade.TiraAcento(DadosEntrada.objetoSocial);
                    //Request.objetoSocial = DadosViabilidade.TiraAcento(DadosEntrada.objetoSocial);
                    //Request.objetoSocial = DadosViabilidade.TiraAcento(DadosEntrada.objetoSocial);

                    if (DadosEntrada.cnaePrincipal != "")
                    {
                        WsServices01RFB.Cnae CnaePrincipal = new WsServices01RFB.Cnae();
                        CnaePrincipal.Codigo = DadosEntrada.cnaePrincipal;
                        Request.cnaePrincipal = CnaePrincipal;
                    }

                    /*
                        Preenche Cnae Secundaria
                     */
                    if (DadosEntrada.cnaeSecundaria != null && DadosEntrada.cnaeSecundaria.Length > 0)
                    {
                        //WsServices01RFB.Cnae[] CnaeSecudaria = new WsServices01RFB.Cnae[DadosEntrada.cnaeSecundaria.Length];
                        WsServices01RFB.Cnae[] CnaeSecudaria = new WsServices01RFB.Cnae[99];
                        int i = 0;
                        foreach (string cnaeSecundaria in DadosEntrada.cnaeSecundaria)
                        {
                            //Aqui e somente para enviar 99 cnae paara a RBF ja que eles somente aceitam 99
                            if (i == 99)
                                break;
                            if (cnaeSecundaria != null && cnaeSecundaria != "")
                            {
                                WsServices01RFB.Cnae cnae = new WsServices01RFB.Cnae();
                                cnae.Codigo = cnaeSecundaria.Trim();
                                CnaeSecudaria.SetValue(cnae, i);
                                i++;
                            }
                        }
                        Request.cnaeSecundaria = CnaeSecudaria;
                    }
                }
                /*
                    Preenche complemento
                 */

                if (Request.indicadorCnpjNomeEmpresarial != "S" && dv.isEvento(Request.codEventosViabilidade, "101, 106, 220, 102, 210"))
                {
                    /*
                     * Retira carateres especiais e retira ME EPP
                     */
                    Request.nomeEmpresarial = GlobalV1.RetiraTipoEnquadramento(DadosViabilidade.TiraAcentoNomeEmpresarial(DadosEntrada.nomeEmpresarial.ToUpper(), Request.naturezaJuridica)).Trim();



                    /*
                        Para colocar o ERELLI quando nao tiver no nome
                     */
                    if (Request.naturezaJuridica == "2305" || Request.naturezaJuridica == "2313")
                    {
                        if (Request.nomeEmpresarial.IndexOf(" EIRELI") == -1)
                        {
                            Request.nomeEmpresarial = Request.nomeEmpresarial + " EIRELI";

                        }
                    }

                    if (Request.naturezaJuridica != "2305" && Request.naturezaJuridica != "2313")
                    {
                        Request.nomeEmpresarial = Request.nomeEmpresarial.Replace(" EIRELI", "");
                    }

                    if (Request.nomeEmpresarial.IndexOf(NomeNoCnpj) >= 0)
                    {
                        throw new Exception("Verificar o Nome " + Request.nomeEmpresarial + " e um nome não certo para enviar.");
                    }
                }

                if (dv.isEvento(Request.codEventosViabilidade, "101, 102, 106, 248"))
                {
                    string[] tipodeUnidade = DadosEntrada.tipoUnidade.Split('-');

                    Request.tipoUnidade = tipodeUnidade;

                    /*
                        caso a alteração seja de Tipo de Unidade e seja 00
                     * coloco o evento 249 
                     */
                    //if (Request.tipoUnidade == "00" && !dv.isEvento(Request.codEventosViabilidade, "249") && dv.isEvento(Request.codEventosViabilidade, "248"))
                    if (dv.isTipoUnidade(Request.tipoUnidade, "00") && !dv.isEvento(Request.codEventosViabilidade, "249") && dv.isEvento(Request.codEventosViabilidade, "248"))
                    {

                        string[] codEventosViabilidade2 = new string[Request.codEventosViabilidade.Length + 1];
                        int even2 = 0;
                        foreach (string codEventos in DadosEntrada.codEventosViabilidade)
                        {
                            codEventosViabilidade2[even2] = codEventos;
                            even2++;
                        }
                        codEventosViabilidade2[even2] = "249";
                        Request.codEventosViabilidade = codEventosViabilidade2;
                    }

                }


                if (dv.isEvento(Request.codEventosViabilidade, "101, 102, 249"))
                {
                    //Somente envio Forma de atuação se o tipo de unidade for 00
                    if (Request.tipoUnidade != null && dv.isTipoUnidade(Request.tipoUnidade, "00"))
                    {
                        if (DadosEntrada.formaAtuacao != null && DadosEntrada.formaAtuacao.Length > 0)
                        {

                            Request.formaAtuacao = DadosEntrada.formaAtuacao;

                        }
                        else
                        {
                            if (DadosEntrada.formaAtuacaoStr != "")
                            {

                                string[] formaAtuacaoStr = DadosEntrada.formaAtuacaoStr.Split('-');

                                Request.formaAtuacao = formaAtuacaoStr;

                            }
                        }
                    }

                    if (Request.tipoUnidade == null)
                    {
                        if (DadosEntrada.formaAtuacao != null && DadosEntrada.formaAtuacao.Length > 0)
                        {

                            Request.formaAtuacao = DadosEntrada.formaAtuacao;

                        }
                        else
                        {
                            if (DadosEntrada.formaAtuacaoStr != "")
                            {

                                string[] formaAtuacaoStr = DadosEntrada.formaAtuacaoStr.Split('-');

                                Request.formaAtuacao = formaAtuacaoStr;

                            }
                        }
                    }
                }

                Request.inscricaoImovel = DadosEntrada.inscricaoImovel;
                /*
                   Somente envia socios quando for diferente de empresario e ERELI
                */
                //if (Request.naturezaJuridica != "2135" && Request.naturezaJuridica != "2305" && Request.naturezaJuridica != "2313")
                //{
                if (dv.isEvento(Request.codEventosViabilidade, "101"))
                {
                    //Esse i e porque a RFB esta retornando o siguinte para essa natureza Juridica de cartorios, mudado dia 06/09/2019, por isso vamos somente enviar
                    // quando for Junta
                    //01-G3-028-Natureza Jurídica não permite a informação de Quadro Societário
                    if (DadosEntrada.tipoOrgaoRegistro == "1")
                    {
                        #region Explicação do if
                        /*
                        0005395: [Jucesc - Viabilidade]_ Não enviado para RFB.
                        Descrição	Usuário gerou a viabilidade SCP1901539769, o pedido trata da abertura de uma empresa de natureza jurídica 217-8 (Estabelecimento, no Brasil, de Sociedade Estrangeira).
                        Porém, o pedido não foi enviado para a Receita Federal:
                        "Não enviado por: 01-G3-028-Natureza Jurídica não permite a informação de Quadro Societário".

                        Usuário entrou em contato com a RFB e informaram que para essa natureza jurídica não tem quadro societário, mas a viabilidade solicita.

                        Poderiam verificar o que pode ser corrigido, pois usuário precisa gerar o DBE com urgência.*/
                        #endregion
                        if (Request.naturezaJuridica != "2178")
                        {
                            if (DadosEntrada.cpfSocioPf != null)
                            {
                                string[] cpfSocioPf = new string[DadosEntrada.cpfSocioPf.Length];
                                string[] nomeSocioPf = new string[DadosEntrada.cpfSocioPf.Length];
                                int pf = 0;
                                foreach (string cpfSocio in DadosEntrada.cpfSocioPf)
                                {
                                    if (cpfSocio != null && cpfSocio.Length == 11)
                                    {
                                        cpfSocioPf[pf] = cpfSocio;
                                        nomeSocioPf[pf] = DadosEntrada.nomeSocioPf[pf];
                                        pf++;

                                    }
                                }

                                Request.cpfSocioPf = cpfSocioPf;
                                Request.nomeSocioPf = nomeSocioPf;
                            }
                        }
                    }
                }
                //}

                Request.tipoInscricao = DadosEntrada.tipoInscricao;




                using (WsServices01RFB.WS01Service c = new WsServices01RFB.WS01Service())
                {

                    X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());
                    c.ClientCertificates.Add(cert);

                    c.Url = ConfigurationManager.AppSettings.Get("UrlWs01").ToString();
                    try
                    {
                        System.Net.ServicePointManager.Expect100Continue = false;
                        ServicePointManager.DefaultConnectionLimit = 100;
                        ServicePointManager.MaxServicePointIdleTime = 5000;

                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                        request.KeepAlive = false;
                        request.ClientCertificates.Add(cert);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream stream = response.GetResponseStream();
                        StreamReader sr = new StreamReader(stream);
                        string resp = sr.ReadToEnd();

                        c.Timeout = 12000;

                        result.XmlRFB = GlobalV1.CreateXML(Request);

                        pResult = c.processarViabilidade(Request);



                        cert.Reset();
                    }
                    catch (Exception ex)
                    {
                        cert.Reset();
                        result.XmlResponseRFB = GlobalV1.CreateXML(pResult);
                        string detail = "";
                        try
                        {
                            detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                        }
                        catch { }
                        //string XmlDados = GlobalV1.CreateXML(dadosenvio);
                        //string xmlDadosEntrada = GlobalV1.CreateXML(dados);
                        result.status = "NOK";
                        result.descricao = ex.Message + " detail " + detail;

                        using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                        {
                            e35.t73307_arquivo_RFB = GlobalV1.CreateXML(Request);
                            e35.t73307_erro = GlobalV1.CreateXML(pResult);
                            e35.t73307_arquivo_regin = GlobalV1.CreateXML(DadosEntrada);
                            e35.t73307_ide_solicitacao = "WS01";
                            e35.t73307_rec_solicitacao = DadosEntrada.protocoloViabilidade;
                            e35.UpdateS01();
                        }


                        return result;
                    }

                    result.XmlResponseRFB = GlobalV1.CreateXML(pResult);

                    if (pResult.retornoWSRedesim.statusEnvio == "OK")
                    {
                        result.status = pResult.retornoWSRedesim.statusEnvio;
                        result.descricao = pResult.retornoWSRedesim.descricaoRetorno;// pResult.retornoWSRedesim.descricaoRetorno;

                        if (DadosEntrada.codStatusViabilidade != "99")
                        {
                            //Somente devolvo se for Balcao Unico isso porque a JUCERJA grava numa tabela diferenciada
                            //if (Request.protocoloViabilidade.Substring(2, 1) == "B")
                            //{
                            result.XmlRFB = GlobalV1.CreateXML(Request);
                            //}
                            using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                            {
                                e35.t73307_arquivo_RFB = GlobalV1.CreateXML(Request);
                                e35.t73307_erro = "";// GlobalV1.CreateXML(pResult);
                                                     //e35.t73307_arquivo_regin = GlobalV1.CreateXML(DadosEntrada);
                                e35.t73307_ide_solicitacao = "WS01Ok";
                                e35.t73307_rec_solicitacao = DadosEntrada.protocoloViabilidade;
                                //pAREI DE GRAVAR oK, ja esta certinho o codigo, caso precise coloco de novo depois
                                //e35.UpdateS01();
                            }
                        }
                    }
                    else
                    {
                        if (pResult.retornoWSRedesim.mensagemRetorno != null)
                        {
                            result.status = pResult.retornoWSRedesim.statusEnvio;
                            result.descricao += "RFB: ";
                            //incompRegistroIntegradorEstadual[] codResul = new incompRegistroIntegradorEstadual[99];


                            int tamanhoArray = 0;
                            foreach (WsServices01RFB.mensagemRetorno RetornoNOK in pResult.retornoWSRedesim.mensagemRetorno)
                            {
                                if (RetornoNOK.descricaoRetorno != "")
                                {
                                    tamanhoArray += 1;
                                }
                            }
                            incompRegistroIntegradorEstadual[] codResul = new incompRegistroIntegradorEstadual[tamanhoArray];
                            int ss = 0;
                            foreach (WsServices01RFB.mensagemRetorno RetornoNOK in pResult.retornoWSRedesim.mensagemRetorno)
                            {
                                if (RetornoNOK.descricaoRetorno != "")
                                {
                                    incompRegistroIntegradorEstadual cod = new incompRegistroIntegradorEstadual();
                                    cod.codigo = RetornoNOK.codigoRetorno;
                                    cod.mensagem = RetornoNOK.descricaoRetorno;
                                    result.descricao += " - " + RetornoNOK.descricaoRetorno;
                                    codResul.SetValue(cod, ss);
                                }
                                ss += 1;
                            }

                            result.codretorno = codResul;

                            //return result;
                        }
                        else
                        {
                            result.status = pResult.retornoWSRedesim.statusEnvio;
                            result.descricao = "RFB: " + pResult.retornoWSRedesim.descricaoRetorno;
                        }
                    }
                }

                if (result.status != "OK")
                {
                    using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    {
                        e35.t73307_arquivo_RFB = GlobalV1.CreateXML(Request);
                        e35.t73307_erro = GlobalV1.CreateXML(pResult);
                        e35.t73307_arquivo_regin = GlobalV1.CreateXML(DadosEntrada);
                        e35.t73307_ide_solicitacao = "WS01";
                        e35.t73307_rec_solicitacao = DadosEntrada.protocoloViabilidade;
                        e35.UpdateS01();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {

                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = GlobalV1.CreateXML(Request);
                    e35.t73307_erro = GlobalV1.CreateXML(pResult);
                    e35.t73307_arquivo_regin = GlobalV1.CreateXML(DadosEntrada);
                    e35.t73307_ide_solicitacao = "WS01";
                    e35.t73307_rec_solicitacao = DadosEntrada.protocoloViabilidade;
                    e35.UpdateS01();
                }

                result.status = "NOK";
                result.descricao = "Erro fora da rotina:" + ex.Message;
                return result;
            }

        }

        public WsServices01RFB.complementoLogradouro[] processaComplemento(string complementoLogradouroRFB, string complementoLogradouro, string Tipo)
        {
            if (complementoLogradouroRFB == "" && complementoLogradouro != "")
            {
                throw new Exception("Processa o normal porque da RFB esta vazio");
            }
            if (complementoLogradouroRFB.Length > 0 && Tipo == "RFB")
            {
                //if (complementoLogradouroRFB.IndexOf(';') < 0 && complementoLogradouroRFB.IndexOf(':') < 0)
                //{
                //    throw new Exception("Nao pode ser de RFB ja que nao tem ");
                //}

                WsServices01RFB.complementoLogradouro comp = new WsServices01RFB.complementoLogradouro();
                //string[] complemento = new string[6];
                WsServices01RFB.complementoLogradouro[] complemento = new WsServices01RFB.complementoLogradouro[6];
                string[] linha = complementoLogradouroRFB.Split(';');
                int aaa = 0;
                foreach (string pLinhaComplemento in linha)
                {
                    comp = new WsServices01RFB.complementoLogradouro();
                    string[] separaCom = pLinhaComplemento.Split(':');

                    comp.tipoComplementoLogradouro = separaCom[0].ToString().Trim();
                    string descricao = "";
                    int occorencia = 0;
                    foreach (string pDescricao in separaCom)
                    {
                        if (occorencia > 0)
                            descricao += pDescricao;
                        occorencia++;
                    }
                    comp.descricaoComplementoLogradouro = DadosViabilidade.TiraAcento(descricao, "Complemento");
                    if (comp.descricaoComplementoLogradouro.Length > 20)
                    {
                        throw new Exception("Erro passa de vinte");

                    }
                    if (comp.tipoComplementoLogradouro.Length > 6)
                    {
                        throw new Exception("Erro passa de seis");

                    }
                    complemento.SetValue(comp, aaa);
                    aaa = aaa + 1;
                }
                //Request.complementoLogradouro = complemento;

                return complemento;
            }

            throw new Exception("Vazio");


        }

        #endregion

        #region Serviço 02 RFB
        [WebMethod]
        public RetornoS01 ServiceWs02(DadosViabilidade DadosEntrada, string codStatusAtual)
        {

            RetornoS01 result = new RetornoS01();
            WsServices02RFB.alterarStatusViabilidadeRequest Request = new WsServices02RFB.alterarStatusViabilidadeRequest();
            WsServices02RFB.alterarStatusViabilidadeResponse pResult = new WsServices02RFB.alterarStatusViabilidadeResponse();



            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                WsServices02RFB.alterarStatusViabilidadeRequest R = new WsServices02RFB.alterarStatusViabilidadeRequest();
                DadosViabilidade dv = new DadosViabilidade();

                //Todos devem ser preenchidos
                Request.codServico = "S02";


                Request.codStatusViabilidade = DadosEntrada.codStatusViabilidade;
                if (codStatusAtual == "01" && Request.codStatusViabilidade == "02")
                {
                    result.status = "NOK";
                    result.descricao = "Viabilidade aprovada com Status de reprovada";
                    return result;
                }

                if (Request.codStatusViabilidade == "02")
                {
                    result.status = "OK";
                    result.descricao = "";
                    return result;
                }
                Request.protocoloViabilidade = DadosEntrada.protocoloViabilidade;
                Request.dataFimAnaliseViabilidadeEndereco = DadosEntrada.dataFimAnaliseViabilidadeEndereco;
                Request.dataFimAnaliseViabilidadeNome = DadosEntrada.dataFimAnaliseViabilidadeNome;
                Request.dataInicioAnaliseViabilidade = DadosEntrada.dataInicioAnaliseViabilidade;
                Request.dataValidadeViabilidade = DadosEntrada.dataValidadeViabilidade;
                Request.resultadoViabilidadeEndereco = DadosEntrada.resultadoViabilidadeEndereco;
                Request.resultadoViabilidadeNome = DadosEntrada.resultadoViabilidadeNome;

                using (WsServices02RFB.WS02Service c = new WsServices02RFB.WS02Service())
                {

                    X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());
                    c.ClientCertificates.Add(cert);

                    c.Url = ConfigurationManager.AppSettings.Get("UrlWs02").ToString();
                    try
                    {
                        //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                        //request.ClientCertificates.Add(cert);
                        //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        //Stream stream = response.GetResponseStream();
                        //StreamReader sr = new StreamReader(stream);
                        //string resp = sr.ReadToEnd();
                        c.Timeout = 12000;

                        pResult = c.alterarStatusViabilidade(Request);
                        cert.Reset();
                    }
                    catch (Exception ex)
                    {
                        cert.Reset();
                        string detail = "";
                        try
                        {
                            detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                        }
                        catch { }
                        //string XmlDados = GlobalV1.CreateXML(dadosenvio);
                        //string xmlDadosEntrada = GlobalV1.CreateXML(dados);
                        result.status = "NOK";
                        result.descricao = ex.Message + " detail " + detail;

                        using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                        {
                            e35.t73307_arquivo_RFB = GlobalV1.CreateXML(Request);
                            e35.t73307_erro = GlobalV1.CreateXML(pResult);
                            e35.t73307_arquivo_regin = GlobalV1.CreateXML(DadosEntrada);
                            e35.t73307_ide_solicitacao = "WS02";
                            e35.t73307_rec_solicitacao = DadosEntrada.protocoloViabilidade;
                            e35.UpdateS01();
                        }


                        return result;
                    }

                    if (pResult.retornoWSRedesim.statusEnvio == "OK")
                    {
                        result.status = pResult.retornoWSRedesim.statusEnvio;
                        result.descricao = pResult.retornoWSRedesim.descricaoRetorno;// pResult.retornoWSRedesim.descricaoRetorno;

                        if (DadosEntrada.codStatusViabilidade != "99")
                        {
                            using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                            {
                                e35.t73307_arquivo_RFB = GlobalV1.CreateXML(Request);
                                e35.t73307_erro = "";// GlobalV1.CreateXML(pResult);
                                e35.t73307_arquivo_regin = "";// GlobalV1.CreateXML(DadosEntrada);
                                e35.t73307_ide_solicitacao = "WS02Ok";
                                e35.t73307_rec_solicitacao = DadosEntrada.protocoloViabilidade;
                                e35.UpdateS01();
                            }
                        }
                    }
                    else
                    {
                        if (pResult.retornoWSRedesim.mensagemRetorno != null)
                        {
                            result.status = pResult.retornoWSRedesim.statusEnvio;
                            result.descricao += "RFB: ";
                            //incompRegistroIntegradorEstadual[] codResul = new incompRegistroIntegradorEstadual[99];


                            int tamanhoArray = 0;
                            foreach (WsServices02RFB.mensagemRetorno RetornoNOK in pResult.retornoWSRedesim.mensagemRetorno)
                            {
                                if (RetornoNOK.descricaoRetorno != "")
                                {
                                    tamanhoArray += 1;
                                }
                            }
                            incompRegistroIntegradorEstadual[] codResul = new incompRegistroIntegradorEstadual[tamanhoArray];
                            int ss = 0;
                            foreach (WsServices02RFB.mensagemRetorno RetornoNOK in pResult.retornoWSRedesim.mensagemRetorno)
                            {
                                if (RetornoNOK.descricaoRetorno != "")
                                {
                                    incompRegistroIntegradorEstadual cod = new incompRegistroIntegradorEstadual();
                                    cod.codigo = RetornoNOK.codigoRetorno;
                                    cod.mensagem = RetornoNOK.descricaoRetorno;
                                    result.descricao += " - " + RetornoNOK.descricaoRetorno;
                                    codResul.SetValue(cod, ss);
                                }
                                ss += 1;
                            }

                            result.codretorno = codResul;

                            //return result;
                        }
                        else
                        {
                            result.status = pResult.retornoWSRedesim.statusEnvio;
                            result.descricao = "RFB: " + pResult.retornoWSRedesim.descricaoRetorno;
                        }
                    }
                }

                if (result.status != "OK")
                {
                    using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    {
                        e35.t73307_arquivo_RFB = GlobalV1.CreateXML(Request);
                        e35.t73307_erro = GlobalV1.CreateXML(pResult);
                        e35.t73307_arquivo_regin = GlobalV1.CreateXML(DadosEntrada);
                        e35.t73307_ide_solicitacao = "WS02";
                        e35.t73307_rec_solicitacao = DadosEntrada.protocoloViabilidade;
                        e35.UpdateS01();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {

                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = GlobalV1.CreateXML(Request);
                    e35.t73307_erro = GlobalV1.CreateXML(pResult);
                    e35.t73307_arquivo_regin = GlobalV1.CreateXML(DadosEntrada);
                    e35.t73307_ide_solicitacao = "WS01";
                    e35.t73307_rec_solicitacao = DadosEntrada.protocoloViabilidade;
                    e35.UpdateS01();
                }

                result.status = "NOK";
                result.descricao = "Erro fora da rotina:" + ex.Message;
                return result;
            }

        }



        #endregion

        #region Serviço 04 RFB
        /// <summary>
        /// 0  - Retorno OK
        /// 93 – Transação não efetuada – Tente Novamente
        /// 99 - CNPJ não fecha DV ou não encontrado na Base
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        [WebMethod]
        public Retorno ServiceWs04(DadosWs04 dados)
        {
            Retorno result = new Retorno();
            try
            {

                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                string ResponseRFB = "";

                WsServices04RFB.ws04Request dadosenvio = new WsServices04RFB.ws04Request();
                WsServices04RFB.ws04Response pResult = new WsServices04RFB.ws04Response();

                //WsServices04RFB.mensagem mensagem = new WsServices04RFB.mensagem();
                using (WsServices04RFB.ws04 c = new WsServices04RFB.ws04())
                {

                    //X509Certificate2 cert = new X509Certificate2(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());
                    //c.ClientCredentials.ClientCertificate.Certificate = cert;

                    X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                    c.ClientCertificates.Add(cert);


                    //System.ServiceModel.EndpointAddress endereco = new System.ServiceModel.EndpointAddress(ConfigurationManager.AppSettings.Get("UrlWs04").ToString());
                    //c.Endpoint.Address = endereco;

                    //c.ClientCredentials.ServiceCertificate.DefaultCertificate = cert;
                    //c.ClientCredentials.ClientCertificate.Certificate = cert;


                    dadosenvio.codServico = "S04";
                    //dadosenvio.protocolo = dados.protocolo;
                    //dadosenvio.protocoloOcorrencia = dados.protocoloOcorrencia;
                    dadosenvio.resultadoValidacao = dados.resultadoValidacao;
                    dadosenvio.versao = "100000";
                    dadosenvio.identificacaoSolicitacao = dados.identificacaoSolicitacao;
                    dadosenvio.reciboSolicitacao = dados.reciboSolicitacao;

                    #region Mensagem para enviar caso indeferido
                    /*
                       Aqui e se vem preenchido o arrays de menssagem                 
                    */
                    if (dados.mensagem != null && dados.mensagem.Length > 0)
                    {
                        WsServices04RFB.mensagem[] inconpa = new WsServices04RFB.mensagem[20];

                        foreach (mensagemInformativa _dadosWs04 in dados.mensagem)
                        {
                            if (_dadosWs04 != null && _dadosWs04.nomeOrgaoResponsavel != "" && _dadosWs04.texto != "")
                            {
                                int i = 0;
                                WsServices04RFB.mensagem inconpa2 = new WsServices04RFB.mensagem();
                                inconpa2.nomeOrgaoResponsavel = _dadosWs04.nomeOrgaoResponsavel;
                                inconpa2.texto = _dadosWs04.texto;
                                inconpa.SetValue(inconpa2, i);
                                i++;
                            }
                        }

                        dadosenvio.mensagem = inconpa;
                    }

                    /*
                       Aqui e se vem uma unica linha de menssagem
                    */
                    if (dados.mensagemUnica.nomeOrgaoResponsavel != "" && dados.mensagemUnica.texto != "")
                    {
                        WsServices04RFB.mensagem[] inconpa = new WsServices04RFB.mensagem[20];

                        WsServices04RFB.mensagem inconpa2 = new WsServices04RFB.mensagem();
                        inconpa2.nomeOrgaoResponsavel = dados.mensagemUnica.nomeOrgaoResponsavel;
                        inconpa2.texto = dados.mensagemUnica.texto;
                        inconpa.SetValue(inconpa2, 0);
                        dadosenvio.mensagem = inconpa;
                    }

                    #endregion

                    c.Url = ConfigurationManager.AppSettings.Get("UrlWs04").ToString();

                    //c.Url = ConfigurationManager.AppSettings.Get("UrlWs04").ToString();
                    //c.Endpoint.Address = "";
                    try
                    {
                        //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                        //request.ClientCertificates.Add(cert);
                        //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        //Stream stream = response.GetResponseStream();
                        //StreamReader sr = new StreamReader(stream);
                        //string resp = sr.ReadToEnd();

                        pResult = c.validacaoCadastral(dadosenvio);

                        cert.Reset();
                    }
                    catch (Exception ex)
                    {
                        cert.Reset();
                        string detail = "";
                        try
                        {
                            detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                        }
                        catch { }
                        result.codretorno = "99";
                        result.status = "NOK";
                        result.descricao = "Comunicação com a Receita Federal do Brasil fora do ar, tente novamente mais tarde " + ex.Message + " SoapException " + detail.ToString();

                        using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                        {
                            e35.t73307_arquivo_RFB = "";
                            e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                            e35.t73307_ide_solicitacao = dados.identificacaoSolicitacao;
                            e35.t73307_rec_solicitacao = "ws04";
                            e35.Update();
                        }

                        return result;
                    }


                    ResponseRFB = GlobalV1.CreateXML(pResult);

                    if (pResult.statusEnvio == "OK")
                    {
                        result.codretorno = "00";
                        result.status = pResult.statusEnvio;
                        //result.descricao = pResult.mensagemRetorno[0].descricaoRetorno;
                        result.XmlDBE = ResponseRFB;
                    }
                    else
                    {


                        //ServiceReginRFB regin35 = new ServiceReginRFB();

                        //Retorno resulRegin35 = new Retorno();

                        //resulRegin35 = regin35.ServiceWs35Soarquivo(dadosenvio.identificacaoSolicitacao, dadosenvio.reciboSolicitacao);

                        //if (resulRegin35.status == "OK")
                        //{
                        //    if (resulRegin35.codretorno != "57")
                        //    {
                        //        result.codretorno = "00";
                        //        result.status = "OK";
                        //        return result;
                        //        //result.descricao = pResult.mensagemRetorno[0].descricaoRetorno;
                        //        //result.XmlDBE = ResponseRFB;
                        //    }
                        //}

                        result.codretorno = "99";
                        result.descricao = "RFB: Erro no retorno da aplicação do S04" + pResult.mensagemRetorno[0].descricaoRetorno;

                        result.status = pResult.statusEnvio;

                        if (pResult.mensagemRetorno[0].codigoRetorno != null && pResult.mensagemRetorno[0].codigoRetorno != "")
                        {
                            result.codretorno = pResult.mensagemRetorno[0].codigoRetorno;
                            result.descricao = "RFB: " + pResult.mensagemRetorno[0].codigoRetorno + " - " + pResult.mensagemRetorno[0].descricaoRetorno;

                            if (pResult.mensagemRetorno[0].descricaoRetorno == "CHAVE DE ACESSO NAO ESTA EM AGUARDANDO SERVIÇO S04.")
                            {
                                result.codretorno = "05";
                                result.status = "OK";
                                //result.descricao = pResult.mensagemRetorno[0].descricaoRetorno;
                                result.XmlDBE = ResponseRFB;
                            }

                            if (pResult.mensagemRetorno[0].descricaoRetorno == "UF INTEGRADOR INVALIDA(DESTINO).")
                            {
                                result.codretorno = "06";
                                result.status = "OK";
                                //result.descricao = pResult.mensagemRetorno[0].descricaoRetorno;
                                result.XmlDBE = ResponseRFB;
                            }

                        }


                    }

                    return result;
                }
            }

            catch (Exception ex)
            {
                string detail = "";
                try
                {
                    detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                }
                catch { }
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.Message + " StackTrace " + ex.StackTrace + " SoapException " + detail.ToString();

                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = "";
                    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    e35.t73307_ide_solicitacao = dados.identificacaoSolicitacao;
                    e35.t73307_rec_solicitacao = "ws04";
                    e35.Update();
                }
                return result;
            }

        }
        #endregion

        #region Envia procedimento de viabildiade mudança status
        [WebMethod]
        public void ProcessaEnviaViabilidadeMudancaStatusRFB(string NroViabilidade)
        {
            GlobalV1 c = new GlobalV1();

            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            DataTable toReturn = c.EnviaViabiliadesServico02RFB(NroViabilidade);
            //DataTable DtTipoLogra = GlobalV1.BuscarTipoLogradouro();
            ServiceReginRFB wsRegin = new ServiceReginRFB();
            for (int a = 0; a < toReturn.Rows.Count; a++)
            {
                string pProtocolo = toReturn.Rows[a]["ProtocoloViab"].ToString().Trim();
                DataTable DtViabilidade = GlobalV1.BuscaDadosViabilidade(pProtocolo);
                if (DtViabilidade.Rows.Count > 0)
                {
                    DataSet dsTabelas = new DataSet();
                    DataTable DadosVia = new DataTable("DadosViabilidade");

                    dsTabelas.Tables.Add(DtViabilidade.Copy());

                    WsServicesReginRFB.RetornoS01 result = new WsServicesReginRFB.RetornoS01();
                    WsServicesReginRFB.ServiceReginRFB envia = new WsServicesReginRFB.ServiceReginRFB();
                    envia.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                    result = envia.EnviaWsReceitaFederalServico02(pProtocolo, dsTabelas);

                    //RetornoS01 result = EnviaWsReceitaFederalServico02(pProtocolo, dsTabelas);
                    GlobalV1 cc = new GlobalV1();

                    if (DtViabilidade.Rows[0]["codStatusViabilidade"].ToString() != "99")
                    {
                        if (result.status == "OK")
                        {
                            cc.UpdateViabilidadeEnviadaReceitaWs02(pProtocolo, DtViabilidade.Rows[0]["codStatusViabilidade"].ToString());

                        }
                        else
                        {
                            if (result.codretorno != null && (result.codretorno[0].codigo == "23"))
                            {
                                cc.UpdateViabilidadeEnviadaReceitaWs02(pProtocolo, DtViabilidade.Rows[0]["codStatusViabilidade"].ToString());
                            }
                            else
                            {
                                cc.UpdateViabilidadeNAOOKReceitaWs02(pProtocolo, GlobalV1.CreateXML(result));
                            }
                        }

                    }
                }
            }
        }


        [WebMethod]
        public RetornoS01 EnviaWsReceitaFederalServico02(string pProtocolo, DataSet dsTabelas)
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            DataRow DadosViabi = dsTabelas.Tables["DadosViabilidade"].Rows[0];
            return EnviaWsReceitaFederalServico02(pProtocolo, DadosViabi);
        }

        public static string TrimAll(string s)
        {
            s = s.Trim();
            while (s.IndexOf("\r\n\r\n") != -1)
                s = s.Replace("\r\n\r\n", "\r\n");


            while (s.IndexOf("  ") != -1)
                s = s.Replace("  ", " ");
            return s;


        }

        public RetornoS01 EnviaWsReceitaFederalServico02(string pProtocolo, DataRow DadosViabi)
        {
            RetornoS01 result = new RetornoS01();
            using (ServiceReginRFB c = new ServiceReginRFB())
            {
                DadosViabilidade dados = new DadosViabilidade();

                dados.areaEstabelecimento = DadosViabi["AreaEstablecimento"].ToString() == "" ? 0 : int.Parse(DadosViabi["AreaEstablecimento"].ToString());
                dados.areaImovel = DadosViabi["AreaImovel"].ToString() == "" ? 0 : int.Parse(DadosViabi["AreaImovel"].ToString());
                dados.bairro = DadosViabi["bairro"].ToString();
                dados.cep = DadosViabi["cep"].ToString();
                dados.cnaePrincipal = DadosViabi["cnaePrincipal"].ToString();

                dados.cnpj = DadosViabi["cnpj"].ToString().Trim();
                //dados.cnpjFilial = DadosViabi["cnpjFilial"].ToString();
                dados.CNPJMatriz = DadosViabi["CNPJMatriz"].ToString().Trim();
                dados.nire = DadosViabi["Nire"].ToString();
                dados.nireMatriz = DadosViabi["nireMatriz"].ToString();



                dados.codMunicipio = DadosViabi["codMunicipio"].ToString();
                dados.codStatusViabilidade = DadosViabi["codStatusViabilidade"].ToString();
                dados.dataFimAnaliseViabilidadeEndereco = DadosViabi["DataFimViabilidadeEndereco"].ToString();

                dados.dataFimAnaliseViabilidadeNome = DadosViabi["dataFimAnaliseViabilidadeNome"].ToString();
                dados.dataInicioAnaliseViabilidade = DadosViabi["dataInicioAnaliseViabilidade"].ToString();
                dados.dataValidadeViabilidade = DadosViabi["dataValidadeViabilidade"].ToString();
                dados.nomeEmpresarial = DadosViabi["nomeEmpresarial"].ToString();
                dados.numLogradouro = DadosViabi["numLogradouro"].ToString();
                dados.protocoloViabilidade = pProtocolo;
                dados.resultadoViabilidadeEndereco = DadosViabi["resultadoViabilidadeEndereco"].ToString();
                dados.resultadoViabilidadeNome = DadosViabi["resultadoViabilidadeNome"].ToString();
                string codStatusAtual = "";
                try
                {
                    codStatusAtual = DadosViabi["codStatusAtual"].ToString();
                }
                catch
                {
                }
                result = ServiceWs02(dados, codStatusAtual);

                if (result.codretorno != null)
                {
                    foreach (incompRegistroIntegradorEstadual cod in result.codretorno)
                    {
                        if (cod.codigo != null && cod.codigo != "" && (cod.codigo == "37" || cod.codigo == "22"))
                        {
                            result.status = "OK";
                        }
                    }
                }
            }
            return result;
        }

        #endregion

        #region Envia procedimento de viabildiade
        [WebMethod]
        public void ProcessaEnviaViabilidadeRFB(string NroViabilidade)
        {
            //DataTable DtViabilidade2 = GlobalV1.BuscaDadosViabilidade(NroViabilidade);

            //try
            //{
            //    string ffff = DtViabilidade2.Rows[0]["indicadorCnpjNomeEmpresarial"].ToString();
            //}
            //catch
            //{
            //    var col = new DataColumn("indicadorCnpjNomeEmpresarial");
            //    col.DefaultValue = "N";
            //    DtViabilidade2.Columns.Add(col);
            //}
            //string fff = DtViabilidade2.Rows[0]["indicadorCnpjNomeEmpresarial"].ToString();

            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            GlobalV1 c = new GlobalV1();

            if (ConfigurationManager.AppSettings["Main.ConnectionString"] == null || ConfigurationManager.AppSettings["Main.ConnectionString"].ToString() == "")
            {
                throw new Exception("Falta paramentro Main.ConnectionString no web.config");
            }

            if (ConfigurationManager.AppSettings["TipoBanco"] == null || ConfigurationManager.AppSettings["TipoBanco"].ToString() == "")
            {
                throw new Exception("Falta paramentro Main.ConnectionString no web.config");
            }

            DataTable toReturn = c.EnviaViabiliadesRFB(NroViabilidade);

            DataTable DtTipoLogra = GlobalV1.BuscarTipoLogradouro();

            ServiceReginRFB wsRegin = new ServiceReginRFB();
            for (int a = 0; a < toReturn.Rows.Count; a++)
            {
                string pProtocolo = "";
                try
                {
                    pProtocolo = toReturn.Rows[a]["ProtocoloViab"].ToString().Trim();
                    DataTable DtViabilidade = GlobalV1.BuscaDadosViabilidade(pProtocolo);
                    if (DtViabilidade.Rows.Count > 0)
                    {


                        DataTable DtCnae = new DataTable();
                        DataTable DtSocio = new DataTable();
                        DataTable DtEvento = new DataTable();
                        DataSet dsTabelas = new DataSet();
                        DataTable DadosVia = new DataTable("DadosViabilidade");

                        //DadosVia.ImportRow(Dt.Rows[0].);
                        //DataTable Dttemp = DadosVia.Copy();

                        DtCnae = GlobalV1.BuscaDadosViabilidadeCnaeSecundaria(pProtocolo);
                        DtSocio = GlobalV1.BuscaDadosViabilidadeQsa(pProtocolo);
                        DtEvento = GlobalV1.BuscaDadosViabilidadeEvento(pProtocolo);

                        dsTabelas.Tables.Add(DtTipoLogra.Copy());
                        dsTabelas.Tables.Add(DtCnae.Copy());
                        dsTabelas.Tables.Add(DtSocio.Copy());
                        dsTabelas.Tables.Add(DtEvento.Copy());
                        dsTabelas.Tables.Add(DtViabilidade.Copy());

                        WsServicesReginRFB.RetornoS01 result = new WsServicesReginRFB.RetornoS01();
                        WsServicesReginRFB.ServiceReginRFB envia = new WsServicesReginRFB.ServiceReginRFB();
                        envia.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                        result = envia.EnviaWsReceitaFederal(pProtocolo, dsTabelas);

                        //RetornoS01 result = EnviaWsReceitaFederal(pProtocolo, dsTabelas);
                        GlobalV1 cc = new GlobalV1();

                        if (DtViabilidade.Rows[0]["codStatusViabilidade"].ToString() != "99")
                        {
                            if (result.status == "OK")
                            {
                                cc.UpdateViabilidadeEnviadaReceita(pProtocolo, DtViabilidade.Rows[0]["codStatusViabilidade"].ToString(), result.XmlRFB, result.XmlResponseRFB);
                                try
                                {
                                    if (DtViabilidade.Columns.Contains("vpv_email_solic"))
                                    {
                                        string tipo = "1"; //Viabilidade deferida
                                        string AssuntoEmail = "Viabilidade Deferida";
                                        if (DtViabilidade.Rows[0]["codStatusViabilidade"].ToString() == "02")
                                        {
                                            tipo = "2";
                                            AssuntoEmail = "Viabilidade Indeferida";
                                        }
                                        dHelperQuery.CriaEmail(pProtocolo, "", "", tipo, AssuntoEmail, "", DtViabilidade.Rows[0]["vpv_email_solic"].ToString());
                                    }
                                }
                                catch { }
                            }
                            else
                            {
                                cc.UpdateViabilidadeNAOOKReceita(pProtocolo, GlobalV1.CreateXML(result), result.XmlRFB, result.XmlResponseRFB);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    GlobalV1 cc = new GlobalV1();
                    string err = "Erro interno: " + ex.StackTrace + " Mes " + ex.Message;
                    cc.UpdateViabilidadeNAOOKReceita(pProtocolo, err);

                }
            }

            ProcessaEnviaViabilidadeMudancaStatusRFB(NroViabilidade);

            //ProcedimentosVarios rr = new ProcedimentosVarios();
            //rr.ProcessaCancelamentoViabilidadeBA("");
        }


        [WebMethod]
        public RetornoS01 EnviaWsReceitaFederal(string pProtocolo, DataSet dsTabelas)
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            DataTable DadosCnae = dsTabelas.Tables["DadosViabilidadeCnae"];
            DataTable DadosQSA = dsTabelas.Tables["DadosViabilidadeQSA"];
            DataTable DadosEvento = dsTabelas.Tables["DadosViabilidadeEvento"];
            DataTable DtTipoLogra = dsTabelas.Tables["TIPODELOGRADOURO"];
            DataRow DadosViabi = dsTabelas.Tables["DadosViabilidade"].Rows[0];
            return EnviaWsReceitaFederal(pProtocolo, DadosViabi, DadosCnae, DadosQSA, DadosEvento, DtTipoLogra);
        }


        public RetornoS01 EnviaWsReceitaFederal(string pProtocolo, DataRow DadosViabi, DataTable DadosCnae, DataTable DadosQSA, DataTable DadosEvento, DataTable DtTipoLogra)
        {
            RetornoS01 result = new RetornoS01();
            using (ServiceReginRFB c = new ServiceReginRFB())
            {
                DadosViabilidade dados = new DadosViabilidade();

                dados.areaEstabelecimento = DadosViabi["AreaEstablecimento"].ToString() == "" ? 0 : int.Parse(DadosViabi["AreaEstablecimento"].ToString());
                dados.areaImovel = DadosViabi["AreaImovel"].ToString() == "" ? 0 : int.Parse(DadosViabi["AreaImovel"].ToString());
                dados.bairro = DadosViabi["bairro"].ToString();
                dados.cep = DadosViabi["cep"].ToString();
                dados.cnaePrincipal = DadosViabi["cnaePrincipal"].ToString();

                dados.cnpj = DadosViabi["cnpj"].ToString().Trim();
                //dados.cnpjFilial = DadosViabi["cnpjFilial"].ToString();
                dados.CNPJMatriz = DadosViabi["CNPJMatriz"].ToString().Trim();
                dados.nire = DadosViabi["Nire"].ToString();
                dados.nireMatriz = DadosViabi["nireMatriz"].ToString();

                try
                {
                    if (DadosViabi["horaInicioAnaliseViabilidade"] != null && DadosViabi["horaInicioAnaliseViabilidade"].ToString() != "")
                        dados.horaInicioAnaliseViabilidade = DadosViabi["horaInicioAnaliseViabilidade"].ToString();

                    if (DadosViabi["horaFimAnaliseViabilidadeNome"] != null && DadosViabi["horaFimAnaliseViabilidadeNome"].ToString() != "")
                        dados.horaFimAnaliseViabilidadeNome = DadosViabi["horaFimAnaliseViabilidadeNome"].ToString();

                    if (DadosViabi["horaFimAnaliseViabilidadeEnd"] != null && DadosViabi["horaFimAnaliseViabilidadeEnd"].ToString() != "")
                        dados.horaFimAnaliseViabilidadeEndereco = DadosViabi["horaFimAnaliseViabilidadeEnd"].ToString();

                    if (DadosViabi["cpfSolicitante"] != null && DadosViabi["cpfSolicitante"].ToString() != "")
                        dados.cpfSolicitante = DadosViabi["cpfSolicitante"].ToString();
                }
                catch
                {

                }

                dados.codMunicipio = DadosViabi["codMunicipio"].ToString();
                dados.codStatusViabilidade = DadosViabi["codStatusViabilidade"].ToString();

                string tipo = "";// DadosViabi["codTipoLogradouro"].ToString().Trim();

                foreach (DataRow TipoLogradouro in DtTipoLogra.Rows)
                {
                    if (TipoLogradouro["COD_REGIN"].ToString().Trim().ToUpper() == DadosViabi["codTipoLogradouro"].ToString().Trim().ToUpper())
                    {
                        tipo = TipoLogradouro["COD_RFB"].ToString().Trim().ToUpper();
                        //return;
                        break;
                    }
                }

                dados.codTipoLogradouro = tipo;//DadosViabi["codTipoLogradouro"].ToString();
                dados.complementoLogradouro = DadosViabi["complementoLogradouro"].ToString();
                dados.complementoLogradouroRFB = DadosViabi["complementoLogradouroRFB"].ToString();

                /*
                    Evento
                 */
                int even = 0;
                string[] codEventosViabilidade = new string[DadosEvento.Rows.Count];
                foreach (DataRow CodEvento in DadosEvento.Rows)
                {
                    if (CodEvento["CodEvento"].ToString() != null && CodEvento["CodEvento"].ToString() != "")
                    {
                        codEventosViabilidade[even] = CodEvento["CodEvento"].ToString();
                        even++;
                    }
                }

                dados.codEventosViabilidade = codEventosViabilidade;

                dados.formaAtuacaoStr = DadosViabi["formaAtuacao"].ToString(); ;

                even = 0;
                string[] cpfSocioPf = new string[DadosQSA.Rows.Count];
                string[] nomeSocioPf = new string[DadosQSA.Rows.Count];

                foreach (DataRow Linha in DadosQSA.Rows)
                {
                    if (Linha["CPF"].ToString() != null && Linha["CPF"].ToString() != "")
                    {
                        cpfSocioPf[even] = Linha["CPF"].ToString().Trim();
                        nomeSocioPf[even] = Linha["Nome"].ToString().Trim();
                        even++;
                    }
                }
                dados.cpfSocioPf = cpfSocioPf;
                dados.nomeSocioPf = nomeSocioPf;

                //Cnae
                even = 0;
                string[] cnaeSecundaria = new string[DadosCnae.Rows.Count];
                foreach (DataRow Linha in DadosCnae.Rows)
                {
                    if (Linha["CodCnae"].ToString() != null && Linha["CodCnae"].ToString() != "")
                    {
                        cnaeSecundaria[even] = Linha["CodCnae"].ToString();
                        even++;
                    }
                }
                dados.cnaeSecundaria = cnaeSecundaria;



                dados.dataFimAnaliseViabilidadeEndereco = DadosViabi["DataFimViabilidadeEndereco"].ToString();

                dados.dataFimAnaliseViabilidadeNome = DadosViabi["dataFimAnaliseViabilidadeNome"].ToString();
                dados.dataInicioAnaliseViabilidade = DadosViabi["dataInicioAnaliseViabilidade"].ToString();
                dados.dataValidadeViabilidade = DadosViabi["dataValidadeViabilidade"].ToString();
                dados.distrito = DadosViabi["distrito"].ToString();
                dados.inscricaoImovel = DadosViabi["inscricaoImovel"].ToString();
                dados.logradouro = DadosViabi["logradouro"].ToString();
                dados.naturezaJuridica = DadosViabi["naturezaJuridica"].ToString();
                dados.nomeEmpresarial = DadosViabi["nomeEmpresarial"].ToString();
                dados.numLogradouro = DadosViabi["numLogradouro"].ToString();
                dados.objetoSocial = DadosViabi["objetoSocial"].ToString();
                dados.protocoloViabilidade = pProtocolo;
                dados.referencia = DadosViabi["referencia"].ToString();
                dados.resultadoViabilidadeEndereco = DadosViabi["resultadoViabilidadeEndereco"].ToString();
                dados.resultadoViabilidadeNome = DadosViabi["resultadoViabilidadeNome"].ToString();
                dados.tipoInscricao = DadosViabi["tipoInscricao"].ToString();
                dados.tipoOrgaoRegistro = DadosViabi["tipoOrgaoRegistro"].ToString();
                dados.tipoUnidade = DadosViabi["tipoUnidade"].ToString();
                dados.uf = DadosViabi["uf"].ToString();
                try
                {
                    if (DadosViabi["indicadorContabilista"] != null && DadosViabi["indicadorContabilista"].ToString() != "")
                        dados.indicadorContabilista = DadosViabi["indicadorContabilista"].ToString();
                }
                catch
                {

                }

                try
                {
                    if (DadosViabi["indicadorCnpjNomeEmpresarial"] != null && DadosViabi["indicadorCnpjNomeEmpresarial"].ToString() != "")
                        dados.indicadorCnpjNomeEmpresarial = DadosViabi["indicadorCnpjNomeEmpresarial"].ToString();
                }
                catch
                {

                }



                //c.Url = "http://localhost/wsrfbregin/ServiceReginRFB.asmx?WSDL";
                //c.Url = "http://regin.jucepa.pa.gov.br/wsrfbreginv2/ServiceReginRFB.asmx?WSDL";
                //c.SoapVersion = SoapProtocolVersion.Soap12;
                //c.Url = "http://regin.jucepa.pa.gov.br/wsservicerfb/Service.asmx?wsdl";
                //c.Url = "http://localhost/es/WsRFBRegin/ServiceReginRFB.asmx";
                //c.Url = "http://localhost:14698/WsRFBRegin/ServiceReginRFB.asmx";
                //c.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                result = ServiceWs01(dados);

                if (result.codretorno != null)
                {
                    foreach (incompRegistroIntegradorEstadual cod in result.codretorno)
                    {
                        if (cod.codigo != null && cod.codigo != "" && cod.codigo == "37")
                        {
                            result.status = "OK";
                        }
                    }
                }

            }
            return result;
        }

        #endregion

        #region Devolve XML DBE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Identificacao"></param>
        /// <param name="Recibo"></param>
        /// <returns></returns>
        [WebMethod]
        public Retorno ServiceXMLDBE(string Identificacao, string Recibo)
        {

            Retorno result = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());


                WsServices35RFB.serviceResponse pResponseNovo = new WsServices35RFB.serviceResponse();

                using (T73300_DBE_CONTROL c = new T73300_DBE_CONTROL())
                {
                    c.t73300_ide_solicitacao = Identificacao;
                    c.t73300_rec_solicitacao = Recibo;

                    string xml = c.getXMLDBE();

                    if (xml.ToString() != "")
                    {
                        result.codretorno = "09";
                        result.status = "OK";
                        result.descricao = "";
                        result.oWs35Response = (WsServices35RFB.serviceResponse)GlobalV1.CreateObject(xml, pResponseNovo);
                        result.XmlDBE = xml;
                    }
                    else
                    {
                        result.codretorno = "01";
                        result.status = "NOK";
                        result.descricao = "DBE Da receita federal não existe na base do Regin";
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.StackTrace + " Mes " + ex.Message;

                return result;
            }
        }
        #endregion

        #region Ws35

        [WebMethod]
        public Retorno ServiceWs35Soarquivo(string Identificacao, string Recibo)
        {
            Retorno result = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                if (Recibo.Trim().Length != 13 && Recibo.Trim().Length != 10)
                {
                    result.status = "NOK";
                    result.codretorno = "80";
                    result.descricao = "Número de Recibo Invalido: " + Recibo;
                    return result;
                }

                if (Identificacao.Trim().Length != 14 && Recibo.Trim().Length == 10)
                {
                    result.status = "NOK";
                    result.codretorno = "80";
                    result.descricao = "Número de Identificacao Invalido:" + Identificacao;
                    return result;
                }

                if (Identificacao.Trim().Length == 14 && Recibo.Trim().Length != 10)
                {
                    result.status = "NOK";
                    result.codretorno = "80";
                    result.descricao = "Número de Recibo/Identificação Invalido: " + Recibo + Identificacao;
                    return result;
                }


                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();

                result.Identificacao = Identificacao;
                result.Recibo = Recibo;

                if (Recibo.Trim().Length == 13)
                {
                    ServiceConsultaS99 s99 = new ServiceConsultaS99();

                    DataTable toReturnNu = s99.RecuperaS99Regin("", "", "", "", Recibo.Trim());
                    if (toReturnNu.Rows.Count == 0)
                    {

                        if (ConfigurationManager.AppSettings["urlServicesRFBReginNumeroUnico"] != null && ConfigurationManager.AppSettings["urlServicesRFBReginNumeroUnico"].ToString() != "")
                        {
                            WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();
                            WsServicesReginRFB.Retorno resulRegin = new WsServicesReginRFB.Retorno();
                            regin.Url = ConfigurationManager.AppSettings["urlServicesRFBReginNumeroUnico"].ToString();
                            resulRegin = regin.getS99NumeroUnicoRegin(Recibo);
                            if (resulRegin.status == "NOK")
                            {
                                result.status = "NOK";
                                result.descricao = resulRegin.descricao;

                                if (resulRegin.codretorno == "99")
                                {
                                    result.codretorno = "99";
                                    result.descricao = "Informe o N° de e Controle do DBE, que está localizado no roda pé do painel de eventos do DBE.";
                                }
                                return result;
                            }
                            Identificacao = resulRegin.Identificacao;
                            Recibo = resulRegin.Recibo;
                        }
                        else
                        {
                            result.status = "NOK";
                            result.codretorno = "99";
                            result.descricao = "Informe o N° de e Controle do DBE, que está localizado no roda pé do painel de eventos do DBE.";
                            return result;
                        }
                    }
                    else
                    {
                        Identificacao = toReturnNu.Rows[0]["identificacaoSolicitacao"].ToString();
                        Recibo = toReturnNu.Rows[0]["reciboSolicitacao"].ToString();
                    }
                }

                #region Busca XML na tabela
                int diasPrazoBuscaws35 = 0;
                if (ConfigurationManager.AppSettings.Get("diasPrazoBuscaws35") != null && ConfigurationManager.AppSettings.Get("diasPrazoBuscaws35").ToString() != "")
                {
                    try
                    {
                        diasPrazoBuscaws35 = int.Parse(ConfigurationManager.AppSettings.Get("diasPrazoBuscaws35").ToString());
                    }
                    catch { }
                }

                if (diasPrazoBuscaws35 > 0)
                {
                    try
                    {
                        T73309_WS35_RFB pResultWs35 = new T73309_WS35_RFB();
                        pResultWs35.t73309_recibo = Recibo;
                        pResultWs35.t73309_Identificacao = Identificacao;

                        pResultWs35 = pResultWs35.Query();

                        if (pResultWs35.t73309_recibo != null && pResultWs35.t73309_recibo != "")
                        {
                            ServiceConsultaS99 s99 = new ServiceConsultaS99();
                            DataTable toReturn = s99.RecuperaS99Regin(Recibo, Identificacao, "", "S22", "");
                            //se encoontro um registro de s22 dbe cancelado e o ultimo registro gravado nao era de cancelamento, apago e deixo buscar de novo
                            if (toReturn.Rows.Count > 0)
                            {
                                if (pResultWs35.t73309_codretorno != "05")
                                {
                                    GlobalV1 trata = new GlobalV1();
                                    trata.TrataS35TabelaMySql("S22", Recibo, Identificacao, "", "");
                                    pResultWs35.t73309_recibo = "";
                                    pResultWs35.t73309_Identificacao = "";

                                }
                            }
                        }

                        if (pResultWs35.t73309_recibo != null && pResultWs35.t73309_recibo != "")
                        {
                            WsServices35RFB.serviceResponse pResponseNovo = new WsServices35RFB.serviceResponse();
                            result.descricao = "";
                            result.oWs35Response = (WsServices35RFB.serviceResponse)GlobalV1.CreateObject(pResultWs35.t73309_xml, pResponseNovo);

                            result.codretorno = result.oWs35Response.@return.codigoRetorno;
                            result.status = result.oWs35Response.@return.statusEnvio;
                            result.descricao = result.oWs35Response.@return.descricaoRetorno;

                            result.XmlDBE = pResultWs35.t73309_xml;
                            return result;
                        }

                    }
                    catch
                    {

                    }
                }
                #endregion


                WsServices35RFB.ws35 c = new WsServices35RFB.ws35();

                X509Certificate cert = getCertificado(ConfigurationManager.AppSettings["DiretorioCertificado"].ToString(), ConfigurationManager.AppSettings["SenhaArquivo"].ToString());

                c.ClientCertificates.Add(cert);

                ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                c.Url = ConfigurationManager.AppSettings["UrlWs35"].ToString();

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                request.ClientCertificates.Add(cert);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string resp = sr.ReadToEnd();

                /*
                "01", "Número de solicitação invalido/inexistente");
                "04", "Solicitação está Indeferida!");
                "05", "Solicitação está Cancelada!");
                "07", "Solicitação encontra-se em andamento.");
                "08", "Solicitação em fase de preenchimento.");
                "09", "Solicitação está com DBE disponibilizado");
                "10", "Solicitação foi deferida.";
                "11", "Solicitação não é de deferimento da Junta Comercial.");
                */

                WsServices35RFB.serviceRequest dadosenvio = new WsServices35RFB.serviceRequest();
                WsServices35RFB.serviceResponse pResult = new WsServices35RFB.serviceResponse();

                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();

                dadosenvio.codServico = "S35"; //WSWS4115E
                dadosenvio.identificacaoSolicitacao = Identificacao;
                dadosenvio.reciboSolicitacao = Recibo;
                dadosenvio.versao = "100000";

                c.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
                c.Url = ConfigurationManager.AppSettings["UrlWs35"].ToString();

                try
                {
                    c.Timeout = 12000;

                    pResult = c.buscar(dadosenvio);

                    cert.Reset();
                }
                catch (Exception ex)
                {
                    cert.Reset();
                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = "DBE não está disponível para uso ou comunicação com a Receita Federal está indisponível no momento. Por favor tente mais tarde!. Consulte a situação no site:  http://www.receita.fazenda.gov.br/PessoaJuridica/CNPJ/fcpj/consulta.asp" + " Erro: (" + ex.Message + ")";
                    //result.descricao = "A comunicação com a Receita Federal está indisponível no momento. Por favor tente mais tarde!. (" + ex.Message + ")";
                    return result;
                }

                string ResponseRFB = GlobalV1.CreateXML(pResult);
                if (pResult.@return.statusEnvio == "OK")
                {
                    if (pResult.dadosRedesim.cnpj == null)
                    {
                        pResult.dadosRedesim.cnpj = "";

                        ResponseRFB = GlobalV1.CreateXML(pResult);
                    }

                    result.codretorno = pResult.@return.codigoRetorno;
                    result.status = pResult.@return.statusEnvio;
                    result.descricao = pResult.@return.descricaoRetorno;
                    result.XmlDBE = ResponseRFB;
                    result.oWs35Response = pResult;
                    result.Identificacao = Identificacao;
                    result.Recibo = Recibo;

                    GlobalV1 trata = new GlobalV1();
                    trata.TrataS35TabelaMySql("S35", Recibo, Identificacao, pResult.@return.codigoRetorno, ResponseRFB);



                }
                else
                {
                    if (pResult.@return.codigoRetorno == "05")
                    {
                        result.codretorno = pResult.@return.codigoRetorno;
                        result.status = pResult.@return.statusEnvio;
                        result.descricao = "RFB: " + "O DBE está cancelado.";
                        result.XmlDBE = ResponseRFB;
                        result.oWs35Response = pResult;

                    }
                    else
                    {
                        //if (pResult.@return.codigoRetorno == "07")
                        //{
                        //    return ServiceXMLDBE(Identificacao, Recibo);
                        //}

                        result.codretorno = pResult.@return.codigoRetorno;
                        result.status = pResult.@return.statusEnvio;
                        result.descricao = "RFB: " + pResult.@return.descricaoRetorno;
                        result.XmlDBE = ResponseRFB;
                        result.oWs35Response = pResult;

                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Codigo de Retorno RFB
        /// "01", "Número de solicitação invalido/inexistente");
        /// "04", "Solicitação está Indeferida!");
        /// "05", "Solicitação está Cancelada!");
        /// "07", "Solicitação encontra-se em andamento.");
        /// "08", "Solicitação em fase de preenchimento.");
        /// "09", "Solicitação está com DBE disponibilizado");
        /// "10", "Solicitação foi deferida.";
        /// "11", "Solicitação não é de deferimento da Junta Comercial.");
        /// "61", "Exigência pela Junta."
        /// </summary>
        /// <param name="Identificacao"></param>
        /// <param name="Recibo"></param>
        /// <returns></returns>
        [WebMethod]
        public Retorno ServiceWs35Regin(string Identificacao, string Recibo)
        {
            string ResponseRFB = "";
            Retorno result = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                if (Recibo.Trim().Length != 13 && Recibo.Trim().Length != 10)
                {
                    result.status = "NOK";
                    result.codretorno = "80";
                    result.descricao = "Número de Recibo Invalido: " + Recibo;
                    return result;
                }

                if (Identificacao.Trim().Length != 14 && Recibo.Trim().Length == 10)
                {
                    result.status = "NOK";
                    result.codretorno = "80";
                    result.descricao = "Número de Identificacao Invalido:" + Identificacao;
                    return result;
                }

                if (Identificacao.Trim().Length == 14 && Recibo.Trim().Length != 10)
                {
                    result.status = "NOK";
                    result.codretorno = "80";
                    result.descricao = "Número de Recibo/Identificação Invalido: " + Recibo + Identificacao;
                    return result;
                }


                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();

                result.Recibo = Recibo;
                result.Identificacao = Identificacao;

                //E porque e numero Unico
                if (Recibo.Trim().Length == 13)
                {
                    ServiceConsultaS99 s99 = new ServiceConsultaS99();

                    DataTable toReturnNu = s99.RecuperaS99Regin("", "", "", "", Recibo.Trim());
                    if (toReturnNu.Rows.Count == 0)
                    {

                        if (ConfigurationManager.AppSettings["urlServicesRFBReginNumeroUnico"] != null && ConfigurationManager.AppSettings["urlServicesRFBReginNumeroUnico"].ToString() != "")
                        {
                            WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();
                            WsServicesReginRFB.Retorno resulRegin = new WsServicesReginRFB.Retorno();
                            regin.Url = ConfigurationManager.AppSettings["urlServicesRFBReginNumeroUnico"].ToString();
                            resulRegin = regin.getS99NumeroUnicoRegin(Recibo);
                            if (resulRegin.status == "NOK")
                            {
                                result.status = "NOK";
                                result.descricao = resulRegin.descricao;

                                if (resulRegin.codretorno == "99")
                                {
                                    result.codretorno = "99";
                                    result.descricao = "Informe o N° de e Controle do DBE, que está localizado no roda pé do painel de eventos do DBE.";
                                }
                                return result;
                            }
                            Identificacao = resulRegin.Identificacao;
                            Recibo = resulRegin.Recibo;
                        }
                        else
                        {
                            result.status = "NOK";
                            result.codretorno = "99";
                            result.descricao = "Informe o N° de e Controle do DBE, que está localizado no roda pé do painel de eventos do DBE.";
                            return result;
                        }
                    }
                    else
                    {
                        Identificacao = toReturnNu.Rows[0]["identificacaoSolicitacao"].ToString();
                        Recibo = toReturnNu.Rows[0]["reciboSolicitacao"].ToString();
                    }

                    result.Identificacao = Identificacao;
                    result.Recibo = Recibo;

                }


                #region Busca XML na tabela
                int diasPrazoBuscaws35 = 0;
                if (ConfigurationManager.AppSettings.Get("diasPrazoBuscaws35") != null && ConfigurationManager.AppSettings.Get("diasPrazoBuscaws35").ToString() != "")
                {
                    try
                    {
                        diasPrazoBuscaws35 = int.Parse(ConfigurationManager.AppSettings.Get("diasPrazoBuscaws35").ToString());
                    }
                    catch { }
                }

                if (diasPrazoBuscaws35 > 0)
                {
                    try
                    {
                        T73309_WS35_RFB pResultWs35 = new T73309_WS35_RFB();
                        pResultWs35.t73309_recibo = Recibo;
                        pResultWs35.t73309_Identificacao = Identificacao;

                        pResultWs35 = pResultWs35.Query();

                        if (pResultWs35.t73309_recibo != null && pResultWs35.t73309_recibo != "")
                        {
                            ServiceConsultaS99 s99 = new ServiceConsultaS99();
                            DataTable toReturn = s99.RecuperaS99Regin(Recibo, Identificacao, "", "S22", "");
                            //se encoontro um registro de s22 dbe cancelado e o ultimo registro gravado nao era de cancelamento, apago e deixo buscar de novo
                            if (toReturn.Rows.Count > 0)
                            {
                                if (pResultWs35.t73309_codretorno != "05")
                                {
                                    GlobalV1 trata = new GlobalV1();
                                    trata.TrataS35TabelaMySql("S22", Recibo, Identificacao, "", "");
                                    pResultWs35.t73309_recibo = "";
                                    pResultWs35.t73309_Identificacao = "";

                                }
                            }
                        }

                        if (pResultWs35.t73309_recibo != null && pResultWs35.t73309_recibo != "")
                        {
                            //voy a apagar caso seja diferente de junta, porque a RFB nao manda o s22 uando e OAB e cartorio, ou quando e cancelado se a analise e feita na RFB
                            result = ServiceWs35(Identificacao, Recibo, pResultWs35.t73309_xml);
                            if (result.oWs35Response != null && result.oWs35Response.dadosRedesim != null && result.oWs35Response.dadosRedesim.orgaoResponsavelDeferimento != null)
                            {
                                if (result.oWs35Response.dadosRedesim.orgaoResponsavelDeferimento.Length > 2 &&
                                    result.oWs35Response.dadosRedesim.orgaoResponsavelDeferimento.Substring(0, 2) != "JC")
                                {
                                    T73309_WS35_RFB pResultWs35delete = new T73309_WS35_RFB();
                                    pResultWs35delete.t73309_recibo = Recibo;
                                    pResultWs35delete.t73309_Identificacao = Identificacao;
                                    pResultWs35delete.DeletePk();
                                }
                                else
                                {
                                    return result;
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                #endregion

                /*
                 Caso nao seja JUCERJA continuo com a rotina
                 * buscar dbe, gravar, etc
                 */

                WsServices35RFB.ws35 c = new WsServices35RFB.ws35();


                X509Certificate cert = getCertificado(ConfigurationManager.AppSettings["DiretorioCertificado"].ToString(), ConfigurationManager.AppSettings["SenhaArquivo"].ToString());

                c.ClientCertificates.Add(cert);

                c.Url = ConfigurationManager.AppSettings["UrlWs35"].ToString();

                WsServices35RFB.serviceRequest dadosenvio = new WsServices35RFB.serviceRequest();
                WsServices35RFB.serviceResponse pResult = new WsServices35RFB.serviceResponse();

                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();

                dadosenvio.codServico = "S35"; //WSWS4115E
                dadosenvio.identificacaoSolicitacao = Identificacao;
                dadosenvio.reciboSolicitacao = Recibo;
                dadosenvio.versao = "100000";

                try
                {
                    c.Timeout = 12000;

                    pResult = c.buscar(dadosenvio);

                    cert.Reset();
                }
                catch (Exception ex)
                {
                    cert.Reset();
                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = "A comunicação com a Receita Federal está indisponível no momento. Por favor tente mais tarde!. (" + ex.Message + ")";
                    result.descricao = "DBE não está disponível para uso ou comunicação com a Receita Federal está indisponível no momento. Por favor tente mais tarde!. Consulte a situação no site:  http://www.receita.fazenda.gov.br/PessoaJuridica/CNPJ/fcpj/consulta.asp" + " Erro: (" + ex.Message + ")";

                    using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    {
                        e35.t73307_arquivo_RFB = ResponseRFB;
                        e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                        e35.t73307_ide_solicitacao = Identificacao;
                        e35.t73307_rec_solicitacao = Recibo;
                        e35.Update();
                    }
                    return result;
                }

                ResponseRFB = GlobalV1.CreateXML(pResult);

                if (pResult.@return.statusEnvio == "OK")
                {
                    if (pResult.dadosRedesim.cnpj == null)
                    {
                        pResult.dadosRedesim.cnpj = "";

                        ResponseRFB = GlobalV1.CreateXML(pResult);
                    }

                    GlobalV1 trata = new GlobalV1();
                    trata.TrataS35TabelaMySql("S35", Recibo, Identificacao, pResult.@return.codigoRetorno, ResponseRFB);

                    return ServiceWs35(Identificacao, Recibo, ResponseRFB);
                }
                else
                {
                    if (pResult.@return.codigoRetorno == "05")
                    {
                        result.codretorno = pResult.@return.codigoRetorno;
                        result.status = pResult.@return.statusEnvio;
                        result.descricao = "RFB: " + "O DBE está cancelado.";
                        result.XmlDBE = ResponseRFB;
                        result.oWs35Response = pResult;

                    }
                    else
                    {
                        //if (pResult.@return.codigoRetorno == "07")
                        //{
                        //    return ServiceXMLDBE(Identificacao, Recibo);
                        //}

                        result.codretorno = pResult.@return.codigoRetorno;
                        result.status = pResult.@return.statusEnvio;
                        result.descricao = "RFB: " + pResult.@return.descricaoRetorno;
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = ResponseRFB;
                    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    e35.t73307_ide_solicitacao = Identificacao;
                    e35.t73307_rec_solicitacao = Recibo;
                    e35.Update();
                }
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.StackTrace + " Mes " + ex.Message;

                return result;
            }
        }


        /// <summary>
        /// Codigo de Retorno RFB
        /// "01", "Número de solicitação invalido/inexistente");
        /// "04", "Solicitação está Indeferida!");
        /// "05", "Solicitação está Cancelada!");
        /// "07", "Solicitação encontra-se em andamento.");
        /// "08", "Solicitação em fase de preenchimento.");
        /// "09", "Solicitação está com DBE disponibilizado");
        /// "10", "Solicitação foi deferida.";
        /// "11", "Solicitação não é de deferimento da Junta Comercial.");
        /// </summary>
        /// <param name="Identificacao"></param>
        /// <param name="Recibo"></param>
        /// <param name="ResponseRFB"></param>
        /// <returns></returns>
        [WebMethod]
        public Retorno ServiceWs35(string Identificacao, string Recibo, string ResponseRFB)
        {
            //string pXml = "";

            Retorno result = new Retorno();
            try
            {

                // AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                WsServices35RFB.serviceResponse pResponseNovo = new WsServices35RFB.serviceResponse();

                pResponseNovo = (WsServices35RFB.serviceResponse)GlobalV1.CreateObject(ResponseRFB, pResponseNovo);

                if (pResponseNovo.@return.statusEnvio == "OK")
                {
                    using (T73300_DBE_CONTROL ss = new T73300_DBE_CONTROL())
                    {
                        Identificacao = Identificacao.ToUpper();
                        Recibo = Recibo.ToUpper();

                        ss.t73300_ide_solicitacao = Identificacao;
                        ss.t73300_rec_solicitacao = Recibo;

                        //string Xml = ResponseRFB;
                        int hashcode = ResponseRFB.GetHashCode();
                        string HashCodeArquivoGravado = ss.getHashCode();

                        if (hashcode.ToString() != HashCodeArquivoGravado)
                        {
                            result.regisroDiferente = "1";
                        }
                    }

                    psc.Receita.ProcessaDBE dbe = new psc.Receita.ProcessaDBE();
                    dbe.ProcessaDadosDbe(pResponseNovo, Recibo, Identificacao);

                    result.codretorno = pResponseNovo.@return.codigoRetorno;
                    result.status = pResponseNovo.@return.statusEnvio;
                    result.descricao = pResponseNovo.@return.descricaoRetorno;
                    result.XmlDBE = ResponseRFB;
                    result.Recibo = Recibo;
                    result.Identificacao = Identificacao;

                    if (GlobalV1.valNuloBranco(pResponseNovo.dadosRedesim.fcpj.nire) != "")
                    {
                        result.Nire = GlobalV1.valNuloBranco(decimal.Parse(pResponseNovo.dadosRedesim.fcpj.nire));
                    }

                    result.Cnpj = pResponseNovo.dadosRedesim.cnpj;
                    result.oWs35Response = pResponseNovo;

                    if (!String.IsNullOrEmpty(result.Cnpj))
                    {
                        //Apaga registro de CNPJ caso exista
                        using (T73309_WS11_RFB cc = new T73309_WS11_RFB())
                        {
                            cc.t73309_cnpj = result.Cnpj;
                            cc.DeletePk();
                        }
                    }

                    return result;

                }
                else
                {
                    result.codretorno = pResponseNovo.@return.codigoRetorno;
                    result.status = pResponseNovo.@return.statusEnvio;
                    result.descricao = "RFB: " + pResponseNovo.@return.descricaoRetorno;

                    return result;
                }
            }
            catch (Exception ex)
            {

                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = ResponseRFB;
                    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    e35.t73307_ide_solicitacao = Identificacao;
                    e35.t73307_rec_solicitacao = Recibo;
                    e35.Update();
                }
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.StackTrace + " Mes " + ex.Message;

                return result;

                //txtResposta.Text = ex.Message;
            }
        }

        #endregion

        #region ws05
        /// <summary>
        /// Codigo de retorno
        /// *** Os códigos de erro acima de "90" são relativos a procedimentos internos. Recebendo algum deles será necessário informar à RFB.
        /// Segue a lista de retorno do S05:
        /// * MENSAGENS DE RETORNO PROGRAMADA
        /// * 00 - 'SUCESSO'
        /// * 01 - 'REQUISICAO INVALIDA'
        /// * 02 - 'SOLICITACAO ESTA INDEFERIDA'
        /// * 03 - 'SOLICITACAO ESTA CANCELADA PELO CONTRIBUINTE' OU * 'SOLICITACAO ESTA CANCELADA DE OFICIO PELA RFB' OU * 'SOLICITACAO ESTA CANCELADA POR DECURSO DE PRAZO DE 90 DIAS'
        /// * 04 - 'SOLICITACAO JA ESTA DEFERIDA'
        /// * 05 - 'DBE AINDA NAO FOI DISPONIBILIZADO'
        /// * 06 - 'SOLICITACAO NAO E DE DEFERIMENTO DA JUNTA COMERCIAL'
        /// * 07 - EVENTO X NJ NAO PODE SER DEFERIDA PELA JUNTA COMERCIAL VIA WEBSERVICE
        /// * 08 - 'DBE INDISPONIBILIZADO'
        /// * 09 - 'SISTEMA FECHADO PARA ACEITACAO DA DOC - TENTE MAIS TARDE'
        /// * 10 - 'NUMERO DE SOLICITACAO INEXISTENTE'
        /// * 12 - 'DBE JA RECEPCIONADO PELO ORGAO DE REGISTRO'
        /// * 17 - 'SITUAçãO DA SOLICITAçãO NãO PERMITE COLOCAçãO EM EXIGêNCIA'
        /// * 20 - UF ENVIADA PELO SERVICO S05 EH INVALIDA.
        /// </summary>
        /// <param name="Identificacao"></param>
        /// <param name="Recibo"></param>
        /// <param name="cpfRecepcionador"></param>
        /// <returns></returns>
        [WebMethod]
        public Retorno ServiceWs05(string Identificacao, string Recibo, string cpfRecepcionador)
        {
            Retorno result = new Retorno();

            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                //if (ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"] == null)
                //{
                //    result.codretorno = "99";
                //    result.status = "NOK";
                //    result.descricao = "Falta configurar patametro No arquivo web.config pCNPJInstituicaoFixoJUNTA";
                //    return result;
                //}

                return ServiceWs05V2(Identificacao, Recibo, cpfRecepcionador, "", ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [WebMethod]
        public Retorno ServiceWs05V2(string Identificacao, string Recibo, string cpfRecepcionador, string numeroServentia, string cnpjOrgaoRegistro)
        {
            Retorno result = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();


                if (numeroServentia.Trim() != "")
                    numeroServentia = numeroServentia.PadLeft(6, '0');


                WsServices05RFB.entregaDocumentosRequest dadosenvio = new WsServices05RFB.entregaDocumentosRequest();
                WsServices05RFB.entregaDocumentosResponse pResult = new WsServices05RFB.entregaDocumentosResponse();
                WsServices05RFB.WS05Serviceserviceagent c = new WsServices05RFB.WS05Serviceserviceagent();


                X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                c.ClientCertificates.Add(cert);

                //ws05.entregaDocumentosRequest dadosenvio = new ws05.entregaDocumentosRequest();
                //ws05.entregaDocumentosResponse pResult = new ws05.entregaDocumentosResponse();

                if (cnpjOrgaoRegistro == "")
                {
                    cnpjOrgaoRegistro = ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString();
                }

                //if (cnpjOrgaoRegistro == "")
                //{
                //    result.codretorno = "99";
                //    result.status = "NOK";
                //    result.descricao = "Parametro em branco cnpjOrgaoRegistro não passado";
                //    return result;
                //}
                if (cpfRecepcionador == "")
                {
                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = "Parametro em branco cpfRecepcionador não passado";
                    return result;
                }
                //Sempre vou mndar em branco porque foi retirada aobrigatoriedade, mas continuo validando o parametro porque no s06 precisa
                cnpjOrgaoRegistro = "";

                if (ConfigurationManager.AppSettings["cpfRecepcionadorWS05SomenteHomolog"] != null && ConfigurationManager.AppSettings["cpfRecepcionadorWS05SomenteHomolog"].ToString() != "")
                {
                    cpfRecepcionador = ConfigurationManager.AppSettings["cpfRecepcionadorWS05SomenteHomolog"].ToString();
                }

                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();

                dadosenvio.codServico = "S05";
                dadosenvio.versao = "100000";
                if (numeroServentia != "")
                {
                    dadosenvio.numeroServentia = numeroServentia;
                }
                //if (cnpjOrgaoRegistro != "")
                //{
                //    dadosenvio.cnpjOrgaoRegistro = "";
                //}
                dadosenvio.cpfRecepcionador = cpfRecepcionador;
                dadosenvio.dataRecepcao = DateTime.Now.ToString("yyyyMMdd");
                dadosenvio.reciboSolicitacao = Recibo;
                dadosenvio.identificacaoSolicitacao = Identificacao;
                dadosenvio.situacaoDocumentacaoOrgao = "R";

                c.Url = ConfigurationManager.AppSettings.Get("UrlWs05").ToString();


                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                request.ClientCertificates.Add(cert);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string resp = sr.ReadToEnd();


                try
                {
                    c.Timeout = 12000;


                    // Aqui e para confirmar o s04 porque no balcao Unico por exemplo nao envias o s99 com isso falta passar o s04
                    DadosWs04 dados04 = new DadosWs04();
                    Retorno resulS04 = new Retorno();
                    dados04.identificacaoSolicitacao = dadosenvio.identificacaoSolicitacao;
                    dados04.reciboSolicitacao = dadosenvio.reciboSolicitacao;
                    dados04.resultadoValidacao = "01";

                    resulS04 = ServiceWs04(dados04);

                    pResult = c.entregaDocumentos(dadosenvio);

                    cert.Reset();

                    GlobalV1 trata = new GlobalV1();

                    string codigoRetorno = "";
                    if (pResult.retornoWSRedesim.codigoRetorno != null && pResult.retornoWSRedesim.codigoRetorno != "")
                        codigoRetorno = pResult.retornoWSRedesim.codigoRetorno;

                    if (codigoRetorno == "")
                    {
                        if (pResult.retornoWSRedesim.statusEnvio != null && pResult.retornoWSRedesim.statusEnvio != "")
                            codigoRetorno = pResult.retornoWSRedesim.statusEnvio;
                    }

                    trata.TrataS35TabelaMySql(dadosenvio.codServico, Recibo, Identificacao, codigoRetorno, "");
                }
                catch (Exception ex)
                {

                    string detail = "";
                    try
                    {
                        detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                    }
                    catch { }

                    //string XmlDados = " cpfRecepcionador " + cpfRecepcionador + " numeroServentia " + numeroServentia + " cnpjOrgaoRegistro " + cnpjOrgaoRegistro;
                    string XmlDados = "Serviço temporariamente indisponível na RFB, tente novamente mais tarde! " + ex.Message + " SoapException " + detail.ToString();

                    //result.descricao = "Serviço temporariamente indisponível na RFB, tente novamente mais tarde! " + ex.Message + " SoapException " + detail.ToString();

                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = XmlDados;
                    return result;
                }

                if (pResult.retornoWSRedesim.statusEnvio == "OK")
                {
                    result.codretorno = "00";
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = pResult.retornoWSRedesim.descricaoRetorno;
                }
                else
                {
                    result.codretorno = pResult.retornoWSRedesim.codigoRetorno;
                    if (pResult.retornoWSRedesim.codigoRetorno == "12")
                    {
                        result.codretorno = "8";
                    }
                    string mensagemRegin = "";
                    if (pResult.retornoWSRedesim.codigoRetorno == "03")
                    {
                        mensagemRegin = "DBE CANCELADO, Verificar na consulta da RFB http://www.receita.fazenda.gov.br/PessoaJuridica/CNPJ/fcpj/consulta.asp";
                    }

                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = "RFB: " + result.codretorno + " - " + mensagemRegin + " " + pResult.retornoWSRedesim.descricaoRetorno;
                }

                if (result.codretorno == "04")
                    result.codretorno = "4";

                if (result.codretorno == "08")
                    result.codretorno = "8";

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        [WebMethod]
        public Retorno ServiceWs05EX(string Identificacao, string Recibo, string cpfRecepcionador, string numeroServentia, string cnpjOrgaoRegistro)
        {
            Retorno result = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();

                if (numeroServentia.Trim() != "")
                    numeroServentia = numeroServentia.PadLeft(6, '0');

                WsServices05RFB.entregaDocumentosRequest dadosenvio = new WsServices05RFB.entregaDocumentosRequest();
                WsServices05RFB.entregaDocumentosResponse pResult = new WsServices05RFB.entregaDocumentosResponse();
                WsServices05RFB.WS05Serviceserviceagent c = new WsServices05RFB.WS05Serviceserviceagent();


                X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                c.ClientCertificates.Add(cert);

                //ws05.entregaDocumentosRequest dadosenvio = new ws05.entregaDocumentosRequest();
                //ws05.entregaDocumentosResponse pResult = new ws05.entregaDocumentosResponse();

                if (cnpjOrgaoRegistro == "")
                {
                    cnpjOrgaoRegistro = ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString();
                }

                //if (cnpjOrgaoRegistro == "")
                //{
                //    result.codretorno = "99";
                //    result.status = "NOK";
                //    result.descricao = "Parametro em branco cnpjOrgaoRegistro não passado";
                //    return result;
                //}
                if (cpfRecepcionador == "")
                {
                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = "Parametro em branco cpfRecepcionador não passado";
                    return result;
                }

                if (ConfigurationManager.AppSettings["cpfRecepcionadorWS05SomenteHomolog"] != null && ConfigurationManager.AppSettings["cpfRecepcionadorWS05SomenteHomolog"].ToString() != "")
                {
                    cpfRecepcionador = ConfigurationManager.AppSettings["cpfRecepcionadorWS05SomenteHomolog"].ToString();
                }

                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();

                dadosenvio.codServico = "S05";
                dadosenvio.versao = "100000";
                if (numeroServentia != "")
                {
                    dadosenvio.numeroServentia = numeroServentia;
                }
                //if (cnpjOrgaoRegistro != "")
                //{
                //    dadosenvio.cnpjOrgaoRegistro = cnpjOrgaoRegistro;
                //}
                dadosenvio.cpfRecepcionador = cpfRecepcionador;
                dadosenvio.dataRecepcao = DateTime.Now.ToString("yyyyMMdd");
                dadosenvio.reciboSolicitacao = Recibo;
                dadosenvio.identificacaoSolicitacao = Identificacao;
                dadosenvio.situacaoDocumentacaoOrgao = "E";

                c.Url = ConfigurationManager.AppSettings.Get("UrlWs05").ToString();


                //servicepointmanager.servercertificatevalidationcallback = new remotecertificatevalidationcallback(delegate { return true; });
                //httpwebrequest request = (httpwebrequest)webrequest.create(c.url);
                //request.clientcertificates.add(cert);
                //httpwebresponse response = (httpwebresponse)request.getresponse();
                //stream stream = response.getresponsestream();
                //streamreader sr = new streamreader(stream);
                //string resp = sr.readtoend();


                try
                {
                    c.Timeout = 12000;

                    pResult = c.entregaDocumentos(dadosenvio);
                    cert.Reset();

                    GlobalV1 trata = new GlobalV1();
                    string codigoRetorno = "";
                    if (pResult.retornoWSRedesim.codigoRetorno != null && pResult.retornoWSRedesim.codigoRetorno != "")
                        codigoRetorno = pResult.retornoWSRedesim.codigoRetorno;

                    if (codigoRetorno == "")
                    {
                        if (pResult.retornoWSRedesim.statusEnvio != null && pResult.retornoWSRedesim.statusEnvio != "")
                            codigoRetorno = pResult.retornoWSRedesim.statusEnvio;
                    }

                    trata.TrataS35TabelaMySql(dadosenvio.codServico, Recibo, Identificacao, codigoRetorno, "");

                }
                catch (Exception ex)
                {

                    string detail = "";
                    try
                    {
                        detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                    }
                    catch { }

                    //string XmlDados = " cpfRecepcionador " + cpfRecepcionador + " numeroServentia " + numeroServentia + " cnpjOrgaoRegistro " + cnpjOrgaoRegistro;
                    string XmlDados = "Serviço temporariamente indisponível na RFB, tente novamente mais tarde! " + ex.Message + " SoapException " + detail.ToString();

                    //result.descricao = "Serviço temporariamente indisponível na RFB, tente novamente mais tarde! " + ex.Message + " SoapException " + detail.ToString();

                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = XmlDados;
                    return result;
                }

                if (pResult.retornoWSRedesim.statusEnvio == "OK")
                {
                    result.codretorno = "00";
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = pResult.retornoWSRedesim.descricaoRetorno;

                }
                else
                {
                    result.codretorno = pResult.retornoWSRedesim.codigoRetorno;
                    if (pResult.retornoWSRedesim.codigoRetorno == "12")
                    {
                        result.codretorno = "8";
                    }
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = "RFB: " + result.codretorno + " - " + pResult.retornoWSRedesim.descricaoRetorno;
                }
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region ws06
        /// <summary>
        /// 
        /// *** Os códigos de erro acima de "90" são relativos a procedimentos internos. Recebendo algum deles será necessário informar à RFB.
        /// Segue a lista de retorno do S05:
        /// * MENSAGENS DE RETORNO PROGRAMADA
        /// * 00 - 'SUCESSO'
        /// * 01 - 'REQUISICAO INVALIDA'
        /// * 02 - 'SOLICITACAO ESTA INDEFERIDA'
        /// * 03 - 'SOLICITACAO ESTA CANCELADA PELO CONTRIBUINTE' OU * 'SOLICITACAO ESTA CANCELADA DE OFICIO PELA RFB' OU * 'SOLICITACAO ESTA CANCELADA POR DECURSO DE PRAZO DE 90 DIAS'
        /// * 04 - 'SOLICITACAO JA ESTA DEFERIDA'
        /// * 05 - 'DBE AINDA NAO FOI DISPONIBILIZADO'
        /// * 06 - 'SOLICITACAO NAO E DE DEFERIMENTO DA JUNTA COMERCIAL'
        /// * 07 - EVENTO X NJ NAO PODE SER DEFERIDA PELA JUNTA COMERCIAL VIA WEBSERVICE
        /// * 08 - 'DBE INDISPONIBILIZADO'
        /// * 09 - 'SISTEMA FECHADO PARA ACEITACAO DA DOC - TENTE MAIS TARDE'
        /// * 10 - 'NUMERO DE SOLICITACAO INEXISTENTE'
        /// </summary>
        /// <param name="dados"></param>
        /// <returns></returns>
        [WebMethod]
        public Retorno ServiceWs06(DadosWs06 dados)
        {
            Retorno result = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                dados.identificacaoSolicitacao = dados.identificacaoSolicitacao.ToUpper();
                dados.reciboSolicitacao = dados.reciboSolicitacao.ToUpper();

                WsServices06RFB.processarComunicacaoDeferIndeferRequest dadosenvio = new WsServices06RFB.processarComunicacaoDeferIndeferRequest();
                WsServices06RFB.processarComunicacaoDeferIndeferResponse pResult = new WsServices06RFB.processarComunicacaoDeferIndeferResponse();
                WsServices06RFB.WS06Serviceserviceagent c = new WsServices06RFB.WS06Serviceserviceagent();


                X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                c.ClientCertificates.Add(cert);

                //ws05.entregaDocumentosRequest dadosenvio = new ws05.entregaDocumentosRequest();
                //ws05.entregaDocumentosResponse pResult = new ws05.entregaDocumentosResponse();

                dadosenvio.codServico = "S06";
                dadosenvio.versao = "100000";

                if (dados.resultadoRegistroIntegradorEstadual == "02") //Indeferimento
                {
                    if (dados.incompRegistroIntegradorEstadual != null && dados.incompRegistroIntegradorEstadual.Length > 0)
                    {
                        WsServices06RFB.incompRegistroIntegradorEstadual[] inconpa = new WsServices06RFB.incompRegistroIntegradorEstadual[20];

                        foreach (incompRegistroIntegradorEstadual _dadosWs06 in dados.incompRegistroIntegradorEstadual)
                        {
                            if (_dadosWs06 != null && _dadosWs06.codigo != "")
                            {
                                int i = 0;
                                WsServices06RFB.incompRegistroIntegradorEstadual inconpa2 = new WsServices06RFB.incompRegistroIntegradorEstadual();
                                inconpa2.codigo = _dadosWs06.codigo;
                                inconpa2.mensagem = _dadosWs06.mensagem;
                                inconpa.SetValue(inconpa2, i);
                                i++;
                            }
                        }

                        dadosenvio.incompRegistroIntegradorEstadual = inconpa;
                    }

                }
                if (dados.resultadoRegistroIntegradorEstadual == "01")
                {
                    dadosenvio.dataRegistro = dados.dataRegistro;
                    /*

                        Verifica se o DBE tem evento de constituição
                     */
                    if (dados.IndicadorCNPJNome != "1") // so manda o nome se nao for viabilidade de indicação de cnpj no nome empresarial
                    {
                        if (dados.nomeEmpresarial != null && dados.nomeEmpresarial.ToString() != "")
                        {
                            Retorno pResws35 = new Retorno();
                            DadosViabilidade dv = new DadosViabilidade();
                            pResws35 = ServiceWs35Regin(dados.identificacaoSolicitacao, dados.reciboSolicitacao);
                            if (pResws35.status.ToUpper() == "OK")
                            {
                                if (pResws35.oWs35Response.dadosRedesim.fcpj.codEvento != null)
                                {
                                    //Isto aqui e porque ao fazer s06 a RFB retorna
                                    //NAO PODE ENVIAR NM EMPRESARIAL EXISTE EVENTO ESPECIAL. 
                                    //Com isso nao podemos passar o nome empresarial com esses eventos
                                    if (!dv.isEvento(pResws35.oWs35Response.dadosRedesim.fcpj.codEvento, "417") // Início da Liquidação Extrajudicial
                                        && !dv.isEvento(pResws35.oWs35Response.dadosRedesim.fcpj.codEvento, "418")// Recuperação Judicial
                                        && !dv.isEvento(pResws35.oWs35Response.dadosRedesim.fcpj.codEvento, "416")// Liquidação
                                        && !dv.isEvento(pResws35.oWs35Response.dadosRedesim.fcpj.codEvento, "408") //Término de liquidação
                                        )
                                    {
                                        if (dv.isEvento(pResws35.oWs35Response.dadosRedesim.fcpj.codEvento, "101, 106, 220"))
                                        {
                                            dadosenvio.nomeEmpresarial = GlobalV1.RetiraTipoEnquadramento(DadosViabilidade.TiraAcentoNomeEmpresarial(dados.nomeEmpresarial, "")).Trim();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (dados.numeroNire != null && dados.numeroNire.ToString() != "")
                    {
                        dadosenvio.numeroNire = dados.numeroNire;
                    }

                    if (dados.numeroRegistroOab != null && dados.numeroRegistroOab.ToString() != "")
                    {
                        /*
                         * Formatação OAB
                            OABUFNNNNN 
                         */
                        dadosenvio.numeroRegistroOab = "OAB" + dados.Uf.ToUpper() + dados.numeroRegistroOab.PadLeft(5, '0'); ;
                    }

                    if (dados.numeroRegistroCartorio != null && dados.numeroRegistroCartorio.ToString() != "")
                    {
                        dadosenvio.numeroRegistroCartorio = dados.numeroRegistroCartorio;
                        dadosenvio.numeroServentia = dados.numeroServentia;
                        if (dados.numeroRegistroCartorio.Length < 9)
                        {
                            dados.numeroServentia = dados.numeroServentia.Trim();
                            dados.numeroRegistroCartorio = dados.numeroRegistroCartorio.Trim();

                            string numero = dados.numeroServentia + dados.numeroRegistroCartorio.PadLeft(8, '0');
                            int dv1 = psc.Framework.General.CalculateVerificationDigit(numero, 9);
                            numero = numero + dv1.ToString();
                            int dv2 = psc.Framework.General.CalculateVerificationDigit(numero, 9);
                            numero = dados.numeroServentia + "PJ" + dados.numeroRegistroCartorio.PadLeft(8, '0') + dv1.ToString() + dv2.ToString();
                            dadosenvio.numeroRegistroCartorio = numero;
                        }


                    }

                    if (dados.numeroNire246 != null && dados.numeroNire246.ToString() != "")
                    {
                        dadosenvio.numeroNire246 = dados.numeroNire246;
                    }
                }

                if (ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"] == null || ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString() == "")
                {
                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = "Falta configurar patametro No arquivo web.config pCNPJInstituicaoFixoJUNTA";
                    return result;
                }

                string CNPJDeferimento = ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString();

                dadosenvio.cnpjOrgaoRegistro = CNPJDeferimento;
                dadosenvio.cpfResponsavelDeferimento = dados.cpfResponsavelDeferimento;

                if (ConfigurationManager.AppSettings["cpfRecepcionadorWS05SomenteHomolog"] != null && ConfigurationManager.AppSettings["cpfRecepcionadorWS05SomenteHomolog"].ToString() != "")
                {
                    dadosenvio.cpfResponsavelDeferimento = ConfigurationManager.AppSettings["cpfRecepcionadorWS05SomenteHomolog"].ToString();
                }


                dadosenvio.resultadoRegistroIntegradorEstadual = dados.resultadoRegistroIntegradorEstadual;
                dadosenvio.reciboSolicitacao = dados.reciboSolicitacao;
                dadosenvio.identificacaoSolicitacao = dados.identificacaoSolicitacao;

                c.Url = ConfigurationManager.AppSettings.Get("UrlWs06").ToString();


                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                request.ClientCertificates.Add(cert);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string resp = sr.ReadToEnd();


                try
                {
                    System.Net.ServicePointManager.Expect100Continue = false;

                    ServicePointManager.DefaultConnectionLimit = 100;
                    ServicePointManager.MaxServicePointIdleTime = 5000;

                    c.Timeout = 20000;

                    pResult = c.processarComunicacaoDeferIndefer(dadosenvio);

                    cert.Reset();

                    GlobalV1 trata = new GlobalV1();
                    string codigoRetorno = "";
                    if (pResult.retornoWSRedesim.codigoRetorno != null && pResult.retornoWSRedesim.codigoRetorno != "")
                        codigoRetorno = pResult.retornoWSRedesim.codigoRetorno;

                    if (codigoRetorno == "")
                    {
                        if (pResult.retornoWSRedesim.statusEnvio != null && pResult.retornoWSRedesim.statusEnvio != "")
                            codigoRetorno = pResult.retornoWSRedesim.statusEnvio;
                    }

                    trata.TrataS35TabelaMySql(dadosenvio.codServico, dadosenvio.reciboSolicitacao, dadosenvio.identificacaoSolicitacao, codigoRetorno, "");

                }
                catch (Exception ex)
                {
                    cert.Reset();
                    string XmlDados = GlobalV1.CreateXML(dadosenvio);
                    //string xmlDadosRetorno = GlobalV1.CreateXML(ex);
                    result.codretorno = "99";
                    result.status = "NOK";
                    string detail = "";
                    try
                    {
                        detail = " - " + ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                    }
                    catch { }

                    string Erro = ex.Message + detail;

                    result.descricao = Erro;


                    using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    {
                        e35.t73307_arquivo_RFB = XmlDados;
                        e35.t73307_erro = ex.StackTrace + " Mens Erro " + Erro;
                        e35.t73307_ide_solicitacao = dados.identificacaoSolicitacao;
                        e35.t73307_rec_solicitacao = "ws06";
                        e35.Update();
                    }


                    return result;
                }

                if (pResult.retornoINDEFWSRedesim != null)
                {
                    result.codretorno = pResult.retornoINDEFWSRedesim.codigoRetorno;
                    result.status = pResult.retornoINDEFWSRedesim.statusEnvio;
                    result.descricao += "RFB: ";
                    foreach (string RetornoNOK in pResult.retornoINDEFWSRedesim.descricaoRetorno)
                    {
                        result.descricao += " - " + RetornoNOK;
                    }

                    return result;
                }

                if (pResult.retornoWSRedesim.statusEnvio == "OK")
                {
                    result.codretorno = pResult.retornoWSRedesim.codigoRetorno;
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.Cnpj = pResult.retornoWS06Redesim.numeroCnpj;
                    result.Nire = pResult.retornoWS06Redesim.numeroNire;
                    result.nomeEmpresarial = pResult.retornoWS06Redesim.nomeEmpresarial;
                    result.indicadorCNPJNome = dados.IndicadorCNPJNome;

                    if (dados.numeroRegistroCartorio != "")
                    {
                        result.Nire = pResult.retornoWS06Redesim.numeroRegistroCartorio;
                    }
                    result.descricao = GlobalV1.CreateXML(pResult);// pResult.retornoWSRedesim.descricaoRetorno;
                }
                else
                {
                    //string retur = GlobalV1.CreateXML(pResult);
                    result.codretorno = pResult.retornoWSRedesim.codigoRetorno;
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = "RFB: " + pResult.retornoWSRedesim.descricaoRetorno;
                    result.Cnpj = pResult.retornoWSRedesim.numeroCnpj;

                    string XmlDados = GlobalV1.CreateXML(dadosenvio);
                    using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    {
                        e35.t73307_arquivo_RFB = XmlDados;
                        e35.t73307_erro = result.descricao;
                        e35.t73307_ide_solicitacao = dados.identificacaoSolicitacao;
                        e35.t73307_rec_solicitacao = "ws06";
                        e35.Update();
                    }

                }
                //Apaga registro de CNPJ caso exista
                if (result.Cnpj != "")
                {
                    using (T73309_WS11_RFB cc = new T73309_WS11_RFB())
                    {
                        cc.t73309_cnpj = result.Cnpj;
                        cc.DeletePk();
                    }
                }
                try
                {
                    /*
                     * Isto porque a RFB antes devolvia sem zeros a izquerda e o sistema estava mapeado assim
                     * com a publicação do dia 10/10/2018 mudou exxemplo de 4 para 04
                     */
                    if (result.codretorno == "04")
                        result.codretorno = "4";

                    if (result.codretorno == "08")
                        result.codretorno = "8";
                }
                catch
                {
                }

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region ws07
        /// <summary>
        /// Comunicação deferimento na Matriz de Filial pertencente a outra UF
        /// 
        /// O Serviço "S07" ocorre para fechar o fluxo informacional do deferimento que ocorreu na Junta Comercial da Matriz 
        /// para a Junta Comercial da UF onde está localizada a Filial.
        /// </summary>
        /// <param name="Identificacao"></param>
        /// <param name="Recibo"></param>
        /// <returns></returns>
        [WebMethod]
        public RetornoV2 ServiceWs07(string Identificacao, string Recibo)
        {
            RetornoV2 result = new RetornoV2();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                string ResponseRFB = "";


                WsServices07RFB.serviceRequest dadosenvio = new WsServices07RFB.serviceRequest();
                WsServices07RFB.serviceResponse pResult = new WsServices07RFB.serviceResponse();
                using (WsServices07RFB.ws07 c = new WsServices07RFB.ws07())
                {


                    X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                    c.ClientCertificates.Add(cert);

                    dadosenvio.codServico = "S07";
                    dadosenvio.versao = "100000";
                    dadosenvio.identificacaoSolicitacao = Identificacao;
                    dadosenvio.reciboSolicitacao = Recibo;

                    c.Url = ConfigurationManager.AppSettings.Get("UrlWs07").ToString();
                    try
                    {
                        //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                        //request.ClientCertificates.Add(cert);
                        //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        //Stream stream = response.GetResponseStream();
                        //StreamReader sr = new StreamReader(stream);
                        //string resp = sr.ReadToEnd();



                        pResult = c.buscar(dadosenvio);

                        cert.Reset();
                    }
                    catch (Exception ex)
                    {
                        cert.Reset();
                        string detail = "";
                        try
                        {
                            detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                        }
                        catch { }
                        result.codretorno = "99";
                        result.status = "NOK";
                        result.descricao = "Comunicação com a Receita Federal do Brasil fora do ar, tente novamente mais tarde " + ex.Message + " SoapException " + detail.ToString();

                        using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                        {
                            e35.t73307_arquivo_RFB = "";
                            e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                            e35.t73307_ide_solicitacao = Identificacao;
                            e35.t73307_rec_solicitacao = "ws11";
                            e35.Update();
                        }

                        return result;
                    }
                }

                ResponseRFB = GlobalV1.CreateXML(pResult);

                if (pResult.@return.statusEnvio == "OK")
                {
                    result.codretorno = "00";
                    result.status = pResult.@return.statusEnvio;
                    result.descricao = pResult.@return.descricaoRetorno;
                    result.Cnpj = pResult.dadosRedesim.cnpj;
                    result.XmlDBE = ResponseRFB;
                    //result.oWsResponse07 = pResult;

                }
                else
                {
                    result.codretorno = "99";
                    if (pResult.@return.codigoRetorno != null && (pResult.@return.codigoRetorno != ""))
                    {
                        result.codretorno = pResult.@return.codigoRetorno;
                    }
                    result.status = pResult.@return.statusEnvio;
                    result.descricao = "RFB: " + pResult.@return.codigoRetorno + " - " + pResult.@return.descricaoRetorno;

                    if (pResult.@return.descricaoRetorno == "INDEFERIDO")
                    {
                        result.codretorno = "07";
                    }
                }

                return result;
            }

            catch (Exception ex)
            {
                string detail = "";
                try
                {
                    detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                }
                catch { }
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.Message + " StackTrace " + ex.StackTrace + " SoapException " + detail.ToString();

                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = "";
                    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    e35.t73307_ide_solicitacao = Identificacao;
                    e35.t73307_rec_solicitacao = "ws07";
                    e35.Update();
                }
                return result;
            }

        }
        #endregion

        #region ws08
        /// <summary>
        /// </summary>
        /// <param name="Identificacao"></param>
        /// <param name="Recibo"></param>
        /// <param name="cpfRecepcionador"></param>
        /// <returns></returns>
        [WebMethod]
        public Retorno ServiceWs08(string Identificacao, string Recibo)
        {
            Retorno result = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());


                result.codretorno = "00";
                result.status = "OK";
                result.descricao = "";
                result.XmlDBE = "";

                // return result;


                string ResponseRFB = "";

                WsServices08RFB.serviceRequest dadosenvio = new WsServices08RFB.serviceRequest();
                WsServices08RFB.serviceResponse pResult = new WsServices08RFB.serviceResponse();
                WsServices08RFB.ws08 c = new WsServices08RFB.ws08();
                X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                c.ClientCertificates.Add(cert);
                dadosenvio.codServico = "S08";
                //dadosenvio.numeroOcorrencia = "1";
                //dadosenvio.numeroProtocolo = "RJP0000000011";
                dadosenvio.versao = "100000";

                dadosenvio.identificacaoSolicitacao = Identificacao;
                dadosenvio.reciboSolicitacao = Recibo;

                c.Url = ConfigurationManager.AppSettings.Get("UrlWs08").ToString();

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                    request.ClientCertificates.Add(cert);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream);
                    string resp = sr.ReadToEnd();


                    pResult = c.buscar(dadosenvio);
                }
                catch (Exception ex)
                {
                    string detail = "";
                    try
                    {
                        detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                    }
                    catch { }
                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = "Comunicação com a Receita Federal do Brasil fora do ar, tente novamente mais tarde " + ex.Message + " SoapException " + detail.ToString();

                    using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    {
                        e35.t73307_arquivo_RFB = "";
                        e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                        e35.t73307_ide_solicitacao = Identificacao;
                        e35.t73307_rec_solicitacao = "ws08";
                        e35.Update();
                    }
                    return result;
                }

                ResponseRFB = GlobalV1.CreateXML(pResult);

                if (pResult.@return.statusEnvio == "OK")
                {
                    result.codretorno = "00";
                    result.status = pResult.@return.statusEnvio;
                    result.descricao = pResult.@return.descricaoRetorno;
                    result.XmlDBE = ResponseRFB;

                    //using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    //{
                    //    e35.t73307_arquivo_RFB = "";
                    //    e35.t73307_erro = Recibo;
                    //    e35.t73307_ide_solicitacao = Identificacao;
                    //    e35.t73307_rec_solicitacao = "ws08Ok";
                    //    e35.Update();
                    //}
                }
                else
                {
                    result.codretorno = "99";
                    if (pResult.@return.codigoRetorno != null && (pResult.@return.codigoRetorno != ""))
                    {
                        result.codretorno = pResult.@return.codigoRetorno;
                    }
                    result.status = pResult.@return.statusEnvio;
                    result.descricao = "RFB: " + pResult.@return.codigoRetorno + " - " + pResult.@return.descricaoRetorno;
                }
                return result;
            }
            catch (Exception ex)
            {
                string detail = "";
                try
                {
                    detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                }
                catch { }
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.Message + " StackTrace " + ex.StackTrace + " SoapException " + detail.ToString();


                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = "";
                    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    e35.t73307_ide_solicitacao = Identificacao;
                    e35.t73307_rec_solicitacao = "ws08";
                    e35.Update();
                }


                return result;
            }

        }
        #endregion

        #region ws09
        /// <summary>
        /// Codigo de retorno
        /// 0 – Ok
        /// 1 – CPF Inválido 
        /// 2 - CPF não encontrado na base
        /// 9 – Erro Natural/Adabas
        /// </summary>
        /// <param name="Identificacao"></param>
        /// <param name="Recibo"></param>
        /// <param name="cpfRecepcionador"></param>
        /// <returns></returns>
        [WebMethod]
        public Retorno ServiceWs09(string cpf)
        {
            Retorno result = new Retorno();
            try
            {
                /*
                    Situação Cadastral
                 * 0 – Regular
                 * 1 – Cancelada por Encerramento de espólio
                 * 2 – Suspensa
                 * 3 - Cancelada por óbito sem espólio
                 * 4 - Pendente de Regularização
                 * 5 -Cancelada por Multiplicidade
                 * 8 – Nula
                 * 9 – Cancelada de ofício
                 */
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                #region Verifica Ws09 encapsulado
                int diasPrazoBuscaws09 = 0;

                if (ConfigurationManager.AppSettings.Get("diasPrazoBuscaws09") != null && ConfigurationManager.AppSettings.Get("diasPrazoBuscaws09").ToString() != "")
                {
                    try
                    {
                        diasPrazoBuscaws09 = int.Parse(ConfigurationManager.AppSettings.Get("diasPrazoBuscaws09").ToString());
                    }
                    catch { }
                }

                if (diasPrazoBuscaws09 > 0)
                {
                    try
                    {
                        T73309_WS09_RFB pResultWs09 = new T73309_WS09_RFB();

                        pResultWs09.t73309_cpf = cpf;
                        pResultWs09 = pResultWs09.Query();

                        if (pResultWs09.t73309_cpf != null)
                        {
                            if (pResultWs09.t73309_data_consulta > DateTime.Now.AddDays(-diasPrazoBuscaws09))
                            {
                                WsServices09RFB.consultaCPFResponse pResponseNovo = new WsServices09RFB.consultaCPFResponse();

                                result.codretorno = "00";
                                result.status = "OK";
                                result.descricao = "";
                                result.oCPFResponse = (WsServices09RFB.consultaCPFResponse)GlobalV1.CreateObject(pResultWs09.t73309_xml, pResponseNovo);

                                /*
                                    So retorno a informação que esta no arquivo se ele for ativo status = 0
                                 * isso porque o cpf ja esteve suspenso e passa a ser ativo, entao quando nao e ativo
                                 * busco da fonte que e a RFB
                                 */
                                if (result.oCPFResponse.retornoWS09Redesim.dadosCPF[0].situacaoCadastral == "0"
                                    && result.oCPFResponse.retornoWS09Redesim.dadosCPF[0].anoObito == "0000"
                                    )
                                {
                                    result.XmlDBE = pResultWs09.t73309_xml;
                                    return result;
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }

                #endregion

                string ResponseRFB = "";



                string[] pCpf = new string[1];


                pCpf[0] = cpf;

                WsServices09RFB.consultaCPFRequest dadosenvio = new WsServices09RFB.consultaCPFRequest();
                WsServices09RFB.consultaCPFResponse pResult = new WsServices09RFB.consultaCPFResponse();
                WsServices09RFB.WS09Serviceserviceagent c = new WsServices09RFB.WS09Serviceserviceagent();
                X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                c.ClientCertificates.Add(cert);
                dadosenvio.codServico = "S09";
                dadosenvio.cpf = pCpf;
                dadosenvio.numeroOcorrencia = 1;
                dadosenvio.numeroProtocolo = "RJP0000000011";
                dadosenvio.versao = "100000";
                dadosenvio.codEvento = "101";

                dadosenvio.identificacaoSolicitacao = "11111111000191";
                dadosenvio.reciboSolicitacao = "1234567890";


                c.Url = ConfigurationManager.AppSettings.Get("UrlWs09").ToString();

                //string MD5 = GlobalV1.GetWSDL(new Uri(c.Url), cert);
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                    request.ClientCertificates.Add(cert);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream);
                    string resp = sr.ReadToEnd();

                    c.Timeout = 12000;

                    pResult = c.consultaCPF(dadosenvio);
                    cert.Reset();
                }
                catch (Exception ex)
                {
                    cert.Reset();
                    string detail = "";
                    try
                    {
                        detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                    }
                    catch { }
                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = "Comunicação com a Receita Federal do Brasil fora do ar, tente novamente mais tarde " + ex.Message + " SoapException " + detail.ToString();

                    using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    {
                        e35.t73307_arquivo_RFB = "";
                        e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                        e35.t73307_ide_solicitacao = cpf;
                        e35.t73307_rec_solicitacao = "ws09";
                        e35.Update();
                    }


                    return result;
                }

                ResponseRFB = GlobalV1.CreateXML(pResult);

                if (pResult.retornoWSRedesim.statusEnvio == "OK")
                {
                    result.codretorno = "00";
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = pResult.retornoWSRedesim.descricaoRetorno;
                    result.XmlDBE = ResponseRFB;
                    result.oCPFResponse = pResult;

                    if (diasPrazoBuscaws09 > 0)
                    {
                        T73309_WS09_RFB ws09 = new T73309_WS09_RFB();
                        ws09.t73309_cpf = cpf;
                        ws09.t73309_xml = ResponseRFB;
                        ws09.Update();
                    }

                }
                else
                {
                    result.codretorno = "99";
                    if (pResult.retornoWSRedesim.codigoRetorno != null && (pResult.retornoWSRedesim.codigoRetorno != ""))
                    {
                        result.codretorno = pResult.retornoWSRedesim.codigoRetorno;
                    }
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = "RFB: " + pResult.retornoWSRedesim.codigoRetorno + " - " + pResult.retornoWSRedesim.descricaoRetorno;
                }
                return result;
            }
            catch (Exception ex)
            {
                string detail = "";
                try
                {
                    detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                }
                catch { }
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.Message + " StackTrace " + ex.StackTrace + " SoapException " + detail.ToString();


                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = "";
                    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    e35.t73307_ide_solicitacao = cpf;
                    e35.t73307_rec_solicitacao = "ws09";
                    e35.Update();
                }


                return result;
            }

        }
        #endregion

        #region ws11
        /// <summary>
        /// 0  - Retorno OK
        /// 93 – Transação não efetuada – Tente Novamente
        /// 99 - CNPJ não fecha DV ou não encontrado na Base
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        [WebMethod]
        public Retorno ServiceWs11(string cnpj)
        {
            Retorno result = new Retorno();
            try
            {
                /*
                 *Situação Cadastral
                01	Nula
                02	Ativa Regular
                03	Suspensa
                04	Inapta
                05	Ativa Não Regular
                08	Baixada
                 */

                string ResponseRFB = "";


                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                #region Verifica Ws11 encapsulado
                int diasPrazoBuscaws11 = 0;

                if (ConfigurationManager.AppSettings.Get("diasPrazoBuscaws11") != null && ConfigurationManager.AppSettings.Get("diasPrazoBuscaws11").ToString() != "")
                {
                    try
                    {
                        diasPrazoBuscaws11 = int.Parse(ConfigurationManager.AppSettings.Get("diasPrazoBuscaws11").ToString());
                    }
                    catch { }
                }

                if (diasPrazoBuscaws11 > 0)
                {
                    try
                    {
                        T73309_WS11_RFB pResultWs11 = new T73309_WS11_RFB();

                        pResultWs11.t73309_cnpj = cnpj;
                        pResultWs11 = pResultWs11.Query();

                        if (pResultWs11.t73309_cnpj != null)
                        {
                            //if (pResultWs11.t73309_data_consulta > DateTime.Now.AddDays(-diasPrazoBuscaws09))
                            //{
                            WsServices11RFB.consultaCNPJResponse pResponseNovo = new WsServices11RFB.consultaCNPJResponse();

                            result.codretorno = "00";
                            result.status = "OK";
                            result.descricao = "";
                            pResponseNovo = (WsServices11RFB.consultaCNPJResponse)GlobalV1.CreateObject(pResultWs11.t73309_xml, pResponseNovo);
                            result.oCNPJResponse = pResponseNovo.retornoWS11Redesim;
                            result.XmlDBE = pResultWs11.t73309_xml;
                            return result;

                        }
                    }
                    catch
                    {

                    }
                }

                #endregion



                WsServices11RFB.consultaCNPJRequest dadosenvio = new WsServices11RFB.consultaCNPJRequest();
                WsServices11RFB.consultaCNPJResponse pResult = new WsServices11RFB.consultaCNPJResponse();
                using (WsServices11RFB.WS11Serviceserviceagent c = new WsServices11RFB.WS11Serviceserviceagent())
                {


                    X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                    c.ClientCertificates.Add(cert);

                    string[] pCnpj = new string[1];

                    pCnpj[0] = cnpj;

                    dadosenvio.codServico = "S11";
                    //dadosenvio.cnpjOrgaoRegistro = "03110616000103";
                    dadosenvio.cnpj = pCnpj;
                    dadosenvio.numeroOcorrencia = 1;
                    dadosenvio.numeroProtocolo = "RJP0000000011";
                    dadosenvio.versao = "100000";
                    dadosenvio.codEvento = "101";
                    dadosenvio.identificacaoSolicitacao = cnpj;
                    dadosenvio.reciboSolicitacao = "1234567890";


                    c.Url = ConfigurationManager.AppSettings.Get("UrlWs11").ToString();
                    try
                    {
                        //  System.Net.ServicePointManager.Expect100Continue = true;

                        //   ServicePointManager.DefaultConnectionLimit = 100;
                        //    ServicePointManager.MaxServicePointIdleTime = 5000;

                        // ServicePointManager.Expect100Continue = true;
                        // ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;

                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                        request.KeepAlive = false;
                        request.ClientCertificates.Add(cert);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream stream = response.GetResponseStream();
                        StreamReader sr = new StreamReader(stream);
                        string resp = sr.ReadToEnd();


                        c.Timeout = 120000;

                        pResult = c.consultaCNPJ(dadosenvio);

                        cert.Reset();
                    }
                    catch (Exception ex)
                    {
                        cert.Reset();
                        string detail = "";
                        try
                        {
                            detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                        }
                        catch { }
                        result.codretorno = "99";
                        result.status = "NOK";
                        result.descricao = "Comunicação com a Receita Federal do Brasil fora do ar, tente novamente mais tarde " + ex.Message + " SoapException " + detail.ToString();

                        using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                        {
                            e35.t73307_arquivo_RFB = "";
                            e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                            e35.t73307_ide_solicitacao = cnpj;
                            e35.t73307_rec_solicitacao = "ws11";
                            e35.Update();
                        }

                        return result;
                    }
                }

                ResponseRFB = GlobalV1.CreateXML(pResult);

                if (pResult.retornoWSRedesim.statusEnvio == "OK")
                {
                    result.codretorno = "00";
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = pResult.retornoWSRedesim.descricaoRetorno;
                    result.XmlDBE = ResponseRFB;

                    if (pResult.retornoWS11Redesim.dadosCNPJ[0].objetoSocial == null)
                    {
                        pResult.retornoWS11Redesim.dadosCNPJ[0].objetoSocial = "";
                        ResponseRFB = GlobalV1.CreateXML(pResult);
                        result.XmlDBE = ResponseRFB;
                    }

                    if (pResult.retornoWS11Redesim.dadosCNPJ[0].cnaeSecundaria == null)
                    {
                        /*
                            Isto aqui e porque a nova url da RFB quando nao tem cnae secundaria o objeto vinha preenchido na url antiga
                         * com a nova nao vem, e tem programmas nao nao tratabam o objeto vazio, com isso estou forçando para colocar sempre esse objeto
                         */
                        string[] pcnaeSecundaria = new string[] { "0000000" };
                        pResult.retornoWS11Redesim.dadosCNPJ[0].cnaeSecundaria = pcnaeSecundaria;
                        ResponseRFB = GlobalV1.CreateXML(pResult);
                        result.XmlDBE = ResponseRFB;
                    }
                    result.oCNPJResponse = pResult.retornoWS11Redesim;

                    if (diasPrazoBuscaws11 > 0)
                    {
                        T73309_WS11_RFB ws11 = new T73309_WS11_RFB();
                        ws11.t73309_cnpj = cnpj;
                        ws11.t73309_xml = ResponseRFB;
                        ws11.Update();
                    }
                }
                else
                {
                    result.codretorno = "99";
                    if (pResult.retornoWSRedesim.codigoRetorno != null && (pResult.retornoWSRedesim.codigoRetorno != ""))
                    {
                        result.codretorno = pResult.retornoWSRedesim.codigoRetorno;
                    }
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = "RFB: " + pResult.retornoWSRedesim.codigoRetorno + " - " + pResult.retornoWSRedesim.descricaoRetorno;
                }

                return result;
            }

            catch (Exception ex)
            {
                string detail = "";
                try
                {
                    detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                }
                catch { }
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.Message + " StackTrace " + ex.StackTrace + " SoapException " + detail.ToString();

                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = "";
                    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    e35.t73307_ide_solicitacao = cnpj;
                    e35.t73307_rec_solicitacao = "ws11";
                    e35.Update();
                }
                return result;
            }

        }
        #endregion

        #region ws13
        /// <summary>
        /// 0  - Retorno OK
        /// 93 – Transação não efetuada – Tente Novamente
        /// 99 - CNPJ não fecha DV ou não encontrado na Base
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        [WebMethod]
        public Retorno ServiceWs13(DadosWs13 dados)
        {
            Retorno result = new Retorno();
            try
            {


                string ResponseRFB = "";

                WsServices13RFB.ws13Request dadosenvio = new WsServices13RFB.ws13Request();
                WsServices13RFB.ws13Response pResult = new WsServices13RFB.ws13Response();
                //WsServices13RFB.mensagemInformativa []mensageminformativa;
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                using (WsServices13RFB.ws13 c = new WsServices13RFB.ws13())
                {


                    X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                    c.ClientCertificates.Add(cert);

                    dadosenvio.codServico = "S13";
                    dadosenvio.identificacaoSolicitacao = dados.identificacaoSolicitacao;
                    //dadosenvio.protocolo = dados.protocolo;
                    //dadosenvio.protocoloOcorrencia = dados.protocoloOcorrencia;
                    dadosenvio.reciboSolicitacao = dados.reciboSolicitacao;
                    dadosenvio.versao = "100000";

                    //dadosenvio.mensagens = dados.mensagemInformativa;

                    /*
                        Aqui e se vem preenchido o arrays de menssagem                 
                     */
                    if (dados.mensagemInformativa != null && dados.mensagemInformativa.Length > 0)
                    {
                        WsServices13RFB.mensagemInformativa[] inconpa = new WsServices13RFB.mensagemInformativa[20];

                        int i = 0;

                        foreach (mensagemInformativa _dadosWs13 in dados.mensagemInformativa)
                        {
                            if (_dadosWs13 != null && _dadosWs13.nomeOrgaoResponsavel != "" && _dadosWs13.texto.Trim() != "")
                            {

                                WsServices13RFB.mensagemInformativa inconpa2 = new WsServices13RFB.mensagemInformativa();
                                inconpa2.nomeOrgaoResponsavel = _dadosWs13.nomeOrgaoResponsavel;
                                inconpa2.texto = _dadosWs13.texto;
                                inconpa2.link = _dadosWs13.link;
                                inconpa.SetValue(inconpa2, i);
                                i++;
                            }
                        }

                        dadosenvio.mensagem = inconpa;
                    }
                    /*
                        Aqui e se vem uma unica linha de menssagem
                     */
                    if (dados.mensagemInformativaUnica.nomeOrgaoResponsavel != "" && dados.mensagemInformativaUnica.texto != "")
                    {
                        WsServices13RFB.mensagemInformativa[] inconpa = new WsServices13RFB.mensagemInformativa[1];

                        WsServices13RFB.mensagemInformativa inconpa2 = new WsServices13RFB.mensagemInformativa();
                        inconpa2.nomeOrgaoResponsavel = dados.mensagemInformativaUnica.nomeOrgaoResponsavel;
                        inconpa2.texto = dados.mensagemInformativaUnica.texto;
                        inconpa2.link = dados.mensagemInformativaUnica.link;
                        inconpa.SetValue(inconpa2, 0);
                        dadosenvio.mensagem = inconpa;
                    }

                    c.Url = ConfigurationManager.AppSettings.Get("UrlWs13").ToString();
                    try
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                        request.ClientCertificates.Add(cert);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream stream = response.GetResponseStream();
                        StreamReader sr = new StreamReader(stream);
                        string resp = sr.ReadToEnd();

                        pResult = c.enviarMensagem(dadosenvio);

                        cert.Reset();
                    }
                    catch (Exception ex)
                    {
                        cert.Reset();
                        string detail = "";
                        try
                        {
                            detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                        }
                        catch { }
                        result.codretorno = "99";
                        result.status = "NOK";
                        result.descricao = "Comunicação com a Receita Federal do Brasil fora do ar, tente novamente mais tarde " + ex.Message + " SoapException " + detail.ToString();

                        using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                        {
                            e35.t73307_arquivo_RFB = "";
                            e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                            e35.t73307_ide_solicitacao = dados.reciboSolicitacao;
                            e35.t73307_rec_solicitacao = "ws13";
                            e35.Update();
                        }

                        return result;
                    }
                }

                ResponseRFB = GlobalV1.CreateXML(pResult);

                if (pResult.statusEnvio == "OK")
                {
                    result.codretorno = pResult.mensagemRetorno[0].codigoRetorno;
                    result.status = pResult.statusEnvio;
                    result.descricao = pResult.mensagemRetorno[0].descricaoRetorno;
                    result.XmlDBE = ResponseRFB;
                }
                else
                {
                    if (pResult.mensagemRetorno[0].codigoRetorno == "03" || pResult.mensagemRetorno[0].codigoRetorno == "04")
                    {
                        pResult.statusEnvio = "OK";
                    }
                    result.codretorno = pResult.mensagemRetorno[0].codigoRetorno;
                    result.status = pResult.statusEnvio;
                    result.descricao = pResult.mensagemRetorno[0].descricaoRetorno;
                    //result.descricao = "RFB: " + pResult.mensagemRetorno;
                }

                return result;
            }

            catch (Exception ex)
            {
                string detail = "";
                try
                {
                    detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                }
                catch { }
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.Message + " StackTrace " + ex.StackTrace + " SoapException " + detail.ToString();

                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = "";
                    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    e35.t73307_ide_solicitacao = dados.reciboSolicitacao;
                    e35.t73307_rec_solicitacao = "ws13";
                    e35.Update();
                }
                return result;
            }

        }
        #endregion


        #region ws15
        /// <summary>
        /// Envio de atos de Interesse do Integrador Estadual sobre MEI e Simples Nacional pelo sistema Integrador Nacional
        /// 
        /// serviço “S15” destina-se ao envio de atos de Interesse, pelo Sistema Integrador Nacional, para o Sistema Integrador Estadual exclusivamente para atos praticados 
        /// de pelo MEI utilizando o Portal do Empreendedor e nas opções,     /// alterações e exclusão de período do Simples Nacional (evento 327). 
        /// Ocorre, ainda, na alteração do nome do MEI originado pela alteração do nome do cidadão na base CPF.
        /// </summary>
        /// <param name="Identificacao"></param>
        /// <param name="Recibo"></param>
        /// <returns></returns>
        [WebMethod]
        public RetornoV2 ServiceWs15(string Identificacao, string Recibo, string numeroAtoOficio, string convenioAtoOficio)
        {
            RetornoV2 result = new RetornoV2();
            try
            {

                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();

                result.Identificacao = Identificacao;
                result.Recibo = Recibo;


                WsServices15RFB.ws15 c = new WsServices15RFB.ws15();

                X509Certificate cert = getCertificado(ConfigurationManager.AppSettings["DiretorioCertificado"].ToString(), ConfigurationManager.AppSettings["SenhaArquivo"].ToString());

                c.ClientCertificates.Add(cert);

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                c.Url = ConfigurationManager.AppSettings["UrlWs15"].ToString();

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                request.ClientCertificates.Add(cert);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string resp = sr.ReadToEnd();
                /*
                "01", "Número de solicitação invalido/inexistente");
                "04", "Solicitação está Indeferida!");
                "05", "Solicitação está Cancelada!");
                "07", "Solicitação encontra-se em andamento.");
                "08", "Solicitação em fase de preenchimento.");
                "09", "Solicitação está com DBE disponibilizado");
                "10", "Solicitação foi deferida.";
                "11", "Solicitação não é de deferimento da Junta Comercial.");
                */

                WsServices15RFB.serviceRequest dadosenvio = new WsServices15RFB.serviceRequest();
                WsServices15RFB.serviceResponse pResult = new WsServices15RFB.serviceResponse();

                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();

                dadosenvio.codServico = "S15"; //WSWS4115E
                dadosenvio.versao = "100000";

                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();
                numeroAtoOficio = numeroAtoOficio.ToUpper();
                convenioAtoOficio = convenioAtoOficio.ToUpper();

                if (numeroAtoOficio != "")
                {
                    dadosenvio.numeroAtoOficio = numeroAtoOficio;
                    dadosenvio.convenioAtoOficio = "SRF";
                }

                if (convenioAtoOficio != "")
                    dadosenvio.convenioAtoOficio = convenioAtoOficio;

                if (Identificacao != "")
                    dadosenvio.identificacaoSolicitacao = Identificacao;

                if (Recibo != "")
                    dadosenvio.reciboSolicitacao = Recibo;


                c.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;

                try
                {
                    c.Timeout = 12000;

                    pResult = c.buscarAtosInteresse(dadosenvio);

                    cert.Reset();
                }
                catch (Exception ex)
                {
                    cert.Reset();
                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = "A comunicação com a Receita Federal está indisponível no momento. Por favor tente mais tarde!. (" + ex.Message + ")";
                    return result;
                }

                string ResponseRFB = GlobalV1.CreateXML(pResult);
                if (pResult.@return.statusEnvio == "OK")
                {
                    if (pResult.dadosRedesim.cnpj == null)
                    {
                        pResult.dadosRedesim.cnpj = "";

                        ResponseRFB = GlobalV1.CreateXML(pResult);
                    }


                    result.codretorno = pResult.@return.codigoRetorno;
                    result.status = pResult.@return.statusEnvio;
                    result.descricao = pResult.@return.descricaoRetorno;
                    result.XmlDBE = ResponseRFB;
                    //result.oWsResponse15 = pResult;
                    result.Identificacao = Identificacao;
                    result.Recibo = Recibo;
                }
                else
                {

                    result.codretorno = pResult.@return.codigoRetorno;
                    result.status = pResult.@return.statusEnvio;
                    result.descricao = "RFB: " + pResult.@return.descricaoRetorno;
                    result.XmlDBE = ResponseRFB;
                    //result.oWs35Response = pResult;

                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region ws17
        /// <summary>
        /// Envio dos demais Atos de interesse do Integrador Estadual pelo sistema Integrador Nacional
        /// 
        /// Este serviço “S17” destina-se ao envio ao envio dos demais atos de Interessepara o Sistema Integrador Estadual. 
        /// Ocorre para Atos deferido, efetuado pelo cidadão utilizando o Coletor Nacional ou para Ato de ofício praticado pela Receita Federal do Brasil. 
        /// Exemplos: Para eventos privativos de matriz, após o seu deferimento, deverão ser enviados atos informativos para os Sistemas Integradores Estaduais 
        /// onde estão localizadas as suas filiais; Para Informação de Marcação de Interesse de estabelecimento localizado em outro estado; 
        /// Para os atos de oficio praticados no CNPJ.
        /// </summary>
        /// <param name="Identificacao"></param>
        /// <param name="Recibo"></param>
        /// <returns></returns>
        [WebMethod]
        public RetornoV2 ServiceWs17(string Identificacao, string Recibo, string numeroAtoOficio, string convenioAtoOficio)
        {
            RetornoV2 result = new RetornoV2();
            try
            {

                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                if (Recibo == "")
                    Identificacao = "";

                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();

                convenioAtoOficio = convenioAtoOficio.ToUpper();
                numeroAtoOficio = numeroAtoOficio.ToUpper();

                if (Identificacao != "")
                    result.Identificacao = Identificacao;

                if (Recibo != "")
                    result.Recibo = Recibo;


                WsServices17RFB.ws17 c = new WsServices17RFB.ws17();

                X509Certificate cert = getCertificado(ConfigurationManager.AppSettings["DiretorioCertificado"].ToString(), ConfigurationManager.AppSettings["SenhaArquivo"].ToString());

                c.ClientCertificates.Add(cert);

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                c.Url = ConfigurationManager.AppSettings["UrlWs17"].ToString();

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                request.ClientCertificates.Add(cert);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                string resp = sr.ReadToEnd();
                /*
                "01", "Número de solicitação invalido/inexistente");
                "04", "Solicitação está Indeferida!");
                "05", "Solicitação está Cancelada!");
                "07", "Solicitação encontra-se em andamento.");
                "08", "Solicitação em fase de preenchimento.");
                "09", "Solicitação está com DBE disponibilizado");
                "10", "Solicitação foi deferida.";
                "11", "Solicitação não é de deferimento da Junta Comercial.");
                */

                WsServices17RFB.serviceRequest dadosenvio = new WsServices17RFB.serviceRequest();
                WsServices17RFB.serviceResponse pResult = new WsServices17RFB.serviceResponse();

                dadosenvio.versao = "100000";
                dadosenvio.codServico = "S17"; //WSWS4115E

                Identificacao = Identificacao.ToUpper();
                Recibo = Recibo.ToUpper();
                numeroAtoOficio = numeroAtoOficio.ToUpper();
                convenioAtoOficio = convenioAtoOficio.ToUpper();

                if (numeroAtoOficio != "")
                {
                    dadosenvio.numeroAtoOficio = numeroAtoOficio;
                    dadosenvio.convenioAtoOficio = "SRF";
                }

                if (convenioAtoOficio != "")
                    dadosenvio.convenioAtoOficio = convenioAtoOficio;

                if (Identificacao != "")
                    dadosenvio.identificacaoSolicitacao = Identificacao;

                if (Recibo != "")
                    dadosenvio.reciboSolicitacao = Recibo;


                c.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;

                try
                {
                    c.Timeout = 12000;

                    pResult = c.buscar(dadosenvio);

                    cert.Reset();
                }
                catch (Exception ex)
                {
                    cert.Reset();
                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = "A comunicação com a Receita Federal está indisponível no momento. Por favor tente mais tarde!. (" + ex.Message + ")";
                    return result;
                }

                string ResponseRFB = GlobalV1.CreateXML(pResult);
                if (pResult.@return.statusEnvio == "OK")
                {

                    result.codretorno = pResult.@return.codigoRetorno;
                    result.status = pResult.@return.statusEnvio;
                    result.Cnpj = pResult.dadosRedesim.cnpj;
                    result.descricao = pResult.@return.descricaoRetorno;
                    result.XmlDBE = ResponseRFB;
                    //result.oWsResponse17 = pResult;
                    result.Identificacao = Identificacao;
                    result.Recibo = Recibo;


                }
                else
                {
                    result.codretorno = pResult.@return.codigoRetorno;
                    result.status = pResult.@return.statusEnvio;
                    result.descricao = "RFB: " + pResult.@return.descricaoRetorno;
                    result.XmlDBE = ResponseRFB;

                    if (pResult.@return.descricaoRetorno == "Solicitação encontra-se em andamento.")
                    {
                        result.codretorno = "07";
                    }



                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region ws22
        [WebMethod]
        public Retorno ServiceWs22(string Identificacao, string Recibo)
        {
            Retorno result = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                result.codretorno = "00";
                result.status = "OK";
                result.descricao = "";
                result.XmlDBE = "";

                return result;

                string ResponseRFB = "";

                WsServices22RFB.comunicarCancelamentoSolicitacaoRequest dadosenvio = new WsServices22RFB.comunicarCancelamentoSolicitacaoRequest();
                WsServices22RFB.comunicarCancelamentoSolicitacaoResponse pResult = new WsServices22RFB.comunicarCancelamentoSolicitacaoResponse();
                WsServices22RFB.WS22Service c = new WsServices22RFB.WS22Service();
                X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                c.ClientCertificates.Add(cert);
                dadosenvio.codServico = "S22";
                //dadosenvio.numeroOcorrencia = "1";
                //dadosenvio.numeroProtocolo = "RJP0000000011";
                dadosenvio.versao = "100000";

                dadosenvio.identificacaoSolicitacao = Identificacao;
                dadosenvio.reciboSolicitacao = Recibo;

                c.Url = ConfigurationManager.AppSettings.Get("UrlWs22").ToString();

                try
                {
                    //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                    //request.ClientCertificates.Add(cert);
                    //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    //Stream stream = response.GetResponseStream();
                    //StreamReader sr = new StreamReader(stream);
                    //string resp = sr.ReadToEnd();


                    pResult = c.comunicarCancelamentoSolicitacao(dadosenvio);
                }
                catch (Exception ex)
                {
                    string detail = "";
                    try
                    {
                        detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                    }
                    catch { }
                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = "Comunicação com a Receita Federal do Brasil fora do ar, tente novamente mais tarde " + ex.Message + " SoapException " + detail.ToString();

                    using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    {
                        e35.t73307_arquivo_RFB = "";
                        e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                        e35.t73307_ide_solicitacao = Identificacao;
                        e35.t73307_rec_solicitacao = "ws08";
                        e35.Update();
                    }
                    return result;
                }

                ResponseRFB = GlobalV1.CreateXML(pResult);

                if (pResult.retornoWSRedesim.statusEnvio == "OK")
                {
                    result.codretorno = "00";
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = pResult.retornoWSRedesim.descricaoRetorno;
                    result.XmlDBE = ResponseRFB;
                    //using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    //{
                    //    e35.t73307_arquivo_RFB = "";
                    //    e35.t73307_erro = Recibo;
                    //    e35.t73307_ide_solicitacao = Identificacao;
                    //    e35.t73307_rec_solicitacao = "ws22Ok";
                    //    e35.Update();
                    //}
                }
                else
                {
                    result.codretorno = "99";
                    if (pResult.retornoWSRedesim.codigoRetorno != null && (pResult.retornoWSRedesim.codigoRetorno != ""))
                    {
                        result.codretorno = pResult.retornoWSRedesim.codigoRetorno;
                    }
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = "RFB: " + pResult.retornoWSRedesim.codigoRetorno + " - " + pResult.retornoWSRedesim.descricaoRetorno;
                }
                return result;
            }
            catch (Exception ex)
            {
                string detail = "";
                try
                {
                    detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                }
                catch { }
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.Message + " StackTrace " + ex.StackTrace + " SoapException " + detail.ToString();


                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = "";
                    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    e35.t73307_ide_solicitacao = Identificacao;
                    e35.t73307_rec_solicitacao = "ws09";
                    e35.Update();
                }


                return result;
            }

        }
        #endregion

        #region ws2 Pega nires de filial na RFB
        [WebMethod]
        public Retorno ServiceWs23(string Identificacao, string Recibo, string Uf)
        {
            Retorno result = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                result.codretorno = "00";
                result.status = "OK";
                result.descricao = "";
                result.XmlDBE = "";


                string ResponseRFB = "";

                WsServices23RFB.ws23Request dadosenvio = new WsServices23RFB.ws23Request();
                WsServices23RFB.ws23Response pResult = new WsServices23RFB.ws23Response();
                WsServices23RFB.ws23 c = new WsServices23RFB.ws23();
                X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                c.ClientCertificates.Add(cert);
                dadosenvio.codServico = "S23";
                dadosenvio.versao = "100000";

                WsServices23RFB.geradorNire[] gerNire = new WsServices23RFB.geradorNire[0];

                int i = 0;
                WsServices23RFB.geradorNire gerNireDeta = new WsServices23RFB.geradorNire();
                gerNireDeta.quantidade = "1";
                gerNireDeta.uf = Uf;
                gerNire.SetValue(gerNireDeta, i);

                dadosenvio.geradorNire = gerNire;

                dadosenvio.identificacaoSolicitacao = Identificacao;
                dadosenvio.reciboSolicitacao = Recibo;

                c.Url = ConfigurationManager.AppSettings.Get("UrlWs23").ToString();

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                    request.ClientCertificates.Add(cert);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream);
                    string resp = sr.ReadToEnd();


                    pResult = c.encaminharNire(dadosenvio);


                }
                catch (Exception ex)
                {
                    string detail = "";
                    try
                    {
                        detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                    }
                    catch { }
                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = "Comunicação com a Receita Federal do Brasil fora do ar, tente novamente mais tarde " + ex.Message + " SoapException " + detail.ToString();

                    using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    {
                        e35.t73307_arquivo_RFB = "";
                        e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                        e35.t73307_ide_solicitacao = Identificacao;
                        e35.t73307_rec_solicitacao = "ws23";
                        e35.Update();
                    }
                    return result;
                }

                ResponseRFB = GlobalV1.CreateXML(pResult);

                if (pResult.statusEnvio == "OK")
                {
                    result.Nire = pResult.niresGerados[0].nires[0].ToUpper();
                    result.codretorno = "00";
                    result.XmlDBE = ResponseRFB;
                }
                else
                {
                    result.codretorno = "99";
                    result.status = pResult.statusEnvio;
                    result.descricao = "RFB: " + pResult.mensagem;
                }
                return result;
            }
            catch (Exception ex)
            {
                string detail = "";
                try
                {
                    detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                }
                catch { }
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.Message + " StackTrace " + ex.StackTrace + " SoapException " + detail.ToString();

                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = "";
                    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    e35.t73307_ide_solicitacao = Identificacao;
                    e35.t73307_rec_solicitacao = "ws23";
                    e35.Update();
                }


                return result;
            }

        }
        #endregion

        #region ws24
        /// <summary>
        /// Marcar = 1 - Sim
        ///          2 = Não
        /// </summary>
        /// <param name="cnpjOrgaoRegistro"></param>
        /// <param name="cnpjEmpresa"></param>
        /// <param name="Marcar"></param>
        /// <returns></returns>
        [WebMethod]
        public RetornoBasico ServiceWs24(string cnpjOrgaoRegistro, string cnpjEmpresa, string Marcar)
        {
            RetornoBasico result = new RetornoBasico();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                if (Marcar != "1" && Marcar != "2")
                {
                    result.status = "NOK";
                    result.codretorno = "99";
                    result.descricao = "Campo marcar tem que ter valor 1 ou 2";
                    return result;
                }

                if (cnpjOrgaoRegistro.Length != 14)
                {
                    result.status = "NOK";
                    result.codretorno = "99";
                    result.descricao = "Campo cnpjOrgaoRegistro formato invalido";
                    return result;
                }

                if (cnpjEmpresa.Length != 14)
                {
                    result.status = "NOK";
                    result.codretorno = "99";
                    result.descricao = "Campo cnpjEmpresa formato invalido";
                    return result;
                }

                string ResponseRFB = "";

                WsServices24RFB.ws24Request dadosenvio = new WsServices24RFB.ws24Request();
                WsServices24RFB.ws24Response pResult = new WsServices24RFB.ws24Response();
                using (WsServices24RFB.ws24 c = new WsServices24RFB.ws24())
                {

                    X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                    dadosenvio.codServico = "S24";
                    dadosenvio.cnpj = cnpjEmpresa;
                    dadosenvio.versao = "100000";
                    dadosenvio.marcacaoInteresse = Marcar;

                    c.Url = ConfigurationManager.AppSettings.Get("UrlWs24").ToString();
                    try
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                        request.ClientCertificates.Add(cert);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream stream = response.GetResponseStream();
                        StreamReader sr = new StreamReader(stream);
                        string resp = sr.ReadToEnd();


                        c.ClientCertificates.Add(cert);
                        pResult = c.atualizarInteresseEstabelecimento(dadosenvio);

                        ResponseRFB = GlobalV1.CreateXML(pResult);
                        //result.XmlDBE = ResponseRFB;

                        if (pResult.statusEnvio == "OK")
                        {
                            T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35();
                            {
                                e35.UpdateSolicitaS24(cnpjOrgaoRegistro, cnpjEmpresa, Marcar);
                            }
                        }
                        if (pResult.mensagemRetorno != null)
                        {
                            if (pResult.mensagemRetorno[0].codigoRetorno == "53" || pResult.mensagemRetorno[0].codigoRetorno == "54")
                            {
                                T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35();
                                {
                                    e35.UpdateSolicitaS24(cnpjOrgaoRegistro, cnpjEmpresa, Marcar);
                                }
                            }
                        }

                        result.status = pResult.statusEnvio;
                        if (pResult.mensagemRetorno != null)
                        {
                            result.descricao = pResult.mensagemRetorno[0].descricaoRetorno;
                            result.codretorno = pResult.mensagemRetorno[0].codigoRetorno;
                        }

                        cert.Reset();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        cert.Reset();
                        string detail = "";
                        try
                        {
                            detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                        }
                        catch { }
                        result.codretorno = "99";
                        result.status = "NOK";
                        result.descricao = "Comunicação com a Receita Federal do Brasil fora do ar, tente novamente mais tarde " + ex.Message + " SoapException " + detail.ToString();

                        using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                        {
                            e35.t73307_arquivo_RFB = "";
                            e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                            e35.t73307_ide_solicitacao = cnpjEmpresa;
                            e35.t73307_rec_solicitacao = "ws24";
                            e35.Update();
                        }

                        return result;
                    }
                }
            }

            catch (Exception ex)
            {
                string detail = "";
                try
                {
                    detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                }
                catch { }
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.Message + " StackTrace " + ex.StackTrace + " SoapException " + detail.ToString();

                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = "";
                    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    e35.t73307_ide_solicitacao = cnpjEmpresa;
                    e35.t73307_rec_solicitacao = "ws24";
                    e35.Update();
                }
                return result;
            }

        }
        #endregion

        #region Ws50 Envio de licencias

        #region Servicos Processos RFB
        [WebMethod]
        public Retorno ServiceWs50(string TipoLicenciaInscricao, string SupertipoRFB, string cpfAnalista, string cnpjEmpresa, string cnpjOrgao, string nomeOrgao,
                                            string identificacaoSolicitacao, string reciboSolicitacao,
                                            string protocoloRedesim, string situacao, string dataHoraEmissao, string dataValidade, string dataAlteracao,
                                            string codMunicipio, string uf,
                                            string link, string numero, string nomeLicenca, string motivo, string TipoInstituicaoRegin, DataSet dsTabelasCnae
                                            )
        {
            try
            {
                Retorno result = new Retorno();

                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                WsServices50RFB.s50Request dadosenvio = new WsServices50RFB.s50Request();
                WsServices50RFB.DadosRFB dadosRFB = new WsServices50RFB.DadosRFB();
                WsServices50RFB.Licenca dadosLicenca = new WsServices50RFB.Licenca();


                WsServices50RFB.LicencaEstabelecimento EstabdadosLicenca = new WsServices50RFB.LicencaEstabelecimento();


                WsServices50RFB.InscricaoTributaria inscricaoTributaria = new WsServices50RFB.InscricaoTributaria();
                WsServices50RFB.InscricaoTributaria[] ArrayinscricaoTributaria = new WsServices50RFB.InscricaoTributaria[1];

                WsServices50RFB.Estabelecimento EstabInscricaoTributaria = new WsServices50RFB.Estabelecimento();
                WsServices50RFB.Estabelecimento[] ArrayEstabinscricaoTributaria = new WsServices50RFB.Estabelecimento[1];

                WsServices50RFB.ProcessoLicenciamento dadosProcesso = new WsServices50RFB.ProcessoLicenciamento();

                WsServices50RFB.AndamentoProcessoLicenciamento andamento = new WsServices50RFB.AndamentoProcessoLicenciamento();

                WsServices50RFB.s50Response pResult = new WsServices50RFB.s50Response();
                WsServices50RFB.S50PortService c = new WsServices50RFB.S50PortService();
                WsServices50RFB.CNAE cnae = new WsServices50RFB.CNAE();

                //Isso e somente uma validação para a licença do bombeiro, para nao enviar uma licença do
                //bombeiro com um CNPJ da uma institução prefeitura por exemplo
                if (SupertipoRFB == "1" && TipoLicenciaInscricao == "1" && TipoInstituicaoRegin != "4")
                {
                    throw new InvalidCastException("Tipo de Instituição Bombeiro diferente ao Supertipo de Bombero", 62);
                }


                if (cpfAnalista == "11111111111" || cpfAnalista == "22222222222" || cpfAnalista == "33333333333" ||
                    cpfAnalista == "44444444444" || cpfAnalista == "55555555555" ||
                    cpfAnalista == "66666666666" || cpfAnalista == "77777777777" ||
                    cpfAnalista == "88888888888" || cpfAnalista == "99999999999")
                    cpfAnalista = "";


                DataTable DtCnaeEmpresa = new DataTable();
                DataTable DtCnaeLicenca = new DataTable();
                DataTable DtCnaeDISPENSADO = new DataTable();
                if (TipoLicenciaInscricao == "1")
                {
                    DtCnaeEmpresa = dsTabelasCnae.Tables[0];
                    DtCnaeLicenca = dsTabelasCnae.Tables[1];
                    DtCnaeDISPENSADO = dsTabelasCnae.Tables[2];

                    if (DtCnaeLicenca.Rows.Count == 0)
                    {
                        DtCnaeLicenca = DtCnaeEmpresa;
                        DtCnaeDISPENSADO = new DataTable();
                    }
                }


                string[] pCpf = new string[1];
                pCpf[0] = cpfAnalista;

                if (cpfAnalista != "")
                    dadosRFB.cpf = pCpf;

                dadosRFB.codEvento = "101";
                dadosRFB.codServico = "S50";
                dadosRFB.versao = "100000";
                dadosRFB.numeroOcorrencia = 1;
                dadosRFB.identificacaoSolicitacao = identificacaoSolicitacao;
                dadosRFB.numeroProtocolo = protocoloRedesim;
                dadosRFB.reciboSolicitacao = reciboSolicitacao;
                //dadosRFB.usoRFB = "";

                dadosenvio.dadosRFB = dadosRFB;



                if (TipoLicenciaInscricao == "1")
                {
                    WsServices50RFB.ProcessoLicenciamento[] arraydadosProcesso = new WsServices50RFB.ProcessoLicenciamento[1];
                    WsServices50RFB.AndamentoProcessoLicenciamento[] arrayAndamento = new WsServices50RFB.AndamentoProcessoLicenciamento[DtCnaeLicenca.Rows.Count];

                    for (int a = 0; a < DtCnaeLicenca.Rows.Count; a++)
                    {
                        //Preencher Processos
                        andamento = PreencherDadosProcesso(protocoloRedesim, SupertipoRFB, cnpjOrgao, nomeOrgao, nomeLicenca, dataAlteracao, motivo, situacao, DtCnaeLicenca.Rows[a]["CodCnae"].ToString());
                        arrayAndamento.SetValue(andamento, a);
                        dadosProcesso.andamentoProcessoLicenciamento = arrayAndamento;

                    }

                    dadosProcesso.cnpjEstabelecimento = cnpjEmpresa;
                    arraydadosProcesso.SetValue(dadosProcesso, 0);

                    dadosenvio.processos = arraydadosProcesso;
                }

                if (TipoLicenciaInscricao == "1")
                {
                    WsServices50RFB.LicencaEstabelecimento[] ArrayEstabdadosLicenca = new WsServices50RFB.LicencaEstabelecimento[1];
                    int pTotalLicencas = DtCnaeLicenca.Rows.Count + DtCnaeDISPENSADO.Rows.Count;
                    WsServices50RFB.Licenca[] ArraydadosLicenca = new WsServices50RFB.Licenca[pTotalLicencas];

                    int totalArray = 0;
                    for (int a = 0; a < DtCnaeLicenca.Rows.Count; a++)
                    {
                        //Prenche licença
                        dadosLicenca = PreencheDadosLicenca(protocoloRedesim, SupertipoRFB, WsServices50RFB.StatusLicencaSemMotivoEnum.REGULAR, cpfAnalista, dataHoraEmissao, dataValidade, link, nomeLicenca, numero, cnpjOrgao, nomeOrgao, DtCnaeLicenca.Rows[a]["CodCnae"].ToString());
                        ArraydadosLicenca.SetValue(dadosLicenca, totalArray);
                        EstabdadosLicenca.licencas = ArraydadosLicenca;
                        totalArray = totalArray + 1;
                    }

                    for (int a = 0; a < DtCnaeDISPENSADO.Rows.Count; a++)
                    {

                        dadosLicenca = PreencheDadosLicenca(protocoloRedesim, SupertipoRFB, WsServices50RFB.StatusLicencaSemMotivoEnum.DISPENSADA, cpfAnalista, dataHoraEmissao, dataValidade, link, nomeLicenca, numero, cnpjOrgao, nomeOrgao, DtCnaeDISPENSADO.Rows[a]["CodCnae"].ToString());
                        ArraydadosLicenca.SetValue(dadosLicenca, totalArray);
                        EstabdadosLicenca.licencas = ArraydadosLicenca;
                        totalArray = totalArray + 1;
                    }



                    EstabdadosLicenca.cnpjEstabelecimento = cnpjEmpresa;
                    ArrayEstabdadosLicenca.SetValue(EstabdadosLicenca, 0);
                    dadosenvio.licencas = ArrayEstabdadosLicenca;
                }

                //Prenche Inscriçoes
                if (TipoLicenciaInscricao == "2")
                {
                    inscricaoTributaria = PreecheDadosInscricoes(cnpjOrgao, nomeOrgao, protocoloRedesim, SupertipoRFB, numero, codMunicipio, cpfAnalista, dataAlteracao, dataHoraEmissao, link, uf);
                    ArrayinscricaoTributaria.SetValue(inscricaoTributaria, 0);

                    EstabInscricaoTributaria.cnpj = cnpjEmpresa;
                    EstabInscricaoTributaria.inscricoes = ArrayinscricaoTributaria;

                    ArrayEstabinscricaoTributaria.SetValue(EstabInscricaoTributaria, 0);
                    dadosenvio.inscricoes = ArrayEstabinscricaoTributaria;
                }



                //string XmlDados = GlobalV1.CreateXML(dadosenvio);

                //return result;
                X509Certificate cert = new X509Certificate();
                try
                {
                    c.Url = ConfigurationManager.AppSettings.Get("UrlWs50").ToString();

                    cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                    c.ClientCertificates.Add(cert);

                    //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                    //request.ClientCertificates.Add(cert);
                    //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    //Stream stream = response.GetResponseStream();
                    //StreamReader sr = new StreamReader(stream);
                    //string resp = sr.ReadToEnd();

                    //O wsdl do webservice está direcionando para o lugar errado. Ele direciona para um endereço de consumo via browse (texto ou ISO-8859-1) ao invés 
                    //do endereço que espera o xml soap.
                    //Obrigado.

                    string XmlDados = GlobalV1.CreateXML(dadosenvio);

                    c.Timeout = 12000;

                    pResult = c.s50(dadosenvio);

                    if (pResult.retornoRedesim.statusEnvio == "OK")
                    {
                        pResult.retornoRedesim.descricaoRetorno = XmlDados;
                    }


                    cert.Reset();

                }
                catch (Exception ex)
                {
                    cert.Reset();
                    string detail = "";
                    try
                    {
                        detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                    }
                    catch { }
                    result.codretorno = "99";
                    result.status = "NOK";
                    result.descricao = "Comunicação com a Receita Federal do Brasil fora do ar, tente novamente mais tarde " + ex.Message + " SoapException " + detail.ToString();

                    //using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    //{
                    //    e35.t73307_arquivo_RFB = "";
                    //    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    //    e35.t73307_ide_solicitacao = identificacaoSolicitacao;
                    //    e35.t73307_rec_solicitacao = "ws50";
                    //    e35.Update();
                    //}
                    return result;
                }

                if (pResult.retornoRedesim.statusEnvio == "OK")
                {
                    result.codretorno = "00";
                    result.status = pResult.retornoRedesim.statusEnvio;
                    result.descricao = pResult.retornoRedesim.descricaoRetorno;
                    //       result.XmlDBE = ResponseRFB;

                }
                else
                {
                    result.codretorno = "99";
                    //if (pResult.retornoRedesim.codigoRetorno != null && (pResult.retornoRedesim.codigoRetorno != ""))
                    //{
                    //    result.codretorno = pResult.retornoRedesim.codigoRetorno;
                    //}
                    result.status = pResult.retornoRedesim.statusEnvio;
                    result.descricao = "RFB: " + pResult.retornoRedesim.codigoRetorno + " - " + pResult.retornoRedesim.descricaoRetorno;
                }
                return result;
            }
            catch (Exception ex)
            {
                Retorno result = new Retorno();
                result.codretorno = "88";
                if (ex.HResult > 0)
                    result.codretorno = ex.HResult.ToString();

                result.status = "NOK";
                result.descricao = ex.Message;
                return result;
            }
        }
        #endregion

        #region PreencherDadosProcesso 
        /// <summary>

        /// </summary>
        /// <returns></returns>
        private WsServices50RFB.AndamentoProcessoLicenciamento PreencherDadosProcesso(string protocoloRedesim, string SupertipoRFB, string cnpjOrgao, string nomeOrgao,
                                                                                       string nomeLicenca,
                                                                                        string dataModificacaoSituacao,
                                                                                        string motivo, string situacao,
                                                                                        string codigoCnae)
        {
            Retorno result = new Retorno();

            WsServices50RFB.IdentificadorProcessoLicenciamento identificadorProcessoLicenciamento = new WsServices50RFB.IdentificadorProcessoLicenciamento();
            WsServices50RFB.SituacaoProcessoLicenciamento situacaoProcessoLicenciamento = new WsServices50RFB.SituacaoProcessoLicenciamento();
            WsServices50RFB.SituacaoProcesso situacaoProcesso = new WsServices50RFB.SituacaoProcesso();

            WsServices50RFB.AndamentoProcessoLicenciamento[] arrayAndamento = new WsServices50RFB.AndamentoProcessoLicenciamento[1];
            WsServices50RFB.AndamentoProcessoLicenciamento andamento = new WsServices50RFB.AndamentoProcessoLicenciamento();

            WsServices50RFB.ClassificacaoRisco cRisgo = new WsServices50RFB.ClassificacaoRisco();
            WsServices50RFB.CNAE cnae = new WsServices50RFB.CNAE();


            //ALVARA_FUNCIONAMENTO_MUNICIPAL = 0,
            //CORPO_DE_BOMBEIROS = 1,
            //VIGILANCIA_SANITARIA = 2,
            //MEIO_AMBIENTE = 3,

            if (SupertipoRFB == "0")
                identificadorProcessoLicenciamento.superTipo = WsServices50RFB.SurperTipo.ALVARA_FUNCIONAMENTO_MUNICIPAL;

            if (SupertipoRFB == "1")
                identificadorProcessoLicenciamento.superTipo = WsServices50RFB.SurperTipo.CORPO_DE_BOMBEIROS;

            if (SupertipoRFB == "2")
                identificadorProcessoLicenciamento.superTipo = WsServices50RFB.SurperTipo.VIGILANCIA_SANITARIA;

            if (SupertipoRFB == "3")
                identificadorProcessoLicenciamento.superTipo = WsServices50RFB.SurperTipo.MEIO_AMBIENTE;



            cRisgo.situacao = WsServices50RFB.ClassificacaoRiscoType.BAIXO;
            cRisgo.motivo = "MOTIVO DO GRAU DE RISCO NÃO INFORMADO";



            if (codigoCnae != "")
            {
                cnae.codigo = codigoCnae;
                //cnae.descricao = nomeCnae;

            }

            identificadorProcessoLicenciamento.atividadeEconomica = cnae;

            WsServices50RFB.Orgao pOrgao = new WsServices50RFB.Orgao();
            pOrgao.cnpj = cnpjOrgao;
            pOrgao.nome = nomeOrgao;

            identificadorProcessoLicenciamento.orgao = pOrgao;


            identificadorProcessoLicenciamento.protocoloRedesim = protocoloRedesim;
            identificadorProcessoLicenciamento.tipoLicenca = nomeLicenca;


            andamento.identificadorProcessoLicenciamento = identificadorProcessoLicenciamento;


            //Campo situacao
            //NAO_INICIADO = 0,
            //DEFERIDO = 1,
            //INDEFERIDO = 2,
            //CANCELADO = 3,
            //DISPENSADO = 4,
            //EM_ANDAMENTO = 5,
            //EM_EXIGENCIA = 6,
            //NAO_INTEGRADO = 7,
            situacaoProcessoLicenciamento.classificacaoRisco = cRisgo;
            situacaoProcessoLicenciamento.dataModificacaoSituacao = dataModificacaoSituacao + "000000";
            if (motivo == "")
                motivo = motivo + " Sem Motivo";

            situacaoProcesso.motivo = motivo;

            situacaoProcesso.situacao = WsServices50RFB.SituacaoProcessoType.DEFERIDO;
            situacaoProcessoLicenciamento.situacaoProcesso = situacaoProcesso;

            andamento.situacaoProcessoLicenciamento = situacaoProcessoLicenciamento;

            return andamento;


        }
        #endregion

        #region Servicos Licencias
        /*
         * string cpf, string cnpjEmpresa, string cnpjOrgao, string nomeOrgao,
                                            string identificacaoSolicitacao, string reciboSolicitacao,
                                            string numeroProtocolo, string dataHoraEmissao,
                                            string codMunicipio, string uf,
                                            string link, string numero, string orgao, string cpfAnalista
         * 
         * */
        private WsServices50RFB.Licenca PreencheDadosLicenca(string protocoloRedesim, string SupertipoRFB, WsServices50RFB.StatusLicencaSemMotivoEnum statusLicenca, string cpfAnalista, string dataLicenca, string datavalidade, string link, string nomeLicenca, string numero,
                                                            string cnpjOrgao, string nomeOrgao, string codigoCnae)
        {
            Retorno result = new Retorno();

            //WsServices50RFB.Licenca dadosEstab = new WsServices50RFB.Licenca();
            WsServices50RFB.Licenca Licenca = new WsServices50RFB.Licenca();
            WsServices50RFB.Licenca[] arrayLicenca = new WsServices50RFB.Licenca[1];
            WsServices50RFB.StatusLicenca StatusLicenca = new WsServices50RFB.StatusLicenca();

            WsServices50RFB.SituacaoLicenca situacaoLicenca = new WsServices50RFB.SituacaoLicenca();
            WsServices50RFB.IdentificadorLicenca identificadorLicenca = new WsServices50RFB.IdentificadorLicenca();
            WsServices50RFB.CNAE atividadeEconomica = new WsServices50RFB.CNAE();
            WsServices50RFB.InformacoesLicenca informacoes = new WsServices50RFB.InformacoesLicenca();


            //ALVARA_FUNCIONAMENTO_MUNICIPAL = 0,
            //CORPO_DE_BOMBEIROS = 1,
            //VIGILANCIA_SANITARIA = 2,
            //MEIO_AMBIENTE = 3,

            if (SupertipoRFB == "0")
                identificadorLicenca.superTipo = WsServices50RFB.SurperTipo1.ALVARA_FUNCIONAMENTO_MUNICIPAL;

            if (SupertipoRFB == "1")
                identificadorLicenca.superTipo = WsServices50RFB.SurperTipo1.CORPO_DE_BOMBEIROS;

            if (SupertipoRFB == "2")
                identificadorLicenca.superTipo = WsServices50RFB.SurperTipo1.VIGILANCIA_SANITARIA;

            if (SupertipoRFB == "3")
                identificadorLicenca.superTipo = WsServices50RFB.SurperTipo1.MEIO_AMBIENTE;



            if (cpfAnalista != "")
                situacaoLicenca.cpfAnalista = cpfAnalista;



            if (dataLicenca == "")
                throw new InvalidCastException("Data da licença vazia", 66);

            if (numero.Length < 4)
                throw new InvalidCastException("numero da licença muito pequeno", 67);

            if (dataLicenca != "")
                situacaoLicenca.data = dataLicenca + "000000";

            if (link != "")
                informacoes.link = link;

            informacoes.nomeLicenca = nomeLicenca;
            informacoes.numero = numero;

            if (datavalidade != "")
                informacoes.validade = datavalidade;// +"235959";

            situacaoLicenca.informacoes = informacoes;

            WsServices50RFB.StatusLicencaSemMotivo statusSemMotivo = new WsServices50RFB.StatusLicencaSemMotivo();
            WsServices50RFB.StatusLicencaComMotivo statusComMotivo = new WsServices50RFB.StatusLicencaComMotivo();
            WsServices50RFB.StatusLicenca item = new WsServices50RFB.StatusLicenca();
            if (1 == 1)
            {

                statusSemMotivo.motivo = "";
                statusSemMotivo.situacao = statusLicenca; // WsServices50RFB.StatusLicencaSemMotivoEnum.REGULAR;
                item.Item = statusSemMotivo;
            }
            else
            {
                statusComMotivo.motivo = "";
                statusComMotivo.situacao = WsServices50RFB.StatusLicencaComMotivoEnum.NAO_REGULAR;
                item.Item = statusComMotivo;
            }

            situacaoLicenca.situacao = item;

            Licenca.situacaoLicenca = situacaoLicenca;

            //Licenca.situacaoLicenca



            WsServices50RFB.Orgao pOrgao = new WsServices50RFB.Orgao();




            pOrgao.cnpj = cnpjOrgao;
            pOrgao.nome = nomeOrgao;
            identificadorLicenca.orgao = pOrgao;

            identificadorLicenca.tipoLicenca = nomeLicenca;


            atividadeEconomica.codigo = codigoCnae;
            //atividadeEconomica.descricao = nomeCnae;


            identificadorLicenca.atividadeEconomica = atividadeEconomica;

            identificadorLicenca.protocoloRedesim = protocoloRedesim;

            Licenca.identificadorLicenca = identificadorLicenca;

            return Licenca;


        }
        #endregion



        #region Servicos Inscriçoes Tributarias /Alvaras Numero de Inscrição
        private WsServices50RFB.InscricaoTributaria PreecheDadosInscricoes(string cnpjOrgao, string nomeOrgao, string protocoloRedesim, string SupertipoRFB, string numero, string codMunicipio,
                                                                           string cpfAnalista, string dataHoraAtualizacaoSituacao, string dataHoraEmissao,
                                                                           string link, string Uf)
        {
            Retorno result = new Retorno();

            //WsServices50RFB.Estabelecimento dadosEstab = new WsServices50RFB.Estabelecimento();
            //WsServices50RFB.InscricaoTributaria[] pInscricaoTributaria = new WsServices50RFB.InscricaoTributaria[1];
            WsServices50RFB.InscricaoTributaria pInscricaoTributariaDados = new WsServices50RFB.InscricaoTributaria();
            WsServices50RFB.Orgao pOrgao = new WsServices50RFB.Orgao();
            WsServices50RFB.TipoSituacaoInscricaoTributaria sit = new WsServices50RFB.TipoSituacaoInscricaoTributaria();
            WsServices50RFB.SituacaoInscricaoTributaria situacaoInscricao = new WsServices50RFB.SituacaoInscricaoTributaria();

            //INSCRICAO_TRIBUTARIA_ESTADUAL = 0,
            //INSCRICAO_TRIBUTARIA_MUNICIPAL = 1,

            if (SupertipoRFB == "0")
            {
                pInscricaoTributariaDados.superTipo = WsServices50RFB.SuperTipo.INSCRICAO_TRIBUTARIA_ESTADUAL;
                pInscricaoTributariaDados.tipoInscricao = WsServices50RFB.TipoInscricao.ESTADUAL;
            }
            if (SupertipoRFB == "1")
            {
                pInscricaoTributariaDados.superTipo = WsServices50RFB.SuperTipo.INSCRICAO_TRIBUTARIA_MUNICIPAL;
                pInscricaoTributariaDados.tipoInscricao = WsServices50RFB.TipoInscricao.MUNICIPAL;
            }

            pOrgao.cnpj = cnpjOrgao;
            pOrgao.nome = nomeOrgao;
            pInscricaoTributariaDados.orgao = pOrgao;
            pInscricaoTributariaDados.protocoloRedesim = protocoloRedesim;
            pInscricaoTributariaDados.numero = numero;


            WsServices50RFB.SituacaoInscricaoSemMotivo statusSemMotivo = new WsServices50RFB.SituacaoInscricaoSemMotivo();
            WsServices50RFB.SituacaoInscricaoComMotivo statusComMotivo = new WsServices50RFB.SituacaoInscricaoComMotivo();
            WsServices50RFB.TipoSituacaoInscricaoTributaria item = new WsServices50RFB.TipoSituacaoInscricaoTributaria();
            //WsServices50RFB.TipoSituacaoInscricaoTributaria 

            //WsServices50RFB.situaca
            if (1 == 1)
            {
                statusSemMotivo.motivo = "";
                statusSemMotivo.situacao = WsServices50RFB.SituacaoInscricaoSemMotivoEnum.ATIVA;
                item.Item = statusSemMotivo;
            }
            if (2 == 1)
            {
                statusComMotivo.motivo = "";
                statusComMotivo.situacao = WsServices50RFB.SituacaoInscricaoComMotivoEnum.BAIXADA;
                item.Item = statusComMotivo;
            }

            situacaoInscricao.situacao = item;

            //situacaoInscricao.numero = numero;
            //Request.codMunicipio =

            situacaoInscricao.codigoMunicipio = codMunicipio.PadLeft(5, '0').Substring(0, 4);
            if (cpfAnalista != "")
                situacaoInscricao.cpfAnalista = cpfAnalista;

            situacaoInscricao.dataHoraAtualizacaoSituacao = dataHoraAtualizacaoSituacao + "000000";
            // situacaoInscricao.dataHoraAtualizacaoSituacao = "2002-05-30T09:00:00";
            situacaoInscricao.dataHoraEmissao = dataHoraEmissao + "000000";
            // situacaoInscricao.dataHoraEmissao = "2002-05-30T09:00:00";
            if (link != "")
                situacaoInscricao.link = link;

            situacaoInscricao.uf = Uf;

            pInscricaoTributariaDados.situacaoInscricao = situacaoInscricao;


            return pInscricaoTributariaDados;
        }
        #endregion

        #endregion

        #region ws53
        [WebMethod]
        public RetornoS53 ServiceWs53(string XMLResquest53, string pProtocolo, string cpfSolicitante)
        {
            RetornoS53 result = new RetornoS53();
            WsServices53RFB.serviceRequest dadosenvio = new WsServices53RFB.serviceRequest();
            WsServices53RFB.serviceResponse pResult = new WsServices53RFB.serviceResponse();

            dadosenvio = (WsServices53RFB.serviceRequest)GlobalV1.CreateObject(XMLResquest53, dadosenvio);
            try
            {

                string ResponseRFB = "";

                //WsServices53RFB.serviceRequest dadosenvio = new WsServices53RFB.serviceRequest();

                //WsServices13RFB.mensagemInformativa []mensageminformativa;
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                using (WsServices53RFB.ws53 c = new WsServices53RFB.ws53())
                {


                    X509Certificate cert = getCertificado(ConfigurationManager.AppSettings.Get("DiretorioCertificado").ToString(), ConfigurationManager.AppSettings.Get("SenhaArquivo").ToString());

                    c.ClientCertificates.Add(cert);

                    dadosenvio.codServico = "S53";
                    dadosenvio.versao = "100000";
                    dadosenvio.numeroProtocolo = pProtocolo;
                    dadosenvio.numeroOcorrencia = 1;
                    dadosenvio.cpfPreenchedor = cpfSolicitante;


                    //dadosenvio.mensagens = dados.mensagemInformativa;

                    /*
                        Aqui e se vem preenchido o arrays de menssagem                 
                     */


                    c.Url = ConfigurationManager.AppSettings.Get("UrlWs53").ToString();
                    try
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(c.Url);
                        request.ClientCertificates.Add(cert);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        Stream stream = response.GetResponseStream();
                        StreamReader sr = new StreamReader(stream);
                        string resp = sr.ReadToEnd();

                        pResult = c.enviar(dadosenvio);
                        string XMLResult = GlobalV1.CreateXML(pResult);
                        cert.Reset();
                    }
                    catch (Exception ex)
                    {
                        cert.Reset();
                        string detail = "";
                        try
                        {
                            detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                        }
                        catch { }
                        result.codretorno = "99";
                        result.status = "NOK";
                        result.descricao = "Comunicação com a Receita Federal do Brasil fora do ar, tente novamente mais tarde " + ex.Message + " SoapException " + detail.ToString();

                        using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                        {
                            e35.t73307_arquivo_RFB = "";
                            e35.t73307_arquivo_RFB = GlobalV1.CreateXML(dadosenvio);
                            e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                            e35.t73307_ide_solicitacao = "WS53";
                            e35.t73307_rec_solicitacao = pProtocolo;
                            e35.UpdateS01();
                        }

                        return result;
                    }
                }

                ResponseRFB = GlobalV1.CreateXML(pResult);

                if (pResult.retornoWSRedesim.statusEnvio == "OK")
                {
                    result.codretorno = pResult.retornoWSRedesim.codigoRetorno;
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = pResult.retornoWSRedesim.descricaoRetorno;
                    result.Recibo = pResult.retornoWSRedesim.reciboSolicitacao;
                    result.Identificacao = pResult.retornoWSRedesim.identificacaoSolicitacao;
                    result.mensagemRetorno = PreencheArrayErroS53(pResult);
                    result.XmlRFB = ResponseRFB;

                    //using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    //{
                    //    e35.t73307_arquivo_RFB = GlobalV1.CreateXML(dadosenvio);
                    //    e35.t73307_erro = "";
                    //    e35.t73307_arquivo_regin = ""; //nao temos esse arquivo
                    //    e35.t73307_ide_solicitacao = "WS53OK";
                    //    e35.t73307_rec_solicitacao = pProtocolo;
                    //    e35.UpdateS01();
                    //}

                }
                else
                {

                    result.codretorno = pResult.retornoWSRedesim.codigoRetorno;
                    result.status = pResult.retornoWSRedesim.statusEnvio;
                    result.descricao = pResult.retornoWSRedesim.descricaoRetorno;
                    result.mensagemRetorno = PreencheArrayErroS53(pResult); ;
                    result.XmlRFB = ResponseRFB;

                    using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    {
                        e35.t73307_arquivo_RFB = GlobalV1.CreateXML(dadosenvio);
                        e35.t73307_erro = GlobalV1.CreateXML(pResult);
                        e35.t73307_arquivo_regin = ""; //nao temos esse arquivo
                        e35.t73307_ide_solicitacao = "WS53";
                        e35.t73307_rec_solicitacao = pProtocolo;
                        e35.UpdateS01();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                string detail = "";
                try
                {
                    detail = ((System.Web.Services.Protocols.SoapException)(ex)).Detail.InnerText;
                }
                catch { }
                result.codretorno = "99";
                result.status = "NOK";
                result.descricao = ex.Message + " StackTrace " + ex.StackTrace + " SoapException " + detail.ToString();

                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = GlobalV1.CreateXML(dadosenvio);
                    e35.t73307_erro = GlobalV1.CreateXML(pResult);
                    e35.t73307_arquivo_regin = ""; //nao temos esse arquivo
                    e35.t73307_ide_solicitacao = "WS533";
                    e35.t73307_rec_solicitacao = pProtocolo;
                    e35.UpdateS01();


                }
                return result;
            }

        }

        private incompRegistroIntegradorEstadual[] PreencheArrayErroS53(WsServices53RFB.serviceResponse pResult)
        {

            if (pResult.retornoWSRedesim.mensagemRetorno != null)
            {
                int tamanhoArray = 0;
                foreach (WsServices53RFB.mensagemRetorno RetornoNOK in pResult.retornoWSRedesim.mensagemRetorno)
                {
                    if (RetornoNOK.descricaoRetorno != "")
                    {
                        tamanhoArray += 1;
                    }
                }
                incompRegistroIntegradorEstadual[] codResul = new incompRegistroIntegradorEstadual[tamanhoArray];
                int ss = 0;
                foreach (WsServices53RFB.mensagemRetorno RetornoNOK in pResult.retornoWSRedesim.mensagemRetorno)
                {
                    if (RetornoNOK.descricaoRetorno != "")
                    {
                        incompRegistroIntegradorEstadual cod = new incompRegistroIntegradorEstadual();
                        cod.codigo = RetornoNOK.codigoRetorno;
                        cod.mensagem = RetornoNOK.descricaoRetorno;
                        codResul.SetValue(cod, ss);
                    }
                    ss += 1;
                }
                return codResul;
            }
            return null;
        }

        private incompRegistroIntegradorEstadual[] PreencheArrayMensagemS53(WsServicesReginRFB.incompRegistroIntegradorEstadual[] pResult)
        {

            if (pResult != null)
            {
                int tamanhoArray = 0;
                tamanhoArray = pResult.Length;

                incompRegistroIntegradorEstadual[] codResul = new incompRegistroIntegradorEstadual[tamanhoArray];
                int ss = 0;
                foreach (WsServicesReginRFB.incompRegistroIntegradorEstadual RetornoNOK in pResult)
                {
                    if (RetornoNOK.mensagem != "")
                    {
                        incompRegistroIntegradorEstadual cod = new incompRegistroIntegradorEstadual();
                        cod.codigo = RetornoNOK.codigo;
                        cod.mensagem = RetornoNOK.mensagem;
                        codResul.SetValue(cod, ss);
                    }
                    ss += 1;
                }
                return codResul;
            }
            return null;
        }
        #endregion

        #region Serviços do 53
        /// <summary>
        /// 0  - Retorno OK
        /// 93 – Transação não efetuada – Tente Novamente
        /// 99 - CNPJ não fecha DV ou não encontrado na Base
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>



        [WebMethod]
        public void ProcessaWs53(string pProtocolo)
        {
            dHelperQuery.AtualizaStatus53comErroParaReenvio();

            DataTable dt = dHelperQuery.getEnvioS53(pProtocolo);
            RetornoS53 dadosenvio = new RetornoS53();
            foreach (DataRow r in dt.Rows)
            {
                try
                {
                    dadosenvio = new RetornoS53();
                    dadosenvio = EnviaWs53(r["requerimento"].ToString());
                }
                catch (Exception ex)
                {
                    using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                    {
                        e35.t73307_arquivo_RFB = "";
                        e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                        e35.t73307_ide_solicitacao = dadosenvio.Requerimento;
                        e35.t73307_rec_solicitacao = "ws53";
                        e35.Update();
                    }
                }
            }
        }

        [WebMethod]
        public RetornoS53 EnviaWs53(string pProtocolo)
        {
            RetornoS53 ret = new RetornoS53();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                DataTable DadosEmpresa = dHelperQuery.getdadosComplementaresBU(pProtocolo);
                DataTable DadosContador = dHelperQuery.getdadosContadorBU(pProtocolo);
                DataTable DadosQSA = dHelperQuery.getdadosSociosBU(pProtocolo);

                WsServices53RFB.serviceResponse response = new WsServices53RFB.serviceResponse();
                WsServices53RFB.serviceRequest request = new WsServices53RFB.serviceRequest();

                WsServices53RFB.viabilidade dadosViabilidade = new WsServices53RFB.viabilidade();
                WsServices53RFB.dadosComplementares dadosComplementares = new WsServices53RFB.dadosComplementares();
                WsServices53RFB.quotasEmpresa quotasEmpresa = new WsServices53RFB.quotasEmpresa();
                WsServices53RFB.contato contatoEmpresa = new WsServices53RFB.contato();
                WsServices53RFB.contato contatoContadorPf = new WsServices53RFB.contato();
                WsServices53RFB.endereco enderecoCorrespondencia = new WsServices53RFB.endereco();

                WsServices53RFB.endereco endContadorPf = new WsServices53RFB.endereco();

                DataRow row = DadosEmpresa.Rows[0];
                DataRow rowContador = DadosContador.Rows[0];

                string nrViabilidade = row["numeroViabilidade"].ToString();

                //Recupera os dados do S01 da viabilidade
                WsServices01RFB.processarViabilidadeRequest viabilidadeS01 = RecuperaViabilidadeS01(nrViabilidade);
                string cpfSolicitante = viabilidadeS01.cpfSolicitante;
                string naturezaJuridica = viabilidadeS01.naturezaJuridica;
                dadosViabilidade = TrataViabilidadeS53(viabilidadeS01);

                request.viabilidade = dadosViabilidade;

                #region DadosComplementares
                dadosComplementares.indicadorPrazoDuracaoAtividade = row["indicadorPrazoDuracaoAtividade"].ToString();
                dadosComplementares.empresaComEstabelecimento = row["empresaComEstabelecimento"].ToString();
                dadosComplementares.nomeFantasia = row["nomeFantasia"].ToString();

                dadosComplementares.capitalSocial = row["capitalSocial"].ToString();
                dadosComplementares.capitalIntegralizado = row["capitalIntegralizado"].ToString();
                dadosComplementares.capitalIntegralizar = "0";
                if (decimal.Parse(row["capitalIntegralizar"].ToString()) > 0)
                    dadosComplementares.capitalIntegralizar = row["capitalIntegralizar"].ToString();

                if (naturezaJuridica != "2135" && naturezaJuridica != "2305")
                {
                    quotasEmpresa.qtdeQuotas = row["qtdeQuotas"].ToString();
                    quotasEmpresa.valorQuota = row["valorQuota"].ToString();
                    dadosComplementares.quotasEmpresa = quotasEmpresa;
                }
                contatoEmpresa.dddTelefone1 = row["dddTelefone1"].ToString();
                contatoEmpresa.telefone1 = row["telefone1"].ToString();
                contatoEmpresa.dddTelefone2 = row["dddTelefone2"].ToString();
                contatoEmpresa.dddTelefone2 = row["dddTelefone2"].ToString();
                contatoEmpresa.correioEletronico = row["correioEletronico"].ToString();

                dadosComplementares.codPorteEmpresa = row["codPorteEmpresa"].ToString();
                dadosComplementares.indicadorPrazoDuracaoAtividade = row["indicadorPrazoDuracaoAtividade"].ToString();
                dadosComplementares.dataInicioAtividades = string.IsNullOrEmpty(row["dataInicioAtividades"].ToString()) ? "10010101" : row["dataInicioAtividades"].ToString();
                dadosComplementares.dataTerminoAtividades = string.IsNullOrEmpty(row["dataTerminoAtividades"].ToString()) ? "10010101" : row["dataTerminoAtividades"].ToString();
                //dadosComplementares.dataInicioAtividades = string.IsNullOrEmpty(row["dataInicioAtividades"].ToString()) ? "" : row["dataInicioAtividades"].ToString();
                //dadosComplementares.dataTerminoAtividades = string.IsNullOrEmpty(row["dataTerminoAtividades"].ToString()) ? "" : row["dataTerminoAtividades"].ToString();
                if (row["dataAssinaturaContrato"].ToString() != "")
                    dadosComplementares.dataAssinaturaContrato = row["dataAssinaturaContrato"].ToString();
                if (row["cpfAdvogado"].ToString() != "")
                {
                    dadosComplementares.cpfAdvogado = row["cpfAdvogado"].ToString();
                    dadosComplementares.nomeAdvogado = row["nomeAdvogado"].ToString();
                    dadosComplementares.registroOAB = row["registroOAB"].ToString();
                    dadosComplementares.ufOAB = row["ufOAB"].ToString();
                }
                #endregion

                #region Contador
                if (1 == 1 && DadosContador.Rows.Count > 0 &&
                    (rowContador["cpfContadorPF"].ToString() != "" || rowContador["cnpjEmpresaContabil"].ToString() != ""))
                {
                    dadosComplementares.codClassificCRCcontadorPF = rowContador["codClassificCRCcontadorPF"].ToString();
                    dadosComplementares.ufContadorPF = rowContador["ufContadorPF"].ToString();
                    dadosComplementares.numSeqContadorPF = rowContador["numSeqContadorPF"].ToString();
                    dadosComplementares.codTipoCRCcontadorPF = rowContador["codTipoCRCcontadorPF"].ToString();
                    dadosComplementares.cpfContadorPF = rowContador["cpfContadorPF"].ToString();
                    dadosComplementares.nomeContadorPF = rowContador["nomeContadorPF"].ToString();
                    //empresa contabil
                    if (rowContador["cnpjEmpresaContabil"].ToString().Length == 14)
                    {
                        dadosComplementares.codClassificEmpresaContabil = rowContador["codClassificEmpresaContabil"].ToString();
                        dadosComplementares.ufCRCempresaContabil = rowContador["ufCRCempresaContabil"].ToString();
                        dadosComplementares.seqCRCempresaContabil = rowContador["seqCRCempresaContabil"].ToString();
                        dadosComplementares.codTipoCRCempresaContabil = rowContador["codTipoCRCempresaContabil"].ToString();
                        dadosComplementares.cnpjEmpresaContabil = rowContador["cnpjEmpresaContabil"].ToString();
                        dadosComplementares.nomeEmpresaContabil = rowContador["nomeEmpresaContabil"].ToString();
                    }
                    //endContadorPf.indOrigemEndereco = rowContador["nomeEmpresaContabil"].ToString();

                    endContadorPf.cep = rowContador["cep"].ToString();
                    endContadorPf.uf = rowContador["uf"].ToString();
                    endContadorPf.codMunicipio = "";
                    if (rowContador["codMunicipio"] != null && rowContador["codMunicipio"].ToString() != "")
                        endContadorPf.codMunicipio = rowContador["codMunicipio"].ToString().PadLeft(4, '0').Substring(0, 4);

                    //endContadorPf.codMunicipio = rowContador["codMunicipio"].ToString();
                    endContadorPf.codTipoLogradouro = TrataTipoLogaouro(rowContador["codTipoLogradouro"].ToString());
                    //endContadorPf.numLogradouro = string.IsNullOrEmpty(rowContador["numLogradouro"].ToString()) ? "S/N" : rowContador["numLogradouro"].ToString();
                    endContadorPf.numLogradouro = rowContador["numLogradouro"].ToString();
                    endContadorPf.logradouro = DadosViabilidade.TiraAcento(rowContador["logradouro"].ToString());
                    //verificar
                    //endContadorPf.complementoLogradouro = TrataComplmentoS53(rowContador["logradouro"].ToString());
                    endContadorPf.bairro = rowContador["bairro"].ToString();
                    endContadorPf.distrito = rowContador["distrito"].ToString();
                    endContadorPf.referencia = rowContador["referencia"].ToString();


                    contatoContadorPf.dddTelefone1 = rowContador["dddTelefone1"].ToString();
                    contatoContadorPf.telefone1 = rowContador["telefone1"].ToString();
                    contatoContadorPf.dddTelefone2 = rowContador["dddTelefone2"].ToString();
                    contatoContadorPf.telefone2 = rowContador["telefone2"].ToString();
                    contatoContadorPf.correioEletronico = rowContador["correioEletronico"].ToString();

                    dadosComplementares.endContadorPf = endContadorPf;
                    dadosComplementares.contatoContadorPf = contatoContadorPf;
                }
                #endregion

                dadosComplementares.enderecoCorrespondencia = TrataEnderecoCorrespondencia(DadosEmpresa);
                request.dadosComplementares = dadosComplementares;

                request.dadosComplementares.contatoEmpresa = contatoEmpresa;

                //QSA
                request.socios = PreencherDadosSocioBU(DadosQSA, naturezaJuridica);

                string XMLResquest53 = GlobalV1.CreateXML(request);

                using (WsServicesReginRFB.ServiceReginRFB envio = new WsServicesReginRFB.ServiceReginRFB())
                {
                    WsServicesReginRFB.RetornoS53 retornows = new WsServicesReginRFB.RetornoS53();
                    envio.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                    envio.Timeout = 10000000;
                    retornows = envio.ServiceWs53(XMLResquest53, nrViabilidade, cpfSolicitante);

                    ret.codretorno = retornows.codretorno;
                    ret.descricao = retornows.descricao;
                    ret.Recibo = retornows.Recibo;
                    ret.Identificacao = retornows.Identificacao;
                    ret.mensagemRetorno = PreencheArrayMensagemS53(retornows.mensagemRetorno);
                    ret.Requerimento = pProtocolo;
                    ret.XmlRFB = retornows.XmlRFB;

                    if (ret.descricao == null || retornows.mensagemRetorno != null)
                    {
                        ret.descricao = GlobalV1.CreateXML(ret.mensagemRetorno);
                    }
                    if (ret.descricao == null)
                        ret.descricao = "";

                    //Atualiza tabela VIA_PRO_XMLRFB
                    if (ret.codretorno == "00")
                    {
                        ret.status = "OK";
                        dHelperQuery.UpdateVIA_PRO_XMLRFB_OK(pProtocolo, ret.Recibo + ret.Identificacao);
                        //atualiza o numero do DBE no requerimento
                        dHelperQuery.UpdateDBERequerimento(pProtocolo, ret.Recibo + ret.Identificacao);
                        try
                        {
                            string email = dHelperQuery.getEmailBalcaoUnico(pProtocolo);
                            dHelperQuery.CriaEmail("", pProtocolo, "", "8", "Status do DBE", "", email);
                        }
                        catch { }

                        //Se passa o S04 para poder mudar a observação que aparece no site da RFB, ja que a RFB nao envia
                        //Registros para o s99 do Balcão Unico
                        //este codigo abaixo e porque se entrega o DBE e passa o s04 logo enseguida, a RFB demora para dar os outros andamentos
                        //Entao parece que o s04 nao foi executado
                        System.Threading.Thread.Sleep(3000);
                        WsServicesReginRFB.DadosWs04 dadoss04 = new WsServicesReginRFB.DadosWs04();
                        WsServicesReginRFB.Retorno R04 = new WsServicesReginRFB.Retorno();
                        dadoss04.identificacaoSolicitacao = retornows.Identificacao;
                        dadoss04.reciboSolicitacao = retornows.Recibo;
                        dadoss04.resultadoValidacao = "01";
                        envio.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                        R04 = envio.ServiceWs04(dadoss04);

                    }
                    else
                    {
                        string status = "2";
                        //if(ret.mensagemRetorno == null)
                        //{
                        if (ret.descricao.Contains("Ou esse protocolo já foi transmitido anteriormente"))
                            status = "88";
                        if (ret.descricao.Contains("PERTENCENTE A FAIXA DE CEP PERMITIDA"))
                            status = "87";
                        if (ret.descricao.Contains("Retorno NOK"))
                            status = "86";
                        if (ret.descricao.Contains("dataFimAnaliseViabilidadeEndereco"))
                            status = "85";
                        if (ret.descricao.Contains("dataValidadeViabilidade"))
                            status = "85";
                        if (ret.descricao.Contains("codStatusViabilidade")) // Acahamos que quando traz esse codigo e porque a RFB indefiriu ou cancelou a viabilidade, que que nem DBE tem
                            status = "98";

                        //}
                        dHelperQuery.UpdateVIA_PRO_XMLRFB_NOK(pProtocolo, ret.descricao, status);
                        //try
                        //{
                        //    string email = dHelperQuery.getEmailBalcaoUnico(pProtocolo);
                        //    dHelperQuery.CriaEmail("", pProtocolo, "", "9", "Status do DBE", "", email);
                        //}
                        //catch { }
                    }
                }
                return ret;

            }
            catch (Exception ex)
            {

                using (T73307_ERRO_PROCESSO_35 e35 = new T73307_ERRO_PROCESSO_35())
                {
                    e35.t73307_arquivo_RFB = "";
                    e35.t73307_erro = ex.StackTrace + " Mens: " + ex.Message;
                    e35.t73307_ide_solicitacao = pProtocolo;
                    e35.t73307_rec_solicitacao = "ws53";
                    e35.Update();

                    dHelperQuery.UpdateVIA_PRO_XMLRFB_NOK(pProtocolo, e35.t73307_erro, "98");
                }
                RetornoS53 cRetorno = new RetornoS53();
                cRetorno.codretorno = "99";
                cRetorno.descricao = ex.Message;
                return cRetorno;

            }

        }

        private WsServices53RFB.socio[] PreencherDadosSocioBU(DataTable dtSocio, string naturezaJuridica)
        {


            WsServices53RFB.endereco endSocio = new WsServices53RFB.endereco();
            WsServices53RFB.endereco endRepLegal = new WsServices53RFB.endereco();

            WsServices53RFB.contato contatoSocio = new WsServices53RFB.contato();
            WsServices53RFB.contato contatoRepLegal = new WsServices53RFB.contato();
            WsServices53RFB.integralizacaoCapitalSocio integralizacaoSocio = new WsServices53RFB.integralizacaoCapitalSocio();
            WsServices53RFB.integralizacaoCapitalSocio[] integralizacaoCapitalSocio = new WsServices53RFB.integralizacaoCapitalSocio[1];

            //Tratamento do socio da natureza 2062/2305, que não pode ter um registro de qualificação 22 e 05 para o mesmo cpf
            //transformando o sicio com qualificaçao 22 para 49
            if (naturezaJuridica == "2062" || naturezaJuridica == "2305")
                TrataSocioAdminstradorS53(ref dtSocio, naturezaJuridica);

            WsServices53RFB.socio[] aSocio = new WsServices53RFB.socio[dtSocio.Rows.Count];

            int a = 0;
            foreach (DataRow r in dtSocio.Rows)
            {
                WsServices53RFB.socio cSocio = new WsServices53RFB.socio();


                cSocio.indicadorRepresentantePJ = r["indicadorRepresentantePJ"].ToString();
                cSocio.codEvento = r["codEvento"].ToString();
                cSocio.indCnpjCpfSocio = r["indCnpjCpfSocio"].ToString();
                cSocio.cnpjCpfSocio = r["cnpjCpfSocio"].ToString();
                cSocio.socio1 = r["socio"].ToString();
                cSocio.codQualificacaoSocio = r["codQualificacaoSocio"].ToString().PadLeft(2, '0');
                //retirado pois na validação fala para não informar
                //cSocio.codPais = r["codPais"].ToString(); 
                if (cSocio.codQualificacaoSocio != "05")
                {
                    cSocio.valorParticipacaoCapitalSocial = r["valorParticipacaoCapitalSocial"].ToString();
                    cSocio.valorIntegralizado = r["valorIntegralizado"].ToString();
                    cSocio.valorIntegralizar = r["valorIntegralizar"].ToString();
                    if (naturezaJuridica == "2062")
                    {
                        cSocio.qtdeQuotas = r["qtdeQuotas"].ToString();
                    }
                    //integralização do socio
                    integralizacaoSocio = new WsServices53RFB.integralizacaoCapitalSocio();
                    integralizacaoSocio.formaIntegralizacao = "01";
                    integralizacaoSocio.valorIntegralizacao = r["valorParticipacaoCapitalSocial"].ToString();
                    integralizacaoSocio.dataIntegralizacao = DateTime.Now.ToString("yyyyMMdd");

                    integralizacaoCapitalSocio = new WsServices53RFB.integralizacaoCapitalSocio[1];
                    integralizacaoCapitalSocio.SetValue(integralizacaoSocio, 0);

                    cSocio.integralizacaoCapitalSocio = integralizacaoCapitalSocio;
                }

                //inibido pois a validação da RFB diz para não informar
                if (naturezaJuridica != "2305" && naturezaJuridica != "2062")
                {
                    cSocio.estadoCivil = r["estadoCivil"].ToString();
                    if (cSocio.estadoCivil == "02")
                        cSocio.comunhaoBens = r["comunhaoBens"].ToString();

                    cSocio.indicadorEmancipacao = r["indicadorEmancipacao"].ToString();
                    if (!string.IsNullOrEmpty(r["tipoEmancipacao"].ToString()))
                        cSocio.tipoEmancipacao = r["tipoEmancipacao"].ToString();
                }

                contatoSocio.dddTelefone1 = r["dddTelefone1"].ToString();
                contatoSocio.telefone1 = r["telefone1"].ToString();
                contatoSocio.dddTelefone2 = r["dddTelefone2"].ToString();
                contatoSocio.telefone2 = r["telefone2"].ToString();
                contatoSocio.correioEletronico = r["correioEletronico"].ToString();

                DataTable dtRepre = dHelperQuery.getdadosRepresentanteBU(r["SqPessoa"].ToString());
                foreach (DataRow re in dtRepre.Rows)
                {
                    cSocio.representanteLegalSocio = r["representanteLegalSocio"].ToString();
                    cSocio.codQualificacaoRepresentanteLegalSocio = r["codQualificacaoRepresentanteLegalSocio"].ToString();
                    cSocio.cpfRepresentanteLegalSocio = r["cpfRepresentanteLegalSocio"].ToString();

                    contatoRepLegal.dddTelefone1 = r["dddTelefone1"].ToString();
                    contatoRepLegal.telefone1 = r["telefone1"].ToString();
                    contatoRepLegal.dddTelefone2 = r["dddTelefone2"].ToString();
                    contatoRepLegal.telefone2 = r["telefone2"].ToString();
                    contatoRepLegal.correioEletronico = r["correioEletronico"].ToString();

                    cSocio.endRepLegal = TrataEndereco(re);
                    cSocio.contatoRepLegal = contatoRepLegal;
                }
                cSocio.endSocio = TrataEndereco(r);
                cSocio.contatoSocio = contatoSocio;


                aSocio.SetValue(cSocio, a);
                a++;
            }
            return aSocio;
        }

        private void TrataSocioAdminstradorS53(ref DataTable dt, string naturezaJuridica)
        {
            List<tSocio> ListSocio = new List<tSocio>();
            //procurar registros com o mesmo cpf e qualificação 22 e 05
            if (naturezaJuridica == "2062")
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (r["codQualificacaoSocio"].ToString().PadLeft(2, '0') != "05")
                    {
                        DataRow[] results = dt.Select("cnpjCpfSocio = '" + r["cnpjCpfSocio"].ToString() + "'" +
                            " AND codQualificacaoSocio = '05'");
                        if (results.Length > 0)
                        {
                            r["codQualificacaoSocio"] = "49";

                            tSocio socio = new tSocio();
                            socio.cpf = results[0]["cnpjCpfSocio"].ToString();
                            socio.qualificacao = results[0]["codQualificacaoSocio"].ToString().PadLeft(2, '0');
                            ListSocio.Add(socio);

                        }
                    }
                }
                foreach (tSocio s in ListSocio)
                {

                    foreach (DataRow r in dt.Rows)
                    {
                        if (r["cnpjCpfSocio"].ToString() == s.cpf
                            && r["codQualificacaoSocio"].ToString().PadLeft(2, '0') == s.qualificacao)
                        {
                            r.Delete();
                        }
                    }
                    dt.AcceptChanges();
                }
            }
            if (naturezaJuridica == "2305")
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (r["codQualificacaoSocio"].ToString().PadLeft(2, '0') != "05")
                    {
                        DataRow[] results = dt.Select("cnpjCpfSocio = '" + r["cnpjCpfSocio"].ToString() + "'" +
                            " AND codQualificacaoSocio = '05'");
                        if (results.Length > 0)
                        {
                            tSocio socio = new tSocio();
                            socio.cpf = results[0]["cnpjCpfSocio"].ToString();
                            socio.qualificacao = results[0]["codQualificacaoSocio"].ToString().PadLeft(2, '0');
                            ListSocio.Add(socio);

                        }
                    }
                }
                foreach (tSocio s in ListSocio)
                {

                    foreach (DataRow r in dt.Rows)
                    {
                        if (r["cnpjCpfSocio"].ToString() == s.cpf
                            && r["codQualificacaoSocio"].ToString().PadLeft(2, '0') == s.qualificacao)
                        {
                            r.Delete();
                        }
                    }
                    dt.AcceptChanges();
                }
            }
        }
        private class tSocio
        {
            public string cpf { get; set; }
            public string qualificacao { get; set; }
            public string excluir { get; set; }
        }
        private WsServices53RFB.complementoLogradouro[] TrataComplmentoS53(string pComplemento)
        {
            WsServices53RFB.complementoLogradouro[] complemento = new WsServices53RFB.complementoLogradouro[6];
            if (pComplemento.Length > 0)
            {
                WsServices53RFB.complementoLogradouro comp = new WsServices53RFB.complementoLogradouro();

                string complementoLogradouro = "";

                complementoLogradouro = DadosViabilidade.TiraAcento(pComplemento);

                string[] palavras = complementoLogradouro.Split(' ');
                string linha = "";
                int aaa = 0;
                foreach (string pLinhaComplemento in palavras)
                {
                    string pLinhaTratada = DadosViabilidade.TiraAcento(pLinhaComplemento, "Complemento");
                    if (pLinhaTratada.Length > 19)
                    {
                        throw new Exception("Erro palavra passa de vinte carateres");
                    }
                    if (linha.Length + pLinhaTratada.Length > 19)
                    {
                        comp = new WsServices53RFB.complementoLogradouro();
                        comp.tipoComplementoLogradouro = "";
                        comp.descricaoComplementoLogradouro = linha;
                        complemento.SetValue(comp, aaa);
                        aaa += 1;
                        linha = "";
                    }
                    if (pLinhaTratada != "")
                        linha += " " + pLinhaTratada;
                }

                if (linha != "")
                {
                    comp = new WsServices53RFB.complementoLogradouro();
                    comp.tipoComplementoLogradouro = "";
                    comp.descricaoComplementoLogradouro = linha.Trim();
                    complemento.SetValue(comp, aaa);
                    aaa += 1;
                    linha = "";
                }
            }
            return complemento;
        }

        private WsServices01RFB.processarViabilidadeRequest RecuperaViabilidadeS01(string nrViabilidade)
        {
            WsServices01RFB.processarViabilidadeRequest requestViab = new WsServices01RFB.processarViabilidadeRequest();

            string XMLResquestS01 = dHelperQuery.getXMLS01(nrViabilidade);
            if (string.IsNullOrEmpty(XMLResquestS01))
            {
                return requestViab;
            }

            requestViab = (WsServices01RFB.processarViabilidadeRequest)GlobalV1.CreateObject(XMLResquestS01, requestViab);

            return requestViab;
        }
        private WsServices53RFB.viabilidade TrataViabilidadeS53(WsServices01RFB.processarViabilidadeRequest requestS01)
        {
            WsServices53RFB.viabilidade dadosViabilidade = new WsServices53RFB.viabilidade();
            WsServices53RFB.endereco endereco = new WsServices53RFB.endereco();
            WsServices53RFB.ativEcon ativEcon = new WsServices53RFB.ativEcon();
            WsServices53RFB.complementoLogradouro[] WScomplemento = new WsServices53RFB.complementoLogradouro[6];

            dadosViabilidade.codEventosViabilidade = requestS01.codEventosViabilidade;
            dadosViabilidade.codStatusViabilidade = "01";
            dadosViabilidade.dataValidadeViabilidade = requestS01.dataValidadeViabilidade;
            dadosViabilidade.resultadoViabilidadeNome = requestS01.resultadoViabilidadeNome;
            dadosViabilidade.resultadoViabilidadeEndereco = requestS01.resultadoViabilidadeEndereco;
            dadosViabilidade.dataInicioAnaliseViabilidade = requestS01.dataInicioAnaliseViabilidade;
            dadosViabilidade.dataFimAnaliseViabilidadeNome = requestS01.dataFimAnaliseViabilidadeNome;
            dadosViabilidade.dataFimAnaliseViabilidadeEndereco = requestS01.dataFimAnaliseViabilidadeEndereco;
            dadosViabilidade.nomeEmpresarial = requestS01.nomeEmpresarial;
            dadosViabilidade.codNaturezaJuridica = requestS01.naturezaJuridica;
            dadosViabilidade.codTipoOrgaoRegistro = requestS01.tipoOrgaoRegistro;
            dadosViabilidade.cnpj = requestS01.cnpj;
            if (requestS01.indicadorCnpjNomeEmpresarial != null && requestS01.indicadorCnpjNomeEmpresarial != "")
                dadosViabilidade.indicadorCnpjNomeEmpresarial = requestS01.indicadorCnpjNomeEmpresarial;

            endereco.cep = requestS01.cep;
            endereco.uf = requestS01.uf;
            endereco.codMunicipio = requestS01.codMunicipio;
            endereco.codTipoLogradouro = requestS01.codTipoLogradouro;
            endereco.logradouro = requestS01.logradouro.Trim();
            endereco.numLogradouro = requestS01.numLogradouro.Trim();
            //verificar
            if (requestS01.complementoLogradouro != null)
            {
                string pXml = GlobalV1.CreateXML(requestS01.complementoLogradouro);
                WScomplemento = (WsServices53RFB.complementoLogradouro[])GlobalV1.CreateObject(pXml, WScomplemento);

                endereco.complementoLogradouro = WScomplemento;
            }
            endereco.bairro = requestS01.bairro.Trim();
            endereco.distrito = requestS01.distrito;
            if (!string.IsNullOrEmpty(requestS01.referencia))
                endereco.referencia = requestS01.referencia.Trim();
            endereco.cidadeExterior = "";
            dadosViabilidade.endereco = endereco;

            ativEcon.codCnaeFiscal = requestS01.cnaePrincipal.Codigo;
            int ind = 0;
            if (requestS01.cnaeSecundaria != null)
            {
                string[] codCnaeSecundaria = new string[requestS01.cnaeSecundaria.Length];

                foreach (WsServices01RFB.Cnae cnae in requestS01.cnaeSecundaria)
                {
                    codCnaeSecundaria[ind] = cnae.Codigo;
                    ind++;
                }
                ativEcon.codCnaeSecundaria = codCnaeSecundaria;
            }
            string[] arrTipoUnidade = new string[requestS01.tipoUnidade.Length];
            ind = 0;
            foreach (string forma in requestS01.tipoUnidade)
            {
                arrTipoUnidade[ind] = forma;
                ind++;
            }
            ativEcon.codTipoUnidade = arrTipoUnidade;

            if (requestS01.formaAtuacao != null && requestS01.formaAtuacao.Length > 0)
            {
                string[] arrFormaAtuacao = new string[requestS01.formaAtuacao.Length];
                ind = 0;
                foreach (string forma in requestS01.formaAtuacao)
                {
                    arrFormaAtuacao[ind] = forma;
                    ind++;
                }
                ativEcon.codFormaDeAtuacao = arrFormaAtuacao;
            }
            ativEcon.objetoSocial = requestS01.objetoSocial;

            dadosViabilidade.atividadeEconomica = ativEcon;

            string[] arrCpfSocio = new string[requestS01.cpfSocioPf.Length];
            ind = 0;
            foreach (string forma in requestS01.cpfSocioPf)
            {
                arrCpfSocio[ind] = forma;
                ind++;
            }

            string[] arrNomeSocio = new string[requestS01.nomeSocioPf.Length];
            ind = 0;
            foreach (string forma in requestS01.nomeSocioPf)
            {
                arrNomeSocio[ind] = forma;
                ind++;
            }

            dadosViabilidade.cpfSocioPf = arrCpfSocio;
            dadosViabilidade.nomeSocioPf = arrNomeSocio;
            if (!string.IsNullOrEmpty(requestS01.indicadorContabilista))
                dadosViabilidade.indicadorContabilista = requestS01.indicadorContabilista;

            return dadosViabilidade;
        }

        private WsServices53RFB.endereco TrataEndereco(DataRow r)
        {
            WsServices53RFB.endereco end = new WsServices53RFB.endereco();
            //DataRow r = DadosEmpresa.Rows[0];
            end.cep = r["cep"].ToString();
            end.uf = r["uf"].ToString();
            end.codMunicipio = r["codMunicipio"].ToString().PadLeft(4, '0').Substring(0, 4); ;
            end.codTipoLogradouro = TrataTipoLogaouro(r["codTipoLogradouro"].ToString());
            end.logradouro = DadosViabilidade.TiraAcento(r["logradouro"].ToString());
            end.numLogradouro = string.IsNullOrEmpty(r["numLogradouro"].ToString()) ? "S/N" : r["numLogradouro"].ToString();
            //end.complementoLogradouro = TrataComplmentoS53(r["complementoLogradouro"].ToString());
            end.bairro = r["bairro"].ToString();
            end.distrito = r["distrito"].ToString();
            end.referencia = r["referencia"].ToString();
            end.cidadeExterior = r["cidadeExterior"].ToString();
            end.codPais = r["codPaisEnd"].ToString();

            return end;

        }
        private WsServices53RFB.endereco TrataEnderecoCorrespondencia(DataTable DadosEmpresa)
        {
            WsServices53RFB.endereco end = new WsServices53RFB.endereco();
            DataRow r = DadosEmpresa.Rows[0];
            end.cep = r["cep"].ToString();
            end.uf = r["uf"].ToString();
            end.codMunicipio = r["codMunicipio"].ToString().PadLeft(4, '0').Substring(0, 4); ;
            end.codTipoLogradouro = TrataTipoLogaouro(r["codTipoLogradouro"].ToString());
            end.logradouro = DadosViabilidade.TiraAcento(r["logradouro"].ToString());
            end.numLogradouro = string.IsNullOrEmpty(r["numLogradouro"].ToString()) ? "S/N" : r["numLogradouro"].ToString();
            //end.complementoLogradouro = TrataComplmentoS53(r["complementoLogradouro"].ToString());
            end.bairro = r["bairro"].ToString();
            end.distrito = r["distrito"].ToString();
            end.referencia = r["referencia"].ToString();
            end.cidadeExterior = r["cidadeExterior"].ToString();
            end.codPais = r["codPaisEnd"].ToString();

            return end;

        }
        private string TrataTipoLogaouro(string tipoLogadouro)
        {
            TipoLogradouroRFB ListaTipoLogradouroRFB = new TipoLogradouroRFB();
            foreach (TipoLogradouroRFB.Tipo TipoLogradouro in ListaTipoLogradouroRFB.Lista)
            {
                if (TipoLogradouro.Cod_regin.Trim().ToUpper() == tipoLogadouro.Trim().ToUpper())
                {
                    return TipoLogradouro.Cod_rfb.ToString().Trim().ToUpper();

                }
            }
            return tipoLogadouro;
        }

        #region buscar CpfResponsavel
        private string fnCpfResponsavel()
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(" Select pkg_proc_automatico.getcpfprocautomatico from dual");

            using (ConnectionProviderORACLE cp = new ConnectionProviderORACLE())
            {
                cp.OpenConnection();
                using (OracleCommand cmdToExecute = new OracleCommand())
                {
                    cmdToExecute.CommandText = sql.ToString();
                    cmdToExecute.CommandType = CommandType.Text;
                    cmdToExecute.Connection = cp.DBConnection;

                    //cmdToExecute.Connection.Open();
                    object pCpf = cmdToExecute.ExecuteScalar();

                    return pCpf.ToString();
                }
            }


        }
        #endregion

        [WebMethod]
        public Retorno CancelaViabilidadeBalcaoUnico(string pProtocoloViabilidade)
        {
            Retorno pResult = new Retorno();
            try
            {
                DataTable toReturn = new DataTable("via_pro_xmlrfb");
                RetornoS01 pResulPrivado = new RetornoS01();

                WsServices01RFB.processarViabilidadeRequest rDadosViabilidade = new WsServices01RFB.processarViabilidadeRequest();


                rDadosViabilidade = RecuperaViabilidadeS01(pProtocoloViabilidade);

                using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (OracleTransaction _conn = conn.BeginTransaction())
                    {
                        dHelperQuery c = new dHelperQuery();
                        c.CancelarProtocoloViabilidade(_conn, pProtocoloViabilidade);
                        _conn.Commit();
                    }
                }
                if (string.IsNullOrEmpty(rDadosViabilidade.protocoloViabilidade))
                {
                    pResult.descricao = "";
                    pResult.status = "OK";
                    return pResult;
                }

                DadosViabilidade dados = new DadosViabilidade();

                dados.protocoloViabilidade = rDadosViabilidade.protocoloViabilidade;
                dados.dataFimAnaliseViabilidadeEndereco = rDadosViabilidade.dataFimAnaliseViabilidadeEndereco;
                dados.dataFimAnaliseViabilidadeNome = rDadosViabilidade.dataFimAnaliseViabilidadeNome;
                dados.dataInicioAnaliseViabilidade = rDadosViabilidade.dataInicioAnaliseViabilidade;
                dados.dataValidadeViabilidade = rDadosViabilidade.dataValidadeViabilidade;
                dados.resultadoViabilidadeEndereco = rDadosViabilidade.resultadoViabilidadeEndereco;
                dados.resultadoViabilidadeNome = rDadosViabilidade.resultadoViabilidadeNome;
                dados.codStatusViabilidade = "05"; //Cancelamento

                pResulPrivado = ServiceWs02(dados, "01");

                pResult.descricao = pResulPrivado.descricao;
                pResult.descricao = pResulPrivado.descricao;
                pResult.status = pResulPrivado.status;

                //Caso nao de OK no serviço do s02 para cancelar a viabilidade (porque o DBE ja foi recepcionado s05) vamos a indeferir o DBE caso tenha e o DBE cancela a viabilidade
                if (pResult.status == "NOK")
                {
                    #region Select para pegar o numero de DBE
                    using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                    {
                        OracleCommand cmdToExecute = new OracleCommand();
                        StringBuilder Sql = new StringBuilder();


                        Sql.AppendLine(" select substr(a.vpx_nr_dbe, 1, 10) reciboSolicitacao, substr(a.vpx_nr_dbe, 11) identificacaoSolicitacao ");
                        Sql.AppendLine(" from via_pro_xmlrfb a ");
                        Sql.AppendLine(" where a.vpx_cod_protocolo = '" + pProtocoloViabilidade + "'");
                        cmdToExecute.CommandText = Sql.ToString();

                        cmdToExecute.CommandType = CommandType.Text;
                        // Use base class' connection object
                        cmdToExecute.Connection = _conn;

                        //cmdToExecute.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, true, 20, 0, "", DataRowVersion.Proposed, pProtocolo));
                        //cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 20, ParameterDirection.Output, true, 20, 0, "", DataRowVersion.Proposed, toReturn));

                        OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);

                        try
                        {
                            adapter.Fill(toReturn);
                        }
                        catch (Exception ex)
                        {
                            // some error occured. Bubble it to caller and encapsulate Exception object
                            throw ex;
                        }
                        finally
                        {
                            _conn.Close();
                            cmdToExecute.Dispose();
                            adapter.Dispose();
                        }
                    }
                    #endregion

                    string identificacaoSolicitacao = "";
                    string reciboSolicitacao = "";
                    if (toReturn.Rows.Count > 0)
                    {
                        reciboSolicitacao = toReturn.Rows[0]["reciboSolicitacao"].ToString();
                        identificacaoSolicitacao = toReturn.Rows[0]["identificacaoSolicitacao"].ToString();
                    }
                    //caso tenha
                    if (reciboSolicitacao != "" && identificacaoSolicitacao != "")
                    {
                        string pCpfResponsavel = fnCpfResponsavel();
                        //Pega o CPF da tabela de parametro, porque nao pode enviar o da viabilidade, ja que da problema, que o CPF de cancelamento nao pode pertencer ao QSA

                        Retorno Result06 = new Retorno();
                        DadosWs06 s06 = new DadosWs06();
                        s06.identificacaoSolicitacao = identificacaoSolicitacao;
                        s06.reciboSolicitacao = reciboSolicitacao;
                        s06.dataRegistro = DateTime.Today.ToString("yyyyMMdd");
                        s06.cpfResponsavelDeferimento = pCpfResponsavel;
                        //s06.numeroServentia = txtNumeroServentia.Text;
                        //s06.numeroRegistroOab = txtNumeroOAB.Text;
                        //s06.Uf = txtUf.Text;
                        s06.resultadoRegistroIntegradorEstadual = "02";

                        incompRegistroIntegradorEstadual[] inconpa = new incompRegistroIntegradorEstadual[20];
                        incompRegistroIntegradorEstadual inconpa2 = new incompRegistroIntegradorEstadual();
                        inconpa2.codigo = "Z01";
                        inconpa2.mensagem = "Solicitacao indeferida pelo Orgão de Registro a pedido do contribuinte";
                        inconpa.SetValue(inconpa2, 0);
                        s06.incompRegistroIntegradorEstadual = inconpa;

                        Result06 = ServiceWs06(s06);

                        pResult.codretorno = Result06.codretorno;
                        pResult.descricao = "S06 - " + Result06.descricao;
                        //string dd = GlobalV1.CreateXML(s06);
                        //pResult.descricao = dd;
                        pResult.status = Result06.status;
                    }


                }

                return pResult;
            }
            catch (Exception ex)
            {
                pResult.descricao = ex.Message;
                pResult.status = "NOK";
                return pResult;
            }
        }
        #endregion

        #region Gravar PSC_RECEITA_ARQUIVO_V2
        [WebMethod]
        public void CompletaWs11()
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");

            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
            {
                using (OracleCommand cmdToExecute = new OracleCommand())
                {
                    //DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
                    cmdToExecute.CommandText = "pkg_jucesc.processodeactualizacaows11";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;

                    cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

                    _conn.Open();

                    cmdToExecute.Connection = _conn;

                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute))
                    {
                        adapter.Fill(toReturn);
                    }
                }
            }
            for (int a = 0; a < toReturn.Rows.Count; a++)
            {
                string CNPJ = toReturn.Rows[a]["pcnpj"].ToString().Trim();
                string pProtocolo = toReturn.Rows[a]["pprotocolo"].ToString().Trim();
                GravaArquivoWs11(pProtocolo, CNPJ);
            }
        }



        private void GravaArquivoWs11(string pProtocolo, string pCNPJ)
        {
            Retorno result = new Retorno();
            Retorno result11 = new Retorno();
            try
            {
                #region insert Command

                StringBuilder sql = new StringBuilder();
                StringBuilder sqlD = new StringBuilder();
                StringBuilder sqlU = new StringBuilder();

                sql.AppendLine(" Insert Into PSC_RECEITA_ARQUIVO_V2 ");
                sql.AppendLine(" (PRA_PRO_PROTOCOLO, PRA_ARQUIVO, PRA_CNPJ) ");
                sql.AppendLine(" Values ");
                sql.AppendLine(" (:v_PRA_PROTOCOLO, :v_PRA_ARQUIVO, :v_PRA_CNPJ) ");


                sqlU.AppendLine(" update MAC_LOG_CARGA_JUNTA_HOMOLOG set MLC_DATA_CARREGA_WS11 = sysdate where MLC_PROTOCOLO = :v_PRA_PROTOCOLO ");

                sqlD.AppendLine(" delete PSC_RECEITA_ARQUIVO_V2 where PRA_PRO_PROTOCOLO = :v_PRA_PROTOCOLO and PRA_CNPJ = :v_PRA_CNPJ");

                #endregion


                result11 = ServiceWs11(pCNPJ);

                if (result11.status == "OK")
                {
                    using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                    {
                        conn.Open();
                        using (OracleTransaction _conn = conn.BeginTransaction())
                        {
                            using (OracleCommand cmdToExecute = new OracleCommand())
                            {
                                cmdToExecute.Connection = _conn.Connection;
                                cmdToExecute.Transaction = _conn;

                                /*
                                    Atualiza MAC_LOG_CARGA_JUNTA_HOMOLOG
                                 */
                                cmdToExecute.Parameters.Add(new OracleParameter("v_PRA_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                                cmdToExecute.CommandText = sqlU.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();

                                /*
                                  A paga arquivo da tabela caso reprocesse
                                */
                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.Parameters.Add(new OracleParameter("V_PRA_CNPJ", OracleType.VarChar, 30, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, pCNPJ));
                                cmdToExecute.ExecuteNonQuery();


                                /*
                                  Inclui o arquivo
                                */
                                cmdToExecute.CommandText = sql.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.Parameters.Add(new OracleParameter("v_PRA_ARQUIVO", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, result11.XmlDBE));
                                cmdToExecute.ExecuteNonQuery();

                            }
                            _conn.Commit();
                        }
                    }
                }
                else
                {

                    atualizaProtocoloNOK(pProtocolo, result11.descricao);
                }
            }
            catch (Exception ex)
            {
                atualizaProtocoloNOK(pProtocolo, ex.Message + " StackTrace " + ex.StackTrace);

            }
        }
        private void atualizaProtocoloNOK(string pProtocolo, string mensagem)
        {
            atualizaProtocoloNOK(pProtocolo, mensagem, "9");
        }
        private void atualizaProtocoloNOK(string pProtocolo, string mensagem, string status)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {

                StringBuilder sqlU = new StringBuilder();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction _conn = conn.BeginTransaction())
                    {
                        using (SqlCommand cmdToExecute = new SqlCommand())
                        {
                            cmdToExecute.Connection = _conn.Connection;
                            cmdToExecute.Transaction = _conn;
                            mensagem = " Erro buscando ws11 " + mensagem;

                            sqlU.AppendLine(" update psc_protocolo set pro_status = @v_status, pro_error_processo = @v_pro_error_processo where pro_protocolo = @v_PRA_PROTOCOLO ");

                            cmdToExecute.Parameters.Add(new SqlParameter("v_PRA_PROTOCOLO", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                            cmdToExecute.Parameters.Add(new SqlParameter("v_status", SqlDbType.Int, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, status));
                            cmdToExecute.Parameters.Add(new SqlParameter("v_pro_error_processo", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, mensagem));

                            cmdToExecute.CommandText = sqlU.ToString();
                            cmdToExecute.CommandType = CommandType.Text;
                            cmdToExecute.ExecuteNonQuery();
                            _conn.Commit();
                        }
                    }
                }
            }
            else
            {
                StringBuilder sqlU = new StringBuilder();
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (OracleTransaction _conn = conn.BeginTransaction())
                    {
                        using (OracleCommand cmdToExecute = new OracleCommand())
                        {
                            cmdToExecute.Connection = _conn.Connection;
                            cmdToExecute.Transaction = _conn;
                            mensagem = " Erro buscando ws11 " + mensagem;

                            sqlU.AppendLine(" update psc_protocolo set pro_status = :v_status, pro_error_processo = :v_pro_error_processo where pro_protocolo = :v_PRA_PROTOCOLO ");

                            cmdToExecute.Parameters.Add(new OracleParameter("v_PRA_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_status", OracleType.Number, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, status));
                            cmdToExecute.Parameters.Add(new OracleParameter("v_pro_error_processo", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, mensagem));

                            cmdToExecute.CommandText = sqlU.ToString();
                            cmdToExecute.CommandType = CommandType.Text;
                            cmdToExecute.ExecuteNonQuery();
                            _conn.Commit();
                        }
                    }
                }
            }

        }
        #endregion

        #region GravaDadosWs11SqlServer
        [WebMethod]
        public void CompletaDadosRegin()
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            //DataSet dsTabelasTeste = new DataSet();
            //DataTable DtTipoLograTeste = GlobalV1.BuscarTipoLogradouro();
            //dsTabelasTeste.Tables.Add(DtTipoLograTeste.Copy());
            //string CNPJTeste = "45242914037017";
            //string pProtocoloTeste = "RJP1800019320";
            ////string pDbeTeste = "RJ91772647044085120001424";
            //string pDbeTeste = "";
            //CompletaGravaDadosWs11(pProtocoloTeste, CNPJTeste, pDbeTeste, dsTabelasTeste);
            //return;

            #region Processo de completa dados SqlServer

            DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
            if (1 == 1)
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
                {
                    using (SqlCommand cmdToExecute = new SqlCommand())
                    {
                        //DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
                        cmdToExecute.CommandText = "processodeactualizacaows11";
                        cmdToExecute.CommandType = CommandType.StoredProcedure;

                        //cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

                        _conn.Open();

                        cmdToExecute.Connection = _conn;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute))
                        {
                            adapter.Fill(toReturn);
                        }
                    }
                }

                DataSet dsTabelas = new DataSet();
                if (toReturn.Rows.Count > 0)
                {
                    DataTable DtTipoLogra = GlobalV1.BuscarTipoLogradouro();
                    dsTabelas.Tables.Add(DtTipoLogra.Copy());
                }

                for (int a = 0; a < toReturn.Rows.Count; a++)
                {
                    string CNPJ = toReturn.Rows[a]["pcnpj"].ToString().Trim();
                    string pProtocolo = toReturn.Rows[a]["pprotocolo"].ToString().Trim();
                    string pDbe = toReturn.Rows[a]["pDbe"].ToString().Trim();
                    string pOrigemDado = toReturn.Rows[a]["pOrigemDado"].ToString().Trim();
                    CompletaGravaDadosWs11(pProtocolo, CNPJ, pDbe, pOrigemDado, dsTabelas);
                }
            }
            #endregion

            #region Processo de Para validar Xml para ser enviado
            toReturn = new DataTable("WBS_CONTROL_ENVIO");

            using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
            {
                using (SqlCommand cmdToExecute = new SqlCommand())
                {
                    //DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
                    cmdToExecute.CommandText = "processodeValidaXMLRegin";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;

                    //cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

                    _conn.Open();

                    cmdToExecute.Connection = _conn;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute))
                    {
                        adapter.Fill(toReturn);
                    }
                }
            }

            for (int a = 0; a < toReturn.Rows.Count; a++)
            {
                string CNPJ = toReturn.Rows[a]["pcnpj"].ToString().Trim();
                string pProtocolo = toReturn.Rows[a]["pprotocolo"].ToString().Trim();
                string pTipoOperacao = toReturn.Rows[a]["pTipoOperacao"].ToString().Trim();
                ProcessodePegarXmlParaValidar(pProtocolo, pTipoOperacao);
            }
            #endregion


        }

        private string getMotivoBaixaRFB(string pDBE)
        {

            pDBE = pDBE.Trim();

            if (pDBE == "")
            {
                return "";
            }

            if (pDBE.Length != 24)
            {
                throw new Exception("DBE Invalido " + pDBE);
            }

            WsServicesReginRFB.Retorno result35 = new WsServicesReginRFB.Retorno();
            WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();

            //WsServicesReginRFB.Retorno resulRegin = new WsServicesReginRFB.Retorno();

            regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

            string Recibo = "";
            string Identificacao = "";

            if (pDBE != "")
            {
                Recibo = pDBE.Substring(0, 10);
                Identificacao = pDBE.Substring(10, 14);
            }


            result35 = regin.ServiceWs35Soarquivo(Identificacao, Recibo);

            if (result35.status == "OK")
            {
                string Mot = result35.oWs35Response.dadosRedesim.fcpj.codMotivoSituacaoCadastral == null ? "" : result35.oWs35Response.dadosRedesim.fcpj.codMotivoSituacaoCadastral;
                return Mot;
            }
            else
            {
                throw new Exception("getMotivoBaixaRFB " + result35.descricao);
            }
        }


        private void CompletaDadosDbeOracle(OracleTransaction bd, string pCodServico, string NroAtoOficio, string CodConvenioAto, string pDBE, string pProtocolo, DataTable DtTipoLogra, string pCnpjEmpresa, string pNumeroOrgaoRegistroWs11, string TIPOPERACAOPROT, string IdMySql, string IndBalcaoUnico)
        {
            //pCodServico = "S35";
            string XmlDBE = "";
            string pXmlPeriodoSimples = "";
            string pXmlContatoCNPJ = "";
            string pXMlCNPJ = "";
            string pXMlEnderecoFcpj = "";
            pDBE = pDBE.Trim();
            int pCount = 0;
            if (pDBE == "" && NroAtoOficio == "" && CodConvenioAto == "")
            {
                return;
            }
            if (IndBalcaoUnico == "S")
            {
                return;
            }

            if (pDBE.ToString() != "" && pDBE.Length != 24)
            {
                throw new Exception("DBE Invalido " + pDBE);
            }

            #region Pega Informação do DBE

            WsServicesReginRFB.Retorno result35 = new WsServicesReginRFB.Retorno();
            WsServicesReginRFB.Retorno result08 = new WsServicesReginRFB.Retorno();
            WsServicesReginRFB.RetornoV2 result07 = new WsServicesReginRFB.RetornoV2();
            WsServicesReginRFB.RetornoV2 resulOtros = new WsServicesReginRFB.RetornoV2();
            WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();
            regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();



            psc.Ruc.Tablelas.Helper.dHelper dhe = new psc.Ruc.Tablelas.Helper.dHelper();
            string Recibo = "";
            string Identificacao = "";

            if (pDBE != "")
            {
                Recibo = pDBE.Substring(0, 10);
                Identificacao = pDBE.Substring(10, 14);
            }

            #region  Serviços difrentes de S35
            if (pCodServico != "S15" && pCodServico != "S17" && pCodServico != "S08" && pCodServico != "S07")
            {
                if (pDBE != "")
                {
                    result35 = regin.ServiceWs35Soarquivo(Identificacao, Recibo);
                    if (result35.status != "OK")
                    {
                        throw new Exception("CompletaDadosDbeOracle S35 " + result35.descricao);
                    }
                    XmlDBE = result35.XmlDBE;


                    if (result35.oWs35Response.dadosRedesim.fcpj.contato != null)
                    {
                        pXmlContatoCNPJ = GlobalV1.CreateXML(result35.oWs35Response.dadosRedesim.fcpj.contato);
                    }


                    if (result35.oWs35Response.dadosRedesim != null && result35.oWs35Response.dadosRedesim.fcpj != null
                        && result35.oWs35Response.dadosRedesim.fcpj.enderecoCorrespondencia != null)
                    {
                        WsServicesReginRFB.endereco end = new WsServicesReginRFB.endereco();

                        end = result35.oWs35Response.dadosRedesim.fcpj.enderecoCorrespondencia;

                        //psc.Ruc.Tablelas.Helper.Endereco end = new psc.Ruc.Tablelas.Helper.Endereco();
                        psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();

                        cc.Bairro = end.bairro == null ? "" : end.bairro;
                        cc.Cep = end.cep == null ? "" : end.cep;
                        cc.Codigo_municipio = end.codMunicipio == null ? "" : end.codMunicipio;
                        cc.Complemento = end.complementoLogradouro == null ? "" : end.complementoLogradouro;
                        cc.Logradouro = end.logradouro == null ? "" : end.logradouro;
                        cc.Numero = end.numLogradouro == null ? "" : end.numLogradouro;
                        cc.Pais = end.codPais == null ? "" : end.codPais;
                        cc.TipLogradoro = end.codTipoLogradouro == null ? "" : end.codTipoLogradouro;
                        cc.Uf = end.uf == null ? "" : end.uf;
                        dhe.gravarDadosEnderecoCorrespondencia(bd, cc, pProtocolo, pCnpjEmpresa, DtTipoLogra, pCodServico);
                    }



                }
            }
            #endregion

            #region  Serviço S07
            if (pCodServico == "S07")
            {
                result07 = regin.ServiceWs07(Identificacao, Recibo);
                if (result07.status != "OK")
                {
                    throw new Exception("CompletaDadosDbeOracle S17 " + result08.descricao);
                }
                XmlDBE = result07.XmlDBE;

                //Ver se tem dados espeficicos e carregar na tabela
                WsServices07RFB.serviceResponse ws07v2 = new WsServices07RFB.serviceResponse();
                ws07v2 = (WsServices07RFB.serviceResponse)GlobalV1.CreateObject(result08.XmlDBE, ws07v2);

                //ws17v2.dadosRedesim.dadosCNPJ

                if (ws07v2.dadosRedesim.dadosCNPJ[0].enderecoCorrespondencia != null)
                {
                    WsServices07RFB.endereco end = new WsServices07RFB.endereco();

                    end = ws07v2.dadosRedesim.dadosCNPJ[0].enderecoCorrespondencia;

                    //psc.Ruc.Tablelas.Helper.Endereco end = new psc.Ruc.Tablelas.Helper.Endereco();
                    psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();

                    cc.Bairro = end.bairro == null ? "" : end.bairro;
                    cc.Cep = end.cep == null ? "" : end.cep;
                    cc.Codigo_municipio = end.codMunicipio == null ? "" : end.codMunicipio;
                    cc.Complemento = end.complementoLogradouro == null ? "" : end.complementoLogradouro;
                    cc.Logradouro = end.logradouro == null ? "" : end.logradouro;
                    cc.Numero = end.numLogradouro == null ? "" : end.numLogradouro;
                    cc.Pais = end.codPais == null ? "" : end.codPais;
                    cc.TipLogradoro = end.codTipoLogradouro == null ? "" : end.codTipoLogradouro;
                    cc.Uf = end.uf == null ? "" : end.uf;
                    dhe.gravarDadosEnderecoCorrespondencia(bd, cc, pProtocolo, pCnpjEmpresa, DtTipoLogra, pCodServico);
                }

                if (ws07v2.dadosRedesim.simplesNacional != null)
                {
                    pXmlPeriodoSimples = GlobalV1.CreateXML(ws07v2.dadosRedesim.simplesNacional);
                }

                if (ws07v2.dadosRedesim.dadosCNPJ[0].contato != null)
                {
                    pXmlContatoCNPJ = GlobalV1.CreateXML(ws07v2.dadosRedesim.dadosCNPJ[0].contato);
                }


                if (pXmlContatoCNPJ == "" && ws07v2.dadosRedesim.fcpj != null && ws07v2.dadosRedesim.fcpj.contato != null)
                {
                    pXmlContatoCNPJ = GlobalV1.CreateXML(ws07v2.dadosRedesim.fcpj.contato);
                }


                if (ws07v2.dadosRedesim.dadosCNPJ[0] != null)
                {
                    pXMlCNPJ = GlobalV1.CreateXML(ws07v2.dadosRedesim.dadosCNPJ[0]);
                }

            }
            #endregion

            #region  Serviço S08
            if (pCodServico == "S08")
            {
                result08 = regin.ServiceWs08(Identificacao, Recibo);
                if (result08.status != "OK")
                {
                    throw new Exception("CompletaDadosDbeOracle S17 " + result08.descricao);
                }
                XmlDBE = result08.XmlDBE;

                //Ver se tem dados espeficicos e carregar na tabela
                WsServices08RFB.serviceResponse ws08v2 = new WsServices08RFB.serviceResponse();
                ws08v2 = (WsServices08RFB.serviceResponse)GlobalV1.CreateObject(result08.XmlDBE, ws08v2);

                //ws17v2.dadosRedesim.dadosCNPJ

                if (ws08v2.dadosRedesim.dadosCNPJ[0].enderecoCorrespondencia != null)
                {
                    WsServices08RFB.endereco end = new WsServices08RFB.endereco();

                    end = ws08v2.dadosRedesim.dadosCNPJ[0].enderecoCorrespondencia;

                    //psc.Ruc.Tablelas.Helper.Endereco end = new psc.Ruc.Tablelas.Helper.Endereco();
                    psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();

                    cc.Bairro = end.bairro == null ? "" : end.bairro;
                    cc.Cep = end.cep == null ? "" : end.cep;
                    cc.Codigo_municipio = end.codMunicipio == null ? "" : end.codMunicipio;
                    cc.Complemento = end.complementoLogradouro == null ? "" : end.complementoLogradouro;
                    cc.Logradouro = end.logradouro == null ? "" : end.logradouro;
                    cc.Numero = end.numLogradouro == null ? "" : end.numLogradouro;
                    cc.Pais = end.codPais == null ? "" : end.codPais;
                    cc.TipLogradoro = end.codTipoLogradouro == null ? "" : end.codTipoLogradouro;
                    cc.Uf = end.uf == null ? "" : end.uf;
                    dhe.gravarDadosEnderecoCorrespondencia(bd, cc, pProtocolo, pCnpjEmpresa, DtTipoLogra, pCodServico);
                }

                if (ws08v2.dadosRedesim.simplesNacional != null)
                {
                    pXmlPeriodoSimples = GlobalV1.CreateXML(ws08v2.dadosRedesim.simplesNacional);
                }

                if (ws08v2.dadosRedesim.dadosCNPJ[0].contato != null)
                {
                    pXmlContatoCNPJ = GlobalV1.CreateXML(ws08v2.dadosRedesim.dadosCNPJ[0].contato);
                }


                if (pXmlContatoCNPJ == "" && ws08v2.dadosRedesim.fcpj != null && ws08v2.dadosRedesim.fcpj.contato != null)
                {
                    pXmlContatoCNPJ = GlobalV1.CreateXML(ws08v2.dadosRedesim.fcpj.contato);
                }


                if (ws08v2.dadosRedesim.dadosCNPJ[0] != null)
                {
                    pXMlCNPJ = GlobalV1.CreateXML(ws08v2.dadosRedesim.dadosCNPJ[0]);
                }

            }
            #endregion

            #region  Serviço S15
            if (pCodServico == "S15")
            {
                resulOtros = regin.ServiceWs15(Identificacao, Recibo, NroAtoOficio, CodConvenioAto);
                if (resulOtros.status != "OK")
                {
                    throw new Exception("CompletaDadosDbeOracle S15 " + resulOtros.descricao);
                }
                XmlDBE = resulOtros.XmlDBE;

                //Ver se tem dados espeficicos e carregar na tabela
                WsServices15RFB.serviceResponse ws15 = new WsServices15RFB.serviceResponse();
                ws15 = (WsServices15RFB.serviceResponse)GlobalV1.CreateObject(resulOtros.XmlDBE, ws15);

                //Gravar  tabela com o ip que vem da RFB
                string IpMEI = "";
                if (ws15.dadosRedesim.dadosEspecificosMei != null && ws15.dadosRedesim.dadosEspecificosMei.ipOrigem != null
                    && ws15.dadosRedesim.dadosEspecificosMei.ipOrigem != "")
                {
                    IpMEI = ws15.dadosRedesim.dadosEspecificosMei.ipOrigem;
                }

                if (IpMEI != "")
                {
                    Helper hp = new Helper();
                    hp.Update_RUC_DADOS_SEGURANCA(bd, pProtocolo, IpMEI, "1");
                }


                if (ws15.dadosRedesim.simplesNacional != null)
                {
                    pXmlPeriodoSimples = GlobalV1.CreateXML(ws15.dadosRedesim.simplesNacional);
                }

                if (ws15.dadosRedesim.dadosCNPJ.contato != null)
                {
                    pXmlContatoCNPJ = GlobalV1.CreateXML(ws15.dadosRedesim.dadosCNPJ.contato);
                }

                if (pXmlContatoCNPJ == "" && ws15.dadosRedesim.fcpj != null && ws15.dadosRedesim.fcpj.contato != null)
                {
                    pXmlContatoCNPJ = GlobalV1.CreateXML(ws15.dadosRedesim.fcpj.contato);
                }

                if (ws15.dadosRedesim.dadosCNPJ != null)
                {
                    pXMlCNPJ = GlobalV1.CreateXML(ws15.dadosRedesim.dadosCNPJ);
                }

                #region Outros dados Dados do MEI (ocupação)
                using (OracleCommand cmdToExecute = new OracleCommand())
                {
                    cmdToExecute.Transaction = bd;
                    cmdToExecute.Connection = bd.Connection;
                    string Sql = " Delete RUC_CBO_ECON Where  RAE_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'";
                    cmdToExecute.CommandText = Sql.ToString();
                    cmdToExecute.ExecuteNonQuery();

                }

                //caso seja vazio como estamos processando um protocolo por dia praticamente
                // entao busco esse protococolo na tabela que tenha os eventos 703, 244 ou 210, para buscar esse dado da ocupação
                // e enviar para os entes
                // raul gamboa 29/03/2022
                if (ws15.dadosRedesim.dadosEspecificosMei == null || ws15.dadosRedesim.dadosEspecificosMei.codOcupacaoPrincipal == null)
                {
                    using (OracleCommand cmdToExecute = new OracleCommand())
                    {
                        cmdToExecute.Transaction = bd;
                        cmdToExecute.Connection = bd.Connection;
                        StringBuilder Sql = new StringBuilder();
                        Sql.AppendLine(" select max(t.t0101_id_rfb) Id ");
                        Sql.AppendLine(" from   T0101_RFB_PROCESSO_DEFERIDOS t, r0101_processo_ev_rfb ev");
                        Sql.AppendLine(" where  t.t0101_id_rfb = ev.r0101_t0101_id_rfb  ");
                        Sql.AppendLine(" and    t.t0101_protocolo_regin = '" + pProtocolo + "'");
                        Sql.AppendLine(" and    ev.r0101_mer_cod_evento in (703, 244, 210)");

                        cmdToExecute.CommandText = Sql.ToString();
                        object numeroId = cmdToExecute.ExecuteScalar();

                        if (numeroId.ToString() != "")
                        {
                            Sql = new StringBuilder();
                            DataTable toReturnPro = new DataTable();
                            Sql.AppendLine(" Select    t.t0101_dbe DBE, t.t0101_cod_serv_rfb ServicoRFB, ");
                            Sql.AppendLine("           t.t0101_numeroprotocolo ProtRedeSim, t.t0101_numero_ato_oficio NroAtoOficio, t.t0101_codigoconvenioato CodConvenioAto ");
                            Sql.AppendLine(" From      T0101_RFB_PROCESSO_DEFERIDOS t ");
                            Sql.AppendLine(" Where     1 = 1 ");
                            Sql.AppendLine(" And       t.t0101_id_rfb = " + numeroId);

                            cmdToExecute.CommandText = Sql.ToString();
                            cmdToExecute.CommandType = CommandType.Text;

                            cmdToExecute.Connection = bd.Connection;
                            //    cmdToExecute.Transaction = _conn;
                            OracleDataAdapter adapterPro = new OracleDataAdapter(cmdToExecute);
                            adapterPro = new OracleDataAdapter(cmdToExecute);
                            adapterPro.Fill(toReturnPro);
                            //caso encontre pego a nformação do s15 desse novo protocolo para ver se tem ocupação
                            if (toReturnPro.Rows.Count > 0)
                            {
                                string pDBEv2 = toReturnPro.Rows[0]["DBE"].ToString();
                                string pNroAtoOficiov2 = toReturnPro.Rows[0]["NroAtoOficio"].ToString();
                                string pCodConvenioAtov2 = toReturnPro.Rows[0]["CodConvenioAto"].ToString();
                                string Recibov2 = "";
                                string Identificacaov2 = "";

                                if (pDBEv2 != "")
                                {
                                    Recibov2 = pDBEv2.Substring(0, 10);
                                    Identificacaov2 = pDBEv2.Substring(10, 14);
                                }

                                WsServicesReginRFB.RetornoV2 v2resulOtros = regin.ServiceWs15(Identificacaov2, Recibov2, pNroAtoOficiov2, pCodConvenioAtov2);
                                if (v2resulOtros.status != "OK")
                                {
                                    throw new Exception("CompletaDadosDbeOracle v2 S15 " + v2resulOtros.descricao);
                                }
                                //Ver se tem dados espeficicos e carregar na tabela
                                WsServices15RFB.serviceResponse ws15v2 = new WsServices15RFB.serviceResponse();
                                ws15v2 = (WsServices15RFB.serviceResponse)GlobalV1.CreateObject(v2resulOtros.XmlDBE, ws15v2);
                                //caso tenha ocupação igualo a consulta original com a nova consulta para processar essa ocupação
                                if (ws15v2.dadosRedesim.dadosEspecificosMei != null && ws15v2.dadosRedesim.dadosEspecificosMei.codOcupacaoPrincipal != null)
                                {
                                    ws15.dadosRedesim.dadosEspecificosMei = ws15v2.dadosRedesim.dadosEspecificosMei;
                                }
                            }
                        }

                    }
                }
                //Para pegar as informaçoes do CBO que vem no MEI e enviar para os entes, isso foi feito para o MEI trasportador
                if (ws15.dadosRedesim.dadosEspecificosMei != null && ws15.dadosRedesim.dadosEspecificosMei.codOcupacaoPrincipal != null)
                {
                    string cboPrincipal = ws15.dadosRedesim.dadosEspecificosMei.codOcupacaoPrincipal;

                    if (cboPrincipal != "")
                    {
                        Ruc_Cbo_Econ re = new Ruc_Cbo_Econ();
                        re.rae_rge_pra_protocolo = pProtocolo;
                        re.rae_tae_cod_actvd = cboPrincipal;
                        re.rae_calif_actv = "1";
                        re.Update(bd);
                    }


                    if (ws15.dadosRedesim.dadosEspecificosMei.codOcupacaoSecundaria != null && ws15.dadosRedesim.dadosEspecificosMei.codOcupacaoSecundaria != null && ws15.dadosRedesim.dadosEspecificosMei.codOcupacaoSecundaria.Length > 0)
                    {
                        foreach (string cboSecundaria in ws15.dadosRedesim.dadosEspecificosMei.codOcupacaoSecundaria)
                        {
                            if (cboSecundaria != "")
                            {
                                Ruc_Cbo_Econ re = new Ruc_Cbo_Econ();
                                re.rae_rge_pra_protocolo = pProtocolo;
                                re.rae_tae_cod_actvd = cboSecundaria;
                                re.rae_calif_actv = "2";
                                re.Update(bd);
                            }
                        }
                    }
                }
                #endregion


                // Isto aqui e porque na FCN da RFB vem um endereço e no cNPJ tem outro, exemplo:, o MEI fez uma constituição e logo depois mudou o endereço
                // Com isso e endereço que ia no processo e o endereço novo e nao o endereço que foi constituida a empresa
                // Isso foi solicitado por PE na semana de 07/03/2022
                if (ws15.dadosRedesim.fcpj.endereco != null)
                {
                    pXMlEnderecoFcpj = GlobalV1.CreateXML(ws15.dadosRedesim.fcpj.endereco);

                    if (pXMlEnderecoFcpj != "")
                    {
                        Helper endereco = new Helper();
                        endereco.UpdateRucEstab(bd, pProtocolo, pXMlEnderecoFcpj, DtTipoLogra, pCodServico);
                    }
                }

                if (ws15.dadosRedesim.dadosCNPJ.enderecoCorrespondencia != null)
                {
                    WsServices15RFB.endereco end = new WsServices15RFB.endereco();

                    end = ws15.dadosRedesim.dadosCNPJ.enderecoCorrespondencia;

                    //psc.Ruc.Tablelas.Helper.Endereco end = new psc.Ruc.Tablelas.Helper.Endereco();
                    psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();

                    cc.Bairro = end.bairro == null ? "" : end.bairro;
                    cc.Cep = end.cep == null ? "" : end.cep;
                    cc.Codigo_municipio = end.codMunicipio == null ? "" : end.codMunicipio;
                    cc.Complemento = end.complementoLogradouro == null ? "" : end.complementoLogradouro;
                    cc.Logradouro = end.logradouro == null ? "" : end.logradouro;
                    cc.Numero = end.numLogradouro == null ? "" : end.numLogradouro;
                    cc.Pais = end.codPais == null ? "" : end.codPais;
                    cc.TipLogradoro = end.codTipoLogradouro == null ? "" : end.codTipoLogradouro;
                    cc.Uf = end.uf == null ? "" : end.uf;
                    dhe.gravarDadosEnderecoCorrespondencia(bd, cc, pProtocolo, pCnpjEmpresa, DtTipoLogra, pCodServico);
                }

            }
            #endregion

            #region  Serviço S17
            if (pCodServico == "S17")
            {
                resulOtros = regin.ServiceWs17(Identificacao, Recibo, NroAtoOficio, CodConvenioAto);
                if (resulOtros.status != "OK")
                {
                    throw new Exception("CompletaDadosDbeOracle S17 " + resulOtros.descricao);
                }
                XmlDBE = resulOtros.XmlDBE;

                //Ver se tem dados espeficicos e carregar na tabela
                WsServices17RFB.serviceResponse ws17v2 = new WsServices17RFB.serviceResponse();
                ws17v2 = (WsServices17RFB.serviceResponse)GlobalV1.CreateObject(resulOtros.XmlDBE, ws17v2);

                //ws17v2.dadosRedesim.dadosCNPJ

                if (ws17v2.dadosRedesim.dadosCNPJ[0].enderecoCorrespondencia != null)
                {
                    WsServices17RFB.endereco end = new WsServices17RFB.endereco();

                    end = ws17v2.dadosRedesim.dadosCNPJ[0].enderecoCorrespondencia;

                    //psc.Ruc.Tablelas.Helper.Endereco end = new psc.Ruc.Tablelas.Helper.Endereco();
                    psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();

                    cc.Bairro = end.bairro == null ? "" : end.bairro;
                    cc.Cep = end.cep == null ? "" : end.cep;
                    cc.Codigo_municipio = end.codMunicipio == null ? "" : end.codMunicipio;
                    cc.Complemento = end.complementoLogradouro == null ? "" : end.complementoLogradouro;
                    cc.Logradouro = end.logradouro == null ? "" : end.logradouro;
                    cc.Numero = end.numLogradouro == null ? "" : end.numLogradouro;
                    cc.Pais = end.codPais == null ? "" : end.codPais;
                    cc.TipLogradoro = end.codTipoLogradouro == null ? "" : end.codTipoLogradouro;
                    cc.Uf = end.uf == null ? "" : end.uf;
                    dhe.gravarDadosEnderecoCorrespondencia(bd, cc, pProtocolo, pCnpjEmpresa, DtTipoLogra, pCodServico);
                }

                if (ws17v2.dadosRedesim.simplesNacional != null)
                {
                    pXmlPeriodoSimples = GlobalV1.CreateXML(ws17v2.dadosRedesim.simplesNacional);
                }

                if (ws17v2.dadosRedesim.dadosCNPJ[0].contato != null)
                {
                    pXmlContatoCNPJ = GlobalV1.CreateXML(ws17v2.dadosRedesim.dadosCNPJ[0].contato);
                }


                if (pXmlContatoCNPJ == "" && ws17v2.dadosRedesim.fcpj != null && ws17v2.dadosRedesim.fcpj.contato != null)
                {
                    pXmlContatoCNPJ = GlobalV1.CreateXML(ws17v2.dadosRedesim.fcpj.contato);
                }


                if (ws17v2.dadosRedesim.dadosCNPJ[0] != null)
                {
                    pXMlCNPJ = GlobalV1.CreateXML(ws17v2.dadosRedesim.dadosCNPJ[0]);
                }


                if (ws17v2.dadosRedesim.dadosEspecificos != null && ws17v2.dadosRedesim.dadosEspecificos.cnpj != null)
                {
                    WsServices17RFB.dadosEspecificos de = ws17v2.dadosRedesim.dadosEspecificos;
                    dhe.Update_RUC_CARD(bd, pProtocolo,
                              GlobalV1.valNuloBranco(de.cpfSolicitante),
                               GlobalV1.valNuloBranco(de.indQualifSolicitante),
                               GlobalV1.valNuloBranco(de.cnpj),
                               GlobalV1.valNuloBranco(de.ufSelecionada),
                               GlobalV1.valNuloBranco(de.municipioSelecionado),
                               GlobalV1.valNuloBranco(de.eventos),
                               GlobalV1.valNuloBranco(de.indInscrReativAtualizEstado),
                               GlobalV1.valNuloBranco(de.indSubstTrib),
                               GlobalV1.valNuloBranco(de.indCCFE),
                               GlobalV1.valNuloBranco(de.indSTE),
                               GlobalV1.valNuloBranco(de.indCOE),
                               GlobalV1.valNuloBranco(de.indEDGTEEE),
                               GlobalV1.valNuloBranco(de.inscrMunLocal),
                               GlobalV1.valNuloBranco(de.inscrMunVinc),
                               GlobalV1.valNuloBranco(de.inscrOutroMun),
                               GlobalV1.valNuloBranco(de.numViabilidade),
                               GlobalV1.valNuloBranco(de.inscricaoMunicipal)
                               );
                }

                if (ws17v2.dadosRedesim.dadosInovaSimples != null)
                {
                    string formaCaptacao = "";
                    if (ws17v2.dadosRedesim.dadosInovaSimples.formaCaptacao != null)
                    {
                        foreach (string formaCaptacaoAl in ws17v2.dadosRedesim.dadosInovaSimples.formaCaptacao)
                        {
                            formaCaptacao += formaCaptacaoAl + ";";
                        }
                    }

                    if (formaCaptacao.Length > 2)
                    {
                        formaCaptacao = formaCaptacao.Substring(0, formaCaptacao.Length - 1);
                    }

                    Helper dh = new Helper();
                    WsServices17RFB.dadosInovaSimples de = ws17v2.dadosRedesim.dadosInovaSimples;
                    dh.Update_RUC_INOVA_SIMPLES(bd, pProtocolo,
                              GlobalV1.valNuloBranco(de.localAtuacao),
                               GlobalV1.valNuloBranco(de.cnpjEntidadeVinculada),
                               GlobalV1.valNuloBranco(formaCaptacao),
                               GlobalV1.valNuloBranco(de.outrasFormasCaptacao)
                               );
                }

            }
            #endregion




            if (XmlDBE == "")
                return;


            WsServicesReginRFB.serviceResponse dados = new WsServicesReginRFB.serviceResponse();
            dados = (WsServicesReginRFB.serviceResponse)GlobalV1.CreateObject(XmlDBE, dados);

            WsServices17RFB.dadosCNPJ dadosCNPJ = new WsServices17RFB.dadosCNPJ();

            if (pXMlCNPJ != "")
                dadosCNPJ = (WsServices17RFB.dadosCNPJ)GlobalV1.CreateObject(pXMlCNPJ, dadosCNPJ);


            #region Grava Periodos Simples
            if (pXmlPeriodoSimples != "")
            {
                T0101_RFB_PROCESSO_DEFERIDOS t01 = new T0101_RFB_PROCESSO_DEFERIDOS();
                t01.UpdateXmlSIMPLESMei(bd, pCnpjEmpresa, IdMySql, pXmlPeriodoSimples);
            }

            #endregion

            #endregion

            dhe.gravarDadosProcesso(bd, XmlDBE, pProtocolo, pCnpjEmpresa, pCodServico);

            Helper ch = new Helper();
            ch.gravarNodeQSAFichaRFB(bd, XmlDBE, pProtocolo, pCnpjEmpresa, pCodServico);

            //Buscar para ver se tem a tag sucessora
            dhe.gravarDadosSucessoraWs35(bd, XmlDBE, pProtocolo, pCnpjEmpresa, pCodServico);

            dhe.gravarEventosFaltantesDBEEventoWs35(bd, XmlDBE, pProtocolo, pCnpjEmpresa, pCodServico);

            //Pega o endereço do responsavel caso tenha, to fazedo isso so com MEI
            if (TIPOPERACAOPROT == "7") //MEI
            {
                dhe.gravarDadosEnderecoResponsavel(bd, XmlDBE, pProtocolo, pCnpjEmpresa, DtTipoLogra, pCodServico);
            }


            dhe.gravarDataSituacaoCadastalRucGeneral(bd, XmlDBE, pProtocolo, pCnpjEmpresa, pCodServico);

            using (OracleCommand cmdToExecute = new OracleCommand())
            {
                StringBuilder sqlD = new StringBuilder();
                cmdToExecute.Transaction = bd;
                cmdToExecute.Connection = bd.Connection;

                #region 218 (Alteração de correio eletrônico)

                //sqlD = new StringBuilder();
                //sqlD.AppendLine(" Select    Count(*) from TAB_INFORM_EXTRA_JUNTA ");
                //sqlD.AppendLine(" where	    TIE_PROTOCOLO = '" + pProtocolo + "'");
                //sqlD.AppendLine(" and       TIE_TIPO_RELACAO = 4  ");
                //sqlD.AppendLine(" and       TIE_EMAIL is not null  "); //Correio eletronico

                //cmdToExecute.Parameters.Clear();
                //cmdToExecute.CommandText = sqlD.ToString();
                //cmdToExecute.CommandType = CommandType.Text;
                //pCount = int.Parse(cmdToExecute.ExecuteScalar().ToString());

                //if (pCount == 0 && pXmlContatoCNPJ != "")
                if (pXmlContatoCNPJ != "")
                {
                    dhe.gravar218CorreioWs35(bd, pXmlContatoCNPJ, pProtocolo, pCnpjEmpresa, pCodServico);
                }

                #endregion

                #region 214 (Alteração de telefone (DDD/telefone))
                //comentado para pagar se tiver na RFB
                //sqlD = new StringBuilder();
                //sqlD.AppendLine(" Select    Count(*) from TAB_INFORM_EXTRA_JUNTA ");
                //sqlD.AppendLine(" where	    TIE_PROTOCOLO = '" + pProtocolo + "'");
                //sqlD.AppendLine(" and       TIE_TIPO_RELACAO = 4  ");
                //sqlD.AppendLine(" and       TIE_FONE1 is not null  "); //Correio eletronico

                //cmdToExecute.Parameters.Clear();
                //cmdToExecute.CommandText = sqlD.ToString();
                //cmdToExecute.CommandType = CommandType.Text;
                //pCount = int.Parse(cmdToExecute.ExecuteScalar().ToString());

                if (pXmlContatoCNPJ != "")
                {
                    //    dhe = new psc.Ruc.Tablelas.Helper.dHelper();

                    dhe.gravar214TelefoneWs35(bd, pXmlContatoCNPJ, pProtocolo, pCnpjEmpresa, pCodServico);

                }

                #endregion

                #region 216 (Alteração de fax (DDD/fax))

                //sqlD = new StringBuilder();
                //sqlD.AppendLine(" Select    Count(*) from TAB_INFORM_EXTRA_JUNTA ");
                //sqlD.AppendLine(" where	    TIE_PROTOCOLO = '" + pProtocolo + "'");
                //sqlD.AppendLine(" and       TIE_TIPO_RELACAO = 4  ");
                //sqlD.AppendLine(" and       TIE_FAX is not null  "); //Correio eletronico

                //cmdToExecute.Parameters.Clear();
                //cmdToExecute.CommandText = sqlD.ToString();
                //cmdToExecute.CommandType = CommandType.Text;
                //pCount = int.Parse(cmdToExecute.ExecuteScalar().ToString());

                if (XmlDBE != "")
                {
                    //      dhe = new psc.Ruc.Tablelas.Helper.dHelper();

                    dhe.gravar216FaxWs35(bd, XmlDBE, pProtocolo, pCnpjEmpresa, pCodServico);

                }

                #endregion

                #region 232 Contador


                if (dados.dadosRedesim.fcpj != null && (dados.dadosRedesim.fcpj.cnpjEmpresaContabil != null || dados.dadosRedesim.fcpj.cpfContadorPF != null))
                {
                    sqlD = new StringBuilder();
                    sqlD.AppendLine(" Select    Count(*) from ruc_relat_prof ");
                    sqlD.AppendLine(" where	    RRP_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                    sqlD.AppendLine(" and       RRP_TGE_VTIP_RELAC = 3  ");

                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;
                    pCount = int.Parse(cmdToExecute.ExecuteScalar().ToString());

                    string dadosResonse = GlobalV1.CreateXML(dados);

                    if (pCount == 0)
                    {
                        Helper he = new Helper();
                        he.gravarContadorWs35(bd, dadosResonse, pProtocolo, DtTipoLogra, pCodServico, "fcpj");
                    }
                }

                if (dadosCNPJ != null && dadosCNPJ.cpfContadorPF != null)
                {
                    sqlD = new StringBuilder();
                    sqlD.AppendLine(" Select    Count(*) from ruc_relat_prof ");
                    sqlD.AppendLine(" where	    RRP_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                    sqlD.AppendLine(" and       RRP_TGE_VTIP_RELAC = 3  ");

                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;
                    pCount = int.Parse(cmdToExecute.ExecuteScalar().ToString());

                    if (pCount == 0)
                    {
                        Helper he = new Helper();
                        he.gravarContadorWs35(bd, pXMlCNPJ, pProtocolo, DtTipoLogra, pCodServico, "dadosCNPJ");
                    }
                }

                #endregion

                #region 221 Nome de fantasia 221

                string NomeFantasia = "";
                if (dados.dadosRedesim.fcpj != null)
                {
                    NomeFantasia = dados.dadosRedesim.fcpj.nomeFantasia == null ? "" : dados.dadosRedesim.fcpj.nomeFantasia;
                }

                if (dadosCNPJ.nomeFantasia != null && dadosCNPJ.nomeFantasia != "")
                {
                    NomeFantasia = dadosCNPJ.nomeFantasia;
                }

                if (NomeFantasia != "")
                {
                    sqlD = new StringBuilder();
                    sqlD.AppendLine(" update RUC_ESTAB ");
                    sqlD.AppendLine(" set	 RES_NOM_ESTAB = :v_NomeFantasia");
                    sqlD.AppendLine("       , RES_IND_FANTASIA = 1 ");
                    sqlD.AppendLine(" where	 RES_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                    cmdToExecute.Parameters.Clear();

                    cmdToExecute.Parameters.Add(new OracleParameter("v_NomeFantasia", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, NomeFantasia));

                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;
                    cmdToExecute.ExecuteNonQuery();
                }

                #endregion

                #region verificar eventos comunes
                cmdToExecute.Parameters.Clear();
                sqlD = new StringBuilder();
                //Busco se tem QSA gravado
                sqlD.AppendLine(" select	* ");
                sqlD.AppendLine(" from	    psc_prot_evento_rfb");
                sqlD.AppendLine(" where	    PEV_PRO_PROTOCOLO = '" + pProtocolo + "'");
                sqlD.AppendLine(" and		PEV_COD_EVENTO in (517, 232, 237, 238, 257, 204) ");
                //sqlD.AppendLine(" and		PEV_COD_EVENTO in (517, 221) ");

                cmdToExecute.CommandText = sqlD.ToString();
                cmdToExecute.CommandType = CommandType.Text;

                DataTable toReturnEvento = new DataTable("Eventos");
                using (OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute))
                {
                    adapter.Fill(toReturnEvento);

                    for (int a = 0; a <= toReturnEvento.Rows.Count - 1; a++)
                    {
                        decimal evento = decimal.Parse(toReturnEvento.Rows[a]["PEV_COD_EVENTO"].ToString());



                        #region 517 Baixa
                        if (evento == 517)
                        {
                            //      psc.Ruc.Tablelas.Helper.dHelper dhe = new psc.Ruc.Tablelas.Helper.dHelper();
                            dhe.gravar517DadosBaixaWs35(bd, XmlDBE, pProtocolo, pCnpjEmpresa, pCodServico);
                        }
                        #endregion

                        #region 237 (Indicação de Preposto), 238 (Substituição de Preposto)
                        if (evento == 237 || evento == 238)
                        {
                            //      psc.Ruc.Tablelas.Helper.dHelper dhe = new psc.Ruc.Tablelas.Helper.dHelper();
                            Helper dheHq = new Helper();
                            dheHq.gravarPrepostoWs(bd, XmlDBE, pProtocolo, DtTipoLogra, pCnpjEmpresa, pCodServico);
                        }
                        #endregion

                        #region 257 (Alteração do número de registro no órgão competente)
                        if (evento == 257)
                        {
                            //     dhe = new psc.Ruc.Tablelas.Helper.dHelper();

                            dhe.gravar257AlteraNumeroOrgaRegistro35(bd, pProtocolo, pNumeroOrgaoRegistroWs11);

                        }
                        #endregion

                    }
                }

                #endregion

                #region Atualiza Referencia do endereço
                string referencia = "";
                if (dados.dadosRedesim != null && dados.dadosRedesim.fcpj != null && dados.dadosRedesim.fcpj.endereco != null && dados.dadosRedesim.fcpj.endereco.referencia != null && dados.dadosRedesim.fcpj.endereco.referencia != "")
                {
                    referencia = dados.dadosRedesim.fcpj.endereco.referencia;
                    sqlD = new StringBuilder();
                    sqlD.AppendLine(" update	ruc_estab ");
                    sqlD.AppendLine(" set	    RES_REFER = :v_RES_REFER");
                    sqlD.AppendLine(" where	    RES_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                    cmdToExecute.Parameters.Clear();
                    cmdToExecute.Parameters.Add(new OracleParameter("v_RES_REFER", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, referencia));
                    cmdToExecute.CommandText = sqlD.ToString();
                    cmdToExecute.CommandType = CommandType.Text;
                    cmdToExecute.ExecuteNonQuery();

                }
                #endregion
            }

        }



        public void ProcessodePegarXmlParaValidar(string pProtocolo, string pTipoOperacao)
        {
            try
            {
                string pUrlValidaXml = "";
                if (ConfigurationManager.AppSettings["UrlValidaXml"] != null && ConfigurationManager.AppSettings["UrlValidaXml"].ToString() != "")
                {
                    pUrlValidaXml = ConfigurationManager.AppSettings["UrlValidaXml"].ToString();
                }

                string pXml = "";
                #region Pega Xml Regin
                if (pUrlValidaXml != "")
                {
                    if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.CommandText = "ProcessodePegarXmlParaValidar";
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                                cmd.Parameters.Add(new SqlParameter("P_XML", SqlDbType.VarChar, 200000, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));


                                cmd.ExecuteNonQuery();
                                //OracleLob CLOB = (OracleLob)cmd.Parameters["pOutXml"].Value;

                                pXml = (string)cmd.Parameters["P_XML"].Value;
                            }
                        }
                    }
                    else
                    {
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                        {
                            conn.Open();
                            using (OracleCommand cmd = new OracleCommand())
                            {
                                cmd.Connection = conn;
                                cmd.CommandText = "PKG_JUCESC.Crear_Arch_Xml_Group";
                                cmd.CommandType = CommandType.StoredProcedure;

                                decimal pOrdem = int.MinValue;
                                string parametros = "";

                                cmd.Parameters.Add(new OracleParameter("P_COD_APLIC", OracleType.Char, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, "RECEITA"));
                                cmd.Parameters.Add(new OracleParameter("P_PARAMETRO0", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, pProtocolo));
                                cmd.Parameters.Add(new OracleParameter("P_PARAMETRO1", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                                cmd.Parameters.Add(new OracleParameter("P_PARAMETRO2", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                                cmd.Parameters.Add(new OracleParameter("P_PARAMETRO3", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                                cmd.Parameters.Add(new OracleParameter("P_PARAMETRO4", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                                cmd.Parameters.Add(new OracleParameter("P_PARAMETRO5", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                                cmd.Parameters.Add(new OracleParameter("P_PARAMETRO6", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                                cmd.Parameters.Add(new OracleParameter("P_PARAMETRO7", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                                cmd.Parameters.Add(new OracleParameter("P_PARAMETRO8", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                                cmd.Parameters.Add(new OracleParameter("P_PARAMETRO9", OracleType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, parametros));
                                cmd.Parameters.Add(new OracleParameter("p_ordem", OracleType.Number, 15, ParameterDirection.Input, false, 15, 0, "", DataRowVersion.Proposed, pOrdem));
                                cmd.Parameters.Add(new OracleParameter("P_XML", OracleType.Clob, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));


                                cmd.ExecuteNonQuery();
                                OracleLob CLOB = (OracleLob)cmd.Parameters["P_XML"].Value;

                                pXml = (string)CLOB.Value;

                            }
                        }

                    }
                }
                #endregion

                WsServicosConsulta.ServicosConsultas c = new WsServicosConsulta.ServicosConsultas();
                WsServicosConsulta.Retorno pRetur = new WsServicosConsulta.Retorno();
                /*
                 * Isto aquie  para ver se esta configurada a URL de validação, faz a validação., caso que nao esteja 
                 * atualiza como se estivesse passado pela validação
                 */
                if (pUrlValidaXml == "")
                {
                    pRetur.status = "OK";
                }
                else
                {
                    c.Url = pUrlValidaXml;
                    pRetur = c.ValidaXMLRegin(pXml, pProtocolo, pTipoOperacao, ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());
                }

                if (pRetur.status != "OK")
                {
                    string status = "9"; //Erro Normal com isso ele reenvia de novo o XML para validação
                    bool atualizou = dHelperQuery.UpdateCamposTabelasRuc(pRetur.XmlDBE, pProtocolo);
                    if (!atualizou)
                    {
                        status = "-9"; //Se passa por aqui e porque nao atualizou nada (cep) e outros campos, entao nao tenho porque reenviar
                                       // entao vou colocar um novo statatus para nao ficar revalidadando o XML, ate corregisr o Orgao de registro e refazer o xml
                                       //Reenviando novamente, so reenvio caso seja refeito o XML com dados novos (status generica 10 cod -9 pendete de validação)
                    }

                    atualizaProtocoloNOK(pProtocolo, " Validando XML " + pRetur.descricao, status);
                }
                else
                {
                    if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                        {
                            conn.Open();
                            using (SqlTransaction _conn = conn.BeginTransaction())
                            {
                                using (SqlCommand cmdToExecute = new SqlCommand())
                                {
                                    cmdToExecute.Connection = _conn.Connection;
                                    cmdToExecute.Transaction = _conn;

                                    StringBuilder sqlU = new StringBuilder();
                                    sqlU.AppendLine(" update MAC_LOG_CARGA_JUNTA_HOMOLOG set mlc_data_carrega_envio = null, mlc_data_valida_xml = getdate() where MLC_PROTOCOLO = '" + pProtocolo + "'");
                                    cmdToExecute.Parameters.Clear();
                                    cmdToExecute.CommandText = sqlU.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }
                                _conn.Commit();
                            }
                        }
                    }
                    else
                    {
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                        {
                            conn.Open();
                            using (OracleTransaction _conn = conn.BeginTransaction())
                            {
                                using (OracleCommand cmdToExecute = new OracleCommand())
                                {
                                    cmdToExecute.Connection = _conn.Connection;
                                    cmdToExecute.Transaction = _conn;

                                    StringBuilder sqlU = new StringBuilder();
                                    sqlU.AppendLine(" update MAC_LOG_CARGA_JUNTA_HOMOLOG set mlc_data_carrega_envio = null, mlc_data_valida_xml = sysdate where MLC_PROTOCOLO = '" + pProtocolo + "'");
                                    cmdToExecute.Parameters.Clear();
                                    cmdToExecute.CommandText = sqlU.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }
                                _conn.Commit();
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                atualizaProtocoloNOK(pProtocolo, " Validando XML " + ex.Message + " StackTrace " + ex.StackTrace);

            }

        }

        public void CompletaGravaDadosWs11(string pProtocolo, string pCNPJ, string pDbe, string pOrigemDado, DataSet dsTabelas)
        {
            WsServicesReginRFB.Retorno result = new WsServicesReginRFB.Retorno();
            WsServicesReginRFB.Retorno result11 = new WsServicesReginRFB.Retorno();
            try
            {

                DataTable DtTipoLogra = dsTabelas.Tables["TIPODELOGRADOURO"];


                //pProtocolo = "RJP1700186636";
                //pCNPJ = "11950487006554";

                #region update Command


                StringBuilder sql = new StringBuilder();
                StringBuilder sqlD = new StringBuilder();
                StringBuilder sqlU = new StringBuilder();


                #endregion

                if (pCNPJ.Trim() == "")
                {
                    throw new Exception("CNPJ vazio não consegue pegar os dados ws 11");
                }


                WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();

                //WsServicesReginRFB.Retorno resulRegin = new WsServicesReginRFB.Retorno();

                regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                result11 = regin.ServiceWs11(pCNPJ);

                //result11 = ServiceWs11(pCNPJ);

                if (result11.status == "OK")
                {

                    string TipoDeUnidade = "";
                    string TipoDeUnidadeOutros = "";
                    string FormaAtuacaoOutros = "";
                    string FormaAtuacao = "";
                    string CNPJMatriz = "";
                    string pRESTIPESTAB = "1";
                    string RGE_OPT_SIMP = result11.oCNPJResponse.dadosCNPJ[0].opcaoSimplesNacional.ToString();
                    string RGE_OPT_SIMEI = result11.oCNPJResponse.dadosCNPJ[0].opcaoSimei.ToString();
                    string RGE_TGE_VTAMANHO = result11.oCNPJResponse.dadosCNPJ[0].porte;
                    string MotivoBaixaRFB = "";// getMotivoBaixaRFB("RJ6109290312188216000101");
                    string NaturezaJuridicaRFB = "";
                    decimal pValorCapitalMatriz = 0;

                    string TIPOPERACAOPROT = "";
                    string pCodMunic = "";
                    string pvpv_cod_protocolo = "";


                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                    {
                        conn.Open();
                        using (SqlCommand cmdToExecute = new SqlCommand())
                        {
                            DataTable toReturnPro = new DataTable("DadosProcesso");

                            sqlD = new StringBuilder();

                            sqlD.AppendLine(" select	PRO_TIP_OPERACAO, pro_tmu_cod_mun CodMunic, ");
                            sqlD.AppendLine("           vpv_cod_protocolo ");
                            sqlD.AppendLine(" from	    PSC_PROTOCOLO ");
                            sqlD.AppendLine("           left join VIA_PROTOCOLO_VIAB ");
                            sqlD.AppendLine("           on pro_vpv_cod_protocolo = vpv_cod_protocolo ");
                            sqlD.AppendLine(" where	    1 = 1 ");
                            //  sqlD.AppendLine(" and       pro_vpv_cod_protocolo = vpv_cod_protocolo(+) ");
                            sqlD.AppendLine(" and       PRO_PROTOCOLO = '" + pProtocolo + "'");

                            cmdToExecute.CommandText = sqlD.ToString();
                            cmdToExecute.CommandType = CommandType.Text;

                            cmdToExecute.Connection = conn;
                            //    cmdToExecute.Transaction = _conn;

                            SqlDataAdapter adapterPro = new SqlDataAdapter(cmdToExecute);
                            adapterPro.Fill(toReturnPro);

                            TIPOPERACAOPROT = toReturnPro.Rows[0]["PRO_TIP_OPERACAO"].ToString();
                            pCodMunic = toReturnPro.Rows[0]["CodMunic"].ToString();
                            pvpv_cod_protocolo = toReturnPro.Rows[0]["vpv_cod_protocolo"].ToString();

                            adapterPro.Dispose();

                        }
                    }





                    if (result11.oCNPJResponse.dadosCNPJ[0].cnpjMatriz != null)
                    {
                        CNPJMatriz = result11.oCNPJResponse.dadosCNPJ[0].cnpjMatriz;
                    }

                    if (result11.oCNPJResponse.dadosCNPJ[0].naturezaJuridica != null)
                    {
                        NaturezaJuridicaRFB = result11.oCNPJResponse.dadosCNPJ[0].naturezaJuridica.Trim();
                    }


                    if (result11.oCNPJResponse.dadosCNPJ[0].tipoUnidade != null)
                    {
                        foreach (string tipoUni in result11.oCNPJResponse.dadosCNPJ[0].tipoUnidade)
                        {
                            TipoDeUnidadeOutros += tipoUni + ";";
                        }
                    }

                    if (TipoDeUnidadeOutros.Length > 2)
                    {
                        TipoDeUnidadeOutros = TipoDeUnidadeOutros.Substring(0, TipoDeUnidadeOutros.Length - 1);
                        TipoDeUnidade = TipoDeUnidadeOutros.Substring(0, 2);
                    }



                    if (result11.oCNPJResponse.dadosCNPJ[0].formaAtuacao != null)
                    {
                        foreach (string forma in result11.oCNPJResponse.dadosCNPJ[0].formaAtuacao)
                        {
                            FormaAtuacaoOutros += forma + ";";
                        }
                    }

                    if (FormaAtuacaoOutros.Length > 2)
                    {
                        FormaAtuacaoOutros = FormaAtuacaoOutros.Substring(0, FormaAtuacaoOutros.Length - 1);
                        FormaAtuacao = FormaAtuacaoOutros.Substring(0, 2);
                    }

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                    {
                        conn.Open();

                        using (SqlTransaction _conn = conn.BeginTransaction())
                        {
                            /*
                                Aqui e para atualizar os dados da empresa da RFB, menos QSA
                             * 5 - S08 rECEITA fEDERAL
                             * 16 - Processo de Sistema Solicitação do Municipio (Outro sistema)
                             * 6 - Processo de Solicitação SEFAZ
                             * 25 - Solicitação de MEI quando nao consegue atualizar na Junta comercial
                             * 27 - Sustituto tributario S17 registros marcados para SEFAZ
                             * 
                             */
                            //string pOrigemDado = "";
                            if (pOrigemDado == "6" || pOrigemDado == "16" || pOrigemDado == "5" || pOrigemDado == "25" || pOrigemDado == "27")
                            {
                                psc.Ruc.Tablelas.Helper.dHelper em = new psc.Ruc.Tablelas.Helper.dHelper();
                                string ResponseRFBEmpresa = GlobalV1.CreateXML(result11.oCNPJResponse);
                                em.GravaWsRFB11RucSqlServer(_conn, ResponseRFBEmpresa, pProtocolo);
                            }

                            using (SqlCommand cmdToExecute = new SqlCommand())
                            {
                                if (result11.oCNPJResponse.dadosCNPJ[0].indMatrizFilial == "2")
                                {
                                    pRESTIPESTAB = "2";
                                }

                                cmdToExecute.Connection = _conn.Connection;
                                cmdToExecute.Transaction = _conn;

                                #region BuscarDadoMotivoBaixa
                                cmdToExecute.Parameters.Clear();
                                sqlD = new StringBuilder();
                                //Busco se tem QSA gravado
                                sqlD.AppendLine(" select	count(*) ");
                                sqlD.AppendLine(" from	    psc_prot_evento_rfb");
                                sqlD.AppendLine(" where	    PEV_PRO_PROTOCOLO = '" + pProtocolo + "'");
                                sqlD.AppendLine(" and		PEV_COD_EVENTO = 517 ");

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                int pCountBaixa = (int)cmdToExecute.ExecuteScalar();

                                if (pCountBaixa > 0)
                                {
                                    MotivoBaixaRFB = getMotivoBaixaRFB(pDbe);
                                }
                                #endregion

                                #region Busca QSA na Receita federal caso nao tenha encontrado o QSA na Junta Comercial
                                cmdToExecute.Parameters.Clear();
                                sqlD = new StringBuilder();

                                //Busco se tem QSA gravado
                                sqlD.AppendLine(" select	count(*) ");
                                sqlD.AppendLine(" from	    ruc_relat_prof  ");
                                sqlD.AppendLine(" where	    RRP_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                sqlD.AppendLine(" and		RRP_TGE_VTIP_RELAC = 2  ");

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                int pCountQSA = (int)cmdToExecute.ExecuteScalar();

                                if (pCountQSA == 0 || pRESTIPESTAB == "2")
                                {
                                    WsServicesReginRFB.Retorno resultMatiz = new WsServicesReginRFB.Retorno();
                                    resultMatiz = result11;
                                    if (pRESTIPESTAB == "2") //Filial
                                    {
                                        if (result11.oCNPJResponse.dadosCNPJ[0].indMatrizFilial == "2")
                                        {
                                            resultMatiz = regin.ServiceWs11(result11.oCNPJResponse.dadosCNPJ[0].cnpjMatriz);

                                            if (resultMatiz.status != "OK")
                                            {
                                                throw new Exception("Erro ao Buscar o CNPJ da matriz quando e filial erro: " + resultMatiz.descricao);
                                            }

                                        }

                                        if (resultMatiz.oCNPJResponse.dadosCNPJ[0].capitalSocial != null)
                                            pValorCapitalMatriz = decimal.Parse(resultMatiz.oCNPJResponse.dadosCNPJ[0].capitalSocial) / 100;

                                    }
                                    if (pCountQSA == 0)
                                    {
                                        psc.Ruc.Tablelas.Helper.dHelper c = new psc.Ruc.Tablelas.Helper.dHelper();
                                        string ResponseRFB = GlobalV1.CreateXML(resultMatiz.oCNPJResponse);
                                        c.GravaCompletaRucQsaSqlServer(_conn, ResponseRFB, pProtocolo, DtTipoLogra);
                                    }
                                }

                                cmdToExecute.Parameters.Clear();

                                #endregion


                                sqlD = new StringBuilder();

                                string pTIE_CNPJ_REGISTRO = "09280442000103";
                                // 234-8 - Empresa Simples de Inovação, solictação da sefaz, mantis 10096
                                if (NaturezaJuridicaRFB == "2348")
                                    pTIE_CNPJ_REGISTRO = "00394460005887";

                                sqlD.AppendLine(" update TAB_INFORM_EXTRA_JUNTA ");
                                sqlD.AppendLine(" set	 TIE_FORMA_ATUACAO = @v_TIE_FORMA_ATUACAO, ");
                                sqlD.AppendLine("        TIE_FORMA_ATUACAO_OUTROS = @v_TIE_FORMA_ATUACAO_OUTROS, ");
                                sqlD.AppendLine("        TIE_CPF_CNPJ = @v_TIE_CPF_CNPJ, ");
                                sqlD.AppendLine(" 		 TIE_TIPO_UNIDADE = @v_TIE_TIPO_UNIDADE, ");
                                sqlD.AppendLine(" 		 TIE_TIPO_UNIDADE_OUTROS = @v_TIE_TIPO_UNIDADE_OUTROS, ");
                                sqlD.AppendLine(" 		 TIE_ORGAO_REGISTRO = 1, ");
                                sqlD.AppendLine(" 		 TIE_CNPJ_REGISTRO = @v_TIE_CNPJ_REGISTRO ");
                                sqlD.AppendLine(" where	 TIE_PROTOCOLO = @v_TIE_PROTOCOLO ");
                                sqlD.AppendLine(" and	 TIE_TIPO_RELACAO = 4 ");

                                cmdToExecute.Parameters.Clear();
                                cmdToExecute.Parameters.Add(new SqlParameter("v_TIE_PROTOCOLO", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                                cmdToExecute.Parameters.Add(new SqlParameter("v_TIE_FORMA_ATUACAO", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FormaAtuacao));
                                cmdToExecute.Parameters.Add(new SqlParameter("v_TIE_FORMA_ATUACAO_OUTROS", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FormaAtuacaoOutros));
                                cmdToExecute.Parameters.Add(new SqlParameter("v_TIE_CPF_CNPJ", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCNPJ));
                                cmdToExecute.Parameters.Add(new SqlParameter("v_TIE_TIPO_UNIDADE", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, TipoDeUnidade));
                                cmdToExecute.Parameters.Add(new SqlParameter("v_TIE_CNPJ_REGISTRO", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pTIE_CNPJ_REGISTRO));
                                cmdToExecute.Parameters.Add(new SqlParameter("v_TIE_TIPO_UNIDADE_OUTROS", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, TipoDeUnidadeOutros));

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                if (cmdToExecute.ExecuteNonQuery() == 0)
                                {
                                    sqlD = new StringBuilder();
                                    sqlD.AppendLine(" Insert into TAB_INFORM_EXTRA_JUNTA (TIE_PROTOCOLO, TIE_TIPO_RELACAO, TIE_CPF_CNPJ, TIE_FORMA_ATUACAO, TIE_FORMA_ATUACAO_OUTROS, TIE_TIPO_UNIDADE, TIE_TIPO_UNIDADE_OUTROS, TIE_ORGAO_REGISTRO, TIE_CNPJ_REGISTRO) ");
                                    sqlD.AppendLine(" Values (@v_TIE_PROTOCOLO, 4, @v_TIE_CPF_CNPJ, @v_TIE_FORMA_ATUACAO, @v_TIE_FORMA_ATUACAO_OUTROS, @v_TIE_TIPO_UNIDADE, @v_TIE_TIPO_UNIDADE_OUTROS, '1', @v_TIE_CNPJ_REGISTRO) ");
                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();

                                }

                                if (pValorCapitalMatriz > 0)
                                {
                                    sqlD = new StringBuilder();
                                    cmdToExecute.Parameters.Clear();

                                    sqlD.AppendLine(" update RUC_COMP ");
                                    sqlD.AppendLine(" set	 RCO_VAL_CAP_SOC_MATRIZ = @v_RCO_VAL_CAP_SOC_MATRIZ ");
                                    sqlD.AppendLine(" where	 RCO_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");

                                    cmdToExecute.Parameters.Add(new SqlParameter("v_RCO_VAL_CAP_SOC_MATRIZ", SqlDbType.Decimal, 0, ParameterDirection.Input, true, 15, 2, "", DataRowVersion.Proposed, pValorCapitalMatriz));

                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }


                                /*
                                   Atualiza RUC_ESTAB
                                */
                                sqlD = new StringBuilder();
                                sqlD.AppendLine(" update RUC_ESTAB ");
                                sqlD.AppendLine(" set	RES_CNPJ_SEDE = '" + CNPJMatriz + "'");
                                sqlD.AppendLine(",      RES_TIP_ESTAB = " + pRESTIPESTAB);
                                sqlD.AppendLine(" where	RES_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");

                                cmdToExecute.Parameters.Clear();
                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();

                                #region Atualiza QSA
                                StringBuilder sSocio = new StringBuilder();
                                sSocio.AppendLine("select	distinct RRP_RGE_PRA_PROTOCOLO, RRP_CGC_CPF_SECD, ");
                                sSocio.AppendLine("         RPR_FEC_CONST_NASC, ");
                                sSocio.AppendLine("         RPR_NUME, ");
                                sSocio.AppendLine("         RPR_TTL_TIP_LOGRADORO, ");
                                sSocio.AppendLine("         RPR_DIRECCION, ");
                                sSocio.AppendLine("         RPR_URBANIZACION, ");
                                sSocio.AppendLine("         RPR_TES_COD_ESTADO, ");
                                sSocio.AppendLine("         RPR_ZONA_POSTAL, ");
                                sSocio.AppendLine("         RPR_TMU_COD_MUN, ");
                                sSocio.AppendLine("         RPR_FEC_CONST_NASC, ");
                                sSocio.AppendLine("         len(LTrim(RTrim(RRP_CGC_CPF_SECD))) Tipo ");
                                sSocio.AppendLine("from	    ruc_prof, ruc_relat_prof ");
                                sSocio.AppendLine("where	RPR_RGE_PRA_PROTOCOLO = RRP_RGE_PRA_PROTOCOLO ");
                                sSocio.AppendLine("and		RRP_CGC_CPF_SECD = RPR_CGC_CPF_SECD ");
                                sSocio.AppendLine("and		RRP_TGE_VTIP_RELAC not in( 3) ");
                                sSocio.AppendLine("And      RPR_TES_COD_ESTADO <> 'EX' ");
                                sSocio.AppendLine("and		RRP_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                sSocio.AppendLine("and		(isnull(RPR_NUME, '') = '' ");
                                sSocio.AppendLine("             or		isnull(RPR_TTL_TIP_LOGRADORO, '') = '' ");
                                sSocio.AppendLine("             or		isnull(RPR_DIRECCION, '') = '' ");
                                sSocio.AppendLine("             or		isnull(RPR_URBANIZACION, '') = '' ");
                                sSocio.AppendLine("             or		isnull(RPR_TES_COD_ESTADO,'') = ''  ");
                                sSocio.AppendLine("             or		isnull(RPR_ZONA_POSTAL, '') = '' ");
                                sSocio.AppendLine("             or		len(isnull(RPR_TMU_COD_MUN, '0')) < 2 ");
                                sSocio.AppendLine("             or		isnull(RPR_FEC_CONST_NASC, '') = '' ");
                                sSocio.AppendLine("         ) ");


                                cmdToExecute.CommandText = sSocio.ToString();
                                cmdToExecute.CommandType = CommandType.Text;

                                DataTable toReturn = new DataTable("QSA");
                                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
                                adapter.Fill(toReturn);

                                for (int a = 0; a <= toReturn.Rows.Count - 1; a++)
                                {
                                    try
                                    {
                                        WsServicesReginRFB.Retorno ws0911Socio = new WsServicesReginRFB.Retorno();
                                        DateTime FecNacimento = new DateTime();
                                        string pRPR_NUME = "";
                                        string pRPR_TTL_TIP_LOGRADORO = "";
                                        string pRPR_DIRECCION = "";
                                        string pRPR_URBANIZACION = "";
                                        string pRPR_TES_COD_ESTADO = "";
                                        string pRPR_ZONA_POSTAL = "";
                                        string pRPR_TMU_COD_MUN = "";
                                        string pRPR_IDENT_COMP = "";
                                        string pRPR_TGE_VPAIS = "";
                                        FecNacimento = DateTime.MinValue;
                                        string CpfCnpj = toReturn.Rows[a]["RRP_CGC_CPF_SECD"].ToString().Trim();

                                        if (toReturn.Rows[a]["Tipo"].ToString() != "11" && toReturn.Rows[a]["Tipo"].ToString() != "14")
                                        {
                                            throw new Exception("Erro:" + " cpf ou cnpj errado " + toReturn.Rows[a]["RRP_CGC_CPF_SECD"].ToString());
                                        }

                                        if (toReturn.Rows[a]["Tipo"].ToString() == "11")
                                        {
                                            #region CPF

                                            regin = new WsServicesReginRFB.ServiceReginRFB();
                                            regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                                            ws0911Socio = regin.ServiceWs09(CpfCnpj);

                                            if (ws0911Socio.status == "OK")
                                            {
                                                if (toReturn.Rows[a]["RPR_FEC_CONST_NASC"].ToString() == "")
                                                {
                                                    if (ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].dataNascimento != "")
                                                    {
                                                        string data = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].dataNascimento;
                                                        FecNacimento = DateTime.ParseExact(data, "yyyyMMdd", null);
                                                    }
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

                                                    pRPR_DIRECCION = cc.Logradouro;
                                                    pRPR_NUME = cc.Numero;
                                                    pRPR_TGE_VPAIS = cc.Pais;
                                                    pRPR_TTL_TIP_LOGRADORO = cc.TipLogradoro;
                                                    pRPR_URBANIZACION = cc.Bairro;
                                                    pRPR_TES_COD_ESTADO = cc.Uf;
                                                    pRPR_ZONA_POSTAL = cc.Cep;
                                                    pRPR_IDENT_COMP = cc.Complemento;
                                                    pRPR_TMU_COD_MUN = cc.Codigo_municipio;
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception("Erro:" + ws0911Socio.descricao + " ao tentar buscar o cpf " + toReturn.Rows[a]["RRP_CGC_CPF_SECD"].ToString());
                                            }
                                            #endregion
                                        }
                                        if (toReturn.Rows[a]["Tipo"].ToString() == "14")
                                        {
                                            #region CNPJ
                                            regin = new WsServicesReginRFB.ServiceReginRFB();
                                            regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                                            ws0911Socio = regin.ServiceWs11(CpfCnpj);

                                            if (ws0911Socio.status == "OK")
                                            {
                                                if (toReturn.Rows[a]["RPR_FEC_CONST_NASC"].ToString() == "")
                                                {
                                                    if (ws0911Socio.oCNPJResponse.dadosCNPJ[0].dataAberturaEmpresa != "")
                                                    {
                                                        string data = ws0911Socio.oCNPJResponse.dadosCNPJ[0].dataAberturaEmpresa;
                                                        FecNacimento = DateTime.ParseExact(data, "yyyyMMdd", null);
                                                    }
                                                }

                                                if (ws0911Socio.oCNPJResponse.dadosCNPJ[0] != null &&
                                                    ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco != null)
                                                {
                                                    psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                                                    cc.Bairro = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.bairro == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.bairro;
                                                    cc.Cep = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.cep == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.cep;
                                                    cc.Codigo_municipio = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codMunicipio == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codMunicipio;
                                                    cc.Complemento = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.complementoLogradouro == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.complementoLogradouro;
                                                    cc.Logradouro = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.logradouro == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.logradouro;
                                                    cc.Numero = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.numLogradouro == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.numLogradouro;
                                                    cc.Pais = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codPais == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codPais;
                                                    cc.TipLogradoro = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codTipoLogradouro == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codTipoLogradouro;
                                                    cc.Uf = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.uf == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.uf;

                                                    cc.TrataEndereco(ref cc, DtTipoLogra);

                                                    pRPR_DIRECCION = cc.Logradouro;
                                                    pRPR_NUME = cc.Numero;
                                                    pRPR_TGE_VPAIS = cc.Pais;
                                                    pRPR_TTL_TIP_LOGRADORO = cc.TipLogradoro;
                                                    pRPR_URBANIZACION = cc.Bairro;
                                                    pRPR_TES_COD_ESTADO = cc.Uf;
                                                    pRPR_ZONA_POSTAL = cc.Cep;
                                                    pRPR_IDENT_COMP = cc.Complemento;
                                                    pRPR_TMU_COD_MUN = cc.Codigo_municipio;
                                                }


                                            }
                                            else
                                            {
                                                throw new Exception("Erro:" + ws0911Socio.descricao + " ao tentar buscar o cpf " + toReturn.Rows[a]["RRP_CGC_CPF_SECD"].ToString());
                                            }
                                            #endregion
                                        }

                                        if (FecNacimento != DateTime.MinValue)
                                        {
                                            sSocio = new StringBuilder();
                                            sSocio.AppendLine(" update	ruc_prof ");
                                            sSocio.AppendLine(" set		RPR_FEC_CONST_NASC = @v_FecNacimento");
                                            sSocio.AppendLine(" where	RPR_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                            sSocio.AppendLine(" and		RPR_CGC_CPF_SECD = '" + toReturn.Rows[a]["RRP_CGC_CPF_SECD"].ToString() + "'");

                                            cmdToExecute.Parameters.Clear();
                                            cmdToExecute.Parameters.Add(new SqlParameter("v_FecNacimento", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FecNacimento));


                                            cmdToExecute.CommandText = sSocio.ToString();
                                            cmdToExecute.CommandType = CommandType.Text;
                                            cmdToExecute.ExecuteNonQuery();


                                        }

                                        if (pRPR_DIRECCION != "" || pRPR_TMU_COD_MUN != "")
                                        {
                                            if (toReturn.Rows[a]["RPR_NUME"].ToString() == "" ||
                                                toReturn.Rows[a]["RPR_TTL_TIP_LOGRADORO"].ToString() == "" ||
                                                toReturn.Rows[a]["RPR_DIRECCION"].ToString() == "" ||
                                                toReturn.Rows[a]["RPR_URBANIZACION"].ToString() == "" ||
                                                //toReturn.Rows[a]["RPR_TES_COD_ESTADO"].ToString() == "" ||
                                                toReturn.Rows[a]["RPR_ZONA_POSTAL"].ToString() == "" ||
                                                toReturn.Rows[a]["RPR_TMU_COD_MUN"].ToString() == "")
                                            {
                                                sSocio = new StringBuilder();
                                                sSocio.AppendLine(" update	ruc_prof ");
                                                sSocio.AppendLine(" set		RPR_ORIGEM_ENDERECO = 2, "); //Origem RFB
                                                sSocio.AppendLine("         RPR_NUME = @v_RPR_NUME,");
                                                sSocio.AppendLine("         RPR_TTL_TIP_LOGRADORO = @v_RPR_TTL_TIP_LOGRADORO, ");
                                                sSocio.AppendLine("         RPR_DIRECCION = @v_RPR_DIRECCION, ");
                                                sSocio.AppendLine("         RPR_URBANIZACION = @v_RPR_URBANIZACION, ");
                                                sSocio.AppendLine("         RPR_IDENT_COMP = @v_RPR_IDENT_COMP, ");
                                                sSocio.AppendLine("         RPR_ZONA_POSTAL = @v_RPR_ZONA_POSTAL, ");
                                                sSocio.AppendLine("         RPR_TES_COD_ESTADO = @v_RPR_TES_COD_ESTADO, ");
                                                sSocio.AppendLine("         RPR_TMU_COD_MUN = @v_RPR_TMU_COD_MUN, ");
                                                sSocio.AppendLine("         RPR_TGE_VPAIS = @v_RPR_TGE_VPAIS ");
                                                sSocio.AppendLine(" where	RPR_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                                sSocio.AppendLine(" and		RPR_CGC_CPF_SECD = '" + toReturn.Rows[a]["RRP_CGC_CPF_SECD"].ToString() + "'");

                                                cmdToExecute.Parameters.Clear();
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_NUME", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_NUME));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_TTL_TIP_LOGRADORO", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TTL_TIP_LOGRADORO));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_DIRECCION", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_DIRECCION));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_URBANIZACION", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_URBANIZACION));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_IDENT_COMP", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_IDENT_COMP));

                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_ZONA_POSTAL", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_ZONA_POSTAL));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_TES_COD_ESTADO", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TES_COD_ESTADO));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_TMU_COD_MUN", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TMU_COD_MUN));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_TGE_VPAIS", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TGE_VPAIS == "" ? null : pRPR_TGE_VPAIS));

                                                cmdToExecute.CommandText = sSocio.ToString();
                                                cmdToExecute.CommandType = CommandType.Text;
                                                cmdToExecute.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("erro ao completar QSA:" + ex.Message + " StackTrace " + ex.StackTrace);
                                    }
                                }
                                #endregion


                                sqlD = new StringBuilder();
                                sqlD.AppendLine(" update RUC_GENERAL ");
                                sqlD.AppendLine(" set	RGE_OPT_SIMP = '" + RGE_OPT_SIMP + "'");
                                sqlD.AppendLine("     , RGE_MOT_BAIXA_RFB = '" + MotivoBaixaRFB + "'");
                                sqlD.AppendLine(" where	RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");

                                cmdToExecute.Parameters.Clear();
                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();

                                if (RGE_OPT_SIMEI == "S" && NaturezaJuridicaRFB == "2135")
                                {
                                    //Marcar quando e MEI
                                    sqlD = new StringBuilder();
                                    sqlD.AppendLine(" update RUC_GENERAL ");
                                    sqlD.AppendLine(" set	RGE_TGE_VTIP_REG = 13");
                                    sqlD.AppendLine(" where	RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");

                                    cmdToExecute.Parameters.Clear();
                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();

                                }


                                #region Atualiza Endereço do representante caso falte

                                StringBuilder sRepre = new StringBuilder();
                                sRepre.AppendLine(" select	RSR_CGC_CPF_SECD, RSR_CGC_CPF_PRINC ");
                                sRepre.AppendLine(" from	ruc_representantes ");
                                sRepre.AppendLine(" where	1 = 1 ");
                                sRepre.AppendLine(" and     RSR_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                sRepre.AppendLine(" and		(len(isnull(RSR_TTL_TIP_LOGRADORO, '')) = 0 ");
                                sRepre.AppendLine(" 		or		len(isnull(RSR_DIRECCION, '')) = 0 ");
                                sRepre.AppendLine(" 		or		len(isnull(RSR_NUME, '')) = 0 ");
                                sRepre.AppendLine(" 		or		len(isnull(RSR_URBANIZACION, '')) = 0  ");
                                sRepre.AppendLine(" 		or		len(isnull(RSR_TMU_COD_MUN, '0')) < 2  ");
                                sRepre.AppendLine(" 		or		len(isnull(RSR_TES_COD_ESTADO, '')) = 0  ");
                                sRepre.AppendLine(" 		or		len(isnull(RSR_ZONA_POSTAL, '')) = 0  ");
                                sRepre.AppendLine(" 		) ");

                                cmdToExecute.CommandText = sRepre.ToString();
                                cmdToExecute.CommandType = CommandType.Text;

                                toReturn = new DataTable("Representante");
                                adapter = new SqlDataAdapter(cmdToExecute);
                                adapter.Fill(toReturn);

                                for (int a = 0; a <= toReturn.Rows.Count - 1; a++)
                                {
                                    try
                                    {
                                        WsServicesReginRFB.Retorno ws0911Socio = new WsServicesReginRFB.Retorno();
                                        string pRPR_NUME = "";
                                        string pRPR_TTL_TIP_LOGRADORO = "";
                                        string pRPR_DIRECCION = "";
                                        string pRPR_URBANIZACION = "";
                                        string pRPR_TES_COD_ESTADO = "";
                                        string pRPR_ZONA_POSTAL = "";
                                        string pRPR_TMU_COD_MUN = "";
                                        string pRPR_IDENT_COMP = "";
                                        string pRPR_TGE_VPAIS = "";
                                        string CpfCnpj = toReturn.Rows[a]["RSR_CGC_CPF_SECD"].ToString().Trim();
                                        if (CpfCnpj.Length == 11)
                                        {
                                            #region CPF

                                            regin = new WsServicesReginRFB.ServiceReginRFB();
                                            regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                                            ws0911Socio = regin.ServiceWs09(CpfCnpj);

                                            if (ws0911Socio.status == "OK")
                                            {

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

                                                    pRPR_DIRECCION = cc.Logradouro;
                                                    pRPR_NUME = cc.Numero;
                                                    pRPR_TGE_VPAIS = cc.Pais;
                                                    pRPR_TTL_TIP_LOGRADORO = cc.TipLogradoro;
                                                    pRPR_URBANIZACION = cc.Bairro;
                                                    pRPR_TES_COD_ESTADO = cc.Uf;
                                                    pRPR_ZONA_POSTAL = cc.Cep;
                                                    pRPR_IDENT_COMP = cc.Complemento;
                                                    pRPR_TMU_COD_MUN = cc.Codigo_municipio;
                                                }
                                                sSocio = new StringBuilder();
                                                sSocio.AppendLine(" update	ruc_representantes ");
                                                sSocio.AppendLine(" set		"); //Origem RFB
                                                sSocio.AppendLine("         RSR_NUME = @v_RPR_NUME,");
                                                sSocio.AppendLine("         RSR_TTL_TIP_LOGRADORO = @v_RPR_TTL_TIP_LOGRADORO, ");
                                                sSocio.AppendLine("         RSR_DIRECCION = @v_RPR_DIRECCION, ");
                                                sSocio.AppendLine("         RSR_URBANIZACION = @v_RPR_URBANIZACION, ");
                                                sSocio.AppendLine("         RSR_ZONA_POSTAL = @v_RPR_ZONA_POSTAL, ");
                                                sSocio.AppendLine("         RSR_TES_COD_ESTADO = @v_RPR_TES_COD_ESTADO, ");
                                                sSocio.AppendLine("         RSR_TMU_COD_MUN = @v_RPR_TMU_COD_MUN, ");
                                                sSocio.AppendLine("         RSR_TGE_VPAIS = @v_RPR_TGE_VPAIS,  ");
                                                sSocio.AppendLine("         RSR_IDENT_COMP = @v_RSR_IDENT_COMP ");
                                                sSocio.AppendLine(" where	RSR_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                                sSocio.AppendLine(" and		RSR_CGC_CPF_SECD = '" + toReturn.Rows[a]["RSR_CGC_CPF_SECD"].ToString() + "'");
                                                sSocio.AppendLine(" and     RSR_CGC_CPF_PRINC = '" + toReturn.Rows[a]["RSR_CGC_CPF_PRINC"].ToString() + "'");


                                                cmdToExecute.Parameters.Clear();
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_NUME", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_NUME));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_TTL_TIP_LOGRADORO", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TTL_TIP_LOGRADORO));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_DIRECCION", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_DIRECCION));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_URBANIZACION", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_URBANIZACION));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RSR_IDENT_COMP", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_IDENT_COMP));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_ZONA_POSTAL", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_ZONA_POSTAL));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_TES_COD_ESTADO", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TES_COD_ESTADO));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_TMU_COD_MUN", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TMU_COD_MUN));
                                                cmdToExecute.Parameters.Add(new SqlParameter("v_RPR_TGE_VPAIS", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TGE_VPAIS == "" ? null : pRPR_TGE_VPAIS));

                                                cmdToExecute.CommandText = sSocio.ToString();
                                                cmdToExecute.CommandType = CommandType.Text;
                                                cmdToExecute.ExecuteNonQuery();

                                            }
                                            else
                                            {
                                                throw new Exception("Erro:" + ws0911Socio.descricao + " ao tentar buscar o cpf " + CpfCnpj);
                                            }
                                            #endregion
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("erro ao completar Endereço Representante QSA:" + ex.Message + " StackTrace " + ex.StackTrace);
                                    }

                                }
                                #endregion

                                if (TIPOPERACAOPROT == "7")
                                {
                                    DateTime DtHoje = DateTime.Now;//dHelper.SysdateOracle();
                                    using (psc.Ruc.Tablelas.Ruc.Ruc_Gen_Protocolo_sqlserver gc = new psc.Ruc.Tablelas.Ruc.Ruc_Gen_Protocolo_sqlserver())
                                    {
                                        gc.rgp_rge_pra_protocolo = pProtocolo;
                                        gc.rgp_tge_tip_tab = 902;
                                        gc.rgp_tge_cod_tip_tab = 10;
                                        gc.rgp_valor = "NÃO";
                                        gc.rgp_valor_cod = "2";
                                        gc.rgp_tus_cod_usr = "REGIN";
                                        gc.rgp_fec_actl = DtHoje;
                                        if (gc.rgp_valor != "")
                                        {
                                            gc.Update(_conn);
                                        }
                                    }
                                }
                                if (TIPOPERACAOPROT == "51" && pOrigemDado == "16")
                                {
                                    DateTime DtHoje = DateTime.Now;//dHelper.SysdateOracle();
                                    using (psc.Ruc.Tablelas.Ruc.Ruc_Gen_Protocolo_sqlserver gc = new psc.Ruc.Tablelas.Ruc.Ruc_Gen_Protocolo_sqlserver())
                                    {
                                        gc.rgp_rge_pra_protocolo = pProtocolo;
                                        gc.rgp_tge_tip_tab = 902;
                                        gc.rgp_tge_cod_tip_tab = 10;
                                        gc.rgp_valor = "Sim";
                                        gc.rgp_valor_cod = "1";
                                        gc.rgp_tus_cod_usr = "REGIN";
                                        gc.rgp_fec_actl = DtHoje;
                                        if (gc.rgp_valor != "")
                                        {
                                            gc.Update(_conn);
                                        }
                                    }
                                }

                                sqlU.AppendLine(" update MAC_LOG_CARGA_JUNTA_HOMOLOG set mlc_data_carrega_envio = null, mlc_data_valida_xml = null, MLC_DATA_CARREGA_WS11 = getdate() where MLC_PROTOCOLO = '" + pProtocolo + "'");
                                cmdToExecute.Parameters.Clear();
                                cmdToExecute.CommandText = sqlU.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();
                            }


                            _conn.Commit();
                        }
                    }
                }
                else
                {
                    atualizaProtocoloNOK(pProtocolo, result11.descricao);
                }
            }
            catch (Exception ex)
            {
                atualizaProtocoloNOK(pProtocolo, ex.Message + " StackTrace " + ex.StackTrace);

            }
        }
        #endregion

        #region GravaDadosWs11Oracle
        [WebMethod]
        public void CompletaDadosReginOracle()
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            //DataSet dsTabelasTeste = new DataSet();
            //DataTable DtTipoLograTeste = GlobalV1.BuscarTipoLogradouro();
            //dsTabelasTeste.Tables.Add(DtTipoLograTeste.Copy());
            //string CNPJTeste = "12467416025350";
            //string pProtocoloTeste = "87900000014830";
            ////string pDbeTeste = "rj8388445500499162000116";
            //string pDbeTeste = "";
            //CompletaGravaDadosWs11Oracle(pProtocoloTeste, CNPJTeste, pDbeTeste, "", dsTabelasTeste);
            //return;

            #region Processo de completa dados Oracle

            DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
            {
                using (OracleCommand cmdToExecute = new OracleCommand())
                {
                    //DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
                    cmdToExecute.CommandText = "PKG_JUCESC.processodeactualizacaows11";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;

                    cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

                    _conn.Open();

                    cmdToExecute.Connection = _conn;

                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute))
                    {
                        adapter.Fill(toReturn);
                    }
                }
            }


            DataSet dsTabelas = new DataSet();
            if (toReturn.Rows.Count > 0)
            {
                DataTable DtTipoLogra = GlobalV1.BuscarTipoLogradouro();
                dsTabelas.Tables.Add(DtTipoLogra.Copy());
            }

            for (int a = 0; a < toReturn.Rows.Count; a++)
            {
                string CNPJ = toReturn.Rows[a]["pcnpj"].ToString().Trim();
                string pProtocolo = toReturn.Rows[a]["pprotocolo"].ToString().Trim();
                string pDbe = toReturn.Rows[a]["pDbe"].ToString().Trim();
                string pOrigemDado = toReturn.Rows[a]["pOrigemDado"].ToString().Trim();
                CompletaGravaDadosWs11Oracle(pProtocolo, CNPJ, pDbe, pOrigemDado, dsTabelas);
            }
            #endregion

            #region Processo de Para validar Xml para ser enviado
            toReturn = new DataTable("WBS_CONTROL_ENVIO");

            using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
            {
                using (OracleCommand cmdToExecute = new OracleCommand())
                {
                    //DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
                    cmdToExecute.CommandText = "PKG_JUCESC.processodeValidaXMLRegin";
                    cmdToExecute.CommandType = CommandType.StoredProcedure;

                    cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

                    _conn.Open();

                    cmdToExecute.Connection = _conn;

                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute))
                    {
                        adapter.Fill(toReturn);
                    }
                }
            }

            for (int a = 0; a < toReturn.Rows.Count; a++)
            {
                string CNPJ = toReturn.Rows[a]["pcnpj"].ToString().Trim();
                string pProtocolo = toReturn.Rows[a]["pprotocolo"].ToString().Trim();
                string pTipoOperacao = toReturn.Rows[a]["pTipoOperacao"].ToString().Trim();
                ProcessodePegarXmlParaValidar(pProtocolo, pTipoOperacao);
            }
            #endregion
        }

        [WebMethod]
        [Obsolete]
        public void CompletaGravaDadosWs11Oracle(string pProtocolo, string pCNPJ, string pDbe, string pOrigemDado, DataSet dsTabelas)
        {
            //WsServicesReginRFB.Retorno result = new WsServicesReginRFB.Retorno();
            WsServicesReginRFB.Retorno result11 = new WsServicesReginRFB.Retorno();
            try
            {

                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                DataTable DtTipoLogra = dsTabelas.Tables["TIPODELOGRADOURO"];


                #region update Command


                StringBuilder sql = new StringBuilder();
                StringBuilder sqlD = new StringBuilder();
                StringBuilder sqlU = new StringBuilder();
                // DateTime pDtDeferimentoMEITipo25 = new DateTime();
                // pDtDeferimentoMEITipo25 = DateTime.MinValue;

                #endregion

                if (pCNPJ.Trim() == "")
                {
                    throw new Exception("CNPJ vazio não consegue pegar os dados ws 11");
                }


                #region Veifica se Registro de MEI com pendencia

                if (pOrigemDado == "2555")
                {
                    try
                    {
                        DataTable toReturnws11 = new DataTable("WBS_CONTROL_ENVIO");
                        using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
                        {
                            using (OracleCommand cmdToExecute = new OracleCommand())
                            {
                                cmdToExecute.CommandText += " Select a.t0101_id_rfb, a.t0101_indicador_mei IndMei, a.t0101_xml_rfb XMLws11,  a.t0101_dt_deferimento dtDeferimento ";
                                cmdToExecute.CommandText += " from   t0101_rfb_processo_deferidos a ";
                                cmdToExecute.CommandText += " Where  a.t0101_protocolo_regin = '" + pProtocolo + "'";
                                //cmdToExecute.CommandText += " And    a.t0101_id_rfb = 119294 ";
                                cmdToExecute.CommandText += " And    rownum = 1";

                                cmdToExecute.CommandType = CommandType.Text;

                                _conn.Open();

                                cmdToExecute.Connection = _conn;

                                using (OracleDataAdapter adapws11 = new OracleDataAdapter(cmdToExecute))
                                {
                                    adapws11.Fill(toReturnws11);
                                }
                            }
                        }
                        if (toReturnws11.Rows.Count > 0)
                        {
                            //pDtDeferimentoMEITipo25 = DateTime.Parse(toReturnws11.Rows[0]["dtDeferimento"].ToString());
                            WsServices11RFB.consultaCNPJResponse result11ResponseNovo = new WsServices11RFB.consultaCNPJResponse();
                            WsServicesReginRFB.retornoWS11Redesim result11ResponseNovo2 = new WsServicesReginRFB.retornoWS11Redesim();
                            result11ResponseNovo = (WsServices11RFB.consultaCNPJResponse)GlobalV1.CreateObject(toReturnws11.Rows[0]["XMLws11"].ToString(), result11ResponseNovo);
                            string pXMlretornoWS11Redesim = GlobalV1.CreateXML(result11ResponseNovo.retornoWS11Redesim);

                            result11.status = "OK";
                            result11.codretorno = "00";
                            result11.oCNPJResponse = (WsServicesReginRFB.retornoWS11Redesim)GlobalV1.CreateObject(pXMlretornoWS11Redesim, result11ResponseNovo2);

                            if (result11.oCNPJResponse.dadosCNPJ[0].cnpj.Trim() != pCNPJ)
                            {
                                result11 = new WsServicesReginRFB.Retorno();
                            }

                        }
                    }
                    catch
                    {
                        result11 = new WsServicesReginRFB.Retorno();
                    }
                }

                #endregion

                DataTable toReturnPro = new DataTable("DadosProcesso");
                string TIPOPERACAOPROT = "";
                string pCodMunic = "";
                string pNroTVL = "";
                string pvpv_cod_protocolo = "";
                string IdMySql = "";
                string pServicoRFB = "S35";
                string pProtUnico = "";
                string NroAtoOficio = "";
                string CodConvenioAto = "";
                string pSituacaoRFB = "";
                string IndBalcaoUnico = "N";
                string ufPscProtocolo = "";
                string MunicipioPscProtocolo = "";
                string NomePscProtocolo = "";
                string pUnidadeDependente = "";




                using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (OracleCommand cmdToExecute = new OracleCommand())
                    {
                        sqlD = new StringBuilder();

                        sqlD.AppendLine(" select	PRO_T0101_ID_RFB IdMySql, PRO_NR_REQUERIMENTO, PRO_TIP_OPERACAO, pro_tmu_cod_mun CodMunic, ");
                        sqlD.AppendLine("           VPV_VIABILIDADE NroTVL, vpv_cod_protocolo, PIP_NOME_RAZAO_SOCIAL, PRO_TMU_TUF_UF, PRO_TMU_COD_MUN, ");
                        sqlD.AppendLine("           vpv_unidade_dependente UnidadeDependente ");
                        sqlD.AppendLine(" from	    PSC_PROTOCOLO, VIA_PROTOCOLO_VIAB, PSC_IDENT_PROTOCOLO ");
                        sqlD.AppendLine(" where	    1 = 1 ");
                        sqlD.AppendLine(" AND       PRO_PROTOCOLO = pip_pro_protocolo ");
                        sqlD.AppendLine(" and       pro_vpv_cod_protocolo = vpv_cod_protocolo(+) ");
                        sqlD.AppendLine(" and       PRO_PROTOCOLO = '" + pProtocolo + "'");

                        cmdToExecute.CommandText = sqlD.ToString();
                        cmdToExecute.CommandType = CommandType.Text;

                        cmdToExecute.Connection = conn;
                        //    cmdToExecute.Transaction = _conn;

                        OracleDataAdapter adapterPro = new OracleDataAdapter(cmdToExecute);
                        adapterPro.Fill(toReturnPro);

                        ufPscProtocolo = toReturnPro.Rows[0]["PRO_TMU_TUF_UF"].ToString();
                        MunicipioPscProtocolo = toReturnPro.Rows[0]["PRO_TMU_COD_MUN"].ToString();
                        NomePscProtocolo = toReturnPro.Rows[0]["PIP_NOME_RAZAO_SOCIAL"].ToString();
                        pUnidadeDependente = toReturnPro.Rows[0]["UnidadeDependente"].ToString();

                        if (NomePscProtocolo.IndexOf("CNPJ") >= 0)
                        {
                            throw new Exception("Nome da empresa aparentemente errado " + NomePscProtocolo);
                        }

                        TIPOPERACAOPROT = toReturnPro.Rows[0]["PRO_TIP_OPERACAO"].ToString();
                        pCodMunic = toReturnPro.Rows[0]["CodMunic"].ToString();
                        pNroTVL = toReturnPro.Rows[0]["NroTVL"].ToString();
                        pvpv_cod_protocolo = toReturnPro.Rows[0]["vpv_cod_protocolo"].ToString();
                        IdMySql = toReturnPro.Rows[0]["IdMySql"].ToString();

                        if (!String.IsNullOrEmpty(pvpv_cod_protocolo))
                        {
                            if (pvpv_cod_protocolo.Substring(2, 1) == "B")
                                IndBalcaoUnico = "S";
                        }

                        if (IdMySql != "")
                        {
                            //Verifica dados do SXX 
                            sqlD = new StringBuilder();
                            toReturnPro = new DataTable();
                            sqlD.AppendLine(" Select    t.t0101_dbe DBE, t.t0101_cod_serv_rfb ServicoRFB, ");
                            sqlD.AppendLine("           t.t0101_numeroprotocolo ProtRedeSim, t.t0101_numero_ato_oficio NroAtoOficio, t.t0101_codigoconvenioato CodConvenioAto ");
                            sqlD.AppendLine(" From      T0101_RFB_PROCESSO_DEFERIDOS t ");
                            sqlD.AppendLine(" Where     1 = 1 ");
                            sqlD.AppendLine(" And       t.t0101_id_rfb = " + IdMySql);

                            cmdToExecute.CommandText = sqlD.ToString();
                            cmdToExecute.CommandType = CommandType.Text;

                            cmdToExecute.Connection = conn;
                            //    cmdToExecute.Transaction = _conn;

                            adapterPro = new OracleDataAdapter(cmdToExecute);
                            adapterPro.Fill(toReturnPro);

                            if (toReturnPro.Rows.Count > 0)
                            {
                                pServicoRFB = toReturnPro.Rows[0]["ServicoRFB"].ToString();
                                pProtUnico = toReturnPro.Rows[0]["ProtRedeSim"].ToString();
                                NroAtoOficio = toReturnPro.Rows[0]["NroAtoOficio"].ToString();
                                CodConvenioAto = toReturnPro.Rows[0]["CodConvenioAto"].ToString();
                            }
                        }
                        adapterPro.Dispose();

                    }
                }


                WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();
                regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                //Aqui so entra se nao for orgigem 25, porque se nao ele busca o XML baixado na epoca que foi feito o MEI
                if (result11.status != "OK")
                {
                    result11 = regin.ServiceWs11(pCNPJ);
                    //result11 = ServiceWs11(pCNPJ);
                }

                if (result11.status == "OK")
                {
                    string TipoDeUnidade = "";
                    string TipoDeUnidadeOutros = "";
                    string FormaAtuacaoOutros = "";
                    string FormaAtuacao = "";
                    string CNPJMatriz = "";
                    string pRESTIPESTAB = "1";
                    string RGE_OPT_SIMP = result11.oCNPJResponse.dadosCNPJ[0].opcaoSimplesNacional.ToString();
                    string RGE_OPT_SIMEI = result11.oCNPJResponse.dadosCNPJ[0].opcaoSimei.ToString();
                    string RGE_TGE_VTAMANHO = result11.oCNPJResponse.dadosCNPJ[0].porte;
                    string pCodNatureza = "";
                    decimal pValorCapitalMatriz = 0;
                    decimal pValorCapitalEmpresa = 0;
                    string pNumeroOrgaoRegistroWs11 = "";
                    string pFilialDeOutroEstado = "NAO";
                    string pUfOrgaoDeRegistro = "";

                    //string MotivoBaixaRFB = "";

                    string ResponseRFBEmpresa = GlobalV1.CreateXML(result11.oCNPJResponse);

                    if (result11.oCNPJResponse.dadosCNPJ[0].capitalSocial != null)
                        pValorCapitalEmpresa = decimal.Parse(result11.oCNPJResponse.dadosCNPJ[0].capitalSocial) / 100;


                    if (result11.oCNPJResponse.dadosCNPJ[0].cnpjMatriz != null)
                    {
                        CNPJMatriz = result11.oCNPJResponse.dadosCNPJ[0].cnpjMatriz;
                    }

                    #region pega o numero de Matricula ou nire
                    if (result11.oCNPJResponse.dadosCNPJ[0].numeroOrgaoRegistro != null)
                    {
                        try
                        {
                            pNumeroOrgaoRegistroWs11 = decimal.Parse(result11.oCNPJResponse.dadosCNPJ[0].numeroOrgaoRegistro).ToString();
                        }
                        catch
                        {
                            pNumeroOrgaoRegistroWs11 = "";
                        }
                        //nirefilial = decimal.Parse(dMatriz.numeroRegistroFilial[i].ToString()).ToString();


                        if (pNumeroOrgaoRegistroWs11 == "0")
                            pNumeroOrgaoRegistroWs11 = "";

                        if (pNumeroOrgaoRegistroWs11.Length > 11)
                            pNumeroOrgaoRegistroWs11 = "";
                    }
                    #endregion

                    DateTime rge_fec_ini_act_ec = new DateTime();

                    if (GlobalV1.valNuloBranco(result11.oCNPJResponse.dadosCNPJ[0].dataAberturaEstabelecimento) != "")
                        rge_fec_ini_act_ec = GlobalV1.ConvertStringDateTime(result11.oCNPJResponse.dadosCNPJ[0].dataAberturaEstabelecimento);


                    if (GlobalV1.valNuloBranco(result11.oCNPJResponse.dadosCNPJ[0].situacaoCadastral) != "")
                        pSituacaoRFB = result11.oCNPJResponse.dadosCNPJ[0].situacaoCadastral;

                    if (result11.oCNPJResponse.dadosCNPJ[0].naturezaJuridica != null)
                    {
                        pCodNatureza = result11.oCNPJResponse.dadosCNPJ[0].naturezaJuridica.Trim();
                    }

                    if (result11.oCNPJResponse.dadosCNPJ[0].tipoUnidade != null)
                    {
                        foreach (string tipoUni in result11.oCNPJResponse.dadosCNPJ[0].tipoUnidade)
                        {
                            TipoDeUnidadeOutros += tipoUni + ";";
                        }
                    }

                    if (TipoDeUnidadeOutros.Length > 2)
                    {
                        TipoDeUnidadeOutros = TipoDeUnidadeOutros.Substring(0, TipoDeUnidadeOutros.Length - 1);
                        TipoDeUnidade = TipoDeUnidadeOutros.Substring(0, 2);
                    }

                    if (result11.oCNPJResponse.dadosCNPJ[0].formaAtuacao != null)
                    {
                        foreach (string forma in result11.oCNPJResponse.dadosCNPJ[0].formaAtuacao)
                        {
                            FormaAtuacaoOutros += forma + ";";
                        }
                    }

                    if (FormaAtuacaoOutros.Length > 2)
                    {
                        FormaAtuacaoOutros = FormaAtuacaoOutros.Substring(0, FormaAtuacaoOutros.Length - 1);
                        FormaAtuacao = FormaAtuacaoOutros.Substring(0, 2);
                    }

                    using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                    {
                        conn.Open();
                        using (OracleTransaction _conn = conn.BeginTransaction())
                        {





                            /*
                                Aqui e para atualizar os dados da empresa da RFB, menos QSA
                             * 5 - S08 rECEITA fEDERAL
                             * 16 - Processo de Sistema Solicitação do Municipio (Outro sistema)
                             * 6 - Processo de Solicitação SEFAZ
                             * 25 - Solicitação de MEI quando nao consegue atualizar na Junta comercial
                             * 27 - Sustituto tributario S17 registros marcados para SEFAZ
                             * 26 - SImple Nacional
                             * 
                             */
                            if (pOrigemDado == "6" || pOrigemDado == "16" || pOrigemDado == "5" || pOrigemDado == "25" || pOrigemDado == "26" || pOrigemDado == "27")
                            {
                                psc.Ruc.Tablelas.Helper.dHelper em = new psc.Ruc.Tablelas.Helper.dHelper();
                                em.GravaWsRFB11RucOracle(_conn, ResponseRFBEmpresa, pProtocolo);
                            }



                            using (OracleCommand cmdToExecute = new OracleCommand())
                            {
                                cmdToExecute.Connection = _conn.Connection;
                                cmdToExecute.Transaction = _conn;

                                #region Buscar Dados da UF

                                cmdToExecute.Parameters.Clear();
                                sqlD = new StringBuilder();

                                //Busco se tem QSA gravado
                                sqlD.AppendLine(" select pcr_uf from psc_config_regin  ");

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                object pUfOR = cmdToExecute.ExecuteScalar();
                                pUfOrgaoDeRegistro = pUfOR.ToString();

                                #endregion




                                if (result11.oCNPJResponse.dadosCNPJ[0].indMatrizFilial == "2")
                                {
                                    pRESTIPESTAB = "2";
                                    if (result11.oCNPJResponse.dadosCNPJ[0].endereco.uf != null && result11.oCNPJResponse.dadosCNPJ[0].endereco.uf != ""
                                            && result11.oCNPJResponse.dadosCNPJ[0].ufMatriz != null && result11.oCNPJResponse.dadosCNPJ[0].ufMatriz != "")
                                    {
                                        if (result11.oCNPJResponse.dadosCNPJ[0].endereco.uf != result11.oCNPJResponse.dadosCNPJ[0].ufMatriz)
                                        {
                                            {
                                                pFilialDeOutroEstado = "SIM";
                                            }
                                        }
                                    }
                                }

                                cmdToExecute.Connection = _conn.Connection;
                                cmdToExecute.Transaction = _conn;

                                if (pNumeroOrgaoRegistroWs11 != "")
                                {
                                    //Atualiza o numero de matricula, caso nao tenha o protocolo
                                    psc.Ruc.Tablelas.Helper.dHelper dhe = new psc.Ruc.Tablelas.Helper.dHelper();
                                    dhe.gravar257AlteraNumeroOrgaRegistro35(_conn, pProtocolo, pNumeroOrgaoRegistroWs11);
                                }

                                #region atualiza dados psc_protocolo e psc_ident_protocolo
                                if (ufPscProtocolo == "" || MunicipioPscProtocolo == "")
                                {
                                    MunicipioPscProtocolo = result11.oCNPJResponse.dadosCNPJ[0].endereco.codMunicipio;
                                    ufPscProtocolo = result11.oCNPJResponse.dadosCNPJ[0].endereco.uf;

                                    MunicipioPscProtocolo = Helper.CalDvMunicipio(MunicipioPscProtocolo);

                                    ufPscProtocolo = result11.oCNPJResponse.dadosCNPJ[0].endereco.uf;

                                    sqlD = new StringBuilder();
                                    sqlD.AppendLine(" update PSC_PROTOCOLO ");
                                    sqlD.AppendLine(" set	PRO_TMU_TUF_UF = :v_PRO_TMU_TUF_UF,");
                                    sqlD.AppendLine(" 	    PRO_TMU_COD_MUN = :v_PRO_TMU_COD_MUN");
                                    sqlD.AppendLine(" where	PRO_PROTOCOLO = '" + pProtocolo + "'");

                                    cmdToExecute.Parameters.Clear();
                                    cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_TMU_TUF_UF", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ufPscProtocolo));
                                    cmdToExecute.Parameters.Add(new OracleParameter("v_PRO_TMU_COD_MUN", OracleType.Number, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, MunicipioPscProtocolo));

                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }

                                if (NomePscProtocolo == "")
                                {
                                    NomePscProtocolo = result11.oCNPJResponse.dadosCNPJ[0].nomeEmpresarial;

                                    sqlD = new StringBuilder();
                                    sqlD.AppendLine(" update PSC_IDENT_PROTOCOLO ");
                                    sqlD.AppendLine(" set	PIP_NOME_RAZAO_SOCIAL = :v_PIP_NOME_RAZAO_SOCIAL");
                                    sqlD.AppendLine(" where	PIP_PRO_PROTOCOLO = '" + pProtocolo + "'");

                                    cmdToExecute.Parameters.Clear();
                                    cmdToExecute.Parameters.Add(new OracleParameter("v_PIP_NOME_RAZAO_SOCIAL", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, NomePscProtocolo));

                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }


                                #endregion


                                #region Busca Contador Requerimento
                                sqlD = new StringBuilder();
                                sqlD.AppendLine(" Select    Count(*) from ruc_relat_prof ");
                                sqlD.AppendLine(" where	    RRP_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                sqlD.AppendLine(" and       RRP_TGE_VTIP_RELAC = 3  ");

                                cmdToExecute.Parameters.Clear();
                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                int pCount = int.Parse(cmdToExecute.ExecuteScalar().ToString());

                                if (pCount == 0 && IndBalcaoUnico == "S")
                                {
                                    //Ve se tem contador no Requerimento e atualiza ou COloca os dados no Xml
                                    //Vou comentar para pegar somente da rfb ja que hoje esta com dados inconsistentes, deixando digitar 
                                    //maria por exemplo no crc e o cep tb coletado esta invalido

                                    if (1 == 1 && toReturnPro.Rows.Count > 0)
                                    {
                                        string NroRequerimento = toReturnPro.Rows[0]["PRO_NR_REQUERIMENTO"].ToString();

                                        if (NroRequerimento != "")
                                        {
                                            psc.Ruc.Tablelas.Helper.dHelper gRequ = new psc.Ruc.Tablelas.Helper.dHelper();

                                            gRequ.gravarContadorRequerimento(_conn, pProtocolo, NroRequerimento);
                                        }
                                    }
                                }

                                #endregion

                                #region Preenche CNAE RFB caso nao tenha

                                cmdToExecute.Parameters.Clear();
                                sqlD = new StringBuilder();

                                //Busco se tem QSA gravado
                                sqlD.AppendLine(" select	count(*) ");
                                sqlD.AppendLine(" from	    ruc_actv_econ  ");
                                sqlD.AppendLine(" where	    rae_rge_pra_protocolo = '" + pProtocolo + "'");

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                object pCountCNAE = cmdToExecute.ExecuteScalar();
                                int ppCountCNAE = int.Parse(pCountCNAE.ToString());
                                if (ppCountCNAE == 0)
                                {
                                    psc.Ruc.Tablelas.Helper.dHelper em = new psc.Ruc.Tablelas.Helper.dHelper();
                                    //string ResponseRFBEmpresa = GlobalV1.CreateXML(result11.oCNPJResponse);
                                    em.gravarRucActvEconRFB(_conn, ResponseRFBEmpresa, pProtocolo);
                                }

                                #endregion

                                #region Busca QSA na Receita federal caso nao tenha encontrado o QSA na Junta Comercial E valida se tem natureza Juridica

                                cmdToExecute.Parameters.Clear();
                                sqlD = new StringBuilder();

                                //Busco se tem QSA gravado
                                sqlD.AppendLine(" select	count(*) ");
                                sqlD.AppendLine(" From      ruc_comp  ");
                                sqlD.AppendLine(" where	    rco_rge_pra_protocolo = '" + pProtocolo + "'");
                                sqlD.AppendLine(" And       Length(nvl(RCO_TNC_COD_NATUR,1)) < 4 ");

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                object ppCountNatur = cmdToExecute.ExecuteScalar();
                                int pCountNatur = int.Parse(ppCountNatur.ToString());


                                cmdToExecute.Parameters.Clear();
                                sqlD = new StringBuilder();

                                //Busco se tem QSA gravado
                                sqlD.AppendLine(" select	count(*) ");
                                sqlD.AppendLine(" from	    ruc_relat_prof  ");
                                sqlD.AppendLine(" where	    RRP_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                sqlD.AppendLine(" and		RRP_TGE_VTIP_RELAC = 2  ");

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                object ppCountQSA = cmdToExecute.ExecuteScalar();
                                int pCountQSA = int.Parse(ppCountQSA.ToString());

                                if (pCountQSA == 0 || pCountNatur > 0 || pRESTIPESTAB == "2")
                                {
                                    WsServicesReginRFB.Retorno resultMatiz = new WsServicesReginRFB.Retorno();
                                    resultMatiz = result11;

                                    if (pRESTIPESTAB == "2") //Filial
                                    {
                                        /*
                                         * Verifica se a matriz esta na mesma UF da filial,
                                         * caso nao esteja e porque a matriz e de outra UF e vamos carregar o QSA da RFB
                                         * combinado com xico dia 12/11/2019
                                         */
                                        if (result11.oCNPJResponse.dadosCNPJ[0].endereco.uf != null && result11.oCNPJResponse.dadosCNPJ[0].endereco.uf != ""
                                            && result11.oCNPJResponse.dadosCNPJ[0].ufMatriz != null && result11.oCNPJResponse.dadosCNPJ[0].ufMatriz != "")
                                        {
                                            if (result11.oCNPJResponse.dadosCNPJ[0].endereco.uf != result11.oCNPJResponse.dadosCNPJ[0].ufMatriz)
                                            {
                                                //Esta tabela so atualizao, nao apago porque pode ter registros de contador, etc
                                                sqlD = new StringBuilder();
                                                sqlD.AppendLine(" update ruc_prof  set");
                                                sqlD.AppendLine("         RPR_NUME = null , ");
                                                sqlD.AppendLine("         RPR_TTL_TIP_LOGRADORO = null,  ");
                                                sqlD.AppendLine("         RPR_DIRECCION = null,  ");
                                                sqlD.AppendLine("         RPR_URBANIZACION = null,  ");
                                                sqlD.AppendLine("         RPR_TES_COD_ESTADO = null,  ");
                                                sqlD.AppendLine("         RPR_ZONA_POSTAL = null,  ");
                                                sqlD.AppendLine("         RPR_TMU_COD_MUN = null,  ");
                                                sqlD.AppendLine("         RPR_FEC_CONST_NASC = null ");
                                                sqlD.AppendLine(" Where   rpr_rge_pra_protocolo = '" + pProtocolo + "'");
                                                sqlD.AppendLine(" and     rpr_cgc_cpf_secd in (select a.rrp_cgc_cpf_secd ");
                                                sqlD.AppendLine("                              from   ruc_relat_prof a ");
                                                sqlD.AppendLine("                              where  a.rrp_rge_pra_protocolo = '" + pProtocolo + "'");
                                                sqlD.AppendLine("                              and    a.rrp_tge_vtip_relac = 2)");
                                                cmdToExecute.CommandText = sqlD.ToString();
                                                cmdToExecute.CommandType = CommandType.Text;
                                                cmdToExecute.ExecuteNonQuery();

                                                sqlD = new StringBuilder();
                                                sqlD.AppendLine(" Delete ruc_relat_prof Where RRP_TGE_VTIP_RELAC = 2 and rrp_rge_pra_protocolo = '" + pProtocolo + "'");
                                                cmdToExecute.CommandText = sqlD.ToString();
                                                cmdToExecute.CommandType = CommandType.Text;
                                                cmdToExecute.ExecuteNonQuery();

                                                pCountQSA = 0;
                                            }
                                        }

                                        if (result11.oCNPJResponse.dadosCNPJ[0].indMatrizFilial == "2")
                                        {
                                            resultMatiz = regin.ServiceWs11(result11.oCNPJResponse.dadosCNPJ[0].cnpjMatriz);

                                            if (resultMatiz.status != "OK")
                                            {
                                                throw new Exception("Erro ao Buscar o CNPJ da matriz quando e filial erro: " + resultMatiz.descricao);
                                            }

                                            if (resultMatiz.oCNPJResponse.dadosCNPJ[0].capitalSocial != null)
                                                pValorCapitalMatriz = decimal.Parse(resultMatiz.oCNPJResponse.dadosCNPJ[0].capitalSocial) / 100;
                                        }
                                    }
                                    //Se entra qui e porque nao tem Natureza Juridica, entao vai atualizar com a da RFB mais abaixo
                                    if (pCountNatur > 0)
                                    {
                                        pCodNatureza = resultMatiz.oCNPJResponse.dadosCNPJ[0].naturezaJuridica.Trim();
                                    }
                                    //So grava QSA se nao tiver gravado, caso tenha nao busca da RFB
                                    if (pCountQSA == 0)
                                    {
                                        psc.Ruc.Tablelas.Helper.dHelper c = new psc.Ruc.Tablelas.Helper.dHelper();
                                        string ResponseRFB = GlobalV1.CreateXML(resultMatiz.oCNPJResponse);
                                        c.GravaCompletaRucQsaOracle(_conn, ResponseRFB, pProtocolo, DtTipoLogra);
                                    }
                                }

                                cmdToExecute.Parameters.Clear();

                                #endregion

                                #region BuscarDadoDbe


                                CompletaDadosDbeOracle(_conn, pServicoRFB, NroAtoOficio, CodConvenioAto, pDbe, pProtocolo, DtTipoLogra, pCNPJ, pNumeroOrgaoRegistroWs11, TIPOPERACAOPROT, IdMySql, IndBalcaoUnico);

                                // E temporario e so para a Junta PE, quando e alteração de eireli
                                if (NroAtoOficio.Trim().Length > 2)
                                {
                                    if (NroAtoOficio.Substring(0, 1) == "9")
                                    {
                                        if (ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString() == "10054583000197")
                                        {
                                            Helper chelperTemp = new Helper();
                                            //Evento REGIN atualizacao Siarco sem protocolo
                                            chelperTemp.gravaEvento_psc_prot_evento_rfb(_conn, "998", pProtocolo);
                                        }
                                    }
                                }



                                #endregion

                                #region Pega Datos Processo TVL etc novamente, porque a viabilidade pode ter sido gravado pelo s17
                                if (pvpv_cod_protocolo == "" && pServicoRFB == "S17")
                                {
                                    cmdToExecute.Parameters.Clear();
                                    sqlD = new StringBuilder();

                                    sqlD.AppendLine(" select	PRO_T0101_ID_RFB IdMySql, PRO_NR_REQUERIMENTO, PRO_TIP_OPERACAO, pro_tmu_cod_mun CodMunic, ");
                                    sqlD.AppendLine("           VPV_VIABILIDADE NroTVL, vpv_cod_protocolo ");
                                    sqlD.AppendLine(" from	    PSC_PROTOCOLO, VIA_PROTOCOLO_VIAB ");
                                    sqlD.AppendLine(" where	    1 = 1 ");
                                    sqlD.AppendLine(" and       pro_vpv_cod_protocolo = vpv_cod_protocolo(+) ");
                                    sqlD.AppendLine(" and       PRO_PROTOCOLO = '" + pProtocolo + "'");

                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;

                                    DataTable toReturnProv2 = new DataTable("DadosProcesso");
                                    OracleDataAdapter adapterPro = new OracleDataAdapter(cmdToExecute);
                                    adapterPro.Fill(toReturnProv2);

                                    TIPOPERACAOPROT = toReturnProv2.Rows[0]["PRO_TIP_OPERACAO"].ToString();
                                    pCodMunic = toReturnProv2.Rows[0]["CodMunic"].ToString();
                                    pNroTVL = toReturnProv2.Rows[0]["NroTVL"].ToString();
                                    pvpv_cod_protocolo = toReturnProv2.Rows[0]["vpv_cod_protocolo"].ToString();
                                    IdMySql = toReturnProv2.Rows[0]["IdMySql"].ToString();
                                }
                                #endregion

                                #region Buscar outros dados Viabilidade salvador BA
                                if ((pvpv_cod_protocolo != "" || pNroTVL != "") && pCodMunic == "38490")
                                {
                                    try
                                    {
                                        psc.WebServices.Global cws = new psc.WebServices.Global();
                                        string _pXmlViabilidade = "";
                                        if (_pXmlViabilidade == "" && pvpv_cod_protocolo != "")
                                        {
                                            _pXmlViabilidade = cws.getViabilidadeAll(pvpv_cod_protocolo, int.MinValue, pCodMunic, "", "");
                                        }

                                        //if (_pXmlViabilidade == "" && pNroTVL != "")
                                        //{
                                        //    _pXmlViabilidade = cws.getViabilidadeAll(pNroTVL, int.MinValue, pCodMunic, "", "");
                                        //}

                                        DataSet result = new DataSet();
                                        XmlTextReader reader = new XmlTextReader(new StringReader(_pXmlViabilidade));
                                        result.ReadXml(reader);

                                        string Resp = result.Tables["RESPOSTA"].Rows[0]["STATUS"].ToString();
                                        string ProtocoloSEDUR = "";

                                        if (result.Tables["RESPOSTA"].Rows[0]["PROTOCOLO"] != null && result.Tables["RESPOSTA"].Rows[0]["PROTOCOLO"].ToString() != "")
                                            ProtocoloSEDUR = result.Tables["RESPOSTA"].Rows[0]["PROTOCOLO"].ToString();

                                        if (Resp != "00" && Resp != "01" && Resp != "02")
                                        {
                                            throw new Exception("status Resposta do ws tvl invalido diferente de 00, 01, 02 ");
                                        }

                                        if (Resp == "00")
                                        {
                                            if (pNroTVL != "")
                                            {
                                                _pXmlViabilidade = cws.getViabilidadeAll(pNroTVL, int.MinValue, pCodMunic, "", "");

                                                DataSet result2 = new DataSet();
                                                XmlTextReader reader2 = new XmlTextReader(new StringReader(_pXmlViabilidade));
                                                result2.ReadXml(reader2);
                                                Resp = result2.Tables["RESPOSTA"].Rows[0]["STATUS"].ToString();

                                                if (result2.Tables["RESPOSTA"].Rows[0]["PROTOCOLO"] != null && result2.Tables["RESPOSTA"].Rows[0]["PROTOCOLO"].ToString() != "")
                                                    ProtocoloSEDUR = result2.Tables["RESPOSTA"].Rows[0]["PROTOCOLO"].ToString();

                                                if (Resp != "00" && Resp != "01" && Resp != "02")
                                                {
                                                    throw new Exception("status Resposta do ws tvl invalido diferente tvl de 00, 01, 02 ");
                                                }
                                            }
                                        }

                                        if (Resp == "01" || Resp == "02")
                                        {
                                            if (ProtocoloSEDUR != "")
                                            {
                                                sqlD = new StringBuilder();
                                                cmdToExecute.Parameters.Clear();
                                                sqlD.AppendLine(" update via_protocolo_viab ");
                                                sqlD.AppendLine(" set	 VPV_VIABILIDADE = :v_VPV_VIABILIDADE ");
                                                sqlD.AppendLine(" where	 VPV_COD_PROTOCOLO = :v_vde_cod_protocolo ");

                                                cmdToExecute.Parameters.Add(new OracleParameter("v_VPV_VIABILIDADE", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ProtocoloSEDUR));
                                                cmdToExecute.Parameters.Add(new OracleParameter("v_vde_cod_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pvpv_cod_protocolo));
                                                cmdToExecute.CommandText = sqlD.ToString();
                                                cmdToExecute.CommandType = CommandType.Text;
                                                cmdToExecute.ExecuteNonQuery();
                                            }
                                        }


                                        XmlDocument Xml = new XmlDocument();

                                        Xml.LoadXml(_pXmlViabilidade);

                                        XmlNode no = Xml;

                                        string DtVencTVL = (no.SelectSingleNode("ROOT/EMPRESA/DATA_VENCIMENTO") != null) ? no.SelectSingleNode("ROOT/EMPRESA/DATA_VENCIMENTO").InnerText : "";
                                        string tipoTVL = (no.SelectSingleNode("ROOT/EMPRESA/TIPOTVL") != null) ? no.SelectSingleNode("ROOT/EMPRESA/TIPOTVL").InnerText : "";
                                        string InscImobiliaria = (no.SelectSingleNode("ROOT/EMPRESA/IPTU") != null) ? no.SelectSingleNode("ROOT/EMPRESA/IPTU").InnerText : "";


                                        if (DtVencTVL != "" || tipoTVL != "" || InscImobiliaria != "")
                                        {
                                            cmdToExecute.Parameters.Clear();
                                            sqlD = new StringBuilder();

                                            sqlD.AppendLine(" update VIA_DADOS_EXTRAS ");
                                            sqlD.AppendLine(" set	 vde_tipo_tvl = :v_vde_tipo_tvl, ");
                                            sqlD.AppendLine("        vde_dt_vencimento_tvl = :v_vde_dt_vencimento_tvl, ");
                                            sqlD.AppendLine("        VDE_INCRI_IMOBILIARIA_TVL = :v_VDE_INCRI_IMOBILIARIA_TVL ");
                                            sqlD.AppendLine(" where	 vde_cod_protocolo = :v_vde_cod_protocolo ");

                                            cmdToExecute.Parameters.Add(new OracleParameter("v_vde_tipo_tvl", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, tipoTVL));
                                            cmdToExecute.Parameters.Add(new OracleParameter("v_vde_dt_vencimento_tvl", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DtVencTVL));
                                            cmdToExecute.Parameters.Add(new OracleParameter("v_vde_cod_protocolo", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pvpv_cod_protocolo));
                                            cmdToExecute.Parameters.Add(new OracleParameter("v_VDE_INCRI_IMOBILIARIA_TVL", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, InscImobiliaria));

                                            cmdToExecute.CommandText = sqlD.ToString();
                                            cmdToExecute.CommandType = CommandType.Text;
                                            if (cmdToExecute.ExecuteNonQuery() == 0)
                                            {
                                                sqlD = new StringBuilder();
                                                sqlD.AppendLine(" Insert into VIA_DADOS_EXTRAS (vde_tipo_tvl, vde_dt_vencimento_tvl, vde_cod_protocolo, VDE_INCRI_IMOBILIARIA_TVL) ");
                                                sqlD.AppendLine(" Values (:v_vde_tipo_tvl, :v_vde_dt_vencimento_tvl, :v_vde_cod_protocolo, :v_VDE_INCRI_IMOBILIARIA_TVL) ");
                                                cmdToExecute.CommandText = sqlD.ToString();
                                                cmdToExecute.CommandType = CommandType.Text;
                                                cmdToExecute.ExecuteNonQuery();

                                            }
                                        }




                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("erro ao buscar os dados da TVL processo: " + ex.Message);
                                    }
                                }
                                #endregion

                                #region Verifica se tem Evento o protocolo
                                cmdToExecute.Parameters.Clear();
                                sqlD = new StringBuilder();

                                //Busco se tem QSA gravado
                                sqlD.AppendLine(" select	count(*) ");
                                sqlD.AppendLine(" from	    psc_prot_evento_rfb  ");
                                sqlD.AppendLine(" where	    pev_pro_protocolo = '" + pProtocolo + "'");

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                object ppQtdEvento = cmdToExecute.ExecuteScalar();
                                int pQtdEvento = int.Parse(ppQtdEvento.ToString());
                                if (pQtdEvento == 0)
                                {
                                    throw new Exception("MS:95 - Protocolo sem Evento, por favor verificar");
                                }
                                #endregion

                                #region Outras Atualizaçoes TAB_INFORM_EXTRA_JUNTA, RUC_COMP, RUC_ESTAB

                                sqlD = new StringBuilder();

                                sqlD.AppendLine(" update TAB_INFORM_EXTRA_JUNTA ");
                                sqlD.AppendLine(" set	 TIE_FORMA_ATUACAO = :v_TIE_FORMA_ATUACAO, ");
                                sqlD.AppendLine("        TIE_FORMA_ATUACAO_OUTROS = :v_TIE_FORMA_ATUACAO_OUTROS, ");
                                sqlD.AppendLine("        TIE_CPF_CNPJ = :v_TIE_CPF_CNPJ, ");
                                sqlD.AppendLine(" 		 TIE_TIPO_UNIDADE = :v_TIE_TIPO_UNIDADE, ");
                                sqlD.AppendLine(" 		 TIE_TIPO_UNIDADE_OUTROS = :v_TIE_TIPO_UNIDADE_OUTROS ");
                                sqlD.AppendLine(" where	TIE_PROTOCOLO = :v_TIE_PROTOCOLO ");
                                sqlD.AppendLine(" and	TIE_TIPO_RELACAO = 4 ");

                                cmdToExecute.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                                cmdToExecute.Parameters.Add(new OracleParameter("v_TIE_FORMA_ATUACAO", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FormaAtuacao));
                                cmdToExecute.Parameters.Add(new OracleParameter("v_TIE_FORMA_ATUACAO_OUTROS", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FormaAtuacaoOutros));
                                cmdToExecute.Parameters.Add(new OracleParameter("v_TIE_CPF_CNPJ", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pCNPJ));
                                cmdToExecute.Parameters.Add(new OracleParameter("v_TIE_TIPO_UNIDADE", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, TipoDeUnidade));
                                cmdToExecute.Parameters.Add(new OracleParameter("v_TIE_TIPO_UNIDADE_OUTROS", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, TipoDeUnidadeOutros));


                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                if (cmdToExecute.ExecuteNonQuery() == 0)
                                {
                                    sqlD = new StringBuilder();
                                    sqlD.AppendLine(" Insert into TAB_INFORM_EXTRA_JUNTA (TIE_PROTOCOLO, TIE_TIPO_RELACAO, TIE_CPF_CNPJ, TIE_FORMA_ATUACAO, TIE_FORMA_ATUACAO_OUTROS, TIE_TIPO_UNIDADE, TIE_TIPO_UNIDADE_OUTROS) ");
                                    sqlD.AppendLine(" Values (:v_TIE_PROTOCOLO, 4, :v_TIE_CPF_CNPJ, :v_TIE_FORMA_ATUACAO, :v_TIE_FORMA_ATUACAO_OUTROS, :v_TIE_TIPO_UNIDADE, :v_TIE_TIPO_UNIDADE_OUTROS) ");
                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();

                                }


                                if (pCodNatureza != "" && decimal.Parse(pCodNatureza) > 0)
                                {
                                    sqlD = new StringBuilder();
                                    sqlD.AppendLine(" update RUC_COMP ");
                                    sqlD.AppendLine(" set	 RCO_TNC_COD_NATUR = " + pCodNatureza);
                                    sqlD.AppendLine(" where	 RCO_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                    sqlD.AppendLine(" And    Length(nvl(RCO_TNC_COD_NATUR,1)) < 4 ");

                                    cmdToExecute.Parameters.Clear();
                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }

                                if (pValorCapitalEmpresa > 0)
                                {
                                    sqlD = new StringBuilder();
                                    cmdToExecute.Parameters.Clear();

                                    sqlD.AppendLine(" update RUC_COMP ");
                                    sqlD.AppendLine(" set	 RCO_VAL_CAP_SOC = :v_RCO_VAL_CAP_SOC ");
                                    sqlD.AppendLine(" where	 RCO_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                    sqlD.AppendLine(" and	 nvl(RCO_VAL_CAP_SOC, 0) = 0");

                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RCO_VAL_CAP_SOC", OracleType.Number, 0, ParameterDirection.Input, true, 15, 2, "", DataRowVersion.Proposed, pValorCapitalEmpresa));

                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }

                                if (pValorCapitalMatriz > 0)
                                {
                                    sqlD = new StringBuilder();
                                    cmdToExecute.Parameters.Clear();

                                    sqlD.AppendLine(" update RUC_COMP ");
                                    sqlD.AppendLine(" set	 RCO_VAL_CAP_SOC_MATRIZ = :v_RCO_VAL_CAP_SOC_MATRIZ ");
                                    sqlD.AppendLine(" where	 RCO_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");

                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RCO_VAL_CAP_SOC_MATRIZ", OracleType.Number, 0, ParameterDirection.Input, true, 15, 2, "", DataRowVersion.Proposed, pValorCapitalMatriz));

                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }

                                /*
                                   Atualiza RUC_ESTAB
                                */
                                sqlD = new StringBuilder();
                                sqlD.AppendLine(" update RUC_ESTAB ");
                                sqlD.AppendLine(" set	RES_CNPJ_SEDE = '" + CNPJMatriz + "'");
                                sqlD.AppendLine(",      RES_TIP_ESTAB = " + pRESTIPESTAB);
                                sqlD.AppendLine(" where	RES_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");

                                cmdToExecute.Parameters.Clear();
                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();
                                #endregion

                                #region Valida Endereço Empresa
                                sqlD = new StringBuilder();
                                sqlD.AppendLine(@"select count(*) 
                                            from   ruc_estab 
                                            where (Res_NUME is null 
                                                       or    Res_TTL_TIP_LOGRADORO is null 
                                                       or    Res_DIRECCION is null 
                                                       or    Res_URBANIZACION is null  
                                                       or    Res_TES_COD_ESTADO is null   
                                                       or    Res_ZONA_POSTAL is null  
                                                       or		Length(nvl(Res_TMU_COD_MUN, '0')) < 2 
                                                      )");
                                sqlD.AppendLine(" And res_rge_pra_protocolo = '" + pProtocolo + "'");

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                object countEstb = cmdToExecute.ExecuteScalar();
                                int pcountEstb = int.Parse(countEstb.ToString());
                                if (pcountEstb > 0)
                                {
                                    psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                                    cc.Bairro = result11.oCNPJResponse.dadosCNPJ[0].endereco.bairro == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.bairro;
                                    cc.Cep = result11.oCNPJResponse.dadosCNPJ[0].endereco.cep == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.cep;
                                    cc.Codigo_municipio = result11.oCNPJResponse.dadosCNPJ[0].endereco.codMunicipio == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.codMunicipio;
                                    cc.Complemento = result11.oCNPJResponse.dadosCNPJ[0].endereco.complementoLogradouro == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.complementoLogradouro;
                                    cc.Logradouro = result11.oCNPJResponse.dadosCNPJ[0].endereco.logradouro == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.logradouro;
                                    cc.Numero = result11.oCNPJResponse.dadosCNPJ[0].endereco.numLogradouro == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.numLogradouro;
                                    cc.Pais = result11.oCNPJResponse.dadosCNPJ[0].endereco.codPais == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.codPais;
                                    cc.TipLogradoro = result11.oCNPJResponse.dadosCNPJ[0].endereco.codTipoLogradouro == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.codTipoLogradouro;
                                    cc.Uf = result11.oCNPJResponse.dadosCNPJ[0].endereco.uf == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.uf;


                                    cc.TrataEndereco(ref cc, DtTipoLogra);

                                    sqlD = new StringBuilder();
                                    sqlD.AppendLine(" update	ruc_estab ");
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
                                #endregion

                                #region Valida Endereço correspondencia ruc_comp
                                sqlD = new StringBuilder();
                                sqlD.AppendLine(@"select count(*) 
                                            from   ruc_comp 
                                            where (rco_NUME is null 
                                                       or    Rco_TTL_TIP_LOGRADORO is null 
                                                       or    Rco_DIRECCION is null 
                                                       or    Rco_URBANIZACION is null  
                                                       or    Rco_TES_COD_ESTADO is null   
                                                       or    Rco_ZONA_POSTAL is null  
                                                       or		Length(nvl(Rco_TMU_COD_MUN, '0')) < 2 
                                                      )");
                                sqlD.AppendLine(" And Rco_rge_pra_protocolo = '" + pProtocolo + "'");
                                cmdToExecute.Parameters.Clear();
                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                object countComp = cmdToExecute.ExecuteScalar();
                                int pcountComp = int.Parse(countComp.ToString());
                                if (pcountComp > 0)
                                {
                                    psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                                    cc.Bairro = result11.oCNPJResponse.dadosCNPJ[0].endereco.bairro == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.bairro;
                                    cc.Cep = result11.oCNPJResponse.dadosCNPJ[0].endereco.cep == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.cep;
                                    cc.Codigo_municipio = result11.oCNPJResponse.dadosCNPJ[0].endereco.codMunicipio == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.codMunicipio;
                                    cc.Complemento = result11.oCNPJResponse.dadosCNPJ[0].endereco.complementoLogradouro == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.complementoLogradouro;
                                    cc.Logradouro = result11.oCNPJResponse.dadosCNPJ[0].endereco.logradouro == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.logradouro;
                                    cc.Numero = result11.oCNPJResponse.dadosCNPJ[0].endereco.numLogradouro == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.numLogradouro;
                                    cc.Pais = result11.oCNPJResponse.dadosCNPJ[0].endereco.codPais == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.codPais;
                                    cc.TipLogradoro = result11.oCNPJResponse.dadosCNPJ[0].endereco.codTipoLogradouro == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.codTipoLogradouro;
                                    cc.Uf = result11.oCNPJResponse.dadosCNPJ[0].endereco.uf == null ? "" : result11.oCNPJResponse.dadosCNPJ[0].endereco.uf;

                                    cc.TrataEndereco(ref cc, DtTipoLogra);

                                    sqlD = new StringBuilder();
                                    sqlD.AppendLine(" update	ruc_comp ");
                                    sqlD.AppendLine(" set	  Rco_NUME = :v_RPR_NUME,");
                                    sqlD.AppendLine("         Rco_TTL_TIP_LOGRADORO = :v_RPR_TTL_TIP_LOGRADORO, ");
                                    sqlD.AppendLine("         Rco_DIRECCION = :v_RPR_DIRECCION, ");
                                    sqlD.AppendLine("         Rco_URBANIZACION = :v_RPR_URBANIZACION, ");
                                    sqlD.AppendLine("         Rco_IDENT_COMP = :v_RPR_IDENT_COMP, ");
                                    sqlD.AppendLine("         Rco_ZONA_POSTAL = :v_RPR_ZONA_POSTAL, ");
                                    sqlD.AppendLine("         Rco_TES_COD_ESTADO = :v_RPR_TES_COD_ESTADO, ");
                                    sqlD.AppendLine("         Rco_TMU_COD_MUN = :v_RPR_TMU_COD_MUN, ");
                                    sqlD.AppendLine("         Rco_TUS_COD_USR = 'RFBE'");

                                    sqlD.AppendLine(" where	Rco_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");


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
                                #endregion

                                #region Atualiza QSA
                                StringBuilder sSocio = new StringBuilder();
                                cmdToExecute.Parameters.Clear();
                                sSocio.AppendLine("select	distinct RRP_RGE_PRA_PROTOCOLO, RRP_CGC_CPF_SECD, ");
                                sSocio.AppendLine("         RPR_FEC_CONST_NASC, ");
                                sSocio.AppendLine("         RPR_NUME, ");
                                sSocio.AppendLine("         RPR_TTL_TIP_LOGRADORO, ");
                                sSocio.AppendLine("         RPR_DIRECCION, ");
                                sSocio.AppendLine("         RPR_URBANIZACION, ");
                                sSocio.AppendLine("         RPR_TES_COD_ESTADO, ");
                                sSocio.AppendLine("         RPR_ZONA_POSTAL, ");
                                sSocio.AppendLine("         RPR_TMU_COD_MUN, ");
                                sSocio.AppendLine("         RPR_FEC_CONST_NASC, ");
                                sSocio.AppendLine("         Length(Trim(RRP_CGC_CPF_SECD)) Tipo ");
                                sSocio.AppendLine("from	    ruc_prof, ruc_relat_prof ");
                                sSocio.AppendLine("where	RPR_RGE_PRA_PROTOCOLO = RRP_RGE_PRA_PROTOCOLO ");
                                sSocio.AppendLine("and		RRP_CGC_CPF_SECD = RPR_CGC_CPF_SECD ");
                                sSocio.AppendLine("and		RRP_TGE_VTIP_RELAC not in( 3) ");
                                sSocio.AppendLine("and		RRP_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                sSocio.AppendLine("and		(RPR_NUME is null ");
                                sSocio.AppendLine("             or		RPR_TTL_TIP_LOGRADORO is null ");
                                sSocio.AppendLine("             or		RPR_DIRECCION is null ");
                                sSocio.AppendLine("             or		RPR_URBANIZACION is null  ");
                                sSocio.AppendLine("             or		RPR_TES_COD_ESTADO is null   ");
                                sSocio.AppendLine("             or		RPR_ZONA_POSTAL is null  ");
                                sSocio.AppendLine("             or		Length(nvl(RPR_TMU_COD_MUN, '0')) < 2 ");
                                sSocio.AppendLine("             or		RPR_FEC_CONST_NASC  is null ");
                                sSocio.AppendLine("         ) ");

                                cmdToExecute.CommandText = sSocio.ToString();
                                cmdToExecute.CommandType = CommandType.Text;

                                DataTable toReturn = new DataTable("QSA");
                                OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute);
                                adapter.Fill(toReturn);

                                for (int a = 0; a <= toReturn.Rows.Count - 1; a++)
                                {
                                    try
                                    {
                                        WsServicesReginRFB.Retorno ws0911Socio = new WsServicesReginRFB.Retorno();
                                        DateTime FecNacimento = new DateTime();
                                        string pRPR_NUME = "";
                                        string pRPR_TTL_TIP_LOGRADORO = "";
                                        string pRPR_DIRECCION = "";
                                        string pRPR_URBANIZACION = "";
                                        string pRPR_TES_COD_ESTADO = "";
                                        string pRPR_ZONA_POSTAL = "";
                                        string pRPR_TMU_COD_MUN = "";
                                        string pRPR_IDENT_COMP = "";
                                        string pRPR_TGE_VPAIS = "";
                                        FecNacimento = DateTime.MinValue;
                                        string CpfCnpj = toReturn.Rows[a]["RRP_CGC_CPF_SECD"].ToString().Trim();

                                        if (toReturn.Rows[a]["Tipo"].ToString() != "11" && toReturn.Rows[a]["Tipo"].ToString() != "14")
                                        {
                                            throw new Exception("Erro:" + " cpf ou cnpj errado " + toReturn.Rows[a]["RRP_CGC_CPF_SECD"].ToString());
                                        }

                                        if (toReturn.Rows[a]["Tipo"].ToString() == "11")
                                        {
                                            #region CPF

                                            regin = new WsServicesReginRFB.ServiceReginRFB();
                                            regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                                            ws0911Socio = regin.ServiceWs09(CpfCnpj);

                                            if (ws0911Socio.status == "OK")
                                            {
                                                if (toReturn.Rows[a]["RPR_FEC_CONST_NASC"].ToString() == "")
                                                {
                                                    if (ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].dataNascimento != "")
                                                    {
                                                        string data = ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].dataNascimento;
                                                        FecNacimento = DateTime.ParseExact(data, "yyyyMMdd", null);
                                                    }
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

                                                    pRPR_DIRECCION = cc.Logradouro;
                                                    pRPR_NUME = cc.Numero;
                                                    pRPR_TGE_VPAIS = cc.Pais;
                                                    pRPR_TTL_TIP_LOGRADORO = cc.TipLogradoro;
                                                    pRPR_URBANIZACION = cc.Bairro;
                                                    pRPR_TES_COD_ESTADO = cc.Uf;
                                                    pRPR_ZONA_POSTAL = cc.Cep;
                                                    pRPR_IDENT_COMP = cc.Complemento;
                                                    pRPR_TMU_COD_MUN = cc.Codigo_municipio;
                                                }


                                            }
                                            else
                                            {
                                                throw new Exception("Erro:" + ws0911Socio.descricao + " ao tentar buscar o cpf " + toReturn.Rows[a]["RRP_CGC_CPF_SECD"].ToString());
                                            }
                                            #endregion
                                        }
                                        if (toReturn.Rows[a]["Tipo"].ToString() == "14")
                                        {
                                            #region CNPJ
                                            regin = new WsServicesReginRFB.ServiceReginRFB();
                                            regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                                            ws0911Socio = regin.ServiceWs11(CpfCnpj);

                                            if (ws0911Socio.status == "OK")
                                            {
                                                if (toReturn.Rows[a]["RPR_FEC_CONST_NASC"].ToString() == "")
                                                {
                                                    if (ws0911Socio.oCNPJResponse.dadosCNPJ[0].dataAberturaEmpresa != "")
                                                    {
                                                        string data = ws0911Socio.oCNPJResponse.dadosCNPJ[0].dataAberturaEmpresa;
                                                        FecNacimento = DateTime.ParseExact(data, "yyyyMMdd", null);
                                                    }
                                                }


                                                if (ws0911Socio.oCNPJResponse.dadosCNPJ[0] != null &&
                                                    ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco != null)
                                                {
                                                    psc.Ruc.Tablelas.Helper.Endereco cc = new psc.Ruc.Tablelas.Helper.Endereco();
                                                    cc.Bairro = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.bairro == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.bairro;
                                                    cc.Cep = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.cep == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.cep;
                                                    cc.Codigo_municipio = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codMunicipio == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codMunicipio;
                                                    cc.Complemento = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.complementoLogradouro == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.complementoLogradouro;
                                                    cc.Logradouro = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.logradouro == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.logradouro;
                                                    cc.Numero = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.numLogradouro == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.numLogradouro;
                                                    cc.Pais = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codPais == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codPais;
                                                    cc.TipLogradoro = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codTipoLogradouro == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.codTipoLogradouro;
                                                    cc.Uf = ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.uf == null ? "" : ws0911Socio.oCNPJResponse.dadosCNPJ[0].endereco.uf;

                                                    cc.TrataEndereco(ref cc, DtTipoLogra);

                                                    pRPR_DIRECCION = cc.Logradouro;
                                                    pRPR_NUME = cc.Numero;
                                                    pRPR_TGE_VPAIS = cc.Pais;
                                                    pRPR_TTL_TIP_LOGRADORO = cc.TipLogradoro;
                                                    pRPR_URBANIZACION = cc.Bairro;
                                                    pRPR_TES_COD_ESTADO = cc.Uf;
                                                    pRPR_ZONA_POSTAL = cc.Cep;
                                                    pRPR_IDENT_COMP = cc.Complemento;
                                                    pRPR_TMU_COD_MUN = cc.Codigo_municipio;

                                                }


                                            }
                                            else
                                            {
                                                throw new Exception("Erro:" + ws0911Socio.descricao + " ao tentar buscar o cpf " + toReturn.Rows[a]["RRP_CGC_CPF_SECD"].ToString());
                                            }
                                            #endregion
                                        }

                                        if (FecNacimento != DateTime.MinValue)
                                        {
                                            sSocio = new StringBuilder();
                                            sSocio.AppendLine(" update	ruc_prof ");
                                            sSocio.AppendLine(" set		RPR_FEC_CONST_NASC = :v_FecNacimento");
                                            sSocio.AppendLine(" where	RPR_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                            sSocio.AppendLine(" and		RPR_CGC_CPF_SECD = '" + toReturn.Rows[a]["RRP_CGC_CPF_SECD"].ToString() + "'");

                                            cmdToExecute.Parameters.Clear();
                                            cmdToExecute.Parameters.Add(new OracleParameter("v_FecNacimento", OracleType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, FecNacimento));


                                            cmdToExecute.CommandText = sSocio.ToString();
                                            cmdToExecute.CommandType = CommandType.Text;
                                            cmdToExecute.ExecuteNonQuery();


                                        }

                                        if (pRPR_DIRECCION != "" || pRPR_TMU_COD_MUN != "")
                                        {
                                            if (toReturn.Rows[a]["RPR_NUME"].ToString() == "" ||
                                                toReturn.Rows[a]["RPR_TTL_TIP_LOGRADORO"].ToString() == "" ||
                                                toReturn.Rows[a]["RPR_DIRECCION"].ToString() == "" ||
                                                toReturn.Rows[a]["RPR_URBANIZACION"].ToString() == "" ||
                                                //toReturn.Rows[a]["RPR_TES_COD_ESTADO"].ToString() == "" ||
                                                toReturn.Rows[a]["RPR_ZONA_POSTAL"].ToString() == "" ||
                                                toReturn.Rows[a]["RPR_TMU_COD_MUN"].ToString() == "")
                                            {
                                                sSocio = new StringBuilder();
                                                sSocio.AppendLine(" update	ruc_prof ");
                                                sSocio.AppendLine(" set		RPR_ORIGEM_ENDERECO = 2, "); //Origem RFB
                                                sSocio.AppendLine("         RPR_NUME = :v_RPR_NUME,");
                                                sSocio.AppendLine("         RPR_TTL_TIP_LOGRADORO = :v_RPR_TTL_TIP_LOGRADORO, ");
                                                sSocio.AppendLine("         RPR_DIRECCION = :v_RPR_DIRECCION, ");
                                                sSocio.AppendLine("         RPR_URBANIZACION = :v_RPR_URBANIZACION, ");
                                                sSocio.AppendLine("         RPR_IDENT_COMP = :v_RPR_IDENT_COMP, ");
                                                sSocio.AppendLine("         RPR_ZONA_POSTAL = :v_RPR_ZONA_POSTAL, ");
                                                sSocio.AppendLine("         RPR_TES_COD_ESTADO = :v_RPR_TES_COD_ESTADO, ");
                                                sSocio.AppendLine("         RPR_TMU_COD_MUN = :v_RPR_TMU_COD_MUN ");
                                                if (pRPR_TGE_VPAIS != "")
                                                {
                                                    sSocio.AppendLine(",         RPR_TGE_VPAIS = :v_RPR_TGE_VPAIS ");
                                                }
                                                sSocio.AppendLine(" where	RPR_RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                                sSocio.AppendLine(" and		RPR_CGC_CPF_SECD = '" + toReturn.Rows[a]["RRP_CGC_CPF_SECD"].ToString() + "'");

                                                if (pRPR_TGE_VPAIS == "" ||
                                                    pRPR_TGE_VPAIS.Length < 2 ||
                                                    pRPR_TGE_VPAIS == "000" ||
                                                    pRPR_TGE_VPAIS == "0" ||
                                                    pRPR_TGE_VPAIS == "00")
                                                {
                                                    if (pRPR_TES_COD_ESTADO != "EX")
                                                        pRPR_TGE_VPAIS = "105";
                                                }

                                                cmdToExecute.Parameters.Clear();
                                                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_NUME", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_NUME));
                                                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_TTL_TIP_LOGRADORO", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TTL_TIP_LOGRADORO));
                                                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_DIRECCION", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_DIRECCION));
                                                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_URBANIZACION", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_URBANIZACION));
                                                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_IDENT_COMP", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_IDENT_COMP));

                                                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_ZONA_POSTAL", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_ZONA_POSTAL));
                                                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_TES_COD_ESTADO", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TES_COD_ESTADO));
                                                cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_TMU_COD_MUN", OracleType.Number, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TMU_COD_MUN));
                                                if (pRPR_TGE_VPAIS != "")
                                                {
                                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_TGE_VPAIS", OracleType.Number, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TGE_VPAIS == "" ? null : pRPR_TGE_VPAIS));
                                                }

                                                cmdToExecute.CommandText = sSocio.ToString();
                                                cmdToExecute.CommandType = CommandType.Text;
                                                cmdToExecute.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("erro ao completar QSA:" + ex.Message + " StackTrace " + ex.StackTrace);
                                    }
                                }
                                #endregion

                                #region insert evento de sefaz 625, 626 de requerimento
                                try
                                {
                                    sqlD = new StringBuilder();
                                    cmdToExecute.Parameters.Clear();
                                    sqlD.AppendLine(" delete ruc_empresas_vinculadas ");
                                    sqlD.AppendLine(" where rev_protocolo = :v_TIE_PROTOCOLO ");
                                    sqlD.AppendLine(" and   rev_tipo = 2 ");

                                    cmdToExecute.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();

                                    sqlD = new StringBuilder();
                                    cmdToExecute.Parameters.Clear();
                                    sqlD.AppendLine(" insert into ruc_empresas_vinculadas(rev_protocolo, rev_tipo, rev_cnpj_vinculada) ");
                                    sqlD.AppendLine(" select a.rev_protocolo, a.rev_tipo, a.rev_cnpj_vinculada  ");
                                    sqlD.AppendLine(" from PRO_EMP_VINCULADAS a ");
                                    sqlD.AppendLine(" where a.rev_protocolo = :v_TIE_PROTOCOLO ");

                                    cmdToExecute.Parameters.Add(new OracleParameter("v_TIE_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }
                                catch
                                {
                                    // fiz isto porquue essa tabela PRO_EMP_VINCULADAS, possiblemente nao tenha em todos os bancos de dados
                                }
                                #endregion

                                #region Verifica dados Ruc_general
                                //atualiza data inicio de atividade
                                cmdToExecute.Parameters.Clear();
                                sqlD = new StringBuilder();

                                //Busco se tem QSA gravado
                                sqlD.AppendLine(" select	count(*) ");
                                sqlD.AppendLine(" from	    RUC_GENERAL  ");
                                sqlD.AppendLine(" where	    RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                sqlD.AppendLine(" and       RGE_FEC_INI_ACT_EC is null ");

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                object pcountAec = cmdToExecute.ExecuteScalar();
                                int ppcountAec = int.Parse(pcountAec.ToString());
                                if (ppcountAec > 0)
                                {
                                    sqlD = new StringBuilder();
                                    sqlD.AppendLine(" update RUC_GENERAL ");
                                    sqlD.AppendLine(" set	RGE_FEC_INI_ACT_EC = :v_RGE_FEC_INI_ACT_EC");
                                    sqlD.AppendLine(" where	RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");

                                    cmdToExecute.Parameters.Clear();
                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RGE_FEC_INI_ACT_EC", OracleType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, rge_fec_ini_act_ec));

                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }



                                /*mATIS 6774
                                Conforme reunião realizada em 08 / 09 / 2020 com Xico, Thamires e Tiago, utilizando-se de exemplo casos do evento 109 da RFB -Patrimônio de afetação CNPJ: 04.916.201 / 0008 - 60 em que a abertura é feita apenas da RFB, ficou discutido que:
                                o upate RGE_NOMB
                                1.A aplicação de evento 801 vai fazer a busca de dados na Junta Comercial.
                                2.Quando não encontrados os dados, a mesma vai fazer a busca na RFB.
                                3.Quando a matriz for de fora do Estado a aplicação do 801, vai fazer a busca dos dados como Razão Social e QSA da RFB.
                                */


                                //Vejo o porte da RFB
                                if (RGE_TGE_VTAMANHO == "01")
                                    RGE_TGE_VTAMANHO = "1";

                                if (RGE_TGE_VTAMANHO == "03")
                                    RGE_TGE_VTAMANHO = "2";

                                if (RGE_TGE_VTAMANHO == "05")
                                    RGE_TGE_VTAMANHO = "3";

                                if (RGE_TGE_VTAMANHO != "1" && RGE_TGE_VTAMANHO != "2" && RGE_TGE_VTAMANHO != "3")
                                    RGE_TGE_VTAMANHO = "3";


                                sqlD = new StringBuilder();
                                cmdToExecute.Parameters.Clear();
                                sqlD.AppendLine(" update RUC_GENERAL ");
                                sqlD.AppendLine(" set	RGE_OPT_SIMP = '" + RGE_OPT_SIMP + "'");
                                sqlD.AppendLine(" 	   , RGE_TGE_VTAMANHO = " + RGE_TGE_VTAMANHO);

                                if (pSituacaoRFB != "")
                                    sqlD.AppendLine(" 	    , RGE_SITUACAO_RFB = '" + pSituacaoRFB + "'");

                                if (pFilialDeOutroEstado == "SIM" && result11.oCNPJResponse.dadosCNPJ[0].nomeEmpresarial != null
                                    && result11.oCNPJResponse.dadosCNPJ[0].nomeEmpresarial != "")
                                {
                                    sqlD.AppendLine("     , RGE_NOMB = :v_RGE_NOMB ");
                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RGE_NOMB", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, result11.oCNPJResponse.dadosCNPJ[0].nomeEmpresarial));
                                }
                                sqlD.AppendLine("     , RGE_CGC_CPF = '" + pCNPJ + "'");
                                sqlD.AppendLine(" where	RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");

                                cmdToExecute.CommandText = sqlD.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();


                                if (RGE_OPT_SIMEI == "S" && pCodNatureza == "2135")
                                {
                                    //Marcar quando e MEI
                                    sqlD = new StringBuilder();
                                    sqlD.AppendLine(" update RUC_GENERAL ");
                                    sqlD.AppendLine(" set	RGE_TGE_VTIP_REG = 13");
                                    sqlD.AppendLine(" where	RGE_PRA_PROTOCOLO = '" + pProtocolo + "'");

                                    cmdToExecute.Parameters.Clear();
                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();

                                }
                                #endregion

                                #region Objeto Social da empresa
                                string pObjetoSocial = "";
                                if (result11.oCNPJResponse.dadosCNPJ[0].objetoSocial != null && result11.oCNPJResponse.dadosCNPJ[0].objetoSocial != "")
                                {
                                    pObjetoSocial = result11.oCNPJResponse.dadosCNPJ[0].objetoSocial;
                                }
                                if (pObjetoSocial != "")
                                {
                                    cmdToExecute.Parameters.Clear();
                                    sqlD = new StringBuilder();

                                    //Busco se tem QSA gravado
                                    sqlD.AppendLine(" select	count(*) ");
                                    sqlD.AppendLine(" from	    ruc_gen_protocolo  ");
                                    sqlD.AppendLine(" where	    rgp_rge_pra_protocolo = '" + pProtocolo + "'");
                                    sqlD.AppendLine(" and       rgp_tge_cod_tip_tab = 1 ");

                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    object pcountObjeto = cmdToExecute.ExecuteScalar();
                                    int ppcountObjeto = int.Parse(pcountAec.ToString());
                                    if (ppcountObjeto == 0)
                                    {
                                        DateTime DtHoje = DateTime.Now;//dHelper.SysdateOracle();
                                        using (psc.Ruc.Tablelas.Ruc.Ruc_Gen_Protocolo gc = new psc.Ruc.Tablelas.Ruc.Ruc_Gen_Protocolo())
                                        {
                                            gc.rgp_rge_pra_protocolo = pProtocolo;
                                            gc.rgp_tge_tip_tab = 902;
                                            gc.rgp_tge_cod_tip_tab = 1;
                                            gc.rgp_valor = pObjetoSocial;
                                            gc.rgp_tus_cod_usr = "REGIN";
                                            gc.rgp_fec_actl = DtHoje;
                                            if (gc.rgp_valor != "")
                                            {
                                                gc.Update(_conn);
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Carrega Representante da empresa perante ao cnpj
                                psc.Ruc.Tablelas.Helper.dHelper reprs = new psc.Ruc.Tablelas.Helper.dHelper();
                                reprs.GravaRepresentanteDoCNPJRFNB(_conn, ResponseRFBEmpresa, pProtocolo, DtTipoLogra);

                                #endregion

                                #region Atualiza Representante RFB
                                Helper he = new Helper();
                                he.AtualizaRepresentantedoQSARFB(_conn, ResponseRFBEmpresa, pProtocolo, DtTipoLogra, "");
                                #endregion

                                #region atualiza Preposto ws11
                                Helper dheHq = new Helper();
                                dheHq.gravarPrepostoWs(_conn, ResponseRFBEmpresa, pProtocolo, DtTipoLogra, pCNPJ, "S11");
                                #endregion

                                #region Atualiza os municipios das filiais 
                                try
                                {
                                    Helper hef = new Helper();
                                    hef.AtualizaMunicipiosdasFiliais(_conn, ResponseRFBEmpresa, pProtocolo, "", pUfOrgaoDeRegistro);
                                }
                                catch
                                {
                                    //Isto aqui e porque deve ter lugares que nao tem a tabela, entao e para nao da erro
                                }
                                #endregion

                                #region Atualiza Qualificação do QSA Igual a RFB
                                //Quando nao vem do Requerimento RCPJ
                                if (pOrigemDado != "2")
                                {
                                    Helper hee = new Helper();
                                    hee.AtualizaQualificacaoQSAdaRFB(_conn, ResponseRFBEmpresa, pProtocolo, "");
                                }
                                #endregion

                                #region Atualiza Endereço e nome do representante caso falte

                                StringBuilder sRepre = new StringBuilder();
                                sRepre.AppendLine(" select	RSR_CGC_CPF_SECD, RSR_CGC_CPF_PRINC, RSR_NOMB, RSR_TTL_TIP_LOGRADORO, RSR_DIRECCION,  RSR_NUME, RSR_URBANIZACION, RSR_TES_COD_ESTADO, RSR_ZONA_POSTAL");
                                sRepre.AppendLine(" from	ruc_representantes ");
                                sRepre.AppendLine(" where	1 = 1 ");
                                sRepre.AppendLine(" and     RSR_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                sRepre.AppendLine(" and		(RSR_TTL_TIP_LOGRADORO is null ");
                                sRepre.AppendLine(" 		or		RSR_DIRECCION is null ");
                                sRepre.AppendLine(" 		or		RSR_NUME is null ");
                                sRepre.AppendLine(" 		or		RSR_URBANIZACION is null ");

                                sRepre.AppendLine(" 		or		Length(nvl(RSR_TMU_COD_MUN, '0')) < 2  ");
                                sRepre.AppendLine(" 		or		RSR_TES_COD_ESTADO is null ");
                                sRepre.AppendLine(" 		or		RSR_ZONA_POSTAL is null ");
                                sRepre.AppendLine(" 		or      RSR_NOMB is null ");
                                sRepre.AppendLine(" 		) ");

                                cmdToExecute.Parameters.Clear();
                                cmdToExecute.CommandText = sRepre.ToString();
                                cmdToExecute.CommandType = CommandType.Text;

                                toReturn = new DataTable("Representante");
                                adapter = new OracleDataAdapter(cmdToExecute);
                                adapter.Fill(toReturn);

                                for (int a = 0; a <= toReturn.Rows.Count - 1; a++)
                                {
                                    try
                                    {
                                        WsServicesReginRFB.Retorno ws0911Socio = new WsServicesReginRFB.Retorno();
                                        string pRPR_NUME = "";
                                        string pRPR_TTL_TIP_LOGRADORO = "";
                                        string pRPR_DIRECCION = "";
                                        string pRPR_URBANIZACION = "";
                                        string pRPR_TES_COD_ESTADO = "";
                                        string pRPR_ZONA_POSTAL = "";
                                        string pRPR_TMU_COD_MUN = "";
                                        string pRPR_IDENT_COMP = "";
                                        string pRPR_TGE_VPAIS = "";
                                        string CpfCnpj = toReturn.Rows[a]["RSR_CGC_CPF_SECD"].ToString().Trim();
                                        if (CpfCnpj.Length == 11)
                                        {
                                            #region CPF

                                            regin = new WsServicesReginRFB.ServiceReginRFB();
                                            regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                                            ws0911Socio = regin.ServiceWs09(CpfCnpj);

                                            if (ws0911Socio.status == "OK")
                                            {
                                                #region Atualiza o nome do representante
                                                //Atualiza o Nome do representante, caso falte
                                                if (ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0] != null &&
                                                    ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].nome != null
                                                    && toReturn.Rows[a]["RSR_NOMB"].ToString() == "")
                                                {
                                                    sSocio = new StringBuilder();
                                                    sSocio.AppendLine(" update	ruc_representantes ");
                                                    sSocio.AppendLine(" set		");
                                                    sSocio.AppendLine("         RSR_NOMB = :v_RSR_NOMB");
                                                    sSocio.AppendLine(" where	RSR_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                                    sSocio.AppendLine(" and		RSR_CGC_CPF_SECD = '" + toReturn.Rows[a]["RSR_CGC_CPF_SECD"].ToString() + "'");
                                                    sSocio.AppendLine(" and     RSR_CGC_CPF_PRINC = '" + toReturn.Rows[a]["RSR_CGC_CPF_PRINC"].ToString() + "'");


                                                    cmdToExecute.Parameters.Clear();
                                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RSR_NOMB", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].nome));

                                                    cmdToExecute.CommandText = sSocio.ToString();
                                                    cmdToExecute.CommandType = CommandType.Text;
                                                    cmdToExecute.ExecuteNonQuery();
                                                }
                                                #endregion

                                                #region Atualiza o endereço do representante
                                                //Atualiza O endereço do representante
                                                if (ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0] != null &&
                                                    ws0911Socio.oCPFResponse.retornoWS09Redesim.dadosCPF[0].endereco != null &&
                                                    (toReturn.Rows[a]["RSR_TTL_TIP_LOGRADORO"].ToString() == "" ||
                                                    toReturn.Rows[a]["RSR_DIRECCION"].ToString() == "" ||
                                                    toReturn.Rows[a]["RSR_NUME"].ToString() == "" ||
                                                    toReturn.Rows[a]["RSR_URBANIZACION"].ToString() == "" ||
                                                    toReturn.Rows[a]["RSR_ZONA_POSTAL"].ToString() == "" ||
                                                    toReturn.Rows[a]["RSR_TES_COD_ESTADO"].ToString() == "")
                                                    )
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

                                                    pRPR_DIRECCION = cc.Logradouro;
                                                    pRPR_NUME = cc.Numero;
                                                    pRPR_TGE_VPAIS = cc.Pais;
                                                    pRPR_TTL_TIP_LOGRADORO = cc.TipLogradoro;
                                                    pRPR_URBANIZACION = cc.Bairro;
                                                    pRPR_TES_COD_ESTADO = cc.Uf;
                                                    pRPR_ZONA_POSTAL = cc.Cep;
                                                    pRPR_IDENT_COMP = cc.Complemento;
                                                    pRPR_TMU_COD_MUN = cc.Codigo_municipio;

                                                    sSocio = new StringBuilder();
                                                    sSocio.AppendLine(" update	ruc_representantes ");
                                                    sSocio.AppendLine(" set		"); //Origem RFB
                                                    sSocio.AppendLine("         RSR_NUME = :v_RPR_NUME,");
                                                    sSocio.AppendLine("         RSR_TTL_TIP_LOGRADORO = :v_RPR_TTL_TIP_LOGRADORO, ");
                                                    sSocio.AppendLine("         RSR_DIRECCION = :v_RPR_DIRECCION, ");
                                                    sSocio.AppendLine("         RSR_URBANIZACION = :v_RPR_URBANIZACION, ");
                                                    sSocio.AppendLine("         RSR_ZONA_POSTAL = :v_RPR_ZONA_POSTAL, ");
                                                    sSocio.AppendLine("         RSR_TES_COD_ESTADO = :v_RPR_TES_COD_ESTADO, ");
                                                    sSocio.AppendLine("         RSR_TMU_COD_MUN = :v_RPR_TMU_COD_MUN, ");
                                                    sSocio.AppendLine("         RSR_TGE_VPAIS = :v_RPR_TGE_VPAIS,  ");
                                                    sSocio.AppendLine("         RSR_IDENT_COMP = :v_RSR_IDENT_COMP ");
                                                    sSocio.AppendLine(" where	RSR_PRA_PROTOCOLO = '" + pProtocolo + "'");
                                                    sSocio.AppendLine(" and		RSR_CGC_CPF_SECD = '" + toReturn.Rows[a]["RSR_CGC_CPF_SECD"].ToString() + "'");
                                                    sSocio.AppendLine(" and     RSR_CGC_CPF_PRINC = '" + toReturn.Rows[a]["RSR_CGC_CPF_PRINC"].ToString() + "'");


                                                    cmdToExecute.Parameters.Clear();
                                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_NUME", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_NUME));
                                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_TTL_TIP_LOGRADORO", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TTL_TIP_LOGRADORO));
                                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_DIRECCION", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_DIRECCION));
                                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_URBANIZACION", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_URBANIZACION));
                                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RSR_IDENT_COMP", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_IDENT_COMP));
                                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_ZONA_POSTAL", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_ZONA_POSTAL));
                                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_TES_COD_ESTADO", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TES_COD_ESTADO));
                                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_TMU_COD_MUN", OracleType.Number, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TMU_COD_MUN));
                                                    cmdToExecute.Parameters.Add(new OracleParameter("v_RPR_TGE_VPAIS", OracleType.Number, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pRPR_TGE_VPAIS == "" ? null : pRPR_TGE_VPAIS));

                                                    cmdToExecute.CommandText = sSocio.ToString();
                                                    cmdToExecute.CommandType = CommandType.Text;
                                                    cmdToExecute.ExecuteNonQuery();
                                                }
                                                #endregion


                                            }
                                            else
                                            {
                                                throw new Exception("Erro:" + ws0911Socio.descricao + " ao tentar buscar o cpf " + CpfCnpj);
                                            }
                                            #endregion
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("erro ao completar Endereço Representante QSA:" + ex.Message + " StackTrace " + ex.StackTrace);
                                    }

                                }
                                #endregion

                                #region Atualiza endereço da unidade dependente no caso de salvador BA, isso foi definição feita 23/09/2021
                                if (1 == 1 && pUnidadeDependente == "1" && pCodMunic == "38490")
                                {
                                    if (pvpv_cod_protocolo == "")
                                    {
                                        throw new Exception("Viabilidae de Dependente Vazia no registro de legalização");
                                    }

                                    sqlD = new StringBuilder();
                                    sqlD.AppendLine(" Select    Count(*) from via_protocolo_viab ");
                                    sqlD.AppendLine(" where	    vpv_cod_protocolo = '" + pvpv_cod_protocolo + "'");

                                    cmdToExecute.Parameters.Clear();
                                    cmdToExecute.CommandText = sqlD.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    pCount = int.Parse(cmdToExecute.ExecuteScalar().ToString());
                                    if (pCount == 0)
                                    {
                                        throw new Exception("Viabilidae de Dependente não eocntrotada para subtituir o endereço na legalização");
                                    }

                                    //Quando e unidade dependente de Salvador
                                    //Se atualiza o endereço deferido na viabilidade para a legalização
                                    sqlU.AppendLine(@"update ruc_estab e
                                                    set(e.res_nume,
                                                        e.RES_IDENT_COMP,
                                                        e.RES_REFER,
                                                        e.RES_TTL_TIP_LOGRADORO,
                                                        e.RES_DIRECCION,
                                                        e.RES_URBANIZACION,
                                                        e.RES_TES_COD_ESTADO,
                                                        e.RES_ZONA_POSTAL,
                                                        e.RES_TMU_COD_MUN
                                                        ) = (select v.vpv_num_logradouro,
                                                                    v.vpv_comp_logradouro,
                                                                    v.vpv_refer,
                                                                    v.vpv_ttl_tip_logradoro,
                                                                    v.vpv_logradoro,
                                                                    v.vpv_bairro,
                                                                    v.vpv_tmu_tuf_uf,
                                                                    v.vpv_cep,
                                                                    v.vpv_tmu_cod_mun
                                                             from via_protocolo_viab v
                                                             where v.vpv_cod_protocolo = :v_vpv_cod_protocolo
                                                            )
                                                    where e.res_rge_pra_protocolo = :v_res_rge_pra_protocolo");
                                    cmdToExecute.Parameters.Clear();
                                    cmdToExecute.CommandText = sqlU.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.Parameters.Add(new OracleParameter("v_res_rge_pra_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));
                                    cmdToExecute.Parameters.Add(new OracleParameter("v_vpv_cod_protocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pvpv_cod_protocolo));
                                    cmdToExecute.ExecuteNonQuery();
                                }
                                #endregion

                                sqlU = new StringBuilder();
                                sqlU.AppendLine(" update MAC_LOG_CARGA_JUNTA_HOMOLOG set mlc_data_carrega_envio = null, mlc_data_valida_xml = null, MLC_DATA_CARREGA_WS11 = sysdate where MLC_PROTOCOLO = '" + pProtocolo + "'");
                                cmdToExecute.Parameters.Clear();
                                cmdToExecute.CommandText = sqlU.ToString();
                                cmdToExecute.CommandType = CommandType.Text;
                                cmdToExecute.ExecuteNonQuery();


                                bool atualizaDtParNAOValidarXML = false;

                                //Isto aqui e para quando a junta nao valida XML, atualizamos a data do xml para nao entrar no cursor de validação
                                if (ConfigurationManager.AppSettings["UrlValidaXml"] == null || ConfigurationManager.AppSettings["UrlValidaXml"].ToString() == "")
                                {
                                    atualizaDtParNAOValidarXML = true;
                                }

                                //Isto para atualizar se sao serviços da RFB, para na validar o XML ja que vai buscar tudo da RFB
                                if (pServicoRFB == "S17" || pServicoRFB == "S08" || pServicoRFB == "S15")
                                {
                                    atualizaDtParNAOValidarXML = true;
                                }

                                if (atualizaDtParNAOValidarXML)
                                {
                                    sqlU = new StringBuilder();
                                    sqlU.AppendLine(" update MAC_LOG_CARGA_JUNTA_HOMOLOG set mlc_data_carrega_envio = null, mlc_data_valida_xml = sysdate where MLC_PROTOCOLO = '" + pProtocolo + "'");
                                    cmdToExecute.Parameters.Clear();
                                    cmdToExecute.CommandText = sqlU.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }

                            }
                            //_conn.Rollback();
                            _conn.Commit();
                        }
                    }
                }
                else
                {
                    atualizaProtocoloNOK(pProtocolo, result11.descricao);
                }
            }
            catch (Exception ex)
            {
                atualizaProtocoloNOK(pProtocolo, ex.Message + " StackTrace " + ex.StackTrace);
            }
        }
        #endregion

        #region Setar Certificado

        #region Buscar Certificado versao 01, indo buscar pelo arquivo

        public X509Certificate getCertificado(string DiretorioCertificado, string SenhaArquivo)
        {
            try
            {

                if (ConfigurationManager.AppSettings.Get("certificateThumbPrint") != null && ConfigurationManager.AppSettings.Get("certificateThumbPrint").ToString() != "")
                {
                    return GetCertificateByThumbprint(ConfigurationManager.AppSettings.Get("certificateThumbPrint").ToString());
                }


                X509Certificate cert;
                if (SenhaArquivo == null || SenhaArquivo == "")
                {
                    cert = X509Certificate.CreateFromCertFile(DiretorioCertificado);
                }
                else
                {
                    cert = new X509Certificate2(DiretorioCertificado, SenhaArquivo);
                    //cert = null;
                    //string certPath = DiretorioCertificado;
                    //string certPass = SenhaArquivo;
                    //X509Certificate2Collection collection = new X509Certificate2Collection();
                    //collection.Import(certPath, certPass, X509KeyStorageFlags.PersistKeySet);
                    //foreach (X509Certificate2 c in collection)
                    //{
                    //    cert = c;
                    //    break;
                    //}

                }

                return cert;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar buscar o certificado " + ex.Message);
            }
        }
        #endregion
        #region Buscar Certificado Versao 2 pelo Thumbprint

        public static X509Certificate2 GetCertificateByThumbprint(string certificateThumbPrint, StoreLocation certificateStoreLocation)
        {

            var certificate = GetListaCertificados(certificateStoreLocation).FirstOrDefault(cert =>

                new string(cert.Thumbprint.Where(char.IsLetterOrDigit).ToArray())

                .Equals(new string(certificateThumbPrint.Where(char.IsLetterOrDigit).ToArray()),

                StringComparison.InvariantCultureIgnoreCase));



            return certificate;

        }
        public static X509Certificate2 GetCertificateByThumbprint(string certificateThumbprint)
        {

            return GetCertificateByThumbprint(certificateThumbprint, StoreLocation.LocalMachine);

        }

        public static IEnumerable<X509Certificate2> GetListaCertificados(StoreLocation certificateStoreLocation)
        {

            var certificateStore = new X509Store(StoreLocation.LocalMachine);

            certificateStore.Open(OpenFlags.ReadOnly);

            var certCollection = certificateStore.Certificates;

            return certCollection.Cast<X509Certificate2>();

        }
        #endregion

        #endregion

        #region Processo para o envio de licencas e processos
        [WebMethod]
        public void ProcessoEnvioLicencaProcessos()
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            // return;
            WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();
            WsServicesReginRFB.Retorno retorno = new WsServicesReginRFB.Retorno();
            DataTable toReturn = new DataTable("RFBCURSORLICENCIAS");
            DataTable Dt = new DataTable();



            #region Pega Cursor para o envio

            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {
                using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
                {
                    using (SqlCommand cmdToExecute = new SqlCommand())
                    {
                        //DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
                        cmdToExecute.CommandText = "RFBCURSORLICENCIAS";
                        cmdToExecute.CommandType = CommandType.StoredProcedure;

                        //cmdToExecute.Parameters.Add(new SqlParameter("pCursor", SqlDbType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

                        _conn.Open();

                        cmdToExecute.Connection = _conn;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute))
                        {
                            adapter.Fill(toReturn);
                        }
                    }
                }
            }
            else
            {
                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
                {
                    using (OracleCommand cmdToExecute = new OracleCommand())
                    {
                        //DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
                        cmdToExecute.CommandText = "RFBCURSORLICENCIAS";
                        cmdToExecute.CommandType = CommandType.StoredProcedure;

                        cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

                        _conn.Open();

                        cmdToExecute.Connection = _conn;

                        using (OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute))
                        {
                            adapter.Fill(toReturn);
                        }
                    }
                }
            }
            #endregion


            for (int a = 0; a < toReturn.Rows.Count; a++)
            {
                #region preencher dados ws
                DataRow linha = toReturn.Rows[a];
                string IdRegistro = linha["IdRegistro"].ToString().Trim();
                string SupertipoRFB = linha["SupertipoRFB"].ToString().Trim();
                string TipoLicenciaInscricao = linha["TipoLicenciaInscricao"].ToString().Trim();
                string cpfAnalista = linha["cpfAnalista"].ToString().Trim();
                string cnpjEmpresa = linha["CnpjEmpresa"].ToString().Trim();
                string cnpjOrgao = linha["CnpjOrgao"].ToString().Trim();
                string nomeOrgao = linha["nomeOrgao"].ToString().Trim();
                string TipoInstituicaoRegin = linha["TipoInstituicao"].ToString().Trim();
                string identificacaoSolicitacao = linha["DBE"].ToString().Trim().Substring(10, 14);
                string reciboSolicitacao = linha["DBE"].ToString().Trim().Substring(0, 10);

                string protocoloRedesim = linha["NumeroUnico"].ToString().Trim();
                string situacao = linha["SituacaoLicenca"].ToString().Trim();
                string dataHoraEmissao = linha["DtConcessao"].ToString().Trim();
                string dataValidade = linha["DtValidade"].ToString().Trim();
                string dataAlteracao = linha["DtCriacao"].ToString().Trim();

                string codMunicipio = linha["CodMunicipio"].ToString().Trim();
                string uf = linha["uf"].ToString().Trim();

                string link = linha["link"].ToString().Trim();
                string numero = linha["numerolicenca"].ToString().Trim();
                string nomeLicenca = linha["NomeLicencia"].ToString().Trim();
                string motivo = "";

                #endregion

                #region Pegas dados da Cnae
                DataSet toReturnCnae = new DataSet("RFBLicencasDados");

                if (TipoLicenciaInscricao == "1")
                {
                    if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
                    {
                        using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
                        {
                            using (SqlCommand cmdToExecute = new SqlCommand())
                            {
                                //DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
                                cmdToExecute.CommandText = "RFBLicencasDadosCnae";
                                cmdToExecute.CommandType = CommandType.StoredProcedure;

                                cmdToExecute.Parameters.Add(new SqlParameter("id", SqlDbType.Decimal, 0, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, IdRegistro));
                                //cmdToExecute.Parameters.Add(new OracleParameter("CnaeEmpresa", SqlDbType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));
                                //cmdToExecute.Parameters.Add(new OracleParameter("CnaeLicenca", SqlDbType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));
                                //cmdToExecute.Parameters.Add(new OracleParameter("CnaeDISPENSADO", SqlDbType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

                                _conn.Open();

                                cmdToExecute.Connection = _conn;

                                using (SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute))
                                {
                                    adapter.Fill(toReturnCnae);
                                }
                            }
                        }
                    }
                    else
                    {
                        using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
                        {
                            using (OracleCommand cmdToExecute = new OracleCommand())
                            {
                                //DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
                                cmdToExecute.CommandText = "RFBLicencasDadosCnae";
                                cmdToExecute.CommandType = CommandType.StoredProcedure;

                                cmdToExecute.Parameters.Add(new OracleParameter("id", OracleType.Number, 0, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, IdRegistro));
                                cmdToExecute.Parameters.Add(new OracleParameter("CnaeEmpresa", OracleType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));
                                cmdToExecute.Parameters.Add(new OracleParameter("CnaeLicenca", OracleType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));
                                cmdToExecute.Parameters.Add(new OracleParameter("CnaeDISPENSADO", OracleType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

                                _conn.Open();

                                cmdToExecute.Connection = _conn;

                                using (OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute))
                                {
                                    adapter.Fill(toReturnCnae);
                                }
                            }
                        }
                    }
                }
                #endregion

                regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                retorno = regin.ServiceWs50(TipoLicenciaInscricao, SupertipoRFB, cpfAnalista, cnpjEmpresa, cnpjOrgao, nomeOrgao,
                                            identificacaoSolicitacao, reciboSolicitacao,
                                            protocoloRedesim, situacao, dataHoraEmissao, dataValidade, dataAlteracao,
                                            codMunicipio, uf,
                                            link, numero, nomeLicenca, motivo, TipoInstituicaoRegin, toReturnCnae);

                StringBuilder sqlU = new StringBuilder();
                if (retorno.status == "OK")
                {
                    //Para nao gravar o aquivo enviado
                    retorno.descricao = "";
                    if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                        {
                            conn.Open();
                            using (SqlTransaction _conn = conn.BeginTransaction())
                            {
                                using (SqlCommand cmdToExecute = new SqlCommand())
                                {
                                    cmdToExecute.Connection = _conn.Connection;
                                    cmdToExecute.Transaction = _conn;

                                    sqlU.AppendLine(" update PSC_PROTOCOLO_LICENCAS set PPL_DT_ENVIO_RFB = getdate(), PPL_STATUS_ENVIO_RFB = 3, PPL_ERRO_ENVIO_RFB = null, PPL_DADOS_ENVIADOS = @v_PPL_DADOS_ENVIADOS where PPL_ID_SEQ = " + IdRegistro);
                                    cmdToExecute.Parameters.Add(new SqlParameter("v_PPL_DADOS_ENVIADOS", SqlDbType.Text, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, retorno.descricao));
                                    cmdToExecute.CommandText = sqlU.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }
                                _conn.Commit();
                            }

                        }
                    }
                    else
                    {
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                        {
                            conn.Open();
                            using (OracleTransaction _conn = conn.BeginTransaction())
                            {
                                using (OracleCommand cmdToExecute = new OracleCommand())
                                {
                                    cmdToExecute.Connection = _conn.Connection;
                                    cmdToExecute.Transaction = _conn;

                                    sqlU.AppendLine(" update PSC_PROTOCOLO_LICENCAS set PPL_DT_ENVIO_RFB = sysdate, PPL_STATUS_ENVIO_RFB = 3, PPL_ERRO_ENVIO_RFB = null, PPL_DADOS_ENVIADOS = :v_PPL_DADOS_ENVIADOS where PPL_ID_SEQ = " + IdRegistro);
                                    cmdToExecute.Parameters.Add(new OracleParameter("v_PPL_DADOS_ENVIADOS", OracleType.Clob, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, retorno.descricao));
                                    cmdToExecute.CommandText = sqlU.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();
                                }
                                _conn.Commit();
                            }
                        }
                    }
                }

                if (retorno.status != "OK")
                {
                    if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                        {
                            conn.Open();
                            using (SqlTransaction _conn = conn.BeginTransaction())
                            {
                                using (SqlCommand cmdToExecute = new SqlCommand())
                                {
                                    cmdToExecute.Connection = _conn.Connection;
                                    cmdToExecute.Transaction = _conn;

                                    sqlU.AppendLine(" update PSC_PROTOCOLO_LICENCAS set PPL_DT_ENVIO_RFB = getdate(), PPL_STATUS_ENVIO_RFB = " + retorno.codretorno + ",  PPL_ERRO_ENVIO_RFB = @v_PPL_ERRO_ENVIO_RFB where PPL_ID_SEQ = " + IdRegistro);
                                    cmdToExecute.Parameters.Add(new SqlParameter("v_PPL_ERRO_ENVIO_RFB", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, retorno.descricao));

                                    cmdToExecute.CommandText = sqlU.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();

                                }
                                _conn.Commit();
                            }
                        }
                    }
                    else
                    {
                        using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                        {
                            conn.Open();
                            using (OracleTransaction _conn = conn.BeginTransaction())
                            {
                                using (OracleCommand cmdToExecute = new OracleCommand())
                                {
                                    cmdToExecute.Connection = _conn.Connection;
                                    cmdToExecute.Transaction = _conn;

                                    sqlU.AppendLine(" update PSC_PROTOCOLO_LICENCAS set PPL_DT_ENVIO_RFB = sysdate, PPL_STATUS_ENVIO_RFB = " + retorno.codretorno + ",  PPL_ERRO_ENVIO_RFB = :v_PPL_ERRO_ENVIO_RFB where PPL_ID_SEQ = " + IdRegistro);
                                    cmdToExecute.Parameters.Add(new OracleParameter("v_PPL_ERRO_ENVIO_RFB", OracleType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, retorno.descricao));

                                    cmdToExecute.CommandText = sqlU.ToString();
                                    cmdToExecute.CommandType = CommandType.Text;
                                    cmdToExecute.ExecuteNonQuery();

                                }
                                _conn.Commit();
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region ProcessaDadosServico99
        private DataTable getCursorDados99S13()
        {
            StringBuilder Sql = new StringBuilder();
            DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
            try
            {
                using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
                {
                    using (OracleCommand cmdToExecute = new OracleCommand())
                    {
                        //DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
                        cmdToExecute.CommandText = "RFBCURSORServicos13";
                        cmdToExecute.CommandType = CommandType.StoredProcedure;

                        cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

                        _conn.Open();

                        cmdToExecute.Connection = _conn;

                        using (OracleDataAdapter adapter = new OracleDataAdapter(cmdToExecute))
                        {
                            adapter.Fill(toReturn);
                        }
                    }
                    return toReturn;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable getCursorDados99S04()
        {
            StringBuilder Sql = new StringBuilder();
            try
            {
                using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "CursorDados99S04";
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable toReturn = new DataTable("CursorDados99S04");

                        cmd.Connection = _conn;
                        cmd.Connection.Open();

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(toReturn);
                            return toReturn;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable getCursorDados99()
        {
            StringBuilder Sql = new StringBuilder();
            try
            {
                using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "CursorDados99";
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString() == "13574983000111")
                        {

                            Sql.Append(@"SELECT *
                                FROM
                                    t73309_dados_servico99
                                WHERE
                                    1 = 1
                                    AND codServicoDisponivel IN ('S15', 'S08')
                                 --   AND codServicoDisponivel IN ('S17')
                                    AND StatusDbe NOT IN (9, 1)
                                    AND DataProcessamento IS NULL
                                    AND datainclusao < adddate(sysdate(), INTERVAL -2 hour)
                                    -- AND (DATE_FORMAT(sysdate(), '%w') IN (0, 6) -- Sabado e domingo
                                    -- OR (DATE_FORMAT(sysdate(), '%H') >= 20 -- de 20 horas ate as 23
                                    -- AND DATE_FORMAT(sysdate(), '%H') <= 23)
                                    -- OR (DATE_FORMAT(sysdate(), '%H') >= 0 -- de meia noite ate as 6 da manha
                                    -- AND DATE_FORMAT(sysdate(), '%H') <= 6)
                                    -- )
                                UNION
                                SELECT *
                                FROM
                                  t73309_dados_servico99
                                WHERE
                                  1 = 1
                                  AND codServicoDisponivel IN ('S17')
                                  AND StatusDbe NOT IN (9, 1)
                                  AND DataProcessamento IS NULL
                                  AND datainclusao < adddate(sysdate(), INTERVAL -2 hour)
                                UNION
                                SELECT *
                                FROM
                                    t73309_dados_servico99
                                WHERE
                                    1 = 1
                                    AND codServicoDisponivel IN ('S07')
                                    AND StatusDbe NOT IN (9, 1)
                                    AND DataProcessamento IS NULL
                                    AND datainclusao < adddate(sysdate(), INTERVAL -2 hour)
                                ORDER BY
                                    codServicoDisponivel DESC
                                , id

                                LIMIT
                                    500;");

                            //Sql = new StringBuilder();
                            //Sql.Append(@"SELECT * FROM t73309_dados_servico99 WHERE 1 = 1 AND id IN (3851319)");

                            cmd.CommandText = Sql.ToString();
                            cmd.CommandType = CommandType.Text;
                        }


                        DataTable toReturn = new DataTable("getCursorDados99");

                        cmd.Connection = _conn;
                        cmd.Connection.Open();

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(toReturn);
                            return toReturn;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable getCursorDados99EnvioMeiJunta()
        {
            StringBuilder Sql = new StringBuilder();
            try
            {
                using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "CursorDados99EnvioMeiJunta";
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable toReturn = new DataTable("getCursorDados99EnvioMeiJunta");

                        cmd.Connection = _conn;
                        cmd.Connection.Open();

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(toReturn);
                            return toReturn;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void ProcessaDadosServico99RespondeS04()
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            decimal pIdTabela = 0;
            DataTable dtCursor = getCursorDados99S04();
            foreach (DataRow pRow in dtCursor.Rows)
            {
                try
                {
                    pIdTabela = decimal.Parse(pRow["id"].ToString());
                    WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();
                    WsServicesReginRFB.Retorno resulRegin = new WsServicesReginRFB.Retorno();
                    WsServicesReginRFB.DadosWs04 dados = new WsServicesReginRFB.DadosWs04();
                    //T0101_RFB_PROCESSO_DEFERIDOS t01 = new T0101_RFB_PROCESSO_DEFERIDOS();
                    WsServicesReginRFB.redesim DadosDbe = new WsServicesReginRFB.redesim();

                    regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                    string pRecibo = pRow["reciboSolicitacao"].ToString();
                    string pIdentificador = pRow["identificacaoSolicitacao"].ToString();

                    dados.reciboSolicitacao = pRecibo;
                    dados.identificacaoSolicitacao = pIdentificador;
                    dados.resultadoValidacao = "01";


                    resulRegin = regin.ServiceWs04(dados);

                    if (resulRegin.status != "OK")
                    {
                        throw new Exception("Erro ao TENTAR ENVIAR S04 " + resulRegin.codretorno + " - " + resulRegin.descricao);
                    }

                    //Atualiza dados no MySql e Oracle
                    #region Atualiza dados
                    using (ConnectionProvider cpM = new ConnectionProvider())
                    {
                        cpM.OpenConnection();
                        cpM.BeginTransaction();

                        T73308_DBE_FORMA_ATUACAO atualiza99 = new T73308_DBE_FORMA_ATUACAO();
                        atualiza99.MainConnectionProvider = cpM;
                        atualiza99.UpdateStatusSERVICOS04RFB(pIdTabela, 1, resulRegin.descricao);


                        cpM.CommitTransaction(); // Commit MySql
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    T73308_DBE_FORMA_ATUACAO atualiza99 = new T73308_DBE_FORMA_ATUACAO();
                    atualiza99.UpdateStatusSERVICOS04RFB(pIdTabela, 9, ex.StackTrace + " " + ex.Message);
                }

            }
        }

        [WebMethod]
        public void ProcessaDadosServico13MensageriaRFB()
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            DataTable dtCursor = getCursorDados99S13();
            foreach (DataRow pRow in dtCursor.Rows)
            {
                string pRowid = "";
                try
                {
                    //pIdTabela = decimal.Parse(pRow["id"].ToString());
                    WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();
                    WsServicesReginRFB.Retorno resulRegin = new WsServicesReginRFB.Retorno();
                    WsServicesReginRFB.DadosWs13 dados = new WsServicesReginRFB.DadosWs13();
                    //T0101_RFB_PROCESSO_DEFERIDOS t01 = new T0101_RFB_PROCESSO_DEFERIDOS();
                    WsServicesReginRFB.DadosWs13 DadosDbe = new WsServicesReginRFB.DadosWs13();

                    regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                    string DBE = pRow["DBE"].ToString();
                    string nomeOrgaoResponsavel = pRow["OrgaoRegistro"].ToString();
                    string texto = pRow["texto"].ToString();
                    string link = pRow["linkUrl"].ToString();
                    string ProtocoloUnico = pRow["ProtocoloUnico"].ToString();
                    pRowid = ProtocoloUnico = pRow["pRowId"].ToString();
                    string pRecibo = "";
                    string pIdentificador = "";

                    pRecibo = DBE.Substring(0, 10);
                    pIdentificador = DBE.Substring(10, 14);

                    dados.reciboSolicitacao = pRecibo;
                    dados.identificacaoSolicitacao = pIdentificador;
                    dados.protocolo = ProtocoloUnico;

                    WsServicesReginRFB.mensagemInformativa mensagem = new WsServicesReginRFB.mensagemInformativa();


                    string[] menssagens = texto.Split(new string[] { "\n" }, StringSplitOptions.None);
                    //string[] menssagens2 = texto.Split(new string[] { "\n" }, StringSplitOptions.None);
                    //string[] menssagens3 = texto.Split(new string[] { "\\n" }, StringSplitOptions.None);

                    if (menssagens.Length > 0)
                    {
                        //string[] menssagensTratar = new string[menssagens.Length];
                        WsServicesReginRFB.mensagemInformativa[] mensagemVarias = new WsServicesReginRFB.mensagemInformativa[menssagens.Length];
                        int linha = 0;
                        foreach (string menssagem in menssagens)
                        {
                            if (menssagem != null && menssagem != "")
                            {
                                WsServicesReginRFB.mensagemInformativa inconpa2 = new WsServicesReginRFB.mensagemInformativa();
                                inconpa2.nomeOrgaoResponsavel = nomeOrgaoResponsavel;
                                inconpa2.texto = menssagem;
                                inconpa2.link = link;
                                //Se o link esta vacio, vejo se o texto tem link como enviar para rfb e aparecer no buton de link
                                if (link == "")
                                {
                                    var linkParser = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                                    var rawString = menssagem;
                                    foreach (Match m in linkParser.Matches(rawString))
                                    {
                                        string url = m.Value;
                                        inconpa2.link = url;
                                    }
                                }
                                mensagemVarias.SetValue(inconpa2, linha);
                                linha++;
                            }
                        }
                        dados.mensagemInformativa = mensagemVarias;
                    }
                    else
                    {
                        mensagem.nomeOrgaoResponsavel = nomeOrgaoResponsavel;
                        mensagem.texto = texto;
                        mensagem.link = link;

                        dados.mensagemInformativaUnica = mensagem;
                    }



                    resulRegin = regin.ServiceWs13(dados);

                    if (resulRegin.status != "OK")
                    {
                        throw new Exception("Erro ao TENTAR ENVIAR S13 " + resulRegin.codretorno + " - " + resulRegin.descricao);
                    }

                    string pMensagem = "";
                    string pStatus = "3";
                    if (resulRegin.codretorno == "03" || resulRegin.codretorno == "04")
                    {
                        pMensagem = resulRegin.codretorno + " - " + resulRegin.descricao;
                        pStatus = "-3";
                    }

                    //Atualiza dados no MySql e Oracle
                    #region Atualiza dados
                    GlobalV1 cc = new GlobalV1();

                    cc.UpdateRegistroEnviadoS13(pMensagem, pStatus, pRowid);


                    #endregion
                }
                catch (Exception ex)
                {
                    GlobalV1 cc = new GlobalV1();
                    cc.UpdateRegistroEnviadoS13(ex.Message, "9", pRowid);
                }
            }
        }

        [WebMethod]
        public void ProcessaDadosServico99()
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            string TipoProcessoMEI = "";

            if (ConfigurationManager.AppSettings["TipoProcessoMEI"] != null && ConfigurationManager.AppSettings["TipoProcessoMEI"].ToString() != "")
            {
                TipoProcessoMEI = ConfigurationManager.AppSettings["TipoProcessoMEI"].ToString();
            }

            /*
             * Se esta setado no Web.config varsao 2 e porque e a vaersao da JUCERJA
             */
            if (TipoProcessoMEI == "GRAVALOGOPSC")
            {

                ProcessoEnvioS99JUCERJA();
                //return;
                /*
                    Isto aqui e porque o RJ esta sem processar, entao para ir guardando o xml do 11 e do 09 para o MEI, e nao gravar no oracle
                 * porque vai precisar de muito espaço na maquina que tem o oracle XE 11. criamos uma rotina que grava os xml no MySql. ai depois teremos que 
                 * fazer outra para verificar o resto.
                 */
                //ProcessaDadosServico99V2();

                /*
                    Este processo envia os dados ja processados no procedimento acima para a JUCERJA
                 * ou seja depois de buscar o XMl do MEI com o ws11 envia para a Junta
                 */

                //ProcessoEnvioMEIJUCERJA();
                //    
            }
            /*
                Se continua e porque e o MEI Normall ou seja vai buscar os dados da RFB tambem mas coloca no Oracle para depois ser processado
             * no siarco e envia para os entes conveniados
             */
            decimal pIdTabela = 0;
            DataTable dtCursor = getCursorDados99();
            foreach (DataRow pRow in dtCursor.Rows)
            {
                try
                {
                    if (ConfigurationManager.AppSettings["TipoProcessoMEI"] != null && ConfigurationManager.AppSettings["TipoProcessoMEI"].ToString() != "")
                    {
                        TipoProcessoMEI = ConfigurationManager.AppSettings["TipoProcessoMEI"].ToString();
                    }

                    pIdTabela = decimal.Parse(pRow["id"].ToString());
                    WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();
                    WsServicesReginRFB.Retorno resulRegin35 = new WsServicesReginRFB.Retorno();
                    WsServicesReginRFB.Retorno resulRegin09 = new WsServicesReginRFB.Retorno();
                    WsServicesReginRFB.Retorno resulRegin11 = new WsServicesReginRFB.Retorno();
                    WsServicesReginRFB.RetornoV2 resulRegins15 = new WsServicesReginRFB.RetornoV2();
                    WsServicesReginRFB.RetornoV2 resulRegins17 = new WsServicesReginRFB.RetornoV2();

                    T0101_RFB_PROCESSO_DEFERIDOS t01 = new T0101_RFB_PROCESSO_DEFERIDOS();
                    WsServicesReginRFB.redesim DadosDbe = new WsServicesReginRFB.redesim();

                    regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                    string pRecibo = pRow["reciboSolicitacao"].ToString();
                    string pIdentificador = pRow["identificacaoSolicitacao"].ToString();
                    string pCnjMei = pRow["cnpjMei"].ToString();
                    string numeroAtoOficio = pRow["numeroAtoOficio"].ToString();
                    string convenioAtoOficio = pRow["codigoConvenioAto"].ToString();
                    string numeroProtocolo = pRow["numeroProtocolo"].ToString();
                    string UfRegistroTabelaRFB = pRow["uf"].ToString();
                    string pXmlPeriodoSimples = "";
                    string pXmlEventos = "";
                    string pAlteraQSA = "N";


                    t01.t0101_cod_serv_rfb = pRow["codServicoDisponivel"].ToString();
                    t01.t0101_cnpj_mei = pRow["cnpjMei"].ToString();
                    t01.t0101_numero_ato_oficio = numeroAtoOficio;
                    t01.t0101_codigoconvenioato = convenioAtoOficio;
                    t01.t0101_numeroProtocolo = numeroProtocolo;

                    if (pIdentificador != "" && pRecibo != "")
                        t01.t0101_dbe = pRecibo + pIdentificador;

                    #region Busca dados 35
                    if (pIdentificador != "" && pRecibo != "" && t01.t0101_cod_serv_rfb != "S15" && t01.t0101_cod_serv_rfb != "S17")
                    {

                        resulRegin35 = regin.ServiceWs35Soarquivo(pIdentificador, pRecibo);
                        t01.t0101_dbe = pRecibo + pIdentificador;
                        if (resulRegin35.status != "OK")
                        {
                            throw new Exception("Erro ao Buscar o 35 " + resulRegin35.descricao);
                        }
                        #region pega Informação ws35
                        if (resulRegin35.status == "OK")
                        {
                            DadosDbe = resulRegin35.oWs35Response.dadosRedesim;
                            t01.t0101_cnpj = DadosDbe.cnpj.Trim();
                            t01.t0101_nome_fantasia = GlobalV1.valNuloBranco(DadosDbe.fcpj.nomeFantasia);
                            t01.t0101_or = GlobalV1.valNuloBranco(DadosDbe.fcpj.codTipoOrgaoRegistro);
                            t01.t0101_tp_estab = Convert.ToInt32(DadosDbe.fcpj.inMatriz);
                            t01.t0101_viabilid_associada = GlobalV1.valNuloBranco(DadosDbe.numViabilidadeAssociada);
                            //t01.t0101_or = GlobalV1.valNuloBranco(DadosDbe.orgaoResponsavelDeferimento);
                            t01.t0101_orgao_deferimento = GlobalV1.valNuloBranco(DadosDbe.orgaoResponsavelDeferimento);

                            if (resulRegin35.oWs35Response.dadosRedesim.fcpj.codEvento != null)
                                pXmlEventos = GlobalV1.CreateXML(resulRegin35.oWs35Response.dadosRedesim.fcpj.codEvento);

                            if (resulRegin35.oWs35Response.dadosRedesim.socios != null)
                                pAlteraQSA = "S";

                            t01.t0101_dt_deferimento = DateTime.Now;
                            if (DadosDbe.fcpj.dataEvento != null && DadosDbe.fcpj.dataEvento.Length > 0)
                            {
                                string iString = "";
                                try
                                {
                                    string DataDeferimento = DadosDbe.fcpj.dataEvento[0];
                                    if (DataDeferimento.Length > 5)
                                    {
                                        string ano = DadosDbe.fcpj.dataEvento[0].Substring(0, 4);
                                        string mes = DadosDbe.fcpj.dataEvento[0].Substring(4).Remove(2);
                                        string dia = DadosDbe.fcpj.dataEvento[0].Substring(6);

                                        iString = ano + mes + dia;
                                        t01.t0101_dt_deferimento = DateTime.ParseExact(iString, "yyyyMMdd", null);
                                    }



                                    // t01.t0101_dt_deferimento = Convert.ToDateTime(dia + "/" + mes + "/" + ano);
                                }
                                catch
                                {
                                    throw new Exception("DadosDbe.fcpj.dataEvento sssss " + iString);

                                }
                            }


                            t01.t0101_nat_juridica = DadosDbe.fcpj.codNaturezaJuridica;
                            t01.t0101_codigo_retorno = resulRegin35.codretorno;
                            t01.t0101_cod_mun = 0;
                            t01.t0101_uf = "";
                            if (DadosDbe.fcpj.endereco != null)
                            {
                                t01.t0101_cod_mun = Convert.ToInt32(DadosDbe.fcpj.endereco.codMunicipio == null ? "0" : DadosDbe.fcpj.endereco.codMunicipio);
                                t01.t0101_uf = DadosDbe.fcpj.endereco.uf;
                            }



                        }
                        #endregion

                    }
                    #endregion

                    #region Buscar Dados S15
                    if (t01.t0101_cod_serv_rfb == "S15")
                    {
                        t01.t0101_indicador_mei = "A";


                        if (pIdentificador != "" && pRecibo != "")
                        {
                            t01.t0101_dbe = pRecibo + pIdentificador;
                            resulRegins15 = regin.ServiceWs15(pIdentificador, pRecibo, "", "");
                        }

                        if (numeroAtoOficio != "")
                        {
                            resulRegins15 = regin.ServiceWs15("", "", numeroAtoOficio, "");
                        }

                        if (resulRegins15.status != "OK")
                        {
                            throw new Exception("Erro ao Buscar o S15 " + resulRegins15.descricao);
                        }

                        if (resulRegins15.status == "OK")
                        {
                            t01.t0102_xml_sxx = resulRegins15.XmlDBE;
                        }



                        WsServices15RFB.serviceResponse ws15 = new WsServices15RFB.serviceResponse();
                        ws15 = (WsServices15RFB.serviceResponse)GlobalV1.CreateObject(resulRegins15.XmlDBE, ws15);
                        // WsServicesReginRFB.redesim DadosDBE = new WsServicesReginRFB.redesim();


                        #region igual a DBE

                        t01.t0101_cnpj = ws15.dadosRedesim.cnpj.Trim();
                        t01.t0101_nome_fantasia = GlobalV1.valNuloBranco(ws15.dadosRedesim.fcpj.nomeFantasia);
                        t01.t0101_or = GlobalV1.valNuloBranco(ws15.dadosRedesim.fcpj.codTipoOrgaoRegistro);
                        t01.t0101_tp_estab = Convert.ToInt32(ws15.dadosRedesim.fcpj.inMatriz);
                        t01.t0101_viabilid_associada = GlobalV1.valNuloBranco(ws15.dadosRedesim.numViabilidadeAssociada);
                        //t01.t0101_or = GlobalV1.valNuloBranco(DadosDbe.orgaoResponsavelDeferimento);
                        t01.t0101_orgao_deferimento = GlobalV1.valNuloBranco(ws15.dadosRedesim.orgaoResponsavelDeferimento);

                        t01.t0101_dt_deferimento = DateTime.Now;
                        if (ws15.dadosRedesim.fcpj.dataEvento != null && ws15.dadosRedesim.fcpj.dataEvento.Length > 0)
                        {
                            string iString = "";
                            try
                            {
                                string DataDeferimento = ws15.dadosRedesim.fcpj.dataEvento[0];
                                if (DataDeferimento.Length > 5)
                                {
                                    string ano = ws15.dadosRedesim.fcpj.dataEvento[0].Substring(0, 4);
                                    string mes = ws15.dadosRedesim.fcpj.dataEvento[0].Substring(4).Remove(2);
                                    string dia = ws15.dadosRedesim.fcpj.dataEvento[0].Substring(6);

                                    iString = ano + mes + dia;
                                    t01.t0101_dt_deferimento = DateTime.ParseExact(iString, "yyyyMMdd", null);
                                }



                                // t01.t0101_dt_deferimento = Convert.ToDateTime(dia + "/" + mes + "/" + ano);
                            }
                            catch
                            {
                                throw new Exception("DadosDbe.fcpj.dataEvento sssss " + iString);

                            }
                        }


                        t01.t0101_nat_juridica = ws15.dadosRedesim.fcpj.codNaturezaJuridica;
                        // Coloquei 30/11/2020 aqui 10 fixo porque retirei a ida ao 35, e aqui no s15 nao tem esse campo, fiz uma pesquisa no banco e todos os registros ate agora estavam com 10
                        t01.t0101_codigo_retorno = "10";
                        t01.t0101_cod_mun = 0;
                        t01.t0101_uf = "";
                        if (ws15.dadosRedesim.fcpj.endereco != null)
                        {
                            t01.t0101_cod_mun = Convert.ToInt32(ws15.dadosRedesim.fcpj.endereco.codMunicipio == null ? "0" : ws15.dadosRedesim.fcpj.endereco.codMunicipio);
                            t01.t0101_uf = ws15.dadosRedesim.fcpj.endereco.uf;
                        }
                        #endregion



                        if (t01.t0101_cnpj_mei == "")
                        {
                            t01.t0101_cnpj_mei = ws15.dadosRedesim.cnpj;
                        }


                        if (ws15.dadosRedesim.simplesNacional != null)
                        {
                            pXmlPeriodoSimples = GlobalV1.CreateXML(ws15.dadosRedesim.simplesNacional);
                        }

                        if (ws15.dadosRedesim.fcpj.codEvento != null)
                            pXmlEventos = GlobalV1.CreateXML(ws15.dadosRedesim.fcpj.codEvento);

                        if (ws15.dadosRedesim.socios != null)
                            pAlteraQSA = "S";


                        t01.t0101_cod_mun = decimal.Parse(ws15.dadosRedesim.dadosCNPJ.enderecoDadosCNPJ.codMunicipio);
                        t01.t0101_uf = ws15.dadosRedesim.dadosCNPJ.enderecoDadosCNPJ.uf;

                        t01.t0101_cnpj = t01.t0101_cnpj_mei;
                        pCnjMei = t01.t0101_cnpj_mei;

                        //Pegar a UF e o municipio oregim, isso acontece mais nos registros com evento 210 
                        if (!GlobalV1.valNulo(ws15.dadosRedesim.fcpj.codMunicOrigem))
                        {
                            if (ws15.dadosRedesim.fcpj.codMunicOrigem != ws15.dadosRedesim.dadosCNPJ.enderecoDadosCNPJ.codMunicipio)
                            {
                                t01.t0101_cod_mun_ant = decimal.Parse(ws15.dadosRedesim.fcpj.codMunicOrigem);
                                t01.t0101_uf_ant = ws15.dadosRedesim.fcpj.ufOrigem;
                            }
                        }


                        if (ws15.dadosRedesim.fcpj != null && ws15.dadosRedesim.fcpj.codEvento != null)
                        {
                            foreach (string pCodEvento in ws15.dadosRedesim.fcpj.codEvento)
                            {
                                if (pCodEvento == "101")
                                {
                                    t01.t0101_indicador_mei = "I";
                                    break;
                                }

                                if (pCodEvento == "517")
                                {
                                    t01.t0101_indicador_mei = "B";
                                    break;
                                }

                                if (pCodEvento == "327")
                                {
                                    t01.t0101_evento_simples = "S";
                                    break;
                                }
                                //t01.EventoUpdate(pIdTabela, pCodEvento);
                            }
                        }

                        //Somente ve se vai enquadradar ou desenquadrar se for eveto 327 ou o indicativo de t0101_evento_simples =  S (Simples)
                        if (t01.t0101_evento_simples == "S")
                        {
                            if (ws15.dadosRedesim.simplesNacional.periodoMei != null)
                            {
                                int quantidade = ws15.dadosRedesim.simplesNacional.periodoMei.Count();
                                WsServices15RFB.periodo Periodo = ws15.dadosRedesim.simplesNacional.periodoMei[quantidade - 1];

                                if (Periodo.dataExclusao == null || Periodo.dataExclusao == "" || Periodo.dataExclusao == "00000000")
                                {
                                    t01.t0101_indicador_mei = "E";
                                }

                                if (Periodo.dataExclusao != null && Periodo.dataExclusao != "" && Periodo.dataExclusao != "00000000")
                                {
                                    t01.t0101_indicador_mei = "D";
                                }
                            }
                            if (ws15.dadosRedesim.simplesNacional.periodoMei == null)
                            {
                                t01.t0101_indicador_mei = "";
                            }
                        }
                        else
                        {
                            //Para nao gravar a informação do XML
                            //t01.t0102_xml_sxx = "";
                        }



                        #region Busca dados MEI s11 e S09
                        if (t01.t0101_cnpj_mei != "")
                        {
                            //t01.t0101_dt_deferimento
                            t01.t0101_dt_evento_mei = DateTime.Parse(pRow["dataEventoMei"].ToString());

                            if (t01.t0101_dt_evento_mei == null || t01.t0101_dt_evento_mei == DateTime.MinValue)
                            {
                                t01.t0101_dt_evento_mei = t01.t0101_dt_deferimento;
                            }


                            resulRegin11 = regin.ServiceWs11(t01.t0101_cnpj_mei);

                            if (resulRegin11.status != "OK")
                            {
                                throw new Exception("Erro ao Buscar ws 11 v2 " + resulRegin11.descricao);
                            }
                            t01.t0101_xml_rfb = resulRegin11.XmlDBE;
                            t01.t0101_tp_estab = decimal.Parse(resulRegin11.oCNPJResponse.dadosCNPJ[0].indMatrizFilial);
                            resulRegin09 = regin.ServiceWs09(resulRegin11.oCNPJResponse.dadosCNPJ[0].cpfRepresentante);

                            if (resulRegin09.status != "OK")
                            {
                                throw new Exception("Erro ao Buscar ws 09 " + resulRegin09.descricao);
                            }

                            t01.t0101_xml_rfb_09 = resulRegin09.XmlDBE;
                        }
                        else
                        {
                            throw new Exception("Erro ao buscar dados do MEI, o cNPJ do MEI esta vazio ");
                        }
                        #endregion


                    }
                    #endregion


                    #region Busca dados s08
                    if (t01.t0101_cnpj != "" && t01.t0101_cnpj.Length == 14 && t01.t0101_cod_serv_rfb == "S08")
                    {
                        resulRegin11 = regin.ServiceWs11(t01.t0101_cnpj);

                        if (resulRegin11.status != "OK")
                        {
                            throw new Exception("Erro ao Buscar ws 11 S08" + resulRegin11.descricao);
                        }
                        t01.t0101_xml_rfb = resulRegin11.XmlDBE;
                        t01.t0101_tp_estab = decimal.Parse(resulRegin11.oCNPJResponse.dadosCNPJ[0].indMatrizFilial);

                        t01.t0101_xml_rfb_09 = "";
                        if (resulRegin11.oCNPJResponse.dadosCNPJ[0].cnpjMatriz != null)
                            t01.t0101_cnpj_matriz = GlobalV1.valNuloBranco(resulRegin11.oCNPJResponse.dadosCNPJ[0].cnpjMatriz);
                    }
                    #endregion

                    #region Busca dados s07
                    if (t01.t0101_cod_serv_rfb == "S07")
                    {
                        GlobalV1 pXMl = new GlobalV1();
                        RetornoBasico pResulS07 = new RetornoBasico();
                        pResulS07 = pXMl.GeraXMLServico07RFB(pRecibo, pIdentificador);

                        if (pResulS07.status != "OK")
                        {
                            if (pResulS07.codretorno == "07")
                            {
                                throw new InvalidCastException(pResulS07.descricao, 07);
                            }

                            throw new Exception("Erro ao Buscar ws S07 " + pResulS07.descricao);
                        }

                        t01.t0101_xml_rfb = pResulS07.XmlDBE;
                        t01.t0101_tp_estab = 2;

                        t01.t0101_xml_rfb_09 = "";

                        // --------------------------------------------------------------
                        // WsServicesReginRFB.ServiceReginRFB regin07 = new WsServicesReginRFB.ServiceReginRFB();
                        WsServicesReginRFB.RetornoV2 resulRegin = new WsServicesReginRFB.RetornoV2();

                        regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();


                        resulRegin = regin.ServiceWs07(pIdentificador, pRecibo);
                        if (resulRegin.status == "OK")
                        {
                            WsServices07RFB.serviceResponse ws07 = new WsServices07RFB.serviceResponse();
                            ws07 = (WsServices07RFB.serviceResponse)GlobalV1.CreateObject(resulRegin.XmlDBE, ws07);
                            WsServices07RFB.simplesNacional dadosRedeSim = new WsServices07RFB.simplesNacional();
                            dadosRedeSim = ws07.dadosRedesim.simplesNacional;
                            if (dadosRedeSim != null)
                            {
                                pXmlPeriodoSimples = GlobalV1.CreateXML(dadosRedeSim);
                            }

                            if (ws07.dadosRedesim.fcpj.codEvento != null)
                                pXmlEventos = GlobalV1.CreateXML(ws07.dadosRedesim.fcpj.codEvento);

                            if (ws07.dadosRedesim.socios != null)
                                pAlteraQSA = "S";

                        }
                        else
                        {
                            throw new Exception("Erro chamar o s07: " + resulRegin.descricao);
                        }

                    }
                    #endregion

                    #region Busca dados s17

                    if (t01.t0101_cod_serv_rfb == "S17")
                    {
                        //WsServicesReginRFB.ServiceReginRFB regin17 = new WsServicesReginRFB.ServiceReginRFB();
                        WsServicesReginRFB.RetornoV2 resulRegin = new WsServicesReginRFB.RetornoV2();
                        regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();
                        resulRegin = regin.ServiceWs17(pIdentificador, pRecibo, numeroAtoOficio, convenioAtoOficio);
                        WsServices17RFB.serviceResponse ws17v2 = new WsServices17RFB.serviceResponse();
                        ws17v2 = (WsServices17RFB.serviceResponse)GlobalV1.CreateObject(resulRegin.XmlDBE, ws17v2);



                        if (resulRegin.status != "NOK")
                        {
                            if (!GlobalV1.valNulo(ws17v2.dadosRedesim.simplesNacional))// != null)
                            {
                                pXmlPeriodoSimples = GlobalV1.CreateXML(ws17v2.dadosRedesim.simplesNacional);
                            }
                        }

                        #region igual a DBE
                        t01.t0101_dt_deferimento = DateTime.Now;

                        if (ws17v2.dadosRedesim.dadosCNPJ[0].indMatrizFilial != null)
                            t01.t0101_tp_estab = Decimal.Parse(ws17v2.dadosRedesim.dadosCNPJ[0].indMatrizFilial);

                        if (ws17v2.dadosRedesim.fcpj != null)
                        {
                            t01.t0101_nome_fantasia = GlobalV1.valNuloBranco(ws17v2.dadosRedesim.fcpj.nomeFantasia);
                            t01.t0101_or = GlobalV1.valNuloBranco(ws17v2.dadosRedesim.fcpj.codTipoOrgaoRegistro);
                            t01.t0101_tp_estab = Convert.ToInt32(ws17v2.dadosRedesim.fcpj.inMatriz);
                            if (ws17v2.dadosRedesim.fcpj.dataEvento != null && ws17v2.dadosRedesim.fcpj.dataEvento.Length > 0)
                            {
                                string iString = "";
                                try
                                {
                                    string DataDeferimento = ws17v2.dadosRedesim.fcpj.dataEvento[0];
                                    if (DataDeferimento.Length > 5)
                                    {
                                        string ano = ws17v2.dadosRedesim.fcpj.dataEvento[0].Substring(0, 4);
                                        string mes = ws17v2.dadosRedesim.fcpj.dataEvento[0].Substring(4).Remove(2);
                                        string dia = ws17v2.dadosRedesim.fcpj.dataEvento[0].Substring(6);

                                        iString = ano + mes + dia;
                                        t01.t0101_dt_deferimento = DateTime.ParseExact(iString, "yyyyMMdd", null);
                                    }



                                    // t01.t0101_dt_deferimento = Convert.ToDateTime(dia + "/" + mes + "/" + ano);
                                }
                                catch
                                {
                                    throw new Exception("DadosDbe.fcpj.dataEvento sssss " + iString);

                                }
                            }
                        }

                        t01.t0101_cnpj = ws17v2.dadosRedesim.cnpj.Trim();

                        t01.t0101_viabilid_associada = GlobalV1.valNuloBranco(ws17v2.dadosRedesim.numViabilidadeAssociada);
                        //t01.t0101_or = GlobalV1.valNuloBranco(DadosDbe.orgaoResponsavelDeferimento);
                        t01.t0101_orgao_deferimento = GlobalV1.valNuloBranco(ws17v2.dadosRedesim.orgaoResponsavelDeferimento);





                        t01.t0101_nat_juridica = ws17v2.dadosRedesim.dadosCNPJ[0].naturezaJuridica;
                        // Coloquei 30/11/2020 aqui 10 fixo porque retirei a ida ao 35, e aqui no s15 nao tem esse campo, fiz uma pesquisa no banco e todos os registros ate agora estavam com 10
                        t01.t0101_codigo_retorno = "10";
                        t01.t0101_cod_mun = 0;
                        t01.t0101_uf = "";
                        if (ws17v2.dadosRedesim.dadosCNPJ[0].enderecoDadosCNPJ != null)
                        {
                            t01.t0101_cod_mun = Convert.ToInt32(ws17v2.dadosRedesim.dadosCNPJ[0].enderecoDadosCNPJ.codMunicipio == null ? "0" : ws17v2.dadosRedesim.dadosCNPJ[0].enderecoDadosCNPJ.codMunicipio);
                            t01.t0101_uf = ws17v2.dadosRedesim.dadosCNPJ[0].enderecoDadosCNPJ.uf;
                        }
                        #endregion






                        if (ws17v2.dadosRedesim.fcpj != null && ws17v2.dadosRedesim.fcpj.codEvento != null)
                            pXmlEventos = GlobalV1.CreateXML(ws17v2.dadosRedesim.fcpj.codEvento);

                        if (ws17v2.dadosRedesim.dadosEspecificos != null && ws17v2.dadosRedesim.dadosEspecificos.eventos != null)
                            pXmlEventos = GlobalV1.CreateXML(ws17v2.dadosRedesim.dadosEspecificos.eventos);

                        if (ws17v2.dadosRedesim.socios != null)
                            pAlteraQSA = "S";


                        GlobalV1 pXMl17 = new GlobalV1();
                        RetornoBasico pResulS17 = new RetornoBasico();
                        pResulS17 = pXMl17.GeraXMLServico17RFB(UfRegistroTabelaRFB, t01.t0101_cnpj, pRecibo, pIdentificador, numeroAtoOficio, convenioAtoOficio);

                        if (pResulS17.status != "OK")
                        {
                            if (pResulS17.codretorno == "07")
                            {
                                throw new InvalidCastException(pResulS17.descricao, 07);
                            }

                            throw new Exception("Erro aofazer o XML do regin com os dados do 17 " + pResulS17.descricao);
                        }



                        //Se entra aqui e porque deu Ok e 00 significa que e um registro de matriz de outro estado que esta alterando
                        // Nome, natureza, porte ou baixa, caso diferente e porque nao e dana odo falado acima
                        if (pResulS17.codretorno == "00")
                        {
                            t01.t0102_xml_regin = pResulS17.XmlDBE;
                            t01.t0101_altera_matriz_fora = "S";
                        }

                        if (numeroAtoOficio != "")
                        {
                            resulRegins17 = regin.ServiceWs17("", "", numeroAtoOficio, convenioAtoOficio);

                            if (resulRegins17.status != "OK")
                            {
                                throw new Exception("Erro ao Buscar o S17 ato de oficio " + resulRegins17.descricao);
                            }

                            if (resulRegins17.status == "OK")
                            {
                                WsServices17RFB.serviceResponse ws17 = new WsServices17RFB.serviceResponse();
                                ws17 = (WsServices17RFB.serviceResponse)GlobalV1.CreateObject(resulRegins17.XmlDBE, ws17);

                                t01.t0101_codigo_retorno = "10"; //Significa que foi deferido, e fixo porque esse capo somente esta no 35
                                t01.t0101_cnpj = ws17.dadosRedesim.cnpj;
                                t01.t0102_xml_sxx = resulRegins17.XmlDBE;
                                t01.t0101_nat_juridica = ws17.dadosRedesim.fcpj.codNaturezaJuridica;

                                t01.t0101_orgao_deferimento = GlobalV1.valNuloBranco(ws17.dadosRedesim.orgaoResponsavelDeferimento);

                                if (ws17.dadosRedesim.fcpj.dataEvento != null && ws17.dadosRedesim.fcpj.dataEvento.Length > 0)
                                {
                                    string iString = "";
                                    try
                                    {
                                        string DataDeferimento = ws17.dadosRedesim.fcpj.dataEvento[0];
                                        if (DataDeferimento.Length > 5)
                                        {
                                            string ano = ws17.dadosRedesim.fcpj.dataEvento[0].Substring(0, 4);
                                            string mes = ws17.dadosRedesim.fcpj.dataEvento[0].Substring(4).Remove(2);
                                            string dia = ws17.dadosRedesim.fcpj.dataEvento[0].Substring(6);

                                            iString = ano + mes + dia;
                                            t01.t0101_dt_deferimento = DateTime.ParseExact(iString, "yyyyMMdd", null);
                                        }
                                    }
                                    catch
                                    {
                                        throw new Exception("ws17.dadosRedesim.fcpj.dataEvento S17 Errada " + iString);

                                    }
                                }

                            }
                        }



                        if (t01.t0101_cnpj != "" && t01.t0101_cnpj.Length == 14)
                        {
                            dHelperQuery q = new dHelperQuery();

                            decimal pMarcados24 = 2;
                            DataTable DtS24 = q.getMarcadoS24(t01.t0101_cnpj);
                            if (DtS24.Rows.Count > 0)
                            {
                                pMarcados24 = decimal.Parse(DtS24.Rows[0]["statusMarcado"].ToString());
                            }

                            t01.t0101_marcado_s24 = pMarcados24;

                            resulRegin11 = regin.ServiceWs11(t01.t0101_cnpj);

                            if (resulRegin11.status != "OK")
                            {
                                throw new Exception("Erro ao Buscar ws 11 s17" + resulRegin11.descricao);
                            }
                            t01.t0101_xml_rfb = resulRegin11.XmlDBE;
                            t01.t0101_tp_estab = decimal.Parse(resulRegin11.oCNPJResponse.dadosCNPJ[0].indMatrizFilial);

                            if (resulRegin11.oCNPJResponse.dadosCNPJ[0].endereco.uf != null)
                            {
                                t01.t0101_uf = resulRegin11.oCNPJResponse.dadosCNPJ[0].endereco.uf;
                                t01.t0101_cod_mun = Convert.ToInt32(resulRegin11.oCNPJResponse.dadosCNPJ[0].endereco.codMunicipio == null ? "0" : resulRegin11.oCNPJResponse.dadosCNPJ[0].endereco.codMunicipio);
                            }

                            t01.t0101_xml_rfb_09 = "";
                            if (resulRegin11.oCNPJResponse.dadosCNPJ[0].cnpjMatriz != null)
                                t01.t0101_cnpj_matriz = GlobalV1.valNuloBranco(resulRegin11.oCNPJResponse.dadosCNPJ[0].cnpjMatriz);
                        }



                    }
                    #endregion

                    if (t01.t0101_uf == "")
                    {
                        t01.t0101_uf = pRow["uf"].ToString();
                    }
                    t01.t0101_id_rfb = pIdTabela;// "t01.t0101_id_rfb";
                    t01.t0101_data_recebido_rfb = DateTime.Parse(pRow["datainclusao"].ToString());


                    //t01.T0101_CNPJ_MATRIZ = string.IsNullOrEmpty(XmlWs11.retornoWS11Redesim.dadosCNPJ.cnpjMatriz) ? Processo.T0101_CNPJ : 
                    //t01.T0101_COD_MUN = Convert.ToInt32(string.IsNullOrEmpty(XmlWs11.retornoWS11Redesim.dadosCNPJ.endereco.codMunicipio) ? "0" : 
                    //t01.t0101_nat_juridica = XmlWs11.retornoWS11Redesim.dadosCNPJ.naturezaJuridica;

                    //Atualiza dados no MySql e Oracle
                    #region Atualiza dados
                    using (ConnectionProvider cpM = new ConnectionProvider())
                    {
                        cpM.OpenConnection();
                        cpM.BeginTransaction();
                        using (ConnectionProviderORACLE cp = new ConnectionProviderORACLE())
                        {
                            cp.OpenConnection();
                            cp.BeginTransaction();
                            t01.MainConnectionProvider = cp;

                            //Isto aqui e para sempre apagar os periodos quando for evento de simples, para caso venha sem periodos apaga o dados
                            //ja que a RFB esta com erro e nao envia ssempre os periodos como devia ser
                            //isso foi conversado dia 26/09/2022 numa reuniao com o pessoal da SC, SEFAZ, Tiago e Raul Gamboa
                            if (t01.t0101_evento_simples == "S")
                            {
                                t01.DeletePeriodosSimplesOracle(cp.CurrentTransaction, t01.t0101_cnpj);
                            }

                            if (pXmlPeriodoSimples != "")
                            {
                                t01.UpdateXmlSIMPLESMei(cp.CurrentTransaction, t01.t0101_cnpj, t01.t0101_id_rfb.ToString(), pXmlPeriodoSimples);
                            }


                            //Isto aqui e para nan gravar MEI direto nas PSCS ja que e evento de simples, caso seja s15 e claro
                            if (t01.t0101_evento_simples == "S")
                            {
                                //Nao grava MEI automatico por ser evento do simples
                                TipoProcessoMEI = "NAOGRAVALOGOPSC";
                            }

                            // Somente nao grava os aquivos se MEI e gravado direto PSC, ja que vou pegar os dados do s11
                            if (TipoProcessoMEI == "GRAVALOGOPSC")
                            {
                                t01.t0101_xml_rfb = "";
                                t01.t0101_xml_rfb_09 = "";
                                t01.t0102_xml_sxx = "";
                            }

                            t01.Update();

                            t01.EventoDelete(pIdTabela);

                            bool gravouEventoDBE = false;
                            //Gravar evento feito novo
                            if (pXmlEventos != "" || pAlteraQSA == "S")
                            {
                                if (pXmlEventos != "")
                                {
                                    XmlDocument doc = new XmlDocument();
                                    doc.LoadXml(pXmlEventos);
                                    XmlNodeList elemList = doc.GetElementsByTagName("string");
                                    for (int i = 0; i < elemList.Count; i++)
                                    {
                                        string evento = elemList[i].InnerXml;
                                        if (evento != "")
                                        {
                                            t01.EventoUpdate(pIdTabela, evento);
                                        }
                                    }
                                }
                                if (pAlteraQSA == "S" && pXmlEventos == "")
                                {
                                    t01.EventoUpdate(pIdTabela, "202");
                                }
                                gravouEventoDBE = true;
                            }


                            if (t01.t0101_cod_serv_rfb != "S15" && !gravouEventoDBE)
                            {
                                //Busco o evento caso esteja no ws35
                                if (DadosDbe.fcpj != null && DadosDbe.fcpj.codEvento != null)
                                {
                                    foreach (string pCodEvento in DadosDbe.fcpj.codEvento)
                                    {
                                        if (pCodEvento == "")
                                        {
                                            break;
                                        }
                                        t01.EventoUpdate(pIdTabela, pCodEvento);
                                        gravouEventoDBE = true;
                                    }
                                }
                            }


                            //Se ele e S17 E NA TIVER GRAVADO EVENTO DO DBE E O NUMERO DO ATO DE OFICIO E DIFERENTE DE BRANCO
                            //E PORQUE  NAO TEM DBE E VAMOS PEGAR OS EVENTOS DO S17
                            if (t01.t0101_cod_serv_rfb == "S17" && numeroAtoOficio != "" && !gravouEventoDBE)
                            {
                                WsServices17RFB.serviceResponse ws17 = new WsServices17RFB.serviceResponse();
                                ws17 = (WsServices17RFB.serviceResponse)GlobalV1.CreateObject(resulRegins17.XmlDBE, ws17);

                                if (ws17.dadosRedesim.fcpj != null && ws17.dadosRedesim.fcpj.codEvento != null)
                                {
                                    foreach (string pCodEvento in ws17.dadosRedesim.fcpj.codEvento)
                                    {
                                        if (pCodEvento == "")
                                        {
                                            break;
                                        }
                                        t01.EventoUpdate(pIdTabela, pCodEvento);
                                    }
                                }
                            }

                            //se for s15 sem DBE busco o evento do s15
                            if (t01.t0101_cod_serv_rfb == "S15" && !gravouEventoDBE)
                            {
                                WsServices15RFB.serviceResponse ws15 = new WsServices15RFB.serviceResponse();
                                ws15 = (WsServices15RFB.serviceResponse)GlobalV1.CreateObject(resulRegins15.XmlDBE, ws15);

                                if (ws15.dadosRedesim.fcpj != null && ws15.dadosRedesim.fcpj.codEvento != null)
                                {
                                    foreach (string pCodEvento in ws15.dadosRedesim.fcpj.codEvento)
                                    {
                                        if (pCodEvento == "")
                                        {
                                            break;
                                        }
                                        t01.EventoUpdate(pIdTabela, pCodEvento);
                                    }
                                }

                                if (TipoProcessoMEI == "GRAVALOGOPSC")
                                {
                                    using (UpdateTabelasRuc p = new UpdateTabelasRuc())
                                    {
                                        dHelperQuery nc = new dHelperQuery();



                                        p.MainConnectionProvider = cp;
                                        decimal pTipoOperacao = 2;

                                        #region Pega Tipo Operação
                                        if (ws15.dadosRedesim.fcpj != null && ws15.dadosRedesim.fcpj.codEvento != null)
                                        {
                                            foreach (string pCodEvento in ws15.dadosRedesim.fcpj.codEvento)
                                            {
                                                if (pCodEvento == "")
                                                {
                                                    break;
                                                }

                                                if (pCodEvento == "101" || pCodEvento == "102")
                                                {
                                                    pTipoOperacao = 1;
                                                }
                                            }
                                        }
                                        #endregion

                                        //por ser ProcessoEnvioMEIJUCERJA

                                        pTipoOperacao = 7;
                                        string pMunic = t01.t0101_cod_mun.ToString() + psc.Framework.General.CalculateVerificationDigit(t01.t0101_cod_mun.ToString(), 11).ToString();

                                        string pProtocolo = "";

                                        //So vou gravar se for difeente de Rio de janeiro, ja que a sefaz nao esta recebendo esse municipio
                                        // e como a prefeitura nem bombeiro tb nao recebe, nao vou gravar para nao encher o banco
                                        // lebrando que esse banco e XE e pode esgotar e essa funcionalidade para o RJ nao e definitiva
                                        // isso e so um paleativo ate a junta comercial voltem a processar o MEI
                                        if (pMunic != "60011")
                                        {
                                            pProtocolo = "MEI" + nc.BuscarCorrelativo(77);


                                            p.UpdatePSC_PROTOCOLO(pProtocolo, t01.t0101_uf, decimal.Parse(pMunic), pTipoOperacao, t01.t0101_viabilid_associada, "");

                                            p.UpdatePSC_IDENT_PROTOOLO(pProtocolo, t01.t0101_cnpj, "", "", ws15.dadosRedesim.fcpj.nomeEmpresarial);

                                            p.UpdateOutrosCamposT01_PROCESSO_DEFERIDOS(pIdTabela, pProtocolo);

                                            string pCNPJParaRetorno = "27079821000111"; //Retornar para 

                                            p.UpdateOutrosCamposPSC_Protocolo(pProtocolo, "25", t01.t0101_viabilid_associada, t01.t0101_dbe, pCNPJParaRetorno, "");

                                            p.Update_mac_log_carga_junta_homolog(pProtocolo, "AUTOMATICO", true, true, false, false, false);

                                            p.Update_mac_log_carga_junta_homolog(pProtocolo, "AUTOMATICO", true, true, false, false, false);

                                            if (ws15.dadosRedesim.fcpj != null && ws15.dadosRedesim.fcpj.codEvento != null)
                                            {
                                                foreach (string pCodEvento in ws15.dadosRedesim.fcpj.codEvento)
                                                {
                                                    if (pCodEvento == "")
                                                    {
                                                        break;
                                                    }
                                                    p.UPDATE_PSC_PROT_EVENTO_RFB(pProtocolo, pCodEvento);
                                                }
                                            }
                                        }
                                        //cp.CommitTransaction();
                                    }
                                }
                            }


                            int pCount = t01.EventoQueryQtd(pIdTabela);
                            if (pCount == 0)
                            {
                                throw new Exception("Protocolo não Tem Evento");
                            }

                            T73308_DBE_FORMA_ATUACAO atualiza99 = new T73308_DBE_FORMA_ATUACAO();
                            atualiza99.MainConnectionProvider = cpM;
                            atualiza99.UpdateStatusT73309Servico99(pIdTabela, 1, "");

                            //cp.RollbackTransaction(""); //Commit Oracle
                            //cpM.RollbackTransaction(""); // Commit MySql

                            cp.CommitTransaction();
                            cpM.CommitTransaction();
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    T73308_DBE_FORMA_ATUACAO atualiza99 = new T73308_DBE_FORMA_ATUACAO();
                    atualiza99.UpdateStatusT73309Servico99(pIdTabela, 9, ex.StackTrace + " " + ex.Message);

                    //Aqui e porque o erro da rfb entao marco como processado, ja que o DBE esta em aberto, isso e na busca do s17
                    if (ex.HResult == 07)
                        atualiza99.UpdateStatusT73309Servico99(pIdTabela, 1, ex.StackTrace + " " + ex.Message);




                }


            }
        }
        #region Processo Exclusivo da JUCERJA

        public DataTable RecuperaS99ReginJUCERJA()
        {


            StringBuilder Sql = new StringBuilder();
            try
            {
                using (MySqlConnection _conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "CursorRecuperaS99ReginJUCERJA";
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable toReturn = new DataTable("TAG_TABELA_REMOVER");

                        cmd.Connection = _conn;
                        cmd.Connection.Open();

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(toReturn);
                            return toReturn;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            //   StringBuilder Sql = new StringBuilder();
            //   Sql.AppendLine(" Select id ");
            //   Sql.AppendLine(", codServicoDisponivel ");
            //   Sql.AppendLine(", codigoConvenioAto ");
            //   Sql.AppendLine(", identificacaoSolicitacao ");
            //   Sql.AppendLine(", numeroAtoOficio ");
            //   Sql.AppendLine(", numeroOcorrencia ");
            //   Sql.AppendLine(", reciboSolicitacao ");
            //   Sql.AppendLine(", numeroProtocolo ");
            //   Sql.AppendLine(", datainclusao ");
            //   Sql.AppendLine(", uf ");
            //   Sql.AppendLine(", cnpjMei ");
            //   Sql.AppendLine(", indicadorMei ");
            //   Sql.AppendLine(", dataEventoMei");

            //   Sql.AppendLine(" From   t73309_dados_servico99 p ");
            //   Sql.AppendLine(" Where  1 = 1 ");
            ////   Sql.AppendLine(" and    id in (1303)");

            //   Sql.AppendLine(" AND codServicoDisponivel in ('S15', 'S17', 'S07') ");
            //   Sql.AppendLine(" AND StatusEnvioMEIJunta = 0 ");

            //   Sql.AppendLine(" ORDER BY id ");

            //   Sql.AppendLine(" limit 1 ");

            //   using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.AppSettings["Main.ConnectionStringMYSQL"].ToString()))
            //   {
            //       using (MySqlCommand cmd = new MySqlCommand())
            //       {
            //           using (DataTable toReturn = new DataTable("S99"))
            //           {
            //               using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
            //               {
            //                   conn.Open();
            //                   cmd.Connection = conn;
            //                   cmd.CommandText = Sql.ToString();
            //                   cmd.CommandType = CommandType.Text;

            //                   adapter.Fill(toReturn);

            //                   return toReturn;

            //               }
            //           }

            //       }
        }


        [WebMethod]
        public void ProcessoEnvioS99JUCERJA()
        {

            DataTable dtCursor = RecuperaS99ReginJUCERJA();

            using (var client = new RJ_Servico_S99_V2.ServicoIntegracaoS99Client())
            {
                decimal pIdTabela = 0;
                //RJ_Servico_S99_V2.Resultado Resul = new RJ_Servico_S99_V2.Resultado();
                var cert = new X509Certificate2("D:\\certificado\\Certificado Juntas RFB\\2018\\RJ SRC\\pscs.pfx", "1234");
                client.ClientCredentials.ClientCertificate.Certificate = cert;
                foreach (DataRow pRow in dtCursor.Rows)
                {
                    try
                    {

                        pIdTabela = decimal.Parse(pRow["id"].ToString());

                        DataTable rt2 = dtCursor.Clone();
                        rt2.ImportRow(pRow);

                        DataSet result = new DataSet("S99");

                        result.Tables.Add(rt2);
                        GlobalV1.setNullToDefVals(ref result);

                        string pXML = result.GetXml();

                        pXML = pXML.Replace("<TAG_TABELA_REMOVER>", "");
                        pXML = pXML.Replace("</TAG_TABELA_REMOVER>", "");

                        //   client.

                        var Resul = client.EnviarS99(pXML);

                        T73308_DBE_FORMA_ATUACAO atualiza99 = new T73308_DBE_FORMA_ATUACAO();

                        if (Resul.Codigok__BackingField != "OK")
                        {
                            StringBuilder ResWs = new StringBuilder();
                            ResWs.Append("Resposta Ws: Codigo: " + Resul.Codigok__BackingField);
                            ResWs.AppendLine("Descrição: " + Resul.Mensagemk__BackingField);
                            atualiza99.UpdateStatusEnviaMEIJUCERJAServico99(pIdTabela, 9, ResWs.ToString());
                        }
                        else
                        {
                            atualiza99.UpdateStatusEnviaMEIJUCERJAServico99(pIdTabela, 1, "");
                        }
                    }
                    catch (Exception ex)
                    {
                        T73308_DBE_FORMA_ATUACAO atualiza99 = new T73308_DBE_FORMA_ATUACAO();
                        atualiza99.UpdateStatusEnviaMEIJUCERJAServico99(pIdTabela, 9, "Erro metodo: StackTrace: " + ex.StackTrace + " Message: " + ex.Message);

                    }
                }

            }

            return;
            //Esto aqui foi feito para a versao 1, ou seja era outro webservices
            using (RJ_Servico_S99.SVC_IntegracaoRegin client = new RJ_Servico_S99.SVC_IntegracaoRegin())
            {
                RJ_Servico_S99.Resultado Resul = new RJ_Servico_S99.Resultado();
                client.Url = ConfigurationManager.AppSettings["pWebServiceJuntaSRC"].ToString(); //"RJ_Servico_S99";

                foreach (DataRow pRow in dtCursor.Rows)
                {

                    decimal pIdTabela = 0;
                    try
                    {

                        pIdTabela = decimal.Parse(pRow["id"].ToString());

                        DataTable rt2 = dtCursor.Clone();
                        rt2.ImportRow(pRow);

                        DataSet result = new DataSet("rowset");

                        result.Tables.Add(rt2);
                        GlobalV1.setNullToDefVals(ref result);

                        Resul = client.EnviarS99(result.GetXml());

                        T73308_DBE_FORMA_ATUACAO atualiza99 = new T73308_DBE_FORMA_ATUACAO();

                        if (Resul.Codigo != "OK")
                        {
                            StringBuilder ResWs = new StringBuilder();
                            ResWs.Append("Resposta Ws: Codigo: " + Resul.Codigo);
                            ResWs.AppendLine("Descrição: " + Resul.Mensagem);
                            atualiza99.UpdateStatusEnviaMEIJUCERJAServico99(pIdTabela, 9, ResWs.ToString());
                        }
                        else
                        {
                            atualiza99.UpdateStatusEnviaMEIJUCERJAServico99(pIdTabela, 1, "");
                        }
                    }

                    catch (Exception ex)
                    {
                        T73308_DBE_FORMA_ATUACAO atualiza99 = new T73308_DBE_FORMA_ATUACAO();
                        atualiza99.UpdateStatusEnviaMEIJUCERJAServico99(pIdTabela, 9, "Erro metodo: StackTrace: " + ex.StackTrace + " Message: " + ex.Message);

                    }
                }

            }
        }


        private void ProcessaDadosServico99V2()
        {

            decimal pIdTabela = 0;
            DataTable dtCursor = getCursorDados99();
            foreach (DataRow pRow in dtCursor.Rows)
            {
                try
                {
                    pIdTabela = decimal.Parse(pRow["id"].ToString());
                    WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();
                    WsServicesReginRFB.Retorno resulRegin35 = new WsServicesReginRFB.Retorno();
                    //WsServicesReginRFB.Retorno resulRegin09 = new WsServicesReginRFB.Retorno();
                    WsServicesReginRFB.Retorno resulRegin11 = new WsServicesReginRFB.Retorno();
                    T0101_RFB_PROCESSO_DEFERIDOS t01 = new T0101_RFB_PROCESSO_DEFERIDOS();
                    WsServicesReginRFB.redesim DadosDbe = new WsServicesReginRFB.redesim();

                    regin.Url = ConfigurationManager.AppSettings["urlServicesRFBRegin"].ToString();

                    string pRecibo = pRow["reciboSolicitacao"].ToString();
                    string pIdentificador = pRow["identificacaoSolicitacao"].ToString();
                    string pCnjMei = pRow["cnpjMei"].ToString();

                    #region Busca dados 35
                    if (pIdentificador != "" && pRecibo != "")
                    {
                        resulRegin35 = regin.ServiceWs35Soarquivo(pIdentificador, pRecibo);
                        t01.t0101_dbe = pRecibo + pIdentificador;
                        if (resulRegin35.status != "OK")
                        {
                            throw new Exception("Erro ao Buscar o 35 " + resulRegin35.descricao);
                        }
                        #region pega Informação ws35
                        if (resulRegin35.status == "OK")
                        {
                            DadosDbe = resulRegin35.oWs35Response.dadosRedesim;
                            t01.t0101_cnpj = DadosDbe.cnpj;
                            t01.t0101_nome_fantasia = GlobalV1.valNuloBranco(DadosDbe.fcpj.nomeFantasia);
                            t01.t0101_or = GlobalV1.valNuloBranco(DadosDbe.fcpj.codTipoOrgaoRegistro);
                            t01.t0101_tp_estab = Convert.ToInt32(DadosDbe.fcpj.inMatriz);

                            if (DadosDbe.fcpj.dataEvento.Length > 0)
                            {
                                string ano = DadosDbe.fcpj.dataEvento[0].Substring(0, 4);
                                string mes = DadosDbe.fcpj.dataEvento[0].Substring(4).Remove(2);
                                string dia = DadosDbe.fcpj.dataEvento[0].Substring(6);

                                t01.t0101_dt_deferimento = Convert.ToDateTime(dia + "/" + mes + "/" + ano);

                            }

                        }
                        #endregion
                    }
                    #endregion

                    #region Busca dados MEI s11 e S09
                    if (pCnjMei != "")
                    {
                        t01.t0101_indicador_mei = pRow["indicadorMei"].ToString();
                        t01.t0101_dt_evento_mei = DateTime.Parse(pRow["dataEventoMei"].ToString());
                        t01.t0101_cnpj_mei = pRow["cnpjMei"].ToString();

                        resulRegin11 = regin.ServiceWs11(t01.t0101_cnpj_mei);

                        if (resulRegin11.status != "OK")
                        {
                            throw new Exception("Erro ao Buscar ws 11 " + resulRegin11.descricao);
                        }
                        t01.t0101_xml_rfb = resulRegin11.XmlDBE;
                        t01.t0101_tp_estab = decimal.Parse(resulRegin11.oCNPJResponse.dadosCNPJ[0].indMatrizFilial);
                        //resulRegin09 = regin.ServiceWs09(resulRegin11.oCNPJResponse.dadosCNPJ[0].cpfRepresentante);

                        //if (resulRegin09.status != "OK")
                        //{
                        //    throw new Exception("Erro ao Buscar ws 09 " + resulRegin09.descricao);
                        //}

                        t01.t0101_xml_rfb_09 = "";// resulRegin09.XmlDBE;
                    }
                    #endregion

                    t01.t0101_uf = pRow["uf"].ToString();
                    t01.t0101_cod_serv_rfb = pRow["codServicoDisponivel"].ToString();
                    t01.t0101_id_rfb = pIdTabela;// "t01.t0101_id_rfb";
                    t01.t0101_data_recebido_rfb = DateTime.Parse(pRow["datainclusao"].ToString());


                    //t01.T0101_CNPJ_MATRIZ = string.IsNullOrEmpty(XmlWs11.retornoWS11Redesim.dadosCNPJ.cnpjMatriz) ? Processo.T0101_CNPJ : 
                    //t01.T0101_COD_MUN = Convert.ToInt32(string.IsNullOrEmpty(XmlWs11.retornoWS11Redesim.dadosCNPJ.endereco.codMunicipio) ? "0" : 
                    //t01.t0101_nat_juridica = XmlWs11.retornoWS11Redesim.dadosCNPJ.naturezaJuridica;

                    //Atualiza dados no MySql e Oracle
                    #region Atualiza dados
                    using (ConnectionProvider cpM = new ConnectionProvider())
                    {
                        cpM.OpenConnection();
                        cpM.BeginTransaction();
                        using (ConnectionProviderORACLE cp = new ConnectionProviderORACLE())
                        {
                            cp.OpenConnection();
                            cp.BeginTransaction();
                            t01.MainConnectionProvider = cp;

                            t01.UpdateV2();

                            if (DadosDbe.fcpj != null && DadosDbe.fcpj.codEvento != null)
                            {
                                foreach (string pCodEvento in DadosDbe.fcpj.codEvento)
                                {
                                    if (pCodEvento == "")
                                    {
                                        break;
                                    }
                                    t01.EventoUpdate(pIdTabela, pCodEvento);
                                }
                            }

                            T73308_DBE_FORMA_ATUACAO atualiza99 = new T73308_DBE_FORMA_ATUACAO();
                            atualiza99.MainConnectionProvider = cpM;
                            atualiza99.UpdateStatusT73309Servico99V2(pIdTabela, 1, "", t01.t0101_xml_rfb, t01.t0101_xml_rfb_09);


                            cp.CommitTransaction(); //Commit Oracle
                            cpM.CommitTransaction(); // Commit MySql
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    T73308_DBE_FORMA_ATUACAO atualiza99 = new T73308_DBE_FORMA_ATUACAO();
                    atualiza99.UpdateStatusT73309Servico99(pIdTabela, 9, ex.StackTrace + " " + ex.Message);
                }

            }
        }
        #endregion

        #region ProcessodeEnviode MEI para JUCERJA
        [WebMethod]
        public void ProcessoEnvioMEIJUCERJA()
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            DataTable dtCursor = getCursorDados99EnvioMeiJunta();
            foreach (DataRow pRow in dtCursor.Rows)
            {
                using (RJ_Servico_MEI.ServicoIntegracaoMEIClient client = new RJ_Servico_MEI.ServicoIntegracaoMEIClient())
                {
                    var cert = new X509Certificate2("D:\\Documentos Raul\\certificado\\Certificado Juntas RFB\\2018\\RJ SRC\\pscs.pfx", "1234");
                    client.ClientCredentials.ClientCertificate.Certificate = cert;

                    decimal pIdTabela = 0;
                    try
                    {
                        RJ_Servico_MEI.RequestMEI dMei = new RJ_Servico_MEI.RequestMEI();
                        RJ_Servico_MEI.RequestS35 s35 = new RJ_Servico_MEI.RequestS35();
                        RJ_Servico_MEI.ResponseMEI Resul = new RJ_Servico_MEI.ResponseMEI();
                        RJ_Servico_MEI.RequestOficio sof = new RJ_Servico_MEI.RequestOficio();

                        pIdTabela = decimal.Parse(pRow["id"].ToString());
                        dMei.IdRegin = pRow["id"].ToString();

                        dMei.CNPJ = pRow["cnpjMei"].ToString();

                        s35.NumeroIdentificacao = pRow["identificacaoSolicitacao"].ToString();
                        s35.NumeroRecibo = pRow["reciboSolicitacao"].ToString();
                        dMei.S35 = s35;

                        sof.CodigoConvenioAtoOficio = pRow["codigoConvenioAto"].ToString();
                        sof.NumeroAtoOficio = pRow["numeroAtoOficio"].ToString();
                        dMei.Oficio = sof;

                        //dMei.DataInclusaoCargaSe
                        dMei.DataEventoMEI = (DateTime)pRow["dataEventoMei"];// DateTime.Parse("30/05/2018");
                        dMei.DataInclusaoCargaServico99 = (DateTime)pRow["datainclusao"];// DateTime.Parse("30/05/2018");
                        dMei.IndicadorAcao = pRow["indicadorMei"].ToString();

                        dMei.UFSolicitacao = pRow["uf"].ToString();
                        dMei.XMLRfbS11 = pRow["XmlRFB11"].ToString();


                        Resul = client.AtualizarMEI(dMei);

                        T73308_DBE_FORMA_ATUACAO atualiza99 = new T73308_DBE_FORMA_ATUACAO();

                        if (Resul.Codigo != "00")
                        {
                            StringBuilder ResWs = new StringBuilder();
                            ResWs.Append("Resposta Ws: Codigo: " + Resul.Codigo);
                            ResWs.AppendLine("Descrição: " + Resul.Descricao);
                            ResWs.AppendLine("Observação: " + Resul.Observacao);

                            atualiza99.UpdateStatusEnviaMEIJUCERJAServico99(pIdTabela, 9, ResWs.ToString());
                        }
                        else
                        {
                            atualiza99.UpdateStatusEnviaMEIJUCERJAServico99(pIdTabela, 1, "");
                        }
                        //return Resul.Codigo + " Obs:" + Resul.Observacao + " Des:" + Resul.Descricao;
                    }

                    catch (Exception ex)
                    {
                        T73308_DBE_FORMA_ATUACAO atualiza99 = new T73308_DBE_FORMA_ATUACAO();
                        atualiza99.UpdateStatusEnviaMEIJUCERJAServico99(pIdTabela, 9, "Erro metodo: StackTrace: " + ex.StackTrace + " Message: " + ex.Message);

                    }
                }

            }
        }
        #endregion
        #endregion

        #region Homologa Processo Orgao de Registro Control de Qualidade
        private void HomologaXMLProcessoOrgaoRegistro(string pProtocolo)
        {
            if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
            {

                StringBuilder sqlU = new StringBuilder();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (SqlTransaction _conn = conn.BeginTransaction())
                    {
                        using (SqlCommand cmdToExecute = new SqlCommand())
                        {
                            cmdToExecute.Connection = _conn.Connection;
                            cmdToExecute.Transaction = _conn;

                            sqlU.AppendLine(" Update mac_log_carga_junta_homolog Set mlc_data_valida_xml = Getdate() Where mlc_protocolo = @v_PRA_PROTOCOLO ");

                            cmdToExecute.Parameters.Add(new SqlParameter("v_PRA_PROTOCOLO", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                            cmdToExecute.CommandText = sqlU.ToString();
                            cmdToExecute.CommandType = CommandType.Text;
                            cmdToExecute.ExecuteNonQuery();


                            sqlU = new StringBuilder();
                            cmdToExecute.Parameters.Clear();
                            sqlU.AppendLine(" update psc_protocolo set pro_status = 1 where pro_protocolo = @v_PRA_PROTOCOLO ");

                            cmdToExecute.Parameters.Add(new SqlParameter("v_PRA_PROTOCOLO", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                            cmdToExecute.CommandText = sqlU.ToString();
                            cmdToExecute.CommandType = CommandType.Text;
                            cmdToExecute.ExecuteNonQuery();

                            _conn.Commit();
                        }
                    }
                }
            }
            else
            {
                StringBuilder sqlU = new StringBuilder();
                using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                {
                    conn.Open();
                    using (OracleTransaction _conn = conn.BeginTransaction())
                    {
                        using (OracleCommand cmdToExecute = new OracleCommand())
                        {
                            cmdToExecute.Connection = _conn.Connection;
                            cmdToExecute.Transaction = _conn;
                            sqlU.AppendLine(" Update mac_log_carga_junta_homolog Set mlc_data_valida_xml = Sysdate Where mlc_protocolo = :v_PRA_PROTOCOLO ");

                            cmdToExecute.Parameters.Add(new OracleParameter("v_PRA_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                            cmdToExecute.CommandText = sqlU.ToString();
                            cmdToExecute.CommandType = CommandType.Text;
                            cmdToExecute.ExecuteNonQuery();

                            sqlU = new StringBuilder();
                            cmdToExecute.Parameters.Clear();
                            sqlU.AppendLine(" update psc_protocolo set pro_status = 1 where pro_protocolo = :v_PRA_PROTOCOLO ");

                            cmdToExecute.Parameters.Add(new OracleParameter("v_PRA_PROTOCOLO", OracleType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                            cmdToExecute.CommandText = sqlU.ToString();
                            cmdToExecute.CommandType = CommandType.Text;
                            cmdToExecute.ExecuteNonQuery();


                            _conn.Commit();
                        }
                    }
                }
            }
        }
        private DataTable getProtocolo(string pProtocolo)
        {
            StringBuilder Sql = new StringBuilder();
            try
            {
                Sql.Append(" Select * ");
                Sql.Append(" from   psc_protocolo ");
                Sql.Append(" where	pro_protocolo = '" + pProtocolo + "'");

                if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
                {
                    using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = Sql.ToString();
                            cmd.CommandType = CommandType.Text;
                            DataTable toReturn = new DataTable("psc_protocolo");

                            cmd.Connection = _conn;
                            cmd.Connection.Open();

                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                adapter.Fill(toReturn);
                                return toReturn;
                            }
                        }
                    }
                }
                else
                {
                    using (OracleConnection _conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                    {
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            cmd.CommandText = Sql.ToString();
                            cmd.CommandType = CommandType.Text;
                            DataTable toReturn = new DataTable("psc_protocolo");

                            cmd.Connection = _conn;
                            cmd.Connection.Open();

                            using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                            {
                                adapter.Fill(toReturn);
                                return toReturn;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public Retorno HomologaProcessoOrgaoRegistroCQ(string pProtocolo, string XML)
        {


            Retorno pResul = new Retorno();

            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                pResul.status = "OK";
                pResul.codretorno = "00";

                DataTable dtProt = getProtocolo(pProtocolo);

                if (dtProt.Rows.Count == 0)
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "99";
                    pResul.descricao = "Registro não encontrado";
                    return pResul;
                }

                if (dtProt.Rows[0]["PRO_STATUS"].ToString() != "-9")
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "98";
                    pResul.descricao = "Registro não esta com pendência";
                    return pResul;
                }
                dHelperQuery c = new dHelperQuery();

                HomologaXMLProcessoOrgaoRegistro(pProtocolo);
            }
            catch (Exception ex)
            {
                pResul.status = "NOK";
                pResul.codretorno = "01";
                pResul.descricao = ex.Message;
                return pResul;
            }

            return pResul;
        }


        [WebMethod]
        public Retorno HomologaProcessoControlQualidade(string pProtocolo, string pCNPJOrgaoRegistro, string pCPFRespCorrecao, string pObservacao, string XML)
        {


            Retorno pResul = new Retorno();

            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                pResul.status = "OK";
                pResul.codretorno = "00";

                if (pCPFRespCorrecao.Trim().Length != 11)
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "10";
                    pResul.descricao = "CPF Invalido";
                    return pResul;
                }

                dHelperQuery c = new dHelperQuery();

                DataTable dt = c.getControlQualidade(pProtocolo, pCNPJOrgaoRegistro);

                if (dt.Rows.Count == 0)
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "99";
                    pResul.descricao = "Registro não encontrado.";
                    return pResul;
                }

                if (dt.Rows[0]["PCQ_STATUS"].ToString() != "1")
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "98";
                    pResul.descricao = "Registro não esta com pendência";
                    return pResul;
                }

                string urlWs = dHelperQuery.getUrlOR(pCNPJOrgaoRegistro);
                //urlWs = "http://10.1.10.50/WsRFBReginprod/ServiceReginRFB.asmx";

                if (urlWs == "")
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "97";
                    pResul.descricao = "Url não configurada para o CNPJ " + pCNPJOrgaoRegistro;
                    return pResul;
                }

                /*
                    Atualiza primeiro os dados no Orgao de Registro o status do protocolo e a data de validação do XML
                 */
                WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();
                WsServicesReginRFB.Retorno pResultOG = new WsServicesReginRFB.Retorno();
                regin.Url = urlWs;
                pResultOG = regin.HomologaProcessoOrgaoRegistroCQ(pProtocolo, "");

                if (pResultOG.status != "OK")
                {
                    pResul.status = pResultOG.status;
                    pResul.codretorno = pResultOG.codretorno;
                    pResul.descricao = "WSOR: " + pResultOG.descricao;
                    return pResul;
                }

                /*
                    Atualiza os dados do sistema de contele de qualidade, isso depois da atualizar os dados do Orgao de Registro
                 */
                c.HomologControlQualidade(pProtocolo, pCNPJOrgaoRegistro, pCPFRespCorrecao, pObservacao);

            }
            catch (Exception ex)
            {
                pResul.status = "NOK";
                pResul.codretorno = "01";
                pResul.descricao = "WSOR: " + ex.Message;
                return pResul;
            }

            return pResul;
        }

        [WebMethod]
        public Retorno ReProcessoControlQualidadeOrgaoRegistro(string pProtocolo, string pCNPJOrgaoRegistro, string pCPFRespCorrecao, string pObservacao, string XML)
        {
            Retorno pResul = new Retorno();

            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                pResul.status = "OK";
                pResul.codretorno = "00";

                if (ConfigurationManager.AppSettings["TipoBanco"].ToUpper() == "SQLSERVER")
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "ReprocessaProtocoloRegin";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("Protocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
                    {
                        conn.Open();
                        using (OracleCommand cmd = new OracleCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "ReprocessaProtocolo";
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new OracleParameter("pProtocolo", OracleType.VarChar, 20, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, pProtocolo));

                            cmd.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                pResul.status = "NOK";
                pResul.codretorno = "01";
                pResul.descricao = "WSOR: " + ex.Message;
                return pResul;
            }
            return pResul;
        }

        [WebMethod]
        public Retorno ReProcessoControlQualidadeOR(string pProtocolo, string pCNPJOrgaoRegistro, string pCPFRespCorrecao, string pObservacao, string XML)
        {
            Retorno pResul = new Retorno();

            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                pResul.status = "OK";
                pResul.codretorno = "00";

                if (pCPFRespCorrecao.Trim().Length != 11)
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "10";
                    pResul.descricao = "CPF Invalido: " + pCPFRespCorrecao;
                    return pResul;
                }

                dHelperQuery c = new dHelperQuery();

                DataTable dt = c.getControlQualidade(pProtocolo, pCNPJOrgaoRegistro);

                if (dt.Rows.Count == 0)
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "99";
                    pResul.descricao = "Registro não encontrado";
                    return pResul;
                }

                if (dt.Rows[0]["PCQ_STATUS"].ToString() != "1")
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "98";
                    pResul.descricao = "Registro não esta com pendência";
                    return pResul;
                }

                string urlWs = dHelperQuery.getUrlOR(pCNPJOrgaoRegistro);
                //urlWs = "http://10.1.10.50/WsRFBReginprod/ServiceReginRFB.asmx";

                if (urlWs == "")
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "97";
                    pResul.descricao = "Url não configurada para o CNPJ " + pCNPJOrgaoRegistro;
                    return pResul;
                }

                /*
                    Atualiza primeiro os dados no Orgao de Registro o status do protocolo e a data de validação do XML
                 */
                WsServicesReginRFB.ServiceReginRFB regin = new WsServicesReginRFB.ServiceReginRFB();
                WsServicesReginRFB.Retorno pResultOG = new WsServicesReginRFB.Retorno();
                regin.Url = urlWs;
                pResultOG = regin.ReProcessoControlQualidadeOrgaoRegistro(pProtocolo, pCNPJOrgaoRegistro, pCPFRespCorrecao, pObservacao, XML);

                if (pResultOG.status != "OK")
                {
                    pResul.status = pResultOG.status;
                    pResul.codretorno = pResultOG.codretorno;
                    pResul.descricao = "WSOR: " + pResultOG.descricao;
                    return pResul;
                }

                c.ReprocessoControlQualidade(pProtocolo, pCNPJOrgaoRegistro, pCPFRespCorrecao, pObservacao);

            }
            catch (Exception ex)
            {
                pResul.status = "NOK";
                pResul.codretorno = "01";
                pResul.descricao = ex.Message;
                return pResul;
            }

            return pResul;
        }
        #endregion

        #region GetNumeroUnico Regin
        [WebMethod]
        public Retorno getS99NumeroUnicoRegin(string pNumeroUnico)
        {
            string pDBE = "";
            string pCNPJMEI = "";
            string pServico = "";
            Retorno pResul = new Retorno();
            try
            {
                AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

                pResul.status = "OK";

                if (pNumeroUnico == "")
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pNumeroUnico tem que ser preenchidos";
                    return pResul;
                }

                if (pDBE == "" && pCNPJMEI == "" && pNumeroUnico == "")
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pDBE, pCNPJMEI ou pNumeroUnico tem que ser preenchidos";
                    return pResul;
                }

                if (pDBE != "" && pDBE.Length != 24)
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pDBE invalido";
                    return pResul;
                }

                if (pCNPJMEI != "" && pCNPJMEI.Length != 14)
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pCNPJMEI invalido";
                    return pResul;
                }

                if (pNumeroUnico != "" && pNumeroUnico.Length != 13)
                {
                    pResul.status = "NOK";
                    pResul.descricao = "Parametros pNumeroUnico invalido";
                    return pResul;
                }



                string Recibo = "";
                string Identificacao = "";
                if (pDBE != "")
                {
                    Recibo = pDBE.Substring(0, 10);
                    Identificacao = pDBE.Substring(10, 14);
                }

                ServiceConsultaS99 cnu = new ServiceConsultaS99();

                DataTable toReturn = cnu.RecuperaS99Regin(Recibo, Identificacao, pCNPJMEI, pServico, pNumeroUnico);

                DataSet result = new DataSet("rowset");

                if (toReturn.Rows.Count == 0)
                {
                    pResul.status = "NOK";
                    pResul.codretorno = "99";
                    pResul.descricao = "Registros não encontrados";
                    return pResul;
                }

                result.Tables.Add(toReturn);
                cnu.setNullToDefVals(ref result);

                pResul.Recibo = toReturn.Rows[0]["reciboSolicitacao"].ToString();
                pResul.Identificacao = toReturn.Rows[0]["identificacaoSolicitacao"].ToString();
                pResul.XmlDBE = result.GetXml();

                return pResul;
            }
            catch (Exception ex)
            {
                pResul.status = "NOK";
                pResul.descricao = ex.Message;
                return pResul;
            }
        }

        #endregion

        #region Varrega dados DBE Requerimento
        [WebMethod]
        public DataSet CarregaDadosComparacaoDBE(string _CodigoDBE)
        {
            DataSet Ds = new DataSet();
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            DataTable result = new DataTable();
            int IdControlDBE = 0;

            result = dHelperQuery.getDadosDBEControl(_CodigoDBE);

            if (result != null && result.Rows.Count > 0)
            {
                IdControlDBE = int.Parse(result.Rows[0]["t73300_id_control"].ToString());

                //DadosDBEControl
                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBEFCPJ(IdControlDBE);

                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBECNAESecundaria(IdControlDBE);
                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBEEventos(IdControlDBE);
                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBEContador(IdControlDBE);
                Ds.Tables.Add(result);

                result = dHelperQuery.getDadosDBEQSA(IdControlDBE);
                Ds.Tables.Add(result);


            }

            return Ds;

        }
        #endregion

        #region Processo para Enviar Viabilidade Resultado RFB01 JUCERJA
        [WebMethod]
        public void ProcessoEnvioS01JUCERJA()
        {
            AcessoPorIPService.Instancia.Verificar(ConfigurationManager.AppSettings["pCNPJInstituicaoFixoJUNTA"].ToString());

            DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
            using (SqlConnection _conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"]))
            {
                using (SqlCommand cmdToExecute = new SqlCommand())
                {
                    StringBuilder Sql = new StringBuilder();
                    Sql.AppendLine(@"select  x.VPX_COD_PROTOCOLO Protocolo,
		                                dbo.fnGetStatusViabilidade(x.VPX_COD_PROTOCOLO) StatusViabilidade, 
		                                v.VPV_STATUS_ENV_RFB,
		                                v.VPV_STATUS_PROC_RFB StatusEntregueRFB, 
		                                VPX_XML_ENVIADO XmlEnviado, 
		                                x.VPX_XML_ENVIADO_ERRO XMLErro, 
		                                x.VPX_XML_RESPONSE Response
                                from	VIA_PROTOCOLO_VIAB v, VIA_PRO_XMLRFB x
                                where	v.VPV_COD_PROTOCOLO = x.VPX_COD_PROTOCOLO
                                and		v.VPV_VIAB_BALCAOUNICO = '1'
                                and		x.VPX_STATUS_ENVIO_JUNTA = 1
                              --  and		x.VPX_COD_PROTOCOLO = 'RJB2100000067'
                                ");

                    //DataTable toReturn = new DataTable("WBS_CONTROL_ENVIO");
                    cmdToExecute.CommandType = CommandType.Text;
                    cmdToExecute.CommandText = Sql.ToString();

                    //cmdToExecute.Parameters.Add(new OracleParameter("pCursor", OracleType.Cursor, 0, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, null));

                    _conn.Open();

                    cmdToExecute.Connection = _conn;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute))
                    {
                        adapter.Fill(toReturn);
                    }
                }
            }

            string pProtocolo = "";
            for (int a = 0; a < toReturn.Rows.Count; a++)
            {
                try
                {
                    RJ_ServiceEnvS01.SVC_IntegracaoRegin c = new RJ_ServiceEnvS01.SVC_IntegracaoRegin();
                    RJ_ServiceEnvS01.DadosS01 s01 = new RJ_ServiceEnvS01.DadosS01();
                    RJ_ServiceEnvS01.RespostaBalcaoUnico result = new RJ_ServiceEnvS01.RespostaBalcaoUnico();

                    decimal StatusEnviado = 3;
                    string ErroEnvio = "";

                    pProtocolo = toReturn.Rows[a]["Protocolo"].ToString().Trim();
                    string DescricaoRetornoS01 = toReturn.Rows[a]["Response"].ToString().Trim();
                    string RequestS01 = toReturn.Rows[a]["XMLErro"].ToString().Trim();
                    string ResultadoS01 = "NOK";
                    string StatusViabilidade = toReturn.Rows[a]["StatusViabilidade"].ToString().Trim();
                    string StatusEntregueRFB = toReturn.Rows[a]["StatusEntregueRFB"].ToString().Trim();

                    if (StatusEntregueRFB == "3" || StatusEntregueRFB == "-3")
                    {
                        ResultadoS01 = "OK";
                        RequestS01 = toReturn.Rows[a]["XmlEnviado"].ToString().Trim();
                    }

                    s01.DescricaoRetornoS01 = DescricaoRetornoS01;
                    s01.RequestS01 = RequestS01;
                    s01.ResultadoS01 = ResultadoS01;


                    result = c.InformarResultadoViabilidade(pProtocolo, StatusViabilidade, s01);

                    if (result.Status != "OK")
                    {
                        StatusEnviado = 9;
                        ErroEnvio = result.Texto;
                    }

                    UpdateVIA_PRO_XMLRFB(pProtocolo, StatusEnviado, ErroEnvio);
                }
                catch (Exception ex)
                {
                    UpdateVIA_PRO_XMLRFB(pProtocolo, 9, "Erro no envio catch " + ex.Message);
                }
            }

        }
        private void UpdateVIA_PRO_XMLRFB(string pProtocolo, decimal pStatus, string pErro)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Main.ConnectionString"].ToString()))
            {
                conn.Open();
                using (SqlTransaction _conn = conn.BeginTransaction())
                {
                    using (SqlCommand cmdToExecute = new SqlCommand())
                    {
                        cmdToExecute.Connection = _conn.Connection;
                        cmdToExecute.Transaction = _conn;

                        StringBuilder sqlU = new StringBuilder(" update VIA_PRO_XMLRFB set VPX_ERRO_ENVIO_JUNTA = @pErro, VPX_STATUS_ENVIO_JUNTA = @pStatus where VPX_COD_PROTOCOLO = @pProtocolo ");

                        cmdToExecute.Parameters.Add(new SqlParameter("pErro", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pErro));
                        cmdToExecute.Parameters.Add(new SqlParameter("pStatus", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pStatus));
                        cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                        cmdToExecute.CommandText = sqlU.ToString();
                        cmdToExecute.CommandType = CommandType.Text;
                        cmdToExecute.ExecuteNonQuery();

                        if (pStatus == 3)
                        {
                            cmdToExecute.Parameters.Clear();
                            sqlU = new StringBuilder(" update VIA_PRO_XMLRFB set VPX_DATA_ENVIO_JUNTA = @DataEnvio where VPX_COD_PROTOCOLO = @pProtocolo ");

                            cmdToExecute.Parameters.Add(new SqlParameter("DataEnvio", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DateTime.Now));
                            cmdToExecute.Parameters.Add(new SqlParameter("pProtocolo", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, pProtocolo));

                            cmdToExecute.CommandText = sqlU.ToString();
                            cmdToExecute.ExecuteNonQuery();

                        }

                        _conn.Commit();
                    }
                }


            }
        }
        #endregion


    }

}
