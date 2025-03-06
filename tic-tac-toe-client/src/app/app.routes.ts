import { Routes } from '@angular/router';
import { InitialPageComponent } from './pages/initial-page/initial-page.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { UserPageComponent } from './user-page/user-page.component';
import { GamePageComponent } from './pages/game-page/game-page.component';

export const routes: Routes = [
    { path: "", component: InitialPageComponent },
    { path: "game", component: MainPageComponent },
    { path: "user", component: UserPageComponent },
    { path: "game/:id", component: GamePageComponent }
];