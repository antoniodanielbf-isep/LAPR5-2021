using System;

namespace DDDNetCore.Domain.Utilizadores
{
    public enum EstadoEmocional
    {
        Alegria,
        Angustia,
        Esperanca,
        Medo,
        Alivio,
        Dececao,
        Orgulho,
        Remorsos,
        Gratidao,
        Raiva
    }

    public class EstadoEmocionalOperations
    {
        public static EstadoEmocional getEstadoEmocionalById(int id)
        {
            var l = Enum.GetValues<EstadoEmocional>();

            return (EstadoEmocional) l.GetValue(id);
        }

        public static int getIDByEstadoEmocionald(EstadoEmocional estado)
        {
            var l = Enum.GetValues<EstadoEmocional>();
            var x = 0;
            foreach (var pos in l)
            {
                if (estado == pos) return x;
                x++;
            }

            return 0;
        }

        public static EstadoEmocional getEstadoEmocionaldByString(string estado)
        {
            var l = Enum.GetValues<EstadoEmocional>();
            foreach (var pos in l)
                if (estado == pos.ToString())
                    return pos;
            return 0;
        }
    }
}