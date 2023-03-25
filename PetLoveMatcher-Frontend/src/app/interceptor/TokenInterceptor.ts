import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpInterceptor,
  HttpEvent
} from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';
import { Observable } from 'rxjs';



// import * as Base64 from 'js-base64';
// import certificateFile from 'raw-loader!../../../ssl/frontend.cer';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(
    public auth: AuthService,
    public userService: UserService,
    ) 
    {   }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    console.log(request.method);

    if(request.method == "POST"){
      request = request.clone({
        withCredentials:  true,
        headers: request.headers.set("XSRF-TOKEN", `${this.auth.getXsrfToken()}`)
      });
    }
    else{
      request = request.clone({
        withCredentials:  true,
      });
    }

    console.log("INTERCEPT");
    return next.handle(request);
  }
}
