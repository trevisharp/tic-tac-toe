import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import Token from '../models/Token';
import Stats from '../models/Stats';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  backend = "http://localhost:5065/";
  constructor(private client: HttpClient) { }

  newSession(username: string) {
    return this.client.post<Token>(this.backend + "newSession", { username })
  }

  getStats() {
    return this.client.get<Stats>(this.backend + "player", { 
      headers: {
        "Authorization": "Bearer " + (sessionStorage.getItem("token") ?? "no-token")
      }
     })
  }
}
