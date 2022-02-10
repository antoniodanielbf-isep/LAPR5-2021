using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Relacoes
{
    public class TagRelacao : IValueObject
    {
        public TagRelacao()
        {
        }

        public TagRelacao(string tagsRelacao)
        {
            if (tagsRelacao.Length > 40 || tagsRelacao.Length < 1 || string.IsNullOrEmpty(tagsRelacao.Trim()))
                throw new BusinessRuleValidationException("Tags Invalidas");

            TagsRelacao = tagsRelacao.Trim();
        }

        private string TagsRelacao { get; }

        public TagRelacao ValueOf(string tagsRelacao)
        {
            return new TagRelacao(tagsRelacao);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (TagRelacao) obj;

            return TagsRelacao.Equals(that.TagsRelacao);
        }

        public override string ToString()
        {
            return $"{TagsRelacao}";
        }
    }
}