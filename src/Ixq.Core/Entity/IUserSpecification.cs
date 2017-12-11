using System;

namespace Ixq.Core.Entity
{
    public interface IUserSpecification
    {
        DateTime? LastSignInDate { get; set; }
        DateTime? LastSignOutDate { get; set; }
        void OnSignInComplete();
        void OnSignOutComplete();
    }
}