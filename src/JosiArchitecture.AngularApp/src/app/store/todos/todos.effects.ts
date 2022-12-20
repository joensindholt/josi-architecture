import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';
import { Todo } from './todo';
import { loadTodosSuccess, TodoActionsNames } from './todos.actions';

@Injectable()
export class TodoEffects {
  loadTodos$ = createEffect(() =>
    this.actions$.pipe(
      ofType(TodoActionsNames.LoadTodos),
      mergeMap(() =>
        this.http.get<Todo[]>('https://jsonplaceholder.typicode.com/todos').pipe(
          map(todos => loadTodosSuccess({ todos })),
          catchError(() => of({ type: TodoActionsNames.LoadTodosError }))
        )
      )
    )
  );

  constructor(private actions$: Actions, private http: HttpClient) {}
}
