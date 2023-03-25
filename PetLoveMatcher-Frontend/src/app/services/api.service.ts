import {HttpClient, HttpHeaders, HttpRequest, HttpResponse, HttpParams} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {catchError, filter, map} from 'rxjs/operators';

export enum RequestMethod {
  Get = 'GET',
  Head = 'HEAD',
  Post = 'POST',
  Put = 'PUT',
  Delete = 'DELETE',
  Options = 'OPTIONS',
  Patch = 'PATCH'
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  headers = new HttpHeaders({
    'Accept': 'application/json',
    'Content-Type': 'application/json'
  });

  constructor(private http: HttpClient) { }

  options : any;
  get(path: string, args?: any): Observable<any> {
    return this.http.get(path).pipe(catchError(this.checkError.bind(this)));
  }

  post(path: string, body: any, customHeaders?: HttpHeaders): Observable<any> {
    return this.http.post(path, body, {'headers': this.headers});
  }

  put(path: string, body: any): Observable<any> {
    return this.http.put(path, body, {'headers': this.headers});
  }

  delete(path: string, body?: any): Observable<any> {
    return this.http.delete(path,{'headers': this.headers});
  }

  private checkError(error: any): any {
    throw error;
  }

}
