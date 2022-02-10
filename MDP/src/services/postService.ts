import { Container, Inject, Service } from 'typedi';
import config from '../../config';
import IPostRepo from '../services/IRepos/IPostRepo';
import { Result } from '../core/logic/Result';
import IPostService from './IServices/IPostService';
import { IPostDTO } from '../dto/Posts/IPostDTO';
import { PostMap } from '../mappers/Posts/PostMap';
import { Post } from '../domain/Agregados/Posts/post';
import UtilizadorRepo from '../repos/utilizadorRepo';

@Service()
export default class PostService implements IPostService {
  constructor(@Inject(config.repos.post.name) private postRepo: IPostRepo) {}

  public async getPost(postId: string): Promise<Result<IPostDTO>> {
    try {
      const post = await this.postRepo.findById(postId);

      if (post === null) {
        return Result.fail<IPostDTO>('Post not found');
      } else {
        const postDTOResult = PostMap.toDTO(post) as IPostDTO;
        return Result.ok<IPostDTO>(postDTOResult);
      }
    } catch (e) {
      throw e;
    }
  }

  public async createPost(postDTO: IPostDTO): Promise<Result<IPostDTO>> {
    try {
      const repo = Container.get(UtilizadorRepo);
      const utilizador = await repo.findByEmail(postDTO.utilizador);

      try {
        const userId = utilizador.id;
      } catch (e) {
        throw Error('User not found');
      }

      const postOrError = Post.create(postDTO);

      if (postOrError.isFailure) {
        return Result.fail<IPostDTO>(postOrError.errorValue());
      }

      const postResult = postOrError.getValue();

      await this.postRepo.save(postResult);

      const postDTOResult = PostMap.toDTO(postResult) as IPostDTO;
      return Result.ok<IPostDTO>(postDTOResult);
    } catch (e) {
      throw e;
    }
  }
}
