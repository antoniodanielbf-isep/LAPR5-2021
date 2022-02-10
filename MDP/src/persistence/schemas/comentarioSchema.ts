import mongoose from 'mongoose';
import { IComentarioPersistence } from '../../dataschema/IComentarioPersistence';

const Comentario = new mongoose.Schema(
  {
    domainId: {
      type: String,
      unique: true,
    },

    texto: {
      type: String,
      required: [true, 'Please enter a text'],
      index: true,
    },

    tags: {
      type: String,
      required: false,
      index: true,
    },

    reacao: {
      type: String,
      required: [true, 'Please enter a reaccion'],
      index: true,
    },

    utilizador: {
      type: String,
      required: [true, 'Please enter a user'],
      index: true,
    },

    post: {
      type: String,
      required: [true, 'Please enter a post'],
      index: true,
    },
  },
  { timestamps: true },
);

export default mongoose.model<IComentarioPersistence & mongoose.Document>('Comentario', Comentario);
