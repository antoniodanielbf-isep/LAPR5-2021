import IPesquisaUtilizadorDTO from "../../dto/utilizador-dto/IPesquisaUtilizadorDTO";

export class PesquisaUtilizadorMap {
  public static toDTO(emailD: string, cidadePaisD: string, nomeD: string): IPesquisaUtilizadorDTO {
    return {
      emailD: emailD,
      cidadePaisD: cidadePaisD,
      nomeD: nomeD
    } as IPesquisaUtilizadorDTO;
  }
}
