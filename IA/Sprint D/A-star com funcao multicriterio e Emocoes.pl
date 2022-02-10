node(a,45,950).
node(b,90,095).
node(c,1500,850).
node(d,40,80).
node(e,70,80).
node(f,25,65).
node(g,65,65).
node(h,45,55).
node(i,5,50).
node(j,80,50).
node(l,65,45).
node(m,5,40).
node(n,55,30).
node(o,80,30).
node(p,25,15).
node(q,80,15).
node(r,55,10).

noSentimentos(a,0.6,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(b,0.5,0.6,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(c,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(d,0.6,0.3,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(e,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(f,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(g,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(h,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).

edge(a,b,50).
edge(b,a,50).
edge(a,c,32).
edge(c,a,32).
edge(b,e,16).
edge(e,b,16).
edge(c,e,3).
edge(e,c,30).

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

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