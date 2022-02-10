import {Result} from '../core/logic/Result';
import {Service, Inject} from 'typedi';
import config from '../../config';

import IUtilizadorDTO from '../dto/Utilizadores/IUtilizadorDTO';
import IUtilizadorFeedDTO from '../dto/Utilizadores/IUtilizadorFeedDTO';
import IUtilizadorIdDTO from '../dto/Utilizadores/IUtilizadorIdDTO';
import IUtilizadorOrDestDTO from '../dto/Utilizadores/IUtilizadorOrDestDTO';
import IUtilizadorLikeDislikeDTO from '../dto/Utilizadores/IUtilizadorLikeDislikeDTO';
import IUtilizadorEraseDTO from '../dto/Utilizadores/IUtilizadorEraseDTO';
import IUtilizadorLikesDislikesDTO from "../dto/Utilizadores/IUtilizadorLikesDislikesDTO";

import IUtilizadorRepo from '../services/IRepos/IUtilizadorRepo';
import IUtilizadorService from './IServices/IUtilizadorService';

import {Utilizador} from '../domain/Agregados/Utilizadores/utilizador';
import {UtilizadorMap} from '../mappers/Utilizadores/UtilizadorMap';
import {UtilizadorEmail} from '../domain/ValueObjects/Utilizadores/utilizadorEmail';
import {UtilizadorNome} from '../domain/ValueObjects/Utilizadores/utilizadorNome';

import IComentarioRepo from './IRepos/IComentarioRepo';
import IPostRepo from './IRepos/IPostRepo';

@Service()
export default class UtilizadorService implements IUtilizadorService {
  constructor(
    @Inject(config.repos.utilizador.name) private utilizadorRepo: IUtilizadorRepo,
    @Inject(config.repos.comentario.name) private comentarioRepo: IComentarioRepo,
    @Inject(config.repos.post.name) private postRepo: IPostRepo,
  ) {
  }

  public async getUtilizador(utilizadorId: string): Promise<Result<IUtilizadorDTO>> {
    try {
      const utilizador = await this.utilizadorRepo.findByDomainId(utilizadorId);

      if (utilizador === null) {
        return Result.fail<IUtilizadorDTO>('Utilizador not found');
      } else {
        const utilizadorDTOResult = UtilizadorMap.toDTO(utilizador) as IUtilizadorDTO;
        return Result.ok<IUtilizadorDTO>(utilizadorDTOResult);
      }
    } catch (e) {
      throw e;
    }
  }

  public async createUtilizador(utilizadorDTO: IUtilizadorDTO): Promise<Result<IUtilizadorDTO>> {
    try {
      const utilizadorOrError = await Utilizador.create(utilizadorDTO);

      if (utilizadorOrError.isFailure) {
        return Result.fail<IUtilizadorDTO>(utilizadorOrError.errorValue());
      }

      const utilizadorResult = utilizadorOrError.getValue();

      await this.utilizadorRepo.save(utilizadorResult);

      const utilizadorDTOResult = UtilizadorMap.toDTO(utilizadorResult) as IUtilizadorDTO;
      return Result.ok<IUtilizadorDTO>(utilizadorDTOResult);
    } catch (e) {
      throw e;
    }
  }

  public async updateUtilizador(utilizadorDTO: IUtilizadorEraseDTO): Promise<Result<IUtilizadorDTO>> {
    try {
      const utilizadors = await this.utilizadorRepo.findAll();
      const comentarios = await this.comentarioRepo.findAll();
      const posts = await this.postRepo.findAll();
      let numero = 0;

      for (let e = 0; e < utilizadors.length; e++) {
        if (utilizadors[e].email.value == utilizadorDTO.old) {
          utilizadors[e].email = new UtilizadorEmail({value: utilizadorDTO.novo});
          utilizadors[e].nome = new UtilizadorNome({value: '-----'});
          await this.utilizadorRepo.save(utilizadors[e]);
          numero = e;
        }
      }

      for (let i = 0; i < posts.length; i++) {
        if (posts[i].utilizador == utilizadorDTO.old) {
          posts[i].utilizador = utilizadorDTO.novo;
          await this.postRepo.save(posts[i]);
        }
      }

      for (let j = 0; j < comentarios.length; j++) {
        if (comentarios[j].utilizador == utilizadorDTO.old) {
          comentarios[j].utilizador = utilizadorDTO.novo;
          await this.comentarioRepo.save(comentarios[j]);
        }
      }

      const utilizadorDTOResult = UtilizadorMap.toDTO(utilizadors[numero]) as IUtilizadorDTO;
      return Result.ok<IUtilizadorDTO>(utilizadorDTOResult);
    } catch (e) {
      throw e;
    }
  }

