import IPedidoLigacaoDTO from "../../dto/utilizador-dto/IPedidoLigacaoDTO";

export class PedidoLigacaoMap {
  public static toDTO(id: string, or: string, dest: string,
                      forca: number, tagsL: string): IPedidoLigacaoDTO {
    return {
      id: id,
      or: or,
      dest: dest,
      forca: forca,
      tagsL: tagsL,
    } as IPedidoLigacaoDTO;
  }
}
