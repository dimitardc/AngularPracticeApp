import { Component } from '@angular/core';

@Component({
  selector: 'app-search-flights',
  standalone: false,
  
  templateUrl: './search-flights.component.html',
  styleUrl: './search-flights.component.css'
})
export class SearchFlightsComponent {

  searchResult: FlightRm[] = [
    {
      airline: "Airline A",
      arrival: { place: "New York (JFK)", time: "2025-02-21T15:30:00Z" },
      departure: { place: "Los Angeles (LAX)", time: "2025-02-21T12:00:00Z" },
      price: "$300",
      remainingNumberOfSeats: 5
    },
    {
      airline: "Airline B",
      arrival: { place: "London (LHR)", time: "2025-02-22T10:00:00Z" },
      departure: { place: "Paris (CDG)", time: "2025-02-22T08:00:00Z" },
      price: "$250",
      remainingNumberOfSeats: 12
    },
    {
      airline: "Airline C",
      arrival: { place: "Tokyo (HND)", time: "2025-02-23T18:45:00Z" },
      departure: { place: "Seoul (ICN)", time: "2025-02-23T16:30:00Z" },
      price: "$400",
      remainingNumberOfSeats: 8
    },
    {
      airline: "Airline D",
      arrival: { place: "Berlin (TXL)", time: "2025-02-24T22:15:00Z" },
      departure: { place: "Madrid (MAD)", time: "2025-02-24T19:45:00Z" },
      price: "$320",
      remainingNumberOfSeats: 3
    }
  ]
}

export interface FlightRm {
  airline: string;
  arrival: TimePlaceRm;
  departure: TimePlaceRm;
  price: string;
  remainingNumberOfSeats: number;
}

export interface TimePlaceRm {
  place: string;
  time: string;
}
