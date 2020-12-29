using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace _4us2watch
{
    public interface IAuth
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task<bool> SignUpWithEmailPassword(string email, string password);
    }   
}
