import { Component, OnInit } from '@angular/core';
import { PassengerService } from '../api/services/passenger.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth/auth.service';
import { Router, ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-register-passenger',
  standalone: false,
  templateUrl: './register-passenger.component.html',
  styleUrl: './register-passenger.component.css'
})
export class RegisterPassengerComponent implements OnInit {

  //ASKAI about reguestedurl

  form!: FormGroup;
  requestedUrl?: string = undefined;

  constructor(private passengerService: PassengerService, private formBuilder: FormBuilder,
    private authService: AuthService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {

    this.activatedRoute.params.subscribe(p => this.requestedUrl = p['requestedUrl'])


    this.form = this.formBuilder.group({
      email: [''],
      firstName: [''],
      lastName: [''],
      isFemale: [true]
      //email: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
      //firstName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(35)])],
      //lastName: ['', Validators.compose([Validators.required, Validators.minLength(2), Validators.maxLength(35)])],
      //isFemale: [true, Validators.required]
    });


  }

  //activates when we click out of the email input
  checkPassenger() {
    const param = { email: this.form.get("email")?.value }

    this.passengerService.findPassenger(param).subscribe(this.Login, e => {
      if(e.status != 404)
        console.error(e)
    })
  }

  register() {
    if (this.form.invalid) { 
      return;
    }
    console.log("Form values:", this.form.value)
    this.passengerService.registerPassenger({ body: this.form.value })
      .subscribe(this.Login, console.error);
  }

  private Login = () => {
    this.authService.loginUser({ email: this.form.get('email')?.value })
    this.router.navigate([this.requestedUrl?? '/search-flights'])
  }
}



