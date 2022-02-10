import { Request, Response, NextFunction } from 'express';
import { Inject, Service } from 'typedi';
import config from '../../config';

import IUtilizadorController from './IControllers/IUtilizadorController';
import IUtilizadorService from '../services/IServices/IUtilizadorService';

import IUtilizadorDTO from '../dto/Utilizadores/IUtilizadorDTO';
import IUtilizadorFeedDTO from '../dto/Utilizadores/IUtilizadorFeedDTO';
import IUtilizadorIdDTO from '../dto/Utilizadores/IUtilizadorIdDTO';
import IUtilizadorOrDestDTO from '../dto/Utilizadores/IUtilizadorOrDestDTO';
import IUtilizadorLikeDislikeDTO from '../dto/Utilizadores/IUtilizadorLikeDislikeDTO';
import IUtilizadorEraseDTO from '../dto/Utilizadores/IUtilizadorEraseDTO';

import { Result } from '../core/logic/Result';
import IUtilizadorLikesDislikesDTO from "../dto/Utilizadores/IUtilizadorLikesDislikesDTO";

@Service()
export default class UtilizadorController
  implements IUtilizadorController /* TODO: extends ../core/infra/BaseController */ {
  constructor(@Inject(config.services.utilizador.name) private utilizadorServiceInstance: IUtilizadorService) {}

  public async createUtilizador(req: Request, res: Response, next: NextFunction) {
    try {
      const utilizadorOrError = (await this.utilizadorServiceInstance.createUtilizador(
        req.body as IUtilizadorDTO,
      )) as Result<IUtilizadorDTO>;

      if (utilizadorOrError.isFailure) {
        return res.status(402).send();
      }

      const utilizadorDTO = utilizadorOrError.getValue();
      return res.json(utilizadorDTO).status(201);
    } catch (e) {
      return next(e);
    }
  }

  public async updateUtilizador(req: Request, res: Response, next: NextFunction) {
    try {
      const utilizadorOrError = (await this.utilizadorServiceInstance.updateUtilizador(
        req.body as IUtilizadorEraseDTO,
      )) as Result<IUtilizadorDTO>;

      if (utilizadorOrError.isFailure) {
        return res.status(404).send();
      }

      const utilizadorDTO = utilizadorOrError.getValue();
      return res.status(201).json(utilizadorDTO);
    } catch (e) {
      return next(e);
    }
  }

  public async getFeedUtilizador(req: Request, res: Response, next: NextFunction) {
    try {
      const utilizadorTypeOrError = (await this.utilizadorServiceInstance.getFeedUtilizador(
        req.body as IUtilizadorIdDTO,
      )) as Result<IUtilizadorFeedDTO[]>;

      if (utilizadorTypeOrError.isFailure) {
        return res.status(404).send(utilizadorTypeOrError.errorValue());
      }

      const utilizadorDTO = utilizadorTypeOrError.getValue();
      return res.status(200).json(utilizadorDTO);
    } catch (e) {
      return next(e);
    }
  }

  public async getStrengthUtilizador(req: Request, res: Response, next: NextFunction) {
    try {
      const utilizadorTypeOrError = (await this.utilizadorServiceInstance.getStrengthUtilizador(
        req.body as IUtilizadorOrDestDTO,
      )) as Result<IUtilizadorLikeDislikeDTO>;

      if (utilizadorTypeOrError.isFailure) {
        return res.status(404).send(utilizadorTypeOrError.errorValue());
      }

      const utilizadorDTO = utilizadorTypeOrError.getValue();
      return res.status(200).json(utilizadorDTO);
    } catch (e) {
      return next(e);
    }
  }

  public async getLikesDislikesUtilizador(req: Request, res: Response, next: NextFunction) {
    try {
      const utilizadorTypeOrError = (await this.utilizadorServiceInstance.getLikeDislikeUtilizador(
        req.body as IUtilizadorIdDTO,
      )) as Result<IUtilizadorLikesDislikesDTO>;

      if (utilizadorTypeOrError.isFailure) {
        return res.status(404).send(utilizadorTypeOrError.errorValue());
      }

      const utilizadorDTO = utilizadorTypeOrError.getValue();
      return res.status(200).json(utilizadorDTO);
    } catch (e) {
      return next(e);
    }
  }
}
