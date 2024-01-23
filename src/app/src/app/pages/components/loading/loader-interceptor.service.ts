import { Injectable } from '@angular/core';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { timeout } from 'rxjs/operators';
import { LoaderService } from './loader.service';

@Injectable({
    providedIn: 'root'
})
export class LoaderInterceptorService {
    private requests: HttpRequest<any>[] = [];
    // TODO : set time out to env
    timeout = 60 * 10000;

    constructor(private loaderService: LoaderService) { }

    removeRequest(req: HttpRequest<any>) {
        const i = this.requests.indexOf(req);
        if (i >= 0) {
            this.requests.splice(i, 1);
        }
        this.loaderService.isInterceptorLoading.next(this.requests.length > 0);
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.requests.push(req);
        this.loaderService.isInterceptorLoading.next(true);

        return new Observable(observer => {
            const subscription = next
                .handle(req)
                .pipe(timeout(this.timeout))
                .subscribe(
                    (event: HttpEvent<any>) => {
                        if (event instanceof HttpResponse) {
                            // logger.debug('HttpEvent', event);
                            this.removeRequest(req);
                            observer.next(event);
                        }
                    },
                    (error: HttpErrorResponse) => {
                        //logger.debug('HttpErrorResponse', error);
                        this.removeRequest(req);
                        observer.error(error);
                    },
                    () => {
                        // logger.debug('complete HttpEvent', req);
                        this.removeRequest(req);
                        observer.complete();
                    }
                );
            // remove request from queue when cancelled
            return () => {
                // logger.debug('remove request from queue when cancelled', req);
                this.removeRequest(req);
                subscription.unsubscribe();
            };
        });
    }
}
