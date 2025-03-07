import { Component, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-initial-page',
  imports: [ MatInputModule, MatFormFieldModule, ReactiveFormsModule, MatButtonModule ],
  templateUrl: './initial-page.component.html',
  styleUrl: './initial-page.component.css'
})
export class InitialPageComponent implements OnInit {
  username = new FormControl("");

  constructor(private service: UserService, private router: Router) { }

  ngOnInit(): void {
    if (sessionStorage.getItem("token") != null) {
      this.router.navigate([ "game" ])
    }
  }

  play() {
    this.service.newSession(this.username.value ?? "randomname")
      .subscribe(res =>
      {
        sessionStorage.setItem("token", res.token)
        this.router.navigate([ "game" ])
      })
  }
}
