import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { EventsComponent } from './events/events.component';
import { AnnouncementsComponent } from './announcements/announcements.component';
import { AttendanceComponent } from './attendance/attendance.component';
import { TithesComponent } from './tithes/tithes.component';
import { AddMemberComponent } from './members/add-member/add-member.component';
import { MembersComponent } from './members/members.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { authGuard } from './guards/auth.guard';
import { HomecellComponent } from './homecell/homecell.component';
import { CheckinMembersComponent } from './attendance/checkin-members/checkin-members.component';

export const routes: Routes = [
    { path: '', component: HomeComponent},
    { path: 'home', component: HomeComponent},
    { path: 'events', component: EventsComponent},
    { path: 'announcements', component: AnnouncementsComponent},
    { path: 'attendance', component: AttendanceComponent},
    { path: 'attendance/members/:id', component: CheckinMembersComponent},
    { path: 'homecell', component: HomecellComponent},
    { path: 'tithes', component: TithesComponent},
    { path: 'admin/members', component: MembersComponent},
    { path: 'admin/members/add', component: AddMemberComponent, canActivate: [authGuard]},
    { path: 'admin/members/update/:id', component: AddMemberComponent, canActivate: [authGuard]},
    { path: 'login', component: LoginComponent},
    { path: 'admin/register', component: RegisterComponent, canActivate: [authGuard]}
]
