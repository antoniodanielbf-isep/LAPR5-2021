import { Repo } from '../../core/infra/Repo';
import { Utilizador } from '../../domain/Agregados/Utilizadores/utilizador';
import { UtilizadorId } from '../../domain/ValueObjects/Utilizadores/utilizadorId';

export default interface IUtilizadorRepo extends Repo<Utilizador> {
  save(utilizador: Utilizador): Promise<Utilizador>;
  findByDomainId(utilizadorId: UtilizadorId | string): Promise<Utilizador>;
  findAll(): Promise<Utilizador[]>;
}
