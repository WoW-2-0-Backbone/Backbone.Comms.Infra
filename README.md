

### Usage

#### Integration

To integrate mediator pipeline install `Backbone.Comms.Infra.Abstractions`

#### Configuration

You can use either `MassTransit` or `MediatR` as mediator implementation. Exact provider of pipeline is configured separately for event and command / query execution. 

```csharp
    /// <summary>
    /// Registers communication infrastructure for components
    /// </summary>
    private static WebApplicationBuilder AddInfraComms(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(Assemblies.ToArray());
        });

        builder.Services.AddMassTransit(
            configuration =>
            {
                var serviceProvider = configuration.BuildServiceProvider();
                var jsonSerializerSettingsProvider =
                    serviceProvider.GetRequiredService<IJsonSerializationSettingsProvider>();

                configuration.UsingInMemory(
                    (context, config) =>
                    {
                        config.ConfigureEndpoints(context);

                        // Change the default serializer to NewtonsoftJson
                        config.UseNewtonsoftJsonSerializer();
                        config.UseNewtonsoftJsonDeserializer();

                        // Change the default serializer settings
                        config.ConfigureNewtonsoftJsonSerializer(settings =>
                            jsonSerializerSettingsProvider.ConfigureWithTypeHandling(settings));
                        config.ConfigureNewtonsoftJsonDeserializer(settings =>
                            jsonSerializerSettingsProvider.ConfigureWithTypeHandling(settings));
                    }
                );
            }
        );

        return builder;
    }
    ```