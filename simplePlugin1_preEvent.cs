//Plugin to add "hello world" to description field in description when creating a new account

using System;
using Microsoft.Xrm.Sdk;

public class simplePlugin_postEvent : IPlugin
{
    public void Execute(IServiceProvider serviceProvider)
    {

        IPluginExecutionContext context = (IPluginExecutionContext)
            serviceProvider.GetService(typeof(IPluginExecutionContext));

        if (context.MessageName != "Create")
            return;

        if (context.InputParameters.Contains("Target") &&
            context.InputParameters["Target"] is Entity)
        {
            Entity entity = (Entity)context.InputParameters["Target"];

            if (entity.LogicalName != "account")
                return;

            IOrganizationServiceFactory serviceFactory =
                (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));

            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            try
            {
                if (context.Stage == 20)
                {
                    entity.Attributes.Add("description", "Hello World");
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
