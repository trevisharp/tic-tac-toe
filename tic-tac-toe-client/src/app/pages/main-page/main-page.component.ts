import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';
import { MatchService } from '../../services/match.service';
import { MatSelectModule } from '@angular/material/select';
import { BoardComponent } from '../../components/board/board.component';
import Match from '../../models/Match';

@Component({
  selector: 'app-main-page',
  imports: [ MatInputModule, MatFormFieldModule, ReactiveFormsModule, 
      MatButtonModule, MatSelectModule, BoardComponent ],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.css'
})
export class MainPageComponent implements OnInit {

  selected = 'in game'
  matches: Match[] = []

  constructor(private router: Router, private service: MatchService) { }

  ngOnInit(): void {
    if (sessionStorage.getItem("token") == null) {
      this.router.navigate([ "" ])
      return;
    }

    this.getData();
  }

  newGame() {
    this.service.new().subscribe(res => {
      alert(res.message)
    });
  }

  userpage() {
    this.router.navigate([ "user" ])
  }

  exit() {
    sessionStorage.removeItem("token")
    this.router.navigate([ "" ])
  }

  selectFilter() {
    this.getData()
  }

  getData() {
    this.service.getMatches(this.selected, 1, 12).subscribe(res => {
      console.log(res)
      this.matches = res
    })
  }
}