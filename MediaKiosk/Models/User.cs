using System;
using System.Collections.Generic;
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
        public string EncryptedPassword { get; set; }
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

            UpdateEncryptedPassword();

            this.Purchases = new MediaLibrary();
            this.Rentals = new MediaLibrary();
        }

        private void UpdateEncryptedPassword()
        { 
            this.EncryptedPassword = Cryptography.EncryptString(this.Password);
        }
    }

    public class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            //Only check username and password
            return x.Username == y.Username && x.Password == y.Password;
        }

        public int GetHashCode(User user)
        {
            //Create tuple and let compiler handle the hash
            return new Tuple<string, string>(user.Username, user.Password).GetHashCode();
        }
    }
}
