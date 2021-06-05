import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MangaSource } from '@app/entity/manga/manga-source';
import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/auth.service';
import { UrlUtils } from '@app/utils/url-utils';

@Injectable({
  providedIn: 'root'
})
export class MangaSourcesService extends BaseGenericService<MangaSource> {

  protected apiUrl = UrlUtils.replaceUrlDomain(environment.mangaHost) + '/api/mangasource';

  constructor(protected http: HttpClient, protected auth: AuthService) {
    super(http, auth);
  }
}
