import { ValueObject } from '../../../core/domain/ValueObject';
import { Result } from '../../../core/logic/Result';
import { Guard } from '../../../core/logic/Guard';

interface PostTagProps {
  value: string;
}

export class PostTag extends ValueObject<PostTagProps> {
  get value(): string {
    return this.props.value;
  }

  public constructor(props: PostTagProps) {
    super(props);
  }

  public static create(tag: string): Result<PostTag> {
    const guardResult = Guard.againstNullOrUndefined(tag, 'tag');
    if (!guardResult.succeeded) {
      return Result.fail<PostTag>(guardResult.message);
    } else {
      return Result.ok<PostTag>(new PostTag({ value: tag }));
    }
  }
}
