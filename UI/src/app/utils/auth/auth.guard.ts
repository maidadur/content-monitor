import { Injectable } from '@angular/core';
import {
	Router,
	CanActivate,
	ActivatedRouteSnapshot,
	RouterStateSnapshot,
} from '@angular/router';

import { AuthService } from '../../services/auth/auth.service';

@Injectable({
	providedIn: 'root',
})
export class AuthGuard implements CanActivate {
	constructor(private _authService: AuthService) {}

	public canActivate(): boolean {
		if (this._authService.isAuthenticated()) {
			return true;
		}
		this._authService.redirectToLogin();
		return false;
	}
}
