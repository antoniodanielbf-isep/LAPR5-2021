import IRelacaoDTO from "../../dto/relacao-dto/IRelacaoDTO";

export class RelacaoMap {
  public static toDTO(relacaoIdV: string, utilizadorOrigemV: string,
                      utilizadorDestinoV: string, forcaLigacaoOrigDestV: string,
                      forcaLigacaoDestOrigV: string,
                      tagsRelacaoABV: string,
                      tagsRelacaoBAV: string): IRelacaoDTO {
    return {
      relacaoId: relacaoIdV,
      utilizadorOrigem: utilizadorOrigemV,
      utilizadorDestino: utilizadorDestinoV,
      forcaLigacaoOrigDest: forcaLigacaoOrigDestV,
      forcaLigacaoDestOrig: forcaLigacaoDestOrigV,
      tagsRelacaoAB: tagsRelacaoABV,
      tagsRelacaoBA: tagsRelacaoBAV,
    } as IRelacaoDTO;
  }
}
