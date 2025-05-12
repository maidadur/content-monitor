import { Injectable } from '@angular/core';
import { BaseGenericService } from '../base-generic.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { AuthService } from '../auth/auth.service';
import { UrlUtils } from '@app/utils/url-utils';
import { BinanceOrder } from '@app/entity/trading/binance-order';

@Injectable({
  providedIn: 'root'
})
export class BinanceOrderService extends BaseGenericService<BinanceOrder> {

  protected apiUrl = UrlUtils.replaceUrlDomain(environment.binanceHost) + '/binance/api';

  constructor(protected http: HttpClient, protected auth: AuthService) {
    super(http, auth);
  }

  loadOrdersByDate(startDate: Date, dueDate: Date): Observable<BinanceOrder[]> {
    const url = `${this.apiUrl}/orders`;
    return this.http.post<BinanceOrder[]>(url, {startDate, dueDate}, this.httpOptions)
      .pipe(
        catchError(this.handleError)
      );
  }
}
