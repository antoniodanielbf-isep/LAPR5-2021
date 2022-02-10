namespace DDDNetCore.Domain.RedesSociais.DTO
{
    public class TamanhoRedeSocialTotalDTO
    {
        public TamanhoRedeSocialTotalDTO(int tamanhoRedeSocialCompleto)
        {
            TamanhoRedeSocialCompleto = tamanhoRedeSocialCompleto;
        }

        public int TamanhoRedeSocialCompleto { get; set; }
    }
}