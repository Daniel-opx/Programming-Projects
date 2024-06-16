namespace ASP.NET_learning
{
    public interface ITaskService
    {
        Todo? GetTodoById(int id);
        List<Todo> GetTodos();
        void DeleteTodoById(int id);
        Todo AddTodo(Todo task);
    }

    public class InMemoryTaskService : ITaskService
    {

        private readonly List<Todo> _todos = new();
        public Todo AddTodo(Todo task)
        {
            _todos.Add(task);
            return task;
        }

        public void DeleteTodoById(int id)
        {
           _todos.RemoveAll(task =>task.ID == id);
        }

        public Todo? GetTodoById(int id)
        {
            return _todos.SingleOrDefault(task => task.ID == id); 
        }

        public List<Todo> GetTodos() => _todos;
       
    }
}
