using System;
using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Utilizadores
{
    public class PerfilLinkedin : IValueObject
    {
        public PerfilLinkedin()
        {
        }

        [JsonConstructor]
        public PerfilLinkedin(Uri perfilURL)
        {
            UrlPerfilLinked = perfilURL;
        }

        public PerfilLinkedin(string perfilUrl)
        {
            if (perfilUrl.Equals("-----"))
            {
                UrlPerfilLinked = new Uri("https://st2.depositphotos.com/5682790/10456/v/600/depositphotos_104564156-stock-illustration-male-user-icon.jpg");
            }
        }

        private Uri UrlPerfilLinked { get; }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (PerfilLinkedin) obj;

            return UrlPerfilLinked.Equals(that.UrlPerfilLinked);
        }

        public override string ToString()
        {
            return $"{UrlPerfilLinked}";
        }
    }
}