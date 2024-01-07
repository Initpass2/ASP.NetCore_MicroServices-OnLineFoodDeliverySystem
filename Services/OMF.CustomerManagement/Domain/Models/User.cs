using OMF.Common.Exception;
using OMF.CustomerManagement.Domain.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace OMF.CustomerManagement.Domain.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; protected set; }
        public string Email { get; set; }
        public string Password { get; protected set; }
        public string Name { get; set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected User() { }

        public User(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new OMFException("empty_user_email", $"user email cann't be empty");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new OMFException("empty_username_email", $"user name cann't be empty");
            }

            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }
        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new OMFException("empty_user_password", $"Password cann't be empty");
            }

            Salt = encrypter.GetSalt(password);
            Password = encrypter.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncrypter encrypter)
          => Password.Equals(encrypter.GetHash(password, Salt));
    }
}
