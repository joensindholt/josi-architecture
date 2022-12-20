import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CounterComponent } from './counter/counter.component';
import { counterReducer } from './store/counter/counter.reducer';
import { TodoEffects } from './store/todos/todos.effects';
import { todosReducer } from './store/todos/todos.reducer';
import { TodosComponent } from './todos/todos.component';

@NgModule({
  declarations: [AppComponent, CounterComponent, TodosComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    StoreModule.forRoot({ count: counterReducer, todos: todosReducer }, {}),
    EffectsModule.forRoot([TodoEffects]),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
