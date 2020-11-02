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
    public class FindAllADUsers : IFindAllADUsers
    {
        private readonly KavoshFrameWorkContext _context;
        IEncriptDescriptString _encriptdescriptStringRepository;
        public FindAllADUsers(IEncriptDescriptString encriptdescriptStringRepository,
             KavoshFrameWorkContext context)
        {

            _encriptdescriptStringRepository = encriptdescriptStringRepository;
            _context = context;
        }

        public List<DomainUser> FindAll(int id, string userName)
        {
            try
            {
                string cipherText = "";

                IQueryable<DomainSetting> domainSetting = _context.DomainSetting.Where(w => w.Id == id);
                var domain = domainSetting.Select(w => new DomainSetting
                {
                    UserName = w.UserName,
                    Server = w.Server,
                    Title = w.Title,
                    Password = w.Password
                }).FirstOrDefault();
                cipherText = _encriptdescriptStringRepository.DecryptString(domain.Password);
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
                System.DirectoryServices.DirectoryEntry searchRoot = new System.DirectoryServices.DirectoryEntry(DomainPath);
                try
                {
                    object nativeObject = searchRoot.NativeObject;

                    searchRoot.Username = domain.UserName;
                    searchRoot.Password = cipherText;
                    DirectorySearcher search = new DirectorySearcher(searchRoot);
                    if (userName == "*")
                    {
                        search.Filter = $"(objectClass=user)";
                    }
                    else
                    {
                        userName = userName.Split("@")[0];
                        search.Filter = $"(samaccountname=*{userName}*)";
                    }

                    search.PropertiesToLoad.Add("samaccountname");
                    search.PropertiesToLoad.Add("mail");
                    search.PropertiesToLoad.Add("usergroup");
                    search.PropertiesToLoad.Add("displayname");//first name
                    search.PropertiesToLoad.Add("givenname");//first name
                    search.PropertiesToLoad.Add("sn");//first name
                    SearchResult resultFetch;


                    //SearchResultCollection resultCol = search.FindAll();
                    SearchResult resultCol = search.FindOne();

                    if (resultCol != null)
                    {
                        //for (int counter = 0; counter < resultCol.Count; counter++)
                        //{
                        string UserNameEmailString = string.Empty;
                        //resultFetch = resultCol[counter];
                        resultFetch = resultCol;
                        if (resultFetch.Properties.Contains("samaccountname"))
                        {
                            objSurveyUsers = new DomainUser();
                            if (resultFetch.Properties.Contains("mail"))
                            {
                                objSurveyUsers.Email = (String)resultFetch.Properties["mail"][0];
                            }
                            else
                            {
                                //  objSurveyUsers.Email = (String)resultFetch.Properties["samaccountname"][0] + id.ToString() + "@Pointer.com";
                            }

                            if (resultFetch.Properties.Contains("displayname"))
                            {
                                objSurveyUsers.DisplayName = (String)resultFetch.Properties["displayname"][0];
                            }

                            else
                            {
                                objSurveyUsers.DisplayName = (String)resultFetch.Properties["samaccountname"][0];
                            }


                            objSurveyUsers.UserName = (String)resultFetch.Properties["samaccountname"][0];

                            if (resultFetch.Properties.Contains("givenname"))
                            {
                                objSurveyUsers.FirstName = (String)resultFetch.Properties["givenname"][0];
                            }
                            else
                            {
                                objSurveyUsers.FirstName = (String)resultFetch.Properties["samaccountname"][0];
                            }
                            if (resultFetch.Properties.Contains("sn"))
                            {
                                objSurveyUsers.LastName = (String)resultFetch.Properties["sn"][0];
                            }
                            else
                            {
                                objSurveyUsers.LastName = (String)resultFetch.Properties["samaccountname"][0];
                            }
                            objSurveyUsers.dcString = dcString;
                            lstADUsers.Add(objSurveyUsers);
                        }
                    }
                }
                // }
                catch (Exception ex)
                {

                }
                return lstADUsers;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                return null;
            }

        }
    }
}
