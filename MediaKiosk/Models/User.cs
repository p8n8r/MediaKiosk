using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace MediaKiosk.Models
{
    [Serializable]
    public class User
    {
        public static readonly User INVALID_USER = new User(); 

        public string Username {  get; set; }
        [XmlIgnore]
        public string Password { get; set; }
        public byte[] PasswordData { get; set; }
        public MediaLibrary Purchases { get; set; }
        public MediaLibrary Rentals { get; set; }

        public User() //Required for XML serialization
        { 
            this.Purchases = new MediaLibrary();
            this.Rentals = new MediaLibrary();
        }

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;

            UpdatePasswordData();

            this.Purchases = new MediaLibrary();
            this.Rentals = new MediaLibrary();
        }

        private void UpdatePasswordData()
        { 
            //Convert password to bytes
            byte[] passwordBytes = Encoding.UTF8.GetBytes(this.Password);

            //Create encrypted password data
            this.PasswordData = ProtectedData.Protect(passwordBytes, null,
                DataProtectionScope.LocalMachine);
        }
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
            return new Tuple<string, string>(user.Username, user.Password).GetHashCode();
        }
    }
}
