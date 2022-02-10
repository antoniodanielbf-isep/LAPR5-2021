using System;

namespace DDDNetCore.Domain.Introducoes
{
    public enum EstadoIntroducao // : Entity<IntroducaoIdd>
    {
        Pendente,
        Aceite,
        Recusada
    }

    public class EstadoIntroducaoOperations
    {
        public static EstadoIntroducao getEstadoIntroducaoLigacaoById(int id)
        {
            var l = Enum.GetValues<EstadoIntroducao>();

            return (EstadoIntroducao) l.GetValue(id);
        }

        public static int getIDByEstadoIntroducao(string estado)
        {
            var l = Enum.GetValues<EstadoIntroducao>();
            var x = 0;
            foreach (var pos in l)
            {
                if (estado == pos.ToString()) return x;
                x++;
            }

            return 0;
        }
    }
}