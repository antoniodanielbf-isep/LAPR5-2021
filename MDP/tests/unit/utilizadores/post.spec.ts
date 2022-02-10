import { Result } from '../../../src/core/logic/Result';
import { Post } from '../../../src/domain/Agregados/Posts/post';
import { PostMap } from '../../../src/mappers/Posts/PostMap';

describe('Domain Posts', () => {
  beforeEach(() => {});
});

test('Não criar post com texto null', () => {
  expect(Post.create(PostMap.toStringDTO('', null, 'Tags', 'User'))).toStrictEqual(
    Result.fail<Post>('Introduza um texto/tags válido!'),
  );
});

test('Não criar post com tags null', () => {
  expect(Post.create(PostMap.toStringDTO('', 'Texto', null, 'User'))).toStrictEqual(
    Result.fail<Post>('Introduza um texto/tags válido!'),
  );
});

test('Não criar post com utilizador null', () => {
  expect(Post.create(PostMap.toStringDTO('', 'Texto', 'Tags', null))).toStrictEqual(
    Result.fail<Post>('Introduza um texto/tags válido!'),
  );
});

test('Não criar post com texto vazio', () => {
  expect(Post.create(PostMap.toStringDTO('', '', 'Tags', 'User'))).toStrictEqual(
    Result.fail<Post>('Introduza um texto/tags válido!'),
  );
});

test('Não criar post com tags vazias', () => {
  expect(Post.create(PostMap.toStringDTO('', 'Texto', '', 'User'))).toStrictEqual(
    Result.fail<Post>('Introduza um texto/tags válido!'),
  );
});

test('Não criar post com utilizador vazio', () => {
  expect(Post.create(PostMap.toStringDTO('', 'Texto', 'Tags', ''))).toStrictEqual(
    Result.fail<Post>('Introduza um texto/tags válido!'),
  );
});

test('Criar post com dados corretos', () => {
  expect(PostMap.toDTO(Post.create(PostMap.toStringDTO('', 'Texto', 'Tags', 'User')).getValue()).texto).toStrictEqual(
    PostMap.toStringDTO('', 'Texto', 'Tags', 'User').texto,
  );
});
