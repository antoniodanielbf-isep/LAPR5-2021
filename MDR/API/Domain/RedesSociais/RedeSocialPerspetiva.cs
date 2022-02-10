using System.Collections.Generic;
using DDDNetCore.Domain.Relacoes;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.RedeSocial
{
    public class RedeSocialPerspetiva
    {
        private readonly int nivel;
        private readonly string utilizador;
        public Graph grafo;

        public RedeSocialPerspetiva(string utilizador, List<Relacao> relacoes,
            List<string> utilizadores, int nivel)
        {
            if (utilizadores.Count == 0)
                throw new BusinessRuleValidationException("Lista de Utilizadores vazia");
            if (relacoes.Count == 0)
                throw new BusinessRuleValidationException("Lista de Relações vazia");
            if (utilizador == null || !utilizadores.Contains(utilizador))
                throw new BusinessRuleValidationException("Utilizador Inválido");
            if (nivel < 0) throw new BusinessRuleValidationException("Nível Inválido");

            this.utilizador = utilizador;
            this.nivel = nivel;
            grafo = new Graph();


            criaListaUtilizadoresAteNivel(utilizadores, relacoes);
            addRelacoes(relacoes);
        }

        public void criaListaUtilizadoresAteNivel(List<string> utilizadores, List<Relacao> relacoes)
        {
            /* recebe lista total de utilizadores da rede e determina aqueles que têm ligação até ao Utilizador
             até ao nível escolhido*/

            // cria lista temporária 
            var listaUtilizadoresDoNivelAnterior = new List<string>();
            listaUtilizadoresDoNivelAnterior.Add(utilizador);

            var listaUtilizadoresNivelSeguinte = new List<string>();
            for (var nivelTemp = 0; nivelTemp < nivel; nivelTemp++)
            {
                foreach (var user in listaUtilizadoresDoNivelAnterior)
                {
                    var utilizadoresDoNivelSeguinteTemp
                        = checkUtilizadoresComQuemTemRelacaoAcima(relacoes, user);

                    foreach (var u1 in utilizadoresDoNivelSeguinteTemp) listaUtilizadoresNivelSeguinte.Add(u1);
                }

                foreach (var u in listaUtilizadoresDoNivelAnterior) grafo.Add(u);

                listaUtilizadoresDoNivelAnterior = new List<string>();
                foreach (var u in listaUtilizadoresNivelSeguinte) listaUtilizadoresDoNivelAnterior.Add(u);

                listaUtilizadoresNivelSeguinte = new List<string>();
            }

            foreach (var user in listaUtilizadoresDoNivelAnterior) grafo.Add(user);
        }

        public List<string> checkUtilizadoresComQuemTemRelacaoAcima(
            List<Relacao> relacoes, string user)
        {
            var listaUtilizadoresNivelSeguinte = new List<string>();
            //verifica relacoes de nivel superior de um determinado user
            foreach (var r in relacoes)

                if (r.UtilizadorOrigem.ToString().Equals(user))
                {
                    // relacao de A->B implica relacao de B->A
                    if (!grafo.getVertices().Contains(r.UtilizadorDestino.ToString()))
                        listaUtilizadoresNivelSeguinte.Add(r.UtilizadorDestino.ToString());
                }
                else if (r.UtilizadorDestino.ToString().Equals(user))
                {
                    // relacao de A->B implica relacao de B->A
                    if (!grafo.getVertices().Contains(r.UtilizadorOrigem.ToString()))
                        listaUtilizadoresNivelSeguinte.Add(r.UtilizadorOrigem.ToString());
                }

            return listaUtilizadoresNivelSeguinte;
        }

        public int getNivel()
        {
            return nivel;
        }

        public string getNomeUtilizador()
        {
            return utilizador;
        }

        public void addRelacoes(List<Relacao> listaRelacoes)
        {
            foreach (var r in listaRelacoes)
                if (grafo.getVertices().Contains(r.UtilizadorOrigem.ToString()) &&
                    grafo.getVertices().Contains(r.UtilizadorDestino.ToString()))
                    grafo.AddConnection(r);
        }

        public override string ToString()
        {
            var ret = "Visão do Uilizador: " + utilizador + " até ao nível " + nivel + "\n";
            ret += "Ligações de nível 1:\n";
            var listaSeguintes = new List<string>();
            var listaAnteriores = new List<string>();
            var utilizadoresCheck = new List<string>();
            for (var i = 1; i < grafo.getNumVertex(); i++)
                if (grafo.getMatrixForcaRelacao()[0][i] != 0)
                {
                    ret += grafo.getUserAtIndex(0) + " -> " + grafo.getUserAtIndex(i) +
                           " : " +
                           grafo.getMatrixForcaRelacao()[0][i] + "\n";
                    ret += grafo.getUserAtIndex(i) + " -> " + grafo.getUserAtIndex(0) +
                           " : " +
                           grafo.getMatrixForcaRelacao()[i][0] + "\n";

                    listaSeguintes.Add(grafo.getUserAtIndex(i));
                }

            utilizadoresCheck.Add(grafo.getUserAtIndex(0));

            var index = 2;
            ret += "\n";
            while (index <= nivel)
            {
                ret += "Nível " + index + "\n";
                foreach (var u1 in listaSeguintes) listaAnteriores.Add(u1);

                listaSeguintes = new List<string>();

                foreach (var u1 in listaAnteriores)
                {
                    var indice = grafo.getIndexOfUser(u1);
                    for (var i = 1; i < grafo.getNumVertex(); i++)
                        if (!utilizadoresCheck.Contains(grafo.getUserAtIndex(i)) &&
                            !utilizadoresCheck.Contains(grafo.getUserAtIndex(indice)) &&
                            grafo.getMatrixForcaRelacao()[indice][i] != 0)
                        {
                            ret += grafo.getUserAtIndex(indice) + " -> " +
                                   grafo.getUserAtIndex(i) + " : " +
                                   grafo.getMatrixForcaRelacao()[indice][i] + "\n";
                            ret += grafo.getUserAtIndex(i) + " -> " +
                                   grafo.getUserAtIndex(indice) + " : " +
                                   grafo.getMatrixForcaRelacao()[i][indice] + "\n";
                            listaSeguintes.Add(grafo.getUserAtIndex(i));
                        }

                    utilizadoresCheck.Add(grafo.getUserAtIndex(indice));
                }

                index++;
            }

            return ret;
        }
    }
}