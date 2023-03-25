import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.css']
})
export class SideNavComponent {

  opened = false;
  @ViewChild('sidenav', { static: true }) sidenav: MatSidenav | undefined;

  eRef: ElementRef;

  constructor( eRef: ElementRef ) 
  {
      this.eRef = eRef
  }

  ngOnInit() {
    this.opened = false;

  }

  @HostListener('document:click', ['$event'])
  clickout(event : any) {
    if(this.eRef.nativeElement.contains(event.target)) {
      this.opened = true;
    } else {
      this.opened = false;
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.opened = false;
  }
  
  isBiggerScreen() {
    const width = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
    if (width < 768) {
      return true;
    } else {
      return false;
    }
  }


}
