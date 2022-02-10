export default interface IPedidoIntroDTO {
  id: string;
  descricao: string;
  emailOrigem: string;
  emailIntermedio: string;
  emailDestino: string;
  estado: number;
  forca: number;
  tags: string;
}
