import { ValueObject } from '../../../core/domain/ValueObject';
import { Result } from '../../../core/logic/Result';
import { Guard } from '../../../core/logic/Guard';

interface UtilizadorEmailProps {
  value: string;
}

export class UtilizadorEmail extends ValueObject<UtilizadorEmailProps> {
  get value(): string {
    return this.props.value;
  }

  public constructor(props: UtilizadorEmailProps) {
    super(props);
  }

  public static create(email: string): Result<UtilizadorEmail> {
    const guardResult = Guard.againstNullOrUndefined(email, 'email');
    if (!guardResult.succeeded) {
      return Result.fail<UtilizadorEmail>(guardResult.message);
    } else {
      return Result.ok<UtilizadorEmail>(new UtilizadorEmail({ value: email }));
    }
  }
}
