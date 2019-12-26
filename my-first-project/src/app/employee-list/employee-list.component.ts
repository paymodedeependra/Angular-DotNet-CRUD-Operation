import { Component, OnInit } from '@angular/core';
import { ApiService } from  '../api.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  private  Employee:  Array<object> = [];
  private firstname: string = "";
  private lastname: string = "";
  private id: number = 0;
  constructor(private  apiService:  ApiService) { }
  ngOnInit() {
      this.getContacts();
  }
  public  getContacts(){
      this.apiService.getContacts().subscribe((data:  Array<object>) => {
          this.Employee  =  data;
          console.log(data);
      });
  }
  public fillData(data: { lngidId: number ,strFirstName: string, strLastName: string}){
    this.firstname = data.strFirstName,
    this.lastname = data.strLastName,
    this.id = data.lngidId
  };

  public UpdateData(){
    var  contact  = {
        lngidId:this.id,
        strFirstName:this.firstname,
        strLastName:this.lastname
    };
    this.apiService.UpdateData(contact).subscribe((response) => {
        this.getContacts();
        console.log(response);
    });
}
}
