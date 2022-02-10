using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.PedidosIntroducao
{
    public class ForcaLigacaoPedidoIntroducao : IValueObject
    {
        public ForcaLigacaoPedidoIntroducao()
        {
        }

        public ForcaLigacaoPedidoIntroducao(int forcaLigacao)
        {
            if (forcaLigacao < 1 || forcaLigacao > 100)
                throw new BusinessRuleValidationException("ForcaLigacao Invalida");

            ForcaPedidoIntroducao = forcaLigacao;
        }

        public int ForcaPedidoIntroducao { get; }

        public ForcaLigacaoPedidoIntroducao ValueOf(int forcaLigacao)
        {
            return new ForcaLigacaoPedidoIntroducao(forcaLigacao);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (ForcaLigacaoPedidoIntroducao) obj;

            return ForcaPedidoIntroducao.Equals(that.ForcaPedidoIntroducao);
        }

        public override string ToString()
        {
            return $"{ForcaPedidoIntroducao}";
        }

        public int getForca()
        {
            return ForcaPedidoIntroducao;
        }
    }
}