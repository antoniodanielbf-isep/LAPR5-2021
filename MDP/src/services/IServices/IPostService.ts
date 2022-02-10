import { Result } from '../../core/logic/Result';
import { IPostDTO } from '../../dto/Posts/IPostDTO';

export default interface IPostService {
  createPost(postDTO: IPostDTO): Promise<Result<IPostDTO>>;
  getPost(postId: string): Promise<Result<IPostDTO>>;
}
