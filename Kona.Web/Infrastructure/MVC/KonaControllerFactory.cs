using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using Kona.Data;
using Kona.Infrastructure;

namespace Kona.Web {
    public class KonaControllerFactory : DefaultControllerFactory {

        protected override IController GetControllerInstance(Type controllerType) {
            IController result = null;
            if (controllerType != null) {
                try {
                    KonaController controller = ObjectFactory.GetInstance(controllerType) as KonaController;
                    result = controller;

                } catch (StructureMapException) {
                    System.Diagnostics.Debug.WriteLine(ObjectFactory.WhatDoIHave());
                    throw;
                }
            }
            return result;
        }
    }
}