using Autofac;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Ninject;
using Ninject.Modules;
using StructureMap;
using System;
using DIContainers.ContainerClasses;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace DIContainers
{
    class WebServer
    {
        static GameServer gameServer;

        static void Main(string[] args)
        {
            //----NinJect---------
            //IKernel kernel = new StandardKernel(new Bindings());
            //gameServer = kernel.Get<GameServer>();

            //IEnumerable<ICharacter> characters = kernel.GetAll<ICharacter>();

            //foreach (ICharacter character in characters)
            //{
            //    Console.WriteLine(character.GetType().Name + " has " + character.Strength + " strength");
            //}

            //-----StructureMap---------
            //var container = new Container(_ =>
            //{
            //    _.Scan(x =>
            //    {
            //        x.TheCallingAssembly();
            //        x.WithDefaultConventions();
            //    });
            //});

            //gameServer = container.GetInstance<GameServer>();

            //-----Castle Windsor---------
            //IWindsorContainer container = new WindsorContainer();
            //container.Register(Component.For<ILoginValidator>().ImplementedBy<LoginValidator>());
            //container.Register(Component.For<IPasswordValidator>().ImplementedBy<PasswordValidator>());
            //container.Register(Component.For<ICharacterSkillPoints>().ImplementedBy<CharacterSkillPoints>());
            //container.Register(Component.For<GameServer>());

            //gameServer = container.Resolve<GameServer>();

            //-----Unity---------
            //IUnityContainer container = new UnityContainer();
            //container.RegisterType<ICharacterSkillPoints, CharacterSkillPoints>(new TransientLifetimeManager());
            //container.RegisterType<ILoginValidator, LoginValidator>(new ContainerControlledLifetimeManager());
            //container.RegisterType<IPasswordValidator, PasswordValidator>(new HierarchicalLifetimeManager());

            //var class3 = container.Resolve<ICharacterSkillPoints>();
            //class3.CountNumberOfCalling();

            //var class4 = container.Resolve<ICharacterSkillPoints>();
            //class4.CountNumberOfCalling();

            //var class1 = container.Resolve<ILoginValidator>();
            //class1.CountNumberOfCalling();

            //var class2 = container.Resolve<ILoginValidator>();
            //class2.CountNumberOfCalling();

            //var class5 = container.Resolve<IPasswordValidator>();
            //class5.CountNumberOfCalling();

            //var class6 = container.Resolve<IPasswordValidator>();
            //class6.CountNumberOfCalling();

            //var childContainer = container.CreateChildContainer();

            //var class7 = childContainer.Resolve<IPasswordValidator>();
            //class7.CountNumberOfCalling();

            //var class8 = childContainer.Resolve<IPasswordValidator>();
            //class8.CountNumberOfCalling();

            //gameServer = container.Resolve<GameServer>();

            //-----Autofac---------
            var builder = new ContainerBuilder();
            builder.RegisterType<LoginValidator>().As<ILoginValidator>();
            builder.RegisterType<PasswordValidator>().As<IPasswordValidator>();
            builder.RegisterType<CharacterSkillPoints>().As<ICharacterSkillPoints>();

            builder.RegisterType<GameServer>();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                gameServer = scope.Resolve<GameServer>();
            }

            gameServer.ResolveInterfaces(container);

            bool ifUserIsLoginIn = LogIn();

            ICharacter createdCharacter = CreateCharacter();

            StartGame(createdCharacter, ifUserIsLoginIn);

            //Register factory through delegate
            //var builder = new ContainerBuilder();
            //builder.RegisterType<DelegateClass>();
            //var container = builder.Build();
            //var delegateClassFactory = container.Resolve<DelegateClass.Factory>();
            //var delegateClass = delegateClassFactory("ABC");
            //Console.WriteLine(delegateClass.getSymbol());

            Console.ReadKey();
        }

        static void Shutdown()
        {
            // our container does not implement this
            // container.Dispose();
        }

        static bool LogIn()
        {
            bool ifvalidate = gameServer.RegisterUser("assd12", "adasd123@");

            if (ifvalidate)
            {
                Console.WriteLine("Register user");
            }
            else
            {
                Console.WriteLine("Login or password are incorrect!");
            }

            return ifvalidate;
        }

        static ICharacter CreateCharacter()
        {
            ICharacter character = gameServer.CreateCharacter(new Barbarian());

            Console.WriteLine("Skill points after give out.");

            Console.WriteLine(character.Strength);
            Console.WriteLine(character.Stamina);

            return character;
        }

        static void StartGame(ICharacter character, bool ifvalidate)
        {
            bool ifGameIsStarted = gameServer.StartGame(character, ifvalidate);

            if (ifGameIsStarted)
            {
                Console.WriteLine("Start the game");
            }
            else
            {
                Console.WriteLine("Something went wrong!");
            }
        }
    }

    public class DelegateClass
    {
        public delegate DelegateClass Factory(string @string);

        public DelegateClass(string @string)
        {
            this.@string = @string;
        }

        public string @string { get; private set; }

        public string getSymbol()
        {
            return @string;
        }
    }
}
