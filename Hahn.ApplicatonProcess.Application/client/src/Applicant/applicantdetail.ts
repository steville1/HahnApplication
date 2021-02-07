import { inject } from 'aurelia-framework';
import { Applicant } from './applicant';
import { ApplicantService } from './applicantservice';
import { HttpClient, json } from 'aurelia-fetch-client';
import { Router } from 'aurelia-router';




@inject(ApplicantService, HttpClient, Router)
export class ApplicantDetail {
    //controller=null;
     router: Router;
     activeApplicant : any;
     applicant: Applicant;
     hasApplicantId: boolean;
     constructor(private applicantService: ApplicantService, private http: HttpClient, router: Router) {
      
      }

    activate(parms) {
        this.hasApplicantId = parms.id;
        if (this.hasApplicantId) {
            return this.applicantService.getById(parms.id)
                .then(applicant => this.applicant = applicant);          
        }
        console.log(this.applicant);
        return null;
    }
    Update() {
      this.applicantService.UpdateData(this.applicant.id,this.applicant);
      this.router.navigateToRoute('applicants');
      
   }
}
