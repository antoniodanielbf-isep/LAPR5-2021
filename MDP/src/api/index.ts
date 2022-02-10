import { Router } from 'express';
import utilizador from './routes/utilizadorRoute';
import comentario from './routes/comentarioRoute';
import post from './routes/postRoute';

export default () => {
  const app = Router();

  utilizador(app);
  comentario(app);
  post(app);

  return app;
};
