import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class httpInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Add your base URL here
    const baseUrl = 'https://localhost:5000';
    const header = { "Accept": "*/*" };

    // Clone the request and set the base URL
    const apiReq = req.clone({ url: `${baseUrl}/${req.url}` }, );


    // Pass the cloned request instead of the original request to the next handler
    return next.handle(apiReq);
  }
}
