using System.Text.Json.Serialization;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Utilizadores
{
    public class CidadeEPaisResidencia : IValueObject
    {
        public CidadeEPaisResidencia()
        {
        }

        [JsonConstructor]
        public CidadeEPaisResidencia(string cidadeEPais)
        {
            if (string.IsNullOrEmpty(cidadeEPais.Trim()))
                throw new BusinessRuleValidationException("Cidade E Pais Residencia Invalidos");

            CidadeEPais = cidadeEPais.Trim();
        }


        private string CidadeEPais { get; }

        public CidadeEPaisResidencia valueOf(string cidadeEPais)
        {
            return new CidadeEPaisResidencia(cidadeEPais);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (CidadeEPaisResidencia) obj;

            return CidadeEPais.ToUpper().Equals(that.CidadeEPais.ToUpper());
        }

        public override string ToString()
        {
            return $"{CidadeEPais}";
        }
    }
}