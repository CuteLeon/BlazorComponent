﻿using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ComponentAbstractBase<TComponent> : ComponentBase where TComponent : IHasProviderComponent
    {
        [CascadingParameter]
        public TComponent Component { get; set; }

        public ComponentCssProvider CssProvider => Component.CssProvider;

        public ComponentAbstractProvider AbstractProvider => Component.AbstractProvider;

        protected override void OnParametersSet()
        {
            if (Component == null)
            {
                throw new ArgumentException(nameof(Component));
            }
        }

        protected RenderFragment Render(Type type, Action<PropsBuilder> propsBuilderAction = null, object key = null, object data = null, Action<object> referenceCapture = null)
        {
            var metadata = AbstractProvider.GetMetadata(type, data);
            return builder =>
            {
                var sequence = 0;
                builder.OpenComponent(sequence++, metadata.Type);

                builder.AddMultipleAttributes(sequence++, metadata.Properties);

                if (propsBuilderAction != null)
                {
                    var propsBuilder = new PropsBuilder();
                    propsBuilderAction.Invoke(propsBuilder);

                    builder.AddMultipleAttributes(sequence++, propsBuilder.Props);
                }

                if (key != null)
                {
                    builder.SetKey(key);
                }

                if (referenceCapture != null)
                {
                    builder.AddComponentReferenceCapture(sequence++, referenceCapture);
                }

                builder.CloseComponent();
            };
        }

        protected RenderFragment Render(Type type, string name, Action<PropsBuilder> propsBuilderAction = null, object key = null, object data = null, string textContent = null)
        {
            var metadata = AbstractProvider.GetMetadata(type, name, data);
            return builder =>
            {
                var sequence = 0;
                builder.OpenComponent(sequence++, metadata.Type);

                builder.AddMultipleAttributes(sequence++, metadata.Properties);

                if (propsBuilderAction != null)
                {
                    var propsBuilder = new PropsBuilder();
                    propsBuilderAction.Invoke(propsBuilder);

                    builder.AddMultipleAttributes(sequence++, propsBuilder.Props);
                }

                if (key != null)
                {
                    builder.SetKey(key);
                }

                if (textContent != null)
                {
                    builder.AddAttribute(sequence++, "ChildContent", RenderText(textContent));
                }

                builder.CloseComponent();
            };
        }

        protected RenderFragment RenderText(object text)
        {
            return builder => builder.AddContent(0, text);
        }

        public EventCallback<TValue> CreateEventCallback<TValue>(Func<TValue, Task> callback)
        {
            return EventCallback.Factory.Create(Component, callback);
        }
    }
}