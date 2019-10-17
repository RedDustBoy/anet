﻿using Anet;
using Anet.Data;
using System;
using System.Data;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AnetBuilderExtensions
    {
        /// <summary>
        /// Adds database services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TDb">The custom type of <see cref="Db"/>.</typeparam>
        /// <typeparam name="TDbConnection">The type of a db provider's connection.</typeparam>
        /// <param name="builder">The <see cref="AnetBuilder"/> to add services to.</param>
        /// <param name="connectionString">The database connection string.</param>
        /// <returns>The <see cref="AnetBuilder"/> so that additional calls can be chained.</returns>
        public static AnetBuilder AddDb<TDb, TDbConnection>(this AnetBuilder builder, string connectionString)
            where TDb : Db
            where TDbConnection : IDbConnection, new()
        {
            builder.Services.AddScoped((serviceProvider) =>
            {
                var connection = new TDbConnection() as IDbConnection;
                connection.ConnectionString = connectionString;

                var db = (TDb)Activator.CreateInstance(typeof(TDb), connection);
                return db;
            });

            return builder;
        }

        /// <summary>
        /// Adds database services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TDbConnection">The type of a db provider's connection.</typeparam>
        /// <param name="services">The <see cref="AnetBuilder"/> to add services to.</param>
        /// <param name="connectionString">The database connection string.</param>
        /// <returns>The <see cref="AnetBuilder"/> so that additional calls can be chained.</returns>
        public static AnetBuilder AddDb<TDbConnection>(this AnetBuilder services, string connectionString)
            where TDbConnection : IDbConnection, new()
        {
            return AddDb<Db, TDbConnection>(services, connectionString);
        }
    }
}