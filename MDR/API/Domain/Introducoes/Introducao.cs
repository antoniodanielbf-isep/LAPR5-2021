using System.Collections.Generic;
using DDDNetCore.Domain.Introducoes.DTO;
using DDDNetCore.Domain.Missoes;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Introducoes
{
    public class Introducao : Entity<IntroducaoId>, IAggregateRoot
    {
        public Introducao()
        {
        }

        [JsonConstructor]
        public Introducao(string idD, string descricao, string utilizadorOrigem, string utilizadorIntermediario,
            string utilizadorDestino, int forca, List<string> listaTags)
        {
            Id = new IntroducaoId(idD);
            DescricaoIntroducao = new Descricao(descricao);
            UtilizadorOrigem = new EmailsIntro(utilizadorOrigem);
            UtilizadorIntermediario = new EmailsIntro(utilizadorIntermediario);
            UtilizadorDestino = new EmailsIntro(utilizadorDestino);
            ForcaLig = new ForcaIntroducao(forca);
            EstadoIntroducao = EstadoIntroducao.Pendente;
            ListaIntroducao = listaTags.ConvertAll(list => new TagIntroducao(list));
        }

        [JsonConstructor]
        public Introducao(string idD, string descricao, string utilizadorOrigem, string utilizadorIntermediario,
            string utilizadorDestino, int forca, string listaTags)
        {
            Id = new IntroducaoId(idD);
            DescricaoIntroducao = new Descricao(descricao);
            UtilizadorOrigem = new EmailsIntro(utilizadorOrigem);
            UtilizadorIntermediario = new EmailsIntro(utilizadorIntermediario);
            UtilizadorDestino = new EmailsIntro(utilizadorDestino);
            ForcaLig = new ForcaIntroducao(forca);
            EstadoIntroducao = EstadoIntroducao.Pendente;
            ListaIntroducao = new List<TagIntroducao>();
            var res = listaTags.Split(",");

            foreach (var pos in res) ListaIntroducao.Add(new TagIntroducao(pos));
        }

        public IntroducaoId Id { get; }

        public Descricao DescricaoIntroducao { get; }
        public EmailsIntro UtilizadorOrigem { get; private set; }
        public EmailsIntro UtilizadorIntermediario { get; private set; }
        public EmailsIntro UtilizadorDestino { get; private set; }
        public ForcaIntroducao ForcaLig { get; }
        public List<TagIntroducao> ListaIntroducao { get; }
        public EstadoIntroducao EstadoIntroducao { get; private set; }
        public List<Missao> ListaMissoes { get; set; }

        public void AddMissao(Missao missao)
        {
            ListaMissoes.Add(missao);
        }

        public void RemoveMissao(Missao missao)
        {
            ListaMissoes.Remove(missao);
        }

        public void setEstadoIntroducao(EstadoIntroducao novo)
        {
            EstadoIntroducao = novo;
        }

        public void setEmailErased(string old, string novo)
        {
            if (UtilizadorOrigem.ToString().Equals(old))
            {
                UtilizadorOrigem = new EmailsIntro(novo);
                if (EstadoIntroducao.Equals(EstadoIntroducao.Pendente))
                {
                    EstadoIntroducao = EstadoIntroducao.Recusada;
                }
            }
            else if (UtilizadorDestino.ToString().Equals(old))
            {
                UtilizadorDestino = new EmailsIntro(novo);
                if (EstadoIntroducao.Equals(EstadoIntroducao.Pendente))
                {
                    EstadoIntroducao = EstadoIntroducao.Recusada;
                }
            }
            else
            {
                UtilizadorIntermediario = new EmailsIntro(novo);
            }
        }

        public IntroducaoDTO toDto()
        {
            var tags = ListaIntroducao.ConvertAll(tag => tag.ToString());

            return new IntroducaoDTO(Id.ToString(), DescricaoIntroducao.ToString(), UtilizadorOrigem.ToString(),
                UtilizadorIntermediario.ToString(), UtilizadorDestino.ToString(), EstadoIntroducao.ToString(), tags);
        }

        public override string ToString()
        {
            return
                $"UtilizadorOrigem{UtilizadorOrigem}\n UtilizadorDestino{UtilizadorDestino}\n Descricao{DescricaoIntroducao}";
        }
    }
}