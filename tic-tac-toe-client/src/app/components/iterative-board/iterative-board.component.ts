import { Component, Input } from '@angular/core';
import Match from '../../models/Match';

@Component({
  selector: 'app-iterative-board',
  imports: [],
  templateUrl: './iterative-board.component.html',
  styleUrl: './iterative-board.component.css'
})
export class IterativeBoardComponent {

  @Input()
  match: Match = {
    id: '',
    player1: '',
    player2: '',
    active: null,
    status: '',
    winnner: null,
    game: [],
    playerTime: null,
    youPlay: false
  }
}
