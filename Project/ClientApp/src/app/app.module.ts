import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Provider, importProvidersFrom } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientJsonpModule, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ListUsersComponent } from './components/user/list-users/list-users.component';
import { EditUserComponent } from './components/user/edit-user/edit-user.component';
import { EditRoleComponent } from './components/role/edit-role/edit-role.component';
import { ListRolesComponent } from './components/role/list-roles/list-roles.component';
import { httpInterceptor } from './app.interceptor';

import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';

import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSelectModule } from '@angular/material/select';




export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: httpInterceptor, multi: true },
 //add all the intercepters here
];


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    ListUsersComponent,
    EditUserComponent,
    ListRolesComponent,
    EditRoleComponent, 

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatSlideToggleModule,
    MatDatepickerModule, MatInputModule, MatNativeDateModule, MatSelectModule,

    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'list-users', component: ListUsersComponent },
      { path: 'edit-user', component: EditUserComponent },
      { path: 'list-roles', component: ListRolesComponent },
      { path: 'edit-role', component: EditRoleComponent },
    ]),
    BrowserAnimationsModule,
  ],
  providers: [
    httpInterceptorProviders,
    importProvidersFrom(HttpClientModule),
    importProvidersFrom(HttpClientJsonpModule),
  ],


  bootstrap: [AppComponent]
})
export class AppModule { }
