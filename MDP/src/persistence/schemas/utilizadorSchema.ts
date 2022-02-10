import { IUtilizadorPersistence } from '../../dataschema/IUtilizadorPersistence';
import mongoose from 'mongoose';

const Utilizador = new mongoose.Schema(
  {
    domainId: {
      type: String,
      unique: true,
    },

    email: {
      type: String,
      lowercase: true,
      unique: true,
      required: [true, 'Please enter an email'],
      index: true,
    },

    nome: {
      type: String,
      required: [true, 'Please enter a name'],
      index: true,
    },
  },
  { timestamps: true },
);

export default mongoose.model<IUtilizadorPersistence & mongoose.Document>('Utilizador', Utilizador);
