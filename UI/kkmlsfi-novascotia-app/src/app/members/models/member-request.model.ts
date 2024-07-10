export interface MemberRequest {
    memberId: number
    firstName: string;
    middleName: string;
    lastName: string;
    dateOfBirth: string;
    city: string;
    userEmail?: string;
    actionDateTime?: Date;
}