using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MediaKiosk.Models
{
    [Serializable]
    public class User
    {
        public string Username {  get; set; }
        public SecureString Password { get; set; }
    }

    public class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            //Only check username
            return x.Username == y.Username && x.Password == y.Password;
        }

        public int GetHashCode(User user)
        {
            //Create tuple and let compiler handle the hash
            return new Tuple<string, SecureString>(user.Username, user.Password).GetHashCode();
        }
    }
}
