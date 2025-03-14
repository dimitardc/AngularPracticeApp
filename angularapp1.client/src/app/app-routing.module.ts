import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SearchFlightsComponent } from './search-flights/search-flights.component';
import { DummyComponentComponent } from './dummy-component/dummy-component.component';

const routes: Routes = [
  //this is the default route because path : '' 
  { path: '', component: SearchFlightsComponent, pathMatch: 'full' },
  //{ path: "", component: DummyComponentComponent, pathMatch: 'full' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
