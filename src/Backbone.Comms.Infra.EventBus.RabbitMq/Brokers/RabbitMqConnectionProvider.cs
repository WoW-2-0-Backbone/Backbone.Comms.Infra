using Backbone.Comms.Infra.EventBus.RabbitMq.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Backbone.Comms.Infra.EventBus.RabbitMq.Brokers;

/// <summary>
/// Provides RabbitMQ connection
/// </summary>
public class RabbitMqConnectionProvider(IOptions<RabbitMqConnectionSettings> rabbitMqConnectionSettings) : IRabbitMqConnectionProvider
{
    private readonly ConnectionFactory _connectionFactory = new()
    {
        HostName = rabbitMqConnectionSettings.Value.HostName,
        Port =  rabbitMqConnectionSettings.Value.Port
    };

    private IConnection? _connection;

    /// <inheritdoc />
    public async ValueTask<IChannel> CreateChannelAsync()
    {
        _connection ??= await _connectionFactory.CreateConnectionAsync();

        return await _connection.CreateChannelAsync();
    }
}