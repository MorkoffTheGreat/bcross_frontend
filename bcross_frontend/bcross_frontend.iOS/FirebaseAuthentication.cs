using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using System.Threading.Tasks;
using Firebase.Auth;

namespace bcross_frontend.iOS
{
    //class FirebaseAuthentication : IFirebaseAuthentication
    //{
    //    public bool IsSignIn()
    //    {
    //        var user = Auth.DefaultInstance.CurrentUser;
    //        return user != null;
    //    }

    //    public async Task<string> LoginWithEmailAndPassword(string email, string password)
    //    {
    //        var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
    //        return await user.User.GetIdTokenAsync();
    //    }

    //    public bool SignOut()
    //    {
    //        try
    //        {
    //            _ = Auth.DefaultInstance.SignOut(out NSError error);
    //            return error == null;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }

    //    public async Task<string> RegisterWithEmailAndPassword(string email, string password)
    //    {
    //        var user = await Auth.DefaultInstance.CreateUserAsync(email, password);
    //        var token = await (Auth.DefaultInstance.CurrentUser.GetIdTokenAsync());
    //        return token;
    //    }
    //}
}