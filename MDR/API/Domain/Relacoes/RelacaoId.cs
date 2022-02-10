using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Relacoes
{
    public class RelacaoId : EntityId
    {
        [JsonConstructor]
        public RelacaoId(string value) : base(value)
        {
            if (value.Trim().Equals("")) throw new BusinessRuleValidationException("Relacao Invalida");
            Id = value.Trim();
        }

        public string Id { get; }

        override
            protected object createFromString(string text)
        {
            return text;
        }

        override
            public string AsString()
        {
            var obj = ObjValue;
            return obj.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (RelacaoId) obj;

            return Id.Equals(that.Id);
        }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}