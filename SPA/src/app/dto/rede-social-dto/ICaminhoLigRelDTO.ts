export interface ICaminhoFLigacaoRelacaoDTO {
  caminho: Array<string>;
  valor: number;
  forcasLigacaoDestinoOrigem: Array<number>;
  forcasLigacaoOrigemDestino: Array<number>;
  forcasRelacaoDestinoOrigem: Array<number>;
  forcasRelacaoOrigemDestino: Array<number>;
}
