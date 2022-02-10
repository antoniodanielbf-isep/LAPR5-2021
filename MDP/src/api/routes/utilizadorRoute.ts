import { Router } from 'express';
import { celebrate, Joi } from 'celebrate';

import { Container } from 'typedi';
import IUtilizadorController from '../../controllers/IControllers/IUtilizadorController';

import config from '../../../config';

const route = Router();

export default (app: Router) => {
  app.use('/utilizadores', route);

  const ctrl = Container.get(config.controllers.utilizador.name) as IUtilizadorController;

  route.post(
    '',
    celebrate({
      body: Joi.object({
        email: Joi.string().required(),
        nome: Joi.string().required(),
      }),
    }),
    (req, res, next) => ctrl.createUtilizador(req, res, next),
  );

  route.post(
    '/erase',
    celebrate({
      body: Joi.object({
        old: Joi.string().required(),
        novo: Joi.string().required(),
      }),
    }),
    (req, res, next) => ctrl.updateUtilizador(req, res, next),
  );

  route.post(
    '/getFeed',
    celebrate({
      body: Joi.object({
        email: Joi.string().required(),
      }),
    }),
    (req, res, next) => ctrl.getFeedUtilizador(req, res, next),
  );

  route.post(
    '/getStrength',
    celebrate({
      body: Joi.object({
        emailOrigem: Joi.string().required(),
        emailDestino: Joi.string().required(),
      }),
    }),
    (req, res, next) => ctrl.getStrengthUtilizador(req, res, next),
  );

  route.post(
    '/getLikesDislike',
    celebrate({
      body: Joi.object({
        email: Joi.string().required(),
      }),
    }),
    (req, res, next) => ctrl.getLikesDislikesUtilizador(req, res, next),
  );
};
