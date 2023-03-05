using Autofac;
using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using NetCivitaiModelManager.ViewModels;
using NetCivitaiModelManager.Views;
using ReactiveUI;
using Splat;
using Splat.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetCivitaiModelManager
{
    public static class Bootstrapper
    {
        public static AppBuilder BuildIoC(this AppBuilder appBuilder)
        {
            /*
			 * Создаем контейнер Autofac.
			 * Регистрируем сервисы и представления
			 */
            var builder = new ContainerBuilder();
            RegisterServices(builder);
            RegisterViews(builder);

            PlatformRegistrationManager.SetRegistrationNamespaces(RegistrationNamespace.Avalonia);
            RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;
            Locator.CurrentMutable.RegisterConstant(new AvaloniaActivationForViewFetcher(), typeof(IActivationForViewFetcher));
            Locator.CurrentMutable.RegisterConstant(new AutoDataTemplateBindingHook(), typeof(IPropertyBindingHook));
            // Регистрируем Autofac контейнер в Splat
            var autofacResolver = builder.UseAutofacDependencyResolver();
            builder.RegisterInstance(autofacResolver);
            // Вызываем InitializeReactiveUI(), чтобы переопределить дефолтный Service Locator
            autofacResolver.InitializeReactiveUI();
            var lifetimeScope = builder.Build();
            autofacResolver.SetLifetimeScope(lifetimeScope);
            return appBuilder;
        }

        private static void RegisterServices(ContainerBuilder builder)
        {

        }

        private static void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindowViewModel>();
            builder.RegisterType<CustomVM>();
        }
    }
}
