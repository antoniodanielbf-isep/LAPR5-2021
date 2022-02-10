using System;

namespace DDDNetCore.Domain.Missoes
{
    public enum EstadoMissao
    {
        EmProgresso,
        Sucesso,
        Falhada
    }

    public class EstadoMissaoOperations
    {
        public static EstadoMissao getEstadoMissaoById(int id)
        {
            var l = Enum.GetValues<EstadoMissao>();

            return (EstadoMissao) l.GetValue(id);
        }
    }
}