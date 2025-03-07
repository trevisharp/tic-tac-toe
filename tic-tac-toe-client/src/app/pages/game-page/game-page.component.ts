import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IterativeBoardComponent } from '../../components/iterative-board/iterative-board.component';
import Match from '../../models/Match';
import { MatchService } from '../../services/match.service';

@Component({
  selector: 'app-game-page',
  imports: [ IterativeBoardComponent ],
  templateUrl: './game-page.component.html',
  styleUrl: './game-page.component.css'
})
export class GamePageComponent implements OnInit {

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

  constructor(private route: ActivatedRoute, private service: MatchService){}

  ngOnInit(): void {
    this.route.params.subscribe(param =>{
      var id = param['id'];
      this.service.getMatch(id).subscribe(res => {
        this.match = res;
      })
    })
  }
}
