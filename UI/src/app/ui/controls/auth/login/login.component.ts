import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '@app/services/auth/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    standalone: false
})
export class LoginComponent implements OnInit {

	public email: string;
	public password: string;
	public returnUrl: string;

	constructor(
		private _auth: AuthService,
		private _route: ActivatedRoute,
		) {}

	ngOnInit(): void {
		this.returnUrl = this._route.snapshot.queryParamMap.get('ReturnUrl');
		if (!this.returnUrl) {
			this._auth.redirectToLogin();
			return;
		}
	}

	public login(): void {
		this._auth.login(this.email, this.password, this.returnUrl)
			.subscribe((response: any) => {
				const data = response;

				if (data && data.isOk) {
					window.location = data.redirectUrl;
				}
			},
			(error) => {

			}
			);
	}

	public keyUp(event): void {
		 if (event.keyCode === 13) {
			event.preventDefault();
			this.login();
		  }
	}
}
