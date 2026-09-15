export interface Country {
    countryId: number;
    countryName: string;
}

export interface State {
    stateId: number;
    stateName: string;
    countryId: number;
}

export interface Employee {
    employeeId?: number;
    employeeName: string;
    age: number;
    mobileNum: string;
    pincode: string;
    dob?: string | null;
    addressLine1: string;
    addressLine2?: string;
    stateId: number;
    countryId: number;
    
    country?: Country;
    state?: State;
}
