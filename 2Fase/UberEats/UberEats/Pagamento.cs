using System;

[Serializable()]
public class Pagamento
{
    private double _preco;
    private string _morada_fat;
    private string _metodo;
    private int _conta_banc;
    private int _referencia;
    private string _id_encomenda;

    public Pagamento()
	{
        // por fazer para já
	}

    public double Preco
    {
        get { return _preco; }
        set
        {   if (value < 0)
            {
                throw new Exception("O preço não pode ser negativo.");
                return;
            }
            _preco = value;
        }
    }

    public string MoradaFat
    {
        get { return _morada_fat; }
        set { _morada_fat = value; }
    }

    public string Metodo
    {
        get { return _metodo; }
        set { _metodo = value; }
    }

    public int ContaBanc
    {
        get { return _conta_banc; }
        set
        {
            if (value.ToString().Length != 13)
            {
                throw new Exception("Conta bancária inválida.");
                return;
            }
            _conta_banc = value;
        }
    }

    public int Referencia
    {
        get { return _referencia; }
        set { _referencia = value; }
    }

    public string IDEncomenda
    {
        get { return _id_encomenda; }
        set { _id_encomenda = value; }
    }

    public override string ToString()
    {
        return "Referência: " + _referencia + " <> IDEncomenda: " + _id_encomenda + " <> Preço: " + _preco + "]";
    }
}
