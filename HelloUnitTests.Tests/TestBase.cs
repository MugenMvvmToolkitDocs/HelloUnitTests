using System;
using System.Collections.Generic;
using Moq;
using MugenMvvmToolkit;
using MugenMvvmToolkit.Infrastructure;
using MugenMvvmToolkit.Interfaces;
using MugenMvvmToolkit.Interfaces.Presenters;
using MugenMvvmToolkit.Models;
using NUnit.Framework;

namespace HelloUnitTests.Tests
{
    public abstract class TestBase : UnitTestBase
    {
        #region Properties

        protected Mock<IMessagePresenter> MessagePresenterMock { get; private set; }

        protected Mock<IWrapperManager> WrapperManagerMock { get; private set; }

        protected Mock<IViewModelPresenter> ViewModelPresenterMock { get; private set; }

        protected Mock<IToastPresenter> ToastPresenterMock { get; private set; }

        protected ISerializer Serializer { get; set; }

        #endregion

        #region Methods

        [SetUp]
        public void TestInitialized()
        {
            var presenters = new List<IDynamicViewModelPresenter>();
            MessagePresenterMock = new Mock<IMessagePresenter>();
            WrapperManagerMock = new Mock<IWrapperManager>();
            ViewModelPresenterMock = new Mock<IViewModelPresenter>();
            ToastPresenterMock = new Mock<IToastPresenter>();
            ViewModelPresenterMock.Setup(presenter => presenter.DynamicPresenters)
                .Returns(() => presenters);
            Serializer = new Serializer(AppDomain.CurrentDomain.GetAssemblies());
            var container = new AutofacContainer();
            container.BindToConstant(MessagePresenterMock.Object);
            container.BindToConstant(WrapperManagerMock.Object);
            container.BindToConstant(ViewModelPresenterMock.Object);
            container.BindToConstant(ToastPresenterMock.Object);
            Initialize(container, new DefaultUnitTestModule(), (IModule)null);
            ApplicationSettings.CommandExecutionMode = CommandExecutionMode.None;
            OnTestInitialized();
        }

        protected virtual void OnTestInitialized()
        {
        }

        #endregion
    }
}
