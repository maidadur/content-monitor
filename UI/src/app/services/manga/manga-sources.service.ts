import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { MangaInfo } from '@app/entity/manga/manga-info';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MangaSourcesService extends BaseService<MangaInfo> {
  constructor(http: HttpClient) {
    super(http, MangaInfo);
  }
}
