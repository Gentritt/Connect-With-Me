import { error } from '@angular/compiler/src/util';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validator, ValidatorFn, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    @Output() cancelRegister = new EventEmitter();
  model: any = {};

  registerForm: FormGroup | undefined;
  constructor(private accountService: AccountService, private toastr: ToastrService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initialForm();
  }

  initialForm() {
    this.registerForm = this.fb.group({
      username: ["", Validators.required],
      gender:["male"],
      knownAs:[ "", Validators.required],
      dateOfBirth: ["", Validators.required],
      city: ["", Validators.required],
      country: ["", Validators.required],
      password: ["", [Validators.required,
        Validators.minLength(4),
        Validators.maxLength(8)]],
      confirmPassword: ["", [Validators.required, this.matchValues('password')]]
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value ? null : { isMatching: true }
    }
  }
  register() {
    console.log(this.registerForm?.value);
        //this.accountService.register(this.model).subscribe(response => {
        //    console.log(response);
        //    this.cancel();
        //}, error => {
        //    console.log(error);
        //    this.toastr.error(error.error);
        //})
       
    }

    cancel() {
        this.cancelRegister.emit(false);
    }
}
