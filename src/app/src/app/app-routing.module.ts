import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AppLayoutComponent } from "./layout/app.layout.component";
import { AuthGuard } from './pages/components/auth/auth.guard';

@NgModule({
  imports: [
    RouterModule.forRoot([
      { path: '', redirectTo: 'auth/login', pathMatch: 'full' },
      { path: 'auth', loadChildren: () => import('./pages/components/auth/auth.module').then(m => m.AuthModule) },
      {
        path: '', component: AppLayoutComponent,
        children: [
          { path: 'main', loadChildren: () => import('./pages/components/main/main.module').then(m => m.MainModule), canActivate: [AuthGuard], },
        ],
      },
    ], { scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled', onSameUrlNavigation: 'reload' })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
