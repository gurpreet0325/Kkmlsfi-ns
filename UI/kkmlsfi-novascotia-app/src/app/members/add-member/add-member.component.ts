import { Component, OnDestroy, OnInit } from '@angular/core';
import { type MemberRequest } from '../models/member-request.model';
import { FormsModule } from '@angular/forms';
import { MemberService } from '../services/member.service';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageSelectorComponent } from "../../shared/components/image-selector/image-selector.component";
import { InputValidationMessageComponent } from '../../shared/components/input-validation-message/input-validation-message.component';
import { AuthService } from '../../login/services/auth.service';

@Component({
    selector: 'app-add-member',
    standalone: true,
    templateUrl: './add-member.component.html',
    styleUrl: './add-member.component.css',
    imports: [FormsModule, ImageSelectorComponent, InputValidationMessageComponent]
})
export class AddMemberComponent implements OnDestroy, OnInit {
  model: MemberRequest = {
    memberId: 0,
    firstName: '',
    middleName: '',
    lastName: '',
    dateOfBirth: '',
    city: ''
  };
  private addMemberSubscription?: Subscription;
  private updateMemberSubscription?: Subscription;
  memberId: number = 0;
  paramsSubscription?: Subscription;
  mode!: string;
  isImageSelectorVisible: boolean = false;

  constructor(private memberService: MemberService, private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute) {
  }
  ngOnInit(): void {
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.setMode(Number(params.get('id')));

        if (!this.isNew()) {
          this.loadMemberInfo(this.memberId)
        }
      }
    });
  }

  ngOnDestroy(): void {
    this.addMemberSubscription?.unsubscribe();
    this.updateMemberSubscription?.unsubscribe();
    this.paramsSubscription?.unsubscribe();
  }

  onFormSubmit(){
    this.setActionLog();
    if (this.isNew()) {
      this.addMemberSubscription = this.memberService.addMember(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/members');
        }
      });
    } else {
      this.updateMemberSubscription = this.memberService.updateMember(this.model)
      .subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/members');
        }
      });
    }
  }

  onCancel() {
    this.router.navigateByUrl('/admin/members');
  }

  private loadMemberInfo(memberId: number) {
    this.memberService.getMemberById(this.memberId)
    .subscribe({
      next: (response) => {
        this.model =  {
          memberId: response.memberId,
          firstName: response.firstName,
          middleName: response.middleName,
          lastName: response.lastName,
          dateOfBirth: response.dateOfBirth,
          city: response.city
        };
      }
    });
  }

  isNew(): boolean {
    return this.mode === 'Add';
  }

  private setMode(memberId: number) {
    if (memberId > 0) {
      this.memberId = memberId;
      this.mode = 'Update';
    } else {
      this.mode = 'Add';
    }
  }

  openImageSelector(): void {
    this.isImageSelectorVisible = true;
  }

  closeImageSelector(): void {
    this.isImageSelectorVisible = false;
  }

  private setActionLog() {
    const userEmail = this.authService.getUser()?.email
    this.model.userEmail = userEmail;
    this.model.actionDateTime = new Date();
  }

}
