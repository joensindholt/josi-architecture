﻿using JosiArchitecture.Core.Shared.Model;
using System.Collections.Generic;
using System.Linq;

namespace JosiArchitecture.Core.Todos
{
    public class TodoList : AggregateRoot
    {
        public TodoList(string title) : this()
        {
            Title = title;
        }

        // For EF
        private TodoList()
        {
            Todos = new List<Todo>();
        }

        public int Id { get; private set; }

        public string Title { get; private set; }

        public List<Todo> Todos { get; set; }

        public void AddTodo(Todo todo)
        {
            Todos.Add(todo);
        }

        public void RemoveTodo(long id)
        {
            var todo = Todos.Single(t => t.Id == id);
            Todos.Remove(todo);
        }
    }
}