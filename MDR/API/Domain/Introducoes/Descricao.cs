using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Introducoes
{
    public class Descricao : IValueObject
    {
        public Descricao()
        {
        }

        public Descricao(string descricao)
        {
            if (descricao.Length > 1000 || descricao.Length <= 0)
                throw new BusinessRuleValidationException("Descricao Invalida");
            DescricaoIntroducao = descricao;
        }

        private string DescricaoIntroducao { get; }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (Descricao) obj;

            return DescricaoIntroducao.Equals(that.DescricaoIntroducao);
        }

        public override string ToString()
        {
            return $"{DescricaoIntroducao}";
        }
    }
}