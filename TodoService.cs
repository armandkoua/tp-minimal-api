namespace tp_minimal_api
{
    public class TodoService
    {
        private List<Todo> _todos=new List<Todo>();

        public List<Todo> GetAll()
        {
            return _todos;
        }
        
        public Todo? GetById(int id)
        {
            return _todos.Find(t => t.id == id);
        }

        public List<Todo> GetActives() 
        {
            return _todos.Where(t => !t.endDate.HasValue).ToList();
        }

        public Todo Create(string title)
        {
            var id = _todos.Any() ? _todos.Max(t => t.id) + 1: 1;
            var create = new Todo(id, title, DateTime.Now, null);
            _todos.Add(create);
            return create;
        }

        public void Update(Todo todo)
        {
            var  updatedIndex = _todos.FindIndex(t => t.id==todo.id);

            if (updatedIndex != -1)
            {
                _todos[updatedIndex] = todo;                
            }
        }

        public void Delete(int id)
        {
            var deleteIndex = _todos.FindIndex(t => t.id == id);
            if (deleteIndex != -1)
            {
                _todos.RemoveAt(deleteIndex);
            }
        }
    }
}
