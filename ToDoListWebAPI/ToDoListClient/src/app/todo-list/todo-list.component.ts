import { Component, OnInit } from '@angular/core';
import { config } from '../app.config';
import { ToDo } from '../Models/ToDo';
import { User } from '../Models/User';
import { TodoService } from '../Services/todo.service';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {
  todos: ToDo[];
  userId: string;

  constructor(private todoService: TodoService) {}

  currentUser: User;

  ngOnInit() {
    console.log('Current User: ', this.currentUser);
    this.currentUser = JSON.parse(localStorage.getItem(config.userKey));
    console.log('Current User: ', this.currentUser);

    this.todoService.getAllTodoForUser(this.currentUser.userId).subscribe(data => {
      this.todos = data;
      console.log('Data: ', data);
      console.log('Todos: ', this.todos);
    });
  }
}
