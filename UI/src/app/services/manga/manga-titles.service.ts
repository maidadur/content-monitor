import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { MangaInfo } from '@app/entity/manga/manga-info';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/auth.service';
import { UrlUtils } from '@app/utils/url-utils';

@Injectable({
  providedIn: 'root'
})
export class MangaTitlesService extends BaseGenericService<MangaInfo> {

  protected apiUrl = UrlUtils.replaceUrlDomain(environment.mangaHost) + '/api/manga';

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
