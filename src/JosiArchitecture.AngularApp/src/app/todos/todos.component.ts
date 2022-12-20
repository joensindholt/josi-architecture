import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Todo } from '../store/todos/todo';
import { TodoActionsNames } from '../store/todos/todos.actions';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.scss']
})
export class TodosComponent implements OnInit {
  todos$: Observable<Todo[]>;

  constructor(private store: Store<{ todos: Todo[] }>) {
    this.todos$ = store.select('todos');
  }

  ngOnInit(): void {
    this.store.dispatch({ type: TodoActionsNames.LoadTodos });
  }
}
