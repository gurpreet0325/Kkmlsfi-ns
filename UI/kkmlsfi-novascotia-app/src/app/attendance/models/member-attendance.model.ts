export interface MemberAttendance {
    membersAttendanceId: number;
    attendanceId: number;
    memberId: number;
    fullName: string;
    tithe: number;
    offering: number;
    mission: number;
    loveGift: number;
    buildingFund: number;
    others: number;
    note?: string;
}