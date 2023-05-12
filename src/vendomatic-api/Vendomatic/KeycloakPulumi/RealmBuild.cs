namespace KeycloakPulumi;

using KeycloakPulumi.Extensions;
using KeycloakPulumi.Factories;
using Pulumi;
using Pulumi.Keycloak;
using Pulumi.Keycloak.Inputs;

class RealmBuild : Stack
{
    public RealmBuild()
    {
        var realm = new Realm("DevRealm-realm", new RealmArgs
        {
            RealmName = "DevRealm",
            RegistrationAllowed = true,
            ResetPasswordAllowed = true,
            RememberMe = true,
            EditUsernameAllowed = true
        });
        var vendomaticmanagerScope = ScopeFactory.CreateScope(realm.Id, "vendomatic_manager");
        var vendomaticoperatorScope = ScopeFactory.CreateScope(realm.Id, "vendomatic_operator");
        var vendomaticapiScope = ScopeFactory.CreateScope(realm.Id, "vendomatic_api");
        
        var vendomaticPostmanMachineClient = ClientFactory.CreateClientCredentialsFlowClient(realm.Id,
            "vendomatic.postman.machine", 
            "974d6f71-d41b-4601-9a7a-a33081f84682", 
            "Vendomatic Postman Machine",
            "https://oauth.pstmn.io");
        vendomaticPostmanMachineClient.ExtendDefaultScopes(vendomaticmanagerScope.Name,vendomaticoperatorScope.Name,vendomaticapiScope.Name);
        vendomaticPostmanMachineClient.AddAudienceMapper("vendomatic_manager");
        vendomaticPostmanMachineClient.AddAudienceMapper("vendomatic_operator");
        vendomaticPostmanMachineClient.AddAudienceMapper("vendomatic_api");
        
        var vendomaticPostmanCodeClient = ClientFactory.CreateCodeFlowClient(realm.Id,
            "vendomatic.postman.code", 
            "974d6f71-d41b-4601-9a7a-a33081f84680", 
            "Vendomatic Postman Code",
            "https://oauth.pstmn.io",
            redirectUris: null,
            webOrigins: null
            );
        vendomaticPostmanCodeClient.ExtendDefaultScopes(vendomaticmanagerScope.Name,vendomaticoperatorScope.Name,vendomaticapiScope.Name);
        vendomaticPostmanCodeClient.AddAudienceMapper("vendomatic_manager");
        vendomaticPostmanCodeClient.AddAudienceMapper("vendomatic_operator");
        vendomaticPostmanCodeClient.AddAudienceMapper("vendomatic_api");
        
        var vendomaticSwaggerClient = ClientFactory.CreateCodeFlowClient(realm.Id,
            "vendomatic.swagger", 
            "974d6f71-d41b-4601-9a7a-a33081f80687", 
            "Vendomatic Swagger",
            "https://localhost:5375",
            redirectUris: null,
            webOrigins: null
            );
        vendomaticSwaggerClient.ExtendDefaultScopes(vendomaticmanagerScope.Name,vendomaticoperatorScope.Name,vendomaticapiScope.Name);
        vendomaticSwaggerClient.AddAudienceMapper("vendomatic_manager");
        vendomaticSwaggerClient.AddAudienceMapper("vendomatic_operator");
        vendomaticSwaggerClient.AddAudienceMapper("vendomatic_api");
        
        var vendomaticBFFClient = ClientFactory.CreateCodeFlowClient(realm.Id,
            "vendomatic.bff", 
            "974d6f71-d41b-4601-9a7a-a33081f80688", 
            "Vendomatic BFF",
            "https://localhost:4378",
            redirectUris: new InputList<string>() 
                {
                "https://localhost:4378/signin-oidc",
                },
            webOrigins: new InputList<string>() 
                {
                "https://localhost:5375",
                "https://localhost:4378",
                }
            );
        vendomaticBFFClient.ExtendDefaultScopes(vendomaticapiScope.Name);
        vendomaticBFFClient.AddAudienceMapper("vendomatic_api");
        
        var bob = new User("bob", new UserArgs
        {
            RealmId = realm.Id,
            Username = "bob",
            Enabled = true,
            Email = "bob@domain.com",
            FirstName = "Smith",
            LastName = "Bobson",
            InitialPassword = new UserInitialPasswordArgs
            {
                Value = "bob",
                Temporary = true,
            },
        });

        var alice = new User("alice", new UserArgs
        {
            RealmId = realm.Id,
            Username = "alice",
            Enabled = true,
            Email = "alice@domain.com",
            FirstName = "Alice",
            LastName = "Smith",
            InitialPassword = new UserInitialPasswordArgs
            {
                Value = "alice",
                Temporary = true,
            },
        });
    }
}