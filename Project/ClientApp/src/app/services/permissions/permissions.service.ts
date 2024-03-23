import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PermissionsService {

  constructor(private http: HttpClient) { }


  GetAllPermissionList(): Observable<any> {
    return new Observable(observer => {
      // Make API call asynchronously
      this.http.get<any>('api/Permission/').subscribe(
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
}
