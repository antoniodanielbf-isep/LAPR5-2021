using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DDDNetCore.Domain.Missoes;
using DDDNetCore.Domain.Utilizadores.DTO;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Utilizadores
{
    public class Utilizador : Entity<EmailUtilizador>
    {
        [JsonConstructor]
        public Utilizador()
        {
        }

        [JsonConstructor]
        public Utilizador(Nome nomeUtilizador, EmailUtilizador email,
            NumeroDeTelefone numeroDeTelefoneUtilizador, DataDeNascimento dataDeNascimentoUtilizador,
            List<Tag> tagsUtilizador, Password password)
        {
            NomeUtilizador = nomeUtilizador;
            Id = email;
            NumeroDeTelefoneUtilizador = numeroDeTelefoneUtilizador;
            DataDeNascimentoUtilizador = dataDeNascimentoUtilizador;
            TagsUtilizador = tagsUtilizador;
            PasswordU = password;
            DataTermos = new DataAceitacaoTermos(DateTime.Now);
        }

        [JsonConstructor]
        public Utilizador(string nomeUtilizador, string email,
            string numeroDeTelefoneUtilizador, string dataDeNascimentoUtilizador,
            string tagsUtilizador, string password)
        {
            NomeUtilizador = new Nome(nomeUtilizador);
            Id = new EmailUtilizador(email);
            NumeroDeTelefoneUtilizador = new NumeroDeTelefone(numeroDeTelefoneUtilizador);
            DataDeNascimentoUtilizador = new DataDeNascimento(dataDeNascimentoUtilizador);
            var listaAux = new List<string>();
            foreach (var tag in tagsUtilizador.Split(",")) listaAux.Add(tag);

            TagsUtilizador = new List<Tag>();
            TagsUtilizador = listaAux.ConvertAll(tt =>
                new Tag(tt));
            PasswordU = new Password(password);
            DataTermos = new DataAceitacaoTermos(DateTime.Now);
        }

        [Key] public EmailUtilizador Id { get; private set; }

        public Nome NomeUtilizador { get; private set; }
        public BreveDescricao BreveDescricaoUtilizador { get; private set; }
        public NumeroDeTelefone NumeroDeTelefoneUtilizador { get; private set; }
        public DataDeNascimento DataDeNascimentoUtilizador { get; private set; }
        public EstadoEmocional EstadoEmocionalUtilizador { get; private set; }
        public DataModificacaoEstado DataModificacao { get; private set; }
        public DataAceitacaoTermos DataTermos { get; private set; }
        public Password PasswordU { get; private set; }
        public PerfilFacebook PerfilFacebookUtilizador { get; private set; }
        public PerfilLinkedin PerfilLinkedinUtilizador { get; private set; }
        public List<Tag> TagsUtilizador { get; private set; }
        public List<Missao> MissoesUtilizador { get; private set; }
        public CidadeEPaisResidencia CidadePais { get; private set; }

        public ImagemAvatar ImagemU { get; private set; }

        public void setNomeUtilizador(Nome nomeUtilizador)
        {
            NomeUtilizador = nomeUtilizador;
        }

        public void setPassword(Password password)
        {
            PasswordU = password;
        }

        public void setEstadoEmocional(EstadoEmocional estadoEmocionalUtilizador)
        {
            EstadoEmocionalUtilizador = estadoEmocionalUtilizador;
            DataModificacao = new DataModificacaoEstado(DateTime.Now);
        }

        public void setCidadeEPaisResidencia(CidadeEPaisResidencia cidadePais)
        {
            CidadePais = cidadePais;
        }

        public void setTags(List<Tag> tags)
        {
            TagsUtilizador = tags;
        }

        public List<string> getTags()
        {
            return TagsUtilizador.ConvertAll(tag => tag.ToString());
        }

        public void setMissoes(List<Missao> missao)
        {
            MissoesUtilizador = missao;
        }

        public void setBreveDescricao(BreveDescricao breveDescricaoUtilizador)
        {
            BreveDescricaoUtilizador = breveDescricaoUtilizador;
        }

        public void setImagemAvatar(ImagemAvatar url)
        {
            ImagemU = url;
        }

        public void setPerfilLinkedin(PerfilLinkedin url)
        {
            PerfilLinkedinUtilizador = url;
        }

        public void setPerfilFacebook(PerfilFacebook url)
        {
            PerfilFacebookUtilizador = url;
        }

        public void setNumeroTelefone(NumeroDeTelefone numeroDeTelefone)
        {
            NumeroDeTelefoneUtilizador = numeroDeTelefone;
        }
        
        public void setDataNascimento(DataDeNascimento dataDeNascimento)
        {
            DataDeNascimentoUtilizador = dataDeNascimento;
        }
        
        public void setEmail(EmailUtilizador emailUtilizador)
        {
            Id = emailUtilizador;
        }

        public UtilizadorDTO toDto()
        {
            var tagFinal = new StringBuilder();
            var i = 0;

            foreach (var tag in TagsUtilizador)
            {
                tagFinal = tagFinal.Append(tag);
                if (i + 1 != TagsUtilizador.Count) tagFinal = tagFinal.Append(", ");

                i += 1;
            }

            var dataAux = DataModificacao.returnData().Subtract(DateTime.Now);
            var dataFinal = "Day: " + -dataAux.Days + "/ Hours: " + -dataAux.Hours + "/ Minutes: " + -dataAux.Minutes +
                            "/ Seconds: " + -dataAux.Seconds;

            return new UtilizadorDTO(NomeUtilizador.ToString(), BreveDescricaoUtilizador.ToString(),
                Id.ToString(), NumeroDeTelefoneUtilizador.ToString(),
                DataDeNascimentoUtilizador.ToString(),
                EstadoEmocionalOperations.getIDByEstadoEmocionald(EstadoEmocionalUtilizador),
                PerfilFacebookUtilizador.ToString(), PerfilLinkedinUtilizador.ToString(),
                tagFinal.ToString(), CidadePais.ToString(), ImagemU.ToString(), "*****",
                dataFinal);
        }

        public UtilizadorSugestoesDTO toDto2(List<string> sugestoesDeAmizade)
        {
            var tagFinal = new StringBuilder();
            var i = 0;

            foreach (var tag in TagsUtilizador)
            {
                tagFinal = tagFinal.Append(tag);
                if (i + 1 != TagsUtilizador.Count) tagFinal = tagFinal.Append(", ");

                i += 1;
            }

            var dataAux = DataModificacao.returnData().Subtract(DateTime.Now);
            var dataFinal = "Day: " + -dataAux.Days + "/ Hours: " + -dataAux.Hours + "/ Minutes: " + -dataAux.Minutes +
                            "/ Seconds: " + -dataAux.Seconds;

            return new UtilizadorSugestoesDTO(NomeUtilizador.ToString(), BreveDescricaoUtilizador.ToString(),
                Id.ToString(), NumeroDeTelefoneUtilizador.ToString(),
                DataDeNascimentoUtilizador.ToString(),
                EstadoEmocionalUtilizador.ToString(),
                PerfilFacebookUtilizador.ToString(),
                PerfilLinkedinUtilizador.ToString(), tagFinal.ToString(), CidadePais.ToString(), ImagemU.ToString(),
                "*****", dataFinal
                , sugestoesDeAmizade);
        }

        public UtilizadorTagsDTO toDtoTags()
        {
            var tagFinal = new StringBuilder();
            var i = 0;

            foreach (var tag in TagsUtilizador)
            {
                tagFinal = tagFinal.Append(tag);
                if (i + 1 != TagsUtilizador.Count) tagFinal = tagFinal.Append(", ");

                i += 1;
            }

            return new UtilizadorTagsDTO(tagFinal.ToString());
        }

        public UtilizadorSugestoesSendDTO toDtoSugestoes()
        {
            return new UtilizadorSugestoesSendDTO(NomeUtilizador.ToString(), Id.ToString());
        }
        
        public UtilizadorEraseDTO toEraseDto()
        {
            return new UtilizadorEraseDTO(Id.ToString(), NomeUtilizador.ToString());
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (Utilizador) obj;

            return Id.Equals(that.Id);
        }

        public string toString()
        {
            return
                $"Utilizador: {NomeUtilizador}\nDescricao Breve: {BreveDescricaoUtilizador}\nEmail: {Id}\nNumero: {NumeroDeTelefoneUtilizador}\nData de Nascimento: {DataDeNascimentoUtilizador}\nEstado Emocional Utilizador: {EstadoEmocionalUtilizador}\nPerfil Facebook Utilizador: {PerfilFacebookUtilizador}\nPerfil Linkedin Utilizador: {PerfilLinkedinUtilizador}\nTags Utilizador: {TagsUtilizador}\nCidade e Pais: {CidadePais}";
        }
    }
}