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

export const routes: Routes = [
    { path: '', component: HomeComponent},
    { path: 'home', component: HomeComponent},
    { path: 'events', component: EventsComponent},
    { path: 'announcements', component: AnnouncementsComponent},
    { path: 'attendance', component: AttendanceComponent},
    { path: 'tithes', component: TithesComponent},
    { path: 'admin/members', component: MembersComponent},
    { path: 'admin/members/add', component: AddMemberComponent},
    { path: 'admin/members/update/:id', component: AddMemberComponent},
    { path: 'login', component: LoginComponent},
    { path: 'register', component: RegisterComponent}
]
