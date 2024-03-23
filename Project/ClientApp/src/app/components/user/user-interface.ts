export interface User {
  Id: any,
  FirstName: string
  LastName: string
  DateOfBirth: any
  IsDelete: boolean
  RoleList: any

}
export interface UserDTO {
  id: any,
  firstName: string
  lastName: string
  dateOfBirth: any
  isDelete: boolean
  roleList: any

}export interface ListUserDTO {
  id: any,
  firstName: string
  lastName: string
  dateOfBirth: any
  isDelete: boolean
  roles: string

}

export interface RoleList {
  id: any
  roleName: string
}
