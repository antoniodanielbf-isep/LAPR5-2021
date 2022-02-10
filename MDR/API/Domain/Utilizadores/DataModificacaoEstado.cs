using System;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Utilizadores
{
    public class DataModificacaoEstado : IValueObject
    {
        public DataModificacaoEstado()
        {
        }

        [JsonConstructor]
        public DataModificacaoEstado(DateTime date)
        {
            DataMod = date;
        }

        private DateTime DataMod { get; }

        public DateTime returnData()
        {
            return DataMod;
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (DataModificacaoEstado) obj;

            return DataMod.Equals(that.DataMod);
        }

        public override string ToString()
        {
            return $"{DataMod}";
        }
    }
}