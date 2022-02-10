namespace DDDNetCore.Domain.PedidosIntroducao.DTO
{
    public class IntermediarioDTO
    {
        public IntermediarioDTO(string email)
        {
            this.email = email;
        }

        public string email { get; set; }
    }
}