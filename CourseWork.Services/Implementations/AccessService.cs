using System.Linq;
using System.Security.Claims;
using CourseWork.Services.Interfaces;

namespace CourseWork.Services.Implementations
{
    /// <summary>
    /// Представляет сервис для проверки уровня доступа клиента.
    /// </summary>
    public class AccessService : IAccessService
    {
        /// <inheritdoc cref="IAccessService.IsAdmin"/>
        public bool IsAdmin(ClaimsPrincipal user)
            => user.Claims.First(a => a.Type == "Access").Value == "Admin";

        /// <inheritdoc cref="IAccessService.IsUser"/>
        public bool IsUser(ClaimsPrincipal user)
            => user.Claims.First(a => a.Type == "Access").Value == "User";
    }
}
