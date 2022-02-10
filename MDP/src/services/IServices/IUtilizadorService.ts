import { Result } from '../../core/logic/Result';
import IUtilizadorDTO from '../../dto/Utilizadores/IUtilizadorDTO';
import IUtilizadorFeedDTO from '../../dto/Utilizadores/IUtilizadorFeedDTO';
import IUtilizadorIdDTO from '../../dto/Utilizadores/IUtilizadorIdDTO';
import IUtilizadorOrDestDTO from '../../dto/Utilizadores/IUtilizadorOrDestDTO';
import IUtilizadorLikeDislikeDTO from '../../dto/Utilizadores/IUtilizadorLikeDislikeDTO';
import IUtilizadorEraseDTO from '../../dto/Utilizadores/IUtilizadorEraseDTO';
import IUtilizadorLikesDislikesDTO from "../../dto/Utilizadores/IUtilizadorLikesDislikesDTO";

export default interface IUtilizadorService {
  createUtilizador(utilizadorDTO: IUtilizadorDTO): Promise<Result<IUtilizadorDTO>>;
  updateUtilizador(utilizadorIdDTO: IUtilizadorEraseDTO): Promise<Result<IUtilizadorDTO>>;
  getFeedUtilizador(utilizadorIdDTO: IUtilizadorIdDTO): Promise<Result<IUtilizadorFeedDTO[]>>;
  getStrengthUtilizador(utilizadorOrDestDTO: IUtilizadorOrDestDTO): Promise<Result<IUtilizadorLikeDislikeDTO>>;
  getUtilizador(utilizadorId: string): Promise<Result<IUtilizadorDTO>>;
  getLikeDislikeUtilizador(utilizadorIdDTO: IUtilizadorIdDTO): Promise<Result<IUtilizadorLikesDislikesDTO>>;
}
