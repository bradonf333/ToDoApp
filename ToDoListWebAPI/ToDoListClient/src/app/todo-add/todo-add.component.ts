import { Component, OnInit } from '@angular/core';
import { ToDo } from '../Models/ToDo';
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

  constructor(private todoService: TodoService) {}

  ngOnInit() {}

  submitTodo() {
    this.todo = {
      userId: this.userId,
      title: this.title,
      description: this.description,
      status: this.status,
      priority: this.priority
    };

    console.log('Todo: ', this.todo);

    this.todoService.addNewTodoForUser(this.todo);
    // TODO: Need some logging here? Or is the logging done in the API?
    // Think its in the API on line 35 of AddToDoObjectService.
    console.log('Hello');
  }
}
