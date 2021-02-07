import { Aurelia, PLATFORM } from 'aurelia-framework';
import { Router, RouterConfiguration } from 'aurelia-router';

export class App {
  router: Router | undefined;

  configureRouter(config: RouterConfiguration, router: Router) {
    config.title = 'Aurelia CRUD';
    config.map([{
      route: [ '', 'home' ],
      name: 'home',
      moduleId: PLATFORM.moduleName('./home/home'),
      title: 'Home'
    }, {
      route: 'applicants-list',
      name: 'applicants',
      moduleId: PLATFORM.moduleName('./applicant/Applicants'),
      title: 'Applicant List'
    },{
      route: 'applicants-Create',
      name: 'ApplicantCreate',
      moduleId: PLATFORM.moduleName('./applicant/ApplicantCreate'),
      title: 'Applicant Create'
    },{
      route: 'applicants-Detail',
      name: 'applicantdetail',
      moduleId: PLATFORM.moduleName('./applicant/ApplicantDetail'),
      title: 'Applicant Create'
    }]);

    this.router = router;
  }
}
