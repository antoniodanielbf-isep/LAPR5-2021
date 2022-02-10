using System;
using System.Globalization;
using DDDSample1.Domain.Shared;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Utilizadores
{
    public class DataDeNascimento : IValueObject
    {
        public DataDeNascimento()
        {
        }

        [JsonConstructor]
        public DataDeNascimento(string dataDeNascimento)
        {
            if (string.IsNullOrEmpty(dataDeNascimento.Trim()))
                throw new BusinessRuleValidationException("Data de Nascimento Invalida");


            try
            {
                var aux = dataDeNascimento.Split("/");


                var dataAux = new DateTime(IntegerType.FromString(aux[2]),
                    IntegerType.FromString(aux[1]), IntegerType.FromString(aux[0]));

                if (dataAux < DateTime.Now.AddDays(-5844))
                    Data = dataAux;
                else
                    throw new BusinessRuleValidationException("Tem Menos de 16 Anos");
            }
            catch (FormatException)
            {
                throw new BusinessRuleValidationException("Formato da Data de Nascimento Invalido");
            }
            catch (CultureNotFoundException)
            {
                throw new BusinessRuleValidationException("Data de Nascimento Invalida");
            }
        }

        private DateTime Data { get; }

        public DataDeNascimento ValueOf(string dataDeNascimento)
        {
            return new DataDeNascimento(dataDeNascimento);
        }

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;

            if (obj == null || obj.GetType() != GetType()) return false;

            var that = (DataDeNascimento) obj;

            return Data.Equals(that.Data);
        }

        public override string ToString()
        {
            return $"{Data}";
        }
    }
}