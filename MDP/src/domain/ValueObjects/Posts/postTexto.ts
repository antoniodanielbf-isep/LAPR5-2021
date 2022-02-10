import { ValueObject } from '../../../core/domain/ValueObject';
import { Result } from '../../../core/logic/Result';
import { Guard } from '../../../core/logic/Guard';

interface PostTextoProps {
  value: string;
}

export class PostTexto extends ValueObject<PostTextoProps> {
  get value(): string {
    return this.props.value;
  }

  public constructor(props: PostTextoProps) {
    super(props);
  }

  public static create(texto: string): Result<PostTexto> {
    const guardResult = Guard.againstNullOrUndefined(texto, 'texto');
    if (!guardResult.succeeded) {
      return Result.fail<PostTexto>(guardResult.message);
    } else {
      return Result.ok<PostTexto>(new PostTexto({ value: texto }));
    }
  }
}
