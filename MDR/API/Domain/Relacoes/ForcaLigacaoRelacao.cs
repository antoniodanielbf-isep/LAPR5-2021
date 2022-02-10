using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Relacoes
{
    public class ForcaLigacaoRelacao : IValueObject
    {
        protected ForcaLigacaoRelacao()
        {
        }

        public ForcaLigacaoRelacao(int forca)
        {
            if (forca < 1 || forca > 100)
                throw new BusinessRuleValidationException("ForcaLigacao Invalida");

            ForcaLigacaoRel = forca;
        }

        public int ForcaLigacaoRel { get; }

        public ForcaLigacaoRelacao ValueOf(int forca)
        {
            return new ForcaLigacaoRelacao(forca);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (ForcaLigacaoRelacao) obj;

            return ForcaLigacaoRel.Equals(that.ForcaLigacaoRel);
        }

        public override string ToString()
        {
            return $"{ForcaLigacaoRel}";
        }
    }
}