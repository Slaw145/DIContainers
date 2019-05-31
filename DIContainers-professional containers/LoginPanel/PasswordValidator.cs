using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DIContainers
{
    public interface IPasswordValidator
    {
        bool PasswordValidate(string password);
        void CountNumberOfCalling();
    }

    public class PasswordValidator : IPasswordValidator
    {
        public int instances;
        string PasswordPattern = @"(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}";

        public bool PasswordValidate(string password)
        {
            Match passwordmatchresult = Regex.Match(password, PasswordPattern);

            if (passwordmatchresult.Success)
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
            Console.WriteLine("Number of calling this method in object " + this.GetType().Name + " : " + instances);
        }
    }
}
