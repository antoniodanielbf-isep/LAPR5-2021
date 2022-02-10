
xTagsComum(X) :-
  getListaUsers(ListaUsers),
  criaListaCombinacoesDeXTgas(X,ListaTagsCombo),
  display_matrix(ListaTagsCombo, ListaUsers,X).
%

matrix(Matrix ,Users,X) :-
    nth0(_, Matrix, Row),
    write('Tags: '), tab(1),write(Row),tab(1),write('Users:  '),
    checkUsers(Row, Users, X),
    write('\n'),
    nth0(_, Row, _).
%

display_matrix(Matrix,  Users, X) :-
    matrix(Matrix,  Users, X),
    fail.
display_matrix(_).
%

checkUsers(_,[],_) :-!.
checkUsers(Tags, [H|Tale],X):-
  checkUsers(Tags, Tale,X),
  intersetar(Tags,H,X).
  
%

intersetar(Tags, User, X):-
  no(User, _, TagsUser),
  intersection(Tags, TagsUser, CommonTags),
  length(CommonTags, N),
  imprimir(Tags, User, X, N, TagsUser, CommonTags).

%

imprimir(Tags, User, X, X, TagUser, CommonTags):-
  write(User), tab(1), !.
imprimir(Tags, User, X, N, TagUser, CommonTags):-
  write('').

%

verificar(X,N):-
  X =:= N.
%

getListaUsers(List):-
  findall(L, no(L,_,_), List).
%

criaListaCombinacoesDeXTgas(X,L) :-
  listaTags(Tags),
  todas_combinacoes(X, Tags, L).
%

todas_combinacoes(X,LTags,LcombXTags):-findall(L,combinacao(X,LTags,L),LcombXTags).
combinacao(0,_,[]):-!.
combinacao(X,[Tag|L],[Tag|T]):-X1 is X-1, combinacao(X1,L,T).
combinacao(X,[_|L],T):- combinacao(X,L,T).
%

listaTags(Result1):-
  findall(L, no(_,_,L), Tags),
  getAllElements(Tags, Result),
  sort(Result, Result1).
%

getAllElements([],[]).
getAllElements([H|T], ElementsList) :- 
    getAllElements(T, NewElementsList),
    append(NewElementsList, H, ElementsList).
%
