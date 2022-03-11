import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { catchError, Subject, take, takeUntil } from 'rxjs';
import { AccountUIService } from '../account-ui.service';
import { LoginDataService } from './login-data.service';
import { LoginResponse } from './login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnInit, OnDestroy {

  username = "";
  password = "";

  private destroy$ = new Subject();

  constructor(
    private dataService$: LoginDataService,
    private accountUIService: AccountUIService) { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

  login() {
    this.dataService$.login(this.username, this.password)
    .pipe(takeUntil(this.destroy$))
    .subscribe((data: LoginResponse) => {
        this.accountUIService.setUserId(data.userId);
        this.accountUIService.setToken(data.token);
    });
  }
}
