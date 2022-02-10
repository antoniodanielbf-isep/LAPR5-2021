using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.RedesSociais.DTO;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Relacoes.DTO;
using DDDNetCore.Domain.Utilizadores;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Domain.RedeSocial
{
    public class RedeSocialService
    {
        private readonly RelacaoService _serviceRelacao;
        private readonly UtilizadorService _serviceUtilizador;

        public RedeSocialService(RelacaoService rService, UtilizadorService uService)
        {
            _serviceRelacao = rService;
            _serviceUtilizador = uService;
        }


        public async Task<List<List<RelacaoDTO>>> getRedePerspetivaUser(string user, int level)
        {
            var resultadoFinal = new List<List<RelacaoDTO>>();

            var visitedNode = new List<string>();


            //NÍVEL 1 -> OBTER RELAÇÕES DO USER TARGET
            var relacaoNivel1 = await _serviceRelacao.GetAllFromUserAsync(user);

            var relacoesOrdenadas = ordenarRelacoes(relacaoNivel1, user);

            relacoesOrdenadas = removerRepeticoes(relacoesOrdenadas);


            resultadoFinal.Add(relacoesOrdenadas);


            //USER TARGET já iterado:
            visitedNode.Add(user);

            var level2Targets = getNextTargets(relacoesOrdenadas, user);

            if (level==3)
            {
                //NÍVEL 3

                var preAddRelacoes = new List<RelacaoDTO>();
                foreach (var tar in level2Targets)
                {
                    var relacoes = await _serviceRelacao.GetAllFromUserAsync(tar);
                    var relacoesOrdenadasAux = ordenarRelacoes(relacoes, tar);

                    foreach (var pos in relacoesOrdenadasAux) preAddRelacoes.Add(pos);
                }

                // guardar nivel 3


                foreach (var pos in relacoesOrdenadas)
                    if (preAddRelacoes.Contains(pos))
                        preAddRelacoes.Remove(pos);

                preAddRelacoes = removerRepeticoes(preAddRelacoes);

                resultadoFinal.Add(preAddRelacoes);  
            }
            
            return resultadoFinal;
        }

        private List<RelacaoDTO> removerRepeticoes(List<RelacaoDTO> relacoesOrdenadas)
        {
            var l1 = new List<RelacaoDTO>();

            foreach (var pos in relacoesOrdenadas)
            {
                var exists = verificarAdicaoRelacao(l1, pos);

                if (!exists) l1.Add(pos);
            }

            return l1;
        }

        private bool verificarAdicaoRelacao(List<RelacaoDTO> l1, RelacaoDTO pos)
        {
            foreach (var i in l1)
                if (string.Compare(i.RelacaoId, pos.RelacaoId, StringComparison.OrdinalIgnoreCase) == 0)
                    return true;

            return false;
        }


        private bool verificarExistenciaUser(string posUtilizadorDestino, List<string> visitedNode)
        {
            foreach (var pos in visitedNode)
                if (string.Compare(pos, posUtilizadorDestino, StringComparison.OrdinalIgnoreCase) == 0)
                    return true;

            return false;
        }

        private List<string> getNextTargets(List<RelacaoDTO> relacoes, string user)
        {
            var response = new List<string>();

            foreach (var pos in relacoes)
                if (string.Compare(pos.UtilizadorOrigem, user, StringComparison.OrdinalIgnoreCase) == 0)
                    response.Add(pos.UtilizadorDestino);
                else
                    response.Add(pos.UtilizadorOrigem);

            return response;
        }

        private List<RelacaoDTO> ordenarRelacoes(List<RelacaoDTO> relacoesFinais, string user)
        {
            var aux = new List<RelacaoDTO>();
            foreach (var pos in relacoesFinais)
                if (string.Compare(pos.UtilizadorOrigem, user, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    aux.Add(pos);
                }
                else
                {
                    var novaRelacao = new RelacaoDTO(pos.RelacaoId, pos.UtilizadorDestino,
                        pos.UtilizadorOrigem, pos.ForcaLigacaoDestOrig,
                        pos.ForcaLigacaoOrigDest, pos.TagsRelacaoBA, pos.TagsRelacaoAB);

                    aux.Add(novaRelacao);
                }

            return aux;
        }

        public async Task<ActionResult<TamanhoRedeSocialTotalDTO>> getTamanhoRedeSocial()
        {
            var count = await _serviceUtilizador.getTotalUsers();
            var dto = new TamanhoRedeSocialTotalDTO(count);
            return dto;
        }
    }
}