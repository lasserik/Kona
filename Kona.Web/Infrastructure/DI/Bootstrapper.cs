using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Configuration;
using Kona.Data;
using Kona.Infrastructure;
//using Kona.SimpleSqlRepository;

public class Bootstrapper {
    public static void ConfigureStructureMap() {
        StructureMapConfiguration.AddRegistry(new SiteRegistry());
    }
}
public class SiteRegistry : Registry {
    protected override void configure() {

        ForRequestedType<IAuthenticationService>()
           .TheDefaultIsConcreteType<AspNetAuthenticationService>();

        ForRequestedType<IObjectStore>()
           .TheDefaultIsConcreteType<ObjectStore>();
        
        ForRequestedType<IPluginEngine>()
            .TheDefaultIsConcreteType<PluginEngine>()
            .CacheBy(StructureMap.Attributes.InstanceScope.Singleton);


    }

}
