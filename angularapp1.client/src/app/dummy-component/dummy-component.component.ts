import { Component } from '@angular/core';

@Component({
  selector: 'app-dummy-component',
  standalone: false,
  
  templateUrl: './dummy-component.component.html',
  styleUrl: './dummy-component.component.css'
})
export class DummyComponentComponent {

  dummyResult: ItemsRM [] = [
    {
      para1: "item1",
      para2: "item2",
      x : { place: "Skopje", time : "idk" }
    },
    {
      para1: "item1",
      para2: "item2",
      x: { place: "Skopje", time: "idk" }
    }
  ]
}

export interface ItemsRM {
  para1: string,
  para2: string,
  x: TimePlace
}

export interface TimePlace {
  place: string;
  time: string;
}
