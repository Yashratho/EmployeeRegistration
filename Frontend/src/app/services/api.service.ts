import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Employee, Country, State } from '../models/api-models';
import { environment } from '../../environments/environment';

export interface ApiResponse<T> {
  success: boolean;
  message: string;
  data: T;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = environment.apiBaseUrl;

  constructor(private http: HttpClient) { }

  getEmployees(): Observable<Employee[]> {
    return this.http.get<ApiResponse<Employee[]>>(`${this.baseUrl}/Employees`)
      .pipe(map(response => response.data));
  }

  getEmployee(id: number): Observable<Employee> {
    return this.http.get<ApiResponse<Employee>>(`${this.baseUrl}/Employees/${id}`)
      .pipe(map(response => response.data));
  }

  createEmployee(employee: Employee): Observable<ApiResponse<Employee>> {
    return this.http.post<ApiResponse<Employee>>(`${this.baseUrl}/Employees`, employee);
  }

  updateEmployee(id: number, employee: Employee): Observable<ApiResponse<any>> {
    return this.http.put<ApiResponse<any>>(`${this.baseUrl}/Employees/${id}`, employee);
  }

  deleteEmployee(id: number): Observable<ApiResponse<any>> {
    return this.http.delete<ApiResponse<any>>(`${this.baseUrl}/Employees/${id}`);
  }

  getCountries(): Observable<Country[]> {
    return this.http.get<ApiResponse<Country[]>>(`${this.baseUrl}/Locations/countries`)
      .pipe(map(response => response.data));
  }

  getStatesByCountry(countryId: number): Observable<State[]> {
    return this.http.get<ApiResponse<State[]>>(`${this.baseUrl}/Locations/countries/${countryId}/states`)
      .pipe(map(response => response.data));
  }
}
