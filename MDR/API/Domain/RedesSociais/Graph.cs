using System.Collections.Generic;
using DDDNetCore.Domain.Relacoes;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.RedeSocial

{
    public class Graph
    {
        public readonly List<string> Vertices;
        public List<List<int>> AdjacencyMatrixForcaLigacao;
        public List<List<List<TagRelacao>>> AdjacencyMatrixTags;
        private int N;

        public Graph()
        {
            Vertices = new List<string>();
            AdjacencyMatrixTags = new List<List<List<TagRelacao>>>();
            AdjacencyMatrixForcaLigacao = new List<List<int>>();
            N = 0;
        }

        public int getNumVertex()
        {
            return N;
        }

        public void Add(string vertex)
        {
            // ********* se utilizador for null

            if (vertex == null || vertex == "")
                throw new BusinessRuleValidationException("Utilizador inválido");


            if (!Vertices.Contains(vertex))
            {
                Vertices.Add(vertex);

                // matriz forca ligacao
                var temp = new List<int>();

                foreach (var rel in AdjacencyMatrixForcaLigacao) rel.Add(0);

                foreach (var element in Vertices) temp.Add(0);

                AdjacencyMatrixForcaLigacao.Add(temp);


                // matriz tags
                var temp1 = new List<List<TagRelacao>>();

                foreach (var rel in AdjacencyMatrixTags) rel.Add(null);

                foreach (var element in Vertices) temp1.Add(null);

                AdjacencyMatrixTags.Add(temp1);

                N++;
            }
            else
            {
                throw new BusinessRuleValidationException("Utilizador já existente");
            }
        }

        public void AddConnection(Relacao value)
        {
            var vertex1 = value.UtilizadorOrigem.ToString();
            var vertex2 = value.UtilizadorDestino.ToString();
            if (Vertices.Contains(vertex1) && Vertices.Contains(vertex2))
            {
                var index1 = Vertices.IndexOf(vertex1);
                var index2 = Vertices.IndexOf(vertex2);
                AdjacencyMatrixForcaLigacao[index1][index2] = value.ForcaLigacaoOrigDest.ForcaLigacaoRel;
                AdjacencyMatrixForcaLigacao[index2][index1] = value.ForcaLigacaoDestOrig.ForcaLigacaoRel;

                AdjacencyMatrixTags[index1][index2] = value.TagA;
                AdjacencyMatrixTags[index2][index1] = value.TagB;
            }
            else
            {
                throw new BusinessRuleValidationException("Utilizador(es) inexistente(s)");
            }
        }

        public List<string> getVertices()
        {
            return Vertices;
        }

        public string getUserAtIndex(int n)
        {
            return Vertices[n];
        }


        public int getNumeroRelacoes() //classe utilizada para testes unitários
        {
            var num = 0;
            for (var i = 0; i < N; i++)
            for (var j = i + 1; j < N; j++)
                if (AdjacencyMatrixForcaLigacao[i][j] != 0)
                {
                    num++;
                    num++;
                }

            return num;
        }

        public int getIndexOfUser(string u)
        {
            var ret = -1;
            foreach (var u1 in Vertices)
            {
                ret++;
                if (u.Equals(u1)) return ret;
            }

            return ret;
        }

        public List<List<int>> getMatrixForcaRelacao()
        {
            return AdjacencyMatrixForcaLigacao;
        }

        public override string ToString()
        {
            return "";
        }
    }
}