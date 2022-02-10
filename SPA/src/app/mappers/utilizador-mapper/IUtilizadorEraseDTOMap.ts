import {IUtilizadorEraseDTO} from "../../dto/utilizador-dto/IUtilizadorEraseDTO";

export class IUtilizadorEraseDTOMap {
  public static toDTO(old:string,novo:string): IUtilizadorEraseDTO {
    return {
      old:old,
      novo:novo,
    } as IUtilizadorEraseDTO;
  }
}
