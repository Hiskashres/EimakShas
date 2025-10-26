import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { UserForm } from './components/user-form/user-form';
import { RouterModule, Routes } from '@angular/router';
import { App } from './app'; // make sure this exists


@NgModule({
  declarations: [],
  imports: [BrowserModule, HttpClientModule, FormsModule, UserForm],
  providers: [],
  bootstrap: [App]
})
export class AppModule {}
