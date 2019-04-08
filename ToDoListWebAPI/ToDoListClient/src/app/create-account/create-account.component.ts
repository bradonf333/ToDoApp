import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

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

  userId: string;
  password: string;
  error: HttpErrorResponse;
  constructor() {}

  ngOnInit() {}

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
}
