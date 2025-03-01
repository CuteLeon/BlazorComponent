﻿using BlazorComponent;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IBlazorComponentBuilder AddBlazorComponent(this IServiceCollection services,
            Action<BlazorComponentOptions>? optionsAction = null)
        {
            if (optionsAction is not null)
            {
                services.AddOptions<BlazorComponentOptions>().Configure(optionsAction);
            }

            services.TryAddScoped<LocalStorage>();
            services.TryAddScoped<DomEventJsInterop>();
            services.TryAddScoped<Document>();
            services.TryAddScoped(serviceProvider => new Window(serviceProvider.GetRequiredService<Document>()));
            services.TryAddScoped<IPopupProvider, PopupProvider>();
            services.TryAddSingleton<IComponentIdGenerator, GuidComponentIdGenerator>();
            services.AddScoped(typeof(BDragDropService));
            services.AddSingleton<IComponentActivator, AbstractComponentActivator>();
            services.AddValidators();
            services.AddI18n();

            services.TryAddTransient<ActivatableJsModule>();
            services.TryAddTransient<OutsideClickJSModule>();
            services.TryAddTransient<ScrollStrategyJSModule>();
            services.TryAddTransient<InputJSModule>();

            return new BlazorComponentBuilder(services);
        }

        internal static IServiceCollection AddValidators(this IServiceCollection services)
        {
            var referenceAssembles = AppDomain.CurrentDomain.GetAssemblies();
            services.AddValidatorsFromAssemblies(referenceAssembles, ServiceLifetime.Scoped, includeInternalTypes: true);

            return services;
        }
    }
}
