using System;
using System.Collections.Generic;
using System.Text;

namespace KavoshFrameWorkData.Repositories.Ldap
{
    public interface IPingLdap
    {
        bool Ping(int id, string Password);
    }
}
