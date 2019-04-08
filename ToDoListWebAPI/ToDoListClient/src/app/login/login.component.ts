import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { timer } from 'rxjs';
import { first } from 'rxjs/operators';
import { AuthService } from '../Services/auth.service';

@Component({
  selector: 'app-login-component',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  userIdControl = new FormControl('', [Validators.required]);
  passwordControl = new FormControl('', [Validators.required]);
  hide = true;

  userId: string;
  password: string;

  loading = false;
  submitted = false;
  returnUrl: string;
  error: HttpErrorResponse;

  timeLeft = 5;
  interval;
  subscribeTimer: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {}

  ngOnInit() {
    // reset login status
    this.authService.logout();

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/todo';
    console.log(this.route.snapshot.queryParams);
    this.returnUrl = '/todo';
  }

  onSubmit() {
    this.submitted = true;

    this.userId = this.userIdControl.value;
    this.password = this.passwordControl.value;

    this.loading = true;
    this.authService
      .login(this.userId, this.password)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.error = error;
          this.loading = false;
          console.log('Error Details: ', this.error.error.message);
        }
      );
  }

  /** Get FormControl Error Messages */
  getErrorMessage(validator: FormControl) {
    if (validator.hasError('required')) {
      return 'You must enter a value';
    } else if (validator.hasError('maxlength')) {
      return 'You have exceeded the Max Length';
    } else if (validator.hasError('min') || validator.hasError('max')) {
      return 'You must enter a number between 0 and 10';
    } else {
      return '';
    }
  }

  disableButton() {
    if (
      this.userIdControl.hasError('required') ||
      this.passwordControl.hasError('required')
    ) {
      return true;
    } else {
      return false;
    }
  }

  oberserableTimer() {
    const source = timer(1000, 2000);
    const abc = source.subscribe(val => {
      console.log(val, '-');
      this.subscribeTimer = this.timeLeft - val;
    });
    console.log('Done');
  }
}
