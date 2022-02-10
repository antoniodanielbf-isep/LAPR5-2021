using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.PedidosIntroducao
{
    public class PedidoIntroducaoId : EntityId
    {
        [JsonConstructor]
        public PedidoIntroducaoId(string value) : base(value)
        {
            Id = value;
        }

        private string Id { get; }

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

            var that = (PedidoIntroducaoId) obj;

            return Id.Equals(that.Id);
        }

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}