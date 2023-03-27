import { HttpHeaders } from '@angular/common/http';
import {Component, ElementRef, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import { DomSanitizer } from '@angular/platform-browser';
import {ActivatedRoute, Router} from '@angular/router';
import { Subject } from 'rxjs';
import {takeUntil} from 'rxjs/operators';
import { AntiforgeryService } from 'src/app/services/antiforgery.service';
import { AuthService } from '../../services/auth.service';

interface DisplayMessage {
  msgType: string;
  msgBody: string;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  title = 'Login';
  form : any;

  submitted = false;
  notification: any;

  @ViewChild('verificationToken') verificationToken:any;

  // returnUrl: string;
  // private ngUnsubscribe: Subject<void> = new Subject<void>();

  // private antiForgeryHeader: string;

  user : any;

  constructor(
    private authService: AuthService,
    private router: Router,
    // private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private sanitizer: DomSanitizer,
    private antiforgeryService: AntiforgeryService
    // private userService: UserService
  ) { }

  ngOnInit() {
    //this.verificationToken
    // this.antiforgeryService.getToken().subscribe(token => {
    //   // this.antiForgeryHeader = token.headerName + '=' + token.token;
    // });
    

    this.form = this.formBuilder.group({
      username: ['', Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(20)])],
      password: ['', Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(20)])]
    });
  }

  ngOnDestroy(){
    alert("destory");

    this.authService.getUserInfo()
    .subscribe(data => {
      console.log(data);
        this.router.navigate(['/']);
      },
      error => {
        this.submitted = false;
        this.notification = {msgType: 'error', msgBody: 'Incorrect username or password.'};
      });
  }

  onSubmit() {

    const customHeaders = new HttpHeaders({
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      // 'RequestVerificationToken': this.antiForgeryHeader
    });

    // console.log(this.verificationToken.nativeElement.value);

    this.submitted = true;

    this.authService.login(this.form.value, customHeaders)
      .subscribe(data => {
        console.log(data);
          this.router.navigate(['/']);
        },
        error => {
          this.submitted = false;
          this.notification = {msgType: 'error', msgBody: 'Incorrect username or password.'};
        });
  }
}
