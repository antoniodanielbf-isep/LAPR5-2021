import { Request, Response, NextFunction } from 'express';

export default interface IPostController {
  createPost(req: Request, res: Response, next: NextFunction);
}
