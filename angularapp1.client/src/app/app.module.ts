import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { authGuard } from '../app/auth/auth.guard'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SearchFlightsComponent } from './search-flights/search-flights.component';
import { DummyComponentComponent } from './dummy-component/dummy-component.component';
import { BookFlightComponent } from './book-flight/book-flight.component';
import { RegisterPassengerComponent } from './register-passenger/register-passenger.component';
import { MyBookingsComponent } from './my-bookings/my-bookings.component';

@NgModule({
  declarations: [
    AppComponent,
    SearchFlightsComponent,
    DummyComponentComponent,
    BookFlightComponent,
    RegisterPassengerComponent,
    MyBookingsComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, FormsModule, ReactiveFormsModule,   
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
