import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { MangaInfo } from '@app/entity/manga/manga-info';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Guid } from 'guid-typescript';

@Injectable({
  providedIn: 'root'
})
export class MangaSourcesService extends BaseGenericService<MangaInfo> {

  protected apiUrl = 'https://localhost:5001/api/manga';

  constructor(http: HttpClient) {
    super(http);
  }

  loadMangaInfo(id: Guid): Observable<MangaInfo> {
    const url = `${this.apiUrl}/LoadMangaInfo`;
    return this.http.post<MangaInfo>(url, { id: id }, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }
}
