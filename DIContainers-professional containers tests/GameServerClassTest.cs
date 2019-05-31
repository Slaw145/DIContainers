using Autofac;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DIContainers;
using DIContainers.ContainerClasses;
using Moq;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace DI_Tests
{
    class GameServerClassTest
    {
        GameServer gameserver;

        Mock <ICharacterRace> characterRaceMock;
        Mock <ICharacter> characterMock;
        ICharacter character;

        [SetUp]
        public void TestSetup()
        {
            characterRaceMock = new Mock<ICharacterRace>();
            characterMock = new Mock<ICharacter>();

            character = characterMock.Object;

            //-------------Ninject----------------
            //IKernel kernel = new StandardKernel(new Bindings());
            //gameserver = kernel.Get<GameServer>();

            //-------------StructureMap-----------
            //var container = new Container(_ =>
            //{
            //    _.Scan(x =>
            //    {
            //        x.TheCallingAssembly();
            //        x.LookForRegistries();
            //    });

            //    _.For<ILoginValidator>().Use<LoginValidator>();
            //    _.For<IPasswordValidator>().Use<PasswordValidator>();
            //    _.For<ICharacterSkillPoints>().Use<CharacterSkillPoints>();
            //});

            //gameserver = container.GetInstance<GameServer>();

            //-----Castle Windsor---------
            //var container = new WindsorContainer();
            //container.Register(Component.For<ILoginValidator>().ImplementedBy<LoginValidator>());
            //container.Register(Component.For<IPasswordValidator>().ImplementedBy<PasswordValidator>());
            //container.Register(Component.For<ICharacterSkillPoints>().ImplementedBy<CharacterSkillPoints>());
            //container.Register(Component.For<GameServer>());

            //gameServer = container.Resolve<GameServer>();

            //-----Unity---------
            //IUnityContainer container = new UnityContainer();
            //container.RegisterType<ILoginValidator, LoginValidator>();
            //container.RegisterType<IPasswordValidator, PasswordValidator>();
            //container.RegisterType<ICharacterSkillPoints, CharacterSkillPoints>();

            //gameserver = container.Resolve<GameServer>();

            //-----Autofac---------
            var builder = new ContainerBuilder();
            builder.RegisterType<LoginValidator>().As<ILoginValidator>();
            builder.RegisterType<PasswordValidator>().As<IPasswordValidator>();
            builder.RegisterType<CharacterSkillPoints>().As<ICharacterSkillPoints>();

            builder.RegisterType<GameServer>();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                gameserver = scope.Resolve<GameServer>();
            }

            gameserver.ResolveInterfaces(container);
        }

        [Test]
        public void test_race_attributes()
        {
            characterRaceMock.Setup(x => x.CreateCharacterRace(It.IsAny<ICharacterRace>())).Returns(() => characterRaceMock.Object);

            Assert.IsInstanceOf(typeof(Mock), characterRaceMock);
        }

        [Test]
        public void start_the_game_test_correctly()
        {
            bool ifvalidate = gameserver.RegisterUser("assd12", "adasd123@");

            bool ifGameIsStarted = gameserver.StartGame(character, ifvalidate);

            Assert.IsTrue(ifGameIsStarted);
        }

        [Test]
        public void start_the_game_test_not_correctly_with_bad_login_and_password()
        {
            bool ifvalidate = gameserver.RegisterUser("assd", "adasd12");

            bool ifGameIsStarted = gameserver.StartGame(character, ifvalidate);

            Assert.IsFalse(ifGameIsStarted);
        }

        [Test]
        public void start_the_game_test_not_correctly_without_created_character()
        {
            bool ifvalidate = gameserver.RegisterUser("assd12", "adasd123@");

            bool ifGameIsStarted = gameserver.StartGame(null, ifvalidate);

            Assert.IsFalse(ifGameIsStarted);
        }
    }
}
