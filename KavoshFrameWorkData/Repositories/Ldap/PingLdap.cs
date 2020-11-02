using KavoshFrameWorkCore;
using KavoshFrameWorkCore.Models;
using KavoshFrameWorkData.Repositories.Generic;
using Serilog;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace KavoshFrameWorkData.Repositories.Ldap
{
    public class PingLdap : IPingLdap
    {
        private readonly KavoshFrameWorkContext _context;
        IEncriptDescriptString _encriptdescriptStringRepository;
        public PingLdap(KavoshFrameWorkContext context, IEncriptDescriptString encriptdescriptStringRepository)
        {
            _context = context;
            _encriptdescriptStringRepository = encriptdescriptStringRepository;
        }
        public bool Ping(int id, string Username)
        {
            try
            {
                bool pingResult = false;
                IQueryable<DomainSetting> domainSetting = _context.DomainSetting.Where(w => w.Id == id);

                var domain = domainSetting.Select(w => new DomainSetting
                {
                    UserName = w.UserName,
                    Server = w.Server,
                    Title = w.Title,
                    Password = w.Password
                }).FirstOrDefault();
                if (domain.Title != "کاربران سیستمی")
                {
                    string cipherText = "";
                    cipherText = _encriptdescriptStringRepository.DecryptString(domain.Password);
                    domain.Password = cipherText;
                }
                List<DomainUser> lstADUsers = new List<DomainUser>();
                DomainUser objSurveyUsers = new DomainUser();
                string dcString = "";
                string rootNode = "";
                string[] arrString;
                arrString = domain.Title.Split('.');
                if (arrString.Length == 1)
                {
                    dcString = "dc=" + domain.Title + "";
                    rootNode = arrString[0];
                }
                else
                {
                    for (int i = 0; i != arrString.Length; i++)
                    {
                        dcString += "dc=" + arrString[i].ToString() + ",";
                    }
                    if (arrString.Length == 3)
                        rootNode = arrString[1].ToString();
                    else if (arrString.Length == 2)
                        rootNode = arrString[0].ToString();
                    dcString = dcString.Substring(0, dcString.Length - 1);
                }


                string DomainPath = "LDAP://" + domain.Server + "/" + dcString;
                string ldapIp = DomainPath.Split("//")[1];
                ldapIp = ldapIp.Split("/dc=")[0];
                int port = int.Parse(ldapIp.Length == 2 ? ldapIp : "389");
                pingResult = CustomPingServer.IsADSAlive(ldapIp, port);
                System.DirectoryServices.DirectoryEntry searchRoot = new System.DirectoryServices.DirectoryEntry(DomainPath);
                try
                {
                    object nativeObject = searchRoot.NativeObject;

                    searchRoot.Username = domain.UserName;
                    searchRoot.Password = domain.Password;
                    DirectorySearcher search = new DirectorySearcher(searchRoot);

                    search.FindAll();
                }
                catch (Exception ex)
                {
                    pingResult = false;
                }

                if (pingResult)
                {
                    return pingResult;

                }
                else
                {
                    pingResult = false;

                }
                return pingResult;

            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return false;
            }
        }

    }
}
