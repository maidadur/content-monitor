import { ModuleWithProviders } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';

import { AuthCallbackComponent } from './ui/pages/auth/auth-callback/auth-callback.component';
import { WorkspaceComponent } from './ui/pages/workspace/workspace.component';
import { workspaceRoutes } from './ui/pages/workspace/workspace.routes';
import { AuthGuard } from './utils/auth/auth.guard';
import { LoginPageComponent } from './ui/pages/auth/login-page/login-page.component';
import { SilentRenewComponent } from './ui/pages/auth/silent-renew/silent-renew.component';

const appRoutes: Routes = [
	{ path: '', redirectTo: 'workspace', pathMatch: 'full',  },
	{
		path: 'workspace',
		component: WorkspaceComponent,
		//canActivate: [AuthGuard],
		canActivate: [MsalGuard],
		children: workspaceRoutes,
	},
	//{ path: 'auth', component: MsalRedirectComponent },
	//{ path: 'auth', component: LoginPageComponent },
	//{ path: 'auth/silent-refresh', component: SilentRenewComponent },
	//{ path: 'auth-callback', component: AuthCallbackComponent },
];

export const routing: ModuleWithProviders<RouterModule> = RouterModule.forRoot(appRoutes, {});
