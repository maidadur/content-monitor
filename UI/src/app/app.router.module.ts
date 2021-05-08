import { ModuleWithProviders } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthCallbackComponent } from './ui/pages/auth/auth-callback/auth-callback.component';
import { WorkspaceComponent } from './ui/pages/workspace/workspace.component';
import { workspaceRoutes } from './ui/pages/workspace/workspace.routes';
import { AuthGuard } from './utils/auth/auth.guard';
import { LoginPageComponent } from './ui/pages/auth/login-page/login-page.component';

const appRoutes: Routes = [
	{ path: '', redirectTo: 'workspace', pathMatch: 'full' },
	{
		path: 'workspace',
		component: WorkspaceComponent,
		canActivate: [AuthGuard],
		children: workspaceRoutes,
	},
	{ path: 'auth', component: LoginPageComponent },
	{ path: 'auth-callback', component: AuthCallbackComponent },
];

export const routing: ModuleWithProviders<RouterModule> = RouterModule.forRoot(appRoutes);
