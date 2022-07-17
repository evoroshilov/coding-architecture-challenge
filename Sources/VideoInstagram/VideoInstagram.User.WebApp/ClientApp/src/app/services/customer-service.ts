import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ICustomer } from "../models/icustomer";
import { SettingsService } from "./settings-service";

@Injectable()
export class CustomerService {

  constructor(
    private http: HttpClient,
    private settings: SettingsService,
  ) { }

  getCustomers(): Observable<ICustomer[]> {
    const endpointUrl = this.settings.getApiUrl(`customer`);
    return this.http.get<ICustomer[]>(endpointUrl);
  }
}