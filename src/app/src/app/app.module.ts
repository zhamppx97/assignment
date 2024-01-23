import { NgModule } from '@angular/core';
import { HashLocationStrategy, LocationStrategy, CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { LoaderComponent } from './pages/components/loading/loader/loader.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthGuard } from './pages/components/auth/auth.guard';
import { AppLayoutModule } from './layout/app.layout.module';
import { NotfoundComponent } from './pages/components/notfound/notfound.component';
import { LoaderService } from './pages/components/loading/loader.service';
import { CookieService } from 'ngx-cookie-service';
import { UserServiceV1 } from './pages/services/v1/user.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoaderInterceptorService } from './pages/components/loading/loader-interceptor.service';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

@NgModule({
    declarations: [
        AppComponent,
        LoaderComponent,
        NotfoundComponent
    ],
    imports: [
        AppRoutingModule,
        AppLayoutModule,
        CommonModule,
        BrowserAnimationsModule,
        NgxSpinnerModule,
    ],
    providers: [
        LoaderService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: LoaderInterceptorService,
            multi: true
        },
        AuthGuard,
        {
            provide: LocationStrategy,
            useClass: HashLocationStrategy
        },
        CookieService,
        UserServiceV1,
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
