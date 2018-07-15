import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import { LandMark } from '../../models/landMark';
import {environment} from '../../../environments/environment'

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class LandmarkService {
  apiUrl : string;
  env : any;
  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiUrl;
   }

  getAll() {
      return this.http.get<LandMark[]>(this.apiUrl + 'api/getalllandmarks');
  }

  create(marker: LandMark) {
      return this.http.post(this.apiUrl + 'api/createMarkerAndText', marker);
  }

  update(marker: LandMark) {
      return this.http.put(this.apiUrl + 'api/updatelandmark' ,marker);
  }

  search(searchText: string) {
    return this.http.get<LandMark[]>(this.apiUrl + 'api/search/' + searchText);
  }
}
