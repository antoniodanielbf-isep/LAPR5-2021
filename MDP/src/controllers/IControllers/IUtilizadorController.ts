import { Request, Response, NextFunction } from 'express';

export default interface IUtilizadorController {
  createUtilizador(req: Request, res: Response, next: NextFunction);
  updateUtilizador(req: Request, res: Response, next: NextFunction);
  getFeedUtilizador(req: Request, res: Response, next: NextFunction);
  getStrengthUtilizador(req: Request, res: Response, next: NextFunction);
  getLikesDislikesUtilizador(req: Request, res: Response, next: NextFunction);
}
