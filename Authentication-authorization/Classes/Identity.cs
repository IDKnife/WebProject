namespace Authentication_authorization.Classes
{
    /// <summary>
    /// Представляет электронную почту и логин клиента.
    /// </summary>
    public class Identity
    {
        /// <summary>
        /// Электронная почта клиента.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль клиента.
        /// </summary>
        public string Password { get; set; }
    }
}
