using System;

namespace Kona.Infrastructure {
    
    public interface IAuthenticationService {
        bool IsValidLogin(string userName, string password);
        object RegisterUser(string userName, 
            string password, 
            string confirmPassword, 
            string email, 
            string reminderQuestion, 
            string reminderAnswer);
    }
}
