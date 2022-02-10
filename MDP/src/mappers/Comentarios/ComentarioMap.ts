import { Container } from 'typedi';

import { Mapper } from '../../core/infra/Mapper';

import { IComentarioDTO } from '../../dto/Comentarios/IComentarioDTO';

import { Comentario } from '../../domain/Agregados/Comentarios/comentario';
import { UniqueEntityID } from '../../core/domain/UniqueEntityID';

import UtilizadorRepo from '../../repos/utilizadorRepo';
import { Document, Model } from 'mongoose';
import { IPostPersistence } from '../../dataschema/IPostPersistence';
import PostRepo from '../../repos/postRepo';

export class ComentarioMap extends Mapper<Comentario> {
  public static toDTO(comentario: Comentario): IComentarioDTO {
    // eslint-disable-next-line @typescript-eslint/no-object-literal-type-assertion
    return {
      //id: comentario.id.toString(),
      reacao: comentario.reacao.value,
      texto: comentario.texto.value,
      tags: comentario.tags.value,
      utilizador: comentario.utilizador,
      post: comentario.post,
    } as IComentarioDTO;
  }

  public static toStringDTO(
    reacao: string,
    texto: string,
    tags: string,
    utilizador: string,
    post: string,
  ): IComentarioDTO {
    // eslint-disable-next-line @typescript-eslint/no-object-literal-type-assertion
    return {
      //id: id,
      reacao: reacao,
      texto: texto,
      tags: tags,
      utilizador: utilizador,
      post: post,
    } as IComentarioDTO;
  }

  public static async toDomain(comentario: any | Model<IPostPersistence & Document>): Promise<Comentario> {
    const repoU = Container.get(UtilizadorRepo);
    const utilizador = await repoU.findByEmail(comentario.utilizador);
    const repoP = Container.get(PostRepo);
    const post = await repoP.findById(comentario.post);

    try {
      const userId = utilizador.id;
      const postId = post.id;
    } catch (e) {
      throw Error('User/Post not found');
    }

    const comentarioOrError = Comentario.create(comentario, new UniqueEntityID(comentario.domainId));

    comentarioOrError.isFailure ? console.log(comentarioOrError.error) : '';

    return comentarioOrError.isSuccess ? comentarioOrError.getValue() : null;
  }

  public static toPersistence(comentario: Comentario): any {
    const a = {
      domainId: comentario.id.toString(),
      reacao: comentario.reacao.value,
      texto: comentario.texto.value,
      tags: comentario.tags.value,
      utilizador: comentario.utilizador,
      post: comentario.post,
    };
    return a;
  }
}
