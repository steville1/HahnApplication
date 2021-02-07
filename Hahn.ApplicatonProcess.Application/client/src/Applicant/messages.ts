export class ApplicantUpdated {
  applicant: any;
  constructor(applicant) {
    this.applicant = applicant;
  }
}

export class ApplicantViewed {
  applicant: any;
  constructor(applicant) {
    this.applicant = applicant;
  }
}

export class ApplicantDeleted {
  applicant: any;
  constructor(applicant) {
    this.applicant = applicant;
  }
}

export class ApplicantCreated {
  //applicant: any;
  messageResponse:string;
  constructor(messageResponse) {
    this.messageResponse=messageResponse;
    console.log(this.messageResponse);
    //this.refresh();
    //console.log(messageResponse);
    //this.applicant = applicant;
    //this.refresh();
    //console.log(this.applicant);
    
  }
  refresh(): void {
    window.location.reload();
  }
}
