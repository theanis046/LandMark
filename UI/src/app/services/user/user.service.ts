import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
 
import { User } from '../../models/user';
import {environment} from '../../../environments/environment'
 
@Injectable()
export class UserService {
  apiUrl : string;
  env : any;
    constructor(private http: HttpClient) 
    { 
      this.env = environment;
      this.apiUrl = environment.apiUrl;
    }
 
    getProfile() {
      return this.http.get<User>(this.env.apiUrl + 'user/getProfile');
  }
 
    create(user: User) {
        return this.http.post(this.apiUrl + 'user/register', user);
    }
}