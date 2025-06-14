import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { ILogin } from '../Models/login';
import { IRegister } from '../Models/register';

@Injectable({
providedIn: 'root'
})
export class LandingService {
  login: ILogin[];
  register: IRegister[];
  constructor(private _http: HttpClient) {
    this.login = [];
    this.register = [];
  }
}

//import { Injectable } from '@angular/core';
//import { HttpClient } from '@angular/common/http';
//import { ILogin } from '../models/login.model';         // adjust path as needed
//import { IRegister } from '../models/register.model';   // adjust path as needed

//@Injectable({
//  providedIn: 'root'
//})
//export class LandingService {
//  private baseUrl = 'https://localhost:port/api'; // replace with actual backend URL

//  constructor(private _http: HttpClient) { }

//  loginUser(data: ILogin) {
//    return this._http.post(`${this.baseUrl}/login`, data);
//  }

//  registerUser(data: IRegister) {
//    return this._http.post(`${this.baseUrl}/register`, data);
//  }
//}





