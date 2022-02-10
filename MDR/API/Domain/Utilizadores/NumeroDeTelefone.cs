using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Utilizadores
{
    public class NumeroDeTelefone : IValueObject
    {
        public NumeroDeTelefone()
        {
        }

        [JsonConstructor]
        public NumeroDeTelefone(string NumeroDeTelefone)
        {
            Telefone = NumeroDeTelefone;
        }

        private string Telefone { get; }

        public NumeroDeTelefone valueOf(string NumeroDeTelefone)
        {
            return new NumeroDeTelefone(NumeroDeTelefone);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (NumeroDeTelefone) obj;

            return Telefone.Trim().ToUpper().Equals(that.Telefone.Trim().ToUpper());
        }

        public override string ToString()
        {
            return $"{Telefone}";
        }
    }
}