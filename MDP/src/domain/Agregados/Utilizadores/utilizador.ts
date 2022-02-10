import { AggregateRoot } from '../../../core/domain/AggregateRoot';
import { UniqueEntityID } from '../../../core/domain/UniqueEntityID';
import { Result } from '../../../core/logic/Result';
import { UtilizadorId } from '../../ValueObjects/Utilizadores/utilizadorId';
import { UtilizadorEmail } from '../../ValueObjects/Utilizadores/utilizadorEmail';
import { UtilizadorNome } from '../../ValueObjects/Utilizadores/utilizadorNome';
import IUtilizadorDTO from '../../../dto/Utilizadores/IUtilizadorDTO';

interface UtilizadorProps {
  email: UtilizadorEmail;
  nome: UtilizadorNome;
}

export class Utilizador extends AggregateRoot<UtilizadorProps> {
  get id(): UniqueEntityID {
    return this._id;
  }

  get userId(): UtilizadorId {
    return UtilizadorId.caller(this.id);
  }

  get email(): UtilizadorEmail {
    return this.props.email;
  }

  set email(value: UtilizadorEmail) {
    this.props.email = value;
  }

  get nome(): UtilizadorNome {
    return this.props.nome;
  }

  set nome(value: UtilizadorNome) {
    this.props.nome = value;
  }

  private constructor(props: UtilizadorProps, id?: UniqueEntityID) {
    super(props, id);
  }

  public static create(utilizadorDTO: IUtilizadorDTO, id?: UniqueEntityID): Result<Utilizador> {
    const email = utilizadorDTO.email;
    const nome = utilizadorDTO.nome;

    if (
      email === null ||
      !!email === false ||
      email.length === 0 ||
      nome === null ||
      !!nome === false ||
      nome.length === 0
    ) {
      return Result.fail<Utilizador>('Introduza um email/nome válido!');
    } else {
      const user = new Utilizador(
        {
          email: new UtilizadorEmail({ value: email }),
          nome: new UtilizadorNome({ value: nome }),
        },
        id,
      );
      return Result.ok<Utilizador>(user);
    }
  }
}
