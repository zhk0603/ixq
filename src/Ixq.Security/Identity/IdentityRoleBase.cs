﻿using Ixq.Core.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public abstract class IdentityRoleBase : IdentityRole, IEntity<string>
    {
    }
}