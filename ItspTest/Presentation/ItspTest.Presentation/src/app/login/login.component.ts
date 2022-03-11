import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, Subject, take, takeUntil } from 'rxjs';
import { AccountUIService } from '../account-ui.service';
import { LoginDataService } from './login-data.service';
import { LoginResponse } from './login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {

  username = "";
  password = "";

  private destroy$ = new Subject();

  constructor(
    private dataService: LoginDataService,
    private accountUIService: AccountUIService,
    private router: Router) { }

  ngOnInit(): void {
    this.subscribeToUserId();
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

  login() {
    this.dataService.login(this.username, this.password)
      .pipe(takeUntil(this.destroy$))
      .subscribe((data: LoginResponse) => {
        localStorage.setItem('itspUserId', data.userId);
        localStorage.setItem('itspToken', data.token);

        this.router.navigate(['movie-collections']);
      });
  }

  navigateToRegister() {
    this.router.navigate(['register']);
  }

  private subscribeToUserId() {
    this.accountUIService.getUserId()
      .pipe(takeUntil(this.destroy$))
      .subscribe((userId: string) => {
        if (!userId && userId !== "") {
          this.router.navigate(['movie-collections']);
        }
      });
  }
}
