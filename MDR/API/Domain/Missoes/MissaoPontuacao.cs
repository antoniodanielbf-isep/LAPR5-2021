using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Missoes
{
    public class MissaoPontuacao : IValueObject
    {
        protected MissaoPontuacao()
        {
        }

        public MissaoPontuacao(int missaoPontucao)
        {
            Pontuacao = missaoPontucao;
        }

        public int Pontuacao { get; set; }

        public MissaoPontuacao ValueOf(int missaoPontucao)
        {
            return new MissaoPontuacao(missaoPontucao);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (MissaoPontuacao) obj;

            return Pontuacao.Equals(that.Pontuacao);
        }

        public override string ToString()
        {
            return $"{Pontuacao}";
        }

        public int getPontuacao()
        {
            return Pontuacao;
        }

        public void addPontuacao(int pontuacao)
        {
            Pontuacao += pontuacao;
        }

        public void takePontuacao(int pontuacao)
        {
            Pontuacao -= pontuacao;
        }
    }
}