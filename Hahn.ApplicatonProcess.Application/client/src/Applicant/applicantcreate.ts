import { HttpClient, json } from 'aurelia-fetch-client';
import { inject, NewInstance } from 'aurelia-framework';
import {ApplicantCreated} from './messages'
import { ValidationControllerFactory,
  ValidationController,
  ValidationRules,
  validateTrigger } from 'aurelia-validation';
  import {BootstrapFormRenderer} from '../resources/bootstrap-form-renderer';
import { Applicant } from './applicant';
import { Router } from 'aurelia-router';
import {EventAggregator} from "aurelia-event-aggregator";

@inject(EventAggregator, ValidationControllerFactory,HttpClient, Router)
export class ApplicantDetail {
  //hired = null;
  private _ea;
  private http:HttpClient;
  controller=null;
  successMessage='';
  errorMessage=null;
  
  applicant = {
    name: '',
    familyName: '',
    address: '',
    countryOfOrigin: '',
    eMailAdress: '',
    age: null,
    hired: false
   
  };
  router: Router;
    constructor(ea: EventAggregator, controllerFactory, http: HttpClient, router: Router) {
      this._ea = ea;
      this.router = router;
      this.http=http; 
      this.controller = controllerFactory.createForCurrentScope();
      this.controller.addRenderer(new BootstrapFormRenderer());
      
      
     // this.controller.validateTrigger=validateTrigger.change;
      ValidationRules
      .ensure((p: Applicant) => p.name).required().minLength(5).withMessage('Name is required and must be A minimum of (5) characters.')
      .ensure((p: Applicant) => p.familyName).required().minLength(5).withMessage('FamilyName is required and must be A minimum of (5) characters.')
      .ensure((p: Applicant) => p.address).required().minLength(10).withMessage('Address is required and must be A minimum of (10) characters.')
      .ensure((p: Applicant) => p.hired).required().withMessage('Hired is required')
      .ensure((p: Applicant) => p.age).required().range(20,60).withMessage('Age is required and must be between (20) and (60)')
      .ensure((p: Applicant) => p.eMailAdress).required().matches(/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/).withMessage('Email is required')
      .ensure((p: Applicant) => p.countryOfOrigin).required().withMessage('Country is required')
      .on(this.applicant);
      this.controller.validate();
         
  }
  submit() {
    
  }
  reset(){
    window.location.reload();
  }
  get canSave(){
    return this.applicant.familyName && this.applicant.name 
    && this.applicant.address && this.applicant.age && this.applicant.countryOfOrigin 
    && this.applicant.address && this.applicant.eMailAdress;
  }
  get canReset(){
    return this.applicant.familyName || this.applicant.name 
    || this.applicant.address || this.applicant.age || this.applicant.countryOfOrigin 
    || this.applicant.address || this.applicant.eMailAdress;
  }
  create() {
    
    this.http.fetch('https://localhost:44330/AddApplicant',
         {
             method: 'post',
             body: json(this.applicant)
         })
         .then(response => {
              return response.json();
      }).catch((error) =>
         { 
           this.errorMessage = error.response
        });
        this.router.navigateToRoute('applicants');
        // console.log(this.successMessage);

        // this.router.navigateToRoute('applicants');
        // this.refresh();
    } 

   /** reset() {
      window.history.back();
    }
**/
    refresh(): void {
      window.location.reload();
  }
}

