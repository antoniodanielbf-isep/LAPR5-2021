%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    %%%%%%%%%%%%%%%%%%%%%%%% Unidirecional
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


    path(X,Y,[X,Y],L):-
    ligacao(X,Y,L,_).

    path(X,Y,[X|W],L):-
    ligacao(X,Z,L1,_),
    path(Z,Y,W,L2),
    L is L1 + L2.
        strongestPathUni(X,X,[X,X],0):- !.
    strongestPathUni(X,Y,MinP,MinD):- 
    findall([L,P],path(X,Y,P,L),Set),
    sort(Set,Sorted1),
    reverse(Sorted1, Sorted),
    Sorted = [[MinD,MinP]|_].
    %


   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
   %%%%%%%%%%%%%%%%%%%%%%%% Bidireciona
   %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


    pathBi(X,Y,[X,Y],L):-
    ligacao(X,Y,L,_).

    pathBi(X,Y,[X|W],L):-
    ligacao(X,Z,L1,L2),
    pathBi(Z,Y,W,L3),
    L is L1 + L2 + L3.
        strongestPathBI(X,X,[X,X],0):- !.
    strongestPathBI(X,Y,MinP,MinD):- get_time(T1),
    findall([L,P],pathBi(X,Y,P,L),Set),
    sort(Set,Sorted1),
    reverse(Sorted1, Sorted),
    Sorted = [[MinD,MinP]|_],
    get_time(T2),T is T2-T1,write(T),write(' segundos').
%
