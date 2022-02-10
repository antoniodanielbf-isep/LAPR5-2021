using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Utilizadores
{
    public class Nome : IValueObject
    {
        public Nome()
        {
        }

        [JsonConstructor]
        public Nome(string nomeUtilizador)
        {
            if (string.IsNullOrEmpty(nomeUtilizador.Trim()) || nomeUtilizador.StartsWith(" "))
                throw new BusinessRuleValidationException("Nome Invalido");

            NomeUtilizador = nomeUtilizador.Trim();
        }

        private string NomeUtilizador { get; }

        public Nome valueOf(string nomeUtilizador)
        {
            return new Nome(nomeUtilizador);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (Nome) obj;

            return NomeUtilizador.ToUpper().Equals(that.NomeUtilizador.ToUpper());
        }

        public override string ToString()
        {
            return $"{NomeUtilizador}";
        }
    }
}