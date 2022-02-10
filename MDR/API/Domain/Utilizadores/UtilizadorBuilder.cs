using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDDNetCore.Domain.Utilizadores
{
    public class UtilizadorBuilder
    {
        [JsonConstructor]
        public UtilizadorBuilder()
        {
            TagsUtilizador = new List<Tag>();
        }

        public Nome NomeUtilizador { get; private set; }
        public BreveDescricao BreveDescricaoUtilizador { get; private set; }
        public EmailUtilizador Email { get; private set; }
        public NumeroDeTelefone NumeroDeTelefoneUtilizador { get; private set; }
        public DataDeNascimento DataDeNascimentoUtilizador { get; private set; }
        public EstadoEmocional EstadoEmocionalUtilizador { get; private set; }
        public PerfilFacebook PerfilFacebookUtilizador { get; private set; }
        public PerfilLinkedin PerfilLinkedinUtilizador { get; private set; }
        public List<Tag> TagsUtilizador { get; private set; }
        public ImagemAvatar Imagem { get; private set; }
        public CidadeEPaisResidencia CidadePais { get; private set; }
        public Password PasswordU { get; private set; }

        public UtilizadorBuilder setNome(string nome)
        {
            NomeUtilizador = new Nome(nome);
            return this;
        }

        public UtilizadorBuilder setPassword(string password)
        {
            PasswordU = new Password(password);
            return this;
        }

        public UtilizadorBuilder setTags(string tags)
        {
            var listaAux = new List<string>();
            foreach (var tag in tags.Split(",")) listaAux.Add(tag);
            TagsUtilizador = listaAux.ConvertAll(tt =>
                new Tag(tt));

            return this;
        }

        public UtilizadorBuilder setDataNascimento(string dataNascimento)
        {
            DataDeNascimentoUtilizador = new DataDeNascimento(dataNascimento);
            return this;
        }

        public UtilizadorBuilder setNrTelefone(string nrTelefone)
        {
            NumeroDeTelefoneUtilizador = new NumeroDeTelefone(nrTelefone);
            return this;
        }

        public UtilizadorBuilder setEmail(string email)
        {
            Email = new EmailUtilizador(email);
            return this;
        }

        public UtilizadorBuilder setEstadoEmocional(int id)
        {
            EstadoEmocionalUtilizador = EstadoEmocionalOperations.getEstadoEmocionalById(id);
            return this;
        }

        public UtilizadorBuilder setCidadeEPaisResidencia(string cidadePaisResidencia)
        {
            CidadePais = new CidadeEPaisResidencia(cidadePaisResidencia);
            return this;
        }

        public UtilizadorBuilder setBreveDescricao(string breveDescricaoText)
        {
            BreveDescricaoUtilizador = new BreveDescricao(breveDescricaoText);
            return this;
        }

        public UtilizadorBuilder setImagemAvatar(string imagemAvatar)
        {
            if (imagemAvatar.Equals("-----"))
            {
                Imagem = new ImagemAvatar(imagemAvatar);
            }
            else
            {
                Imagem = new ImagemAvatar(new Uri(imagemAvatar));
            }
            return this;
        }

        public UtilizadorBuilder setPerfilFacebook(string perfilFacebook)
        {
            if (perfilFacebook.Equals("-----"))
            {
                PerfilFacebookUtilizador = new PerfilFacebook(perfilFacebook);
            }
            else
            {
                PerfilFacebookUtilizador = new PerfilFacebook(new Uri(perfilFacebook));
            }
            return this;
        }

        public UtilizadorBuilder setPerfilLinkedin(string perfilLinkedin)
        {
            if (perfilLinkedin.Equals("-----"))
            {
                PerfilLinkedinUtilizador = new PerfilLinkedin(perfilLinkedin);
            }
            else
            {
                PerfilLinkedinUtilizador = new PerfilLinkedin(new Uri(perfilLinkedin));
            }
            return this;
        }


        public Utilizador createUtilizador()
        {
            var usr = new Utilizador(NomeUtilizador, Email, NumeroDeTelefoneUtilizador,
                DataDeNascimentoUtilizador, TagsUtilizador, PasswordU);

            if (EstadoEmocionalUtilizador != null) usr.setEstadoEmocional(EstadoEmocionalUtilizador);

            if (CidadePais != null) usr.setCidadeEPaisResidencia(CidadePais);

            //string BreveDescricao = BreveDescricaoUtilizador.ToString();
            if (BreveDescricaoUtilizador != null) usr.setBreveDescricao(BreveDescricaoUtilizador);

            if (PerfilFacebookUtilizador != null) usr.setPerfilFacebook(PerfilFacebookUtilizador);

            if (PerfilLinkedinUtilizador != null) usr.setPerfilLinkedin(PerfilLinkedinUtilizador);

            if (Imagem != null) usr.setImagemAvatar(Imagem);
            return usr;
        }
    }
}