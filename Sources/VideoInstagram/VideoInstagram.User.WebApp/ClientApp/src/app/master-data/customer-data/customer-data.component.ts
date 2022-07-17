import { AfterContentInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { ICustomer } from 'src/app/models/icustomer';
import { CustomerService } from 'src/app/services/customer-service';

@Component({
  selector: 'app-customer-data',
  templateUrl: './customer-data.component.html',
  styleUrls: ['./customer-data.component.css'],
  providers: [CustomerService]
})

export class CustomerDataComponent implements AfterContentInit, OnDestroy {
  subscription: Subscription = new Subscription();
  customers: ICustomer[] = []
  customersErrorMessage = ''

  constructor(   
    private customerService: CustomerService,    
  ) { }

  ngAfterContentInit(): void {
    this.loadAreas();   
  }
  ngOnDestroy(): void {    
    this.subscription.unsubscribe();
  }

  private loadAreas(): void {
    const loader = this.customerService.getCustomers().subscribe((customers: ICustomer[]) => {
      this.customers =  customers = customers;      
    }, _ => this.customersErrorMessage = "Error form server");

    this.subscription.add(loader);
  }
}
