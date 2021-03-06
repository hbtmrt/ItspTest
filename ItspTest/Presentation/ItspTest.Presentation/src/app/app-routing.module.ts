import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CollectionDetailsComponent } from './collection-details/collection-details.component';
import { LoginComponent } from './login/login.component';
import { MovieCollectionComponent } from './movie-collection/movie-collection.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  { 
    path: 'login', 
    component: LoginComponent 
  },
  { 
    path: 'register', 
    component: RegisterComponent 
  },
  { 
    path: 'movie-collections', 
    component: MovieCollectionComponent 
  },
  { 
    path: 'movie-collections/:id/movies', 
    component: CollectionDetailsComponent 
  },
  {
    path: '',
    pathMatch: 'full',
    redirectTo: '/login'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
