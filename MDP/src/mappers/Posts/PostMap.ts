import { Mapper } from '../../core/infra/Mapper';

import { Document, Model } from 'mongoose';
import { IPostPersistence } from '../../dataschema/IPostPersistence';

import { IPostDTO } from '../../dto/Posts/IPostDTO';
import { Post } from '../../domain/Agregados/Posts/post';

import { UniqueEntityID } from '../../core/domain/UniqueEntityID';
import { Container } from 'typedi';
import UtilizadorRepo from '../../repos/utilizadorRepo';

export class PostMap extends Mapper<Post> {
  public static toDTO(post: Post): IPostDTO {
    // eslint-disable-next-line @typescript-eslint/no-object-literal-type-assertion
    return {
      id: post.id.toString(),
      texto: post.texto.value,
      tags: post.tag.value,
      utilizador: post.utilizador,
    } as IPostDTO;
  }

  public static toStringDTO(id: string, texto: string, tags: string, utilizador: string): IPostDTO {
    // eslint-disable-next-line @typescript-eslint/no-object-literal-type-assertion
    return {
      id: id,
      texto: texto,
      tags: tags,
      utilizador: utilizador,
    } as IPostDTO;
  }

  public static async toDomain(post: any | Model<IPostPersistence & Document>): Promise<Post> {
    const repo = Container.get(UtilizadorRepo);
    const utilizador = await repo.findByEmail(post.utilizador);

    try {
      const userId = utilizador.id;
    } catch (e) {
      throw Error('User not found');
    }

    const postOrError = Post.create(post, new UniqueEntityID(post.domainId));

    postOrError.isFailure ? console.log(postOrError.error) : '';

    return postOrError.isSuccess ? postOrError.getValue() : null;
  }

  public static toPersistence(post: Post): any {
    const a = {
      domainId: post.id.toString(),
      texto: post.texto.value,
      tags: post.tag.value,
      utilizador: post.utilizador,
    };
    return a;
  }
}
