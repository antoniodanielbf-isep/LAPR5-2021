import expressLoader from './express';
import dependencyInjectorLoader from './dependencyInjector';
import mongooseLoader from './mongoose';
import Logger from './logger';

import config from '../../config';

export default async ({ expressApp }) => {
  const mongoConnection = await mongooseLoader();
  Logger.info('✌️ DB loaded and connected!');

  const utilizadorSchema = {
    // compare with the approach followed in repos and services
    name: 'utilizadorSchema',
    schema: '../persistence/schemas/utilizadorSchema',
  };

  const comentarioSchema = {
    // compare with the approach followed in repos and services
    name: 'comentarioSchema',
    schema: '../persistence/schemas/comentarioSchema',
  };

  const postSchema = {
    // compare with the approach followed in repos and services
    name: 'postSchema',
    schema: '../persistence/schemas/postSchema',
  };

  const utilizadorController = {
    name: config.controllers.utilizador.name,
    path: config.controllers.utilizador.path,
  };

  const comentarioController = {
    name: config.controllers.comentario.name,
    path: config.controllers.comentario.path,
  };

  const postController = {
    name: config.controllers.post.name,
    path: config.controllers.post.path,
  };

  const utilizadorRepo = {
    name: config.repos.utilizador.name,
    path: config.repos.utilizador.path,
  };

  const comentarioRepo = {
    name: config.repos.comentario.name,
    path: config.repos.comentario.path,
  };

  const postRepo = {
    name: config.repos.post.name,
    path: config.repos.post.path,
  };

  const utilizadorService = {
    name: config.services.utilizador.name,
    path: config.services.utilizador.path,
  };

  const comentarioService = {
    name: config.services.comentario.name,
    path: config.services.comentario.path,
  };

  const postService = {
    name: config.services.post.name,
    path: config.services.post.path,
  };

  await dependencyInjectorLoader({
    mongoConnection,
    schemas: [utilizadorSchema, comentarioSchema, postSchema],
    controllers: [utilizadorController, comentarioController, postController],
    repos: [utilizadorRepo, comentarioRepo, postRepo],
    services: [utilizadorService, comentarioService, postService],
  });
  Logger.info('✌️ Schemas, Controllers, Repositories, Services, etc. loaded');

  await expressLoader({ app: expressApp });
  Logger.info('✌️ Express loaded');
};
