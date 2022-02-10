using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Utilizadores
{
    public class BreveDescricao : IValueObject
    {
        public BreveDescricao()
        {
        }

        [JsonConstructor]
        public BreveDescricao(string breveDescricaoC)
        {
            if (string.IsNullOrEmpty(breveDescricaoC))

                throw new BusinessRuleValidationException("Descricao Invalida");

            Descricao = breveDescricaoC.Trim();
        }

        private string Descricao { get; }

        public BreveDescricao valueOf(string Descricao)
        {
            return new BreveDescricao(Descricao);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (BreveDescricao) obj;

            return Descricao.ToUpper().Equals(that.Descricao.ToUpper());
        }

        public override string ToString()
        {
            return $"{Descricao}";
        }
    }
}