import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { BaseEntity } from '../entity/base-entity';
import { Guid } from 'guid-typescript';
import { BaseService} from './base-http-service.service';
import { AuthService } from './auth/auth.service';

@Injectable({
	providedIn: 'root'
})
export class BaseGenericService<TEntity extends BaseEntity> extends BaseService {

	constructor(protected http: HttpClient, protected auth: AuthService) {
		super();
		this.httpOptions.headers = this.httpOptions.headers.append("Authorization", this.auth.authorizationHeaderValue);
	}

	getAll(params?: any): Observable<TEntity[]> {
		let url = this.apiUrl;
		return this.http.get<TEntity[]>(url, Object.assign(this.httpOptions, {
			params: params
		})).pipe(
			catchError(this.handleError)
		);
	}

	get(id: string): Observable<TEntity> {
		const url = `${this.apiUrl}/${id}`;
		return this.http.get<TEntity>(url, this.httpOptions)
			.pipe(
				catchError(this.handleError)
			);
	}

	add(entity: TEntity): Observable<any> {
		return this.http.post<TEntity>(this.apiUrl, entity, this.httpOptions)
			.pipe(
				catchError(this.handleError)
			);
	}

	update(entity: TEntity): Observable<any> {
		return this.http.put<TEntity>(this.apiUrl, entity, this.httpOptions);
	}

	delete(id: Guid): Observable<any> {
		const url = `${this.apiUrl}/${id}`;
		return this.http.delete<TEntity>(url, this.httpOptions)
			.pipe(
				catchError(this.handleError)
			);
	}
}
