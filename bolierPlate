using System;
using Microsoft.Xrm.Sdk;

public class MyPlugin : IPlugin
{
    public void Execute(IServiceProvider serviceProvider)
    {
        //Extract the tracing service for use in debugging sandboxed plug-ins.
        //If you are not registaring the plug-in in the sandbox, then you do not have 
        //to add any tracing service related code.

        //Casting the service provider to an ITracingService object.
        //This is necessary because the serviceProvider object can return any type of service,
        //and we need to explicitly tell the compiler that we want an ITracingService object.
        ITracingService tracingService =
            (ITracingService)serviceProvider.GetService(typeof(ITracingService));

        //Obtain the execution context from the service provider.

        IPluginExecutionContext context = (IPluginExecutionContext)
            serviceProvider.GetService(typeof(IPluginExecutionContext));

        //Ensure plugin does not run on any unwanted events
        //In this instance it triggers only on create events
        if (context.MessageName != "Create")
            return;

        //The inputParameters collection contains all the data passed in the message request
        if (context.InputParameters.Contains("Target") &&
            context.InputParameters["Target"] is Entity)
        {
            //Obtain the target entity from the input parameters.
            Entity entity = (Entity)context.InputParameters["Target"];

            //Verify that the target entity represents an entity type you are expecting.
            //For example, an account.  If not, the plug-in was not registarted correctly.
            if (entity.LogicalName != "account")
                return;

            //Obtain the organization service reference which you will need for
            //web service calls.
            IOrganizationServiceFactory serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            try
            {
                // plug-in business logic goes here
            }

            catch (Exception ex)
            {
                tracingService.Trace("MyPlugin: {0}", ex.ToString());
                throw;
            }
         }
    }
}
