import { ValueObject } from '../../../core/domain/ValueObject';
import { Result } from '../../../core/logic/Result';
import { Guard } from '../../../core/logic/Guard';

interface ComentarioTextualTagProps {
  value: string;
}

export class ComentarioTextualTags extends ValueObject<ComentarioTextualTagProps> {
  get value(): string {
    return this.props.value;
  }

  public constructor(props: ComentarioTextualTagProps) {
    super(props);
  }

  public static create(tag: string): Result<ComentarioTextualTags> {
    const guardResult = Guard.againstNullOrUndefined(tag, 'tag');
    if (!guardResult.succeeded) {
      return Result.fail<ComentarioTextualTags>(guardResult.message);
    } else {
      return Result.ok<ComentarioTextualTags>(new ComentarioTextualTags({ value: tag }));
    }
  }
}
