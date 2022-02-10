using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.PedidosLigacao
{
    public class ForcaLigacaoPedido : IValueObject
    {
        public ForcaLigacaoPedido(int forcaLigacaoRelacao)
        {
            if (forcaLigacaoRelacao < 1 || forcaLigacaoRelacao > 100)
                throw new BusinessRuleValidationException("ForcaLigacao Invalida");

            ForcaPedido = forcaLigacaoRelacao;
        }

        protected ForcaLigacaoPedido()
        {
        }

        public int ForcaPedido { get; }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (ForcaLigacaoPedido) obj;

            return ForcaPedido.Equals(that.ForcaPedido);
        }

        public override string ToString()
        {
            return $"{ForcaPedido}";
        }
    }
}