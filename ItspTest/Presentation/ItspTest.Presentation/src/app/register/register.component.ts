import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { AccountUIService } from '../account-ui.service';
import { RegisterDataService } from './register-data.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject();

  username = "";
  password = "";
  firstName = "";
  lastName = "";

  constructor(
    private accountUIService: AccountUIService,
    private router: Router,
    private dataService: RegisterDataService
  ) { }

  ngOnInit(): void {
    this.subscribeToUserId();
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

  register() {
    this.dataService.register(this.username, this.password, this.firstName, this.lastName)
      .pipe(takeUntil(this.destroy$))
      .subscribe(data => {
        if (data) {
          this.accountUIService.setUserId("");
          this.accountUIService.setToken("");
          this.router.navigate(['login']);
        }
      });
  }

  private subscribeToUserId() {
    this.accountUIService.getUserId()
      .pipe(takeUntil(this.destroy$))
      .subscribe((userId: string) => {
        if (!userId && userId !== "") {
          // TODO: Remove this 
          // this.router.navigate(['movie-collections']);
        }
      });
  }
}
