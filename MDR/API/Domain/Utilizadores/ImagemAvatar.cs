using System;
using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Utilizadores
{
    public class ImagemAvatar : IValueObject
    {
        public ImagemAvatar()
        {
        }

        [JsonConstructor]
        public ImagemAvatar(Uri Url)
        {
            UrlImagem = Url;
        }

        public ImagemAvatar(string url)
        {
            if (url.Equals("-----"))
            {
                UrlImagem = new Uri("https://st2.depositphotos.com/5682790/10456/v/600/depositphotos_104564156-stock-illustration-male-user-icon.jpg");
            }
        }

        private Uri UrlImagem { get; }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (ImagemAvatar) obj;

            return UrlImagem.Equals(that.UrlImagem);
        }

        public override string ToString()
        {
            return $"{UrlImagem}";
        }
    }
}