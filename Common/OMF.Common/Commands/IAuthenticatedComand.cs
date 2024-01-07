using System;
using System.Collections.Generic;
using System.Text;

namespace OMF.Common.Commands
{
    interface IAuthenticatedComand : ICommand
    {
        Guid UserId { get; set; }
    }
}
