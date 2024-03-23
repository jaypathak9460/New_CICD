import { Component } from '@angular/core';
import { RoleList, User, UserDTO } from '../user-interface';
import { ActivatedRoute, Router } from '@angular/router';
import { UsersService } from '../../../services/users/users.service';
import { MatDatepickerInputEvent, MatDatepicker } from '@angular/material/datepicker';
import { RolesService } from '../../../services/roles/roles.service';
import Swal from 'sweetalert2';



@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css'],

})
export class EditUserComponent {

  RolesList = [] as RoleList[];


  selectedDate: any ;

  dateChanged(event: MatDatepickerInputEvent<Date>) {
    this.selectedDate = event.value;
  }

  UsersDTO: UserDTO = {
    id: null,
    firstName: '',
    lastName: '',
    dateOfBirth: null,
    isDelete: false,
    roleList:[]
  };
  UsersId: String = "";
  constructor(private route: ActivatedRoute, private service: UsersService, private router: Router,private roleService: RolesService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.UsersId = params['Id'];
    });
    this.getRoleList();
    if (this.UsersId) {
      this.getUsersDetails();
     
    } else {
      //add code for Add method
    }
   
  }
  getUsersDetails() {
    this.service.GetUserDetails(this.UsersId).subscribe((data: any) => {
      this.UsersDTO = data as UserDTO;
    });
  }
  getRoleList() {
    this.roleService.GetAllRolesList().subscribe(data => { this.RolesList = data as RoleList[]; });
  }

  saveUserDetails() {

    var newDTO: User = {
      Id: undefined,
      FirstName: this.UsersDTO.firstName,
      LastName: this.UsersDTO.lastName,
      DateOfBirth: this.UsersDTO.dateOfBirth,
      IsDelete: this.UsersDTO.isDelete,
      RoleList: this.UsersDTO.roleList
    }
    if (this.UsersDTO && this.UsersDTO.id) {
      newDTO.Id = this.UsersDTO.id
    }

    this.service.saveUserData(newDTO).subscribe(data => {
      Swal.fire('Saved', 'Saved Successfully!', 'success');
      this.UsersDTO = data as UserDTO;
      this.router.navigate(['/edit-user'], { queryParams: { Id: data.id } });
    },
      error => {
        Swal.fire('Error', 'Error While Saving Data!', 'error');
        console.log(error);
      })

  }



}
