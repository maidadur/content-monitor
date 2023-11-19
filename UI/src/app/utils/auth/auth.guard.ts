import { Injectable } from '@angular/core';


import { AuthService } from '../../services/auth/auth.service';

@Injectable({
	providedIn: 'root',
})
export class AuthGuard  {
	constructor(private _authService: AuthService) { }

	public canActivate(): boolean {
		if (this._authService.isAuthenticated()) {
			return true;
		}
		this._authService.redirectToLogin();
		return false;
	}
}
