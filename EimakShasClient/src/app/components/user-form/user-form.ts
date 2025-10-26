import { Component } from '@angular/core';
import { UserService } from '../../services/user';
import { User } from '../../Models/User.model'
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-form.html',
  styleUrls: ['./user-form.scss',]
})
export class UserForm {
user: User = new User('', '', true, true, '')

constructor(private userService: UserService) {}

onSubmit() {
  this.userService.addUser(this.user).subscribe({
    next: (res) => {
      alert(`${this.user.firstName} ${this.user.lastName} has been added`);
      console.log(res);
    },
    error: (err) => {
      alert(`Error while adding ${this.user.firstName} ${this.user.lastName}`);
console.log(err);
    }
  });
}
}
