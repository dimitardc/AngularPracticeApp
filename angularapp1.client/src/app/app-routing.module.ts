import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchFlightsComponent } from './search-flights/search-flights.component';
import { DummyComponentComponent } from './dummy-component/dummy-component.component';
import { BookFlightComponent } from './book-flight/book-flight.component';
import { RegisterPassengerComponent } from './register-passenger/register-passenger.component';
import { MyBookingsComponent } from './my-bookings/my-bookings.component';
import { authGuard } from '../app/auth/auth.guard'


const routes: Routes = [
  //this is the default route because path : '' 
  { path: '', component: SearchFlightsComponent, pathMatch: 'full' },
  { path: "dummy", component: DummyComponentComponent },
  { path: "search-flights", component: SearchFlightsComponent },
  { path: "book-flight/:flightId", component: BookFlightComponent, canActivate: [authGuard] },
  { path: "register-passenger", component: RegisterPassengerComponent },
  { path: "my-booking", component: MyBookingsComponent, canActivate: [authGuard] }


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
