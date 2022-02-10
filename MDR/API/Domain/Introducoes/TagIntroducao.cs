using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Introducoes
{
    public class TagIntroducao : IValueObject
    {
        public TagIntroducao()
        {
        }

        public TagIntroducao(string tag)
        {
            if (tag.Length > 40
                || tag.Length < 1
                || string.IsNullOrEmpty(tag.Trim()))
                throw new BusinessRuleValidationException("Tags Invalidas");

            TagIntro = tag.Trim();
        }

        private string TagIntro { get; }

        public TagIntroducao ValueOf(string tag)
        {
            return new TagIntroducao(tag);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (TagIntroducao) obj;

            return TagIntro.Equals(that.TagIntro);
        }

        public override string ToString()
        {
            return $"{TagIntro}";
        }
    }
}