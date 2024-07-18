export interface MemberAttendanceRequest {
    attendanceId: number;
    memberId: number;
    userEmail?: string;
    actionDateTime?: Date;
}