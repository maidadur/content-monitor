import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { MangaInfo } from '@app/entity/manga/manga-info';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MangaSourcesService extends BaseGenericService<MangaInfo> {

  protected apiUrl = 'https://localhost:5001/api/manga';

  constructor(http: HttpClient) {
    super(http);
  }
}
