import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Todo } from '../Models/todo.moddel';
import { catchError, Observable, observable, retry } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TodoService
{
  private baseUrl = "https://localhost:44313/api/";

  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  GetTodos()
  {
    let url = this.baseUrl + "todo";
    return this.http.get<Todo>(url).pipe(retry(1));
  }
  CreateTodo(todo: Todo): Observable<Todo>
  {
    return this.http.post<Todo>(this.baseUrl + "todo", JSON.stringify(todo), this.httpOptions).pipe(retry(1));
  }
  Delete(id: any)
  {
    let url = this.baseUrl + "todo/" + id;
    return this.http.delete(url, this.httpOptions).pipe(retry(1));
  }
  DoneTodo(id: any, todo: Todo)
  {
    let url = "https://localhost:44313/api/todo/PutDone/" + id;
    return this.http.put(url, todo, this.httpOptions).pipe(retry(1));
  }
  SetColorApi(color: any)
  {
    return this.http.post<any>("https://localhost:44313/api/color", JSON.stringify(color), this.httpOptions).pipe(retry(1));
  }
  GetColorApi(id: any)
  {
    let url = "https://localhost:44313/api/color/" + id;
    return this.http.get(url, this.httpOptions).pipe(retry(1));
  }
  UpdateTodoApi(_id: any, _data: any)
  {
    let url = 'https://localhost:44313/api/todo/' + _id;
    return this.http.put(url, _data, this.httpOptions).pipe(retry(1));
  }
  PutTodoCoolorApi(_id: any, _color: any)
  {
    let url = "https://localhost:44313/api/color/" + _id;
    return this.http.put<any>(url, JSON.stringify(_color), this.httpOptions).pipe(retry(1));
  }
  SetPositionApi(_id: any, _data: any)
  {
    let url = "https://localhost:44313/api/color/PutPosition/" + _id;
    return this.http.put(url, JSON.stringify(_data), this.httpOptions).pipe(retry(1));
  }
}
