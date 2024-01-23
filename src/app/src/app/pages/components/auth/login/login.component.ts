import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { UserServiceV1 } from 'src/app/pages/services/v1/user.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [`
        :host ::ng-deep .pi-eye,
        :host ::ng-deep .pi-eye-slash {
            transform:scale(1.6);
            margin-right: 1rem;
            color: var(--primary-color) !important;
        }
    `]
})
export class LoginComponent implements OnInit {

    loading: boolean = false;
    username!: string;
    password!: string;

    constructor(
        private navigator: Router,
        private cookieService: CookieService,
        private userServiceV1: UserServiceV1,
        public layoutService: LayoutService,
    ) {

    }

    ngOnInit() {
        
    }

    signIn() {
        this.navigator.navigate(['main']);
    }
}
