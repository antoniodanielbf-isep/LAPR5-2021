import {Injectable} from "@angular/core";
import {HttpClient, HttpErrorResponse, HttpHeaders} from "@angular/common/http";
import {urlBaseMDR} from '../../config';
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import {Observable, Subject} from "rxjs";
import {Token} from "@angular/compiler";
import {MatSnackBar} from "@angular/material/snack-bar";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'my-auth-token'
  })
};

@Injectable(
  {providedIn: 'root'}
)

export class LoginService {
  constructor(private http: HttpClient,private notification:MatSnackBar) {
    this.userInfo=localStorage["userInfo"];
  }


  public userInfo: IAuthDTO;
  routeUrl = urlBaseMDR + "Autenticacao/";
  authentication: Subject<IAuthDTO> = new Subject<IAuthDTO>();


  getAuth(email:string,password:string): Observable<boolean>{
    return new Observable<boolean>(observer => {
      this.http.post<IAuthDTO>(this.routeUrl, { email: email, password: password }).subscribe(resposta => {
          if (resposta!=null) {
            this.userInfo = {email:resposta.email}
            localStorage.setItem('userInfo',"");
            localStorage.setItem('userInfo',this.userInfo.email);

            if (this.userInfo) {
              this.authentication.next(this.userInfo);
            }
            observer.next(true);
          } else {
            if (this.userInfo) {
              this.authentication.next(this.userInfo);
            }
            observer.next(false);
          }
        },
        (err: HttpErrorResponse) => {
          if (err.error instanceof Error) {
            this.mostrarNotificacao("Erro na SPA",true);
            console.warn("Erro na SPA!");
          } else {
            this.mostrarNotificacao("Erro no MDR!",true);
            console.warn("Erro no MDR!");
          }
          console.warn(err);

          if (this.userInfo) {
            this.authentication.next(this.userInfo);
          }
          observer.next(false);
        });
    });
  }

  logout() {
    //this.userInfo.email="";
    localStorage.removeItem('userInfo');

    // @ts-ignore
    this.authentication.next(this.userInfo);
  }

  private mostrarNotificacao(mensagem: string, falha: boolean) {
    var snackbarColor = falha ? 'red-snackbar' : 'green-snackbar';
    this.notification.open(mensagem, 'Close', {duration: 4000, panelClass: [snackbarColor]});
  }

}
