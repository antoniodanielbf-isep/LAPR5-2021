namespace DDDSample1.Domain.Leaderboards.DTO
{
    public class LeaderboardDTO
    {
        public LeaderboardDTO(string email, int valor)
        {
            Email = email;
            Valor = valor;
        }

        public string Email { get; set; }
        public int Valor { get; set; }
    }
}