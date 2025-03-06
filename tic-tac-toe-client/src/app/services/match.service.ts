import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  backend = "http://localhost:5065/";
  constructor(private client: HttpClient) { }

  new() {
    return this.client.post(this.backend + "new", null, {
      headers: {
        "Authorization": "Bearer" + (sessionStorage.getItem("token") ?? "no-token")
      }
    })
  }

  getMatches(status: string, page: number, limit: number) {
    return this.client.get(this.backend + `match?status=${status}&page=${page}&limit=${limit}`, {
      headers: {
        "Authorization": "Bearer" + (sessionStorage.getItem("token") ?? "no-token")
      }
    })
  }

  getMatch(id: string) {
    return this.client.get(this.backend + `match/${id}`)
  }

  play(gameId: string, play: number) {
    return this.client.post(this.backend + "new", { gameId, play }, {
      headers: {
        "Authorization": "Bearer" + (sessionStorage.getItem("token") ?? "no-token")
      }
    })
  }
}
