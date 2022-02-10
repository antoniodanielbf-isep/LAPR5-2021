import { ValueObject } from '../../../core/domain/ValueObject';
import { Result } from '../../../core/logic/Result';
import { Guard } from '../../../core/logic/Guard';

interface UtilizadorNomeProps {
  value: string;
}

export class UtilizadorNome extends ValueObject<UtilizadorNomeProps> {
  get value(): string {
    return this.props.value;
  }

  public constructor(props: UtilizadorNomeProps) {
    super(props);
  }

  public static create(nome: string): Result<UtilizadorNome> {
    const guardResult = Guard.againstNullOrUndefined(nome, 'nome');
    if (!guardResult.succeeded) {
      return Result.fail<UtilizadorNome>(guardResult.message);
    } else {
      return Result.ok<UtilizadorNome>(new UtilizadorNome({ value: nome }));
    }
  }
}
