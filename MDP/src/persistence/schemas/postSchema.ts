import { IPostPersistence } from '../../dataschema/IPostPersistence';
import mongoose from 'mongoose';

const Post = new mongoose.Schema(
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

    utilizador: {
      type: String,
      required: [true, 'Please enter a user'],
      index: true,
    },
  },
  { timestamps: true },
);

export default mongoose.model<IPostPersistence & mongoose.Document>('Post', Post);
