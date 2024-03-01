using System;

namespace TodoApp
{
    internal class AuthenticationException: ApplicationException
    {
        public AuthenticationException(string message):base(message) { }
    }

}
