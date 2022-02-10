%% Caminho mais seguro Unidirecional
    path(X,Y,[X,Y],L,MINIMO):-
    ligacao(X,Y,L,_),
    L > MINIMO.
        path(X,Y,[X|W],L,MINIMO):-
    ligacao(X,Z,L1,_),
    L1>MINIMO,
    path(Z,Y,W,L2,MINIMO),
    L is L1 + L2.
        shortestPath(X,X,[X,X],0,MINIMO):- !.
    shortestPath(X,Y,MinP,MinD,MINIMO):-
    findall([L,P],path(X,Y,P,L,MINIMO),Set),
    sort(Set,Sorted1),
    reverse(Sorted1, Sorted),
    Sorted = [[MinD,MinP]|_].
    %

        %% Caminho mais seguro Bidirecional

        pathBI(X,Y,[X,Y],L,MINIMO):-
    ligacao(X,Y,L,L0),
    L + L0> MINIMO.
        pathBI(X,Y,[X|W],L,MINIMO):-
    ligacao(X,Z,L1,_),
    L1>MINIMO,
    pathBI(Z,Y,W,L2,MINIMO),
    L is L1 + L2.
        shortestPathBI(X,X,[X,X],0,MINIMO):- !.
    shortestPathBI(X,Y,MinP,MinD,MINIMO):- 
    findall([L,P],pathBI(X,Y,P,L,MINIMO),Set),
    sort(Set,Sorted1),
    reverse(Sorted1, Sorted),
    Sorted = [[MinD,MinP]|_].
%