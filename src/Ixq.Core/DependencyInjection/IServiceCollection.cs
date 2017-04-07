// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Ixq.Core.DependencyInjection
{
    /// <summary>
    /// 指定服务描述符集合的合同
    /// </summary>
    public interface IServiceCollection : IList<ServiceDescriptor>
    {
    }
}
