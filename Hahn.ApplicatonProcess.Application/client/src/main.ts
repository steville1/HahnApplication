import {Aurelia} from 'aurelia-framework';
import * as environment from '../config/environment.json';
import {PLATFORM} from 'aurelia-pal';
import {GlobalValidationConfiguration, validateTrigger} from "aurelia-validation";

export function configure(aurelia: Aurelia): void {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName('resources/index'));

   // aurelia.use.plugin('aurelia-validation')
   aurelia.use.plugin(PLATFORM.moduleName('aurelia-validation'));
   aurelia.use.plugin("aurelia-validation", (config: GlobalValidationConfiguration) => {
    config.defaultValidationTrigger(validateTrigger.manual);
  });

  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');


  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }

  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app')));
}


