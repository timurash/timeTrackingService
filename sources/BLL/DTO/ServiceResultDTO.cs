namespace BLL.DTO
{
    /// <summary>
    /// DTO для передачи информации о состоянии валидации из сервисов в контроллеры.
    /// </summary>
    public class ServiceResultDTO
    {
        /// <summary>
        /// Конструктор, используемый в случае возникновения ошибки валидации.
        /// </summary>
        public ServiceResultDTO(string message)
        {
            IsValid = false;
            ErrorMessage = message;
        }

        /// <summary>
        /// Конструктор, используемый в случае успешного выполнения метода.
        /// </summary>
        /// <param name="isValid"></param>
        public ServiceResultDTO(bool isValid)
        {
            IsValid = isValid;
        }

        /// <summary>
        /// Хранит значение о том, успешно прошла валидация при выполнении метода или нет.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Хранит сообщение ошибки, произошедшей при валидации.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}