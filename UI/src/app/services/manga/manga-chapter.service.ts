import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { MangaChapter } from '@app/entity/manga/manga-chapter';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MangaChapterService extends BaseGenericService<MangaChapter> {

  protected apiUrl = 'https://localhost:5001/api/mangachapter';

  constructor(http: HttpClient) {
    super(http);
  }
  
  getMangaChapters(id: string): Observable<MangaChapter[]> {
    const url = `${this.apiUrl}/manga/${id}`;
    return this.http.get<MangaChapter[]>(url)
      .pipe(
        catchError(this.handleError)
      );
  }
}
