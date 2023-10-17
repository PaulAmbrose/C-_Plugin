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
                //set the stage to post operation
                if (context.Stage == 40)
                {
                    //creates new entity
                    Entity updateAccount = new Entity("account");
                    //refers to the new entity created using the ID
                    updateAccount.Id = entity.Id;
                    //adds to the attribute in the entity created
                    updateAccount.Attributes["description"] = "Hello World";
                    //updating record to database
                    service.Update(updateAccount);
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
