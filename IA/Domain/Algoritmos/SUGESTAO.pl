

ttagsComum(User, N, T, ListaTags,Final):-
    %validacao
    length(ListaTags, Tamanho),
    T>=Tamanho,
    no(User,_,TagsUser),
    subset(ListaTags, TagsUser),

    %users_com_tags_obrigatorias
    verUsersComTagsObrigatorias(User,ListaTags,Users),
    

    %resto_das_tags_que_nao_sao_obrigatorias
     
    remove_list( TagsUser, ListaTags, RestoDasTags),
    
    TamanhoRestoTags is T - Tamanho,
    todas_combinacoes(TamanhoRestoTags, RestoDasTags, Combos),
    

    %vê user que têm o resto das tags
    verUsersComRestoDasTags(Combos, Users,N,T,[], Final).

todas_combinacoes(X,LTags,LcombXTags):-findall(L,combinacao(X,LTags,L),LcombXTags).
combinacao(0,_,[]):-!.
combinacao(X,[Tag|L],[Tag|T]):-X1 is X-1, combinacao(X1,L,T).
combinacao(X,[_|L],T):- combinacao(X,L,T).

  
verUsersComRestoDasTags([], Users, N, T,Aux,Final):-
append(Aux,[],Final).
verUsersComRestoDasTags([H|Tail], Users, N, T,Aux,Final):-
      checkUsersTags(Users, H, [], Final1),
      length(Final1,Tamanho),Tamanho>=N,verificaOMaior(Final1,Aux,Maior),verUsersComRestoDasTags(Tail,User,N,T,Maior,Final);
      verUsersComRestoDasTags(Tail, Users, N, T,Aux,Final).

verificaOMaior(Act, Aux, Maior):-
    length(Act, A1),
    length(Aux, A2),
    A1>A2,append(Act,[],Maior) ;append(Aux,[],Maior).

subset([], _).
subset([X|Tail], Y):-
  memberchk(X, Y),
  subset(Tail, Y).

verUsersComTagsObrigatorias(Us,ListaTags,Users):-
    todosOsUsersExcepto(Us,ListaUsers),
    write(ListaUsers), nl,
    checkUsersTags(ListaUsers,ListaTags,[],Users),
    write(Users), nl.

todosOsUsersExcepto(User, UsersFinal):-
    findall(U, no(U,_,_), Users),
    remover(User,Users,UsersFinal).

%Remove_item_de_uma_lista
remover( _, [], []).
remover( R, [R|T], T2) :- remover( R, T, T2).
remover( R, [H|T], [H|T2]) :- H \= R, remover( R, T, T2).

%Remove_sublista_de_uma_lista
remove_list([], _, []).
remove_list([X|Tail], L2, Result):- member(X, L2), !, remove_list(Tail, L2, Result). 
remove_list([X|Tail], L2, [X|Result]):- remove_list(Tail, L2, Result).


checkUsersTags([],_,Aux,Final):- append(Aux, [], Final).
checkUsersTags([H|T], Tags, Aux, Final):-
    no(H,_,TagsUser),
    subset(Tags,TagsUser),append([H],Aux,Aux1), checkUsersTags(T, Tags, Aux1,Final); checkUsersTags(T, Tags, Aux,Final).