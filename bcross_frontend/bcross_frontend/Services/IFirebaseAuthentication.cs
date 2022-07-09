using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace bcross_frontend
{
    public interface IFirebaseAuthentication
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        bool SignOut();
        bool IsSignIn();
        Task<string> RegisterWithEmailAndPassword(string email, string password);
        string GetEmail();
    }
}
