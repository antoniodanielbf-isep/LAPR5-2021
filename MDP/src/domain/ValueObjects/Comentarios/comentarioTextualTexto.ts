import { ValueObject } from '../../../core/domain/ValueObject';
import { Result } from '../../../core/logic/Result';
import { Guard } from '../../../core/logic/Guard';

interface ComentarioTextualTextoProps {
  value: string;
}

export class ComentarioTextualTexto extends ValueObject<ComentarioTextualTextoProps> {
  get value(): string {
    return this.props.value;
  }

  public constructor(props: ComentarioTextualTextoProps) {
    super(props);
  }

  public static create(texto: string): Result<ComentarioTextualTexto> {
    const guardResult = Guard.againstNullOrUndefined(texto, 'texto');
    if (!guardResult.succeeded) {
      return Result.fail<ComentarioTextualTexto>(guardResult.message);
    } else {
      return Result.ok<ComentarioTextualTexto>(new ComentarioTextualTexto({ value: texto }));
    }
  }
}
