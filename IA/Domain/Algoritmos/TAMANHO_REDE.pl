%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    %%%%%%%%%%%%%%%%%%%%%%%% Unidirecional
    %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

    tamanho(_,0,Le,ListElem):- Le is 1,!.
    tamanho(X,1,Le,ListElem):-
    findall(L,ligacao(X,L,_,_), ListElem),
    length(ListElem,Le).
            tamanho(X,2,Le,ListElem):-
    findall(L,ligacao(X,L,_,_), Set1),
    tamanhoRede(Set1, Set2),
    append_list(Set1, Set2, ListElem),
    length(ListElem,Le).
            tamanho(X,3,Le,ListElem):-
    findall(L,ligacao(X,L,_,_), Set1),
    tamanhoRede(Set1, Set2),
    append_list(Set1, Set2, S12),
    tamanhoRede(Set2, Set3),
    append_list(S12, Set3, ListElem),
    length(ListElem,Le).
            tamanho(X,4,Le,ListElem):-
    findall(L,ligacao(X,L,_,_), Set1),
    tamanhoRede(Set1, Set2),
    append_list(Set1, Set2, S12),
    tamanhoRede(Set2, Set3),
    append_list(S12, Set3, S123),
    tamanhoRede(Set3, Set4),
    append_list(S123, Set4, ListElem),
    length(ListElem,Le).
            tamanho(X,N,Le,ListElem) :-
    findall(L,ligacao(X,L,_,_), Set1),
    tamanhoRede(Set1, Set2),
    append_list(Set1, Set2, S12),
    tamanhoRede(Set2, Set3),
    append_list(S12, Set3, S123),
    tamanhoRede(Set3, Set4),
    append_list(S123, Set4, S1234),
    tamanhoRede(Set4, Set5),
    append_list(S1234, Set5, ListEl),
    sort(ListEl,ListElem),
    length(ListElem,Le).
            %


                tamanhoRede([H|T], Set):-
    findall(L,ligacao(H,L,_,_), Set1),
    calculaTamanhoRede(T, Set1, Set).
        %
            calculaTamanhoRede([],SetAtual,Set) :-sort(SetAtual, Set).
    calculaTamanhoRede([H|T], SetAtual, Set):-
    findall(L,ligacao(H,L,_,_), Set3),
    append_list(Set3, SetAtual, S),
    calculaTamanhoRede(T, S, Set).
        %
            append_list([],L, L).
    append_list([Head|Tail], List2, [Head|List]):-
    append_list(Tail, List2, List).
%
