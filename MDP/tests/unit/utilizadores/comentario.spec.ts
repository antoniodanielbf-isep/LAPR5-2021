import { Result } from '../../../src/core/logic/Result';
import { Comentario } from '../../../src/domain/Agregados/Comentarios/comentario';
import { ComentarioMap } from '../../../src/mappers/Comentarios/ComentarioMap';

describe('Domain Comentarios', () => {
  beforeEach(() => {});
});

test('Não criar comentario com reacao null', () => {
  expect(Comentario.create(ComentarioMap.toStringDTO(null, 'Texto', 'Tags', 'User', 'Post'))).toStrictEqual(
    Result.fail<Comentario>('Introduza um texto/tags válido!'),
  );
});

test('Não criar comentario com texto null', () => {
  expect(Comentario.create(ComentarioMap.toStringDTO('Reacao', null, 'Tags', 'User', 'Post'))).toStrictEqual(
    Result.fail<Comentario>('Introduza um texto/tags válido!'),
  );
});

test('Não criar comentario com tags null', () => {
  expect(Comentario.create(ComentarioMap.toStringDTO('Reacao', 'Texto', null, 'User', 'Post'))).toStrictEqual(
    Result.fail<Comentario>('Introduza um texto/tags válido!'),
  );
});

test('Não criar comentario com utilizador null', () => {
  expect(Comentario.create(ComentarioMap.toStringDTO('Reacao', 'Texto', 'Tags', null, 'Post'))).toStrictEqual(
    Result.fail<Comentario>('Introduza um texto/tags válido!'),
  );
});

test('Não criar comentario com post null', () => {
  expect(Comentario.create(ComentarioMap.toStringDTO('Reacao', 'Texto', 'Tags', 'User', null))).toStrictEqual(
    Result.fail<Comentario>('Introduza um texto/tags válido!'),
  );
});

test('Não criar comentario com reacao vazia', () => {
  expect(Comentario.create(ComentarioMap.toStringDTO('', 'Texto', 'Tags', 'User', 'Post'))).toStrictEqual(
    Result.fail<Comentario>('Introduza um texto/tags válido!'),
  );
});

test('Não criar comentario com texto vazio', () => {
  expect(Comentario.create(ComentarioMap.toStringDTO('Reacao', '', 'Tags', 'User', 'Post'))).toStrictEqual(
    Result.fail<Comentario>('Introduza um texto/tags válido!'),
  );
});

test('Não criar comentario com tags vazias', () => {
  expect(Comentario.create(ComentarioMap.toStringDTO('Reacao', 'Texto', '', 'User', 'Post'))).toStrictEqual(
    Result.fail<Comentario>('Introduza um texto/tags válido!'),
  );
});

test('Não criar comentario com utilizador vazio', () => {
  expect(Comentario.create(ComentarioMap.toStringDTO('Reacao', 'Texto', 'Tags', '', 'Post'))).toStrictEqual(
    Result.fail<Comentario>('Introduza um texto/tags válido!'),
  );
});

test('Não criar comentario com post vazio', () => {
  expect(Comentario.create(ComentarioMap.toStringDTO('Reacao', 'Texto', 'Tags', 'User', ''))).toStrictEqual(
    Result.fail<Comentario>('Introduza um texto/tags válido!'),
  );
});

test('Criar comentario com dados corretos', () => {
  expect(
    ComentarioMap.toDTO(
      Comentario.create(ComentarioMap.toStringDTO('Reacao', 'Texto', 'Tags', 'User', 'Post')).getValue(),
    ).reacao,
  ).toStrictEqual(ComentarioMap.toStringDTO('Reacao', 'Texto', 'Tags', 'User', 'Post').reacao);
});
