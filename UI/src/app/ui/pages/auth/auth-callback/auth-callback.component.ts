import { Component, OnInit } from '@angular/core';
import { AuthService } from '@app/services/auth/auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
	selector: 'app-auth-callback',
	templateUrl: './auth-callback.component.html',
	styleUrls: ['./auth-callback.component.less'],
})
export class AuthCallbackComponent implements OnInit {
	error: boolean;

	constructor(
		private _authService: AuthService,
		private _router: Router
	) {}

	async ngOnInit() {
		await this._authService.completeAuthentication();
		this._router.navigate(['/']);
	}
}
