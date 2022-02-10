import IUtilizadorDTO from "../../dto/utilizador-dto/IUtilizadorDTO";

export class UtilizadorMap{
  public static toDTO(nomeUtilizador: string, breveDescricaoUtilizador: string, email: string,
                      numeroDeTelefoneUtilizador: string, dataDeNascimentoUtilizador: string,
                      perfilFacebookUtilizador: string, perfilLinkedinUtilizador: string,
                      estadoEmocionalUtilizador: number, tagsUtilizador: string,
                      urlImagem: string, cidadePais: string, dataModificacao: string,
                      password: string): IUtilizadorDTO {
    return {
      nomeUtilizador: nomeUtilizador,
      breveDescricaoUtilizador: breveDescricaoUtilizador,
      email: email,
      numeroDeTelefoneUtilizador: numeroDeTelefoneUtilizador,
      dataDeNascimentoUtilizador: dataDeNascimentoUtilizador,
      perfilFacebookUtilizador: perfilFacebookUtilizador,
      perfilLinkedinUtilizador: perfilLinkedinUtilizador,
      estadoEmocionalUtilizador: estadoEmocionalUtilizador,
      tagsUtilizador: tagsUtilizador,
      urlImagem: urlImagem,
      cidadePaisResidencia: cidadePais,
      dataModificacaoEstado: dataModificacao,
      passwordU: password,
    } as IUtilizadorDTO;
  }

  public static toHelperDTO(): IUtilizadorDTO {
    return {
      nomeUtilizador: "",
      breveDescricaoUtilizador: "",
      email: "",
      numeroDeTelefoneUtilizador: "",
      dataDeNascimentoUtilizador: "",
      perfilFacebookUtilizador: "",
      perfilLinkedinUtilizador: "",
      estadoEmocionalUtilizador: 0,
      tagsUtilizador: "",
      urlImagem: "",
      cidadePaisResidencia: "",
      dataModificacaoEstado: "",
      passwordU: "",
    } as IUtilizadorDTO;
  }
}
