import { HttpClient, HttpStatusCode } from '@angular/common/http';
import { Component } from '@angular/core';
import * as microsoftTeams from '@microsoft/teams-js';
import { nanoid } from 'nanoid';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../environments/environment';
import { AuthenticationService } from '../../services/authentication/authentication.service';
import { DocsieService } from '../../services/docsie/docsie.service';

@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrls: ['./configuration.component.css']
})
export class ConfigurationComponent {

  tenantId: string;
  workspaceMetaModel: any = null;
  deploymentMetaModel: any = null;
  formData: any = {};
  isAuthTokenAvailable: boolean = false;
  selectedWorkspaceId: string;
  selectedDeploymentId: string;
  selectedDeploymentScript: string;

  constructor(
    private http: HttpClient,
    private toastr: ToastrService,
    private docsieService: DocsieService,
    private authService: AuthenticationService
  ) { }

  async ngOnInit(): Promise<void> {

    await microsoftTeams.app.initialize();

    var context = await microsoftTeams.app.getContext();
    this.tenantId = context.user?.tenant?.id as string;
    this.isAuthTokenAvailableAsync();


    microsoftTeams.pages.config.registerOnSaveHandler((saveEvent) => {
      const configPromise = microsoftTeams.pages.config.setConfig({
        websiteUrl: environment.docsieBaseUrl + "/login",
        //contentUrl: `${environment.docsieBaseUrl} + /msteams/home?script=${this.selectedDeploymentScript}`,
        contentUrl: environment.docsieBaseUrl + `/msteams/home?script=${this.selectedDeploymentScript}`,
        entityId: nanoid(),
        suggestedDisplayName: "Docsie",
      });
      configPromise.
        then((result: any) => {
          console.log("Success: " + result);
          saveEvent.notifySuccess();
        }).
        catch((error: any) => {
          console.log("Failed: " + error);
          saveEvent.notifyFailure("failure message");
        });

      
    });
  }

  onSubmit() {
    console.log('Form submitted with data:', this.formData);
    this.authService.saveAuthTokenAsync(this.tenantId, this.formData.apiKey).subscribe(res => {
      if (res.status == HttpStatusCode.Ok) {
        this.toastr.success("Token saved successfully!", '', {
          timeOut: 3000,
        });
        this.isAuthTokenAvailable = true;
        this.getWorkspacesAsync();
      } else {
        this.toastr.error(res.errorMessage, 'Internal Server Error', {
          timeOut: 3000,
        });
      }
    });
  }

  isAuthTokenAvailableAsync() {
    this.authService.isAuthTokenAvailableAsync(this.tenantId).subscribe(res => {
      if (res.status == HttpStatusCode.Ok) {
        this.isAuthTokenAvailable = res.isTokenAvailable;
        if (this.isAuthTokenAvailable) {
          this.getWorkspacesAsync();
        }
      } else {
        this.toastr.error(res.errorMessage, 'Internal Server Error', {
          timeOut: 5000,
        });
      }
    });
  }

  getWorkspacesAsync() {
    this.docsieService.getWorkspacesAsync(this.tenantId).subscribe(res => {
      if (res.status == HttpStatusCode.Ok) {
        this.workspaceMetaModel = res.workspaceMetaModel;
        this.selectedWorkspaceId = this.workspaceMetaModel.workspaceModels[0].id;
      } else {
        this.toastr.error(res.errorMessage, 'Internal Server Error', {
          timeOut: 5000,
        });
      }
    });
  }

  onChangeWorkspace(event: any) {
    this.docsieService.getDeploymentsAsync(this.tenantId, event.target.value).subscribe(res => {
      if (res.status == HttpStatusCode.Ok) {
        this.deploymentMetaModel = res.deploymentMetaModel;
        this.selectedDeploymentId = this.deploymentMetaModel.deploymentModels[0].id;

        this.selectedDeploymentScript = this.deploymentMetaModel.deploymentModels[0].script;
        if (this.selectedDeploymentScript) {
          microsoftTeams.pages.config.setValidityState(true);
        }

      } else {
        this.toastr.error(res.errorMessage, 'Internal Server Error', {
          timeOut: 5000,
        });
      }
    });
  }

  onChangeDeployment(event: any) {
    this.selectedDeploymentId = event.target.value;

    this.selectedDeploymentScript = this.deploymentMetaModel.deploymentModels.filter((x: { id: string; }) => x.id === this.selectedDeploymentId);

    if (this.selectedDeploymentScript) {
      microsoftTeams.pages.config.setValidityState(true);
    }
  }
}
