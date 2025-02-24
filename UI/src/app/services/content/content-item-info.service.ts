import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { ContentItemInfo } from '@app/entity/content/content-item-info';
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
export class ContentItemInfoService extends BaseGenericService<ContentItemInfo> {

  protected apiUrl = UrlUtils.replaceUrlDomain(environment.contentHost) + '/api/contentitem';

  constructor(protected http: HttpClient, protected auth: AuthService) {
    super(http, auth);
  }
  
  getContentInfoItems(id: string, options?: SelectOptions): Observable<ContentItemInfo[]> {
    const url = `${this.apiUrl}/contentinfo/${id}`;
    return this.http.post<ContentItemInfo[]>(url, options, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }
}
