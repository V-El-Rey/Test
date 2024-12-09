using Assets.App.Scripts.Core.MVC.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Scripts.Core.Services.Services
{
    public class ModelsService
    {
        //debug public
        public Dictionary<Type, IRuntimeModel> Models = new();

        public void RegisterModel(IRuntimeModel model)
        {
            if (!TryRegisterModel(model))
                Debug.LogWarning($"Unable to register model of type {model.GetType()}");
        }

        public bool TryGetModel<T>(out T model)
        {
            if (Models.TryGetValue(typeof(T), out var result))
            {
                model = (T)result;
                return true;
            }
            else
            {
                model = default(T);
                return false;
            }
        }

        public void ClearModels()
        {
            foreach(var model in Models.Values) 
            {
                model.Clear();
            }
        }

        private bool TryRegisterModel(IRuntimeModel model)
        {
            return Models.TryAdd(model.GetType(), model);
        }
    }
}