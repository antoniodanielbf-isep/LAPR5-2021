import { Router } from 'express';
import { celebrate, Joi } from 'celebrate';

import { Container } from 'typedi';
import IPostController from '../../controllers/IControllers/IPostController';

import config from '../../../config';

const route = Router();

export default (app: Router) => {
  app.use('/posts', route);

  const ctrl = Container.get(config.controllers.post.name) as IPostController;

  route.post(
    '',
    celebrate({
      body: Joi.object({
        texto: Joi.string().required(),
        tags: Joi.string().required(),
        utilizador: Joi.string().required(),
      }),
    }),
    (req, res, next) => ctrl.createPost(req, res, next),
  );
};
