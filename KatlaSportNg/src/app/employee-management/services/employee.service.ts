import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee';
import { EmployeeListItem } from '../models/employee-list-item';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private url = environment.apiUrl + 'api/employees/';

  constructor(private http: HttpClient) { }

  getEmployees(): Observable<Array<EmployeeListItem>> {
    return this.http.get<Array<EmployeeListItem>>(this.url);
  }

  getEmployee(employeeId: number): Observable<Employee> {
    return this.http.get<Employee>(`${this.url}${employeeId}`);
  }

  addEmployee(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(`${this.url}`, employee);
  }

  updateEmployee(employee: Employee): Observable<Object> {
    return this.http.put<Object>(`${this.url}${employee.id}`, employee);
  }

  deleteEmployee(employeeId: number): Observable<Object> {
    return this.http.delete<Object>(`${this.url}${employeeId}`);
  }

  updateEmployeeCV(employeeId: number,fileToUpload: File): Observable<Object> {
    const formData: FormData = new FormData();
    formData.append('fileKey', fileToUpload, fileToUpload.name);
    return this.http.patch<Object>(`${this.url}${employeeId}/cv`,formData);
  }

}
