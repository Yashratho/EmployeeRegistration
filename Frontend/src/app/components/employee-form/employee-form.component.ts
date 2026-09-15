import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../services/api.service';
import { ToastService } from '../../services/toast.service';
import { Country, State } from '../../models/api-models';

@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employee-form.component.html'
})
export class EmployeeFormComponent implements OnInit {
  employeeForm!: FormGroup;
  countries: Country[] = [];
  states: State[] = [];
  isEditMode = false;
  employeeId?: number;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private toastService: ToastService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadLocations();

    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.isEditMode = true;
        this.employeeId = +id;
        this.loadEmployee(this.employeeId);
      }
    });
  }

  initForm(): void {
    this.employeeForm = this.fb.group({
      employeeId: [{ value: '', disabled: true }],
      employeeName: ['', [Validators.required, Validators.pattern('^[a-zA-Z ]+$')]],
      age: ['', [Validators.required, Validators.pattern('^[0-9]{1,3}$')]],
      mobileNum: ['', [Validators.required, Validators.pattern('^[0-9]{10}$')]],
      dob: ['', [this.noFutureDateValidator]],
      addressLine1: ['', [Validators.required, this.noSpecialCharsValidator]],
      addressLine2: ['', [this.noSpecialCharsValidator]],
      pincode: ['', [Validators.required, Validators.pattern('^[0-9]{6}$')]],
      countryId: ['', Validators.required],
      stateId: ['', Validators.required]
    });

    this.employeeForm.get('dob')?.valueChanges.subscribe(val => {
      if (val) {
        const today = new Date();
        const birthDate = new Date(val);
        if (birthDate <= today) {
          let age = today.getFullYear() - birthDate.getFullYear();
          const m = today.getMonth() - birthDate.getMonth();
          if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age--;
          }
          this.employeeForm.get('age')?.setValue(age, { emitEvent: false });
        }
      }
    });

    this.employeeForm.get('countryId')?.valueChanges.subscribe(val => {
      if (val) {
        this.employeeForm.get('stateId')?.setValue('');
        this.apiService.getStatesByCountry(+val).subscribe(data => this.states = data);
      } else {
        this.states = [];
      }
    });
  }

  loadLocations(): void {
    this.apiService.getCountries().subscribe(data => this.countries = data);
  }

  loadEmployee(id: number): void {
    this.apiService.getEmployee(id).subscribe({
      next: (emp) => {
        this.apiService.getStatesByCountry(emp.countryId).subscribe(states => {
          this.states = states;

          let dobValue = '';
          if (emp.dob) {
            dobValue = new Date(emp.dob).toISOString().split('T')[0];
          }

          this.employeeForm.patchValue({
            employeeId: emp.employeeId,
            employeeName: emp.employeeName,
            age: emp.age,
            mobileNum: emp.mobileNum,
            dob: dobValue,
            addressLine1: emp.addressLine1,
            addressLine2: emp.addressLine2,
            pincode: emp.pincode,
            countryId: emp.countryId,
            stateId: emp.stateId
          });
        });
      },
      error: () => this.toastService.show('Failed to load employee details', 'error')
    });
  }

  onSubmit(): void {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAllAsTouched();
      return;
    }

    const raw = this.employeeForm.getRawValue();

    const payload = {
      employeeId: this.isEditMode && this.employeeId ? this.employeeId : 0,
      employeeName: raw.employeeName,
      age: Number(raw.age),
      mobileNum: raw.mobileNum,
      pincode: raw.pincode,
      dob: raw.dob ? raw.dob : null,
      addressLine1: raw.addressLine1,
      addressLine2: raw.addressLine2 ?? '',
      countryId: Number(raw.countryId),
      stateId: Number(raw.stateId)
    };

    if (this.isEditMode && this.employeeId) {
      this.apiService.updateEmployee(this.employeeId, payload as any).subscribe({
        next: (response) => {
          if (response.success) {
            this.toastService.show('Employee updated successfully', 'success');
            this.router.navigate(['/employees']);
          } else {
            this.toastService.show(response.message, 'warning');
          }
        },
        error: (err) => {
          const msg = err?.error?.message || 'Failed to update employee';
          this.toastService.show(msg, 'error');
        }
      });
    } else {
      this.apiService.createEmployee(payload as any).subscribe({
        next: (response) => {
          if (response.success) {
            this.toastService.show('Employee created successfully', 'success');
            this.router.navigate(['/employees']);
          } else {
            this.toastService.show(response.message, 'warning');
          }
        },
        error: (err) => {
          const msg = err?.error?.message || 'Failed to create employee';
          this.toastService.show(msg, 'error');
        }
      });
    }
  }

  noFutureDateValidator(control: AbstractControl): ValidationErrors | null {
    if (control.value) {
      const selectedDate = new Date(control.value);
      const today = new Date();
      if (selectedDate > today) {
        return { futureDate: true };
      }
    }
    return null;
  }

  noSpecialCharsValidator(control: AbstractControl): ValidationErrors | null {
    if (control.value && /[$%!+]/.test(control.value)) {
      return { specialChars: true };
    }
    return null;
  }

  get f() { return this.employeeForm.controls; }
}
