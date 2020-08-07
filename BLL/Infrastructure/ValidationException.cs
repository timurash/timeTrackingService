using System;
namespace BLL.Infrastructure
{
    // класс для отлова ошибек валидации
    public class ValidationException : Exception
    {
        public string Property { get; protected set; }
        public ValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
