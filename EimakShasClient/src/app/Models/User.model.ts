export class User {
    firstName: string;
    lastName: string;
    phone?: string;
    dafPerDay: boolean;
    hasText: boolean;

    constructor(
    firstName: string,
    lastName: string,
    dafPerDay: boolean,
    hasText: boolean,
    phone?: string
    ) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.phone = phone;
        this.dafPerDay = dafPerDay;
        this.hasText = hasText;
    }
}

