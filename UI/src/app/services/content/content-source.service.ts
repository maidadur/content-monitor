import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ContentSource } from '@app/entity/content/content-source';
import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/auth.service';
import { UrlUtils } from '@app/utils/url-utils';

@Injectable({
  providedIn: 'root'
})
export class ContentSourcesService extends BaseGenericService<ContentSource> {

  protected apiUrl = UrlUtils.replaceUrlDomain(environment.contentHost) + '/api/contentsource';

  constructor(protected http: HttpClient, protected auth: AuthService) {
    super(http, auth);
  }
}
