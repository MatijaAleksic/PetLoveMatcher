import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { ConfigService } from './config.service';
import { ApiService } from './api.service';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class AntiforgeryService {

  constructor(
    private http: HttpClient,
    private config: ConfigService,
    private apiService : ApiService,

    private authService : AuthService
    ) { }

  getToken(): Observable<any> { //requestVerificationToken : any

    const headers = new HttpHeaders({
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      // 'RequestVerificationToken': requestVerificationToken
    });


    return this.apiService.get(this.config.antiforgery_url)
      .pipe(map((response) => {

        this.authService.setXsrfToken(response.token);
        console.log(response);
      }));


    // return this.http.get(this.config.antiforgery_url).pipe(
    //   map( (response) => {
    //     console.log(response);

    //     // return {
    //     //   response
    //     //   // headerName: response['headerName'],
    //     //   // token: response['token']
    //     // };
    //   })
    // );
  }
}
