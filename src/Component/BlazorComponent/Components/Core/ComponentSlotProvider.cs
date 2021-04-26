﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ComponentSlotProvider
    {
        private readonly Dictionary<ComponentKey,Type> _typeConfig=new();
        private readonly Dictionary<ComponentKey,Action<Dictionary<string,object>>> _propertiesConfig=new();

        public ComponentSlotProvider Apply<TComponent, TImplementComponent>(Action<Dictionary<string, object>> propertiesAction = null)
            where TImplementComponent : TComponent
        {
            var key=ComponentKey.Get<TComponent>();
            _typeConfig.TryAdd(key, typeof(TImplementComponent));

            _propertiesConfig[key] = propertiesAction;

            return this;
        }

        public ComponentSlotProvider Apply<TComponent, TImplementComponent>(string name, Action<Dictionary<string, object>> propertiesAction = null)
            where TImplementComponent : TComponent
        {
            var key=ComponentKey.Get<TComponent>(name);
            _typeConfig.TryAdd(key, typeof(TImplementComponent));

            _propertiesConfig[key] = propertiesAction;

            return this;
        }

        public SlotComponentDescription GetSlot<TComponent>()
        {
            var type=_typeConfig.GetValueOrDefault(ComponentKey.Get<TComponent>(), typeof(TComponent));

            var properties=new Dictionary<string,object>();
            var action=_propertiesConfig.GetValueOrDefault(ComponentKey.Get<TComponent>());
            action?.Invoke(properties);

            return new SlotComponentDescription(type, properties);
        }

        public SlotComponentDescription GetSlot<TComponent>(string name)
        {
            var type=_typeConfig.GetValueOrDefault(ComponentKey.Get<TComponent>(name), typeof(TComponent));

            var properties=new Dictionary<string,object>();
            var action=_propertiesConfig.GetValueOrDefault(ComponentKey.Get<TComponent>(name));
            action?.Invoke(properties);

            return new SlotComponentDescription(type, properties);
        }
    }
}
