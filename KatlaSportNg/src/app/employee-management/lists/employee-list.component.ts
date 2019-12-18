import { Component, OnInit } from '@angular/core';
import { EmployeeListItem } from '../models/employee-list-item';
import { EmployeeService } from '../services/employee.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees: EmployeeListItem[];
  fileToUpload: File = null;

  constructor(private employeeService: EmployeeService) { }

  ngOnInit() {
    this.getEmployees();
  }

  getEmployees() {
    this.employeeService.getEmployees().subscribe(h => this.employees = h);
  }

  onDelete(employeeId: number) {
    var employee = this.employees.find(h => h.id == employeeId);
    this.employeeService.deleteEmployee(employeeId).subscribe(c => this.getEmployees());
  }

  uploadFile(employeeId:number) {
    this.employeeService.updateEmployeeCV(employeeId,this.fileToUpload).subscribe(data => { },
      error => {
        console.log(error);
      });
    }

  fileHandler(employeeId:number, files: FileList) {
    this.fileToUpload = files.item(0);
    this.uploadFile(employeeId)
  }
}
