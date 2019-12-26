import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class ApiService {
  API_URL  =  'http://localhost:52686';
  constructor(private  httpClient:  HttpClient) {}
  getContacts(){
    return  this.httpClient.get(`${this.API_URL}/api/Employee/0`);
  }

  UpdateData(contact){
    return  this.httpClient.post(`${this.API_URL}/api/Employee`,contact);
  }
}
