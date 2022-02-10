
%noSentimentos(%id,%alegria,%angustia,%esperanca,%medo,%alivio,%dececao,%orgulho,%remorso,%gratidao,%raiva,%like,%dislike)

noSentimentos(0,0.6,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(1,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(2,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(3,0.6,0.3,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(4,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(6,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(7,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
node(1,45,950).
node(2,90,095).
node(3,300,200).
node(4,40,80).
node(5,700,80).
node(6,25,65).
node(7,65,65).
node(0,80,55).
no(0 ,nomeX,[a,b,c,d,e,t,c]).
no(1 ,nomeX,[a,b,c,d,y,u,i,o]).
no(2 ,nomeX,[t,a,g,b,c]).
no(3 ,nomeX,[s,t,u,v,x,z]).
no(4 ,nomeX,[a,b,z]).
no(5 ,nomeX,[a,b,c,d,e]).
no(6 ,nomeX,[a,y,u,i,o]).
no(7 ,nomeX,[y,u,i,o]).


                %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                %alinea 1
                %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
calcularAlegriaAngustia(Id,NovaAlegria,NovaAngustia):-
  node(Id,L,D), L >= D, aumentoAlegria(Id,NovaAlegria), diminuirAngustia(Id,NovaAngustia);
  node(Id,L,D), diminuirAlegria(Id,NovaAlegria), aumentoAngustia(Id,NovaAngustia).



                %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                %alinea 2
                %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
alinea2(User,N,T,Tags,Desejados,Indesejados,NovaEsperanca,NovoMedo,NovoAlivio,NovaDececao):-
    ttagsComum(User,N,T,Tags,Resultado),
    analisarEsperancaMedo(User,Resultado,Desejados,Indesejados,NovaEsperanca,NovoMedo),
    analisar(User,Resultado,Desejados,Indesejados,NovoAlivio,NovaDececao).

analisar(User,Resultado,Desejados,Indesejados,NovoAlivio,NovaDececao):-
    intersection(Desejados,Resultado,ResultadoDesejados),
    intersection(Indesejados,Resultado,ResultadoIndesejados),
    verAlivioEDececao(ID,ResultadoDesejados,Desejados,ResultadoIndesejados,Indesejados,Resultado,NovoAlivio,NovaDececao).


analisarEsperancaMedo(ID,Resultado,Desejados,Indesejados,NovaEsperanca,NovoMedo):-
    intersection(Desejados,Resultado,ResultadoDesejados),
    verEsperanca(ID,ResultadoDesejados,Desejados,Resultado,NovaEsperanca),
    intersection(Indesejados,Resultado,ResultadoIndesejados),
    verMedo(ID,ResultadoIndesejados,Indesejados,Resultado,NovoMedo).
    

%
verAlivioEDececao(ID,ResultadoDesejado,Desejados,ResultadoIndesejados,Indesejados,Resultado,NovoAlivio,NovaDececao):-
    length(ResultadoIndesejados, TamResulIndesejados),
    length(Indesejados,TamIndesejados),
    length(ResultadoDesejado, TamResulDesejados),
    length(Desejados,TamDesejados),
    Tam1 is TamIndesejados - TamResulIndesejados,
    TamResultTotal is Tam1 + TamResulDesejados,
    TamTotal is (TamDesejados + TamIndesejados) / 2, 
    nl,nl,write(TamResultTotal),nl, write(TamTotal),nl,
    TamResultTotal > TamTotal,
    aumentoAlivio(ID,TamResultTotal,TamTotal,NovoAlivio),
    diminuirDececao(ID,TamResultTotal,TamTotal,NovaDececao);
    length(ResultadoIndesejados, TamResulIndesejados),
    length(Indesejados,TamIndesejados),
    length(ResultadoDesejado, TamResulDesejados),
    length(Desejados,TamDesejados),
    Tam1 is TamIndesejados - TamResulIndesejados,
    TamResultTotal is Tam1 + TamResulDesejados,
    TamTotal is (TamDesejados + TamIndesejados) / 2,
    nl,nl,write(TamResultTotal),nl, write(TamTotal),nl,
    aumentoDececao(ID,TamResultTotal,TamTotal,NovaDececao),
    diminuirAlivio(ID,TamResultTotal,TamTotal,NovoAlivio).


verMedo(ID,ResultadoIndesejados,Indesejados,Resultado,NovoMedo):-
    length(ResultadoIndesejados, TamResulIndesejados),
    length(Indesejados,TamIndesejados), 
    MetadeIndesejados is TamIndesejados / 2,
    TamResulIndesejados >= MetadeIndesejados,
    aumentoMedo(ID,TamResulIndesejados,TamIndesejados,NovoMedo);
    length(ResultadoIndesejados, TamResulIndesejados),
    length(Indesejados,TamIndesejados),
    diminuirMedo(ID,TamResulIndesejados,TamIndesejados,NovoMedo).

verEsperanca(ID, ResultadoDesejado, Desejados, Resultado,NovaEsperanca):-
    length(ResultadoDesejado, TamResulDesejados),
    length(Desejados,TamDesejados),
    MetadeDesejados is TamDesejados / 2,
    TamResulDesejados >= MetadeDesejados,
    aumentoEsperanca(ID,TamResulDesejados,TamDesejados,NovaEsperanca);
    length(ResultadoDesejado, TamResulDesejados),
    length(Desejados,TamDesejados),
    diminuirEsperanca(ID,TamResulDesejados,TamDesejados,NovaEsperanca).


alinea3(User,N,T,Tags,Desejados,Indesejados,NovaOrgulho,NovoRemorso,NovoGratidao,NovaRaiva):-
    ttagsComum(User,N,T,Tags,Resultado),
    analisar3(User,Resultado,Desejados,Indesejados,NovaOrgulho,NovoRemorso,NovoGratidao,NovaRaiva).
    

analisar3(ID,Resultado,Desejados,Indesejados,NovaOrgulho,NovoRemorso,NovoGratidao,NovaRaiva):-
    intersection(Desejados,Resultado,ResultadoDesejados),
    verA3(ID,ResultadoDesejados,Desejados,Resultado,NovaOrgulho,NovoRemorso),
    intersection(Indesejados,Resultado,ResultadoIndesejados),
    verB3(ID,ResultadoIndesejados,Indesejados,Resultado,NovoGratidao,NovaRaiva).

verA3(ID, ResultadoDesejado, Desejados, Resultado,NovoOrgulho,NovoRemorso):-
    length(ResultadoDesejado, TamResulDesejados),
    length(Desejados,TamDesejados),
    MetadeDesejados is TamDesejados / 2,
    TamResulDesejados >= MetadeDesejados,
    aumentoOrgulho(ID,TamResulDesejados,TamDesejados,NovoOrgulho),
    diminuirOrgulho(ID,TamResulDesejados,TamDesejados,NovoRemorso);
    length(ResultadoDesejado, TamResulDesejados),
    length(Desejados,TamDesejados),
    aumentoRemorso(ID,TamResulDesejados,TamDesejados,NovoRemorso),
    diminuirOrgulho(ID,TamResulDesejados,TamDesejados,NovoOrgulho).

verB3(ID,ResultadoIndesejados,Indesejados,Resultado,NovoGratidao,NovaRaiva):-
    length(ResultadoIndesejados, TamResulIndesejados),
    length(Indesejados,TamIndesejados), 
    MetadeIndesejados is TamIndesejados / 2,
    TamResulIndesejados >= MetadeIndesejados,
    aumentoGratidao(ID,TamResulIndesejados,TamIndesejados,NovoGratidao),
    diminuirRaiva(ID,TamResulIndesejados,TamIndesejados,NovaRaiva);
    length(ResultadoIndesejados, TamResulIndesejados),
    length(Indesejados,TamIndesejados),
    diminuirGratidao(ID,TamResulIndesejados,TamIndesejados,NovoGratidao),
    aumentoRaiva(ID,TamResulIndesejados,TamIndesejados,NovaRaiva).

diminuirRaiva(Id,Q1,Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,_,_,_,_,_,_,Emocao,_,_),
  Q1>0,
  Aux1 is Q1 / Q2,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2;
  noSentimentos(Id,_,_,_,_,_,_,_,_,_,Emocao,_,_),
  Aux1 is 0.99,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2.

diminuirGratidao(Id,Q1,Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,_,_,_,_,_,Emocao,_,_,_),
  Q1>0,
  Aux1 is Q1 / Q2,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2;
  noSentimentos(Id,_,_,_,_,_,_,_,_,Emocao,_,_,_),
  Aux1 is 0.99,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2.

diminuirRemorso(Id,Q1,Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,_,_,_,_,Emocao,_,_,_,_),
  Q1>0,
  Aux1 is Q1 / Q2,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2;
  noSentimentos(Id,_,_,_,_,_,_,_,Emocao,_,_,_,_),
  Aux1 is 0.99,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2.

diminuirOrgulho(Id,Q1,Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,_,_,_,Emocao,_,_,_,_,_),
  Q1>0,
  Aux1 is Q1 / Q2,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2;
  noSentimentos(Id,_,_,_,_,_,_,Emocao,_,_,_,_,_),
  Aux1 is 0.99,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2.

diminuirDececao(Id,Q1,Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,_,_,Emocao,_,_,_,_,_,_),
  Q1>0,
  Aux1 is Q1 / Q2,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2;
  noSentimentos(Id,_,_,_,_,_,Emocao,_,_,_,_,_,_),
  Aux1 is 0.99,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2.

diminuirAlivio(Id,Q1,Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,_,Emocao,_,_,_,_,_,_,_),
  Q1>0,
  Aux1 is Q1 / Q2,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2;
  noSentimentos(Id,_,_,_,_,Emocao,_,_,_,_,_,_,_),
  Aux1 is 0.99,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2.


diminuirMedo(Id,Q1,Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,Emocao,_,_,_,_,_,_,_,_),
  Q1>0,
  Aux1 is Q1 / Q2,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2;
  noSentimentos(Id,_,_,_,Emocao,_,_,_,_,_,_,_,_),
  Aux1 is 0.99,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2.

diminuirEsperanca(Id,Q1,Q2,NovaEmocao):-
  noSentimentos(Id,_,_,Emocao,_,_,_,_,_,_,_,_,_),
  Q1>0,
  Aux1 is Q1 / Q2,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2;
  noSentimentos(Id,_,_,Emocao,_,_,_,_,_,_,_,_,_),
  Aux1 is 0.99,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2.

diminuirAngustia(Id,NovaEmocao):-
  node(Id,L,D),
  noSentimentos(Id,_,Emocao,_,_,_,_,_,_,_,_,_,_),
  diferencaLikes(L,D,Dif),
  Aux1 is Dif / 200,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2.

diminuirAlegria(Id,NovaEmocao):-
  node(Id,L,D),
  noSentimentos(Id,Emocao,_,_,_,_,_,_,_,_,_,_,_),
  diferencaLikes(L,D,Dif),
  Aux1 is Dif / 200,
  Aux2 is 1 - Aux1,
  NovaEmocao is Emocao * Aux2.

aumentoDisike(Id,NovaEmocao):-
  node(Id,L,D),
  noSentimentos(Id,_,_,_,_,_,_,_,_,_,_,_,Emocao),
  diferencaLikes(L,D,Dif),
  Aux1 is Dif / 200,
  Aux2 is 1- Emocao,
  Aux3 is Aux1 * Aux2,
  NovaEmocao is Emocao +Aux3.

aumentoLike(Id,NovaEmocao):-
  node(Id,L,D),
  noSentimentos(Id,_,_,_,_,_,_,_,_,_,_,Emocao,_),
  diferencaLikes(L,D,Dif),
  Aux1 is Dif / 200,
  Aux2 is 1- Emocao,
  Aux3 is Aux1 * Aux2,
  NovaEmocao is Emocao +Aux3.

aumentoRaiva(Id,Q1, Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,_,_,_,_,_,Emocao,_,_),
  Aux1 is Q1 / Q2,
  Aux2 is 1- Emocao,
  Aux3 is Aux1 * Aux2,
  NovaEmocao is Emocao + Aux3.

aumentoGratidao(Id,Q1, Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,_,_,_,_,_,Emocao,_,_,_),
  Aux1 is Q1 / Q2,
  Aux2 is 1- Emocao,
  Aux3 is Aux1 * Aux2,
  NovaEmocao is Emocao + Aux3.

aumentoRemorso(Id,Q1, Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,_,_,_,_,Emocao,_,_,_,_),
  Aux1 is Q1 / Q2,
  Aux2 is 1- Emocao,
  Aux3 is Aux1 * Aux2,
  NovaEmocao is Emocao + Aux3.

aumentoOrgulho(Id,Q1, Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,_,_,_,Emocao,_,_,_,_,_),
  Aux1 is Q1 / Q2,
  Aux2 is 1- Emocao,
  Aux3 is Aux1 * Aux2,
  NovaEmocao is Emocao + Aux3.

aumentoDececao(Id,Q1, Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,_,_,Emocao,_,_,_,_,_,_),
  Aux1 is Q1 / Q2,
  Aux2 is 1- Emocao,
  Aux3 is Aux1 * Aux2,
  NovaEmocao is Emocao + Aux3.

aumentoAlivio(Id,Q1, Q2,NovaEmocao):-
  noSentimentos(Id,_,_,_,_,Emocao,_,_,_,_,_,_,_),
  Aux1 is Q1 / Q2,
  Aux2 is 1- Emocao,
  Aux3 is Aux1 * Aux2,
  NovaEmocao is Emocao + Aux3.

aumentoMedo(Id,Q1, Q2, NovaEmocao):-
  noSentimentos(Id,_,_,_,Emocao,_,_,_,_,_,_,_,_),
  Aux1 is Q1 / Q2,
  Aux2 is 1- Emocao,
  Aux3 is Aux1 * Aux2,
  NovaEmocao is Emocao + Aux3.

aumentoEsperanca(Id,Q1,Q2,NovaEmocao):-
  noSentimentos(Id,_,_,Emocao,_,_,_,_,_,_,_,_,_),
  Aux1 is Q1 / Q2,
  Aux2 is 1- Emocao,
  Aux3 is Aux1 * Aux2,
  NovaEmocao is Emocao +Aux3.

aumentoAngustia(Id,NovaEmocao):-
  node(Id,L,D),
  noSentimentos(Id,_,Emocao,_,_,_,_,_,_,_,_,_,_),
  diferencaLikes(L,D,Dif),
  Aux1 is Dif / 200,
  Aux2 is 1- Emocao,
  Aux3 is Aux1 * Aux2,
  NovaEmocao is Emocao +Aux3.

aumentoAlegria(Id,NovaEmocao):-
  node(Id,L,D),
  noSentimentos(Id,Emocao,_,_,_,_,_,_,_,_,_,_,_),
  diferencaLikes(L,D,Dif),
  Aux1 is Dif / 200,
  Aux2 is 1- Emocao,
  Aux3 is Aux1 * Aux2,
  NovaEmocao is Emocao +Aux3.


diferencaLikes(L,D,Valor):-
    Aux is L- D, Aux >=200, Valor is 200;
    Aux is L- D,Aux =< -200, Valor is 200;
    Aux is L- D,Aux >=0, Valor is Aux;
    Aux is L- D,Valor is 0 - Aux.


todas_combinacoes(X,LTags,LcombXTags):-findall(L,combinacao(X,LTags,L),LcombXTags).
combinacao(0,_,[]):-!.
combinacao(X,[Tag|L],[Tag|T]):-X1 is X-1, combinacao(X1,L,T).
combinacao(X,[_|L],T):- combinacao(X,L,T).


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