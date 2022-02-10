using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Missoes
{
    public class MissaoId : EntityId
    {
        [JsonConstructor]
        public MissaoId(string value) : base(value)
        {
            IdMissao = value;
        }

        private string IdMissao { get; }

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

            var that = (MissaoId) obj;

            return IdMissao.Equals(that.IdMissao);
        }

        public override string ToString()
        {
            return $"{IdMissao}";
        }
    }
}