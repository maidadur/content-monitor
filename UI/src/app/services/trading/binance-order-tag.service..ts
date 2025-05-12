import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/auth.service';
import { UrlUtils } from '@app/utils/url-utils';
import { BinanceOrderTag } from '@app/entity/trading/binance-order-tag';

@Injectable({
  providedIn: 'root'
})
export class BinanceOrderTagService extends BaseGenericService<BinanceOrderTag> {

  protected apiUrl = UrlUtils.replaceUrlDomain(environment.binanceHost) + '/order-tags/api';

  constructor(protected http: HttpClient, protected auth: AuthService) {
    super(http, auth);
  }

  loadTagsByOrderId(orderId: string): Observable<BinanceOrderTag[]> {
    const url = `${this.apiUrl}/list/${orderId}`;
    return this.http.get<BinanceOrderTag[]>(url)
      .pipe(
        catchError(this.handleError)
      );
  }
}
