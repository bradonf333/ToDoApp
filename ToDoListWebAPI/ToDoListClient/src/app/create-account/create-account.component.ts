import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material';
import { Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { UserRequest } from '../Models/UserRequest';
import { AuthService } from '../Services/auth.service';

@Component({
  selector: 'app-create-account',
  templateUrl: './create-account.component.html',
  styleUrls: ['./create-account.component.scss']
})
export class CreateAccountComponent implements OnInit {
  userIdControl = new FormControl('', [Validators.required]);
  passwordControl = new FormControl('', [Validators.required]);
  firstNameControl = new FormControl('', [Validators.required]);
  lastNameControl = new FormControl('', [Validators.required]);
  hide = true;

  newUser: UserRequest;

  userId: string;
  password: string;
  firstName: string;
  lastName: string;

  error: HttpErrorResponse;
  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {}

  openSnackBar(message: string) {
    this.snackBar.open(message, null, {
      duration: 2000
    });
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

  onSubmit() {
    this.newUser = {
      firstName: this.firstName,
      lastName: this.lastName,
      userId: this.userId,
      password: this.password,
      accountRoles: 0
    };

    // this.authService.register(this.newUser).subscribe(data => console.log(data));
    console.log('New User', this.newUser);
    this.authService
      .register(this.newUser)
      .pipe(first())
      .subscribe(
        data => {
          console.log(data);
        },
        error => {
          this.error = error;
          console.log('Error Details: ', this.error.error.message);
        }
      );

    this.openSnackBar('Registration Successful, please Login!');
    this.router.navigateByUrl('/login');
  }
}
