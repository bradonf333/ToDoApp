import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ToDo } from './Models/ToDo';

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

  getAllTodoForUser(username: string): Observable<ToDo[]> {
    return this.http.get<ToDo[]>(
      `https://localhost:44318/api/ToDoObject/allTodoForUser?UserId=${username}`
    );
  }

  addNewTodoForUser(todo: ToDo): Observable<ToDo> {
    return this.http.post<ToDo>(
      `https://localhost:44318/api/ToDoObject`,
      todo,
      httpOptions
    );
  }
}
