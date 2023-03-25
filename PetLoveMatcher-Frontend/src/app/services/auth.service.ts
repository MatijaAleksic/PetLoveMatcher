import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { ConfigService } from './config.service';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class AuthService {

  constructor(
    private apiService: ApiService,
    private config: ConfigService,
    private router: Router,
  ) {
  }

  authStatus = false;
  private token = null;
  private access_token = null;
  private currentUser : any = null;
  private userRole : any = null;



  private xsrf_token : any = null;


  login(user : any, headers : HttpHeaders) {

    const body = {
      'username': user.username,
      'password': user.password
    };

    return this.apiService.post(this.config.login_url, JSON.stringify(body), headers)
      .pipe(map((response) => {
        console.log(response);
        
        this.currentUser = true;
        this.userRole = true;

        // this.currentUser = response.user;
        // this.userRole = response.user.roles;

        // this.access_token = response.accessToken;
        // this.authStatus = true;

        sessionStorage.setItem("jwt", response.token);
      }));
  }


  logout() {
    this.currentUser = null;
    this.access_token = null;
    this.authStatus = false;
    this.router.navigate(['/login']);
  
  }

  setXsrfToken(xsrf_token : any){
    this.xsrf_token = xsrf_token
  }

  getXsrfToken(){
    return this.xsrf_token;
  }





  tokenIsPresent() {
    return this.access_token != undefined && this.access_token != null;
  }

  getToken() {
    return this.access_token;
  }

  getExpires(){
    return this.token;
  }

  getAuthStatus(){
    return this.authStatus;
  }

  getUserRole(){
    return this.userRole;
  }

  getCurrentUser(){
    return this.currentUser;
  }


  setCurrentUser(user : any){
    this.currentUser = user;
  }
  setUserRole(role: any){
    this.userRole = role;
  }

  setAuthStatus(authStatus: any){
    this.authStatus = authStatus;
  }

  setAccessToken(accessToken: any){
    this.access_token = accessToken;
  }


}
