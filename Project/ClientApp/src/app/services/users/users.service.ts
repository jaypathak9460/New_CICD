import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, throwError } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) { }

  GetAllUserList(): Observable<any> {
    return new Observable(observer => {
      // Make API call asynchronously
      this.http.get<any>('api/Users/').subscribe(
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

  deleteUserData(id: any) {
    return this.http.delete(`api/Users/${id}`);
  }
  

  GetUserDetails(id: any): Observable<any> {
    return new Observable(observer => {
      // Make API call asynchronously
      this.http.get<any>('api/Users/' + id).subscribe(
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



  saveUserData(value: any): Observable<any> {
    if (value.Id) {
      return this.http.put(`api/Users/${value.Id}`, value);
    }
    else {
      return this.http.post<any>('api/Users/', value)
        .pipe(
          catchError(error => {
            console.error('Error occurred:', error);
            return throwError(error);
          })
        );
    }
  }


}
