import { Component, OnInit } from '@angular/core';
import { BookingRm, BookDto } from '../api/models';
import { BookingService } from '../api/services/booking.service';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router'

@Component({
  selector: 'app-my-bookings',
  standalone: false,

  templateUrl: './my-bookings.component.html',
  styleUrl: './my-bookings.component.css'
})
export class MyBookingsComponent implements OnInit {

  bookings!: BookingRm[];

  constructor(private bookingService: BookingService, private authService: AuthService, private router: Router) {

  }

  ngOnInit(): void {

    this.bookingService.listBooking({ email: this.authService.currentUser?.email ?? '' }).
      subscribe(r => this.bookings = r, this.handleError);  
  }

  cancel(booking: BookingRm) {

    const dto: BookDto = {
      flightId: booking.id,
      numberOfSeats: booking.numberOfSeats,
      passengerEmail: booking.passengerEmail
    }

    this.bookingService.cancelBooking({ body: dto }).subscribe(_ => this.bookings = this.bookings.filter(b => b != booking)
      , this.handleError)
  }

  private handleError(err: any) {
    console.log("Response Error, Status:" + err.status);
    console.log("Response Error, Status Text:" + err.statusText);
    console.log(err);

  }


}
