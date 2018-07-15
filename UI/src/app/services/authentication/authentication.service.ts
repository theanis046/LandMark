import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
 
@Injectable()
export class AuthenticationService {
    constructor(private http: HttpClient) { }
 
    login(username: string, password: string) {
        return this.http.post<any>('http://localhost:53438/user/login', { Username: username, Password: password })
            .map(user => {
                //if response has token in it than it is successfull login
                if (user && user.token) {
                    //store jwt in local storage
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }
 
                return user;
            })
    }

    errorHandler(error: any): void {
        console.log(error)
      }
    // remove user from local storage to log user out
    logout() {
        localStorage.removeItem('currentUser');
    }
}