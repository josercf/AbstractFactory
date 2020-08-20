using System;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Selecione a opção para pagamento:");
            Console.WriteLine("1 - Crédito");
            Console.WriteLine("2 - Débito");

            var opcao = int.Parse(Console.ReadLine());

            ICartao cartao = FabricaDeMeioPagamento.CriarInstancia(opcao);
            bool resultado = cartao.Debitar(150.00M);

            Console.WriteLine($"Resultado da operação: {resultado}");
        }
    }

    public static class FabricaDeMeioPagamento
    {
        public static ICartao CriarInstancia(int tipoCartao)
        {
            ICartao cartao;

            if (tipoCartao == 1)
                cartao = new CartaoDeCredito();
            else
                cartao = new CartaoDebito();

            return cartao;
        }
    }

    public interface ICartao
    {
        bool Debitar(decimal valor);
    }

    public class CartaoDeCredito : ICartao
    {
        public decimal ValorFatuta { get; private set; }
        public const decimal Limite = 2500;

        public bool Debitar(decimal valor)
        {
            if ((valor + ValorFatuta) <= Limite)
            {
                ValorFatuta += valor;
                return true;
            }

            return false;
        }
    }

    public class CartaoDebito : ICartao
    {
        private decimal _saldo;
        public bool Debitar(decimal valor)
        {
            if (valor >= _saldo)
            {
                _saldo -= valor;
                return true;
            }

            return false;
        }
    }
}
