import { Service, Inject } from 'typedi';

import IUtilizadorRepo from '../services/IRepos/IUtilizadorRepo';
import { Utilizador } from '../domain/Agregados/Utilizadores/utilizador';
import { UtilizadorId } from '../domain/ValueObjects/Utilizadores/utilizadorId';
import { UtilizadorMap } from '../mappers/Utilizadores/UtilizadorMap';

import { Document, FilterQuery, Model } from 'mongoose';
import { IUtilizadorPersistence } from '../dataschema/IUtilizadorPersistence';
import { UtilizadorEmail } from '../domain/ValueObjects/Utilizadores/utilizadorEmail';

@Service()
export default class UtilizadorRepo implements IUtilizadorRepo {
  constructor(@Inject('utilizadorSchema') private utilizadorSchema: Model<IUtilizadorPersistence & Document>) {}

  private createBaseQuery(): any {
    return {
      where: {},
    };
  }

  public async exists(utilizador: Utilizador): Promise<boolean> {
    // eslint-disable-next-line @typescript-eslint/no-angle-bracket-type-assertion
    const idX = utilizador.id instanceof UtilizadorId ? (<UtilizadorId>utilizador.id).toValue() : utilizador.id;

    const query = { domainId: idX };
    const utilizadorDocument = await this.utilizadorSchema.findOne(
      query as FilterQuery<IUtilizadorPersistence & Document>,
    );

    return !!utilizadorDocument === true;
  }

  public async save(utilizador: Utilizador): Promise<Utilizador> {
    const query = { domainId: utilizador.id.toString() };

    const utilizadorDocument = await this.utilizadorSchema.findOne(query);

    try {
      if (utilizadorDocument === null) {
        const rawUtilizador: any = UtilizadorMap.toPersistence(utilizador);

        const utilizadorCreated = await this.utilizadorSchema.create(rawUtilizador);

        return UtilizadorMap.toDomain(utilizadorCreated);
      } else {
        utilizadorDocument.email = utilizador.email.value;
        utilizadorDocument.nome = utilizador.nome.value;
        await utilizadorDocument.save();

        return utilizador;
      }
    } catch (err) {
      throw err;
    }
  }

  public async findByDomainId(utilizadorId: UtilizadorId | string): Promise<Utilizador> {
    const query = { domainId: utilizadorId };
    const utilizadorRecord = await this.utilizadorSchema.findOne(
      query as FilterQuery<IUtilizadorPersistence & Document>,
    );

    if (utilizadorRecord != null) {
      return UtilizadorMap.toDomain(utilizadorRecord);
    } else return null;
  }

  public async findByEmail(utilizadorEmail: UtilizadorEmail | string): Promise<Utilizador> {
    const query = { email: utilizadorEmail };
    const utilizadorRecord = await this.utilizadorSchema.findOne(
      query as FilterQuery<IUtilizadorPersistence & Document>,
    );

    if (utilizadorRecord != null) {
      return UtilizadorMap.toDomain(utilizadorRecord);
    } else return null;
  }

  public async findAll(): Promise<Utilizador[]> {
    const utilizadorRecord = await this.utilizadorSchema.find({});

    var utilizadorArray = [];
    try {
      if (utilizadorRecord === null) {
        return null;
      } else {
        for (let i = 0; i < utilizadorRecord.length; i++) {
          utilizadorArray[i] = await UtilizadorMap.toDomain(utilizadorRecord[i]);
        }
        return utilizadorArray;
      }
    } catch (err) {
      throw err;
    }
  }
}
