import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { User } from '../../api/user';
import { UserServiceV1 } from '../../services/v1/user.service';

@Component({
    styleUrls: ['./main.component.scss'],
    templateUrl: './main.component.html',
    providers: [MessageService]
})
export class MainComponent implements OnInit {

    loading: boolean = true;
    users: User[] = [];
    user: User = {};
    userDetailsDialog: boolean = false;
    deleteDialog: boolean = false;
    hnDisabled: boolean = true;

    constructor(
        private messageService: MessageService,
        private userServiceV1: UserServiceV1
    ) {

    }

    ngOnInit() {
        this.selectAll();
    }

    selectAll() {
        this.userServiceV1.select().subscribe((res: any) => {
            this.users = res?.data;
            this.loading = false;
        });
    }

    editUser(u: User) {
        this.hnDisabled = true;
        this.user = u;
        this.userDetailsDialog = true;
    }

    cancel() {
        this.user = {};
        this.userDetailsDialog = false;
    }

    add() {
        this.hnDisabled = false;
        this.user = {};
    }

    save() {
        if (this.user?.firstName == undefined || this.user?.lastName == undefined || this.user?.phoneNo == undefined || this.user?.email == undefined) {
            this.messageService.add({ severity: 'warn', summary: 'Warning', detail: "Please input all required fields", life: 3000 });
            return;
        }
        if (!this.hnDisabled && this.user.userId == undefined) {
            this.userServiceV1.insert(this.user).subscribe((res: any) => {
                if (res?.code == 2000) {
                    this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Saved', life: 3000 });
                    this.selectAll();
                    this.user = {};
                    this.userDetailsDialog = false;
                }
                else {
                    this.messageService.add({ severity: 'error', summary: 'Failed', detail: res?.messages, life: 3000 });
                }
            });
        }
        else {
            this.userServiceV1.update(this.user).subscribe((res: any) => {
                if (res?.code == 2000) {
                    this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Saved', life: 3000 });
                    this.selectAll();
                    this.user = {};
                    this.userDetailsDialog = false;
                }
                else {
                    this.messageService.add({ severity: 'error', summary: 'Failed', detail: res?.messages, life: 3000 });
                }
            });
        }
    }

    delete() {
        this.deleteDialog = true;
    }

    confirmDelete() {
        this.userServiceV1.delete(this.user).subscribe((res: any) => {
            if (res?.code == 2000) {
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Deleted', life: 3000 });
                this.selectAll();
                this.user = {};
                this.deleteDialog = false;
                this.userDetailsDialog = false;
            }
            else {
                this.messageService.add({ severity: 'error', summary: 'Failed', detail: res?.messages, life: 3000 });
            }
        });
    }
}
