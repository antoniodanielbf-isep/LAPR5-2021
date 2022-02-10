import { Request, Response, NextFunction } from 'express';
import { Inject, Service } from 'typedi';
import config from '../../config';

import IComentarioController from './IControllers/IComentarioController';
import IComentarioService from '../services/IServices/IComentarioService';
import { IComentarioDTO } from '../dto/Comentarios/IComentarioDTO';

import { Result } from '../core/logic/Result';

@Service()
export default class ComentarioController
  implements IComentarioController /* TODO: extends ../core/infra/BaseController */ {
  constructor(@Inject(config.services.comentario.name) private comentarioServiceInstance: IComentarioService) {}

  public async createComentario(req: Request, res: Response, next: NextFunction) {
    try {
      const comentarioOrError = (await this.comentarioServiceInstance.createComentario(
        req.body as IComentarioDTO,
      )) as Result<IComentarioDTO>;

      if (comentarioOrError.isFailure) {
        return res.status(400).send();
      }

      const comentarioDTO = comentarioOrError.getValue();
      return res.json(comentarioDTO).status(201);
    } catch (e) {
      return next(e);
    }
  }
}
