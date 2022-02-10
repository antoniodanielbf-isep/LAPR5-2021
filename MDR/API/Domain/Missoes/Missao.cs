using DDDNetCore.Domain.Introducoes;
using DDDNetCore.Domain.PedidosIntroducao;
using DDDNetCore.Domain.Utilizadores;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Missoes
{
    public class Missao : Entity<MissaoId>, IAggregateRoot
    {
        private const int PONTUACAO_ACEITAR_PEDIDO_INTRODUCAO = 100;
        private const int PONTUACAO_REJEITAR_PEDIDO_INTRODUCAO = 75;
        private const int PONTUACAO_ACEITAR_INTRODUCAO = 250;
        private const int PONTUACAO_REJEITAR_INTRODUCAO = 200;

        public Missao()
        {
        }

        [JsonConstructor]
        public Missao(string id, NivelDeDificuldade nivel, EstadoMissao estado)
        {
            Id = new MissaoId(id);
            NivelDeDificuldade = nivel;
            EstadoMissao = estado;
            PontuacaoAtual = new MissaoPontuacao(0);
        }

        [JsonConstructor]
        public Missao(string id, int nivelDeDificuldade, int idEstado)
        {
            Id = new MissaoId(id);
            NivelDeDificuldade = new NivelDeDificuldade(nivelDeDificuldade);
            EstadoMissao = EstadoMissaoOperations.getEstadoMissaoById(idEstado);
            PontuacaoAtual = new MissaoPontuacao(0);
        }

        public MissaoId Id { get; }

        public NivelDeDificuldade NivelDeDificuldade { get; set; }
        public EstadoMissao EstadoMissao { get; set; }
        public MissaoPontuacao PontuacaoAtual { get; set; }

        public Utilizador Utilizador { get; set; }
        public EmailUtilizador EmailUtilizador { get; set; }

        public PedidoIntroducao PedidoIntroducao { get; set; }
        public PedidoIntroducaoId PedidoIntroducaoId { get; set; }

        public Introducao Introducao { get; set; }
        public IntroducaoId IntroducaoId { get; set; }


        public void setUtilizadorID(EmailUtilizador userId)
        {
            EmailUtilizador = userId;
        }


        public void setPedidoIntroducaoID(PedidoIntroducaoId pedidoId)
        {
            PedidoIntroducaoId = pedidoId;
        }

        public void setPedidoIntroducao(PedidoIntroducao pedido)
        {
            PedidoIntroducao = pedido;
        }

        public void setIntroducaoID(IntroducaoId introId)
        {
            IntroducaoId = introId;
        }


        public void setEstadoMissao(EstadoMissao EstadoMissaoAtual)
        {
            EstadoMissao = EstadoMissaoAtual;
        }

        public void setNivelDeDificuldade(NivelDeDificuldade NivelDeDificuldadeAtual)
        {
            NivelDeDificuldade = NivelDeDificuldadeAtual;
        }

        public MissaoDTO toDto()
        {
            return new MissaoDTO(NivelDeDificuldade.obterDificuldade(), EstadoMissao.ToString());
        }

        public override string ToString()
        {
            return $"Nivel De Dificuldade: {NivelDeDificuldade}\nEstado da Missao:{EstadoMissao}";
        }


        //PONTUAÇÕES
        public void PontuacaoAceitarPedido()
        {
            PontuacaoAtual.addPontuacao(PONTUACAO_ACEITAR_PEDIDO_INTRODUCAO);
        }

        public void PontuacaoRecusarPedido()
        {
            PontuacaoAtual.takePontuacao(PONTUACAO_REJEITAR_PEDIDO_INTRODUCAO);
        }

        public void PontuacaoAceitarIntroducao()
        {
            PontuacaoAtual.addPontuacao(PONTUACAO_ACEITAR_INTRODUCAO);
        }

        public void PontuacaoRecusarIntroducao()
        {
            PontuacaoAtual.takePontuacao(PONTUACAO_REJEITAR_INTRODUCAO);
        }
    }
}