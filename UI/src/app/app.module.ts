import { APP_INITIALIZER, NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { DialogWindowComponent } from './ui/controls/dialog-window/dialog-window.component';
import { AuthService } from './services/auth/auth.service';
import { WorkspaceModule } from './ui/pages/workspace/workspace.module';
import { routing } from './app.router.module';
import { LoginComponent } from './ui/controls/auth/login/login.component';
import { AuthCallbackComponent } from './ui/pages/auth/auth-callback/auth-callback.component';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginPageComponent } from './ui/pages/auth/login-page/login-page.component';
import { SilentRenewComponent } from './ui/pages/auth/silent-renew/silent-renew.component';
import { AuthInterceptor } from './utils/auth/auth.interceptor';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { ServiceWorkerModule } from '@angular/service-worker';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MsalGuard, MsalInterceptor, MsalModule, MsalRedirectComponent } from '@azure/msal-angular';
import { InteractionType, PublicClientApplication } from '@azure/msal-browser';
import { msalConfig, msalModule } from './auth-config';

@NgModule({ declarations: [
        AppComponent,
        LoginComponent,
        AuthCallbackComponent,
        LoginPageComponent,
        SilentRenewComponent,
    ],
    bootstrap: [AppComponent], 
    imports: [
        WorkspaceModule,
        FormsModule,
        BrowserAnimationsModule,
        MatIconModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatDialogModule,
        routing,
        ServiceWorkerModule.register("ngsw-worker.js", { enabled: true }),
        msalModule], providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: MsalInterceptor,
            multi: true,
        },
        MsalGuard,
        provideHttpClient(withInterceptorsFromDi()),
    ] })
export class AppModule {}

export function authProviderFactory(provider: AuthService) {
	return () => provider.loadUser();
}
