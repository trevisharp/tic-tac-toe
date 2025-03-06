import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

interface Token {
  token: string
}

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
    return this.client.get(this.backend + "player", { 
      headers: {
        "Authorization": "Bearer" + (sessionStorage.getItem("token") ?? "no-token")
      }
     })
  }
}