  public async getFeedUtilizador(utilizadorIdDTO: IUtilizadorIdDTO): Promise<Result<IUtilizadorFeedDTO[]>> {
    try {
      const comentarios = await this.comentarioRepo.findAll();
      const posts = await this.postRepo.findAll();

      if (posts.length === 0) {
        return Result.fail<IUtilizadorFeedDTO[]>('No posts.');
      }

      const DtoFinal = [];
      let stringArray = [];

      for (let i = 0; i < posts.length; i++) {
        if (posts[i].utilizador == utilizadorIdDTO.email) {
          for (let j = 0; j < comentarios.length; j++) {
            if (comentarios[j].post == posts[i].id.toString()) {
              stringArray.push(comentarios[j].texto.value + '-' + comentarios[j].utilizador);
            }
          }
          DtoFinal.push(UtilizadorMap.toFeedDTO(posts[i], stringArray));
          stringArray = [];
        }
      }
      return Result.ok<IUtilizadorFeedDTO[]>(DtoFinal);
    } catch (e) {
      throw e;
    }
  }

  public async getStrengthUtilizador(
    utilizadorOrDestDTO: IUtilizadorOrDestDTO,
  ): Promise<Result<IUtilizadorLikeDislikeDTO>> {
    try {
      const comentarios = await this.comentarioRepo.findAll();
      const posts = await this.postRepo.findAll();
      let likes = 0;
      let dislikes = 0;

      for (let i = 0; i < posts.length; i++) {
        if (posts[i].utilizador == utilizadorOrDestDTO.emailDestino) {
          for (let j = 0; j < comentarios.length; j++) {
            if (
              comentarios[j].post == posts[i].id.toString() &&
              comentarios[j].utilizador == utilizadorOrDestDTO.emailOrigem
            ) {
              if (comentarios[j].reacao.value == 'like') {
                likes = likes + 1;
              }
              if (comentarios[j].reacao.value == 'dislike') {
                dislikes = dislikes + 1;
              }
            }
          }
        }
      }

      return Result.ok<IUtilizadorLikeDislikeDTO>(UtilizadorMap.toLikeDislikeDTO(likes - dislikes));
    } catch (e) {
      throw e;
    }
  }

  public async getLikeDislikeUtilizador(
    utilizadorIdDTO: IUtilizadorIdDTO,
  ): Promise<Result<IUtilizadorLikesDislikesDTO>> {
    try {
      const comentarios = await this.comentarioRepo.findAll();
      const posts = await this.postRepo.findAll();
      let likes = 0;
      let dislikes = 0;

      for (let i = 0; i < posts.length; i++) {
        if (posts[i].utilizador == utilizadorIdDTO.email) {
          for (let j = 0; j < comentarios.length; j++) {
            if (posts[i].id.toString() == comentarios[j].post) {
              if (comentarios[j].reacao.value == 'like') {
                likes = likes + 1;
              }
              if (comentarios[j].reacao.value == 'dislike') {
                dislikes = dislikes + 1;
              }
            }
          }
        }
      }

      return Result.ok<IUtilizadorLikesDislikesDTO>(UtilizadorMap.toLikesDislikesDTO(likes, dislikes));
    } catch (e) {
      throw e;
    }
  }
}
