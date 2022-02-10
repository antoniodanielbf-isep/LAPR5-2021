using System.Collections.Generic;

namespace DDDNetCore.Domain.RedeSocial
{
    public class GraphDto
    {
        public readonly List<List<int>> AdjacencyMatrixForcaLigacao;
        public readonly List<List<List<string>>> AdjacencyMatrixTags;
        public readonly int N;

        public readonly List<string> Vertices;

        public GraphDto(Graph grafo)
        {
            Vertices = new List<string>();
            AdjacencyMatrixForcaLigacao = new List<List<int>>();
            AdjacencyMatrixTags = new List<List<List<string>>>();
            N = grafo.getNumVertex();

            //vertices
            foreach (var user in grafo.getVertices())
            {
                Vertices.Add(user);

                var temp = new List<List<string>>();

                foreach (var rel in AdjacencyMatrixTags) rel.Add(null);

                for (var i = 0; i < Vertices.Count; i++) temp.Add(null);

                AdjacencyMatrixTags.Add(temp);


                var temp1 = new List<int>();

                foreach (var rel in AdjacencyMatrixForcaLigacao) rel.Add(0);

                for (var i = 0; i < Vertices.Count; i++) temp1.Add(0);

                AdjacencyMatrixForcaLigacao.Add(temp1);
            }

            for (var i = 0; i < N; i++)
            for (var j = 0; j < N; j++)
            {
                AdjacencyMatrixForcaLigacao[i][j] = grafo.AdjacencyMatrixForcaLigacao[i][j];
                AdjacencyMatrixForcaLigacao[j][i] = grafo.AdjacencyMatrixForcaLigacao[j][i];

                var list = new List<string>();
                if (grafo.AdjacencyMatrixTags[i][j] != null)
                {
                    foreach (var tag in grafo.AdjacencyMatrixTags[i][j]) list.Add(tag.ToString());

                    AdjacencyMatrixTags[i][j] = list;
                }
                else
                {
                    AdjacencyMatrixTags[i][j] = null;
                }

                if (grafo.AdjacencyMatrixTags[j][i] != null)
                {
                    list = new List<string>();
                    foreach (var tag in grafo.AdjacencyMatrixTags[j][i]) list.Add(tag.ToString());

                    AdjacencyMatrixTags[j][i] = list;
                }
                else
                {
                    AdjacencyMatrixTags[j][i] = null;
                }
            }
        }
    }
}