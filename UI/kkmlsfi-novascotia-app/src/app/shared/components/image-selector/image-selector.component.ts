import { Component, Input, OnInit } from '@angular/core';
import { MemberRequest } from '../../../members/models/member-request.model';
import { ImageService } from './image.service';
import { format } from 'date-fns';
import { Subscription } from 'rxjs';
import { MemberDisplayPicture } from '../../models/member-display-picture.model';

@Component({
  selector: 'app-image-selector',
  standalone: true,
  imports: [],
  templateUrl: './image-selector.component.html',
  styleUrl: './image-selector.component.css'
})
export class ImageSelectorComponent implements OnInit {
  @Input({required: true}) member!:MemberRequest;
  memberDisplayPicture?: MemberDisplayPicture;

  private file?: File;

  constructor(private imageService: ImageService) {}

  ngOnInit(): void {
    this.getMemberDisplayPicture();
  }

  onFileUploadChange(event: Event): void {
    const element = event.currentTarget as HTMLInputElement;
    this.file = element.files?.[0];
  }

  uploadImage(): void {
    if (this.file) {
      var formattedDate: string = format(new Date(), 'ddMMyyyyHHmmss');
      var fileName = `${formattedDate}${this.member.memberId}`;
      this.imageService.uploadMemberDisplayPicture(this.file,fileName,this.member.memberId.toString())
      .subscribe({
        next: (response) => {
          this.memberDisplayPicture = response;
        }
      });
    }
  }

  getMemberDisplayPicture() {
    this.imageService.getMemberDisplayPicture(this.member.memberId)
    .subscribe({
      next: (response) => {
        this.memberDisplayPicture = response;
      }
    });
  }
}
