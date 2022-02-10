import { AggregateRoot } from '../../../core/domain/AggregateRoot';
import { UniqueEntityID } from '../../../core/domain/UniqueEntityID';
import { Result } from '../../../core/logic/Result';
import { ComentarioId } from '../../ValueObjects/Comentarios/comentarioId';
import { ComentarioTextualTexto } from '../../ValueObjects/Comentarios/comentarioTextualTexto';
import { ComentarioTextualTags } from '../../ValueObjects/Comentarios/comentarioTextualTags';
import { ComentarioReacao } from '../../ValueObjects/Comentarios/comentarioReacao';
import { IComentarioDTO } from '../../../dto/Comentarios/IComentarioDTO';

interface ComentarioProps {
  texto: ComentarioTextualTexto;
  tags: ComentarioTextualTags;
  reacao: ComentarioReacao;
  utilizador: string;
  post: string;
}

export class Comentario extends AggregateRoot<ComentarioProps> {
  get id(): UniqueEntityID {
    return this._id;
  }

  get userId(): ComentarioId {
    return ComentarioId.caller(this.id);
  }

  get texto(): ComentarioTextualTexto {
    return this.props.texto;
  }

  get tags(): ComentarioTextualTags {
    return this.props.tags;
  }

  get reacao(): ComentarioReacao {
    return this.props.reacao;
  }

  get utilizador(): string {
    return this.props.utilizador;
  }

  set utilizador(value: string) {
    this.props.utilizador = value;
  }

  get post(): string {
    return this.props.post;
  }

  private constructor(props: ComentarioProps, id?: UniqueEntityID) {
    super(props, id);
  }

  public static create(comentarioDTO: IComentarioDTO, id?: UniqueEntityID): Result<Comentario> {
    const texto = comentarioDTO.texto;
    const tags = comentarioDTO.tags;
    const reacao = comentarioDTO.reacao;
    const utilizador = comentarioDTO.utilizador;
    const post = comentarioDTO.post;

    if (
      texto === null ||
      texto.length === 0 ||
      tags === null ||
      tags.length === 0 ||
      reacao === null ||
      reacao.length === 0 ||
      utilizador === null ||
      utilizador.length === 0 ||
      post === null ||
      post.length === 0
    ) {
      return Result.fail<Comentario>('Introduza um texto/tags válido!');
    } else {
      const comentario = new Comentario(
        {
          texto: new ComentarioTextualTexto({ value: texto }),
          tags: new ComentarioTextualTags({ value: tags }),
          reacao: new ComentarioReacao({ value: reacao }),
          utilizador: utilizador,
          post: post,
        },
        id,
      );

      return Result.ok<Comentario>(comentario);
    }
  }
}
