import { ValueObject } from '../../../core/domain/ValueObject';
import { Result } from '../../../core/logic/Result';
import { Guard } from '../../../core/logic/Guard';

interface ComentarioReacaoProps {
  value: string;
}

export class ComentarioReacao extends ValueObject<ComentarioReacaoProps> {
  get value(): string {
    return this.props.value;
  }

  public constructor(props: ComentarioReacaoProps) {
    super(props);
  }

  public static create(comentario: string): Result<ComentarioReacao> {
    const guardResult = Guard.againstNullOrUndefined(comentario, 'comentario');
    if (!guardResult.succeeded) {
      return Result.fail<ComentarioReacao>(guardResult.message);
    } else {
      return Result.ok<ComentarioReacao>(new ComentarioReacao({ value: comentario }));
    }
  }
}
