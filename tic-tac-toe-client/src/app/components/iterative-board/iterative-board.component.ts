import { Component, Input } from '@angular/core';
import Match from '../../models/Match';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { MatchService } from '../../services/match.service';

@Component({
  selector: 'app-iterative-board',
  imports: [ MatInputModule, MatFormFieldModule, ReactiveFormsModule, 
    MatButtonModule, MatSelectModule ],
  templateUrl: './iterative-board.component.html',
  styleUrl: './iterative-board.component.css'
})
export class IterativeBoardComponent {

  constructor(private router: Router, private service: MatchService) { }

  selected = 0
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

  fields = [ 0, 1, 2, 3, 4, 5, 6, 7, 8 ]

  play() {
    this.service.play(this.match.id, this.selected)
      .subscribe(res => {
        alert(res.message)
      })
  }

  exit() {
    this.router.navigate(["game"])
  }
}
