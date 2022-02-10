using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DDDNetCore.Domain.Relacoes.DTO;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Relacoes
{
    public class Relacao : Entity<RelacaoId>
    {
        public Relacao()
        {
        }

        [JsonConstructor]
        public Relacao(string id, string utilizadorOrigem, string utilizadorDestino,
            List<string> tagsUa, List<string> tagsUb, int forcaAb, int forcaBa)
        {
            Id = new RelacaoId(id);
            UtilizadorOrigem = new EmailsRelacao(utilizadorOrigem);
            UtilizadorDestino = new EmailsRelacao(utilizadorDestino);
            ForcaLigacaoOrigDest = new ForcaLigacaoRelacao(forcaAb);
            ForcaLigacaoDestOrig = new ForcaLigacaoRelacao(forcaBa);
            TagA = tagsUa.ConvertAll(t => new TagRelacao(t));
            TagB = tagsUb.ConvertAll(t => new TagRelacao(t));
        }

        public Relacao(string id, string utilizadorOrigem, string utilizadorDestino,
            List<string> tagsUa, string tagsUb, int forcaAb, int forcaBa)
        {
            Id = new RelacaoId(id);
            UtilizadorOrigem = new EmailsRelacao(utilizadorOrigem);
            UtilizadorDestino = new EmailsRelacao(utilizadorDestino);
            ForcaLigacaoOrigDest = new ForcaLigacaoRelacao(forcaAb);
            ForcaLigacaoDestOrig = new ForcaLigacaoRelacao(forcaBa);
            TagA = tagsUa.ConvertAll(t => new TagRelacao(t));
            var aux = tagsUb.Split(",").ToList();
            TagB = aux.ConvertAll(t => new TagRelacao(t));
        }


        [Key] public RelacaoId Id { get; }

        public EmailsRelacao UtilizadorOrigem { get; private set; }
        public EmailsRelacao UtilizadorDestino { get; private set; }
        public ForcaLigacaoRelacao ForcaLigacaoOrigDest { get; private set; }
        public ForcaLigacaoRelacao ForcaLigacaoDestOrig { get; private set; }
        public List<TagRelacao> TagA { get; private set; }
        public List<TagRelacao> TagB { get; private set; }

        public void setForcaLigacaoRelacaoOrigDest(ForcaLigacaoRelacao forcaLigacaoOrigDest)
        {
            ForcaLigacaoOrigDest = forcaLigacaoOrigDest;
        }

        public void setForcaLigacaoRelacaoDestOrig(ForcaLigacaoRelacao forcaLigacaoDestOrig)
        {
            ForcaLigacaoDestOrig = forcaLigacaoDestOrig;
        }

        public void setTagRelacaoOrigem(string tagsOrigem)
        {
            TagA.Clear();
            var listaAux = new List<string>();
            foreach (var tag in tagsOrigem.Split(",")) listaAux.Add(tag);

            TagA = new List<TagRelacao>();
            TagA = listaAux.ConvertAll(tt =>
                new TagRelacao(tt));
        }

        public void setTagRelacaoDestino(string tagsDestino)
        {
            TagB.Clear();
            var listaAux = new List<string>();
            foreach (var tag in tagsDestino.Split(",")) listaAux.Add(tag);

            TagB = new List<TagRelacao>();
            TagB = listaAux.ConvertAll(tt =>
                new TagRelacao(tt));
        }

        public void setEmailErased(string old, string novo)
        {
            if (UtilizadorOrigem.ToString().Equals(old))
            {
                UtilizadorOrigem = new EmailsRelacao(novo);
            }
            else
            {
                UtilizadorDestino = new EmailsRelacao(novo);
            }
        }

        public RelacaoDTO toDto()
        {
            var tagsRelacaoAb = TagA.ConvertAll(t => t.ToString());
            var tagsRelacaoBa = TagB.ConvertAll(t => t.ToString());


            return new RelacaoDTO(Id.ToString(), UtilizadorOrigem.ToString(), UtilizadorDestino.ToString(),
                ForcaLigacaoOrigDest.ForcaLigacaoRel, ForcaLigacaoDestOrig.ForcaLigacaoRel, tagsRelacaoAb,
                tagsRelacaoBa);
        }
    }
}