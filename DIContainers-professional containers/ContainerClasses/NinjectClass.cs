using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIContainers.ContainerClasses
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoginValidator>().To<LoginValidator>();
            Bind<IPasswordValidator>().To<PasswordValidator>();
            Bind<ICharacterSkillPoints>().To<CharacterSkillPoints>();

            Bind<ICharacter>().To<Barbarian>();
            Bind<ICharacter>().To<Paladin>();
            Bind<ICharacter>().To<Thief>();
            Bind<ICharacter>().To<Mage>();
        }
    }
}
