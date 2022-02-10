node(a,45,95).
node(b,90,95).
node(c,15,85).
node(d,40,80).
node(e,70,80).

noSentimentos(a,0.6,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(b,0.5,0.6,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(c,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(d,0.6,0.3,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(e,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(f,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(g,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).
noSentimentos(h,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).

ligacao(a,e,5).
ligacao(e,a,32).
ligacao(e,b,16).
ligacao(b,e,16).
ligacao(a,b,30).
ligacao(b,a,30).


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
      findall((CX, [X|LA]), (ligacao(Act, X, CX),
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

calcula_custo([Act, X], C):-!, ligacao(Act, X, C1), ligacao(X, Act, OC1), C is C1 + OC1.
calcula_custo([Act, X|L], S):- calcula_custo([X|L], S1), 
                                ligacao(Act, X, C), ligacao(X, Act, OC), S is S1 + C + OC.