import { Mapper } from '../../core/infra/Mapper';

import { Document, Model } from 'mongoose';
import { IUtilizadorPersistence } from '../../dataschema/IUtilizadorPersistence';

import IUtilizadorDTO from '../../dto/Utilizadores/IUtilizadorDTO';
import IUtilizadorFeedDTO from '../../dto/Utilizadores/IUtilizadorFeedDTO';

import { Utilizador } from '../../domain/Agregados/Utilizadores/utilizador';
import { Post } from '../../domain/Agregados/Posts/post';

import { UniqueEntityID } from '../../core/domain/UniqueEntityID';
import IUtilizadorLikeDislikeDTO from '../../dto/Utilizadores/IUtilizadorLikeDislikeDTO';
import IUtilizadorLikesDislikesDTO from "../../dto/Utilizadores/IUtilizadorLikesDislikesDTO";

export class UtilizadorMap extends Mapper<Utilizador> {
  public static toDTO(utilizador: Utilizador): IUtilizadorDTO {
    // eslint-disable-next-line @typescript-eslint/no-object-literal-type-assertion
    return {
      id: utilizador.id.toString(),
      email: utilizador.email.value,
      nome: utilizador.nome.value,
    } as IUtilizadorDTO;
  }

  public static toStringDTO(id: string, email: string, nome: string): IUtilizadorDTO {
    // eslint-disable-next-line @typescript-eslint/no-object-literal-type-assertion
    return {
      id: id,
      email: email,
      nome: nome,
    } as IUtilizadorDTO;
  }

  public static toFeedDTO(post: Post, array: string[]): IUtilizadorFeedDTO {
    // eslint-disable-next-line @typescript-eslint/no-object-literal-type-assertion
    return {
      post: post.texto.value,
      postUser: post.utilizador,
      postId: post.id.toString(),
      comentarios: array,
    } as IUtilizadorFeedDTO;
  }

  public static toLikeDislikeDTO(valor: number): IUtilizadorLikeDislikeDTO {
    // eslint-disable-next-line @typescript-eslint/no-object-literal-type-assertion
    return {
      valor: valor,
    } as IUtilizadorLikeDislikeDTO;
  }

  public static toLikesDislikesDTO(likes: number, dislikes: number): IUtilizadorLikesDislikesDTO {
    // eslint-disable-next-line @typescript-eslint/no-object-literal-type-assertion
    return {
      likes: likes,
      dislikes: dislikes,
    } as IUtilizadorLikesDislikesDTO;
  }

  public static toDomain(utilizador: any | Model<IUtilizadorPersistence & Document>): Utilizador {
    const utilizadorOrError = Utilizador.create(utilizador, new UniqueEntityID(utilizador.domainId));

    utilizadorOrError.isFailure ? console.log(utilizadorOrError.error) : '';

    return utilizadorOrError.isSuccess ? utilizadorOrError.getValue() : null;
  }

  public static toPersistence(utilizador: Utilizador): any {
    return {
      domainId: utilizador.id.toString(),
      email: utilizador.email.value,
      nome: utilizador.nome.value,
    };
  }
}
