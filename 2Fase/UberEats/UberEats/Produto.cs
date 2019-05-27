using System;
private static Random random = new Random();

namespace Produtos
{
    [Serializable()]
    public class Produto
    {
        private String _ID;
        private String _nome;
        private String _descricao;
        private int _preco;
        

        public Produto(String nome)
        {
            this.ID = RandomString(5);
            this.Nome = nome;
        }

        public String ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public String Nome
        {
            get { return _nome; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("Nome não pode estar vazio!");
                    return;
                }
                _nome = value;
            }
        }

        public String Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        public int Preco
        {
            get { return _preco; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("O preço não pode ser negativo!");
                    return;
                }
                _preco = value;
            }
        }

        public override string ToString()
        {
            return "[ID: " + _ID + " <>  Nome: " + _nome + " <> Preço: " + _preco + "]";
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

