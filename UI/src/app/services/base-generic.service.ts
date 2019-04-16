import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { BaseEntity } from '../entity/base-entity';

@Injectable({
  providedIn: 'root'
})
export class BaseGenericService<TEntity extends BaseEntity> {

  protected apiUrl: string;

  protected httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(protected http: HttpClient) {
  }

  getAll(): Observable<TEntity[]> {
    return this.http.get<TEntity[]>(this.apiUrl)
      .pipe(
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

  delete(id): Observable<any> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<TEntity>(url)
      .pipe(
        catchError(this.handleError)
      );
  }

  protected handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };
}
