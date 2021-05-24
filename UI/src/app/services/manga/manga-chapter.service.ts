import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { MangaChapter } from '@app/entity/manga/manga-chapter';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class MangaChapterService extends BaseGenericService<MangaChapter> {

  protected apiUrl = environment.mangaHost + '/api/mangachapter';

  constructor(protected http: HttpClient, protected auth: AuthService) {
    super(http, auth);
  }
  
  getMangaChapters(id: string): Observable<MangaChapter[]> {
    const url = `${this.apiUrl}/manga/${id}`;
    return this.http.get<MangaChapter[]>(url, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }
}
