import { bootstrapApplication } from '@angular/platform-browser';
import { UserForm } from './components/user-form/user-form';
import { importProvidersFrom } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

bootstrapApplication(UserForm, {
  providers: [
    importProvidersFrom(HttpClientModule, FormsModule)
  ]
}).catch(err => console.error(err));
