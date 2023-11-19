import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { ContentInfo } from '@app/entity/content/content-info';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/auth.service';
import { UrlUtils } from '@app/utils/url-utils';

@Injectable({
  providedIn: 'root'
})
export class ContentInfoService extends BaseGenericService<ContentInfo> {

  protected apiUrl = UrlUtils.replaceUrlDomain(environment.contentHost) + '/api/contentinfo';

  constructor(protected http: HttpClient, protected auth: AuthService) {
    super(http, auth);
  }

  loadContentInfo(model: ContentInfo): Observable<ContentInfo> {
    const url = `${this.apiUrl}/LoadContentInfo`;
    return this.http.post<ContentInfo>(url, model, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }
}
