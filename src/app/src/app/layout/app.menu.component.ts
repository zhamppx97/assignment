import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    model: any[] = [];

    constructor(
        public layoutService: LayoutService,
    ) {

    }

    ngOnInit() {
        this.model = this.appMenu();
    }

    appMenu(): any[] {
        return [
            {
                label: 'Home',
                items: [
                    { label: 'Users', icon: 'pi pi-fw pi-users', routerLink: ['/main'] },
                ]
            },
        ];
    }
}
