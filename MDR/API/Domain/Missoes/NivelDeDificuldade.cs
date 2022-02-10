using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Missoes
{
    public class NivelDeDificuldade
    {
        public NivelDeDificuldade()
        {
        }

        public NivelDeDificuldade(int NivelDeDificuldade)
        {
            if (NivelDeDificuldade < 1 || NivelDeDificuldade > 10)
                throw new BusinessRuleValidationException("Nivel de Dificuldade Invalido");

            NivelDificuldade = NivelDeDificuldade;
        }

        private int NivelDificuldade { get; }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (NivelDeDificuldade) obj;

            return NivelDificuldade.Equals(that.NivelDificuldade);
        }

        public int obterDificuldade()
        {
            return NivelDificuldade;
        }

        public override string ToString()
        {
            return $"{NivelDificuldade}";
        }
    }
}