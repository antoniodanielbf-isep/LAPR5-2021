using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.PedidosIntroducao
{
    public class TagPedidoIntroducao : IValueObject
    {
        public TagPedidoIntroducao()
        {
        }

        public TagPedidoIntroducao(string tags)
        {
            if (tags.Length > 40 || tags.Length < 1 || string.IsNullOrEmpty(tags.Trim()))
                throw new BusinessRuleValidationException("Tags Invalidas");

            TagPIntroducao = tags.Trim();
        }

        private string TagPIntroducao { get; }

        public TagPedidoIntroducao ValueOf(string tagsPedido)
        {
            return new TagPedidoIntroducao(tagsPedido);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (TagPedidoIntroducao) obj;

            return TagPIntroducao.Equals(that.TagPIntroducao);
        }

        public override string ToString()
        {
            return $"{TagPIntroducao}";
        }
    }
}