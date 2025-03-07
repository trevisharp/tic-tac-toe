import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-page',
  imports: [ MatInputModule, MatFormFieldModule, ReactiveFormsModule, MatButtonModule ],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.css'
})
export class MainPageComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
    console.log("oi")
    if (sessionStorage.getItem("token") == null) {
      this.router.navigate([ "" ])
    }
  }

  newGame() {
    
  }

  userpage() {

  }

  exit() {
    sessionStorage.removeItem("token")
    this.router.navigate([ "" ])
  }

}
