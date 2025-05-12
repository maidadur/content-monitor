import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/auth.service';
import { UrlUtils } from '@app/utils/url-utils';
import { BinanceOrderEmotion } from '@app/entity/trading/binance-order-emotion';

@Injectable({
  providedIn: 'root'
})
export class BinanceOrderEmotionService extends BaseGenericService<BinanceOrderEmotion> {

  protected apiUrl = UrlUtils.replaceUrlDomain(environment.binanceHost) + '/order-emotions/api';

  constructor(protected http: HttpClient, protected auth: AuthService) {
    super(http, auth);
  }

  loadEmotionsByOrderId(orderId: string): Observable<BinanceOrderEmotion[]> {
    const url = `${this.apiUrl}/list/${orderId}`;
    return this.http.get<BinanceOrderEmotion[]>(url)
      .pipe(
        catchError(this.handleError)
      );
  }
}
