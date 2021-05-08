import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { MangaInfo } from '@app/entity/manga/manga-info';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Guid } from 'guid-typescript';
import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class MangaTitlesService extends BaseGenericService<MangaInfo> {

  protected apiUrl = environment.mangaUrl + '/manga';

  constructor(protected http: HttpClient, protected auth: AuthService) {
    super(http, auth);
  }

  loadMangaInfo(model: MangaInfo): Observable<MangaInfo> {
    const url = `${this.apiUrl}/LoadMangaInfo`;
    return this.http.post<MangaInfo>(url, model, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }
}
