import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(
    private http: HttpClient
  ) { }

  isAuthTokenAvailableAsync(tenantId: string) {
    var url = "https://localhost:7248" + `/api/auth/token?tenantId=${tenantId}`
    //var url = environment.docsieBaseUrl + `/api/auth/token?tenantId=${tenantId}`

    return this.http.get<any>(url);
  }

  saveAuthTokenAsync(tenantId: string, token: string) {
    var url = "https://localhost:7248" + `/api/auth/token`
    //var url = environment.docsieBaseUrl + `/api/auth/token`
    let body = {tenantId: tenantId, token: token };
    return this.http.post<any>(url, body);
  }
}
