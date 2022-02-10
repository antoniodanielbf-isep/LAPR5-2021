import { Result } from '../../../src/core/logic/Result';
import { Utilizador } from '../../../src/domain/Agregados/Utilizadores/utilizador';
import { UtilizadorMap } from '../../../src/mappers/Utilizadores/UtilizadorMap';

describe('Domain Utilizadores', () => {
  beforeEach(() => {});
});

test('Não criar user com email null', () => {
  expect(Utilizador.create(UtilizadorMap.toStringDTO('', null, 'Nome'))).toStrictEqual(
    Result.fail<Utilizador>('Introduza um email/nome válido!'),
  );
});

test('Não criar user com nome null', () => {
  expect(Utilizador.create(UtilizadorMap.toStringDTO('', 'Email', null))).toStrictEqual(
    Result.fail<Utilizador>('Introduza um email/nome válido!'),
  );
});

test('Não criar user com email vazio', () => {
  expect(Utilizador.create(UtilizadorMap.toStringDTO('', '', 'Nome'))).toStrictEqual(
    Result.fail<Utilizador>('Introduza um email/nome válido!'),
  );
});

test('Não criar user com nome vazio', () => {
  expect(Utilizador.create(UtilizadorMap.toStringDTO('', 'Email', ''))).toStrictEqual(
    Result.fail<Utilizador>('Introduza um email/nome válido!'),
  );
});

test('Criar user com dados corretos', () => {
  expect(
    UtilizadorMap.toDTO(Utilizador.create(UtilizadorMap.toStringDTO('', 'Email', 'Nome')).getValue()).nome,
  ).toStrictEqual(UtilizadorMap.toStringDTO('', 'Email', 'Nome').nome);
});
