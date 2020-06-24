﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alpaca.Markets
{
    public sealed partial class AlpacaTradingClient
    {
        /// <summary>
        /// Gets list of available orders from Alpaca REST API endpoint.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Read-only list of order information objects.</returns>
        [Obsolete("This method will be removed in the next major release.", false)]
        public Task<IReadOnlyList<IOrder>> ListAllOrdersAsync(
            CancellationToken cancellationToken = default) =>
            ListOrdersAsync(new ListOrdersRequest(), cancellationToken);

        /// <summary>
        /// Gets list of available orders from Alpaca REST API endpoint.
        /// </summary>
        /// <param name="request">List orders request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Read-only list of order information objects.</returns>
        public Task<IReadOnlyList<IOrder>> ListOrdersAsync(
            ListOrdersRequest request,
            CancellationToken cancellationToken = default) =>
            _httpClient.GetAsync<IReadOnlyList<IOrder>, List<JsonOrder>>(
                request.EnsureNotNull(nameof(request)).GetUriBuilder(_httpClient),
                cancellationToken, _alpacaRestApiThrottler);

        /// <summary>
        /// Creates new order for execution using Alpaca REST API endpoint.
        /// </summary>
        /// <param name="request">New order placement request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Read-only order information object for newly created order.</returns>
        public Task<IOrder> PostOrderAsync(
            NewOrderRequest request,
            CancellationToken cancellationToken = default) =>
            postOrderAsync(request.EnsureNotNull(nameof(request)).Validate().GetJsonRequest(), cancellationToken);

        /// <summary>
        /// Creates new order for execution using Alpaca REST API endpoint.
        /// </summary>
        /// <param name="orderBase">New order placement request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Read-only order information object for newly created order.</returns>
        public Task<IOrder> PostOrderAsync(
            OrderBase orderBase,
            CancellationToken cancellationToken = default) =>
            postOrderAsync(orderBase.EnsureNotNull(nameof(orderBase)).Validate().GetJsonRequest(), cancellationToken);

        private Task<IOrder> postOrderAsync(
            JsonNewOrder jsonNewOrder,
            CancellationToken cancellationToken = default) =>
            _httpClient.PostAsync<IOrder, JsonOrder, JsonNewOrder>(
                "v2/orders", jsonNewOrder, cancellationToken, _alpacaRestApiThrottler);

        /// <summary>
        /// Updates existing order using Alpaca REST API endpoint.
        /// </summary>
        /// <param name="request">Patch order request parameters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Read-only order information object for updated order.</returns>
        public Task<IOrder> PatchOrderAsync(
            ChangeOrderRequest request,
            CancellationToken cancellationToken = default) =>
            _httpClient.PatchAsync<IOrder, JsonOrder, ChangeOrderRequest>(
                request.EnsureNotNull(nameof(request)).Validate().GetEndpointUri(),
                request, _alpacaRestApiThrottler, cancellationToken);

        /// <summary>
        /// Get single order information by client order ID from Alpaca REST API endpoint.
        /// </summary>
        /// <param name="clientOrderId">Client order ID for searching.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Read-only order information object.</returns>
        public Task<IOrder> GetOrderAsync(
            String clientOrderId,
            CancellationToken cancellationToken = default) =>
            _httpClient.GetAsync<IOrder, JsonOrder>(
                new UriBuilder(_httpClient.BaseAddress)
                {
                    Path = "v2/orders:by_client_order_id",
                    Query = new QueryBuilder()
                        .AddParameter("client_order_id", clientOrderId)
                },
                cancellationToken, _alpacaRestApiThrottler);

        /// <summary>
        /// Get single order information by server order ID from Alpaca REST API endpoint.
        /// </summary>
        /// <param name="orderId">Server order ID for searching.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Read-only order information object.</returns>
        public Task<IOrder> GetOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default) =>
            _httpClient.GetAsync<IOrder, JsonOrder>(
                $"v2/orders/{orderId:D}", cancellationToken, _alpacaRestApiThrottler);

        /// <summary>
        /// Deletes/cancel order on server by server order ID using Alpaca REST API endpoint.
        /// </summary>
        /// <param name="orderId">Server order ID for cancelling.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>True</c> if order cancellation was accepted.</returns>
        public Task<Boolean> DeleteOrderAsync(
            Guid orderId,
            CancellationToken cancellationToken = default) =>
            _httpClient.DeleteAsync(
                $"v2/orders/{orderId:D}", cancellationToken, _alpacaRestApiThrottler);

        /// <summary>
        /// Deletes/cancel all open orders using Alpaca REST API endpoint.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>List of order cancellation status objects.</returns>
        public Task<IReadOnlyList<IOrderActionStatus>> DeleteAllOrdersAsync(
            CancellationToken cancellationToken = default) =>
            _httpClient.DeleteAsync<IReadOnlyList<IOrderActionStatus>, List<JsonOrderActionStatus>>(
                    "v2/orders", cancellationToken, _alpacaRestApiThrottler);
    }
}
