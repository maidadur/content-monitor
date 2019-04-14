import { Injectable } from '@angular/core';
import { BaseLookup } from '@app/entity/base-lookup';
import { BaseSchemaGetService } from './base-schema-get.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class LookupService extends BaseSchemaGetService {

  protected apiUrl = 'https://localhost:5001/api/lookup';

  constructor(http: HttpClient) {
    super(http);
  }
}
