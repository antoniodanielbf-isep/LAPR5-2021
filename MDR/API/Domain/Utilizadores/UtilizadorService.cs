using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Utilizadores.DTO;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Domain.Utilizadores
{
    public class UtilizadorService
    {
        private readonly IRelacaoRepository _repoR;
        private readonly IUtilizadorRepository _repoU;
        private readonly IUnitOfWork _unitOfWork;

        public UtilizadorService(IUnitOfWork unitOfWork, IUtilizadorRepository repoU,
            IRelacaoRepository repoR)
        {
            _unitOfWork = unitOfWork;
            _repoU = repoU;
            _repoR = repoR;
        }

        // GET: api/Utilizador
        public async Task<List<UtilizadorDTO>> GetAllAsync()
        {
            var list = await _repoU.GetAllAsync();

            var listDto = list.ConvertAll(usr =>
                usr.toDto());

            return listDto;
        }

        // GET: api/Utilizador/{email}
        public async Task<UtilizadorDTO> GetByIdAsync(EmailUtilizador id)
        {
            var usr = await _repoU.GetByIdAsync(id);
            if (usr == null) return null;
            return usr.toDto();
        }

        // GET: api/Utilizador/{email}/allTagsAllUsers
        public async Task<ActionResult<UtilizadorTagsDTO>> GetAllTagsAllUsers()
        {
            var list = await _repoU.GetAllAsync();

            var listTags = list.ConvertAll(usr => usr.getTags());

            var tagFinal = new StringBuilder();
            var i = 0;

            foreach (var listT in listTags)
            {
                foreach (var tag in listT)
                    if (!tagFinal.ToString().Contains(tag))
                    {
                        tagFinal = tagFinal.Append(tag);
                        tagFinal = tagFinal.Append(", ");
                    }

                if (i + 1 != listT.Count) tagFinal = tagFinal.Append(", ");

                i += 1;
            }

            return new UtilizadorTagsDTO(tagFinal.ToString());
        }

        // GET: api/Utilizador/{email}/allTagsUser
        public async Task<ActionResult<UtilizadorTagsDTO>> GetAllTagsUser(string email)
        {
            var usr = await _repoU.GetByIdAsync(new EmailUtilizador(email));

            return usr.toDtoTags();
        }

        // GET: api/Utilizador/{emailUtilizador}/search
        public async Task<ActionResult<IEnumerable<UtilizadorDTO>>> GetAllFromSearch(
            UtilizadorPesquisaDTO pesquisaUtilizadorDto)
        {
            var lista = _repoU.pesquisaViaDTO(pesquisaUtilizadorDto);
            var result = lista.ConvertAll(u => u.toDto());
            return result;
        }

        // GET: api/Utilizador/{email}/creating
        public async Task<UtilizadorSugestoesDTO> GetByIdCreationAsync(EmailUtilizador id)
        {
            var usr = await _repoU.GetByIdAsync(id);

            if (usr == null) return null;

            var list = await _repoU.GetAllAsync();
            foreach (var user in list)
                if (user.Id.ToString().Equals(id.ToString()))
                {
                    list.Remove(user);
                    return usr.toDto2(criarSugestoesDeAmizade(list));
                }

            return null;
        }

        // POST: api/Utilizador
        public async Task<UtilizadorDTO> AddAsync(UtilizadorDTO dto)
        {
            await CheckUserExistIdAsync(new EmailUtilizador(dto.Email));
            var builder = new UtilizadorBuilder();


            builder.setNome(dto.NomeUtilizador)
                .setTags(dto.TagsUtilizador)
                .setDataNascimento(dto.DataDeNascimentoUtilizador)
                .setNrTelefone(dto.NumeroDeTelefoneUtilizador)
                .setEmail(dto.Email)
                .setPassword(dto.PasswordU)
                //opcionais
                .setEstadoEmocional(dto.EstadoEmocionalUtilizador)
                .setCidadeEPaisResidencia(dto.CidadePaisResidencia)
                .setBreveDescricao(dto.BreveDescricaoUtilizador)
                .setImagemAvatar(dto.UrlImagem)
                .setPerfilFacebook(dto.PerfilFacebookUtilizador)
                .setPerfilLinkedin(dto.PerfilLinkedinUtilizador);
            var usr = builder.createUtilizador();

            await _repoU.AddAsync(usr);

            await _unitOfWork.CommitAsync();

            return usr.toDto();
        }

        // PUT: api/Utilizador/{email}/changeHumor
        public async Task<UtilizadorDTO> UpdateAsync(string email, UtilizadorAlterarHumorDTO dto)
        {
            await CheckIfIsAValidUser(new EmailUtilizador(email));
            var usr = await _repoU.GetByIdAsync(new EmailUtilizador(email));

            if (usr == null)
                return null;

            usr.setEstadoEmocional(EstadoEmocionalOperations.getEstadoEmocionalById(dto.EstadoEmocionalUtilizador));

            await _unitOfWork.CommitAsync();

            return usr.toDto();
        }

        // PUT: api/Utilizador/{email}/changeAllInfo
        public async Task<UtilizadorDTO> UpdateAllAsync(string email, UtilizadorAlterarDadosDTO dto)
        {
            await CheckIfIsAValidUser(new EmailUtilizador(email));
            var usr = await _repoU.GetByIdAsync(new EmailUtilizador(email));

            if (usr == null)
                return null;

            // change all fields

            //String.IsNullOrEmpty()

            usr.setNomeUtilizador(new Nome(dto.NomeUtilizador));
            if (string.Compare("*****",dto.PasswordU,StringComparison.OrdinalIgnoreCase)!=0)
            {
                usr.setPassword(new Password(dto.PasswordU));

            }
            usr.setBreveDescricao(new BreveDescricao(dto.BreveDescricaoUtilizador));
            usr.setImagemAvatar(new ImagemAvatar(new Uri(dto.UrlImagem)));
            usr.setCidadeEPaisResidencia(new CidadeEPaisResidencia(dto.CidadePaisResidencia));
            usr.setPerfilFacebook(new PerfilFacebook(new Uri(dto.PerfilFacebookUtilizador)));
            usr.setPerfilLinkedin(new PerfilLinkedin(new Uri(dto.PerfilLinkedinUtilizador)));


            var listaAux = new List<string>();

            foreach (var tag in dto.TagsUtilizador.Split(",")) listaAux.Add(tag);

            var tagList = listaAux.ConvertAll(tt =>
                new Tag(tt));

            usr.setTags(tagList);

            await _unitOfWork.CommitAsync();

            return usr.toDto();
        }
        
        // PUT: api/Utilizador/{email}/eraseUser
        public async Task<UtilizadorEraseDTO> EraseAllAsync(string email)
        {
            var usr = await _repoU.GetByIdAsync(new EmailUtilizador(email));
            var emailNovo = "eliminado" + (await _repoU.GetAllAsync()).Count + "@eliminado.com";

            if (usr == null)
                return null;

            _repoU.Remove(usr);

            var builder = new UtilizadorBuilder();
            builder.setNome("-----")
                .setTags("-----")
                .setDataNascimento("01/01/1800")
                .setNrTelefone("-----")
                .setEmail(emailNovo)
                .setPassword("-----")
                //opcionais
                .setEstadoEmocional(0)
                .setCidadeEPaisResidencia("-----")
                .setBreveDescricao("-----")
                .setImagemAvatar("-----")
                .setPerfilFacebook("-----")
                .setPerfilLinkedin("-----");
            var usrNew = builder.createUtilizador();

            await _repoU.AddAsync(usrNew);
            
            await _unitOfWork.CommitAsync();

            return usrNew.toEraseDto();
        }


        // GET: api/Utilizador/{email}/sugestoesAmizade
        public async Task<ActionResult<IEnumerable<UtilizadorSugestoesSendDTO>>> GetSugestoesAmizade(
            string emailUtilizador)
        {
            var usr = await _repoU.GetByIdAsync(new EmailUtilizador(emailUtilizador));

            //OBTER TODOS
            var allNetwork = await _repoU.GetAllAsync();

            //REMOVER USER
            allNetwork.Remove(usr);

            //REMOVER AMIGOS
            var amigos = _repoR.GetRelacoesFromUser(emailUtilizador);

            foreach (var rel in amigos)
                if (rel.UtilizadorOrigem.ToString().Equals(emailUtilizador))
                    allNetwork.Remove(await _repoU.GetByIdAsync(new EmailUtilizador(rel.UtilizadorDestino.ToString())));
                else
                    allNetwork.Remove(await _repoU.GetByIdAsync(new EmailUtilizador(rel.UtilizadorOrigem.ToString())));

            var listFinal = new List<Utilizador>();

            //FILTRAR OS QUE TÊM PELO MENOS X TAGS COMUNS
            if (allNetwork.Count == 0) return null;

            var listTagUser = usr.TagsUtilizador;
            foreach (var user in allNetwork)
            {
                var listTagUserAmigo = user.TagsUtilizador;
                foreach (var tag in listTagUserAmigo)
                    if (!listFinal.Contains(user) && listTagUser.Contains(tag))
                        listFinal.Add(user);
            }

            //RETORNAR 8 ELEMENTOS LISTA
            if (listFinal.Count > 8)
                for (var i = 0; i < listFinal.Count - 8; i++)
                {
                    var random = new Random();
                    listFinal.RemoveAt(random.Next(0, listFinal.Count - 1));
                }

            return listFinal.ConvertAll(users => users.toDtoSugestoes());
        }


        // OTHER
        private async Task CheckUserExistIdAsync(EmailUtilizador emailId)
        {
            var usr = await _repoU.GetByIdAsync(emailId);
            if (usr != null)
                throw new BusinessRuleValidationException("Utilizador ja existe.");
        }

        private async Task CheckIfIsAValidUser(EmailUtilizador emailId)
        {
            var usr = await _repoU.GetByIdAsync(emailId);
            if (usr == null)
                throw new BusinessRuleValidationException("Utilizador nao existe.");
        }

        private List<string> criarSugestoesDeAmizade(List<Utilizador> list)
        {
            var listaUsers = new List<string>();

            if (list.Count > 0)
            {
                var random = new Random();
                //mudar para Tags dps
                var quantidade = random.Next(1, list.Count);
                Console.WriteLine("2:  " + quantidade);

                for (var i = 0; i < quantidade; i++)
                {
                    var numeroRandom = random.Next(0, list.Count - 1);
                    Console.WriteLine("3:  " + numeroRandom);
                    listaUsers.Add(list[numeroRandom].Id.ToString());
                    list.Remove(list[numeroRandom]);
                }
            }

            return listaUsers;
        }

        public async Task<int> getTotalUsers()
        {
            return _repoU.getTotalUsers();
        }

        public async Task<ActionResult<IEnumerable<UtilizadorSugestoesSendDTO>>> GetSugestoesUtilizadoresParaPedirIntroducao(string emailUtilizador)
        {
            //buscar as relações das relações do user
            var relacoesUtilizador = _repoR.GetRelacoesFromUser(emailUtilizador);
            var relacoesAmigosUtilizador = new List<Relacao>();
            
            foreach (var relacaoUtilizador in relacoesUtilizador)
            {
                var amigo = relacaoUtilizador.UtilizadorDestino.Email;
                var relacoesDoAmigo = _repoR.GetRelacoesFromUser(amigo);
                relacoesAmigosUtilizador.AddRange(relacoesDoAmigo);
            }
            
             //tirar delas aquelas q tenham o user 
              relacoesAmigosUtilizador.RemoveAll(rel =>
                      rel.UtilizadorOrigem.Email.Equals(emailUtilizador) ||
                      rel.UtilizadorDestino.Email.Equals(emailUtilizador)
                  );
            
            
            //buscar os utilizadores
            var listaFinal = new List<Utilizador>();
            foreach (var rel in relacoesAmigosUtilizador)
            {
                var userSugestaoOrigem = await _repoU.GetByIdAsync(new EmailUtilizador(rel.UtilizadorOrigem.Email));
                var userSugestaoDestino = await _repoU.GetByIdAsync(new EmailUtilizador(rel.UtilizadorDestino.Email));

                if (!listaFinal.Contains(userSugestaoOrigem))
                {
                    listaFinal.Add(await _repoU.GetByIdAsync(new EmailUtilizador(rel.UtilizadorOrigem.Email)));
                }

                if (!listaFinal.Contains(userSugestaoDestino))
                {
                    listaFinal.Add(await _repoU.GetByIdAsync(new EmailUtilizador(rel.UtilizadorDestino.Email)));
                }
            }
            
            //tirar os amigos do utilizador da lista
            foreach (var rel in relacoesUtilizador)
                if (rel.UtilizadorOrigem.ToString().Equals(emailUtilizador))
                    listaFinal.Remove(await _repoU.GetByIdAsync(new EmailUtilizador(rel.UtilizadorDestino.ToString())));
                else
                    listaFinal.Remove(await _repoU.GetByIdAsync(new EmailUtilizador(rel.UtilizadorOrigem.ToString())));
            
            
            //FILTRAR OS QUE TÊM PELO MENOS X TAGS COMUNS
            if (listaFinal.Count == 0) return null;
            
            var usr = await _repoU.GetByIdAsync(new EmailUtilizador(emailUtilizador));
            var listTagUser = usr.TagsUtilizador;
            foreach (var user in listaFinal)
            {
                var listTagUserAmigo = user.TagsUtilizador;
                foreach (var tag in listTagUserAmigo)
                    if (!listaFinal.Contains(user) && listTagUser.Contains(tag))
                        listaFinal.Add(user);
            }
            
            
            //RETORNAR 8 ELEMENTOS LISTA
            if (listaFinal.Count > 8)
                for (var i = 0; i < listaFinal.Count - 8; i++)
                {
                    var random = new Random();
                    listaFinal.RemoveAt(random.Next(0, listaFinal.Count - 1));
                }

            return listaFinal.ConvertAll(users => users.toDtoSugestoes());
        }
    }
}