import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../services/employee.service';
import { Employee } from '../models/employee';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.css']
})
export class EmployeeFormComponent implements OnInit {

  employee = new Employee(0, "", "", 0, 0, 0);
  exists = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeeService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(p => 
      {
        if (p['id'] === undefined) return;
        this.employeeService.getEmployee(p['id']).subscribe(n => this.employee = n);
        this.exists = true;
      });
  }

  navigateToEmployees() {
    this.router.navigate(['/employees']);
  }

  onCancel() {
    this.navigateToEmployees();
  }

  onSubmit() {
    if (this.exists) 
    {
      this.employeeService.updateEmployee(this.employee).subscribe(s => this.navigateToEmployees());
    }
    else 
    {
      this.employeeService.addEmployee(this.employee).subscribe(s => this.navigateToEmployees());
    }
  }

  onPurge() {
    this.employeeService.deleteEmployee(this.employee.id).subscribe(p => this.navigateToEmployees());
  }
}
