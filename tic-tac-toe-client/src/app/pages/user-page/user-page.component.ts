import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import Stats from '../../models/Stats';
import { Router } from '@angular/router';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-user-page',
  imports: [ MatInputModule, MatFormFieldModule, ReactiveFormsModule, MatButtonModule ],
  templateUrl: './user-page.component.html',
  styleUrl: './user-page.component.css'
})
export class UserPageComponent implements OnInit {

  stats: Stats = {
    username: '',
    finishedMatches: 0,
    wins: 0
  }
  winrate = 0
  constructor(private service: UserService, private router: Router) { }

  ngOnInit(): void {
    this.service.getStats().subscribe(res => {
      this.stats = res
      this.winrate = 100 * this.stats.wins / this.stats.finishedMatches
      if (isNaN(this.winrate))
          this.winrate = 0
    })
  }

  exit() {
    this.router.navigate([ "game" ])
  }
}
