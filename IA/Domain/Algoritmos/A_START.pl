aStar(Orig,Dest,Cam,Custo,N):-
    %maxPath(a,e,C,N),
    aStar2(Dest,[(_,0,[Orig])],Cam,Custo,N,0).
    
aStar2(Dest,[(_,Custo,[Dest|T])|_],Cam,Custo,_,_):-
    reverse([Dest|T],Cam).
    
aStar2(Dest,[(_,Ca,LA)|Outros],Cam,Custo,N,M):-
    LA=[Act|_],
    M1 is M + 1,
    M1=<N, %impede solucao com mais niveis
    findall((CEX,CaX,[X|LA]),
    (Dest\==Act,edge(Act,X,CustoX),\+ member(X,LA),
    CaX is CustoX + Ca, estimativa(N,M1,EstX),
    CEX is CaX +EstX),Novos),
    append(Outros,Novos,Todos),
    write('Novos='),write(Novos),nl,
    sort(Todos,TodosOrd1),
    reverse(TodosOrd1,TodosOrd),
    write('TodosOrd='),write(TodosOrd),nl,
    aStar2(Dest,TodosOrd,Cam,Custo,N, M1).
%

estimativa(N,M,Estimativa):-
    Estimativa is (N-M)*100.
%

%%% Calcula N
path(X,Y,[X,Y],L):- 
    edge(X,Y,_),
    L is 1.
path(X,Y,[X|W],L):-
    edge(X,Z,_), 
    path(Z,Y,W,L2), 
    L is L2 + 1.
maxPath(X,X,[X,X],0):- !.
maxPath(X,Y,MinP,MinD):-
    findall([L,P],path(X,Y,P,L),Set),
    sort(Set,Sorted),
    reverse(Sorted1, Sorted),
    Sorted = [[MinD,MinP]|_].
%