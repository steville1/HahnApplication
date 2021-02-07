import { HttpClient } from 'aurelia-fetch-client';
import { inject } from 'aurelia-framework';
import { Applicant } from '../Applicant/applicant';

@inject(HttpClient)
export class ApplicantService {

    constructor(private http: HttpClient) {
        http.configure(config => {
            config.useStandardConfiguration()
                .withBaseUrl('http://localhost:13322/api/contactsApi/');
        });
    }

    getById(id: string): Promise<Applicant> {
        return this.http.fetch(id)
            .then(response => response.json())
            //.then(applicant => new Applicant(applicant))
            .catch(error => console.log(error));
    }

}
