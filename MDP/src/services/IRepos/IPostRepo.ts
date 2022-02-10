import { Repo } from '../../core/infra/Repo';
import { Post } from '../../domain/Agregados/Posts/post';

export default interface IPostRepo extends Repo<Post> {
  save(post: Post): Promise<Post>;
  findById(id: string): Promise<Post>;
  findAll(): Promise<Post[]>;
}
