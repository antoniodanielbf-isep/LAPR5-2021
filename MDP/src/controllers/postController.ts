import { Request, Response, NextFunction } from 'express';
import { Inject, Service } from 'typedi';
import config from '../../config';

import IPostController from './IControllers/IPostController';
import IPostService from '../services/IServices/IPostService';
import { IPostDTO } from '../dto/Posts/IPostDTO';

import { Result } from '../core/logic/Result';

@Service()
export default class PostController implements IPostController /* TODO: extends ../core/infra/BaseController */ {
  constructor(@Inject(config.services.post.name) private postServiceInstance: IPostService) {}

  public async createPost(req: Request, res: Response, next: NextFunction) {
    try {
      const postOrError = (await this.postServiceInstance.createPost(req.body as IPostDTO)) as Result<IPostDTO>;

      if (postOrError.isFailure) {
        return res.status(400).send();
      }

      const postDTO = postOrError.getValue();
      return res.json(postDTO).status(201);
    } catch (e) {
      return next(e);
    }
  }
}
