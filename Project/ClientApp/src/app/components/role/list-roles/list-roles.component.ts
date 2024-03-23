import { Component, OnInit,OnDestroy } from '@angular/core';
import { RolesService } from '../../../services/roles/roles.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-list-roles',
  templateUrl: './list-roles.component.html',
  styleUrls: ['./list-roles.component.css']
})
export class ListRolesComponent implements OnInit, OnDestroy {
  rolesList: any = [];
  constructor(private service: RolesService, private router: Router) { }


  ngOnInit(): void {

    this.getRolesList();
  }
  ngOnDestroy(): void {
    //  this.dtTrigger.unsubscribe();
  }
  
  getRolesList() {
    
    this.service.GetAllRolesList().subscribe((data: any) => {

      this.rolesList = data;
    });
  }
  gotoEdit(id:any) {
    this.router.navigate(['/edit-role'], { queryParams: { RoleId: id } });
  }
  createANewRole() {
    this.router.navigate(['/edit-role']);
  }
  deleteRole(id: any) {

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
        this.service.deleteRoleData(id).subscribe((data: any) => {
          this.getRolesList();

        }); 
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        // Action cancelled
      }
    });
   
  }


}
