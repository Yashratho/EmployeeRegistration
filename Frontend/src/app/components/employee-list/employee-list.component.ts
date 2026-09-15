import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { ToastService } from '../../services/toast.service';
import { Employee } from '../../models/api-models';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './employee-list.component.html'
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[] = [];
  filteredEmployees: Employee[] = [];

  currentPage = 1;
  pageSize = 5;

  searchTerm = '';

  constructor(
    private apiService: ApiService,
    private toastService: ToastService
  ) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.apiService.getEmployees().subscribe({
      next: (data) => {
        this.employees = data ?? [];
        this.applyFilter();
      },
      error: () => this.toastService.show('Failed to load employees', 'error')
    });
  }

  applyFilter(): void {
    const term = this.searchTerm.toLowerCase().trim();
    this.filteredEmployees = this.employees.filter(e =>
      e.employeeName.toLowerCase().includes(term) ||
      e.mobileNum.includes(term)
    );
    this.currentPage = 1;
  }

  get paginatedEmployees(): Employee[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    return this.filteredEmployees.slice(startIndex, startIndex + this.pageSize);
  }

  get totalPages(): number {
    return Math.ceil(this.filteredEmployees.length / this.pageSize);
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }

  deleteEmployee(id: number | undefined): void {
    if (!id) return;

    if (confirm('Are you sure you want to delete this employee?')) {
      this.apiService.deleteEmployee(id).subscribe({
        next: (response) => {
          if (response.success) {
            this.toastService.show('Employee deleted successfully', 'success');
            this.loadEmployees();
          } else {
            this.toastService.show(response.message, 'warning');
          }
        },
        error: () => this.toastService.show('Failed to delete employee', 'error')
      });
    }
  }
}
