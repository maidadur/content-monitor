import { Injectable } from '@angular/core';
import {
	HttpRequest,
	HttpHandler,
	HttpEvent,
	HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '@app/services/auth/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
	constructor(private _auth: AuthService) { }
	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		const token = this._auth.authorizationHeaderValue;
		if (token) {
			request = request.clone({
				setHeaders: {
					"Authorization": token,
				}
			});
		}
		return next.handle(request);
	}
}