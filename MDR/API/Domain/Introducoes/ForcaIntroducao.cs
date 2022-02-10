using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Introducoes
{
    public class ForcaIntroducao : IValueObject
    {
        public ForcaIntroducao(int forcaLigacao)
        {
            if (forcaLigacao < 0 || forcaLigacao > 100)
                throw new BusinessRuleValidationException("ForcaLigacao Invalida");

            Forca = forcaLigacao;
        }

        protected ForcaIntroducao()
        {
        }

        public int Forca { get; }

        public ForcaIntroducao ValueOf(int forcaLigacao)
        {
            return new ForcaIntroducao(forcaLigacao);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (ForcaIntroducao) obj;

            return Forca.Equals(that.Forca);
        }

        public override string ToString()
        {
            return $"{Forca}";
        }

        public int getForca()
        {
            return Forca;
        }
    }
}