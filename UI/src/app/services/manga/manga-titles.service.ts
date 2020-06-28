import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { MangaInfo } from '@app/entity/manga/manga-info';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Guid } from 'guid-typescript';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MangaTitlesService extends BaseGenericService<MangaInfo> {

  protected apiUrl = environment.mangaUrl + '/manga';

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
