import { HttpClient } from 'aurelia-fetch-client';
import { inject } from 'aurelia-framework';
import { Applicant } from './applicant';
import{ApplicantService} from './applicantservice'

@inject(ApplicantService, HttpClient)
export class Applicants {
    public applicants: Applicant[];
    applicant: Applicant;
    selectedApplicantId: string;
    

    constructor(private applicantService:ApplicantService, http: HttpClient) {
       http.fetch('https://localhost:44330/GetAllApplicants')
            .then(result => result.json())
            .then(data => {
                this.applicants = data;
            });
          
     }   
   
    select(applicant) {
        this.selectedApplicantId = applicant.id;
    }
    remove(applicant) {
      if(confirm('Are you sure that you want to delete this contact?')) {
        this.applicantService.DeleteApplicant(applicant.id);
        this.refresh();
      }
  }
  refresh(): void {
    window.location.reload();
  }
}


