using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Utilizadores
{
    public class Password : IValueObject
    {
        public Password()
        {
        }

        [JsonConstructor]
        public Password(string passwordUtilizador)
        {
            if (string.IsNullOrEmpty(passwordUtilizador.Trim()) || passwordUtilizador.StartsWith(" "))
                throw new BusinessRuleValidationException("Password Invalida");

            PasswordUtilizador = passwordUtilizador.Trim();
        }

        private string PasswordUtilizador { get; }

        public Password valueOf(string passwordUtilizador)
        {
            return new Password(passwordUtilizador);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (Password) obj;

            return PasswordUtilizador.Equals(that.PasswordUtilizador);
        }

        public override string ToString()
        {
            return $"{PasswordUtilizador}";
        }
    }
}