using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DIContainers
{
    public interface ILoginValidator
    {
        bool LoginValidate(string login);
        void CountNumberOfCalling();
    }

    public class LoginValidator : ILoginValidator
    {
        public int instances;
        string LoginPattern = @"(?=.*[A-Za-z0-9]$)[A-Za-z][A-Za-z\d.-]{0,19}";

        public bool LoginValidate(string login)
        {
            Match loginmatchresult = Regex.Match(login, LoginPattern);

            if (loginmatchresult.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CountNumberOfCalling()
        {
            instances += 1;
            Console.WriteLine("Number of calling this method in object "+ this.GetType().Name + " : "+instances);
        }
    }
}
