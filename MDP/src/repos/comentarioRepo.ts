import { Service, Inject } from 'typedi';

import { Document, Model } from 'mongoose';
import { IComentarioPersistence } from '../dataschema/IComentarioPersistence';

import IComentarioRepo from '../services/IRepos/IComentarioRepo';
import { Comentario } from '../domain/Agregados/Comentarios/comentario';
import { ComentarioId } from '../domain/ValueObjects/Comentarios/comentarioId';
import { ComentarioMap } from '../mappers/Comentarios/ComentarioMap';

@Service()
export default class ComentarioRepo implements IComentarioRepo {
  private models: any;

  constructor(@Inject('comentarioSchema') private comentarioSchema: Model<IComentarioPersistence & Document>) {}

  private createBaseQuery(): any {
    return {
      where: {},
    };
  }

  public async exists(comentarioId: ComentarioId | string): Promise<boolean> {
    // eslint-disable-next-line @typescript-eslint/no-angle-bracket-type-assertion
    const idX = comentarioId instanceof ComentarioId ? (<ComentarioId>comentarioId).id.toValue() : comentarioId;

    const query = { domainId: idX };
    const comentarioDocument = await this.comentarioSchema.findOne(query);

    return !!comentarioDocument === true;
  }

  public async save(comentario: Comentario): Promise<Comentario> {
    const query = { domainId: comentario.id.toString() };

    const comentarioDocument = await this.comentarioSchema.findOne(query);

    try {
      if (comentarioDocument === null) {
        const rawComentario: any = ComentarioMap.toPersistence(comentario);

        const comentarioCreated = await this.comentarioSchema.create(rawComentario);

        return ComentarioMap.toDomain(comentarioCreated);
      } else {
        comentarioDocument.reacao = comentario.reacao.value;
        comentarioDocument.texto = comentario.texto.value;
        comentarioDocument.tags = comentario.tags.value;
        comentarioDocument.utilizador = comentario.utilizador;
        comentarioDocument.post = comentario.post;

        await comentarioDocument.save();

        return comentario;
      }
    } catch (err) {
      throw err;
    }
  }

  public async findById(comentarioId: ComentarioId | string): Promise<Comentario> {
    // eslint-disable-next-line @typescript-eslint/no-angle-bracket-type-assertion
    const idX = comentarioId instanceof ComentarioId ? (<ComentarioId>comentarioId).id.toValue() : comentarioId;

    const query = { domainId: idX };
    const comentarioRecord = await this.comentarioSchema.findOne(query);

    if (comentarioRecord != null) {
      return ComentarioMap.toDomain(comentarioRecord);
    } else return null;
  }

  public async findAll(): Promise<Comentario[]> {
    const comentarioRecord = await this.comentarioSchema.find({});

    var comentarioArray = [];
    try {
      if (comentarioRecord === null) {
        return null;
      } else {
        for (let i = 0; i < comentarioRecord.length; i++) {
          comentarioArray[i] = await ComentarioMap.toDomain(comentarioRecord[i]);
        }
        return comentarioArray;
      }
    } catch (err) {
      throw err;
    }
  }
}
