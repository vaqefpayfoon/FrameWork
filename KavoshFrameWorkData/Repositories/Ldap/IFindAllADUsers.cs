using KavoshFrameWorkCore.Models;
using System.Collections.Generic;

namespace KavoshFrameWorkData.Repositories.Ldap
{
    public interface IFindAllADUsers
    {

        List<DomainUser> FindAll(int id, string userName);
    }
}
