import { Component } from '@angular/core';
import { ToDo } from '../Models/ToDo';
import { TodoService } from '../Services/todo.service';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent {
  todos: ToDo[];
  userId: string;

  constructor(private todoService: TodoService) {}

  getTodos() {
    console.log(this.userId);
    this.todoService.getAllTodoForUser(this.userId).subscribe(data => {
      this.todos = data;
      console.log(data);
    });
  }
}
