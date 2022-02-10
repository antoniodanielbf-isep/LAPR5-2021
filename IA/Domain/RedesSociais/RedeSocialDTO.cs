/*
using System.Collections.Generic;
using IA.Domain.Utilizadores;

namespace DDDNetCore.Domain.Relacoes.DTO
{
    public class RedeSocialPerspetivaDto
    {
        public readonly int N;
        public readonly List<List<List<string>>> matrixTags;
        public readonly List<List<int>> matrixForcaLigacao;
        public readonly List<string> vertices;


        public RedeSocialPerspetivaDto()
        {
            vertices = new List<string>();
            matrixForcaLigacao = new List<List<int>>();
            matrixTags = new List<List<List<string>>>();
            N = 0;
        }
        
        
        public RedeSocialPerspetivaDto(List<UtilizadorDTO> users, List<RelacaoDTO> rels)
        {
            
            //vertices
            foreach (var user in users)
            {
                vertices.Add(user.Email);

                var temp = new List<List<string>>();

                foreach (var rel in matrixTags) rel.Add(null);

                for (var i = 0; i < vertices.Count; i++)
                {
                    temp.Add(null);
                }

                matrixTags.Add(temp);


                var temp1 = new List<int>();

                foreach (var rel in matrixForcaLigacao) rel.Add(0);

                for (var i = 0; i < vertices.Count; i++)
                {
                    temp1.Add(0);
                }

                matrixForcaLigacao.Add(temp1);
            }

            foreach (var rel in rels)
            {
                int orig = ;
                int dest = ;
            }
        }

        public int getIndexOfUser(string user)
        {
            int i = 0;
            foreach (var email in vertices)
            {
                if (email.Equals(user))
                {
                    return i;
                }

                i++;
            }
            throw new Exception("Relação ")
        }
    }
} */