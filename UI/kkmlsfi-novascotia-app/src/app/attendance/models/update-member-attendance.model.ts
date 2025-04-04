export interface UpdateMembersAttendanceRequest {
    membersAttendanceId: number;
    valueType: number;
    value: number;
    note?: string;
    userEmail?: string;
    actionDateTime?: Date;
}

export enum ValueTypes {
    Tithe = 1,
    Offering = 2,
    Mission = 3,
    LoveGift = 4,
    BuildingFund = 5,
    Others = 6,
    Note = 7
}