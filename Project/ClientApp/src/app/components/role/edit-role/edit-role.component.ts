import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RolesService } from '../../../services/roles/roles.service';
import { Role, RoleDTO } from '../role-interface';
import Swal from 'sweetalert2';
import { PermissionsService } from '../../../services/permissions/permissions.service';



@Component({
  selector: 'app-edit-role',
  templateUrl: './edit-role.component.html',
  styleUrls: ['./edit-role.component.css']
})



export class EditRoleComponent implements OnInit {
  PermissionList: any;
  RoleDTO: RoleDTO = {
    id: null,
    roleName: '',
    isDelete: false,
    permissionList: null
  };
  roleId: String = "";
  constructor(private route: ActivatedRoute, private service: RolesService, private permissionService: PermissionsService, private router: Router) { }

  ngOnInit() {

    this.route.queryParams.subscribe(params => {
      this.roleId = params['RoleId'];
    });
    if (this.roleId) {
      this.getRoleDetails();
    } else {
      //add code for Add method
    }
    this.getPermissions();
  }
  getRoleDetails() {
    this.service.GetRoleDetails(this.roleId).subscribe((data: any) => {
      this.RoleDTO = data;
    });
  }
  getPermissions() {
    this.permissionService.GetAllPermissionList().subscribe(data => {
     this.PermissionList = data;
    }, error => {
      Swal.fire('Error', 'Error While fetching Data!', 'error');
    })
      ;
  }

  saveRoleDetails() {

    var newDTO: Role = {
      Id: undefined,
      RoleName: this.RoleDTO.roleName,
      IsDelete: this.RoleDTO.isDelete,
      PermissionList: this.RoleDTO.permissionList,
      
    }

    if (this.RoleDTO && this.RoleDTO.id) {
      newDTO.Id = this.RoleDTO.id
    }

    this.service.saveRoleData(newDTO).subscribe(data => {
      Swal.fire('saved', 'Saved Successfully!', 'success');
      this.RoleDTO = data as RoleDTO;
      this.router.navigate(['/edit-role'], { queryParams: { RoleId: data.id } });
    },
      error => {
        Swal.fire('Error', 'Error While Saving Data!', 'error');

        console.log(error);
      })

  }

}
