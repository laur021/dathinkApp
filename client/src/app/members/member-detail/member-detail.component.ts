import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_services/members.service';
import { ActivatedRoute } from '@angular/router';
import { Member } from '../../_models/member';
import { TabsModule } from 'ngx-bootstrap/tabs';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  imports: [TabsModule],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css',
  
})
export class MemberDetailComponent implements OnInit {
  private memberService = inject(MembersService);
  private route = inject(ActivatedRoute);
  member?: Member;

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    const username = this.route.snapshot.paramMap.get('username');
    if (!username) return;
    this.memberService.getMember(username).subscribe({
      next: (member) => (this.member = member),
    });
  }
}

/**
 * This `MemberDetailComponent` is a standalone Angular component that displays the details of a specific member.
 * 
 * Key points:
 * - **DI with `inject`**: The `MembersService` and `ActivatedRoute` are injected using Angular's standalone `inject` function.
 * - **Member Retrieval**:
 *    - The `loadMember()` method retrieves the `username` parameter from the current route (`ActivatedRoute`).
 *    - If the `username` exists, it calls `getMember(username)` on the `MembersService` to fetch the member details.
 *    - The member details are then stored in the `member` property of the component.
 * - **`ngOnInit` Lifecycle Hook**: 
 *    - This is called after the component is initialized.
 *    - It triggers the `loadMember()` method to fetch the member's details as soon as the component is loaded.
 * - **Template Binding**:
 *    - The `member` property can be used in the template to display member details dynamically.
 */

