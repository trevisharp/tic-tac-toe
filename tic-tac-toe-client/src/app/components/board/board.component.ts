import { Component, Input } from '@angular/core';
import Match from '../../models/Match';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';

@Component({
  selector: 'app-board',
  imports: [ MatButtonModule ],
  templateUrl: './board.component.html',
  styleUrl: './board.component.css'
})
export class BoardComponent {

  constructor(private router: Router) { }

  @Input()
  match: Match = {
    id: '',
    player1: '',
    player2: '',
    active: null,
    status: '',
    winnner: null,
    game: [ 0, 0, 0, 0, 0, 0, 0, 0 ],
    playerTime: null,
    youPlay: false
  }

  see() {
    this.router.navigate([ "game", this.match.id ])
  }
}
