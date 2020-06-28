import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MangaSource } from '@app/entity/manga/manga-source';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MangaSourcesService extends BaseGenericService<MangaSource> {

  protected apiUrl = environment.mangaUrl + '/mangasource';

  constructor(http: HttpClient) {
    super(http);
  }
}
