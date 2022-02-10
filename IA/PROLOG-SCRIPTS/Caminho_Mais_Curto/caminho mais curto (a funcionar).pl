ligacao(0,3,10,3).
ligacao(0,4,5,3).
ligacao(0,5,1,3).
ligacao(3,5,3,6).



%%% CAMINHO MAIS CURTO
path(X,Y,[X,Y],L):- 
    ligacao(X,Y,_,_),
    L is 1.
path(X,Y,[X|W],L):- 
    ligacao(X,Z,_,_), 
    path(Z,Y,W,L2), 
    L is L2 + 1.
shortestPath(X,X,[X,X],0):- !.
shortestPath(X,Y,MinP,MinD):-get_time(T1),
    findall([L,P],path(X,Y,P,L),Set),
    sort(Set,Sorted),
    Sorted = [[MinD,MinP]|_],
    get_time(T2),T is T2-T1,write(T),write(' segundos').
%

