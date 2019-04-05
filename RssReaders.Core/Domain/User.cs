using System;

namespace RssReaders.Core.Domain {
    public class User {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public User (Guid id, string firstname, string lastname, string email, string password, string role) {
            Id = id;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}