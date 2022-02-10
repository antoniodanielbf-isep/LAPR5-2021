import {Injectable} from "@angular/core";
import {HttpClient, HttpErrorResponse, HttpHeaders} from "@angular/common/http";
import {Observable, Subject} from "rxjs";
import {Token} from "@angular/compiler";
import {IAuthDTO} from "../../dto/login-dto/IAuthDTO";
import {Router} from "@angular/router";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'my-auth-token'
  })
};

@Injectable(
  {providedIn: 'root'}
)

export class InicioService {
  constructor(private http: HttpClient, private router: Router) {

  }

  // @ts-ignore
  public userInfo: IAuthDTO|null;
  authentication: Subject<IAuthDTO> = new Subject<IAuthDTO>();

  logout() {
    this.userInfo = null;
    localStorage.removeItem("userInfo");
    // @ts-ignore
    this.authentication.next(this.userInfo);
    this.router.navigate(['']);
  }

}
