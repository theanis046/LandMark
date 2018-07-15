import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpRequest, HttpHandler,HttpResponse, HttpEvent, HttpInterceptor , HttpErrorResponse} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
 
@Injectable()
export class RequestInterceptor implements HttpInterceptor {
    constructor(private router : Router){}
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        //Add JWT token to the request 
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.token) {
            request = request.clone({
                setHeaders: { Authorization: `Bearer ${currentUser.token}` }
            });
        }
        
        //Handling Errors from http request.
        return next.handle(request).do((event: HttpEvent<any>) => {
            if (event instanceof HttpResponse) {}
          }, (err: any) => {
            if (err instanceof HttpErrorResponse) {
              if (err.status === 401) { //Unauthorized requests.  redirect to 
                localStorage.removeItem('currentUser'); //remove token if exists because token is expired.
                this.router.navigate(['/login']);
              }
            }
          });
    }
}