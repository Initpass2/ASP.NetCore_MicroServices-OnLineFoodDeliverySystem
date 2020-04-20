using System;
using System.Collections.Generic;
using System.Text;

namespace OMF.Common.Auth
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId);
    }
}
