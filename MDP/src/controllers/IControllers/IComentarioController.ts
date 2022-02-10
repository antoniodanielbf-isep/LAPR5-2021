import { Request, Response, NextFunction } from 'express';

export default interface IComentarioController {
  createComentario(req: Request, res: Response, next: NextFunction);
}
