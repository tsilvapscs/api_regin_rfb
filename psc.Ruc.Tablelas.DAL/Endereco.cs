using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace psc.Ruc.Tablelas.Helper
{
    public class Endereco
    {
        #region Variaveis
        private string _logradouro = "";
        private string _numero = "S/N";
        private string _pais = "105";
        private string _tipLogradoro = "";
        private string _bairro = "";
        private string _uf = "";
        private string _cep = "";
        private string _complemento = "";
        private string _codigo_municipio = "";
        #endregion

        #region properties
        public string Cep
        {
            get { return _cep; }
            set { _cep = value; }
        }

        public string Complemento
        {
            get { return _complemento; }
            set { _complemento = value; }
        }


        public string Logradouro
        {
            get { return _logradouro; }
            set { _logradouro = value; }
        }
        public string Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        public string Pais
        {
            get { return _pais; }
            set { _pais = value; }
        }
        public string TipLogradoro
        {
            get { return _tipLogradoro; }
            set { _tipLogradoro = value; }
        }
        public string Bairro
        {
            get { return _bairro; }
            set { _bairro = value; }
        }
        public string Uf
        {
            get { return _uf; }
            set { _uf = value; }
        }

        public string Codigo_municipio
        {
            get { return _codigo_municipio; }
            set { _codigo_municipio = value; }
        }
        #endregion

        public static String trimAll(string text)
        {
            String pString = text.Trim();
            while (pString.Contains("  "))
            {
                pString = pString.Replace("  ", " ");
            }
            return pString;
        }

        public void TrataEndereco(ref Endereco ende, DataTable DtTipoLogra)
        {
            string tipoRFB = "";// DadosViabi["codTipoLogradouro"].ToString().Trim();
            string tipoRFBEncontradoRFB = "";



            if (ende.Numero == "")
            {
                _numero = "S/N";
            }

            ende.Bairro = trimAll(ende.Bairro);
            ende.Complemento = trimAll(ende.Complemento);
            ende.Logradouro = trimAll(ende.Logradouro);

            bool TpoDeLogradouroDoLogradouro = false;
            if (ende.TipLogradoro == "")
            {
                string[] tipos = Logradouro.Split(' ');
                tipoRFB = tipos[0].Trim();
                if (tipoRFB != "")
                    TpoDeLogradouroDoLogradouro = true;
            }

            if (TpoDeLogradouroDoLogradouro)
            {
                if (tipoRFB.Trim() != "")
                {
                    foreach (DataRow TipoLogradouro in DtTipoLogra.Rows)
                    {
                        if (TipoLogradouro["cod_rfb"].ToString().Trim().ToUpper() == tipoRFB.Trim().ToUpper()
                            || TipoLogradouro["cod_regin"].ToString().Trim().ToUpper() == tipoRFB.Trim().ToUpper())
                        {
                            tipoRFBEncontradoRFB = TipoLogradouro["COD_RFB"].ToString().Trim().ToUpper();
                            //return;
                            break;
                        }
                    }
                }
            }

            if (TpoDeLogradouroDoLogradouro)
            {
                if (tipoRFBEncontradoRFB == "")
                {
                    ende.TipLogradoro = "NC";
                    TpoDeLogradouroDoLogradouro = false;
                }
                else
                {
                    ende.TipLogradoro = tipoRFBEncontradoRFB;
                }
            }

            if (TpoDeLogradouroDoLogradouro)
                ende.Logradouro = ende.Logradouro.Substring(tipoRFB.Trim().Length).Trim();

            ende.Codigo_municipio += psc.Framework.General.CalculateVerificationDigit(ende.Codigo_municipio, 11).ToString();

            if (ende.Uf != "EX")
            {
                ende.Pais = "105";
            }

            if (ende.Pais == "" ||
               ende.Pais.Length < 2 ||
               ende.Pais == "000" ||
               ende.Pais == "0" ||
               ende.Pais == "00")
            {
                if (ende.Uf != "EX")
                    ende.Pais = "105";
            }
                                            
        }

    }
}
