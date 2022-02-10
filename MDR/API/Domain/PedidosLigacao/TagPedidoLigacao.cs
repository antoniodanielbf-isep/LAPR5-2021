using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.PedidosLigacao
{
    public class TagPedidoLigacao : IValueObject
    {
        public TagPedidoLigacao()
        {
        }

        public TagPedidoLigacao(string tagsPedido)
        {
            if (tagsPedido.Length > 40
                || tagsPedido.Length < 1
                || string.IsNullOrEmpty(tagsPedido.Trim()))
                throw new BusinessRuleValidationException("Tags Invalidas");

            TagsPedido = tagsPedido.Trim();
        }

        private string TagsPedido { get; }

        public TagPedidoLigacao ValueOf(string tagsPedido)
        {
            return new TagPedidoLigacao(tagsPedido);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (TagPedidoLigacao) obj;

            return TagsPedido.Equals(that.TagsPedido);
        }

        public override string ToString()
        {
            return $"{TagsPedido}";
        }
    }
}