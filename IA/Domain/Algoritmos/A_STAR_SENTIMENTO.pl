verificaEstados([]):-!.
verificaEstados([H|T]):-
    verificaEstados(T),
    noSentimentos(H,A,B,C,D,E,F,G,I,J,K,L,M),
    B=<0.5,
    D=<0.5,
    F=<0.5,
    K=<0.5.

aStar(Orig,Dest,Cam,Custo,N):-
    %maxPath(a,e,C,N),
    aStar2(Dest,[(_,0,[Orig])],Cam,Custo,N,0),
    length(Cam, Tam),
    Tam < N,
    verificaEstados(Cam)
    ;true.
    
aStar2(Dest,[(_,Custo,[Dest|T])|_],Cam,Custo,_,_):-
    reverse([Dest|T],Cam).
    
aStar2(Dest,[(_,Ca,LA)|Outros],Cam,Custo,N,M):-
    LA=[Act|_],
    M1 is M + 1,
    findall((CEX,CaX,[X|LA]),
    (Dest\==Act,edge(Act,X,CustoX),\+ member(X,LA),
     estimativa(Act,X,EstX),CaX is EstX + Ca,
    CEX is CaX +EstX),Novos),
    append(Outros,Novos,Todos),
    sort(Todos,TodosOrd1),
    reverse(TodosOrd1,TodosOrd),
    aStar2(Dest,TodosOrd,Cam,Custo,N, M1).
%

estimativa(Act,X,Estimativa):-
    edge(Act,X,V),
    edge(X,Act,V1),
    funcaoMulticriterio( V ,X,Estimativa1), 
    funcaoMulticriterio( V1 ,Act,Estimativa2),
    Estimativa is (Estimativa1 + Estimativa2) / 2.
%

funcaoMulticriterio(Value, Nodo, Percentagem) :-
        percentagemForcaDeLigacao(Value, P1),
        likesEDislikes(Nodo, P2),
        Percentagem is P1 + P2.

percentagemForcaDeLigacao(Value, Percentagem):-
        Percentagem is Value * 50 / 100.

percentagemDiferencaLikeseDislikes(Value,Percentagem):- 
        (Value >= 200, Percentagem is 50,!); 
        (Value =< -200, Percentagem is 0,!);
        Percentagem is ((Value + 200) * 50) / 400.

diferencaLikeseDiskikes(Nodo1, Dif):-
    node(Nodo1, X,Y),
    Dif is X - Y.
%

likesEDislikes(Nodo, Percentagem) :-
    diferencaLikeseDiskikes(Nodo, P),
    percentagemDiferencaLikeseDislikes(P, Percentagem).