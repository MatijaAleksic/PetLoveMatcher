import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  private _api_url = 'https://localhost:8080/api';
  private _auth_url = this._api_url + '/auth';
  // private _login_url = this._auth_url + '/login';


  private _users_url = this._api_url + '/users';
  private _login_url = this._auth_url + '/login';
  private _antiforgery_url = this._auth_url + "/antiforgery"


  get api_url(): string {
    return this._api_url;
  }

  get auth_url(): string {
    return this._auth_url;
  }

  get login_url(): string {
    return this._login_url;
  }

  get users_url(): string {
    return this._users_url;
  }

  get antiforgery_url(): string {
    return this._antiforgery_url;
  }


}
