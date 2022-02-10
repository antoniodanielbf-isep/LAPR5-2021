using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.PedidosIntroducao
{
    public class DescricaoPedidoIntroducao : IValueObject
    {
        public DescricaoPedidoIntroducao()
        {
        }

        public DescricaoPedidoIntroducao(string value)
        {
            if (value.Trim().Equals("")) throw new BusinessRuleValidationException("Descricao Invalida");
            DescPedidoIntroducao = value;
        }

        private string DescPedidoIntroducao { get; }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (DescricaoPedidoIntroducao) obj;

            return DescPedidoIntroducao.Equals(that.DescPedidoIntroducao);
        }

        public override string ToString()
        {
            return $"{DescPedidoIntroducao}";
        }
    }
}