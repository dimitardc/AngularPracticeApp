import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router'
import { FlightService } from '../api/services/flight.service';
import { FlightRm } from '../api/models/flight-rm';
import { AuthService } from '../auth/auth.service'
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { BookDto } from '../api/models';

@Component({
  selector: 'app-book-flight',
  standalone: false,

  templateUrl: './book-flight.component.html',
  styleUrl: './book-flight.component.css'
})
export class BookFlightComponent implements OnInit {

  flightId: string = "not loaded";
  flight: FlightRm = {}

  form! :FormGroup

  constructor(private route: ActivatedRoute, private router: Router,
    private flightService: FlightService, private authService: AuthService, private fb: FormBuilder) {

  }

  ngOnInit(): void {

    this.form = this.fb.group({
      number: [1, Validators.compose([Validators.required, Validators.min(1), Validators.max(254)])]
    })

    this.route.paramMap.subscribe(p => this.findFlight(p.get("flightId"))); 
  }

  //we find and save the flightId here
  private findFlight = (flightId: string | null) => {
    this.flightId = flightId ?? "not passed";

    //we use the flightId to find the flightRm obj with the flightSertive id and save it
    this.flightService.findFlight({ id: this.flightId }).subscribe(flight => this.flight = flight, this.handleError); 
  }

  book() {

    if (this.form.invalid)
      return;

    console.log(`Booking ${this.form.get('number')?.value} passengers for the flight: ${this.flight.id}`)

    var booking: BookDto = {
      flightId: this.flight.id,
      passengerEmail: this.authService.currentUser?.email,
      numberOfSeats: this.form.get('number')?.value
    }

    this.flightService.bookFlight({ body: booking }).subscribe(_ => this.router.navigate(['/my-booking']), this.handleError);
  }

  private handleError = (err: any) => {
    if (err.status == 404) {
      alert("Flight not found!")
      this.router.navigate(['/search-flights'])
    }
    if (err.status == 409) {
      console.log("err:" + err);
      alert(JSON.parse(err.error).message);
    }
    console.log("Response Error. Status: ", err.status);
    console.log("Response Error. Status Text: ", err.statusText);
    console.log(err);
  }

  get number() {
    return this.form.controls['number'];
  }

}

