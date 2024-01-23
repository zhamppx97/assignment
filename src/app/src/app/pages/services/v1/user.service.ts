import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map as rxjsMap, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { User } from '../../api/user';

@Injectable()
export class UserServiceV1 {

    constructor(
        private naviagtor: Router,
        private http: HttpClient,
    ) {

    }

    select(): Observable<any> {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'Api-Version': '1',
            'RequestId': crypto.randomUUID(),
        });
        const options = { headers: headers };
        const uri = `/v${1}${environment.apiEndpoint.Users.Select}`;
        return this.http.get<any>(`${environment.apiService.host}${uri}`, options).pipe(
            rxjsMap(res => {
                return res || [];
            }),
            catchError((err) => {
                return of(err);
            })
        );
    }

    insert(user: User): Observable<any> {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'Api-Version': '1',
            'RequestId': crypto.randomUUID(),
        });
        const options = { headers: headers };
        const uri = `/v${1}${environment.apiEndpoint.Users.Insert}`;
        return this.http.post<any>(`${environment.apiService.host}${uri}`, user, options).pipe(
            rxjsMap(res => {
                return res || [];
            }),
            catchError((err) => {
                return of(err);
            })
        );
    }

    update(user: User): Observable<any> {
        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'Api-Version': '1',
            'RequestId': crypto.randomUUID(),
        });
        const options = { headers: headers };
        const uri = `/v${1}${environment.apiEndpoint.Users.Update}`;
        return this.http.put<any>(`${environment.apiService.host}${uri}`, user, options).pipe(
            rxjsMap(res => {
                return res || [];
            }),
            catchError((err) => {
                return of(err);
            })
        );
    }

    delete(user: User): Observable<any> {
        const param = new HttpParams().set("UserId", user.userId || "");
        const headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'Api-Version': '1',
            'RequestId': crypto.randomUUID(),
        });
        const options = { headers: headers, params: param };
        const uri = `/v${1}${environment.apiEndpoint.Users.Delete}`;
        return this.http.delete<any>(`${environment.apiService.host}${uri}`, options).pipe(
            rxjsMap(res => {
                return res || [];
            }),
            catchError((err) => {
                return of(err);
            })
        );
    }
}
