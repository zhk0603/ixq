using System;
using Ixq.Core.Entity;

namespace Ixq.Security.Identity
{
    public interface IUser : Microsoft.AspNet.Identity.IUser, IEntity<string>
    {
        new string Id { get; set; }
        string Email { get; set; }
        bool EmailConfirmed { get; set; }
        string PasswordHash { get; set; }
        string PhoneNumber { get; set; }
        DateTime? LastSignInDate { get; set; }
        DateTime? LastSignOutDate { get; set; }
        void OnSignInComplete();
        void OnSignOutComplete();
    }
}