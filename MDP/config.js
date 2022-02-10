import dotenv from 'dotenv';

// Set the NODE_ENV to 'development' by default
process.env.NODE_ENV = process.env.NODE_ENV || 'development';

const envFound = dotenv.config();
if (!envFound) {
  // This error should crash whole process

  throw new Error("⚠️  Couldn't find .env file  ⚠️");
}

export default {
  /**
   * Your favorite port
   */
  port: parseInt(process.env.PORT, 10) || 10338,

  /**
   * That long string from mlab
   */
  databaseURL:
    process.env.MONGODB_URI ||
    'mongodb+srv://lapr5_g61:IsepInformatica61*@21s5-61-mdp.ga2so.mongodb.net/lapr5_g61?retryWrites=true&w=majority',

  /**
   * Your secret sauce
   */
  jwtSecret: process.env.JWT_SECRET || 'my sakdfho2390asjod$%jl)!sdjas0i secret',

  /**
   * Used by winston logger
   */
  logs: {
    level: process.env.LOG_LEVEL || 'info',
  },

  /**
   * API configs
   */
  api: {
    prefix: '/api',
  },

  controllers: {
    utilizador: {
      name: 'UtilizadorController',
      path: '../controllers/utilizadorController',
    },
    comentario: {
      name: 'ComentarioController',
      path: '../controllers/comentarioController',
    },
    post: {
      name: 'PostController',
      path: '../controllers/postController',
    },
  },

  repos: {
    utilizador: {
      name: 'UtilizadorRepo',
      path: '../repos/utilizadorRepo',
    },
    comentario: {
      name: 'ComentarioRepo',
      path: '../repos/comentarioRepo',
    },
    post: {
      name: 'PostRepo',
      path: '../repos/postRepo',
    },
  },

  services: {
    utilizador: {
      name: 'UtilizadorService',
      path: '../services/utilizadorService',
    },
    comentario: {
      name: 'ComentarioService',
      path: '../services/comentarioService',
    },
    post: {
      name: 'PostService',
      path: '../services/postService',
    },
  },
};
