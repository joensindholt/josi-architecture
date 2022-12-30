import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Store } from '@ngrx/store';
import { BehaviorSubject, combineLatestWith, map, mergeWith, Observable } from 'rxjs';
import { Todo } from '../store/todos/todo';
import { TodoActionsNames } from '../store/todos/todos.actions';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TodosComponent implements OnInit {
  todos$?: Observable<Todo[]>;

  filterTodoForm = this.fb.group({
    title: ['']
  });

  constructor(
    private store: Store<{ todos: Todo[] }>,
    private fb: FormBuilder,
    private changeDetectorRef: ChangeDetectorRef
  ) {
    // When only relying on the form valueschanges for filtering todos
    // it does not work on page refresh. Therefore we merge the valuesChanges with a behavior subject
    // to kick of the todo filtering
    this.todos$ = this.store.select('todos').pipe(
      combineLatestWith(
        this.filterTodoForm.valueChanges.pipe(
          map(values => values.title),
          mergeWith(new BehaviorSubject<string>(''))
        )
      ),
      map(([todos, todosFilter]) => {
        if (!!todosFilter) {
          return todos.filter(t => t.title.includes(todosFilter));
        }

        return todos;
      })
    );
  }

  ngOnInit(): void {
    this.store.dispatch({ type: TodoActionsNames.LoadTodos });
  }
}
