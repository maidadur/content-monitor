import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { catchError } from "rxjs/operators";
import { UserManager, UserManagerSettings, User } from "oidc-client";
import { BehaviorSubject } from "rxjs";
import { BaseService } from "../base-http-service.service";
import { environment } from '../../../environments/environment';

@Injectable({
	providedIn: "root",
})
export class AuthService extends BaseService {
	private _authNavStatusSource = new BehaviorSubject<boolean>(false);
	authNavStatus$ = this._authNavStatusSource.asObservable();

	private manager = new UserManager(getClientSettings());
	private user: User | null;

	constructor(
		private http: HttpClient
	) {
		super();
	}

	public loadUser(): Promise<User> {
		return new Promise((resolve) => {
			this.manager.getUser().then((user) => {
				this.user = user;
				this._authNavStatusSource.next(this.isAuthenticated());
				resolve(user);
			});
		});
	}

	public redirectToLogin() {
		return this.manager.signinRedirect();
	}

	public login(email: string, password: string, returnUrl: string) {
		return this.http.post(environment.authHost + '/api/auth/login', {
			email: email,
			password: password,
			returnUrl: returnUrl
		}, {
			withCredentials: true
		});
	}

	public async completeAuthentication() {
		this.user = await this.manager.signinRedirectCallback();
		this._authNavStatusSource.next(this.isAuthenticated());
	}

	public isAuthenticated(): boolean {
		return this.user != null && !this.user.expired;
	}

	get authorizationHeaderValue(): string {
		return this.user && `${this.user.token_type} ${this.user.access_token}`;
	}

	get name(): string {
		return this.user != null ? this.user.profile.name : "";
	}

	public async signout() {
		await this.manager.signoutRedirect();
	}

	public silentSignin() {
		this.manager.signinSilentCallback()
			.catch((err) => {
            	console.log(err);
       		});
	}
}

export function getClientSettings(): UserManagerSettings {
	const appHost = `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}`;
	return {
		authority: environment.authHost,
		client_id: "angular_spa",
		redirect_uri: `${appHost}/auth-callback`,
		post_logout_redirect_uri: appHost,
		response_type: 'code',
		scope: "openid profile email api",
		filterProtocolClaims: true,
		loadUserInfo: true,
		automaticSilentRenew: true,
		silent_redirect_uri: `${appHost}/auth/silent-refresh`,
	};
}
