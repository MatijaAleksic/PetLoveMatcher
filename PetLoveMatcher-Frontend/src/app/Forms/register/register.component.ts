import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subject } from 'rxjs';
import { UserService } from 'src/app/services/user.service';
import { ActivatedRoute, Router } from '@angular/router';


interface DisplayMessage {
  msgType: string;
  msgBody: string;
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{

  form: any;
  submitted = false;
  notification: any;

  roles: string[] = ["ROLE_USER", "ROLE_ADMIN"]

  constructor(
    private userService: UserService,
    public router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit() {
    this.form = this.formBuilder.group({
      username: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(64)])],
      password: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(32)])],
      firstName: ['',Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(32)])],
      lastName: ['',Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(32)])],
      email: ['',Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(32), Validators.email])],
      // role: ['',Validators.compose([Validators.required])]
    });
  }

  onSubmit() {

    this.submitted = true;

    this.userService.addNewUser(this.form.value)
        .subscribe(data => {
          console.log(data);
          this.router.navigate(['/']);
        
        },
          error => {
            this.submitted = false;
            this.notification = { msgType: 'error', msgBody: error['error'].message };
            this.router.navigate(['/']);
          });
  }
}
