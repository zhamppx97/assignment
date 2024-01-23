import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoaderService } from '../loader.service';

@Component({
    selector: 'app-loader',
    templateUrl: './loader.component.html',
    styleUrls: ['./loader.component.css']
})
export class LoaderComponent implements OnInit {
    isLoading: boolean = false;
    isGlobalLoading: boolean = false;
    private t!: any;
    delay = 300;

    constructor(private loaderService: LoaderService, private spinner: NgxSpinnerService) {
        this.spinner.show();
    }

    ngOnInit(): void {
        this.loaderService.isInterceptorLoading.subscribe(x => {
            this.isLoading = x;
        });

        this.loaderService.isGlobalLoading.subscribe(x => {
            this.isGlobalLoading = x;
        });
    }
}
