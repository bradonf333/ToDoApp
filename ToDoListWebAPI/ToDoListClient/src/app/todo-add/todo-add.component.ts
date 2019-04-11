import { Component, OnInit } from '@angular/core';
import { config } from '../app.config';
import { ToDo } from '../Models/ToDo';
import { User } from '../Models/User';
import { TodoService } from '../Services/todo.service';

@Component({
  selector: 'app-todo-add',
  templateUrl: './todo-add.component.html',
  styleUrls: ['./todo-add.component.scss']
})
export class TodoAddComponent implements OnInit {
  // Form Values
  userId: string;
  title: string;
  description: string;
  status: string;
  priority: string;

  todo: ToDo;
  currentUser: User;

  constructor(private todoService: TodoService) {}

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem(config.userKey));
    this.userId = this.currentUser.userId;
  }

  submitTodo() {
    this.todo = {
      userId: this.currentUser.userId,
      title: this.title,
      description: this.description,
      status: this.status,
      priority: this.priority
    };

    this.todoService.addNewTodoForUser(this.todo);
  }
}
