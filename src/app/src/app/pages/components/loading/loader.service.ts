import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { LoaderInterceptorService } from './loader-interceptor.service';

@Injectable({
    providedIn: 'root'
})
export class LoaderService {

    isInterceptorLoading = new BehaviorSubject(false);
    isGlobalLoading = new BehaviorSubject(false);
    activeRequests = 0;

    constructor() { }

    addRequests() {
        this.activeRequests++;
        this.checkLoading();
    }

    removeRequests() {
        this.activeRequests--;
        this.checkLoading();
    }

    private checkLoading() {
        if (this.activeRequests > 0) {
            this.startLoading();
        } else {
            this.activeRequests = 0;
            this.stopLoading();
        }
    }

    private startLoading() {
        this.isGlobalLoading.next(true);
    }

    private stopLoading() {
        this.isGlobalLoading.next(false);
    }

    resetLoading() {
        this.activeRequests = 0;
        this.isInterceptorLoading.next(false);
        this.isGlobalLoading.next(false);
    }
}
