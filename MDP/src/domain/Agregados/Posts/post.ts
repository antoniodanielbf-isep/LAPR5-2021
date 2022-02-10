import { AggregateRoot } from '../../../core/domain/AggregateRoot';
import { UniqueEntityID } from '../../../core/domain/UniqueEntityID';
import { Result } from '../../../core/logic/Result';
import { PostId } from '../../ValueObjects/Posts/postId';
import { PostTexto } from '../../ValueObjects/Posts/postTexto';
import { PostTag } from '../../ValueObjects/Posts/postTag';
import { IPostDTO } from '../../../dto/Posts/IPostDTO';

interface PostProps {
  texto: PostTexto;
  tags: PostTag;
  utilizador: string;
}

export class Post extends AggregateRoot<PostProps> {
  get id(): UniqueEntityID {
    return this._id;
  }

  get userId(): PostId {
    return PostId.caller(this.id);
  }

  get texto(): PostTexto {
    return this.props.texto;
  }

  get tag(): PostTag {
    return this.props.tags;
  }

  get utilizador(): string {
    return this.props.utilizador;
  }

  set utilizador(value: string) {
    this.props.utilizador = value;
  }

  private constructor(props: PostProps, id?: UniqueEntityID) {
    super(props, id);
  }

  public static create(postDTO: IPostDTO, id?: UniqueEntityID): Result<Post> {
    const texto = postDTO.texto;
    const tags = postDTO.tags;
    const utilizador = postDTO.utilizador;

    if (
      texto === null ||
      texto.length === 0 ||
      tags === null ||
      tags.length === 0 ||
      utilizador === null ||
      utilizador.length === 0
    ) {
      return Result.fail<Post>('Introduza um texto/tags válido!');
    } else {
      const post = new Post(
        {
          texto: new PostTexto({ value: texto }),
          tags: new PostTag({ value: tags }),
          utilizador: utilizador,
        },
        id,
      );

      return Result.ok<Post>(post);
    }
  }
}
