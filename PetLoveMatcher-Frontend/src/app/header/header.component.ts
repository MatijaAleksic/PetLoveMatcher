import { Component, HostListener, OnInit, ViewChild ,ElementRef, Input} from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
// import { AuthService } from '../services/auth.service';
// import {UserService} from '../service/user.service';
// import {AuthService} from '../service/auth.service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  // opened = false;
  // @ViewChild('sidenav', { static: true }) sidenav: MatSidenav;
  
  private signedIn = true;
  private userAdmin = true;
  // eRef: ElementRef;

  // private authService: AuthService;

  constructor() {}

  // constructor( eRef: ElementRef, authService: AuthService
  //   // private userService: UserService
  //   ) {
  //     this. authService = authService
  //     // this.eRef = eRef
  //    }

  ngOnInit() {
    // this.sidenav.fixedTopGap = 55;
    // this.opened = false;
    // console.log(window.innerWidth)
    // if (window.innerWidth < 768) {
    //   this.sidenav.fixedTopGap = 55;
    //   this.opened = false;
    // } else {
    //   this.sidenav.fixedTopGap = 55;
    //   this.opened = true;
    // }
  }


  hasSignedIn() {
    return false;
    // return !!this.authService.getCurrentUser();
  }

  logout() {
    // this.authService.logout();
    alert("Logout()");
  }

  isUserAdmin(){
    // if(this.authService.getCurrentUser() != null){

    //   for (let entry of this.authService.getUserRole()) {
    //     if(entry.name === "ROLE_ADMIN"){
    //       return true;
    //     }
    //   }
    // }
    return true;
  }

  userName() {
    // const user = this.authService.getCurrentUser();
    // return user.firstName + ' ' + user.lastName;
  }

}
