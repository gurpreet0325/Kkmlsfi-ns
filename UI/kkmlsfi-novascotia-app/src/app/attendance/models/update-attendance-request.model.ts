export interface UpdateAttendanceRequest {
    attendanceId: number;
    userEmail?: string;
    actionDateTime?: Date;
    isRemovedFromView: boolean;
    isFinal: boolean;
}