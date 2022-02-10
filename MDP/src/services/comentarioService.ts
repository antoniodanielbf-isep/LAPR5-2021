import { Container, Inject, Service } from 'typedi';
import config from '../../config';
import IComentarioRepo from '../services/IRepos/IComentarioRepo';
import { Result } from '../core/logic/Result';
import IComentarioService from './IServices/IComentarioService';
import { IComentarioDTO } from '../dto/Comentarios/IComentarioDTO';
import { ComentarioMap } from '../mappers/Comentarios/ComentarioMap';
import { Comentario } from '../domain/Agregados/Comentarios/comentario';
import UtilizadorRepo from '../repos/utilizadorRepo';
import PostRepo from '../repos/postRepo';

@Service()
export default class ComentarioService implements IComentarioService {
  constructor(@Inject(config.repos.comentario.name) private comentarioRepo: IComentarioRepo) {}

  public async getComentario(comentarioId: string): Promise<Result<IComentarioDTO>> {
    try {
      const comentario = await this.comentarioRepo.findById(comentarioId);

      if (comentario === null) {
        return Result.fail<IComentarioDTO>('Comentario not found');
      } else {
        const comentarioDTOResult = ComentarioMap.toDTO(comentario) as IComentarioDTO;
        return Result.ok<IComentarioDTO>(comentarioDTOResult);
      }
    } catch (e) {
      throw e;
    }
  }

  public async createComentario(comentarioDTO: IComentarioDTO): Promise<Result<IComentarioDTO>> {
    try {
      const repoU = Container.get(UtilizadorRepo);
      const utilizador = await repoU.findByEmail(comentarioDTO.utilizador);
      const repoP = Container.get(PostRepo);
      const post = await repoP.findById(comentarioDTO.post);

      try {
        const userId = utilizador.id;
      } catch (e) {
        throw Error('User not found');
      }

      try {
        const postUser = post.utilizador;
      } catch (e) {
        throw Error(post.utilizador);
      }

      const comentarioOrError = Comentario.create(comentarioDTO);

      if (comentarioOrError.isFailure) {
        return Result.fail<IComentarioDTO>(comentarioOrError.errorValue());
      }

      const comentarioResult = comentarioOrError.getValue();

      await this.comentarioRepo.save(comentarioResult);

      const comentarioDTOResult = ComentarioMap.toDTO(comentarioResult) as IComentarioDTO;
      return Result.ok<IComentarioDTO>(comentarioDTOResult);
    } catch (e) {
      throw e;
    }
  }
}
