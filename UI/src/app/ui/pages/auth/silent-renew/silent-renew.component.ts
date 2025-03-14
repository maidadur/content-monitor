import { Component, OnInit } from '@angular/core';
import { AuthService } from '@app/services/auth/auth.service';

@Component({
    selector: 'app-silent-renew',
    templateUrl: './silent-renew.component.html',
    standalone: false
})
export class SilentRenewComponent implements OnInit {

  constructor(private _auth: AuthService) { }

  ngOnInit(): void {
	  this._auth.silentSignin();
  }

}
