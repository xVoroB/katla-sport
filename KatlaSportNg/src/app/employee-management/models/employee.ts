export class Employee {
    constructor(
        public id: number,
        public name: string,
        public birthDate: string,
        public positionId: number,
        public chiefEmployeeId: number,
        public employeeCVId: number
    ) { }
}
