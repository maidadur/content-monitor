import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { MangaChapter } from '@app/entity/manga/manga-chapter';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/auth.service';
import { UrlUtils } from '@app/utils/url-utils';
import { SelectOptions } from '@app/common/select-options';

@Injectable({
  providedIn: 'root'
})
export class MangaChapterService extends BaseGenericService<MangaChapter> {

  protected apiUrl = UrlUtils.replaceUrlDomain(environment.mangaHost) + '/api/mangachapter';

  constructor(protected http: HttpClient, protected auth: AuthService) {
    super(http, auth);
  }
  
  getMangaChapters(id: string, options?: SelectOptions): Observable<MangaChapter[]> {
    const url = `${this.apiUrl}/manga/${id}`;
    return this.http.post<MangaChapter[]>(url, options, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }
}
