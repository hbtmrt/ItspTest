import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { LoginDataService } from './login/login-data.service';
import { AccountUIService as AccountUIService } from './account-ui.service';
import { RegisterComponent } from './register/register.component';
import { MovieCollectionComponent } from './movie-collection/movie-collection.component';
import { RegisterDataService } from './register/register-data.service';
import { CollectionDataService } from './collection-data.service';

const providers: any = [
  LoginDataService,
  AccountUIService,
  RegisterDataService,
  CollectionDataService
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    MovieCollectionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    HttpClientModule
  ],
  providers: providers,
  bootstrap: [AppComponent]
})
export class AppModule { }
