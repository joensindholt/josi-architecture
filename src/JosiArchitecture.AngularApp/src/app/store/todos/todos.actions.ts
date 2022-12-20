import { createAction, props } from '@ngrx/store';
import { Todo } from './todo';

export enum TodoActionsNames {
  LoadTodos = '[Todos] Load Todos',
  LoadTodosSuccess = '[Todos] Load Todos Success',
  LoadTodosError = '[Todos] Load Todos Error'
}

export const loadTodos = createAction(TodoActionsNames.LoadTodos);
export const loadTodosSuccess = createAction(TodoActionsNames.LoadTodosSuccess, props<{ todos: Todo[] }>());
export const loadTodosError = createAction(TodoActionsNames.LoadTodosError);
