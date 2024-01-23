import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { CookieService } from 'ngx-cookie-service';
import { LayoutService } from "./service/app.layout.service";
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-topbar',
    templateUrl: './app.topbar.component.html'
})
export class AppTopBarComponent implements OnInit {

    items!: MenuItem[];
    @ViewChild('menubutton') menuButton!: ElementRef;
    @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;
    @ViewChild('topbarmenu') menu!: ElementRef;
    signOutDialog: boolean = false;
    deleteProductDialog: boolean = false;
    Username: any = "system";

    constructor(
        private naviagtor: Router,
        private cookieService: CookieService,
        public layoutService: LayoutService,
    ) {

    }

    ngOnInit(): void {
        
    }

    profile() {
        this.naviagtor.navigate(['profile']);
    }

    signOut() {
        this.signOutDialog = true;
    }

    confirmSignOut() {
        this.signOutDialog = false;
        this.naviagtor.navigate(['auth/login']);
    }
}
