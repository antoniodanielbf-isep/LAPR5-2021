verificaEstados([]):-!.
verificaEstados([H|T]):-
    verificaEstados(T),
    noSentimentos(H,A,B,C,D,E,F,G,I,J,K,L,M),
    B=<0.5,
    D=<0.5,
    F=<0.5,
    K=<0.5.

bestfs1(Orig, Dest, Cam, Custo, N):-
    bestfs12(Dest, [[Orig]], Cam, Custo, N),
    verificaEstados(Cam)
    ;true.

bestfs12(Dest, [[Dest|T]|_], Cam, Custo, _):- 
    reverse([Dest|T], Cam),
    calcula_custo(Cam, Custo).

bestfs12(Dest, [[Dest|_]|LLA2], Cam, Custo, N):- 
    !,
    bestfs12(Dest, LLA2, Cam, Custo, N).

bestfs12(Dest, LLA, Cam, Custo, N):-
    member1(LA, LLA, LLA1),
    LA = [Act|_],
    ((Act == Dest, !, bestfs12(Dest, [LA|LLA1], Cam, Custo, N))
     ;
     (
      length(LA, NLA), NLA =< N,
      findall((CX, [X|LA]), (edge(Act, X, CX),
      \+member(X, LA)), Novos),
      Novos \== [], !,
      sort(0, @>=, Novos, NovosOrd),
      retira_custos(NovosOrd, NovosOrd1),
      append(NovosOrd1, LLA1, LLA2),
      bestfs12(Dest, LLA2, Cam, Custo, N)
     )).

member1(LA, [LA|LAA], LAA).
member1(LA, [_|LAA], LAA1):- member1(LA, LAA, LAA1).

retira_custos([], []).
retira_custos([(_,LA)|L], [LA|L1]):- retira_custos(L, L1).

calcula_custo([Act, X], C):-!, edge(Act, X, C1), edge(X, Act, OC1), C is C1 + OC1.
calcula_custo([Act, X|L], S):- calcula_custo([X|L], S1), 
                                edge(Act, X, C), edge(X, Act, OC), S is S1 + C + OC.