import { Component, OnInit } from '@angular/core';
import { FlightService } from '../api/services/flight.service';
import { FlightRm } from '../api/models/flight-rm';
import { FormBuilder, FormGroup } from '@angular/forms'

//because SearchFlightsComponent is a component, we can use the field searchResult in the html
@Component({
  selector: 'app-search-flights',
  standalone: false,

  templateUrl: './search-flights.component.html',
  styleUrl: './search-flights.component.css'
})
export class SearchFlightsComponent implements OnInit {

  searchResult: FlightRm[] = []
  searchForm !: FormGroup

  ngOnInit(): void {

    this.searchForm = this.formBuilder.group({
      from: [''],
      destination: [''],
      fromDate: [''],
      toDate: [''],
      numberOfPassengers: [1]
    })
    this.search();
  } 

  //dependency injection for the FlightService
  constructor(private flightService: FlightService, private formBuilder: FormBuilder) {

  }


  search() {
    this.flightService.searchFlight(this.searchForm.value).subscribe(response => this.searchResult = response, this.handleError)
  }

  private handleError(err: any) {
    console.log("Response Error. Status: ", err.status);
    console.log("Response Error. Status Text: ", err.statusText);
    console.log(err);
  }

}
import { from } from 'rxjs';
