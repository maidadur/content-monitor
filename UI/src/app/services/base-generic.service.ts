import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { BaseEntity } from '../entity/base-entity';
import { Guid } from 'guid-typescript';
import { BaseService} from './base-http-service.service';

@Injectable({
	providedIn: 'root'
})
export class BaseGenericService<TEntity extends BaseEntity> extends BaseService {

	constructor(protected http: HttpClient) {
		super();
	}

	getAll(params?: any): Observable<TEntity[]> {
		let url = this.apiUrl;
		return this.http.get<TEntity[]>(url, {
			params: params
		}).pipe(
			catchError(this.handleError)
		);
	}

	get(id: string): Observable<TEntity> {
		const url = `${this.apiUrl}/${id}`;
		return this.http.get<TEntity>(url)
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
		return this.http.delete<TEntity>(url)
			.pipe(
				catchError(this.handleError)
			);
	}
}
