import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { Role, RoleDTO } from '../../components/role/role-interface';
import { catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class RolesService {


  constructor(private http: HttpClient) { }

  deleteRoleData(id: any) {
    return this.http.delete(`api/Roles/${id}`);
  }

  GetAllRolesList(): Observable<any> {
    return new Observable(observer => {
      // Make API call asynchronously
      this.http.get<any>('api/Roles/').subscribe(
        data => {

          observer.next(data); // Emit data
          observer.complete(); // Complete the observable
        },
        error => {
          observer.error(error); // Emit error
        }
      );
    });
  }


  GetRoleDetails(id: any): Observable<any> {
    return new Observable(observer => {
      // Make API call asynchronously
      this.http.get<any>('api/Roles/' + id).subscribe(
        data => {

          observer.next(data); // Emit data
          observer.complete(); // Complete the observable
        },
        error => {
          observer.error(error); // Emit error
        }
      );
    });
  }



  saveRoleData(value: Role): Observable<any> {
    if (value.Id) {
      return this.http.put(`api/Roles/${value.Id}`, value);
    }
    else {
      return this.http.post<any>('api/Roles/', value)
        .pipe(
          catchError(error => {
            console.error('Error occurred:', error);
            return throwError(error);
          })
        );
    }
  }

}
