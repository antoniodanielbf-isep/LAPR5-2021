using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Utilizadores
{
    public class EmailUtilizador : EntityId
    {
        [JsonConstructor]
        public EmailUtilizador(string value) : base(value)
        {
            if (!IsValidEmail(value.Trim())) throw new BusinessRuleValidationException("Email Invalido");
            Email = value.Trim();
        }

        private string Email { get; }

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

            var that = (EmailUtilizador) obj;

            return Email.Equals(that.Email);
        }

        public override string ToString()
        {
            return $"{Email}";
        }

        private static bool IsValidEmail(string email)
        {
            var rg = new Regex(
                @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(email))
                return true;
            return false;
        }
    }
}