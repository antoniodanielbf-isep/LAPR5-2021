using System.Collections.Generic;
using DDDNetCore.Domain.RedeSocial;
using DDDNetCore.Domain.Relacoes;
using DDDSample1.Domain.Shared;

public class RedeSocial : IAggregateRoot
{
    public Graph grafo;


    public RedeSocial(List<Relacao> relacoes,
        List<string> utilizadores)
    {
        if (utilizadores.Count == 0)
            throw new BusinessRuleValidationException("Lista de Utilizadores vazia");
        if (relacoes.Count == 0)
            throw new BusinessRuleValidationException("Lista de Relações vazia");

        grafo = new Graph();

        foreach (var user in utilizadores) grafo.Add(user);

        addRelacoes(relacoes);
    }

    public void addRelacoes(List<Relacao> relacoes)
    {
        foreach (var rel in relacoes) grafo.AddConnection(rel);
    }
}