%%% CAMINHO MAIS CURTO
path(X,Y,[X,Y],L):- 
    ligacao(X,Y,_,_),
    L is 1.
path(X,Y,[X|W],L):- 
    ligacao(X,Z,_,_), 
    path(Z,Y,W,L2), 
    L is L2 + 1.
shortestPath(X,X,[X,X],0):- !.
shortestPath(X,Y,MinP,MinD):-
    findall([L,P],path(X,Y,P,L),Set),
    sort(Set,Sorted),
    Sorted = [[MinD,MinP]|_].
%

