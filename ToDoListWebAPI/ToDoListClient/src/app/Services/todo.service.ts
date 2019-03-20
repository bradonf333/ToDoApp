import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { config } from '../app.config';
import { ToDo } from '../Models/ToDo';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'my-auth-token'
  })
};

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  constructor(private http: HttpClient) {}

  getAllTodoForUser(userId: string): Observable<ToDo[]> {
    const result = this.http.get<ToDo[]>(
      `${config.todoWebApiUrl}/ToDoObject/allTodoForUser?UserId=${userId}`
    ); // TODO: Should this be in config obj?

    return result;
  }

  addNewTodoForUser(todo: ToDo) {
    this.http
      .post<ToDo>(`${config.todoWebApiUrl}/ToDoObject`, todo, httpOptions)
      .subscribe(data => console.log(data));

    // return this.http.post<ToDo>(`${config.todoWebApiUrl}/ToDoObject`, todo, httpOptions);
  }
}
