using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Utilizadores
{
    public class Tag : IValueObject
    {
        public Tag()
        {
        }

        [JsonConstructor]
        public Tag(string tag)
        {
            if (string.IsNullOrEmpty(tag.Trim())) throw new BusinessRuleValidationException("TAG Invalida");

            TagText = tag.Trim();
        }

        private string TagText { get; }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (Tag) obj;

            return TagText.ToUpper().Equals(that.TagText.ToUpper());
        }

        public override string ToString()
        {
            return $"{TagText}";
        }
    }
}