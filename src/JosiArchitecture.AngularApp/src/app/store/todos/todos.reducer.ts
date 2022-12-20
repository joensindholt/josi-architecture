import { createReducer, on } from '@ngrx/store';
import { Todo } from './todo';
import { loadTodosSuccess } from './todos.actions';

export const initialState: Todo[] = [];

export const todosReducer = createReducer(
  initialState,
  on(loadTodosSuccess, (state, { todos }) => todos)
);
