import { Component, inject, OnInit } from '@angular/core';
import { Member } from '../../_models/member';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';
import { KeyValuePipe, NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [NgFor, NgIf, KeyValuePipe],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css',
})
export class MemberEditComponent implements OnInit {
  member?: Member;
  private accountService = inject(AccountService);
  private memberService = inject(MembersService);

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    const user = this.accountService.currentUser();
    if (!user) return;
    this.memberService.getMember(user.username).subscribe({
      next: (member) => (this.member = member),
    });
  }
}
