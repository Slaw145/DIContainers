
using Autofac;
using Castle.Windsor;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DIContainers
{
    public class GameServer
    {
        ILoginValidator loginvalidator;
        IPasswordValidator passwordvalidator;
        ICharacterSkillPoints characterskillpoints;
        //ICharacterRace characterrace;

        public void ResolveInterfaces(IContainer container)
        {
            //----------------Ninject---------
            //loginvalidator = kernel.Get<ILoginValidator>();
            //passwordvalidator = kernel.Get<IPasswordValidator>();
            //characterskillpoints = kernel.Get<ICharacterSkillPoints>();

            // characterrace = kernel.Get<ICharacterRace>();

            //----------------StructureMap---------
            //using (var nested = container.GetNestedContainer())
            //{
            //    loginvalidator = nested.GetInstance<ILoginValidator>();
            //    passwordvalidator = nested.GetInstance<IPasswordValidator>();
            //    characterskillpoints = nested.GetInstance<ICharacterSkillPoints>();

            //    //characterrace = nested.GetInstance<ICharacterRace>();
            //}

            //----------------Castle Windsor-------- -
            //loginvalidator = container.Resolve<ILoginValidator>();
            //passwordvalidator = container.Resolve<IPasswordValidator>();
            //characterskillpoints = container.Resolve<ICharacterSkillPoints>();

            //characterrace = container.Resolve<ICharacterRace>();

            //----------------Unity---------
            //loginvalidator = container.Resolve<ILoginValidator>();
            //passwordvalidator = container.Resolve<IPasswordValidator>();
            //characterskillpoints = container.Resolve<ICharacterSkillPoints>();

            //characterrace = container.Resolve<ICharacterRace>();

            //----------------Autofac---------
            using (var scope = container.BeginLifetimeScope())
            {
                loginvalidator = scope.Resolve<ILoginValidator>();
                passwordvalidator = scope.Resolve<IPasswordValidator>();
                characterskillpoints = scope.Resolve<ICharacterSkillPoints>();
            }
        }

        public bool RegisterUser(string login, string password)
        {
            bool ifloginvalidate = loginvalidator.LoginValidate(login);
            bool ifpasswordvalidate = passwordvalidator.PasswordValidate(password);

            if (ifloginvalidate == true && ifpasswordvalidate == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ICharacter CreateCharacter(ICharacter character)
        {
            return characterskillpoints.CreateCharacter(character);
        }

        public ICharacterRace CreateCharacterRace(ICharacterRace characterrace)
        {
            return characterrace.CreateCharacterRace(characterrace);
        }

        public bool StartGame(ICharacter character, bool ifUserIsSigned)
        {
            if (character != null && ifUserIsSigned == true)
            {
                return true;
                //Start the game
            }
            else
            {
                return false;
                //Throw exception
            }
        }
    }
}
