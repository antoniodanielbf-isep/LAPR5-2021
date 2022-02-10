using System;
using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Utilizadores
{
    public class PerfilFacebook : IValueObject
    {
        public PerfilFacebook()
        {
        }

        [JsonConstructor]
        public PerfilFacebook(Uri perfilURL)
        {
            UrlPerfilFacebook = perfilURL;
        }

        public PerfilFacebook(string perfilUrl)
        {
            if (perfilUrl.Equals("-----"))
            {
                UrlPerfilFacebook = new Uri("https://st2.depositphotos.com/5682790/10456/v/600/depositphotos_104564156-stock-illustration-male-user-icon.jpg");
            }
        }

        private Uri UrlPerfilFacebook { get; }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (PerfilFacebook) obj;

            return UrlPerfilFacebook.Equals(that.UrlPerfilFacebook);
        }

        public override string ToString()
        {
            return $"{UrlPerfilFacebook}";
        }
    }
}