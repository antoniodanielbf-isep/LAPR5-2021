import { Repo } from '../../core/infra/Repo';
import { Comentario } from '../../domain/Agregados/Comentarios/comentario';

export default interface IComentarioRepo extends Repo<Comentario> {
  save(comentario: Comentario): Promise<Comentario>;
  findById(id: string): Promise<Comentario>;
  findAll(): Promise<Comentario[]>;
}
