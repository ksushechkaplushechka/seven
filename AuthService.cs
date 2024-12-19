using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobike
{
    public static class AuthService
    {
        public static int? CurrentClientId { get; private set; }
        public static bool IsAdmin { get; private set; }

        public static void SetCurrentUser(int clientId, bool isAdmin)
        {
            CurrentClientId = clientId;
            IsAdmin = isAdmin;
        }

        public static void Logout()
        {
            CurrentClientId = null;
            IsAdmin = false;
        }
    }

}
