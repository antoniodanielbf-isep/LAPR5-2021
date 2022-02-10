import { Result } from '../../core/logic/Result';
import { IComentarioDTO } from '../../dto/Comentarios/IComentarioDTO';

export default interface IComentarioService {
  createComentario(comentarioDTO: IComentarioDTO): Promise<Result<IComentarioDTO>>;
  getComentario(comentarioId: string): Promise<Result<IComentarioDTO>>;
}
