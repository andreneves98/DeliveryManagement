using System;
private static Random random = new Random();

namespace Encomendas
{
    [Serializable()]
    public class Encomenda
    {
        private String _IDEncomenda;
        private String _morada;
        private String _obs;
        private int _preco_total;
        private String _ID_produto;
        private int _ref_pagamento;
        private int _nr_reg_cliente;
        private int _nr_reg_motorista;
        private String _status;

	    public Encomenda(String morada)
	    {
            this.IDEncomenda = RandomString(5);
            this.Morada = morada;
	    }

        public String IDEncomenda
        {
            get { return _IDEncomenda; }
            set { _IDEncomenda = value; }
        }

        public String Morada
        {
            get { return _morada; }
            set {
                if (value == null | String.IsNullOrEmpty(value)) {
                    throw new Exception("Morada não pode estar vazia!");
                    return;
                }
                _morada = value;
            }
        }

        public String Obs
        {
            get { return _obs; }
            set { _obs = value; }
        }

        public int PrecoTotal
        {
            get { return _preco_total; }
            set { if(value < 0) {
                    throw new Exception("O preço total não pode ser negativo!");
                    return;
                }
                _preco_total = value;
            }
        }

        public String IDProduto
        {
            get { return _ID_produto; }
            set { _ID_produto = value; }
        }

        public int RefPagamento
        {
            get { return _ref_pagamento; }
            set { _ref_pagamento = value; }
        }

        public int NumRegCliente
        {
            get { return _nr_reg_cliente; }
            set { if(value <= 0) {
                    throw new Exception("Número de registo inválido!");
                    return;
                }
                _nr_reg_cliente = value;
            }
        }

        public int NumRegMotorista
        {
            get { return _nr_reg_motorista; }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Número de registo inválido!");
                    return;
                }
                _nr_reg_motorista = value;
            }
        }

        public String Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public override string ToString()
        {
            return "[ID: " + _IDEncomenda + " <> Cliente: " + _nr_reg_cliente + " <> Morada: " + _morada + "]"; 
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

