import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

import { Subject } from 'rxjs';
import { UsersService } from '../../../services/users/users.service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-list-users',
  templateUrl: './list-users.component.html',
  styleUrls: ['./list-users.component.css']
})
export class ListUsersComponent implements OnInit, OnDestroy {
  //dtTrigger: Subject<any> = new Subject<any>();
  allUsers: any = [];
  constructor(private service: UsersService, private router: Router) { }

  ngOnInit(): void {

    this.users();
  }
  ngOnDestroy(): void {
    //  this.dtTrigger.unsubscribe();
  }

  users(): void {
    this.service
      .GetAllUserList()
      .subscribe((response: any) => {

        this.allUsers = response;
        // initiate our data table
        //this.dtTrigger.next(10);
      });
  }
  async createNewUser() {
    await this.router.navigate(['/edit-user']);
    this.users();
  }

  async gotoUser(id: any) {
    await this.router.navigate(['/edit-user'], { queryParams: { Id: id } });
    this.users();
  }

  deleteUser(id: any) {

    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to revert this!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, cancel!',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        // Action confirmed
        this.service.deleteUserData(id).subscribe(data => {
          this.users();
        }, error => {
          console.error(error);

        })
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        // Action cancelled
      }
    });


   
  }


}
