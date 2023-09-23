import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DocsieService {

  constructor(
    private http: HttpClient
  ) { }

  getWorkspacesAsync(tenantId: string) {
    var url = "https://localhost:7248" + `/api/docsie/workspaces?tenantId=${tenantId}`
    //var url = environment.docsieBaseUrl + `/api/docsie/workspaces?tenantId=${tenantId}`

    return this.http.get<any>(url);
  }

  getDeploymentsAsync(tenantId: string, workspaceId: string) {
    var url = "https://localhost:7248" + `/api/docsie/deployments?tenantId=${tenantId}&workspaceId=${workspaceId}`
    //var url = environment.docsieBaseUrl + `/api/docsie/deployments?tenantId=${tenantId}&workspaceId=${workspaceId}`

    return this.http.get<any>(url);
  }
}
